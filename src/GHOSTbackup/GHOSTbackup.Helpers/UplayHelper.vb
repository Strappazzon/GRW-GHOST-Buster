Imports Microsoft.Win32
Imports System.IO
Imports GHOSTbackup.BackupHelper
Imports GHOSTbackup.Var

Public Class UplayHelper
    Public Shared Sub GetUplayInstall()
        'Get Uplay install directory
        Using UplayRegKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher", False)
            Try
                UplayPath = UplayRegKey.GetValue("InstallDir")

                If UplayPath <> Nothing Then
                    Logger.Log("[INFO] Uplay is installed in: " & UplayPath)
                Else
                    Logger.Log("[WARNING] Uplay is not installed (""InstallDir"" is Null or Empty). Uplay is required to launch and play Wildlands.")
                End If

            Catch ex As Exception
                Logger.Log("[ERROR] Uplay is not installed: " & ex.Message().TrimEnd("."c) & "." & " Uplay is required to launch and play Wildlands.")
            End Try
        End Using
    End Sub

    Public Shared Sub DisableCloudSync()
        'Disable Uplay cloud save synchronization
        Try
            Dim UplayYamlPath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Ubisoft Game Launcher\settings.yml"
            Logger.Log("[INFO] Parsing and evaluating Uplay settings file: " & UplayYamlPath)
            Dim ParsedUplayYaml As String = File.ReadAllText(UplayYamlPath)

            If ParsedUplayYaml.Contains("syncsavegames: true") Then
                'If cloud save sync is enabled
                'Check if Uplay is running or not before editing its settings file
                Dim UplayProc = Process.GetProcessesByName("upc")
                If UplayProc.Count > 0 Then
                    CustomMsgBox.Show("{\rtf1 You must {\b quit Uplay before restoring a backup} because you chose to let GHOST Buster disable cloud save synchronization for you.}", "Cannot restore", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                Else
                    'Backup Uplay settings file without overwriting an existing backup
                    Logger.Log("[INFO] Backing up Uplay settings file to " & UplayYamlPath & ".bak")
                    File.Copy(UplayYamlPath, UplayYamlPath & ".bak", False)

                    'Disable cloud save sync
                    Dim ReplacedUplayYaml As String = ParsedUplayYaml.Replace("syncsavegames: true", "syncsavegames: false")
                    File.WriteAllText(UplayYamlPath, ReplacedUplayYaml)
                    Logger.Log("[INFO] Uplay cloud save synchronization disabled.")

                    'Launch Uplay again...
                    If UplayPath <> Nothing Then
                        Process.Start(UplayPath & "Uplay.exe")
                    End If

                    '...and start the restore process
                    RestoreBackup()
                End If
            Else
                'Don't replace anything
                Logger.Log("[INFO] Uplay cloud synchronization is already disabled.")

                'Start the restore process
                RestoreBackup()
            End If

        Catch ex As Exception
            'Don't let GHOST Buster disable cloud save sync until the user enables the setting again
            Form1.DisableCloudSyncChkBox.Checked = False

            Logger.Log("[ERROR] Parsing of ""settings.yml"" failed: " & ex.Message())
            CustomMsgBox.Show("{\rtf1 ""Let GHOST Buster disable cloud save synchronization"" setting has been {\b disabled because an error occurred} while trying to parse Uplay settings file. \line\line Make sure to {\b DISABLE} cloud save " _
                              & "synchronization from Uplay (Settings -> Untick ""Enable cloud save synchronization for supported games"") before launching Wildlands, otherwise the restored save games will be {\b OVERWRITTEN} with the old ones from the cloud!",
                              "Parsing failed",
                              MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

            'Start the restore process anyway
            RestoreBackup()
        End Try
    End Sub

    Public Shared Sub EnableCloudSync()
        Try
            'Enable Uplay cloud save synchronization
            Dim UplayYamlPath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Ubisoft Game Launcher\settings.yml"
            Logger.Log("[INFO] Parsing and evaluating Uplay settings file: " & UplayYamlPath)
            Dim ParsedUplayYaml As String = File.ReadAllText(UplayYamlPath)

            If ParsedUplayYaml.Contains("syncsavegames: false") Then
                'If cloud save sync is disabled
                'Check if Uplay is running or not before editing its settings file
                Dim UplayProc = Process.GetProcessesByName("upc")
                If UplayProc.Count > 0 Then
                    CustomMsgBox.Show("{\rtf1 GHOST Buster was unable to enable cloud save synchronization {\b because Uplay is running.} Please re-enable it manually from Uplay (Settings -> Tick ""Enable cloud save synchronization for supported games"").",
                                      "Cannot enable cloud sync",
                                      MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                Else
                    'Enable cloud save sync
                    Dim ReplacedUplayYaml As String = ParsedUplayYaml.Replace("syncsavegames: false", "syncsavegames: true")
                    File.WriteAllText(UplayYamlPath, ReplacedUplayYaml)
                    Logger.Log("[INFO] Uplay cloud save synchronization re-enabled.")
                End If
            Else
                'Don't replace anything
                Logger.Log("[INFO] Uplay cloud synchronization is already enabled.")
            End If
        Catch ex As Exception
            Logger.Log("[ERROR] Parsing of ""settings.yml"" failed: " & ex.Message())
            CustomMsgBox.Show("{\rtf1 {\b An error occurred} while trying to parse Uplay settings file. Please re-enable cloud save synchronization manually from Uplay (Settings -> Tick ""Enable cloud save synchronization for supported games"").",
                              "Parsing failed",
                              MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
    End Sub
End Class

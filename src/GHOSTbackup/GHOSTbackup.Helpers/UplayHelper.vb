#Region "Copyright (c) 2019 - 2020 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster"
''
'' GHOST Buster - Ghost Recon Wildlands backup utility
''
'' Copyright (c) 2019 - 2020 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster
''
'' Permission is hereby granted, free of charge, to any person obtaining a copy
'' of this software and associated documentation files (the "Software"), to deal
'' in the Software without restriction, including without limitation the rights
'' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
'' copies of the Software, and to permit persons to whom the Software is
'' furnished to do so, subject to the following conditions:
''
'' The above copyright notice and this permission notice shall be included in all
'' copies or substantial portions of the Software.
''
'' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
'' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
'' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
'' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
'' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
'' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
'' SOFTWARE.
''
#End Region

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
                    CustomMsgBox.Show(Localization.GetString("msgbox_quit_before_restore_sync"), Localization.GetString("msgbox_quit_before_restore_sync_title"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
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
            CustomMsgBox.Show(Localization.GetString("msgbox_disable_sync_error"), Localization.GetString("msgbox_parsing_error_title"), MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

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
                    CustomMsgBox.Show(Localization.GetString("msgbox_enable_sync_uplay_error"), Localization.GetString("msgbox_enable_sync_uplay_error_title"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
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
            CustomMsgBox.Show(Localization.GetString("msgbox_enable_sync_error"), Localization.GetString("msgbox_parsing_error_title"), MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
    End Sub
End Class

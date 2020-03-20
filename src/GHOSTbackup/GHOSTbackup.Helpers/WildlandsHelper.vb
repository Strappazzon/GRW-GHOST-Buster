Imports Microsoft.Win32
Imports System.IO
Imports GHOSTbackup.ProcessHelper
Imports GHOSTbackup.Var

Public Class WildlandsHelper
    Public Shared Sub GetWildlandsInstall()
        'Get Wildlands install directory
        If Form1.SettingsNonUplayVersionChkBox.Checked = True Then
            If File.Exists(Form1.SettingsCustomExeTextBox.Text) Then
                GamePath = Directory.GetParent(Form1.SettingsCustomExeTextBox.Text).ToString() & "\"
                Form1.PlayGameBtn.Enabled = True
                Logger.Log("[INFO] Wildlands is installed in: " & GamePath & " (Non-Uplay version).")
                StartProcessTimer()
            Else
                'Disable "I'm not using the Uplay version of Wildlands"
                Form1.SettingsNonUplayVersionChkBox.Checked = False
                Form1.PlayGameBtn.Text = "Ghost Recon Wildlands not found"
                Logger.Log("[WARNING] Custom Wildlands executable " & Form1.SettingsCustomExeTextBox.Text & " not found.")
                Banner.Show(48, "The selected Wildlands executable could not be found.")
            End If
        Else
            Using GameRegKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher\Installs\1771", False)
                Try
                    GamePath = TryCast(GameRegKey.GetValue("InstallDir"), String).Replace("/", "\") 'Replace any forward slashes with backward slashes

                    If GamePath <> Nothing Then
                        Form1.PlayGameBtn.Enabled = True
                        Logger.Log("[INFO] Wildlands is installed in: " & GamePath)
                        StartProcessTimer()
                    Else
                        Form1.PlayGameBtn.Text = "Ghost Recon Wildlands is not installed"
                        Logger.Log("[WARNING] Wildlands is not installed (""InstallDir"" is Null or Empty).")
                    End If

                Catch ex As Exception
                    Form1.PlayGameBtn.Text = "Ghost Recon Wildlands is not installed"
                    Logger.Log("[ERROR] Wildlands is not installed: " & ex.Message())
                End Try
            End Using
        End If
    End Sub
End Class

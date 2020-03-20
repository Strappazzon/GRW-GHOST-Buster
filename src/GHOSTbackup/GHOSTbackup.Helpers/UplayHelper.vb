Imports Microsoft.Win32
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
End Class

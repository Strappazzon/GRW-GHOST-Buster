﻿#Region "Copyright (c) 2019 Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster"
''
'' GHOST Buster - Ghost Recon Wildlands backup utility
''
'' Copyright (c) 2019 Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster
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

Imports System.IO
Imports GHOSTbackup.ProcessHelper
Imports GHOSTbackup.UI
Imports Microsoft.Win32

Public Class WildlandsHelper
    Public Shared Property GamePath As String = Nothing

    Public Shared Sub GetWildlandsInstall()
        If Form1.SettingsNonUplayVersionChkBox.Checked = True Then
            'Check if custom Wildlands executable exists
            If File.Exists(Form1.SettingsCustomExeTextBox.Text) Then
                GamePath = Directory.GetParent(Form1.SettingsCustomExeTextBox.Text).ToString() & "\"
                Form1.PlayGameBtn.Enabled = True

                Logger.Log("[INFO] Wildlands is installed in: " & GamePath & " (Non-Uplay version).")

                'Start checking if the game is running or not
                StartProcessTimer()
            Else
                'Disable "Manually locate installed game" option
                Form1.SettingsNonUplayVersionChkBox.Checked = False
                Form1.PlayGameBtn.Text = Localization.GetString("ui_play_disabled_404")

                Logger.Log("[WARNING] Custom Wildlands executable " & Form1.SettingsCustomExeTextBox.Text & " not found.")
                Banner.Show(Localization.GetString("banner_customexe_404_error"), BannerIcon.Warning)
            End If
        Else
            'Look for the Uplay version of Wildlands
            Using GameRegKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher\Installs\1771", False)
                Try
                    GamePath = GameRegKey.GetValue("InstallDir").Replace("/"c, "\"c)

                    If GamePath <> Nothing Then
                        Form1.PlayGameBtn.Enabled = True

                        Logger.Log("[INFO] Wildlands is installed in: " & GamePath)

                        'Start checking if the game is running or not
                        StartProcessTimer()
                    Else
                        Form1.PlayGameBtn.Text = Localization.GetString("ui_play_disabled")

                        Logger.Log("[WARNING] Wildlands is not installed (""InstallDir"" is Null or Empty).")
                    End If
                Catch ex As Exception
                    Form1.PlayGameBtn.Text = Localization.GetString("ui_play_disabled")
                    Logger.Log("[ERROR] Wildlands is not installed: " & ex.Message())
                End Try
            End Using
        End If
    End Sub
End Class

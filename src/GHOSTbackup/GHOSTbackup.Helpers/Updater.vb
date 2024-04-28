#Region "Copyright (c) 2019 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster"
''
'' GHOST Buster - Ghost Recon Wildlands backup utility
''
'' Copyright (c) 2019 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster
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

Imports System.Net.Http
Imports GHOSTbackup.UI

Public Class Updater
    Public Shared ReadOnly VersionString As String = My.Application.Info.Version.ToString().Remove(5)
    Private Shared ReadOnly VersionCode As Integer = My.Application.Info.Version.Revision
    Private Shared ReadOnly VersionURI As Uri = New Uri("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/version")

    'Check for updates
    '//docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
    Public Shared Async Sub CheckUpdates()
        If Form1.SettingsCheckUpdatesChkBox.Checked = True Then
            Try
                Using Updater As HttpClient = New HttpClient()
                    Updater.DefaultRequestHeaders.Add("User-Agent", "GHOST Buster (+https://strappazzon.xyz/GRW-GHOST-Buster)")
                    Dim FetchedVer As Integer = Integer.Parse(Await Updater.GetStringAsync(VersionURI))

                    'Compare downloaded GHOST Buster version number with the current one
                    Select Case FetchedVer
                        Case VersionCode
                            Logger.Log("[INFO] GHOST Buster is up to date.")
                        Case > VersionCode
                            Logger.Log("[INFO] New version of GHOST Buster is available.")
                            CustomMsgBox.Show(
                                Localization.GetString("msgbox_update_available"),
                                Localization.GetString("msgbox_update_available_title"),
                                CustomMsgBoxButtons.YesNoCancel,
                                CustomMsgBoxIcon.Question,
                                CustomMsgBoxDefaultButton.Button2
                            )
                            If CustomMsgBox.DialogResult = DialogResult.Yes Then
                                Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest")
                            End If
                        Case < VersionCode
                            Logger.Log("[INFO] The version in use is greater than the one currently available.")
                        Case Else
                            Exit Select
                    End Select
                End Using
            Catch ex As Exception
                Logger.Log("[ERROR] Unable to check for updates: " & ex.Message())
                Banner.Show(Localization.GetString("banner_update_error"), BannerIcon.Warning)
            End Try
        End If
    End Sub
End Class

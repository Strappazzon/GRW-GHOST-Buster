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

Imports GHOSTbackup.UI
Imports System.Net

Public Class Updater
    Private Shared ReadOnly VersionCode As Integer = 16

    Public Shared Sub CheckUpdates()
        'Check for updates
        '//docs.microsoft.com/en-us/dotnet/api/system.net.downloadstringcompletedeventargs
        If Form1.SettingsCheckUpdatesChkBox.Checked = True Then
            Using Updater As WebClient = New WebClient()
                Updater.Headers.Add("User-Agent", "GHOST Buster (+https://strappazzon.xyz/GRW-GHOST-Buster)")
                Dim VersionURI As Uri = New Uri("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/version")
                Updater.DownloadStringAsync(VersionURI)
                'Call updater_DownloadStringCompleted when the download completes
                AddHandler Updater.DownloadStringCompleted, AddressOf Updater_DownloadStringCompleted
            End Using
        End If
    End Sub

    Private Shared Sub Updater_DownloadStringCompleted(sender As Object, e As DownloadStringCompletedEventArgs)
        If e.Error Is Nothing Then
            Dim FetchedVer As Integer = e.Result

            'Compare downloaded GHOST Buster version number with the current one
            If FetchedVer = VersionCode Then
                Logger.Log("[INFO] GHOST Buster is up to date.")
            ElseIf FetchedVer > VersionCode Then
                Logger.Log("[INFO] New version of GHOST Buster is available.")
                CustomMsgBox.Show(Localization.GetString("msgbox_update_available"), Localization.GetString("msgbox_update_available_title"), CustomMsgBoxButtons.YesNoCancel, CustomMsgBoxIcon.Question, CustomMsgBoxDefaultButton.Button2)
                If CustomMsgBox.DialogResult = DialogResult.Yes Then
                    Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest")
                End If
            ElseIf FetchedVer < VersionCode Then
                Logger.Log("[INFO] The version in use is greater than the one currently available.")
            End If
        Else
            Logger.Log("[ERROR] Unable to check for updates: " & e.Error.Message())
            Banner.Show(Localization.GetString("banner_update_error"), BannerIcon.Warning)
        End If
    End Sub
End Class

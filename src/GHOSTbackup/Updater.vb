Imports System.Net

Public Class Updater
    Private Const VersionCode As Short = 16

    Public Shared Sub CheckUpdates()
        'Check for updates
        '//docs.microsoft.com/en-us/dotnet/api/system.net.downloadstringcompletedeventargs
        If Form1.CheckUpdatesChkBox.Checked = True Then
            Using Updater As New WebClient
                Updater.Headers.Add("User-Agent", "GHOST Buster (+https://strappazzon.xyz/GRW-GHOST-Buster)")
                Dim VersionURI As New Uri("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/version")
                Updater.DownloadStringAsync(VersionURI)
                'Call updater_DownloadStringCompleted when the download completes
                AddHandler Updater.DownloadStringCompleted, AddressOf Updater_DownloadStringCompleted
            End Using
        End If
    End Sub

    Private Shared Sub Updater_DownloadStringCompleted(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
        If e.Error Is Nothing Then
            Dim FetchedVer As Short = e.Result

            'Compare downloaded GHOST Buster version number with the current one
            If FetchedVer = VersionCode Then
                Logger.Log("[INFO] GHOST Buster is up to date.")
            ElseIf FetchedVer > VersionCode Then
                Logger.Log("[INFO] New version of GHOST Buster is available.")
                CustomMsgBox.Show("{\rtf1 A newer version of GHOST Buster is available. Do you want to {\b visit the download page} now?}", "Update available", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If CustomMsgBox.DialogResult = DialogResult.Yes Then
                    Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest")
                End If
            ElseIf FetchedVer < VersionCode Then
                Logger.Log("[INFO] The version in use is greater than the one currently available.")
            End If
        Else
            Logger.Log("[ERROR] Unable to check for updates: " & e.Error.Message())
            Banner.Show(48, "Unable to check for updates. Please check the logs for more details.")
        End If
    End Sub
End Class

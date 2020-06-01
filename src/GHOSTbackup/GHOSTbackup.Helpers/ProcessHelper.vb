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
Imports GHOSTbackup.BackupHelper

Public Class ProcessHelper
    Private Shared WithEvents CheckProcessTimer As Timer = New Timer() With {.Interval = 500}
    Public Shared Property IsGameRunning As Boolean = False

    Public Shared Sub StartProcessTimer()
        CheckProcessTimer.Start()
    End Sub

    Private Shared Sub CheckProcessTimer_Tick(sender As Object, e As EventArgs) Handles CheckProcessTimer.Tick
        Dim WildlandsProc = Process.GetProcessesByName("GRW")
        If WildlandsProc.Count > 0 Then
            IsGameRunning = True
            Form1.PlayGameBtn.Enabled = False
            Form1.RestoreManageContextMenuItem.Enabled = False
        Else
            IsGameRunning = False
            Form1.PlayGameBtn.Enabled = True
            Form1.RestoreManageContextMenuItem.Enabled = True

            If IsBackupRunning = True Then
                StopBackup()
                Logger.Log("[INFO] Wildlands has been closed or crashed. Backup interrupted.")
                CustomMsgBox.Show(Localization.GetString("msgbox_wildlands_closed_crashed"), Localization.GetString("msgbox_wildlands_closed_crashed_title"), CustomMsgBoxButtons.OKCancel, CustomMsgBoxIcon.Warning)
            End If
        End If
    End Sub
End Class

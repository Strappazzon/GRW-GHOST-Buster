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

Imports GHOSTbackup.BackupHelper
Imports GHOSTbackup.Var

Public Class ProcessHelper
    Private Shared WithEvents CheckProcessTimer As New Timer()

    Public Shared Sub StartProcessTimer()
        CheckProcessTimer.Interval = 500
        CheckProcessTimer.Start()
    End Sub

    Public Shared Sub CheckProcessTimer_Tick(sender As Object, e As EventArgs) Handles CheckProcessTimer.Tick
        Dim WildlandsProc = Process.GetProcessesByName("GRW")
        If WildlandsProc.Count > 0 Then
            IsGameRunning = True
            Form1.PlayGameBtn.Enabled = False
        Else
            IsGameRunning = False
            Form1.PlayGameBtn.Enabled = True
            If IsBackupRunning = True Then
                StopBackup()
                Logger.Log("[INFO] Wildlands has been closed or crashed. Backup interrupted.")
                CustomMsgBox.Show("{\rtf1 Wildlands {\b has been closed or crashed}, as a result the backup process has been interrupted.}", "Wildlands is no longer running", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
End Class

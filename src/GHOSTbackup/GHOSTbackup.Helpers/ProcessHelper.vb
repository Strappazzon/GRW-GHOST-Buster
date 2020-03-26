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

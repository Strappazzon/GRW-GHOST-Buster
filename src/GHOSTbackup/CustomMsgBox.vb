Public Class CustomMsgBox
    Private Sub CustomMsgBox_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.Closing
        If BackupDirsDropdownCombo.Visible = True Then
            BackupDirsDropdownCombo.Visible = False
        End If
    End Sub

    Private Sub MessageRTF_MouseDown(sender As Object, e As MouseEventArgs) Handles MessageRTF.MouseDown
        'An hack to disable the caret
        '//www.codeproject.com/Answers/272781/How-to-hide-the-caret-in-RichTextBox#answer1
        MessageRTF.SelectionLength = 0
        MessageRTF.SelectionStart = MessageRTF.TextLength
        ActiveControl = CancelLabel
    End Sub

    Private Sub MessageRTF_KeyDown(sender As Object, e As KeyEventArgs) Handles MessageRTF.KeyDown
        MessageRTF.SelectionLength = 0
        MessageRTF.SelectionStart = MessageRTF.TextLength
        ActiveControl = CancelLabel
    End Sub

    Private Sub BackupDirsDropdownCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles BackupDirsDropdownCombo.SelectedIndexChanged
        MessageRTF.Rtf = "{\rtf1 Restoring a backup will copy the save files over from the backup folder: " & Form1.BackupLocTextBox.Text.Replace("\", "\\") & "\\" & BackupDirsDropdownCombo.SelectedItem.ToString().Substring(0, 13) _
                         & "\line\line and will {\b OVERWRITE} the existing save files inside the game folder: " & Form1.SavegamesLocTextBox.Text.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}"
    End Sub
End Class

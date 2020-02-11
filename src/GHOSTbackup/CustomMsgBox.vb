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
End Class

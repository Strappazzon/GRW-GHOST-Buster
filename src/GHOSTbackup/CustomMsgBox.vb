Public Class CustomMsgBox
    Private Sub messageRTF_MouseDown(sender As Object, e As MouseEventArgs) Handles messageRTF.MouseDown
        'An hack to disable the caret
        '//www.codeproject.com/Answers/272781/How-to-hide-the-caret-in-RichTextBox#answer1
        messageRTF.SelectionLength = 0
        messageRTF.SelectionStart = messageRTF.TextLength
        ActiveControl = CancelButton
    End Sub
End Class

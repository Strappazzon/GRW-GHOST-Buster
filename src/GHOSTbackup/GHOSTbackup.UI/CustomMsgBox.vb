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

Public Class CustomMsgBox
    'Custom MessageBox Class
    '//docs.microsoft.com/en-us/dotnet/api/system.windows.forms.form.dialogresult

#Region "Show Methods"
    Public Overloads Shared Sub Show(ByVal Message As String, ByVal Title As String, ByVal Buttons As MessageBoxButtons, ByVal Icon As MessageBoxIcon, ByVal DefaultButton As MessageBoxDefaultButton)
        'The content of Message MUST be written in Rich Text Format
        '//www.oreilly.com/library/view/rtf-pocket-guide/9781449302047/ch01.html
        'When printing a string variable that is a path or otherwise contains any backward slashes they MUST be escaped with .Replace("\", "\\")
        CustomMsgBox.MessageRTF.Rtf = Message
        CustomMsgBox.TitleLabel.Text = Title

        If Buttons = MessageBoxButtons.OK OrElse Buttons = MessageBoxButtons.OKCancel Then
            '[OK] or [OK][Cancel] dialog
            CustomMsgBox.RightButton.DialogResult = DialogResult.OK
            CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
            'Hide [Yes] button and make [No] button the [OK] button
            CustomMsgBox.LeftButton.Visible = False
            CustomMsgBox.RightButton.Text = "OK"
            CustomMsgBox.AcceptButton = CustomMsgBox.RightButton
            CustomMsgBox.CancelButton = CustomMsgBox.CancelLabel

            Select Case DefaultButton
                Case MessageBoxDefaultButton.Button1
                    '[OK] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.RightButton
                Case MessageBoxDefaultButton.Button2, MessageBoxDefaultButton.Button3
                    '[Cancel] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.CancelLabel
                Case Else
                    Exit Select
            End Select
        ElseIf Buttons = MessageBoxButtons.YesNo OrElse MessageBoxButtons.YesNoCancel Then
            '[Yes][No] or [Yes][No][Cancel] dialog
            CustomMsgBox.LeftButton.DialogResult = DialogResult.Yes
            CustomMsgBox.RightButton.DialogResult = DialogResult.No
            CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
            'Show [Yes] button and make [OK] button the [No] button
            CustomMsgBox.LeftButton.Visible = True
            CustomMsgBox.RightButton.Text = "No"
            CustomMsgBox.AcceptButton = CustomMsgBox.LeftButton
            CustomMsgBox.CancelButton = CustomMsgBox.CancelLabel

            Select Case DefaultButton
                Case MessageBoxDefaultButton.Button1
                    '[Yes] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.LeftButton
                Case MessageBoxDefaultButton.Button2
                    '[No] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.RightButton
                Case MessageBoxDefaultButton.Button3
                    '[Cancel] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.CancelLabel
                Case Else
                    Exit Select
            End Select
        End If

        Select Case Icon
            Case MessageBoxIcon.Error, MessageBoxIcon.Hand, MessageBoxIcon.Stop
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Error
            Case MessageBoxIcon.Exclamation, MessageBoxIcon.Warning
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Warning
            Case MessageBoxIcon.Question
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Question
            Case Else
                Exit Select
        End Select

        'Display the custom MessageBox as a modal
        CustomMsgBox.ShowDialog()
    End Sub

    Public Overloads Shared Sub Show(ByVal Message As String, ByVal Title As String, ByVal Buttons As MessageBoxButtons, ByVal Icon As MessageBoxIcon)
        'The content of Message MUST be written in Rich Text Format
        '//www.oreilly.com/library/view/rtf-pocket-guide/9781449302047/ch01.html
        'When printing a string variable that is a path or otherwise contains any backward slashes they MUST be escaped with .Replace("\", "\\")
        CustomMsgBox.MessageRTF.Rtf = Message
        CustomMsgBox.TitleLabel.Text = Title

        If Buttons = MessageBoxButtons.OK OrElse Buttons = MessageBoxButtons.OKCancel Then
            '[OK] or [OK][Cancel] dialog
            CustomMsgBox.RightButton.DialogResult = DialogResult.OK
            CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
            'Hide [Yes] button and make [No] button the [OK] button
            CustomMsgBox.LeftButton.Visible = False
            CustomMsgBox.RightButton.Text = "OK"
            CustomMsgBox.AcceptButton = CustomMsgBox.RightButton
            CustomMsgBox.CancelButton = CustomMsgBox.CancelLabel
        ElseIf Buttons = MessageBoxButtons.YesNo OrElse MessageBoxButtons.YesNoCancel Then
            '[Yes][No] or [Yes][No][Cancel] dialog
            CustomMsgBox.LeftButton.DialogResult = DialogResult.Yes
            CustomMsgBox.RightButton.DialogResult = DialogResult.No
            CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
            'Show [Yes] button and make [OK] button the [No] button
            CustomMsgBox.LeftButton.Visible = True
            CustomMsgBox.RightButton.Text = "No"
            CustomMsgBox.AcceptButton = CustomMsgBox.LeftButton
            CustomMsgBox.CancelButton = CustomMsgBox.CancelLabel
        End If

        'Set [Cancel] button as the default button
        CustomMsgBox.ActiveControl = CustomMsgBox.CancelLabel

        Select Case Icon
            Case MessageBoxIcon.Error, MessageBoxIcon.Hand, MessageBoxIcon.Stop
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Error
            Case MessageBoxIcon.Exclamation, MessageBoxIcon.Warning
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Warning
            Case MessageBoxIcon.Question
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Question
            Case Else
                Exit Select
        End Select

        'Display the custom MessageBox as a modal
        CustomMsgBox.ShowDialog()
    End Sub
#End Region

    Private Sub CustomMsgBox_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.Closing
        'Hide the dropdown menu, if visible, to avoid displaying it again if not necessary
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
        'An hack to disable the caret
        MessageRTF.SelectionLength = 0
        MessageRTF.SelectionStart = MessageRTF.TextLength
        ActiveControl = CancelLabel
    End Sub

    Private Sub BackupDirsDropdownCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles BackupDirsDropdownCombo.SelectedIndexChanged
        'Update backup folder path when selecting which backup to restore
        MessageRTF.Rtf = "{\rtf1 Restoring a backup will copy the save files over from the backup folder: " & Form1.BackupLocTextBox.Text.Replace("\", "\\") & "\\" & BackupDirsDropdownCombo.SelectedItem.ToString().Substring(0, 13) _
                         & "\line\line and will {\b OVERWRITE} the existing save files inside the game folder: " & Form1.SavegamesLocTextBox.Text.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}"
    End Sub
End Class

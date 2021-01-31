#Region "Copyright (c) 2019 - 2021 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster"
''
'' GHOST Buster - Ghost Recon Wildlands backup utility
''
'' Copyright (c) 2019 - 2021 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster
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

Public Class CustomMsgBox
    'Custom MessageBox Class
    '//docs.microsoft.com/en-us/dotnet/api/system.windows.forms.form.dialogresult

#Region "Show Methods"
    Public Overloads Shared Sub Show(ByVal Message As String, ByVal Title As String, ByVal Buttons As CustomMsgBoxButtons, ByVal Icon As CustomMsgBoxIcon, ByVal DefaultButton As CustomMsgBoxDefaultButton)
        'The content of Message MUST be written in Rich Text Format
        '//www.oreilly.com/library/view/rtf-pocket-guide/9781449302047/ch01.html
        'When printing a string variable that is a path or otherwise contains any backward slashes they MUST be escaped with .Replace("\", "\\")
        CustomMsgBox.MessageRTF.Rtf = Message
        CustomMsgBox.TitleLabel.Text = Title

        Select Case Buttons
            Case CustomMsgBoxButtons.OK, CustomMsgBoxButtons.OKCancel
                '[OK] or [OK][Cancel] dialog
                CustomMsgBox.RightButton.DialogResult = DialogResult.OK
                CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
                'Hide [Yes] button and make [No] button the [OK] button
                CustomMsgBox.LeftButton.Visible = False
                CustomMsgBox.RightButton.Text = Localization.GetString("msgbox_btn_ok")
                CustomMsgBox.AcceptButton = CustomMsgBox.RightButton
                CustomMsgBox.CancelButton = CustomMsgBox.CancelLabel

                Select Case DefaultButton
                    Case CustomMsgBoxDefaultButton.Button1
                        '[OK] button
                        CustomMsgBox.ActiveControl = CustomMsgBox.RightButton
                    Case CustomMsgBoxDefaultButton.Button2, CustomMsgBoxDefaultButton.Button3
                        '[Cancel] button
                        CustomMsgBox.ActiveControl = CustomMsgBox.CancelLabel
                    Case Else
                        Exit Select
                End Select

            Case CustomMsgBoxButtons.YesNo, CustomMsgBoxButtons.YesNoCancel
                '[Yes][No] or [Yes][No][Cancel] dialog
                CustomMsgBox.LeftButton.DialogResult = DialogResult.Yes
                CustomMsgBox.RightButton.DialogResult = DialogResult.No
                CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
                'Show [Yes] button and make [OK] button the [No] button
                CustomMsgBox.LeftButton.Visible = True
                CustomMsgBox.RightButton.Text = Localization.GetString("msgbox_btn_no")
                CustomMsgBox.AcceptButton = CustomMsgBox.LeftButton
                CustomMsgBox.CancelButton = CustomMsgBox.CancelLabel

                Select Case DefaultButton
                    Case CustomMsgBoxDefaultButton.Button1
                        '[Yes] button
                        CustomMsgBox.ActiveControl = CustomMsgBox.LeftButton
                    Case CustomMsgBoxDefaultButton.Button2
                        '[No] button
                        CustomMsgBox.ActiveControl = CustomMsgBox.RightButton
                    Case CustomMsgBoxDefaultButton.Button3
                        '[Cancel] button
                        CustomMsgBox.ActiveControl = CustomMsgBox.CancelLabel
                    Case Else
                        Exit Select
                End Select
            Case Else
                Exit Select
        End Select

        Select Case Icon
            Case CustomMsgBoxIcon.Hand, CustomMsgBoxIcon.Stop, CustomMsgBoxIcon.Error
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Error
            Case CustomMsgBoxIcon.Exclamation, CustomMsgBoxIcon.Warning
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Warning
            Case CustomMsgBoxIcon.Question
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Question
            Case Else
                Exit Select
        End Select

        'Display the custom MessageBox as a modal
        CustomMsgBox.ShowDialog()
    End Sub

    Public Overloads Shared Sub Show(ByVal Message As String, ByVal Title As String, ByVal Buttons As CustomMsgBoxButtons, ByVal Icon As CustomMsgBoxIcon)
        'The content of Message MUST be written in Rich Text Format
        '//www.oreilly.com/library/view/rtf-pocket-guide/9781449302047/ch01.html
        'When printing a string variable that is a path or otherwise contains any backward slashes they MUST be escaped with .Replace("\", "\\")
        CustomMsgBox.MessageRTF.Rtf = Message
        CustomMsgBox.TitleLabel.Text = Title

        Select Case Buttons
            Case CustomMsgBoxButtons.OK, CustomMsgBoxButtons.OKCancel
                '[OK] or [OK][Cancel] dialog
                CustomMsgBox.RightButton.DialogResult = DialogResult.OK
                CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
                'Hide [Yes] button and make [No] button the [OK] button
                CustomMsgBox.LeftButton.Visible = False
                CustomMsgBox.RightButton.Text = Localization.GetString("msgbox_btn_ok")
                CustomMsgBox.AcceptButton = CustomMsgBox.RightButton
                CustomMsgBox.CancelButton = CustomMsgBox.CancelLabel
            Case CustomMsgBoxButtons.YesNo, CustomMsgBoxButtons.YesNoCancel
                '[Yes][No] or [Yes][No][Cancel] dialog
                CustomMsgBox.LeftButton.DialogResult = DialogResult.Yes
                CustomMsgBox.RightButton.DialogResult = DialogResult.No
                CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
                'Show [Yes] button and make [OK] button the [No] button
                CustomMsgBox.LeftButton.Visible = True
                CustomMsgBox.RightButton.Text = Localization.GetString("msgbox_btn_no")
                CustomMsgBox.AcceptButton = CustomMsgBox.LeftButton
                CustomMsgBox.CancelButton = CustomMsgBox.CancelLabel
            Case Else
                Exit Select
        End Select

        'Set [Cancel] button as the default button
        CustomMsgBox.ActiveControl = CustomMsgBox.CancelLabel

        Select Case Icon
            Case CustomMsgBoxIcon.Hand, CustomMsgBoxIcon.Stop, CustomMsgBoxIcon.Error
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Error
            Case CustomMsgBoxIcon.Exclamation, CustomMsgBoxIcon.Warning
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Warning
            Case CustomMsgBoxIcon.Question
                CustomMsgBox.IconPictureBox.Image = My.Resources.CustomMsgBox_Question
            Case Else
                Exit Select
        End Select

        'Display the custom MessageBox as a modal
        CustomMsgBox.ShowDialog()
    End Sub

    Public Overloads Shared Sub Show(ByVal Message As String)
        'The content of Message MUST be written in Rich Text Format
        '//www.oreilly.com/library/view/rtf-pocket-guide/9781449302047/ch01.html
        'When printing a string variable that is a path or otherwise contains any backward slashes they MUST be escaped with .Replace("\", "\\")
        CustomMsgBox.MessageRTF.Rtf = Message
        CustomMsgBox.TitleLabel.Text = ""

        'Make it an [OK][Cancel] dialog
        CustomMsgBox.RightButton.DialogResult = DialogResult.OK
        CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
        'Hide [Yes] button and make [No] button the [OK] button
        CustomMsgBox.LeftButton.Visible = False
        CustomMsgBox.RightButton.Text = Localization.GetString("msgbox_btn_ok")
        CustomMsgBox.AcceptButton = CustomMsgBox.RightButton
        CustomMsgBox.CancelButton = CustomMsgBox.CancelLabel

        'Set [Cancel] button as the default button
        CustomMsgBox.ActiveControl = CustomMsgBox.CancelLabel

        'No MsgBox icon
        CustomMsgBox.IconPictureBox.Image = Nothing

        'Display the custom MessageBox as a modal
        CustomMsgBox.ShowDialog()
    End Sub
#End Region

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
End Class

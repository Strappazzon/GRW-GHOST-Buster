Public Class Banner
    Public Shared Sub Show(AlertType As Short, AlertDesc As String)
        'Alert/info banner
        If AlertType = 48 Then
            'Warning
            Form1.AlertIcon.Image = My.Resources.Banner_Alert_Icon
            If Form1.TitleLabel.Text <> "Logs" Then
                Form1.AlertDot.Visible = True
            End If
        ElseIf AlertType = 64 Then
            'Info
            Form1.AlertIcon.Image = My.Resources.Banner_Info_Icon
        End If

        'Banner message
        Form1.AlertDescriptionLabel.Text = AlertDesc
        'Move logo and Play button
        Form1.LogoBigPictureBox.Location = New Point(12, 115)
        Form1.PlayGameBtn.Location = New Point(12, 180)
        'Move checkboxes
        Form1.ConfirmExitChkBox.Location = New Point(14, 250)
        Form1.ConfirmStopBackupChkBox.Location = New Point(14, 275)
        Form1.DisplayNotificationChkBox.Location = New Point(14, 300)
        Form1.DisableCloudSyncChkBox.Location = New Point(14, 325)
        Form1.EnableCloudSyncChkBox.Location = New Point(14, 350)
        Form1.CheckUpdatesChkBox.Location = New Point(14, 375)
        Form1.RememberFormPositionChkBox.Location = New Point(14, 400)
        'Center alert icon and message
        Form1.AlertDescriptionLabel.Location = New Point(Form1.AlertContainer.Width / 2 - Form1.AlertDescriptionLabel.Width / 2, Form1.AlertContainer.Height / 2 - Form1.AlertDescriptionLabel.Height / 2)
        Form1.AlertIcon.Location = New Point(Form1.AlertContainer.Width / 2 - Form1.AlertDescriptionLabel.Width / 2 - 28, Form1.AlertContainer.Height / 2 - Form1.AlertIcon.Height / 2)
        Form1.AlertContainer.Visible = True
    End Sub
End Class

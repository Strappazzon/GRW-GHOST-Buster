Public Class Banner
    Inherits Panel

    Private Shared WithEvents AlertBanner As Panel = New Panel() With {
        .BackColor = Color.FromArgb(180, 60, 71, 84),
        .Font = New Font("Segoe UI", 9.0!, FontStyle.Regular, GraphicsUnit.Point, 0),
        .ForeColor = Color.White,
        .Location = New Point(0, 60),
        .Size = New Size(834, 38),
        .TabIndex = 1
    }
    Private Shared WithEvents BannerIcon As PictureBox = New PictureBox() With {
        .BackColor = Color.Transparent,
        .Size = New Size(24, 24),
        .SizeMode = PictureBoxSizeMode.AutoSize,
        .TabStop = False
    }
    Private Shared WithEvents BannerMessage As Label = New Label() With {
        .AutoSize = True,
        .BackColor = Color.Transparent,
        .Font = New Font("Segoe UI Semibold", 9.75!, FontStyle.Bold, GraphicsUnit.Point, 0),
        .TabStop = False
    }
    Private Shared WithEvents CloseBanner As PictureBox = New PictureBox() With {
        .BackColor = Color.Transparent,
        .Cursor = Cursors.Hand,
        .Image = My.Resources.Banner_Close_Icon,
        .Location = New Point(800, 8),
        .Size = New Size(24, 24),
        .SizeMode = PictureBoxSizeMode.AutoSize,
        .TabStop = False
    }

    Public Overloads Shared Sub Show(Icon As Integer, Message As String)
        If Icon = 48 Then
            'Warning
            BannerIcon.Image = My.Resources.Banner_Alert_Icon
            If Form1.TitleLabel.Text <> "Logs" Then
                Form1.AlertDot.Visible = True
            End If
        ElseIf Icon = 64 Then
            'Information
            BannerIcon.Image = My.Resources.Banner_Info_Icon
        End If

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
        'Create banner
        AlertBanner.Controls.Add(BannerIcon)
        AlertBanner.Controls.Add(BannerMessage)
        AlertBanner.Controls.Add(CloseBanner)
        'Banner message
        BannerMessage.Text = Message
        'Center alert icon and message
        BannerMessage.Location = New Point(AlertBanner.Width / 2 - BannerMessage.Width / 2, AlertBanner.Height / 2 - BannerMessage.Height / 2)
        BannerIcon.Location = New Point(AlertBanner.Width / 2 - BannerMessage.Width / 2 - 28, AlertBanner.Height / 2 - BannerIcon.Height / 2)
        'Display banner
        Form1.Controls.Add(AlertBanner)
    End Sub

    Public Shared Sub CloseBanner_Click(sender As Object, e As EventArgs) Handles CloseBanner.Click
        AlertBanner.Controls.Remove(BannerIcon)
        AlertBanner.Controls.Remove(BannerMessage)
        AlertBanner.Controls.Remove(CloseBanner)
        'Hide banner
        Form1.Controls.Remove(AlertBanner)
        'Move logo and Play button
        Form1.LogoBigPictureBox.Location = New Point(12, 85)
        Form1.PlayGameBtn.Location = New Point(12, 150)
        'Move checkboxes
        Form1.ConfirmExitChkBox.Location = New Point(14, 225)
        Form1.ConfirmStopBackupChkBox.Location = New Point(14, 250)
        Form1.DisplayNotificationChkBox.Location = New Point(14, 275)
        Form1.DisableCloudSyncChkBox.Location = New Point(14, 300)
        Form1.EnableCloudSyncChkBox.Location = New Point(14, 325)
        Form1.CheckUpdatesChkBox.Location = New Point(14, 350)
        Form1.RememberFormPositionChkBox.Location = New Point(14, 375)
    End Sub
End Class

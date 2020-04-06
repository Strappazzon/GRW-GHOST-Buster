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

Public Class Banner

#Region "Controls"
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
#End Region

    Public Overloads Shared Sub Show(Icon As Integer, Message As String)
        If Icon = 48 Then
            'Warning
            BannerIcon.Image = My.Resources.Banner_Alert_Icon
            If Form1.TitleLabel.Text <> Localization.GetString("ui_title_logs") Then
                Form1.AlertDot.Visible = True
            End If
        ElseIf Icon = 64 Then
            'Information
            BannerIcon.Image = My.Resources.Banner_Info_Icon
        End If

        'Move logo and Play button
        Form1.LogoBigPictureBox.Location = New Point(12, 115)
        Form1.PlayGameBtn.Location = New Point(12, 180)
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
        'Hide banner
        AlertBanner.Controls.Remove(BannerIcon)
        AlertBanner.Controls.Remove(BannerMessage)
        AlertBanner.Controls.Remove(CloseBanner)
        Form1.Controls.Remove(AlertBanner)
        'Move logo and Play button
        Form1.LogoBigPictureBox.Location = New Point(12, 85)
        Form1.PlayGameBtn.Location = New Point(12, 150)
    End Sub
End Class

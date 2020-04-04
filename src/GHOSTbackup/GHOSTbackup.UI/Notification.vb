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

Public Class Notification
    Private WithEvents CloseTimer As New Timer() With {.Interval = 5000}

    Public Overloads Shared Sub Show(ByVal Message As String)
        Notification.MessageLabel.Text = Message

        'Display the notification
        'Use My.Forms.Show because msbuild complains
        My.Forms.Notification.Show()
    End Sub

    Private Sub CloseTimer_Tick(sender As Object, e As EventArgs) Handles CloseTimer.Tick
        CloseTimer.Stop()
        Close()
    End Sub

    Private Sub Notification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set images
        Logo.Image = My.Resources.Notification_Image
        BackgroundImage = My.Resources.Notification_Form_BG

        'Position notification in the bottom right corner of the screen
        Location = New Point(My.Computer.Screen.Bounds.Width - Width, My.Computer.Screen.Bounds.Height - Height)

        'Close the notification automatically
        CloseTimer.Start()
    End Sub

    Private Sub Notification_Closing(sender As Object, e As EventArgs) Handles Me.Closing
        'Dispose images
        Logo.Image.Dispose()
        BackgroundImage.Dispose()
    End Sub
End Class

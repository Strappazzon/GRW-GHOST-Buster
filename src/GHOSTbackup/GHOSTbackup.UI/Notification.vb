Public Class Notification
    Private WithEvents CloseTimer As New Timer() With {.Interval = 5000}

    Public Overloads Shared Sub Show(ByVal Message As String)
        Notification.MessageLabel.Text = Message

        'Display the notification
        Notification.Show()
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

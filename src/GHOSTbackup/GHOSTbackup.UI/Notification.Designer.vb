<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Notification
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.MessageLabel = New System.Windows.Forms.Label()
        Me.Logo = New System.Windows.Forms.PictureBox()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.BackColor = System.Drawing.Color.Transparent
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.ForeColor = System.Drawing.Color.White
        Me.TitleLabel.Location = New System.Drawing.Point(85, 30)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(114, 21)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "GHOST Buster"
        '
        'MessageLabel
        '
        Me.MessageLabel.BackColor = System.Drawing.Color.Transparent
        Me.MessageLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MessageLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(187, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.MessageLabel.Location = New System.Drawing.Point(85, 55)
        Me.MessageLabel.Name = "MessageLabel"
        Me.MessageLabel.Size = New System.Drawing.Size(180, 35)
        Me.MessageLabel.TabIndex = 1
        Me.MessageLabel.Text = "Message"
        '
        'Logo
        '
        Me.Logo.BackColor = System.Drawing.Color.Transparent
        Me.Logo.Location = New System.Drawing.Point(12, 20)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(60, 70)
        Me.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Logo.TabIndex = 0
        Me.Logo.TabStop = False
        '
        'Notification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(300, 111)
        Me.Controls.Add(Me.MessageLabel)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.Logo)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Notification"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Notification"
        Me.TopMost = True
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Logo As PictureBox
    Friend WithEvents TitleLabel As Label
    Friend WithEvents MessageLabel As Label
End Class

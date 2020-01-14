<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomMsgBox
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomMsgBox))
        Me.rButton = New System.Windows.Forms.Button()
        Me.lButton = New System.Windows.Forms.Button()
        Me.titleLabel = New System.Windows.Forms.Label()
        Me.messageRTF = New System.Windows.Forms.RichTextBox()
        Me.cButton = New System.Windows.Forms.Button()
        Me.iconPictureBox = New System.Windows.Forms.PictureBox()
        CType(Me.iconPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rButton
        '
        Me.rButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.rButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.rButton.FlatAppearance.BorderSize = 0
        Me.rButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.rButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rButton.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rButton.ForeColor = System.Drawing.Color.White
        Me.rButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.rButton.Location = New System.Drawing.Point(541, 359)
        Me.rButton.Name = "rButton"
        Me.rButton.Size = New System.Drawing.Size(128, 30)
        Me.rButton.TabIndex = 0
        Me.rButton.Text = "No"
        Me.rButton.UseVisualStyleBackColor = False
        '
        'lButton
        '
        Me.lButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.lButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.lButton.FlatAppearance.BorderSize = 0
        Me.lButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.lButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lButton.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lButton.ForeColor = System.Drawing.Color.White
        Me.lButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lButton.Location = New System.Drawing.Point(407, 359)
        Me.lButton.Name = "lButton"
        Me.lButton.Size = New System.Drawing.Size(128, 30)
        Me.lButton.TabIndex = 1
        Me.lButton.Text = "Yes"
        Me.lButton.UseVisualStyleBackColor = False
        '
        'titleLabel
        '
        Me.titleLabel.AutoSize = True
        Me.titleLabel.BackColor = System.Drawing.Color.Transparent
        Me.titleLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.titleLabel.ForeColor = System.Drawing.Color.White
        Me.titleLabel.Location = New System.Drawing.Point(80, 50)
        Me.titleLabel.Name = "titleLabel"
        Me.titleLabel.Size = New System.Drawing.Size(111, 21)
        Me.titleLabel.TabIndex = 4
        Me.titleLabel.Text = "Message title"
        '
        'messageRTF
        '
        Me.messageRTF.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.messageRTF.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.messageRTF.Cursor = System.Windows.Forms.Cursors.Default
        Me.messageRTF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.messageRTF.ForeColor = System.Drawing.Color.White
        Me.messageRTF.Location = New System.Drawing.Point(84, 88)
        Me.messageRTF.Name = "messageRTF"
        Me.messageRTF.ReadOnly = True
        Me.messageRTF.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.messageRTF.ShortcutsEnabled = False
        Me.messageRTF.Size = New System.Drawing.Size(550, 200)
        Me.messageRTF.TabIndex = 3
        Me.messageRTF.TabStop = False
        Me.messageRTF.Text = "Message content"
        '
        'cButton
        '
        Me.cButton.BackColor = System.Drawing.Color.Transparent
        Me.cButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.cButton.FlatAppearance.BorderSize = 0
        Me.cButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.cButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.cButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cButton.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cButton.Location = New System.Drawing.Point(8, 363)
        Me.cButton.Name = "cButton"
        Me.cButton.Size = New System.Drawing.Size(55, 23)
        Me.cButton.TabIndex = 2
        Me.cButton.Text = "Cancel"
        Me.cButton.UseVisualStyleBackColor = False
        '
        'iconPictureBox
        '
        Me.iconPictureBox.BackColor = System.Drawing.Color.Transparent
        Me.iconPictureBox.Location = New System.Drawing.Point(18, 54)
        Me.iconPictureBox.Name = "iconPictureBox"
        Me.iconPictureBox.Size = New System.Drawing.Size(32, 32)
        Me.iconPictureBox.TabIndex = 4
        Me.iconPictureBox.TabStop = False
        '
        'CustomMsgBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(684, 401)
        Me.Controls.Add(Me.cButton)
        Me.Controls.Add(Me.messageRTF)
        Me.Controls.Add(Me.iconPictureBox)
        Me.Controls.Add(Me.titleLabel)
        Me.Controls.Add(Me.lButton)
        Me.Controls.Add(Me.rButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "CustomMsgBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GHOST Buster"
        CType(Me.iconPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rButton As Button
    Friend WithEvents lButton As Button
    Friend WithEvents titleLabel As Label
    Friend WithEvents iconPictureBox As PictureBox
    Friend WithEvents messageRTF As RichTextBox
    Friend WithEvents cButton As Button
End Class

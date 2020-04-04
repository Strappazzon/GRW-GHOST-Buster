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
        Me.RightButton = New System.Windows.Forms.Button()
        Me.LeftButton = New System.Windows.Forms.Button()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.MessageRTF = New System.Windows.Forms.RichTextBox()
        Me.CancelLabel = New System.Windows.Forms.Button()
        Me.IconPictureBox = New System.Windows.Forms.PictureBox()
        Me.BackupDirsDropdownCombo = New System.Windows.Forms.ComboBox()
        CType(Me.IconPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RightButton
        '
        Me.RightButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.RightButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.RightButton.FlatAppearance.BorderSize = 0
        Me.RightButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.RightButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RightButton.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RightButton.ForeColor = System.Drawing.Color.White
        Me.RightButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RightButton.Location = New System.Drawing.Point(541, 359)
        Me.RightButton.Name = "RightButton"
        Me.RightButton.Size = New System.Drawing.Size(128, 30)
        Me.RightButton.TabIndex = 0
        Me.RightButton.Text = "No"
        Me.RightButton.UseVisualStyleBackColor = False
        '
        'LeftButton
        '
        Me.LeftButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.LeftButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.LeftButton.FlatAppearance.BorderSize = 0
        Me.LeftButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.LeftButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LeftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LeftButton.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LeftButton.ForeColor = System.Drawing.Color.White
        Me.LeftButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LeftButton.Location = New System.Drawing.Point(407, 359)
        Me.LeftButton.Name = "LeftButton"
        Me.LeftButton.Size = New System.Drawing.Size(128, 30)
        Me.LeftButton.TabIndex = 1
        Me.LeftButton.Text = "Yes"
        Me.LeftButton.UseVisualStyleBackColor = False
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.BackColor = System.Drawing.Color.Transparent
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.ForeColor = System.Drawing.Color.White
        Me.TitleLabel.Location = New System.Drawing.Point(80, 50)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(111, 21)
        Me.TitleLabel.TabIndex = 5
        Me.TitleLabel.Text = "Message title"
        '
        'MessageRTF
        '
        Me.MessageRTF.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.MessageRTF.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MessageRTF.Cursor = System.Windows.Forms.Cursors.Default
        Me.MessageRTF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MessageRTF.ForeColor = System.Drawing.Color.White
        Me.MessageRTF.Location = New System.Drawing.Point(84, 88)
        Me.MessageRTF.Name = "MessageRTF"
        Me.MessageRTF.ReadOnly = True
        Me.MessageRTF.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.MessageRTF.ShortcutsEnabled = False
        Me.MessageRTF.Size = New System.Drawing.Size(550, 200)
        Me.MessageRTF.TabIndex = 4
        Me.MessageRTF.TabStop = False
        Me.MessageRTF.Text = "Message content"
        '
        'CancelLabel
        '
        Me.CancelLabel.AutoSize = True
        Me.CancelLabel.BackColor = System.Drawing.Color.Transparent
        Me.CancelLabel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.CancelLabel.FlatAppearance.BorderSize = 0
        Me.CancelLabel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.CancelLabel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.CancelLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CancelLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CancelLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CancelLabel.Location = New System.Drawing.Point(8, 362)
        Me.CancelLabel.Name = "CancelLabel"
        Me.CancelLabel.Size = New System.Drawing.Size(56, 27)
        Me.CancelLabel.TabIndex = 2
        Me.CancelLabel.Text = "Cancel"
        Me.CancelLabel.UseVisualStyleBackColor = False
        '
        'IconPictureBox
        '
        Me.IconPictureBox.BackColor = System.Drawing.Color.Transparent
        Me.IconPictureBox.Location = New System.Drawing.Point(18, 54)
        Me.IconPictureBox.Name = "IconPictureBox"
        Me.IconPictureBox.Size = New System.Drawing.Size(32, 32)
        Me.IconPictureBox.TabIndex = 4
        Me.IconPictureBox.TabStop = False
        '
        'BackupDirsDropdownCombo
        '
        Me.BackupDirsDropdownCombo.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.BackupDirsDropdownCombo.DropDownHeight = 152
        Me.BackupDirsDropdownCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.BackupDirsDropdownCombo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BackupDirsDropdownCombo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupDirsDropdownCombo.ForeColor = System.Drawing.Color.White
        Me.BackupDirsDropdownCombo.FormattingEnabled = True
        Me.BackupDirsDropdownCombo.IntegralHeight = False
        Me.BackupDirsDropdownCombo.Location = New System.Drawing.Point(84, 300)
        Me.BackupDirsDropdownCombo.Name = "BackupDirsDropdownCombo"
        Me.BackupDirsDropdownCombo.Size = New System.Drawing.Size(550, 23)
        Me.BackupDirsDropdownCombo.TabIndex = 3
        Me.BackupDirsDropdownCombo.Visible = False
        '
        'CustomMsgBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(684, 401)
        Me.Controls.Add(Me.BackupDirsDropdownCombo)
        Me.Controls.Add(Me.CancelLabel)
        Me.Controls.Add(Me.MessageRTF)
        Me.Controls.Add(Me.IconPictureBox)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.LeftButton)
        Me.Controls.Add(Me.RightButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "CustomMsgBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GHOST Buster"
        CType(Me.IconPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RightButton As Button
    Friend WithEvents LeftButton As Button
    Friend WithEvents TitleLabel As Label
    Friend WithEvents IconPictureBox As PictureBox
    Friend WithEvents MessageRTF As RichTextBox
    Friend WithEvents CancelLabel As Button
    Friend WithEvents BackupDirsDropdownCombo As ComboBox
End Class

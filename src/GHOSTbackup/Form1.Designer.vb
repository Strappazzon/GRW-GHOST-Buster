<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.saveLocTextBox = New System.Windows.Forms.TextBox()
        Me.browseSaveLocBtn = New System.Windows.Forms.Button()
        Me.browseDestLocBtn = New System.Windows.Forms.Button()
        Me.destLocTextBox = New System.Windows.Forms.TextBox()
        Me.backupTimer = New System.Windows.Forms.Timer(Me.components)
        Me.pathsGroupBox = New System.Windows.Forms.GroupBox()
        Me.destLocHelpLabel = New System.Windows.Forms.Label()
        Me.saveLocHelpLabel = New System.Windows.Forms.Label()
        Me.backupGroupBox = New System.Windows.Forms.GroupBox()
        Me.freqSelectTimeUpDown = New System.Windows.Forms.NumericUpDown()
        Me.stopBtn = New System.Windows.Forms.Button()
        Me.restoreBtn = New System.Windows.Forms.Button()
        Me.backupHelpLabel = New System.Windows.Forms.Label()
        Me.backupBtn = New System.Windows.Forms.Button()
        Me.logoBigPictureBox = New System.Windows.Forms.PictureBox()
        Me.playGameBtn = New System.Windows.Forms.Button()
        Me.logTxtBox = New System.Windows.Forms.TextBox()
        Me.logTxtBoxContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.WriteLogToFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.confirmExitChkBox = New System.Windows.Forms.CheckBox()
        Me.processCheckTimer = New System.Windows.Forms.Timer(Me.components)
        Me.confirmStopBackupChkBox = New System.Windows.Forms.CheckBox()
        Me.taskbarProgressTimer = New System.Windows.Forms.Timer(Me.components)
        Me.updateCheckerChkBox = New System.Windows.Forms.CheckBox()
        Me.pathsGroupBox.SuspendLayout()
        Me.backupGroupBox.SuspendLayout()
        CType(Me.freqSelectTimeUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.logoBigPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.logTxtBoxContext.SuspendLayout()
        Me.SuspendLayout()
        '
        'saveLocTextBox
        '
        Me.saveLocTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.saveLocTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveLocTextBox.ForeColor = System.Drawing.Color.Black
        Me.saveLocTextBox.Location = New System.Drawing.Point(9, 42)
        Me.saveLocTextBox.MaxLength = 256
        Me.saveLocTextBox.Name = "saveLocTextBox"
        Me.saveLocTextBox.ReadOnly = True
        Me.saveLocTextBox.Size = New System.Drawing.Size(247, 23)
        Me.saveLocTextBox.TabIndex = 12
        '
        'browseSaveLocBtn
        '
        Me.browseSaveLocBtn.Cursor = System.Windows.Forms.Cursors.Default
        Me.browseSaveLocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.browseSaveLocBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.browseSaveLocBtn.ForeColor = System.Drawing.Color.White
        Me.browseSaveLocBtn.Image = CType(resources.GetObject("browseSaveLocBtn.Image"), System.Drawing.Image)
        Me.browseSaveLocBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.browseSaveLocBtn.Location = New System.Drawing.Point(262, 40)
        Me.browseSaveLocBtn.Name = "browseSaveLocBtn"
        Me.browseSaveLocBtn.Size = New System.Drawing.Size(92, 27)
        Me.browseSaveLocBtn.TabIndex = 13
        Me.browseSaveLocBtn.Text = "Browse..."
        Me.browseSaveLocBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.browseSaveLocBtn.UseVisualStyleBackColor = True
        '
        'browseDestLocBtn
        '
        Me.browseDestLocBtn.Cursor = System.Windows.Forms.Cursors.Default
        Me.browseDestLocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.browseDestLocBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.browseDestLocBtn.ForeColor = System.Drawing.Color.White
        Me.browseDestLocBtn.Image = CType(resources.GetObject("browseDestLocBtn.Image"), System.Drawing.Image)
        Me.browseDestLocBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.browseDestLocBtn.Location = New System.Drawing.Point(262, 100)
        Me.browseDestLocBtn.Name = "browseDestLocBtn"
        Me.browseDestLocBtn.Size = New System.Drawing.Size(92, 27)
        Me.browseDestLocBtn.TabIndex = 16
        Me.browseDestLocBtn.Text = "Browse..."
        Me.browseDestLocBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.browseDestLocBtn.UseVisualStyleBackColor = True
        '
        'destLocTextBox
        '
        Me.destLocTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.destLocTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.destLocTextBox.ForeColor = System.Drawing.Color.Black
        Me.destLocTextBox.Location = New System.Drawing.Point(9, 102)
        Me.destLocTextBox.MaxLength = 256
        Me.destLocTextBox.Name = "destLocTextBox"
        Me.destLocTextBox.ReadOnly = True
        Me.destLocTextBox.Size = New System.Drawing.Size(247, 23)
        Me.destLocTextBox.TabIndex = 15
        '
        'backupTimer
        '
        '
        'pathsGroupBox
        '
        Me.pathsGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.pathsGroupBox.Controls.Add(Me.destLocHelpLabel)
        Me.pathsGroupBox.Controls.Add(Me.saveLocHelpLabel)
        Me.pathsGroupBox.Controls.Add(Me.destLocTextBox)
        Me.pathsGroupBox.Controls.Add(Me.saveLocTextBox)
        Me.pathsGroupBox.Controls.Add(Me.browseDestLocBtn)
        Me.pathsGroupBox.Controls.Add(Me.browseSaveLocBtn)
        Me.pathsGroupBox.ForeColor = System.Drawing.Color.White
        Me.pathsGroupBox.Location = New System.Drawing.Point(325, 240)
        Me.pathsGroupBox.Name = "pathsGroupBox"
        Me.pathsGroupBox.Size = New System.Drawing.Size(360, 135)
        Me.pathsGroupBox.TabIndex = 10
        Me.pathsGroupBox.TabStop = False
        Me.pathsGroupBox.Text = "Working directories"
        '
        'destLocHelpLabel
        '
        Me.destLocHelpLabel.AutoSize = True
        Me.destLocHelpLabel.Location = New System.Drawing.Point(6, 80)
        Me.destLocHelpLabel.Name = "destLocHelpLabel"
        Me.destLocHelpLabel.Size = New System.Drawing.Size(270, 13)
        Me.destLocHelpLabel.TabIndex = 14
        Me.destLocHelpLabel.Text = "Click the ""Browse..."" button to select the backup folder."
        '
        'saveLocHelpLabel
        '
        Me.saveLocHelpLabel.AutoSize = True
        Me.saveLocHelpLabel.Location = New System.Drawing.Point(6, 20)
        Me.saveLocHelpLabel.Name = "saveLocHelpLabel"
        Me.saveLocHelpLabel.Size = New System.Drawing.Size(287, 13)
        Me.saveLocHelpLabel.TabIndex = 11
        Me.saveLocHelpLabel.Text = "Click the ""Browse..."" button to open the save games folder."
        '
        'backupGroupBox
        '
        Me.backupGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.backupGroupBox.Controls.Add(Me.freqSelectTimeUpDown)
        Me.backupGroupBox.Controls.Add(Me.stopBtn)
        Me.backupGroupBox.Controls.Add(Me.restoreBtn)
        Me.backupGroupBox.Controls.Add(Me.backupHelpLabel)
        Me.backupGroupBox.Controls.Add(Me.backupBtn)
        Me.backupGroupBox.ForeColor = System.Drawing.Color.White
        Me.backupGroupBox.Location = New System.Drawing.Point(325, 130)
        Me.backupGroupBox.Name = "backupGroupBox"
        Me.backupGroupBox.Size = New System.Drawing.Size(360, 100)
        Me.backupGroupBox.TabIndex = 4
        Me.backupGroupBox.TabStop = False
        Me.backupGroupBox.Text = "Tasks"
        '
        'freqSelectTimeUpDown
        '
        Me.freqSelectTimeUpDown.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.freqSelectTimeUpDown.Location = New System.Drawing.Point(9, 60)
        Me.freqSelectTimeUpDown.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.freqSelectTimeUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.freqSelectTimeUpDown.Name = "freqSelectTimeUpDown"
        Me.freqSelectTimeUpDown.ReadOnly = True
        Me.freqSelectTimeUpDown.Size = New System.Drawing.Size(150, 25)
        Me.freqSelectTimeUpDown.TabIndex = 6
        Me.freqSelectTimeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.freqSelectTimeUpDown.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'stopBtn
        '
        Me.stopBtn.BackColor = System.Drawing.Color.Transparent
        Me.stopBtn.Cursor = System.Windows.Forms.Cursors.Default
        Me.stopBtn.Enabled = False
        Me.stopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.stopBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.stopBtn.ForeColor = System.Drawing.Color.White
        Me.stopBtn.Image = CType(resources.GetObject("stopBtn.Image"), System.Drawing.Image)
        Me.stopBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.stopBtn.Location = New System.Drawing.Point(263, 54)
        Me.stopBtn.Name = "stopBtn"
        Me.stopBtn.Size = New System.Drawing.Size(92, 35)
        Me.stopBtn.TabIndex = 9
        Me.stopBtn.Text = "Stop the Backup"
        Me.stopBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.stopBtn.UseVisualStyleBackColor = False
        '
        'restoreBtn
        '
        Me.restoreBtn.BackColor = System.Drawing.Color.Transparent
        Me.restoreBtn.Cursor = System.Windows.Forms.Cursors.Default
        Me.restoreBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.restoreBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.restoreBtn.ForeColor = System.Drawing.Color.White
        Me.restoreBtn.Image = CType(resources.GetObject("restoreBtn.Image"), System.Drawing.Image)
        Me.restoreBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.restoreBtn.Location = New System.Drawing.Point(263, 13)
        Me.restoreBtn.Name = "restoreBtn"
        Me.restoreBtn.Size = New System.Drawing.Size(92, 35)
        Me.restoreBtn.TabIndex = 8
        Me.restoreBtn.Text = "Restore" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "save games"
        Me.restoreBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.restoreBtn.UseVisualStyleBackColor = False
        '
        'backupHelpLabel
        '
        Me.backupHelpLabel.AutoSize = True
        Me.backupHelpLabel.Location = New System.Drawing.Point(6, 16)
        Me.backupHelpLabel.Name = "backupHelpLabel"
        Me.backupHelpLabel.Size = New System.Drawing.Size(133, 26)
        Me.backupHelpLabel.TabIndex = 5
        Me.backupHelpLabel.Text = "Backup save games every" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "specified amount of time."
        '
        'backupBtn
        '
        Me.backupBtn.BackColor = System.Drawing.Color.Transparent
        Me.backupBtn.Cursor = System.Windows.Forms.Cursors.Default
        Me.backupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.backupBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.backupBtn.ForeColor = System.Drawing.Color.White
        Me.backupBtn.Image = CType(resources.GetObject("backupBtn.Image"), System.Drawing.Image)
        Me.backupBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.backupBtn.Location = New System.Drawing.Point(165, 54)
        Me.backupBtn.Name = "backupBtn"
        Me.backupBtn.Size = New System.Drawing.Size(92, 35)
        Me.backupBtn.TabIndex = 7
        Me.backupBtn.Text = "Start the Backup"
        Me.backupBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.backupBtn.UseVisualStyleBackColor = False
        '
        'logoBigPictureBox
        '
        Me.logoBigPictureBox.BackColor = System.Drawing.Color.Transparent
        Me.logoBigPictureBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.logoBigPictureBox.Image = CType(resources.GetObject("logoBigPictureBox.Image"), System.Drawing.Image)
        Me.logoBigPictureBox.Location = New System.Drawing.Point(12, 12)
        Me.logoBigPictureBox.Name = "logoBigPictureBox"
        Me.logoBigPictureBox.Size = New System.Drawing.Size(293, 50)
        Me.logoBigPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.logoBigPictureBox.TabIndex = 10
        Me.logoBigPictureBox.TabStop = False
        '
        'playGameBtn
        '
        Me.playGameBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.playGameBtn.Cursor = System.Windows.Forms.Cursors.Default
        Me.playGameBtn.Enabled = False
        Me.playGameBtn.FlatAppearance.BorderSize = 0
        Me.playGameBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.playGameBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.playGameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.playGameBtn.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.playGameBtn.ForeColor = System.Drawing.Color.White
        Me.playGameBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.playGameBtn.Location = New System.Drawing.Point(12, 75)
        Me.playGameBtn.Name = "playGameBtn"
        Me.playGameBtn.Size = New System.Drawing.Size(293, 32)
        Me.playGameBtn.TabIndex = 0
        Me.playGameBtn.Text = "gamePath"
        Me.playGameBtn.UseVisualStyleBackColor = False
        '
        'logTxtBox
        '
        Me.logTxtBox.ContextMenuStrip = Me.logTxtBoxContext
        Me.logTxtBox.Location = New System.Drawing.Point(12, 215)
        Me.logTxtBox.Multiline = True
        Me.logTxtBox.Name = "logTxtBox"
        Me.logTxtBox.ReadOnly = True
        Me.logTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.logTxtBox.Size = New System.Drawing.Size(293, 160)
        Me.logTxtBox.TabIndex = 3
        '
        'logTxtBoxContext
        '
        Me.logTxtBoxContext.BackColor = System.Drawing.SystemColors.Control
        Me.logTxtBoxContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyToolStripMenuItem, Me.SelectAllToolStripMenuItem, Me.ToolStripSeparator1, Me.WriteLogToFileToolStripMenuItem, Me.ExportLogToolStripMenuItem})
        Me.logTxtBoxContext.Name = "logTxtBoxContext"
        Me.logTxtBoxContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.logTxtBoxContext.Size = New System.Drawing.Size(168, 98)
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        Me.CopyToolStripMenuItem.ToolTipText = "Copy the selected text to clipboard."
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select All"
        Me.SelectAllToolStripMenuItem.ToolTipText = "Select all the text."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(164, 6)
        '
        'WriteLogToFileToolStripMenuItem
        '
        Me.WriteLogToFileToolStripMenuItem.CheckOnClick = True
        Me.WriteLogToFileToolStripMenuItem.Name = "WriteLogToFileToolStripMenuItem"
        Me.WriteLogToFileToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.WriteLogToFileToolStripMenuItem.Text = "Write log to file"
        Me.WriteLogToFileToolStripMenuItem.ToolTipText = "Write all events to a text file."
        Me.WriteLogToFileToolStripMenuItem.Visible = False
        '
        'ExportLogToolStripMenuItem
        '
        Me.ExportLogToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ExportLogToolStripMenuItem.Image = CType(resources.GetObject("ExportLogToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ExportLogToolStripMenuItem.Name = "ExportLogToolStripMenuItem"
        Me.ExportLogToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.ExportLogToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.ExportLogToolStripMenuItem.Text = "Export log"
        Me.ExportLogToolStripMenuItem.ToolTipText = "Export all events to a log file now."
        '
        'confirmExitChkBox
        '
        Me.confirmExitChkBox.AutoSize = True
        Me.confirmExitChkBox.BackColor = System.Drawing.Color.Transparent
        Me.confirmExitChkBox.Checked = True
        Me.confirmExitChkBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.confirmExitChkBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.confirmExitChkBox.ForeColor = System.Drawing.Color.White
        Me.confirmExitChkBox.Location = New System.Drawing.Point(12, 130)
        Me.confirmExitChkBox.Name = "confirmExitChkBox"
        Me.confirmExitChkBox.Size = New System.Drawing.Size(196, 19)
        Me.confirmExitChkBox.TabIndex = 1
        Me.confirmExitChkBox.Text = "Confirm exit (if backup is active)"
        Me.confirmExitChkBox.UseVisualStyleBackColor = False
        '
        'processCheckTimer
        '
        '
        'confirmStopBackupChkBox
        '
        Me.confirmStopBackupChkBox.AutoSize = True
        Me.confirmStopBackupChkBox.BackColor = System.Drawing.Color.Transparent
        Me.confirmStopBackupChkBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.confirmStopBackupChkBox.ForeColor = System.Drawing.Color.White
        Me.confirmStopBackupChkBox.Location = New System.Drawing.Point(12, 155)
        Me.confirmStopBackupChkBox.Name = "confirmStopBackupChkBox"
        Me.confirmStopBackupChkBox.Size = New System.Drawing.Size(178, 19)
        Me.confirmStopBackupChkBox.TabIndex = 2
        Me.confirmStopBackupChkBox.Text = "Confirm backup interruption"
        Me.confirmStopBackupChkBox.UseVisualStyleBackColor = False
        '
        'taskbarProgressTimer
        '
        Me.taskbarProgressTimer.Interval = 1000
        '
        'updateCheckerChkBox
        '
        Me.updateCheckerChkBox.AutoSize = True
        Me.updateCheckerChkBox.BackColor = System.Drawing.Color.Transparent
        Me.updateCheckerChkBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.updateCheckerChkBox.ForeColor = System.Drawing.Color.White
        Me.updateCheckerChkBox.Location = New System.Drawing.Point(12, 180)
        Me.updateCheckerChkBox.Name = "updateCheckerChkBox"
        Me.updateCheckerChkBox.Size = New System.Drawing.Size(122, 19)
        Me.updateCheckerChkBox.TabIndex = 11
        Me.updateCheckerChkBox.Text = "Check for updates"
        Me.updateCheckerChkBox.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(698, 386)
        Me.Controls.Add(Me.updateCheckerChkBox)
        Me.Controls.Add(Me.confirmStopBackupChkBox)
        Me.Controls.Add(Me.confirmExitChkBox)
        Me.Controls.Add(Me.logTxtBox)
        Me.Controls.Add(Me.playGameBtn)
        Me.Controls.Add(Me.logoBigPictureBox)
        Me.Controls.Add(Me.backupGroupBox)
        Me.Controls.Add(Me.pathsGroupBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GHOST Buster"
        Me.pathsGroupBox.ResumeLayout(False)
        Me.pathsGroupBox.PerformLayout()
        Me.backupGroupBox.ResumeLayout(False)
        Me.backupGroupBox.PerformLayout()
        CType(Me.freqSelectTimeUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.logoBigPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.logTxtBoxContext.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents saveLocTextBox As TextBox
    Friend WithEvents browseSaveLocBtn As Button
    Friend WithEvents browseDestLocBtn As Button
    Friend WithEvents destLocTextBox As TextBox
    Friend WithEvents backupTimer As Timer
    Friend WithEvents pathsGroupBox As GroupBox
    Friend WithEvents saveLocHelpLabel As Label
    Friend WithEvents destLocHelpLabel As Label
    Friend WithEvents backupGroupBox As GroupBox
    Friend WithEvents backupBtn As Button
    Friend WithEvents freqSelectTimeUpDown As NumericUpDown
    Friend WithEvents stopBtn As Button
    Friend WithEvents restoreBtn As Button
    Friend WithEvents logoBigPictureBox As PictureBox
    Friend WithEvents playGameBtn As Button
    Friend WithEvents logTxtBox As TextBox
    Friend WithEvents confirmExitChkBox As CheckBox
    Friend WithEvents processCheckTimer As Timer
    Friend WithEvents confirmStopBackupChkBox As CheckBox
    Friend WithEvents backupHelpLabel As Label
    Friend WithEvents taskbarProgressTimer As Timer
    Friend WithEvents updateCheckerChkBox As CheckBox
    Friend WithEvents logTxtBoxContext As ContextMenuStrip
    Friend WithEvents CopyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents WriteLogToFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportLogToolStripMenuItem As ToolStripMenuItem
End Class

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
        Me.exploreSaveLocBtn = New System.Windows.Forms.Button()
        Me.exploreDestLocBtn = New System.Windows.Forms.Button()
        Me.destLocHelpLabel = New System.Windows.Forms.Label()
        Me.saveLocHelpLabel = New System.Windows.Forms.Label()
        Me.backupGroupBox = New System.Windows.Forms.GroupBox()
        Me.latestBackupHelpLabel = New System.Windows.Forms.Label()
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
        Me.ExportLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.confirmExitChkBox = New System.Windows.Forms.CheckBox()
        Me.processCheckTimer = New System.Windows.Forms.Timer(Me.components)
        Me.confirmStopBackupChkBox = New System.Windows.Forms.CheckBox()
        Me.updateCheckerChkBox = New System.Windows.Forms.CheckBox()
        Me.topMenuContainer = New System.Windows.Forms.Panel()
        Me.uplayPictureBtn = New System.Windows.Forms.PictureBox()
        Me.alertDot = New System.Windows.Forms.PictureBox()
        Me.aboutLabel = New System.Windows.Forms.Label()
        Me.homePictureBtn = New System.Windows.Forms.PictureBox()
        Me.settingsLabel = New System.Windows.Forms.Label()
        Me.logLabel = New System.Windows.Forms.Label()
        Me.aboutContainer = New System.Windows.Forms.Panel()
        Me.licenseLabel = New System.Windows.Forms.Label()
        Me.changelogLabel = New System.Windows.Forms.Label()
        Me.supportLabel = New System.Windows.Forms.Label()
        Me.websiteLabel = New System.Windows.Forms.Label()
        Me.appInfoLabel = New System.Windows.Forms.Label()
        Me.logsContainer = New System.Windows.Forms.Panel()
        Me.alertContainer = New System.Windows.Forms.Panel()
        Me.alertDescriptionLabel = New System.Windows.Forms.Label()
        Me.alertIcon = New System.Windows.Forms.PictureBox()
        Me.closeAlertContainerIcon = New System.Windows.Forms.PictureBox()
        Me.formPositionChkBox = New System.Windows.Forms.CheckBox()
        Me.titleLabel = New System.Windows.Forms.Label()
        Me.settingsContainer = New System.Windows.Forms.Panel()
        Me.settingsNonUplayVersionRestartLabel = New System.Windows.Forms.Label()
        Me.settingsOpenCustomExeFolderBtn = New System.Windows.Forms.Button()
        Me.settingsBrowseCustomExeBtn = New System.Windows.Forms.Button()
        Me.settingsCustomExeTextBox = New System.Windows.Forms.TextBox()
        Me.settingsNonUplayVersionChkBox = New System.Windows.Forms.CheckBox()
        Me.settingsWhichBackupLabel = New System.Windows.Forms.Label()
        Me.settingsWhichBackupDropdownCombo = New System.Windows.Forms.ComboBox()
        Me.settingsBrowseLogFolderBtn = New System.Windows.Forms.Button()
        Me.settingsBrowseLogFileBtn = New System.Windows.Forms.Button()
        Me.settingsLogFilePathTextBox = New System.Windows.Forms.TextBox()
        Me.settingsWriteLogToFileChkBox = New System.Windows.Forms.CheckBox()
        Me.disableCloudSyncChkBox = New System.Windows.Forms.CheckBox()
        Me.pathsGroupBox.SuspendLayout()
        Me.backupGroupBox.SuspendLayout()
        CType(Me.freqSelectTimeUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.logoBigPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.logTxtBoxContext.SuspendLayout()
        Me.topMenuContainer.SuspendLayout()
        CType(Me.uplayPictureBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.alertDot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.homePictureBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.aboutContainer.SuspendLayout()
        Me.logsContainer.SuspendLayout()
        Me.alertContainer.SuspendLayout()
        CType(Me.alertIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.closeAlertContainerIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.settingsContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'saveLocTextBox
        '
        Me.saveLocTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.saveLocTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveLocTextBox.ForeColor = System.Drawing.Color.Black
        Me.saveLocTextBox.Location = New System.Drawing.Point(12, 45)
        Me.saveLocTextBox.MaxLength = 256
        Me.saveLocTextBox.Name = "saveLocTextBox"
        Me.saveLocTextBox.ReadOnly = True
        Me.saveLocTextBox.Size = New System.Drawing.Size(316, 23)
        Me.saveLocTextBox.TabIndex = 12
        '
        'browseSaveLocBtn
        '
        Me.browseSaveLocBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.browseSaveLocBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.browseSaveLocBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.browseSaveLocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.browseSaveLocBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.browseSaveLocBtn.ForeColor = System.Drawing.Color.White
        Me.browseSaveLocBtn.Image = CType(resources.GetObject("browseSaveLocBtn.Image"), System.Drawing.Image)
        Me.browseSaveLocBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.browseSaveLocBtn.Location = New System.Drawing.Point(334, 41)
        Me.browseSaveLocBtn.Name = "browseSaveLocBtn"
        Me.browseSaveLocBtn.Size = New System.Drawing.Size(95, 30)
        Me.browseSaveLocBtn.TabIndex = 13
        Me.browseSaveLocBtn.Text = "Browse..."
        Me.browseSaveLocBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.browseSaveLocBtn.UseVisualStyleBackColor = True
        '
        'browseDestLocBtn
        '
        Me.browseDestLocBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.browseDestLocBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.browseDestLocBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.browseDestLocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.browseDestLocBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.browseDestLocBtn.ForeColor = System.Drawing.Color.White
        Me.browseDestLocBtn.Image = CType(resources.GetObject("browseDestLocBtn.Image"), System.Drawing.Image)
        Me.browseDestLocBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.browseDestLocBtn.Location = New System.Drawing.Point(334, 93)
        Me.browseDestLocBtn.Name = "browseDestLocBtn"
        Me.browseDestLocBtn.Size = New System.Drawing.Size(95, 30)
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
        Me.destLocTextBox.Location = New System.Drawing.Point(12, 97)
        Me.destLocTextBox.MaxLength = 256
        Me.destLocTextBox.Name = "destLocTextBox"
        Me.destLocTextBox.ReadOnly = True
        Me.destLocTextBox.Size = New System.Drawing.Size(316, 23)
        Me.destLocTextBox.TabIndex = 15
        '
        'backupTimer
        '
        '
        'pathsGroupBox
        '
        Me.pathsGroupBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.pathsGroupBox.Controls.Add(Me.exploreSaveLocBtn)
        Me.pathsGroupBox.Controls.Add(Me.exploreDestLocBtn)
        Me.pathsGroupBox.Controls.Add(Me.destLocHelpLabel)
        Me.pathsGroupBox.Controls.Add(Me.saveLocHelpLabel)
        Me.pathsGroupBox.Controls.Add(Me.destLocTextBox)
        Me.pathsGroupBox.Controls.Add(Me.saveLocTextBox)
        Me.pathsGroupBox.Controls.Add(Me.browseDestLocBtn)
        Me.pathsGroupBox.Controls.Add(Me.browseSaveLocBtn)
        Me.pathsGroupBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pathsGroupBox.ForeColor = System.Drawing.Color.White
        Me.pathsGroupBox.Location = New System.Drawing.Point(325, 314)
        Me.pathsGroupBox.Name = "pathsGroupBox"
        Me.pathsGroupBox.Size = New System.Drawing.Size(497, 135)
        Me.pathsGroupBox.TabIndex = 10
        Me.pathsGroupBox.TabStop = False
        Me.pathsGroupBox.Text = "Working directories"
        '
        'exploreSaveLocBtn
        '
        Me.exploreSaveLocBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.exploreSaveLocBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.exploreSaveLocBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.exploreSaveLocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.exploreSaveLocBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.exploreSaveLocBtn.ForeColor = System.Drawing.Color.White
        Me.exploreSaveLocBtn.Location = New System.Drawing.Point(435, 41)
        Me.exploreSaveLocBtn.Name = "exploreSaveLocBtn"
        Me.exploreSaveLocBtn.Size = New System.Drawing.Size(50, 30)
        Me.exploreSaveLocBtn.TabIndex = 18
        Me.exploreSaveLocBtn.Text = "Open"
        Me.exploreSaveLocBtn.UseVisualStyleBackColor = True
        '
        'exploreDestLocBtn
        '
        Me.exploreDestLocBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.exploreDestLocBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.exploreDestLocBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.exploreDestLocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.exploreDestLocBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.exploreDestLocBtn.ForeColor = System.Drawing.Color.White
        Me.exploreDestLocBtn.Location = New System.Drawing.Point(435, 93)
        Me.exploreDestLocBtn.Name = "exploreDestLocBtn"
        Me.exploreDestLocBtn.Size = New System.Drawing.Size(50, 30)
        Me.exploreDestLocBtn.TabIndex = 17
        Me.exploreDestLocBtn.Text = "Open"
        Me.exploreDestLocBtn.UseVisualStyleBackColor = True
        '
        'destLocHelpLabel
        '
        Me.destLocHelpLabel.AutoSize = True
        Me.destLocHelpLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.destLocHelpLabel.Location = New System.Drawing.Point(10, 75)
        Me.destLocHelpLabel.Name = "destLocHelpLabel"
        Me.destLocHelpLabel.Size = New System.Drawing.Size(298, 15)
        Me.destLocHelpLabel.TabIndex = 14
        Me.destLocHelpLabel.Text = "Click the ""Browse..."" button to select the backup folder."
        '
        'saveLocHelpLabel
        '
        Me.saveLocHelpLabel.AutoSize = True
        Me.saveLocHelpLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveLocHelpLabel.Location = New System.Drawing.Point(10, 23)
        Me.saveLocHelpLabel.Name = "saveLocHelpLabel"
        Me.saveLocHelpLabel.Size = New System.Drawing.Size(352, 15)
        Me.saveLocHelpLabel.TabIndex = 11
        Me.saveLocHelpLabel.Text = "Click the ""Browse..."" button to open Wildlands save games folder."
        '
        'backupGroupBox
        '
        Me.backupGroupBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.backupGroupBox.Controls.Add(Me.latestBackupHelpLabel)
        Me.backupGroupBox.Controls.Add(Me.freqSelectTimeUpDown)
        Me.backupGroupBox.Controls.Add(Me.stopBtn)
        Me.backupGroupBox.Controls.Add(Me.restoreBtn)
        Me.backupGroupBox.Controls.Add(Me.backupHelpLabel)
        Me.backupGroupBox.Controls.Add(Me.backupBtn)
        Me.backupGroupBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.backupGroupBox.ForeColor = System.Drawing.Color.White
        Me.backupGroupBox.Location = New System.Drawing.Point(325, 204)
        Me.backupGroupBox.Name = "backupGroupBox"
        Me.backupGroupBox.Size = New System.Drawing.Size(497, 100)
        Me.backupGroupBox.TabIndex = 4
        Me.backupGroupBox.TabStop = False
        Me.backupGroupBox.Text = "Available tasks"
        '
        'latestBackupHelpLabel
        '
        Me.latestBackupHelpLabel.AutoSize = True
        Me.latestBackupHelpLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.latestBackupHelpLabel.Location = New System.Drawing.Point(306, 23)
        Me.latestBackupHelpLabel.Name = "latestBackupHelpLabel"
        Me.latestBackupHelpLabel.Size = New System.Drawing.Size(183, 17)
        Me.latestBackupHelpLabel.TabIndex = 10
        Me.latestBackupHelpLabel.Text = "Latest backup: No backup yet."
        '
        'freqSelectTimeUpDown
        '
        Me.freqSelectTimeUpDown.Font = New System.Drawing.Font("Segoe UI Semilight", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.freqSelectTimeUpDown.Location = New System.Drawing.Point(13, 60)
        Me.freqSelectTimeUpDown.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.freqSelectTimeUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.freqSelectTimeUpDown.Name = "freqSelectTimeUpDown"
        Me.freqSelectTimeUpDown.ReadOnly = True
        Me.freqSelectTimeUpDown.Size = New System.Drawing.Size(160, 25)
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
        Me.stopBtn.Location = New System.Drawing.Point(284, 54)
        Me.stopBtn.Name = "stopBtn"
        Me.stopBtn.Size = New System.Drawing.Size(95, 35)
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
        Me.restoreBtn.Location = New System.Drawing.Point(385, 54)
        Me.restoreBtn.Name = "restoreBtn"
        Me.restoreBtn.Size = New System.Drawing.Size(100, 35)
        Me.restoreBtn.TabIndex = 8
        Me.restoreBtn.Text = "Restore" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "save games"
        Me.restoreBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.restoreBtn.UseVisualStyleBackColor = False
        '
        'backupHelpLabel
        '
        Me.backupHelpLabel.AutoSize = True
        Me.backupHelpLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.backupHelpLabel.Location = New System.Drawing.Point(10, 23)
        Me.backupHelpLabel.Name = "backupHelpLabel"
        Me.backupHelpLabel.Size = New System.Drawing.Size(244, 17)
        Me.backupHelpLabel.TabIndex = 5
        Me.backupHelpLabel.Text = "Specify the backup frequency in minutes."
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
        Me.backupBtn.Location = New System.Drawing.Point(183, 54)
        Me.backupBtn.Name = "backupBtn"
        Me.backupBtn.Size = New System.Drawing.Size(95, 35)
        Me.backupBtn.TabIndex = 7
        Me.backupBtn.Text = "Start the Backup"
        Me.backupBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.backupBtn.UseVisualStyleBackColor = False
        '
        'logoBigPictureBox
        '
        Me.logoBigPictureBox.BackColor = System.Drawing.Color.Transparent
        Me.logoBigPictureBox.Image = CType(resources.GetObject("logoBigPictureBox.Image"), System.Drawing.Image)
        Me.logoBigPictureBox.Location = New System.Drawing.Point(12, 85)
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
        Me.playGameBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.playGameBtn.FlatAppearance.BorderSize = 0
        Me.playGameBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.playGameBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.playGameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.playGameBtn.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.playGameBtn.ForeColor = System.Drawing.Color.White
        Me.playGameBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.playGameBtn.Location = New System.Drawing.Point(12, 150)
        Me.playGameBtn.Name = "playGameBtn"
        Me.playGameBtn.Size = New System.Drawing.Size(293, 32)
        Me.playGameBtn.TabIndex = 0
        Me.playGameBtn.Text = "Play Ghost Recon Wildlands"
        Me.playGameBtn.UseVisualStyleBackColor = False
        '
        'logTxtBox
        '
        Me.logTxtBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.logTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.logTxtBox.ContextMenuStrip = Me.logTxtBoxContext
        Me.logTxtBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.logTxtBox.ForeColor = System.Drawing.Color.White
        Me.logTxtBox.Location = New System.Drawing.Point(0, 0)
        Me.logTxtBox.Multiline = True
        Me.logTxtBox.Name = "logTxtBox"
        Me.logTxtBox.ReadOnly = True
        Me.logTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.logTxtBox.Size = New System.Drawing.Size(495, 297)
        Me.logTxtBox.TabIndex = 3
        '
        'logTxtBoxContext
        '
        Me.logTxtBoxContext.BackColor = System.Drawing.SystemColors.Control
        Me.logTxtBoxContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyToolStripMenuItem, Me.SelectAllToolStripMenuItem, Me.ToolStripSeparator1, Me.ExportLogToolStripMenuItem})
        Me.logTxtBoxContext.Name = "logTxtBoxContext"
        Me.logTxtBoxContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.logTxtBoxContext.Size = New System.Drawing.Size(168, 76)
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
        Me.confirmExitChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.confirmExitChkBox.ForeColor = System.Drawing.Color.White
        Me.confirmExitChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.confirmExitChkBox.Location = New System.Drawing.Point(14, 230)
        Me.confirmExitChkBox.Name = "confirmExitChkBox"
        Me.confirmExitChkBox.Size = New System.Drawing.Size(212, 21)
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
        Me.confirmStopBackupChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.confirmStopBackupChkBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.confirmStopBackupChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.confirmStopBackupChkBox.Location = New System.Drawing.Point(14, 255)
        Me.confirmStopBackupChkBox.Name = "confirmStopBackupChkBox"
        Me.confirmStopBackupChkBox.Size = New System.Drawing.Size(191, 21)
        Me.confirmStopBackupChkBox.TabIndex = 2
        Me.confirmStopBackupChkBox.Text = "Confirm backup interruption"
        Me.confirmStopBackupChkBox.UseVisualStyleBackColor = False
        '
        'updateCheckerChkBox
        '
        Me.updateCheckerChkBox.AutoSize = True
        Me.updateCheckerChkBox.BackColor = System.Drawing.Color.Transparent
        Me.updateCheckerChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.updateCheckerChkBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.updateCheckerChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.updateCheckerChkBox.Location = New System.Drawing.Point(14, 305)
        Me.updateCheckerChkBox.Name = "updateCheckerChkBox"
        Me.updateCheckerChkBox.Size = New System.Drawing.Size(133, 21)
        Me.updateCheckerChkBox.TabIndex = 11
        Me.updateCheckerChkBox.Text = "Check for updates"
        Me.updateCheckerChkBox.UseVisualStyleBackColor = False
        '
        'topMenuContainer
        '
        Me.topMenuContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.topMenuContainer.Controls.Add(Me.uplayPictureBtn)
        Me.topMenuContainer.Controls.Add(Me.alertDot)
        Me.topMenuContainer.Controls.Add(Me.aboutLabel)
        Me.topMenuContainer.Controls.Add(Me.homePictureBtn)
        Me.topMenuContainer.Controls.Add(Me.settingsLabel)
        Me.topMenuContainer.Controls.Add(Me.logLabel)
        Me.topMenuContainer.Dock = System.Windows.Forms.DockStyle.Top
        Me.topMenuContainer.Location = New System.Drawing.Point(0, 0)
        Me.topMenuContainer.Name = "topMenuContainer"
        Me.topMenuContainer.Size = New System.Drawing.Size(834, 60)
        Me.topMenuContainer.TabIndex = 12
        '
        'uplayPictureBtn
        '
        Me.uplayPictureBtn.BackColor = System.Drawing.Color.Transparent
        Me.uplayPictureBtn.Image = Global.GHOSTbackup.My.Resources.Resources.uplay
        Me.uplayPictureBtn.Location = New System.Drawing.Point(802, 20)
        Me.uplayPictureBtn.Name = "uplayPictureBtn"
        Me.uplayPictureBtn.Size = New System.Drawing.Size(21, 21)
        Me.uplayPictureBtn.TabIndex = 6
        Me.uplayPictureBtn.TabStop = False
        '
        'alertDot
        '
        Me.alertDot.BackColor = System.Drawing.Color.Transparent
        Me.alertDot.Image = CType(resources.GetObject("alertDot.Image"), System.Drawing.Image)
        Me.alertDot.Location = New System.Drawing.Point(190, 24)
        Me.alertDot.Name = "alertDot"
        Me.alertDot.Size = New System.Drawing.Size(10, 10)
        Me.alertDot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.alertDot.TabIndex = 4
        Me.alertDot.TabStop = False
        Me.alertDot.Visible = False
        '
        'aboutLabel
        '
        Me.aboutLabel.AutoSize = True
        Me.aboutLabel.BackColor = System.Drawing.Color.Transparent
        Me.aboutLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aboutLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.aboutLabel.Location = New System.Drawing.Point(225, 20)
        Me.aboutLabel.Name = "aboutLabel"
        Me.aboutLabel.Size = New System.Drawing.Size(57, 21)
        Me.aboutLabel.TabIndex = 2
        Me.aboutLabel.Text = "About"
        '
        'homePictureBtn
        '
        Me.homePictureBtn.BackColor = System.Drawing.Color.Transparent
        Me.homePictureBtn.Image = Global.GHOSTbackup.My.Resources.Resources.home_white
        Me.homePictureBtn.Location = New System.Drawing.Point(12, 20)
        Me.homePictureBtn.Name = "homePictureBtn"
        Me.homePictureBtn.Size = New System.Drawing.Size(21, 21)
        Me.homePictureBtn.TabIndex = 1
        Me.homePictureBtn.TabStop = False
        '
        'settingsLabel
        '
        Me.settingsLabel.AutoSize = True
        Me.settingsLabel.BackColor = System.Drawing.Color.Transparent
        Me.settingsLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.settingsLabel.Location = New System.Drawing.Point(60, 20)
        Me.settingsLabel.Name = "settingsLabel"
        Me.settingsLabel.Size = New System.Drawing.Size(72, 21)
        Me.settingsLabel.TabIndex = 0
        Me.settingsLabel.Text = "Settings"
        '
        'logLabel
        '
        Me.logLabel.AutoSize = True
        Me.logLabel.BackColor = System.Drawing.Color.Transparent
        Me.logLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.logLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.logLabel.Location = New System.Drawing.Point(155, 20)
        Me.logLabel.Name = "logLabel"
        Me.logLabel.Size = New System.Drawing.Size(45, 21)
        Me.logLabel.TabIndex = 3
        Me.logLabel.Text = "Logs"
        '
        'aboutContainer
        '
        Me.aboutContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.aboutContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.aboutContainer.Controls.Add(Me.licenseLabel)
        Me.aboutContainer.Controls.Add(Me.changelogLabel)
        Me.aboutContainer.Controls.Add(Me.supportLabel)
        Me.aboutContainer.Controls.Add(Me.websiteLabel)
        Me.aboutContainer.Controls.Add(Me.appInfoLabel)
        Me.aboutContainer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aboutContainer.Location = New System.Drawing.Point(325, 149)
        Me.aboutContainer.Name = "aboutContainer"
        Me.aboutContainer.Size = New System.Drawing.Size(497, 299)
        Me.aboutContainer.TabIndex = 13
        Me.aboutContainer.Visible = False
        '
        'licenseLabel
        '
        Me.licenseLabel.BackColor = System.Drawing.Color.Transparent
        Me.licenseLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.licenseLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.licenseLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.licenseLabel.Image = CType(resources.GetObject("licenseLabel.Image"), System.Drawing.Image)
        Me.licenseLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.licenseLabel.Location = New System.Drawing.Point(14, 246)
        Me.licenseLabel.Name = "licenseLabel"
        Me.licenseLabel.Size = New System.Drawing.Size(70, 17)
        Me.licenseLabel.TabIndex = 4
        Me.licenseLabel.Text = "License"
        Me.licenseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'changelogLabel
        '
        Me.changelogLabel.BackColor = System.Drawing.Color.Transparent
        Me.changelogLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.changelogLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.changelogLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.changelogLabel.Image = CType(resources.GetObject("changelogLabel.Image"), System.Drawing.Image)
        Me.changelogLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.changelogLabel.Location = New System.Drawing.Point(14, 222)
        Me.changelogLabel.Name = "changelogLabel"
        Me.changelogLabel.Size = New System.Drawing.Size(90, 17)
        Me.changelogLabel.TabIndex = 3
        Me.changelogLabel.Text = "Changelog"
        Me.changelogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'supportLabel
        '
        Me.supportLabel.BackColor = System.Drawing.Color.Transparent
        Me.supportLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.supportLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.supportLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.supportLabel.Image = CType(resources.GetObject("supportLabel.Image"), System.Drawing.Image)
        Me.supportLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.supportLabel.Location = New System.Drawing.Point(14, 198)
        Me.supportLabel.Name = "supportLabel"
        Me.supportLabel.Size = New System.Drawing.Size(75, 17)
        Me.supportLabel.TabIndex = 2
        Me.supportLabel.Text = "Support"
        Me.supportLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'websiteLabel
        '
        Me.websiteLabel.BackColor = System.Drawing.Color.Transparent
        Me.websiteLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.websiteLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.websiteLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.websiteLabel.Image = CType(resources.GetObject("websiteLabel.Image"), System.Drawing.Image)
        Me.websiteLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.websiteLabel.Location = New System.Drawing.Point(14, 174)
        Me.websiteLabel.Name = "websiteLabel"
        Me.websiteLabel.Size = New System.Drawing.Size(117, 17)
        Me.websiteLabel.TabIndex = 1
        Me.websiteLabel.Text = "Official Website"
        Me.websiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'appInfoLabel
        '
        Me.appInfoLabel.AutoSize = True
        Me.appInfoLabel.BackColor = System.Drawing.Color.Transparent
        Me.appInfoLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.appInfoLabel.ForeColor = System.Drawing.Color.White
        Me.appInfoLabel.Location = New System.Drawing.Point(14, 18)
        Me.appInfoLabel.Name = "appInfoLabel"
        Me.appInfoLabel.Size = New System.Drawing.Size(53, 17)
        Me.appInfoLabel.TabIndex = 0
        Me.appInfoLabel.Text = "appInfo"
        '
        'logsContainer
        '
        Me.logsContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.logsContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.logsContainer.Controls.Add(Me.logTxtBox)
        Me.logsContainer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.logsContainer.Location = New System.Drawing.Point(325, 149)
        Me.logsContainer.Name = "logsContainer"
        Me.logsContainer.Size = New System.Drawing.Size(497, 299)
        Me.logsContainer.TabIndex = 16
        Me.logsContainer.Visible = False
        '
        'alertContainer
        '
        Me.alertContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.alertContainer.Controls.Add(Me.alertDescriptionLabel)
        Me.alertContainer.Controls.Add(Me.alertIcon)
        Me.alertContainer.Controls.Add(Me.closeAlertContainerIcon)
        Me.alertContainer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.alertContainer.ForeColor = System.Drawing.Color.White
        Me.alertContainer.Location = New System.Drawing.Point(0, 60)
        Me.alertContainer.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.alertContainer.Name = "alertContainer"
        Me.alertContainer.Size = New System.Drawing.Size(834, 38)
        Me.alertContainer.TabIndex = 17
        Me.alertContainer.Visible = False
        '
        'alertDescriptionLabel
        '
        Me.alertDescriptionLabel.AutoSize = True
        Me.alertDescriptionLabel.BackColor = System.Drawing.Color.Transparent
        Me.alertDescriptionLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.alertDescriptionLabel.Location = New System.Drawing.Point(42, 12)
        Me.alertDescriptionLabel.Name = "alertDescriptionLabel"
        Me.alertDescriptionLabel.Size = New System.Drawing.Size(103, 17)
        Me.alertDescriptionLabel.TabIndex = 2
        Me.alertDescriptionLabel.Text = "alertDescription"
        '
        'alertIcon
        '
        Me.alertIcon.BackColor = System.Drawing.Color.Transparent
        Me.alertIcon.Location = New System.Drawing.Point(12, 8)
        Me.alertIcon.Name = "alertIcon"
        Me.alertIcon.Size = New System.Drawing.Size(24, 24)
        Me.alertIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.alertIcon.TabIndex = 1
        Me.alertIcon.TabStop = False
        '
        'closeAlertContainerIcon
        '
        Me.closeAlertContainerIcon.BackColor = System.Drawing.Color.Transparent
        Me.closeAlertContainerIcon.Cursor = System.Windows.Forms.Cursors.Hand
        Me.closeAlertContainerIcon.Image = CType(resources.GetObject("closeAlertContainerIcon.Image"), System.Drawing.Image)
        Me.closeAlertContainerIcon.Location = New System.Drawing.Point(800, 8)
        Me.closeAlertContainerIcon.Name = "closeAlertContainerIcon"
        Me.closeAlertContainerIcon.Size = New System.Drawing.Size(24, 24)
        Me.closeAlertContainerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.closeAlertContainerIcon.TabIndex = 0
        Me.closeAlertContainerIcon.TabStop = False
        '
        'formPositionChkBox
        '
        Me.formPositionChkBox.AutoSize = True
        Me.formPositionChkBox.BackColor = System.Drawing.Color.Transparent
        Me.formPositionChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.formPositionChkBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.formPositionChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.formPositionChkBox.Location = New System.Drawing.Point(14, 330)
        Me.formPositionChkBox.Name = "formPositionChkBox"
        Me.formPositionChkBox.Size = New System.Drawing.Size(190, 21)
        Me.formPositionChkBox.TabIndex = 18
        Me.formPositionChkBox.Text = "Remember window position"
        Me.formPositionChkBox.UseVisualStyleBackColor = False
        '
        'titleLabel
        '
        Me.titleLabel.AutoSize = True
        Me.titleLabel.BackColor = System.Drawing.Color.Transparent
        Me.titleLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.titleLabel.ForeColor = System.Drawing.Color.White
        Me.titleLabel.Location = New System.Drawing.Point(325, 115)
        Me.titleLabel.Name = "titleLabel"
        Me.titleLabel.Size = New System.Drawing.Size(94, 21)
        Me.titleLabel.TabIndex = 19
        Me.titleLabel.Text = "{titleLabel}"
        Me.titleLabel.Visible = False
        '
        'settingsContainer
        '
        Me.settingsContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.settingsContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.settingsContainer.Controls.Add(Me.settingsNonUplayVersionRestartLabel)
        Me.settingsContainer.Controls.Add(Me.settingsOpenCustomExeFolderBtn)
        Me.settingsContainer.Controls.Add(Me.settingsBrowseCustomExeBtn)
        Me.settingsContainer.Controls.Add(Me.settingsCustomExeTextBox)
        Me.settingsContainer.Controls.Add(Me.settingsNonUplayVersionChkBox)
        Me.settingsContainer.Controls.Add(Me.settingsWhichBackupLabel)
        Me.settingsContainer.Controls.Add(Me.settingsWhichBackupDropdownCombo)
        Me.settingsContainer.Controls.Add(Me.settingsBrowseLogFolderBtn)
        Me.settingsContainer.Controls.Add(Me.settingsBrowseLogFileBtn)
        Me.settingsContainer.Controls.Add(Me.settingsLogFilePathTextBox)
        Me.settingsContainer.Controls.Add(Me.settingsWriteLogToFileChkBox)
        Me.settingsContainer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsContainer.Location = New System.Drawing.Point(325, 149)
        Me.settingsContainer.Name = "settingsContainer"
        Me.settingsContainer.Size = New System.Drawing.Size(497, 299)
        Me.settingsContainer.TabIndex = 20
        Me.settingsContainer.Visible = False
        '
        'settingsNonUplayVersionRestartLabel
        '
        Me.settingsNonUplayVersionRestartLabel.AutoSize = True
        Me.settingsNonUplayVersionRestartLabel.BackColor = System.Drawing.Color.Transparent
        Me.settingsNonUplayVersionRestartLabel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsNonUplayVersionRestartLabel.ForeColor = System.Drawing.Color.Silver
        Me.settingsNonUplayVersionRestartLabel.Location = New System.Drawing.Point(30, 220)
        Me.settingsNonUplayVersionRestartLabel.Name = "settingsNonUplayVersionRestartLabel"
        Me.settingsNonUplayVersionRestartLabel.Size = New System.Drawing.Size(385, 13)
        Me.settingsNonUplayVersionRestartLabel.TabIndex = 26
        Me.settingsNonUplayVersionRestartLabel.Text = "You'll need to restart GHOST Buster in order for the change to take effect."
        '
        'settingsOpenCustomExeFolderBtn
        '
        Me.settingsOpenCustomExeFolderBtn.Enabled = False
        Me.settingsOpenCustomExeFolderBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.settingsOpenCustomExeFolderBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.settingsOpenCustomExeFolderBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.settingsOpenCustomExeFolderBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.settingsOpenCustomExeFolderBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsOpenCustomExeFolderBtn.ForeColor = System.Drawing.Color.White
        Me.settingsOpenCustomExeFolderBtn.Location = New System.Drawing.Point(398, 184)
        Me.settingsOpenCustomExeFolderBtn.Name = "settingsOpenCustomExeFolderBtn"
        Me.settingsOpenCustomExeFolderBtn.Size = New System.Drawing.Size(82, 30)
        Me.settingsOpenCustomExeFolderBtn.TabIndex = 25
        Me.settingsOpenCustomExeFolderBtn.Text = "Open Folder"
        Me.settingsOpenCustomExeFolderBtn.UseVisualStyleBackColor = True
        '
        'settingsBrowseCustomExeBtn
        '
        Me.settingsBrowseCustomExeBtn.Enabled = False
        Me.settingsBrowseCustomExeBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.settingsBrowseCustomExeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.settingsBrowseCustomExeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.settingsBrowseCustomExeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.settingsBrowseCustomExeBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsBrowseCustomExeBtn.ForeColor = System.Drawing.Color.White
        Me.settingsBrowseCustomExeBtn.Image = CType(resources.GetObject("settingsBrowseCustomExeBtn.Image"), System.Drawing.Image)
        Me.settingsBrowseCustomExeBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.settingsBrowseCustomExeBtn.Location = New System.Drawing.Point(302, 184)
        Me.settingsBrowseCustomExeBtn.Name = "settingsBrowseCustomExeBtn"
        Me.settingsBrowseCustomExeBtn.Size = New System.Drawing.Size(90, 30)
        Me.settingsBrowseCustomExeBtn.TabIndex = 24
        Me.settingsBrowseCustomExeBtn.Text = "Browse..."
        Me.settingsBrowseCustomExeBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.settingsBrowseCustomExeBtn.UseVisualStyleBackColor = True
        '
        'settingsCustomExeTextBox
        '
        Me.settingsCustomExeTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.settingsCustomExeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.settingsCustomExeTextBox.Enabled = False
        Me.settingsCustomExeTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.settingsCustomExeTextBox.ForeColor = System.Drawing.Color.White
        Me.settingsCustomExeTextBox.Location = New System.Drawing.Point(32, 187)
        Me.settingsCustomExeTextBox.MaxLength = 256
        Me.settingsCustomExeTextBox.Name = "settingsCustomExeTextBox"
        Me.settingsCustomExeTextBox.ReadOnly = True
        Me.settingsCustomExeTextBox.Size = New System.Drawing.Size(264, 23)
        Me.settingsCustomExeTextBox.TabIndex = 23
        '
        'settingsNonUplayVersionChkBox
        '
        Me.settingsNonUplayVersionChkBox.AutoSize = True
        Me.settingsNonUplayVersionChkBox.BackColor = System.Drawing.Color.Transparent
        Me.settingsNonUplayVersionChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsNonUplayVersionChkBox.ForeColor = System.Drawing.Color.White
        Me.settingsNonUplayVersionChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.settingsNonUplayVersionChkBox.Location = New System.Drawing.Point(14, 160)
        Me.settingsNonUplayVersionChkBox.Name = "settingsNonUplayVersionChkBox"
        Me.settingsNonUplayVersionChkBox.Size = New System.Drawing.Size(284, 21)
        Me.settingsNonUplayVersionChkBox.TabIndex = 22
        Me.settingsNonUplayVersionChkBox.Text = "I'm not using the Uplay version of Wildlands"
        Me.settingsNonUplayVersionChkBox.UseVisualStyleBackColor = False
        '
        'settingsWhichBackupLabel
        '
        Me.settingsWhichBackupLabel.AutoSize = True
        Me.settingsWhichBackupLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsWhichBackupLabel.ForeColor = System.Drawing.Color.White
        Me.settingsWhichBackupLabel.Location = New System.Drawing.Point(14, 90)
        Me.settingsWhichBackupLabel.Name = "settingsWhichBackupLabel"
        Me.settingsWhichBackupLabel.Size = New System.Drawing.Size(229, 17)
        Me.settingsWhichBackupLabel.TabIndex = 21
        Me.settingsWhichBackupLabel.Text = "Choose which backup will be restored"
        '
        'settingsWhichBackupDropdownCombo
        '
        Me.settingsWhichBackupDropdownCombo.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.settingsWhichBackupDropdownCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.settingsWhichBackupDropdownCombo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.settingsWhichBackupDropdownCombo.ForeColor = System.Drawing.Color.White
        Me.settingsWhichBackupDropdownCombo.FormattingEnabled = True
        Me.settingsWhichBackupDropdownCombo.Items.AddRange(New Object() {"Latest", "Second-to-last", "Let me decide"})
        Me.settingsWhichBackupDropdownCombo.Location = New System.Drawing.Point(16, 115)
        Me.settingsWhichBackupDropdownCombo.MaxDropDownItems = 3
        Me.settingsWhichBackupDropdownCombo.Name = "settingsWhichBackupDropdownCombo"
        Me.settingsWhichBackupDropdownCombo.Size = New System.Drawing.Size(464, 23)
        Me.settingsWhichBackupDropdownCombo.TabIndex = 20
        '
        'settingsBrowseLogFolderBtn
        '
        Me.settingsBrowseLogFolderBtn.Enabled = False
        Me.settingsBrowseLogFolderBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.settingsBrowseLogFolderBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.settingsBrowseLogFolderBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.settingsBrowseLogFolderBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.settingsBrowseLogFolderBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsBrowseLogFolderBtn.ForeColor = System.Drawing.Color.White
        Me.settingsBrowseLogFolderBtn.Location = New System.Drawing.Point(430, 42)
        Me.settingsBrowseLogFolderBtn.Name = "settingsBrowseLogFolderBtn"
        Me.settingsBrowseLogFolderBtn.Size = New System.Drawing.Size(50, 30)
        Me.settingsBrowseLogFolderBtn.TabIndex = 19
        Me.settingsBrowseLogFolderBtn.Text = "Open"
        Me.settingsBrowseLogFolderBtn.UseVisualStyleBackColor = True
        '
        'settingsBrowseLogFileBtn
        '
        Me.settingsBrowseLogFileBtn.Enabled = False
        Me.settingsBrowseLogFileBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.settingsBrowseLogFileBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.settingsBrowseLogFileBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.settingsBrowseLogFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.settingsBrowseLogFileBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsBrowseLogFileBtn.ForeColor = System.Drawing.Color.White
        Me.settingsBrowseLogFileBtn.Image = CType(resources.GetObject("settingsBrowseLogFileBtn.Image"), System.Drawing.Image)
        Me.settingsBrowseLogFileBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.settingsBrowseLogFileBtn.Location = New System.Drawing.Point(334, 42)
        Me.settingsBrowseLogFileBtn.Name = "settingsBrowseLogFileBtn"
        Me.settingsBrowseLogFileBtn.Size = New System.Drawing.Size(90, 30)
        Me.settingsBrowseLogFileBtn.TabIndex = 14
        Me.settingsBrowseLogFileBtn.Text = "Browse..."
        Me.settingsBrowseLogFileBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.settingsBrowseLogFileBtn.UseVisualStyleBackColor = True
        '
        'settingsLogFilePathTextBox
        '
        Me.settingsLogFilePathTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.settingsLogFilePathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.settingsLogFilePathTextBox.Enabled = False
        Me.settingsLogFilePathTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.settingsLogFilePathTextBox.ForeColor = System.Drawing.Color.White
        Me.settingsLogFilePathTextBox.Location = New System.Drawing.Point(32, 45)
        Me.settingsLogFilePathTextBox.MaxLength = 256
        Me.settingsLogFilePathTextBox.Name = "settingsLogFilePathTextBox"
        Me.settingsLogFilePathTextBox.ReadOnly = True
        Me.settingsLogFilePathTextBox.Size = New System.Drawing.Size(296, 23)
        Me.settingsLogFilePathTextBox.TabIndex = 6
        '
        'settingsWriteLogToFileChkBox
        '
        Me.settingsWriteLogToFileChkBox.AutoSize = True
        Me.settingsWriteLogToFileChkBox.BackColor = System.Drawing.Color.Transparent
        Me.settingsWriteLogToFileChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsWriteLogToFileChkBox.ForeColor = System.Drawing.Color.White
        Me.settingsWriteLogToFileChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.settingsWriteLogToFileChkBox.Location = New System.Drawing.Point(14, 18)
        Me.settingsWriteLogToFileChkBox.Name = "settingsWriteLogToFileChkBox"
        Me.settingsWriteLogToFileChkBox.Size = New System.Drawing.Size(170, 21)
        Me.settingsWriteLogToFileChkBox.TabIndex = 3
        Me.settingsWriteLogToFileChkBox.Text = "Write events to a log file"
        Me.settingsWriteLogToFileChkBox.UseVisualStyleBackColor = False
        '
        'disableCloudSyncChkBox
        '
        Me.disableCloudSyncChkBox.AutoSize = True
        Me.disableCloudSyncChkBox.BackColor = System.Drawing.Color.Transparent
        Me.disableCloudSyncChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.disableCloudSyncChkBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.disableCloudSyncChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.disableCloudSyncChkBox.Location = New System.Drawing.Point(14, 280)
        Me.disableCloudSyncChkBox.Name = "disableCloudSyncChkBox"
        Me.disableCloudSyncChkBox.Size = New System.Drawing.Size(202, 21)
        Me.disableCloudSyncChkBox.TabIndex = 4
        Me.disableCloudSyncChkBox.Text = "Disable Uplay cloud save sync"
        Me.disableCloudSyncChkBox.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(834, 461)
        Me.Controls.Add(Me.titleLabel)
        Me.Controls.Add(Me.formPositionChkBox)
        Me.Controls.Add(Me.alertContainer)
        Me.Controls.Add(Me.topMenuContainer)
        Me.Controls.Add(Me.updateCheckerChkBox)
        Me.Controls.Add(Me.confirmStopBackupChkBox)
        Me.Controls.Add(Me.confirmExitChkBox)
        Me.Controls.Add(Me.disableCloudSyncChkBox)
        Me.Controls.Add(Me.playGameBtn)
        Me.Controls.Add(Me.logoBigPictureBox)
        Me.Controls.Add(Me.backupGroupBox)
        Me.Controls.Add(Me.pathsGroupBox)
        Me.Controls.Add(Me.settingsContainer)
        Me.Controls.Add(Me.aboutContainer)
        Me.Controls.Add(Me.logsContainer)
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
        Me.topMenuContainer.ResumeLayout(False)
        Me.topMenuContainer.PerformLayout()
        CType(Me.uplayPictureBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.alertDot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.homePictureBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.aboutContainer.ResumeLayout(False)
        Me.aboutContainer.PerformLayout()
        Me.logsContainer.ResumeLayout(False)
        Me.logsContainer.PerformLayout()
        Me.alertContainer.ResumeLayout(False)
        Me.alertContainer.PerformLayout()
        CType(Me.alertIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.closeAlertContainerIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.settingsContainer.ResumeLayout(False)
        Me.settingsContainer.PerformLayout()
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
    Friend WithEvents updateCheckerChkBox As CheckBox
    Friend WithEvents logTxtBoxContext As ContextMenuStrip
    Friend WithEvents CopyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ExportLogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents topMenuContainer As Panel
    Friend WithEvents settingsLabel As Label
    Friend WithEvents homePictureBtn As PictureBox
    Friend WithEvents aboutLabel As Label
    Friend WithEvents aboutContainer As Panel
    Friend WithEvents appInfoLabel As Label
    Friend WithEvents websiteLabel As Label
    Friend WithEvents licenseLabel As Label
    Friend WithEvents changelogLabel As Label
    Friend WithEvents supportLabel As Label
    Friend WithEvents exploreDestLocBtn As Button
    Friend WithEvents exploreSaveLocBtn As Button
    Friend WithEvents logLabel As Label
    Friend WithEvents logsContainer As Panel
    Friend WithEvents alertContainer As Panel
    Friend WithEvents closeAlertContainerIcon As PictureBox
    Friend WithEvents alertIcon As PictureBox
    Friend WithEvents alertDescriptionLabel As Label
    Friend WithEvents formPositionChkBox As CheckBox
    Friend WithEvents alertDot As PictureBox
    Friend WithEvents titleLabel As Label
    Friend WithEvents settingsContainer As Panel
    Friend WithEvents settingsWriteLogToFileChkBox As CheckBox
    Friend WithEvents disableCloudSyncChkBox As CheckBox
    Friend WithEvents settingsLogFilePathTextBox As TextBox
    Friend WithEvents settingsBrowseLogFileBtn As Button
    Friend WithEvents settingsBrowseLogFolderBtn As Button
    Friend WithEvents settingsWhichBackupDropdownCombo As ComboBox
    Friend WithEvents settingsWhichBackupLabel As Label
    Friend WithEvents latestBackupHelpLabel As Label
    Friend WithEvents uplayPictureBtn As PictureBox
    Friend WithEvents settingsOpenCustomExeFolderBtn As Button
    Friend WithEvents settingsBrowseCustomExeBtn As Button
    Friend WithEvents settingsCustomExeTextBox As TextBox
    Friend WithEvents settingsNonUplayVersionChkBox As CheckBox
    Friend WithEvents settingsNonUplayVersionRestartLabel As Label
End Class

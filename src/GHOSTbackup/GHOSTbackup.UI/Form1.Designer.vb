﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.SavegamesLocTextBox = New System.Windows.Forms.TextBox()
        Me.BrowseSavegamesLocBtn = New System.Windows.Forms.Button()
        Me.BrowseBackupLocBtn = New System.Windows.Forms.Button()
        Me.BackupLocTextBox = New System.Windows.Forms.TextBox()
        Me.ExploreSavegamesLocBtn = New System.Windows.Forms.Button()
        Me.ExploreBackupLocBtn = New System.Windows.Forms.Button()
        Me.BackupLocHelpLabel = New System.Windows.Forms.Label()
        Me.SavegamesLocHelpLabel = New System.Windows.Forms.Label()
        Me.StopBtn = New System.Windows.Forms.Button()
        Me.RestoreBtn = New System.Windows.Forms.Button()
        Me.BackupFreqHelp1Label = New System.Windows.Forms.Label()
        Me.BackupBtn = New System.Windows.Forms.Button()
        Me.LogoBigPictureBox = New System.Windows.Forms.PictureBox()
        Me.PlayGameBtn = New System.Windows.Forms.Button()
        Me.LogTxtBox = New System.Windows.Forms.TextBox()
        Me.LogTxtBoxContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfirmExitChkBox = New System.Windows.Forms.CheckBox()
        Me.ConfirmStopBackupChkBox = New System.Windows.Forms.CheckBox()
        Me.CheckUpdatesChkBox = New System.Windows.Forms.CheckBox()
        Me.TopMenuContainer = New System.Windows.Forms.Panel()
        Me.UplayPictureBtn = New System.Windows.Forms.PictureBox()
        Me.AlertDot = New System.Windows.Forms.PictureBox()
        Me.AboutLabel = New System.Windows.Forms.Label()
        Me.HomePictureBtn = New System.Windows.Forms.PictureBox()
        Me.SettingsLabel = New System.Windows.Forms.Label()
        Me.LogLabel = New System.Windows.Forms.Label()
        Me.AboutContainer = New System.Windows.Forms.Panel()
        Me.LicenseLabel = New System.Windows.Forms.Label()
        Me.ChangelogLabel = New System.Windows.Forms.Label()
        Me.SupportLabel = New System.Windows.Forms.Label()
        Me.WebsiteLabel = New System.Windows.Forms.Label()
        Me.AppInfoLabel = New System.Windows.Forms.Label()
        Me.LogsContainer = New System.Windows.Forms.Panel()
        Me.RememberFormPositionChkBox = New System.Windows.Forms.CheckBox()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.SettingsContainer = New System.Windows.Forms.Panel()
        Me.SettingsNonUplayVersionRestartLabel = New System.Windows.Forms.Label()
        Me.SettingsOpenCustomExeFolderBtn = New System.Windows.Forms.Button()
        Me.SettingsBrowseCustomExeBtn = New System.Windows.Forms.Button()
        Me.SettingsCustomExeTextBox = New System.Windows.Forms.TextBox()
        Me.SettingsNonUplayVersionChkBox = New System.Windows.Forms.CheckBox()
        Me.SettingsOpenLogBtn = New System.Windows.Forms.Button()
        Me.SettingsBrowseLogFileBtn = New System.Windows.Forms.Button()
        Me.SettingsLogFilePathTextBox = New System.Windows.Forms.TextBox()
        Me.SettingsWriteLogToFileChkBox = New System.Windows.Forms.CheckBox()
        Me.WhichBackupLabel = New System.Windows.Forms.Label()
        Me.DisableCloudSyncChkBox = New System.Windows.Forms.CheckBox()
        Me.FoldersContainer = New System.Windows.Forms.Panel()
        Me.FoldersTitleLabel = New System.Windows.Forms.Label()
        Me.TasksContainer = New System.Windows.Forms.Panel()
        Me.WhichBackupDropdown = New System.Windows.Forms.ComboBox()
        Me.BackupFreqHelp2Label = New System.Windows.Forms.Label()
        Me.BackupFreqTextBox = New System.Windows.Forms.TextBox()
        Me.LatestBackupHelpLabel = New System.Windows.Forms.Label()
        Me.TasksTitleLabel = New System.Windows.Forms.Label()
        Me.HelpToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.EnableCloudSyncChkBox = New System.Windows.Forms.CheckBox()
        Me.DisplayNotificationChkBox = New System.Windows.Forms.CheckBox()
        CType(Me.LogoBigPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LogTxtBoxContextMenu.SuspendLayout()
        Me.TopMenuContainer.SuspendLayout()
        CType(Me.UplayPictureBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AlertDot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HomePictureBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AboutContainer.SuspendLayout()
        Me.LogsContainer.SuspendLayout()
        Me.SettingsContainer.SuspendLayout()
        Me.FoldersContainer.SuspendLayout()
        Me.TasksContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'SavegamesLocTextBox
        '
        Me.SavegamesLocTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.SavegamesLocTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SavegamesLocTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SavegamesLocTextBox.ForeColor = System.Drawing.Color.White
        Me.SavegamesLocTextBox.Location = New System.Drawing.Point(12, 36)
        Me.SavegamesLocTextBox.Name = "SavegamesLocTextBox"
        Me.SavegamesLocTextBox.ReadOnly = True
        Me.SavegamesLocTextBox.Size = New System.Drawing.Size(321, 23)
        Me.SavegamesLocTextBox.TabIndex = 1
        '
        'BrowseSavegamesLocBtn
        '
        Me.BrowseSavegamesLocBtn.BackColor = System.Drawing.Color.Transparent
        Me.BrowseSavegamesLocBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.BrowseSavegamesLocBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.BrowseSavegamesLocBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.BrowseSavegamesLocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BrowseSavegamesLocBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrowseSavegamesLocBtn.ForeColor = System.Drawing.Color.White
        Me.BrowseSavegamesLocBtn.Image = CType(resources.GetObject("BrowseSavegamesLocBtn.Image"), System.Drawing.Image)
        Me.BrowseSavegamesLocBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BrowseSavegamesLocBtn.Location = New System.Drawing.Point(339, 32)
        Me.BrowseSavegamesLocBtn.Name = "BrowseSavegamesLocBtn"
        Me.BrowseSavegamesLocBtn.Size = New System.Drawing.Size(90, 30)
        Me.BrowseSavegamesLocBtn.TabIndex = 2
        Me.BrowseSavegamesLocBtn.Text = "Browse..."
        Me.BrowseSavegamesLocBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BrowseSavegamesLocBtn.UseVisualStyleBackColor = False
        '
        'BrowseBackupLocBtn
        '
        Me.BrowseBackupLocBtn.BackColor = System.Drawing.Color.Transparent
        Me.BrowseBackupLocBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.BrowseBackupLocBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.BrowseBackupLocBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.BrowseBackupLocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BrowseBackupLocBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrowseBackupLocBtn.ForeColor = System.Drawing.Color.White
        Me.BrowseBackupLocBtn.Image = CType(resources.GetObject("BrowseBackupLocBtn.Image"), System.Drawing.Image)
        Me.BrowseBackupLocBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.BrowseBackupLocBtn.Location = New System.Drawing.Point(339, 90)
        Me.BrowseBackupLocBtn.Name = "BrowseBackupLocBtn"
        Me.BrowseBackupLocBtn.Size = New System.Drawing.Size(90, 30)
        Me.BrowseBackupLocBtn.TabIndex = 6
        Me.BrowseBackupLocBtn.Text = "Browse..."
        Me.BrowseBackupLocBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BrowseBackupLocBtn.UseVisualStyleBackColor = False
        '
        'BackupLocTextBox
        '
        Me.BackupLocTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.BackupLocTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BackupLocTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupLocTextBox.ForeColor = System.Drawing.Color.White
        Me.BackupLocTextBox.Location = New System.Drawing.Point(12, 94)
        Me.BackupLocTextBox.Name = "BackupLocTextBox"
        Me.BackupLocTextBox.ReadOnly = True
        Me.BackupLocTextBox.Size = New System.Drawing.Size(321, 23)
        Me.BackupLocTextBox.TabIndex = 5
        '
        'ExploreSavegamesLocBtn
        '
        Me.ExploreSavegamesLocBtn.BackColor = System.Drawing.Color.Transparent
        Me.ExploreSavegamesLocBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.ExploreSavegamesLocBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.ExploreSavegamesLocBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.ExploreSavegamesLocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExploreSavegamesLocBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExploreSavegamesLocBtn.ForeColor = System.Drawing.Color.White
        Me.ExploreSavegamesLocBtn.Location = New System.Drawing.Point(435, 32)
        Me.ExploreSavegamesLocBtn.Name = "ExploreSavegamesLocBtn"
        Me.ExploreSavegamesLocBtn.Size = New System.Drawing.Size(50, 30)
        Me.ExploreSavegamesLocBtn.TabIndex = 3
        Me.ExploreSavegamesLocBtn.Text = "Open"
        Me.ExploreSavegamesLocBtn.UseVisualStyleBackColor = False
        '
        'ExploreBackupLocBtn
        '
        Me.ExploreBackupLocBtn.BackColor = System.Drawing.Color.Transparent
        Me.ExploreBackupLocBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.ExploreBackupLocBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.ExploreBackupLocBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.ExploreBackupLocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExploreBackupLocBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExploreBackupLocBtn.ForeColor = System.Drawing.Color.White
        Me.ExploreBackupLocBtn.Location = New System.Drawing.Point(435, 90)
        Me.ExploreBackupLocBtn.Name = "ExploreBackupLocBtn"
        Me.ExploreBackupLocBtn.Size = New System.Drawing.Size(50, 30)
        Me.ExploreBackupLocBtn.TabIndex = 7
        Me.ExploreBackupLocBtn.Text = "Open"
        Me.ExploreBackupLocBtn.UseVisualStyleBackColor = False
        '
        'BackupLocHelpLabel
        '
        Me.BackupLocHelpLabel.AutoSize = True
        Me.BackupLocHelpLabel.BackColor = System.Drawing.Color.Transparent
        Me.BackupLocHelpLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupLocHelpLabel.Location = New System.Drawing.Point(10, 70)
        Me.BackupLocHelpLabel.Name = "BackupLocHelpLabel"
        Me.BackupLocHelpLabel.Size = New System.Drawing.Size(88, 17)
        Me.BackupLocHelpLabel.TabIndex = 4
        Me.BackupLocHelpLabel.Text = "Backup folder"
        '
        'SavegamesLocHelpLabel
        '
        Me.SavegamesLocHelpLabel.AutoSize = True
        Me.SavegamesLocHelpLabel.BackColor = System.Drawing.Color.Transparent
        Me.SavegamesLocHelpLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SavegamesLocHelpLabel.Location = New System.Drawing.Point(10, 12)
        Me.SavegamesLocHelpLabel.Name = "SavegamesLocHelpLabel"
        Me.SavegamesLocHelpLabel.Size = New System.Drawing.Size(177, 17)
        Me.SavegamesLocHelpLabel.TabIndex = 0
        Me.SavegamesLocHelpLabel.Text = "Wildlands save games folder"
        '
        'StopBtn
        '
        Me.StopBtn.BackColor = System.Drawing.Color.Transparent
        Me.StopBtn.Enabled = False
        Me.StopBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.StopBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.StopBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.StopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.StopBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StopBtn.ForeColor = System.Drawing.Color.White
        Me.StopBtn.Image = CType(resources.GetObject("StopBtn.Image"), System.Drawing.Image)
        Me.StopBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StopBtn.Location = New System.Drawing.Point(12, 62)
        Me.StopBtn.Name = "StopBtn"
        Me.StopBtn.Size = New System.Drawing.Size(90, 36)
        Me.StopBtn.TabIndex = 5
        Me.StopBtn.Text = "Stop" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Backup"
        Me.StopBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.StopBtn.UseVisualStyleBackColor = False
        '
        'RestoreBtn
        '
        Me.RestoreBtn.BackColor = System.Drawing.Color.Transparent
        Me.RestoreBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.RestoreBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.RestoreBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.RestoreBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RestoreBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RestoreBtn.ForeColor = System.Drawing.Color.White
        Me.RestoreBtn.Image = CType(resources.GetObject("RestoreBtn.Image"), System.Drawing.Image)
        Me.RestoreBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RestoreBtn.Location = New System.Drawing.Point(114, 62)
        Me.RestoreBtn.Name = "RestoreBtn"
        Me.RestoreBtn.Size = New System.Drawing.Size(90, 36)
        Me.RestoreBtn.TabIndex = 6
        Me.RestoreBtn.Text = "Restore" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Backup"
        Me.RestoreBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RestoreBtn.UseVisualStyleBackColor = False
        '
        'BackupFreqHelp1Label
        '
        Me.BackupFreqHelp1Label.AutoSize = True
        Me.BackupFreqHelp1Label.BackColor = System.Drawing.Color.Transparent
        Me.BackupFreqHelp1Label.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupFreqHelp1Label.Location = New System.Drawing.Point(112, 23)
        Me.BackupFreqHelp1Label.Name = "BackupFreqHelp1Label"
        Me.BackupFreqHelp1Label.Size = New System.Drawing.Size(84, 17)
        Me.BackupFreqHelp1Label.TabIndex = 1
        Me.BackupFreqHelp1Label.Text = "Backup every"
        '
        'BackupBtn
        '
        Me.BackupBtn.BackColor = System.Drawing.Color.Transparent
        Me.BackupBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.BackupBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.BackupBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.BackupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BackupBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupBtn.ForeColor = System.Drawing.Color.White
        Me.BackupBtn.Image = CType(resources.GetObject("BackupBtn.Image"), System.Drawing.Image)
        Me.BackupBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BackupBtn.Location = New System.Drawing.Point(12, 14)
        Me.BackupBtn.Name = "BackupBtn"
        Me.BackupBtn.Size = New System.Drawing.Size(90, 36)
        Me.BackupBtn.TabIndex = 0
        Me.BackupBtn.Text = "Start" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Backup"
        Me.BackupBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BackupBtn.UseVisualStyleBackColor = False
        '
        'LogoBigPictureBox
        '
        Me.LogoBigPictureBox.BackColor = System.Drawing.Color.Transparent
        Me.LogoBigPictureBox.Image = CType(resources.GetObject("LogoBigPictureBox.Image"), System.Drawing.Image)
        Me.LogoBigPictureBox.Location = New System.Drawing.Point(12, 85)
        Me.LogoBigPictureBox.Name = "LogoBigPictureBox"
        Me.LogoBigPictureBox.Size = New System.Drawing.Size(293, 50)
        Me.LogoBigPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.LogoBigPictureBox.TabIndex = 10
        Me.LogoBigPictureBox.TabStop = False
        '
        'PlayGameBtn
        '
        Me.PlayGameBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.PlayGameBtn.Enabled = False
        Me.PlayGameBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.PlayGameBtn.FlatAppearance.BorderSize = 0
        Me.PlayGameBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.PlayGameBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PlayGameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PlayGameBtn.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PlayGameBtn.ForeColor = System.Drawing.Color.White
        Me.PlayGameBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PlayGameBtn.Location = New System.Drawing.Point(12, 150)
        Me.PlayGameBtn.Name = "PlayGameBtn"
        Me.PlayGameBtn.Size = New System.Drawing.Size(293, 32)
        Me.PlayGameBtn.TabIndex = 2
        Me.PlayGameBtn.Text = "Play Ghost Recon Wildlands"
        Me.PlayGameBtn.UseVisualStyleBackColor = False
        '
        'LogTxtBox
        '
        Me.LogTxtBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.LogTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LogTxtBox.ContextMenuStrip = Me.LogTxtBoxContextMenu
        Me.LogTxtBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LogTxtBox.ForeColor = System.Drawing.Color.White
        Me.LogTxtBox.Location = New System.Drawing.Point(0, 0)
        Me.LogTxtBox.Multiline = True
        Me.LogTxtBox.Name = "LogTxtBox"
        Me.LogTxtBox.ReadOnly = True
        Me.LogTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.LogTxtBox.Size = New System.Drawing.Size(495, 297)
        Me.LogTxtBox.TabIndex = 0
        '
        'LogTxtBoxContextMenu
        '
        Me.LogTxtBoxContextMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.LogTxtBoxContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyToolStripMenuItem, Me.SelectAllToolStripMenuItem, Me.ExportLogToolStripMenuItem})
        Me.LogTxtBoxContextMenu.Name = "logTxtBoxContext"
        Me.LogTxtBoxContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.LogTxtBoxContextMenu.Size = New System.Drawing.Size(177, 70)
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        Me.CopyToolStripMenuItem.ToolTipText = "Copy the selected text to clipboard."
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select All"
        Me.SelectAllToolStripMenuItem.ToolTipText = "Select all the text."
        '
        'ExportLogToolStripMenuItem
        '
        Me.ExportLogToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.ExportLogToolStripMenuItem.Image = CType(resources.GetObject("ExportLogToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ExportLogToolStripMenuItem.Name = "ExportLogToolStripMenuItem"
        Me.ExportLogToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.ExportLogToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ExportLogToolStripMenuItem.Text = "Export log..."
        Me.ExportLogToolStripMenuItem.ToolTipText = "Export all events to a file."
        '
        'ConfirmExitChkBox
        '
        Me.ConfirmExitChkBox.AutoSize = True
        Me.ConfirmExitChkBox.BackColor = System.Drawing.Color.Transparent
        Me.ConfirmExitChkBox.Checked = True
        Me.ConfirmExitChkBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ConfirmExitChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfirmExitChkBox.ForeColor = System.Drawing.Color.White
        Me.ConfirmExitChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConfirmExitChkBox.Location = New System.Drawing.Point(14, 225)
        Me.ConfirmExitChkBox.Name = "ConfirmExitChkBox"
        Me.ConfirmExitChkBox.Size = New System.Drawing.Size(238, 21)
        Me.ConfirmExitChkBox.TabIndex = 3
        Me.ConfirmExitChkBox.Text = "Confirm exit when backup is running"
        Me.HelpToolTip.SetToolTip(Me.ConfirmExitChkBox, "If checked, GHOST Buster will show a confirmation dialog before exiting when the " &
        "backup process is running.")
        Me.ConfirmExitChkBox.UseVisualStyleBackColor = False
        '
        'ConfirmStopBackupChkBox
        '
        Me.ConfirmStopBackupChkBox.AutoSize = True
        Me.ConfirmStopBackupChkBox.BackColor = System.Drawing.Color.Transparent
        Me.ConfirmStopBackupChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfirmStopBackupChkBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ConfirmStopBackupChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConfirmStopBackupChkBox.Location = New System.Drawing.Point(14, 250)
        Me.ConfirmStopBackupChkBox.Name = "ConfirmStopBackupChkBox"
        Me.ConfirmStopBackupChkBox.Size = New System.Drawing.Size(191, 21)
        Me.ConfirmStopBackupChkBox.TabIndex = 4
        Me.ConfirmStopBackupChkBox.Text = "Confirm backup interruption"
        Me.HelpToolTip.SetToolTip(Me.ConfirmStopBackupChkBox, "If checked, GHOST Buster will show a confirmation dialog before interrupting the " &
        "backup process.")
        Me.ConfirmStopBackupChkBox.UseVisualStyleBackColor = False
        '
        'CheckUpdatesChkBox
        '
        Me.CheckUpdatesChkBox.AutoSize = True
        Me.CheckUpdatesChkBox.BackColor = System.Drawing.Color.Transparent
        Me.CheckUpdatesChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckUpdatesChkBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CheckUpdatesChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CheckUpdatesChkBox.Location = New System.Drawing.Point(14, 350)
        Me.CheckUpdatesChkBox.Name = "CheckUpdatesChkBox"
        Me.CheckUpdatesChkBox.Size = New System.Drawing.Size(133, 21)
        Me.CheckUpdatesChkBox.TabIndex = 8
        Me.CheckUpdatesChkBox.Text = "Check for updates"
        Me.HelpToolTip.SetToolTip(Me.CheckUpdatesChkBox, "If checked, GHOST Buster will check if the current version is up to date when ope" &
        "ning.")
        Me.CheckUpdatesChkBox.UseVisualStyleBackColor = False
        '
        'TopMenuContainer
        '
        Me.TopMenuContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.TopMenuContainer.Controls.Add(Me.UplayPictureBtn)
        Me.TopMenuContainer.Controls.Add(Me.AlertDot)
        Me.TopMenuContainer.Controls.Add(Me.AboutLabel)
        Me.TopMenuContainer.Controls.Add(Me.HomePictureBtn)
        Me.TopMenuContainer.Controls.Add(Me.SettingsLabel)
        Me.TopMenuContainer.Controls.Add(Me.LogLabel)
        Me.TopMenuContainer.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopMenuContainer.Location = New System.Drawing.Point(0, 0)
        Me.TopMenuContainer.Name = "TopMenuContainer"
        Me.TopMenuContainer.Size = New System.Drawing.Size(834, 60)
        Me.TopMenuContainer.TabIndex = 0
        '
        'UplayPictureBtn
        '
        Me.UplayPictureBtn.BackColor = System.Drawing.Color.Transparent
        Me.UplayPictureBtn.Image = Global.GHOSTbackup.My.Resources.Resources.Uplay_Icon
        Me.UplayPictureBtn.Location = New System.Drawing.Point(802, 20)
        Me.UplayPictureBtn.Name = "UplayPictureBtn"
        Me.UplayPictureBtn.Size = New System.Drawing.Size(21, 21)
        Me.UplayPictureBtn.TabIndex = 6
        Me.UplayPictureBtn.TabStop = False
        Me.HelpToolTip.SetToolTip(Me.UplayPictureBtn, "Launch Uplay.")
        '
        'AlertDot
        '
        Me.AlertDot.BackColor = System.Drawing.Color.Transparent
        Me.AlertDot.Image = Global.GHOSTbackup.My.Resources.Resources.Dot
        Me.AlertDot.Location = New System.Drawing.Point(190, 24)
        Me.AlertDot.Name = "AlertDot"
        Me.AlertDot.Size = New System.Drawing.Size(10, 10)
        Me.AlertDot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.AlertDot.TabIndex = 4
        Me.AlertDot.TabStop = False
        Me.AlertDot.Visible = False
        '
        'AboutLabel
        '
        Me.AboutLabel.AutoSize = True
        Me.AboutLabel.BackColor = System.Drawing.Color.Transparent
        Me.AboutLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AboutLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AboutLabel.Location = New System.Drawing.Point(225, 20)
        Me.AboutLabel.Name = "AboutLabel"
        Me.AboutLabel.Size = New System.Drawing.Size(57, 21)
        Me.AboutLabel.TabIndex = 2
        Me.AboutLabel.Text = "About"
        '
        'HomePictureBtn
        '
        Me.HomePictureBtn.BackColor = System.Drawing.Color.Transparent
        Me.HomePictureBtn.Image = Global.GHOSTbackup.My.Resources.Resources.Home_Icon_White
        Me.HomePictureBtn.Location = New System.Drawing.Point(12, 20)
        Me.HomePictureBtn.Name = "HomePictureBtn"
        Me.HomePictureBtn.Size = New System.Drawing.Size(21, 21)
        Me.HomePictureBtn.TabIndex = 1
        Me.HomePictureBtn.TabStop = False
        Me.HelpToolTip.SetToolTip(Me.HomePictureBtn, "Main Screen")
        '
        'SettingsLabel
        '
        Me.SettingsLabel.AutoSize = True
        Me.SettingsLabel.BackColor = System.Drawing.Color.Transparent
        Me.SettingsLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SettingsLabel.Location = New System.Drawing.Point(60, 20)
        Me.SettingsLabel.Name = "SettingsLabel"
        Me.SettingsLabel.Size = New System.Drawing.Size(72, 21)
        Me.SettingsLabel.TabIndex = 0
        Me.SettingsLabel.Text = "Settings"
        '
        'LogLabel
        '
        Me.LogLabel.AutoSize = True
        Me.LogLabel.BackColor = System.Drawing.Color.Transparent
        Me.LogLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LogLabel.Location = New System.Drawing.Point(155, 20)
        Me.LogLabel.Name = "LogLabel"
        Me.LogLabel.Size = New System.Drawing.Size(45, 21)
        Me.LogLabel.TabIndex = 1
        Me.LogLabel.Text = "Logs"
        '
        'AboutContainer
        '
        Me.AboutContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.AboutContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AboutContainer.Controls.Add(Me.LicenseLabel)
        Me.AboutContainer.Controls.Add(Me.ChangelogLabel)
        Me.AboutContainer.Controls.Add(Me.SupportLabel)
        Me.AboutContainer.Controls.Add(Me.WebsiteLabel)
        Me.AboutContainer.Controls.Add(Me.AppInfoLabel)
        Me.AboutContainer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AboutContainer.Location = New System.Drawing.Point(325, 149)
        Me.AboutContainer.Name = "AboutContainer"
        Me.AboutContainer.Size = New System.Drawing.Size(497, 299)
        Me.AboutContainer.TabIndex = 17
        Me.AboutContainer.Visible = False
        '
        'LicenseLabel
        '
        Me.LicenseLabel.BackColor = System.Drawing.Color.Transparent
        Me.LicenseLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LicenseLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LicenseLabel.Image = Global.GHOSTbackup.My.Resources.Resources.About_License_Icon
        Me.LicenseLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LicenseLabel.Location = New System.Drawing.Point(14, 246)
        Me.LicenseLabel.Name = "LicenseLabel"
        Me.LicenseLabel.Size = New System.Drawing.Size(70, 17)
        Me.LicenseLabel.TabIndex = 4
        Me.LicenseLabel.Text = "License"
        Me.LicenseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ChangelogLabel
        '
        Me.ChangelogLabel.BackColor = System.Drawing.Color.Transparent
        Me.ChangelogLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChangelogLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChangelogLabel.Image = Global.GHOSTbackup.My.Resources.Resources.About_Changelog_Icon
        Me.ChangelogLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ChangelogLabel.Location = New System.Drawing.Point(14, 222)
        Me.ChangelogLabel.Name = "ChangelogLabel"
        Me.ChangelogLabel.Size = New System.Drawing.Size(90, 17)
        Me.ChangelogLabel.TabIndex = 3
        Me.ChangelogLabel.Text = "Changelog"
        Me.ChangelogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SupportLabel
        '
        Me.SupportLabel.BackColor = System.Drawing.Color.Transparent
        Me.SupportLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SupportLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SupportLabel.Image = Global.GHOSTbackup.My.Resources.Resources.About_Support_Icon
        Me.SupportLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SupportLabel.Location = New System.Drawing.Point(14, 198)
        Me.SupportLabel.Name = "SupportLabel"
        Me.SupportLabel.Size = New System.Drawing.Size(75, 17)
        Me.SupportLabel.TabIndex = 2
        Me.SupportLabel.Text = "Support"
        Me.SupportLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'WebsiteLabel
        '
        Me.WebsiteLabel.BackColor = System.Drawing.Color.Transparent
        Me.WebsiteLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WebsiteLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.WebsiteLabel.Image = Global.GHOSTbackup.My.Resources.Resources.About_Web_Icon
        Me.WebsiteLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.WebsiteLabel.Location = New System.Drawing.Point(14, 174)
        Me.WebsiteLabel.Name = "WebsiteLabel"
        Me.WebsiteLabel.Size = New System.Drawing.Size(117, 17)
        Me.WebsiteLabel.TabIndex = 1
        Me.WebsiteLabel.Text = "Official Website"
        Me.WebsiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AppInfoLabel
        '
        Me.AppInfoLabel.AutoSize = True
        Me.AppInfoLabel.BackColor = System.Drawing.Color.Transparent
        Me.AppInfoLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppInfoLabel.ForeColor = System.Drawing.Color.White
        Me.AppInfoLabel.Location = New System.Drawing.Point(14, 18)
        Me.AppInfoLabel.Name = "AppInfoLabel"
        Me.AppInfoLabel.Size = New System.Drawing.Size(85, 17)
        Me.AppInfoLabel.TabIndex = 0
        Me.AppInfoLabel.Text = "AppInfoLabel"
        '
        'LogsContainer
        '
        Me.LogsContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.LogsContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LogsContainer.Controls.Add(Me.LogTxtBox)
        Me.LogsContainer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogsContainer.Location = New System.Drawing.Point(325, 149)
        Me.LogsContainer.Name = "LogsContainer"
        Me.LogsContainer.Size = New System.Drawing.Size(497, 299)
        Me.LogsContainer.TabIndex = 16
        Me.LogsContainer.Visible = False
        '
        'RememberFormPositionChkBox
        '
        Me.RememberFormPositionChkBox.AutoSize = True
        Me.RememberFormPositionChkBox.BackColor = System.Drawing.Color.Transparent
        Me.RememberFormPositionChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RememberFormPositionChkBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RememberFormPositionChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RememberFormPositionChkBox.Location = New System.Drawing.Point(14, 375)
        Me.RememberFormPositionChkBox.Name = "RememberFormPositionChkBox"
        Me.RememberFormPositionChkBox.Size = New System.Drawing.Size(190, 21)
        Me.RememberFormPositionChkBox.TabIndex = 9
        Me.RememberFormPositionChkBox.Text = "Remember window position"
        Me.HelpToolTip.SetToolTip(Me.RememberFormPositionChkBox, "If checked, GHOST Buster will restore its position on the screen when reopened.")
        Me.RememberFormPositionChkBox.UseVisualStyleBackColor = False
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.BackColor = System.Drawing.Color.Transparent
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.ForeColor = System.Drawing.Color.White
        Me.TitleLabel.Location = New System.Drawing.Point(322, 115)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(94, 21)
        Me.TitleLabel.TabIndex = 14
        Me.TitleLabel.Text = "{titleLabel}"
        Me.TitleLabel.Visible = False
        '
        'SettingsContainer
        '
        Me.SettingsContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.SettingsContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SettingsContainer.Controls.Add(Me.SettingsNonUplayVersionRestartLabel)
        Me.SettingsContainer.Controls.Add(Me.SettingsOpenCustomExeFolderBtn)
        Me.SettingsContainer.Controls.Add(Me.SettingsBrowseCustomExeBtn)
        Me.SettingsContainer.Controls.Add(Me.SettingsCustomExeTextBox)
        Me.SettingsContainer.Controls.Add(Me.SettingsNonUplayVersionChkBox)
        Me.SettingsContainer.Controls.Add(Me.SettingsOpenLogBtn)
        Me.SettingsContainer.Controls.Add(Me.SettingsBrowseLogFileBtn)
        Me.SettingsContainer.Controls.Add(Me.SettingsLogFilePathTextBox)
        Me.SettingsContainer.Controls.Add(Me.SettingsWriteLogToFileChkBox)
        Me.SettingsContainer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsContainer.Location = New System.Drawing.Point(325, 149)
        Me.SettingsContainer.Name = "SettingsContainer"
        Me.SettingsContainer.Size = New System.Drawing.Size(497, 299)
        Me.SettingsContainer.TabIndex = 15
        Me.SettingsContainer.Visible = False
        '
        'SettingsNonUplayVersionRestartLabel
        '
        Me.SettingsNonUplayVersionRestartLabel.AutoSize = True
        Me.SettingsNonUplayVersionRestartLabel.BackColor = System.Drawing.Color.Transparent
        Me.SettingsNonUplayVersionRestartLabel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsNonUplayVersionRestartLabel.ForeColor = System.Drawing.Color.Silver
        Me.SettingsNonUplayVersionRestartLabel.Location = New System.Drawing.Point(30, 150)
        Me.SettingsNonUplayVersionRestartLabel.Name = "SettingsNonUplayVersionRestartLabel"
        Me.SettingsNonUplayVersionRestartLabel.Size = New System.Drawing.Size(385, 13)
        Me.SettingsNonUplayVersionRestartLabel.TabIndex = 10
        Me.SettingsNonUplayVersionRestartLabel.Text = "You'll need to restart GHOST Buster in order for the change to take effect."
        '
        'SettingsOpenCustomExeFolderBtn
        '
        Me.SettingsOpenCustomExeFolderBtn.Enabled = False
        Me.SettingsOpenCustomExeFolderBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.SettingsOpenCustomExeFolderBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.SettingsOpenCustomExeFolderBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.SettingsOpenCustomExeFolderBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SettingsOpenCustomExeFolderBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsOpenCustomExeFolderBtn.ForeColor = System.Drawing.Color.White
        Me.SettingsOpenCustomExeFolderBtn.Location = New System.Drawing.Point(398, 114)
        Me.SettingsOpenCustomExeFolderBtn.Name = "SettingsOpenCustomExeFolderBtn"
        Me.SettingsOpenCustomExeFolderBtn.Size = New System.Drawing.Size(82, 30)
        Me.SettingsOpenCustomExeFolderBtn.TabIndex = 9
        Me.SettingsOpenCustomExeFolderBtn.Text = "Open Folder"
        Me.SettingsOpenCustomExeFolderBtn.UseVisualStyleBackColor = True
        '
        'SettingsBrowseCustomExeBtn
        '
        Me.SettingsBrowseCustomExeBtn.Enabled = False
        Me.SettingsBrowseCustomExeBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.SettingsBrowseCustomExeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.SettingsBrowseCustomExeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.SettingsBrowseCustomExeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SettingsBrowseCustomExeBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsBrowseCustomExeBtn.ForeColor = System.Drawing.Color.White
        Me.SettingsBrowseCustomExeBtn.Image = CType(resources.GetObject("SettingsBrowseCustomExeBtn.Image"), System.Drawing.Image)
        Me.SettingsBrowseCustomExeBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.SettingsBrowseCustomExeBtn.Location = New System.Drawing.Point(302, 114)
        Me.SettingsBrowseCustomExeBtn.Name = "SettingsBrowseCustomExeBtn"
        Me.SettingsBrowseCustomExeBtn.Size = New System.Drawing.Size(90, 30)
        Me.SettingsBrowseCustomExeBtn.TabIndex = 8
        Me.SettingsBrowseCustomExeBtn.Text = "Browse..."
        Me.SettingsBrowseCustomExeBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SettingsBrowseCustomExeBtn.UseVisualStyleBackColor = True
        '
        'SettingsCustomExeTextBox
        '
        Me.SettingsCustomExeTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.SettingsCustomExeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SettingsCustomExeTextBox.Enabled = False
        Me.SettingsCustomExeTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SettingsCustomExeTextBox.ForeColor = System.Drawing.Color.White
        Me.SettingsCustomExeTextBox.Location = New System.Drawing.Point(32, 117)
        Me.SettingsCustomExeTextBox.MaxLength = 256
        Me.SettingsCustomExeTextBox.Name = "SettingsCustomExeTextBox"
        Me.SettingsCustomExeTextBox.ReadOnly = True
        Me.SettingsCustomExeTextBox.Size = New System.Drawing.Size(264, 23)
        Me.SettingsCustomExeTextBox.TabIndex = 7
        '
        'SettingsNonUplayVersionChkBox
        '
        Me.SettingsNonUplayVersionChkBox.AutoSize = True
        Me.SettingsNonUplayVersionChkBox.BackColor = System.Drawing.Color.Transparent
        Me.SettingsNonUplayVersionChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsNonUplayVersionChkBox.ForeColor = System.Drawing.Color.White
        Me.SettingsNonUplayVersionChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SettingsNonUplayVersionChkBox.Location = New System.Drawing.Point(14, 90)
        Me.SettingsNonUplayVersionChkBox.Name = "SettingsNonUplayVersionChkBox"
        Me.SettingsNonUplayVersionChkBox.Size = New System.Drawing.Size(284, 21)
        Me.SettingsNonUplayVersionChkBox.TabIndex = 6
        Me.SettingsNonUplayVersionChkBox.Text = "I'm not using the Uplay version of Wildlands"
        Me.SettingsNonUplayVersionChkBox.UseVisualStyleBackColor = False
        '
        'SettingsOpenLogBtn
        '
        Me.SettingsOpenLogBtn.Enabled = False
        Me.SettingsOpenLogBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.SettingsOpenLogBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.SettingsOpenLogBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.SettingsOpenLogBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SettingsOpenLogBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsOpenLogBtn.ForeColor = System.Drawing.Color.White
        Me.SettingsOpenLogBtn.Location = New System.Drawing.Point(430, 42)
        Me.SettingsOpenLogBtn.Name = "SettingsOpenLogBtn"
        Me.SettingsOpenLogBtn.Size = New System.Drawing.Size(50, 30)
        Me.SettingsOpenLogBtn.TabIndex = 3
        Me.SettingsOpenLogBtn.Text = "Open"
        Me.SettingsOpenLogBtn.UseVisualStyleBackColor = True
        '
        'SettingsBrowseLogFileBtn
        '
        Me.SettingsBrowseLogFileBtn.Enabled = False
        Me.SettingsBrowseLogFileBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.SettingsBrowseLogFileBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.SettingsBrowseLogFileBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.SettingsBrowseLogFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SettingsBrowseLogFileBtn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsBrowseLogFileBtn.ForeColor = System.Drawing.Color.White
        Me.SettingsBrowseLogFileBtn.Image = CType(resources.GetObject("SettingsBrowseLogFileBtn.Image"), System.Drawing.Image)
        Me.SettingsBrowseLogFileBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.SettingsBrowseLogFileBtn.Location = New System.Drawing.Point(334, 42)
        Me.SettingsBrowseLogFileBtn.Name = "SettingsBrowseLogFileBtn"
        Me.SettingsBrowseLogFileBtn.Size = New System.Drawing.Size(90, 30)
        Me.SettingsBrowseLogFileBtn.TabIndex = 2
        Me.SettingsBrowseLogFileBtn.Text = "Browse..."
        Me.SettingsBrowseLogFileBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SettingsBrowseLogFileBtn.UseVisualStyleBackColor = True
        '
        'SettingsLogFilePathTextBox
        '
        Me.SettingsLogFilePathTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.SettingsLogFilePathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SettingsLogFilePathTextBox.Enabled = False
        Me.SettingsLogFilePathTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SettingsLogFilePathTextBox.ForeColor = System.Drawing.Color.White
        Me.SettingsLogFilePathTextBox.Location = New System.Drawing.Point(32, 45)
        Me.SettingsLogFilePathTextBox.Name = "SettingsLogFilePathTextBox"
        Me.SettingsLogFilePathTextBox.ReadOnly = True
        Me.SettingsLogFilePathTextBox.Size = New System.Drawing.Size(296, 23)
        Me.SettingsLogFilePathTextBox.TabIndex = 1
        '
        'SettingsWriteLogToFileChkBox
        '
        Me.SettingsWriteLogToFileChkBox.AutoSize = True
        Me.SettingsWriteLogToFileChkBox.BackColor = System.Drawing.Color.Transparent
        Me.SettingsWriteLogToFileChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsWriteLogToFileChkBox.ForeColor = System.Drawing.Color.White
        Me.SettingsWriteLogToFileChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SettingsWriteLogToFileChkBox.Location = New System.Drawing.Point(14, 18)
        Me.SettingsWriteLogToFileChkBox.Name = "SettingsWriteLogToFileChkBox"
        Me.SettingsWriteLogToFileChkBox.Size = New System.Drawing.Size(170, 21)
        Me.SettingsWriteLogToFileChkBox.TabIndex = 0
        Me.SettingsWriteLogToFileChkBox.Text = "Write events to a log file"
        Me.SettingsWriteLogToFileChkBox.UseVisualStyleBackColor = False
        '
        'WhichBackupLabel
        '
        Me.WhichBackupLabel.AutoSize = True
        Me.WhichBackupLabel.BackColor = System.Drawing.Color.Transparent
        Me.WhichBackupLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WhichBackupLabel.ForeColor = System.Drawing.Color.White
        Me.WhichBackupLabel.Location = New System.Drawing.Point(212, 54)
        Me.WhichBackupLabel.Name = "WhichBackupLabel"
        Me.WhichBackupLabel.Size = New System.Drawing.Size(111, 17)
        Me.WhichBackupLabel.TabIndex = 7
        Me.WhichBackupLabel.Text = "Backup to restore"
        '
        'DisableCloudSyncChkBox
        '
        Me.DisableCloudSyncChkBox.AutoSize = True
        Me.DisableCloudSyncChkBox.BackColor = System.Drawing.Color.Transparent
        Me.DisableCloudSyncChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DisableCloudSyncChkBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DisableCloudSyncChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DisableCloudSyncChkBox.Location = New System.Drawing.Point(14, 300)
        Me.DisableCloudSyncChkBox.Name = "DisableCloudSyncChkBox"
        Me.DisableCloudSyncChkBox.Size = New System.Drawing.Size(267, 21)
        Me.DisableCloudSyncChkBox.TabIndex = 6
        Me.DisableCloudSyncChkBox.Text = "Disable Uplay cloud save sync on restore"
        Me.HelpToolTip.SetToolTip(Me.DisableCloudSyncChkBox, "If checked, GHOST Buster will disable Uplay cloud save synchronization before res" &
        "toring a backup.")
        Me.DisableCloudSyncChkBox.UseVisualStyleBackColor = False
        '
        'FoldersContainer
        '
        Me.FoldersContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.FoldersContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FoldersContainer.Controls.Add(Me.ExploreSavegamesLocBtn)
        Me.FoldersContainer.Controls.Add(Me.SavegamesLocTextBox)
        Me.FoldersContainer.Controls.Add(Me.ExploreBackupLocBtn)
        Me.FoldersContainer.Controls.Add(Me.BrowseSavegamesLocBtn)
        Me.FoldersContainer.Controls.Add(Me.BackupLocHelpLabel)
        Me.FoldersContainer.Controls.Add(Me.BrowseBackupLocBtn)
        Me.FoldersContainer.Controls.Add(Me.SavegamesLocHelpLabel)
        Me.FoldersContainer.Controls.Add(Me.BackupLocTextBox)
        Me.FoldersContainer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FoldersContainer.ForeColor = System.Drawing.Color.White
        Me.FoldersContainer.Location = New System.Drawing.Point(325, 311)
        Me.FoldersContainer.Name = "FoldersContainer"
        Me.FoldersContainer.Size = New System.Drawing.Size(497, 137)
        Me.FoldersContainer.TabIndex = 13
        '
        'FoldersTitleLabel
        '
        Me.FoldersTitleLabel.AutoSize = True
        Me.FoldersTitleLabel.BackColor = System.Drawing.Color.Transparent
        Me.FoldersTitleLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FoldersTitleLabel.ForeColor = System.Drawing.Color.White
        Me.FoldersTitleLabel.Location = New System.Drawing.Point(322, 277)
        Me.FoldersTitleLabel.Name = "FoldersTitleLabel"
        Me.FoldersTitleLabel.Size = New System.Drawing.Size(65, 21)
        Me.FoldersTitleLabel.TabIndex = 12
        Me.FoldersTitleLabel.Text = "Folders"
        '
        'TasksContainer
        '
        Me.TasksContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.TasksContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TasksContainer.Controls.Add(Me.WhichBackupDropdown)
        Me.TasksContainer.Controls.Add(Me.BackupFreqHelp2Label)
        Me.TasksContainer.Controls.Add(Me.BackupFreqTextBox)
        Me.TasksContainer.Controls.Add(Me.LatestBackupHelpLabel)
        Me.TasksContainer.Controls.Add(Me.BackupFreqHelp1Label)
        Me.TasksContainer.Controls.Add(Me.BackupBtn)
        Me.TasksContainer.Controls.Add(Me.StopBtn)
        Me.TasksContainer.Controls.Add(Me.WhichBackupLabel)
        Me.TasksContainer.Controls.Add(Me.RestoreBtn)
        Me.TasksContainer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TasksContainer.ForeColor = System.Drawing.Color.White
        Me.TasksContainer.Location = New System.Drawing.Point(325, 149)
        Me.TasksContainer.Name = "TasksContainer"
        Me.TasksContainer.Size = New System.Drawing.Size(497, 115)
        Me.TasksContainer.TabIndex = 11
        '
        'WhichBackupDropdown
        '
        Me.WhichBackupDropdown.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.WhichBackupDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.WhichBackupDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.WhichBackupDropdown.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WhichBackupDropdown.ForeColor = System.Drawing.Color.White
        Me.WhichBackupDropdown.Items.AddRange(New Object() {"Latest", "Second-to-last", "Choose"})
        Me.WhichBackupDropdown.Location = New System.Drawing.Point(214, 76)
        Me.WhichBackupDropdown.Name = "WhichBackupDropdown"
        Me.WhichBackupDropdown.Size = New System.Drawing.Size(271, 25)
        Me.WhichBackupDropdown.TabIndex = 8
        '
        'BackupFreqHelp2Label
        '
        Me.BackupFreqHelp2Label.AutoSize = True
        Me.BackupFreqHelp2Label.BackColor = System.Drawing.Color.Transparent
        Me.BackupFreqHelp2Label.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupFreqHelp2Label.Location = New System.Drawing.Point(233, 23)
        Me.BackupFreqHelp2Label.Name = "BackupFreqHelp2Label"
        Me.BackupFreqHelp2Label.Size = New System.Drawing.Size(56, 17)
        Me.BackupFreqHelp2Label.TabIndex = 3
        Me.BackupFreqHelp2Label.Text = "minutes."
        '
        'BackupFreqTextBox
        '
        Me.BackupFreqTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.BackupFreqTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BackupFreqTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupFreqTextBox.ForeColor = System.Drawing.Color.White
        Me.BackupFreqTextBox.Location = New System.Drawing.Point(196, 22)
        Me.BackupFreqTextBox.MaxLength = 3
        Me.BackupFreqTextBox.Name = "BackupFreqTextBox"
        Me.BackupFreqTextBox.ShortcutsEnabled = False
        Me.BackupFreqTextBox.Size = New System.Drawing.Size(34, 23)
        Me.BackupFreqTextBox.TabIndex = 2
        Me.BackupFreqTextBox.Text = "5"
        Me.BackupFreqTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LatestBackupHelpLabel
        '
        Me.LatestBackupHelpLabel.AutoSize = True
        Me.LatestBackupHelpLabel.BackColor = System.Drawing.Color.Transparent
        Me.LatestBackupHelpLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LatestBackupHelpLabel.Location = New System.Drawing.Point(286, 23)
        Me.LatestBackupHelpLabel.Name = "LatestBackupHelpLabel"
        Me.LatestBackupHelpLabel.Size = New System.Drawing.Size(183, 17)
        Me.LatestBackupHelpLabel.TabIndex = 4
        Me.LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
        '
        'TasksTitleLabel
        '
        Me.TasksTitleLabel.AutoSize = True
        Me.TasksTitleLabel.BackColor = System.Drawing.Color.Transparent
        Me.TasksTitleLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TasksTitleLabel.ForeColor = System.Drawing.Color.White
        Me.TasksTitleLabel.Location = New System.Drawing.Point(322, 115)
        Me.TasksTitleLabel.Name = "TasksTitleLabel"
        Me.TasksTitleLabel.Size = New System.Drawing.Size(50, 21)
        Me.TasksTitleLabel.TabIndex = 10
        Me.TasksTitleLabel.Text = "Tasks"
        '
        'HelpToolTip
        '
        Me.HelpToolTip.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.HelpToolTip.ForeColor = System.Drawing.Color.White
        Me.HelpToolTip.OwnerDraw = True
        Me.HelpToolTip.UseFading = False
        '
        'EnableCloudSyncChkBox
        '
        Me.EnableCloudSyncChkBox.AutoSize = True
        Me.EnableCloudSyncChkBox.BackColor = System.Drawing.Color.Transparent
        Me.EnableCloudSyncChkBox.Checked = True
        Me.EnableCloudSyncChkBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EnableCloudSyncChkBox.Enabled = False
        Me.EnableCloudSyncChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnableCloudSyncChkBox.ForeColor = System.Drawing.Color.White
        Me.EnableCloudSyncChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EnableCloudSyncChkBox.Location = New System.Drawing.Point(14, 325)
        Me.EnableCloudSyncChkBox.Name = "EnableCloudSyncChkBox"
        Me.EnableCloudSyncChkBox.Size = New System.Drawing.Size(241, 21)
        Me.EnableCloudSyncChkBox.TabIndex = 7
        Me.EnableCloudSyncChkBox.Text = "Enable Uplay cloud save sync on exit"
        Me.HelpToolTip.SetToolTip(Me.EnableCloudSyncChkBox, "If checked, GHOST Buster will enable  Uplay cloud save synchronization again befo" &
        "re  quitting.")
        Me.EnableCloudSyncChkBox.UseVisualStyleBackColor = False
        '
        'DisplayNotificationChkBox
        '
        Me.DisplayNotificationChkBox.AutoSize = True
        Me.DisplayNotificationChkBox.BackColor = System.Drawing.Color.Transparent
        Me.DisplayNotificationChkBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DisplayNotificationChkBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DisplayNotificationChkBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DisplayNotificationChkBox.Location = New System.Drawing.Point(14, 275)
        Me.DisplayNotificationChkBox.Name = "DisplayNotificationChkBox"
        Me.DisplayNotificationChkBox.Size = New System.Drawing.Size(233, 21)
        Me.DisplayNotificationChkBox.TabIndex = 5
        Me.DisplayNotificationChkBox.Text = "Display notifications about backups"
        Me.HelpToolTip.SetToolTip(Me.DisplayNotificationChkBox, "If checked, GHOST Buster will display a notification at the edge of the screen ab" &
        "out backups.")
        Me.DisplayNotificationChkBox.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(834, 461)
        Me.Controls.Add(Me.DisplayNotificationChkBox)
        Me.Controls.Add(Me.EnableCloudSyncChkBox)
        Me.Controls.Add(Me.TasksTitleLabel)
        Me.Controls.Add(Me.TasksContainer)
        Me.Controls.Add(Me.FoldersTitleLabel)
        Me.Controls.Add(Me.FoldersContainer)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.RememberFormPositionChkBox)
        Me.Controls.Add(Me.TopMenuContainer)
        Me.Controls.Add(Me.CheckUpdatesChkBox)
        Me.Controls.Add(Me.ConfirmStopBackupChkBox)
        Me.Controls.Add(Me.ConfirmExitChkBox)
        Me.Controls.Add(Me.DisableCloudSyncChkBox)
        Me.Controls.Add(Me.PlayGameBtn)
        Me.Controls.Add(Me.LogoBigPictureBox)
        Me.Controls.Add(Me.SettingsContainer)
        Me.Controls.Add(Me.AboutContainer)
        Me.Controls.Add(Me.LogsContainer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GHOST Buster"
        CType(Me.LogoBigPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LogTxtBoxContextMenu.ResumeLayout(False)
        Me.TopMenuContainer.ResumeLayout(False)
        Me.TopMenuContainer.PerformLayout()
        CType(Me.UplayPictureBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AlertDot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HomePictureBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AboutContainer.ResumeLayout(False)
        Me.AboutContainer.PerformLayout()
        Me.LogsContainer.ResumeLayout(False)
        Me.LogsContainer.PerformLayout()
        Me.SettingsContainer.ResumeLayout(False)
        Me.SettingsContainer.PerformLayout()
        Me.FoldersContainer.ResumeLayout(False)
        Me.FoldersContainer.PerformLayout()
        Me.TasksContainer.ResumeLayout(False)
        Me.TasksContainer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SavegamesLocTextBox As TextBox
    Friend WithEvents BrowseSavegamesLocBtn As Button
    Friend WithEvents BrowseBackupLocBtn As Button
    Friend WithEvents BackupLocTextBox As TextBox
    Friend WithEvents SavegamesLocHelpLabel As Label
    Friend WithEvents BackupLocHelpLabel As Label
    Friend WithEvents BackupBtn As Button
    Friend WithEvents StopBtn As Button
    Friend WithEvents RestoreBtn As Button
    Friend WithEvents LogoBigPictureBox As PictureBox
    Friend WithEvents PlayGameBtn As Button
    Friend WithEvents LogTxtBox As TextBox
    Friend WithEvents ConfirmExitChkBox As CheckBox
    Friend WithEvents ConfirmStopBackupChkBox As CheckBox
    Friend WithEvents BackupFreqHelp1Label As Label
    Friend WithEvents CheckUpdatesChkBox As CheckBox
    Friend WithEvents LogTxtBoxContextMenu As ContextMenuStrip
    Friend WithEvents CopyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportLogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TopMenuContainer As Panel
    Friend WithEvents SettingsLabel As Label
    Friend WithEvents HomePictureBtn As PictureBox
    Friend WithEvents AboutLabel As Label
    Friend WithEvents AboutContainer As Panel
    Friend WithEvents AppInfoLabel As Label
    Friend WithEvents WebsiteLabel As Label
    Friend WithEvents LicenseLabel As Label
    Friend WithEvents ChangelogLabel As Label
    Friend WithEvents SupportLabel As Label
    Friend WithEvents ExploreBackupLocBtn As Button
    Friend WithEvents ExploreSavegamesLocBtn As Button
    Friend WithEvents LogLabel As Label
    Friend WithEvents LogsContainer As Panel
    Friend WithEvents RememberFormPositionChkBox As CheckBox
    Friend WithEvents AlertDot As PictureBox
    Friend WithEvents TitleLabel As Label
    Friend WithEvents SettingsContainer As Panel
    Friend WithEvents SettingsWriteLogToFileChkBox As CheckBox
    Friend WithEvents DisableCloudSyncChkBox As CheckBox
    Friend WithEvents SettingsLogFilePathTextBox As TextBox
    Friend WithEvents SettingsBrowseLogFileBtn As Button
    Friend WithEvents SettingsOpenLogBtn As Button
    Friend WithEvents WhichBackupLabel As Label
    Friend WithEvents UplayPictureBtn As PictureBox
    Friend WithEvents SettingsOpenCustomExeFolderBtn As Button
    Friend WithEvents SettingsBrowseCustomExeBtn As Button
    Friend WithEvents SettingsCustomExeTextBox As TextBox
    Friend WithEvents SettingsNonUplayVersionChkBox As CheckBox
    Friend WithEvents SettingsNonUplayVersionRestartLabel As Label
    Friend WithEvents FoldersContainer As Panel
    Friend WithEvents FoldersTitleLabel As Label
    Friend WithEvents TasksContainer As Panel
    Friend WithEvents TasksTitleLabel As Label
    Friend WithEvents LatestBackupHelpLabel As Label
    Friend WithEvents HelpToolTip As ToolTip
    Friend WithEvents EnableCloudSyncChkBox As CheckBox
    Friend WithEvents DisplayNotificationChkBox As CheckBox
    Friend WithEvents BackupFreqTextBox As TextBox
    Friend WithEvents BackupFreqHelp2Label As Label
    Friend WithEvents WhichBackupDropdown As ComboBox
End Class
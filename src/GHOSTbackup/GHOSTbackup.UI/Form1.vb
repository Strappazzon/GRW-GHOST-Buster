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

Imports System.IO
Imports GHOSTbackup.BackupHelper
Imports GHOSTbackup.Updater
Imports GHOSTbackup.UplayHelper
Imports GHOSTbackup.Var
Imports GHOSTbackup.WildlandsHelper

Public Class Form1

#Region "Draw Events"
    Private Sub HelpToolTip_Draw(sender As Object, e As DrawToolTipEventArgs) Handles HelpToolTip.Draw
        'Draw tooltip with custom colors
        e.DrawBackground()
        'Don't draw the border
        'e.DrawBorder()
        e.DrawText()
    End Sub
#End Region

#Region "Form Events"
    Private Sub LoadFormPosition()
        If Settings.RememberFormPosition = True Then
            Dim FormLocation As Point = Settings.FormPosition

            If FormLocation.X <> -1 Or FormLocation.Y <> -1 Then
                Dim LocationVisible As Boolean = False
                For Each S As Screen In Screen.AllScreens
                    If S.Bounds.Contains(FormLocation) Then
                        LocationVisible = True
                    End If
                Next

                If LocationVisible Then
                    StartPosition = FormStartPosition.Manual
                    Location = FormLocation
                End If
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load settings and set defaults
        Settings.Init()
        LoadFormPosition()

        'Load localization
        Localization.Load()

        'Align form items
        LogLabel.Location = New Point(UplayBtn.Location.X + UplayBtn.Width + 20, 20)
        AlertDot.Location = New Point(LogLabel.Location.X + LogLabel.Width - 10, 22)
        AboutLabel.Location = New Point(LogLabel.Location.X + LogLabel.Width + 20, 20)
        'Remove controls
        Controls.Remove(AboutContainer)
        Controls.Remove(SettingsContainer)
        Controls.Remove(LogsContainer)

        'Start logging session
        Logger.StartSession()

        'Get Wildlands installation directory
        GetWildlandsInstall()

        'Get Uplay installation directory
        GetUplayInstall()

        'Check if save games directory exists
        If SavegamesLocTextBox.Text <> "" AndAlso Not Directory.Exists(SavegamesLocTextBox.Text) Then
            Logger.Log("[WARNING] Wildlands save games folder " & SavegamesLocTextBox.Text & " no longer exists.")
            Banner.Show(48, Localization.GetString("banner_savegames_folder_deleted"))
            SavegamesLocTextBox.Text = ""
        End If

        'Check if backup directory exists
        If BackupLocTextBox.Text <> "" AndAlso Not Directory.Exists(BackupLocTextBox.Text) Then
            Logger.Log("[WARNING] Backup folder " & BackupLocTextBox.Text & " no longer exists.")
            Banner.Show(48, Localization.GetString("banner_backup_folder_deleted"))
            BackupLocTextBox.Text = ""
        End If

        'Detect latest backup timestamp
        If BackupLocTextBox.Text <> "" Then
            LatestBackupHelpLabel.Text = Localization.GetString("ui_tasks_latest_loading")
            DetectLatestBackup()
        End If

        'Check for updates
        CheckUpdates()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If IsBackupRunning = True AndAlso SettingsConfirmExitChkBox.Checked = True Then
            CustomMsgBox.Show(Localization.GetString("msgbox_confirm_exit"), Localization.GetString("msgbox_confirm_exit_title"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If CustomMsgBox.DialogResult = DialogResult.No OrElse CustomMsgBox.DialogResult = DialogResult.Cancel Then
                e.Cancel = True
            Else
                'Save settings before quitting
                Settings.Save()

                'Enable Uplay cloud save synchronization again before quitting
                If SettingsDisableCloudSyncChkBox.Checked = True AndAlso (SettingsEnableCloudSyncChkBox.Enabled = True And SettingsEnableCloudSyncChkBox.Checked = True) Then
                    EnableCloudSync()
                End If
            End If
        Else
            'Save settings before quitting
            Settings.Save()

            'Enable Uplay cloud save synchronization again before quitting
            If SettingsDisableCloudSyncChkBox.Checked = True AndAlso (SettingsEnableCloudSyncChkBox.Enabled = True And SettingsEnableCloudSyncChkBox.Checked = True) Then
                EnableCloudSync()
            End If
        End If
    End Sub
#End Region

#Region "Main Screen"
    Private Sub UplayBtn_Click(sender As Object, e As EventArgs) Handles UplayBtn.Click
        'Launch Uplay only if it's installed
        If UplayPath <> Nothing Then
            Process.Start(UplayPath & "Uplay.exe")
        Else
            Banner.Show(64, Localization.GetString("banner_uplay_not_installed"))
        End If
    End Sub

    Private Sub LogLabel_Click(sender As Object, e As EventArgs) Handles LogLabel.Click
        'Remove background image
        BackgroundImage = Nothing
        'Change top menu labels color
        TopMenuContainer.BackColor = Color.FromArgb(255, 22, 26, 31)
        LogLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        AboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        'Change buttons image and color
        SidemenuTasks.Image = My.Resources.Tasks
        SidemenuTasks.ForeColor = Color.FromArgb(255, 85, 170, 255)
        SidemenuSettings.Image = My.Resources.Settings
        SidemenuSettings.ForeColor = Color.FromArgb(255, 85, 170, 255)
        'Change section title
        TitleLabel.Text = Localization.GetString("ui_title_logs")
        'Remove controls
        Controls.Remove(TasksContainer)
        Controls.Remove(FoldersTitleLabel)
        Controls.Remove(FoldersContainer)
        Controls.Remove(AboutContainer)
        Controls.Remove(SettingsContainer)
        AlertDot.Visible = False
        'Show logs
        Controls.Add(LogsContainer)
        'Close the alert banner when switching to Logs tab
        Banner.CloseBanner_Click(sender, e)
        'Scroll to the last line when switching to the Logs tab
        LogTxtBox.ScrollToCaret()
    End Sub

    Private Sub AboutLabel_Click(sender As Object, e As EventArgs) Handles AboutLabel.Click
        'Restore background image
        BackgroundImage = My.Resources.Bg
        'Change top menu labels color
        TopMenuContainer.BackColor = Color.FromArgb(180, 22, 26, 31)
        LogLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        AboutLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        'Change buttons image and color
        SidemenuTasks.Image = My.Resources.Tasks
        SidemenuTasks.ForeColor = Color.FromArgb(255, 85, 170, 255)
        SidemenuSettings.Image = My.Resources.Settings
        SidemenuSettings.ForeColor = Color.FromArgb(255, 85, 170, 255)
        'Change section title
        TitleLabel.Text = Localization.GetString("ui_title_about")
        'Remove controls
        Controls.Remove(TasksContainer)
        Controls.Remove(FoldersTitleLabel)
        Controls.Remove(FoldersContainer)
        Controls.Remove(LogsContainer)
        Controls.Remove(SettingsContainer)
        'Show about section
        Controls.Add(AboutContainer)
    End Sub

    Private Sub PlayGameBtn_Click(sender As Object, e As EventArgs) Handles PlayGameBtn.Click
        Process.Start(GamePath & "GRW.exe")
    End Sub

    Private Sub SidemenuTasks_Click(sender As Object, e As EventArgs) Handles SidemenuTasks.Click
        'Restore background image
        BackgroundImage = My.Resources.Bg
        'Change top menu labels color
        TopMenuContainer.BackColor = Color.FromArgb(180, 22, 26, 31)
        LogLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        AboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        'Change buttons image and color
        SidemenuTasks.Image = My.Resources.Tasks_White
        SidemenuTasks.ForeColor = Color.FromArgb(255, 255, 255, 255)
        SidemenuSettings.Image = My.Resources.Settings
        SidemenuSettings.ForeColor = Color.FromArgb(255, 85, 170, 255)
        'Change section title
        TitleLabel.Text = Localization.GetString("ui_title_tasks")
        'Remove controls
        Controls.Remove(LogsContainer)
        Controls.Remove(AboutContainer)
        Controls.Remove(SettingsContainer)
        'Show tasks section
        Controls.Add(TasksContainer)
        Controls.Add(FoldersTitleLabel)
        Controls.Add(FoldersContainer)
    End Sub

    Private Sub SidemenuSettings_Click(sender As Object, e As EventArgs) Handles SidemenuSettings.Click
        'Remove background image
        BackgroundImage = Nothing
        'Change top menu labels color
        TopMenuContainer.BackColor = Color.FromArgb(255, 22, 26, 31)
        LogLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        AboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        'Change buttons image and color
        SidemenuTasks.Image = My.Resources.Tasks
        SidemenuTasks.ForeColor = Color.FromArgb(255, 255, 255, 255)
        SidemenuSettings.Image = My.Resources.Settings_White
        SidemenuSettings.ForeColor = Color.FromArgb(255, 255, 255, 255)
        'Change section title
        TitleLabel.Text = Localization.GetString("ui_title_settings")
        'Remove controls
        Controls.Remove(LogsContainer)
        Controls.Remove(AboutContainer)
        Controls.Remove(TasksContainer)
        Controls.Remove(FoldersTitleLabel)
        Controls.Remove(FoldersContainer)
        'Show settings
        Controls.Add(SettingsContainer)
        SettingsContainer.Visible = True
    End Sub

    Private Sub BackupBtn_Click(sender As Object, e As EventArgs) Handles BackupBtn.Click
        PerformFirstBackup()
    End Sub

    Private Sub StopBtn_Click(sender As Object, e As EventArgs) Handles StopBtn.Click
        If SettingsConfirmStopBackupChkBox.Checked = True Then
            CustomMsgBox.Show(Localization.GetString("msgbox_confirm_backup_interruption"), Localization.GetString("msgbox_confirm_backup_interruption_title"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If CustomMsgBox.DialogResult = DialogResult.Yes Then
                StopBackup()
                Logger.Log("[INFO] Backup interrupted by the user.")
            End If
        Else
            StopBackup()
            Logger.Log("[INFO] Backup interrupted by the user.")
        End If
    End Sub

    Private Sub RestoreBtn_Click(sender As Object, e As EventArgs) Handles RestoreBtn.Click
        If SavegamesLocTextBox.Text = "" Or BackupLocTextBox.Text = "" Then
            Banner.Show(64, Localization.GetString("banner_specify_folders_info"))
        ElseIf IsGameRunning = True Then
            Banner.Show(64, Localization.GetString("banner_quit_before_restore_info"))
        ElseIf IsGameRunning = False And SettingsDisableCloudSyncChkBox.Checked = True Then
            'If the game is not running and "Let GHOST Buster disable cloud save synchronization" is checked
            'Disable Uplay cloud save synchronization
            DisableCloudSync()
        ElseIf IsGameRunning = False And SettingsDisableCloudSyncChkBox.Checked = False Then
            'If the game is not running and "Let GHOST Buster disable cloud save synchronization" is not checked
            'Start the backup process
            RestoreBackup()
        End If
    End Sub

    Private Sub BackupFreqTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
        'Accept only numbers
        If (Not Char.IsNumber(e.KeyChar)) AndAlso (Not Char.IsControl(e.KeyChar)) Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub BrowseSavegamesLocBtn_Click(sender As Object, e As EventArgs) Handles BrowseSavegamesLocBtn.Click
        'Choose save games directory
        Using O As New FolderBrowserDialog
            O.ShowNewFolderButton = False
            O.Description = Localization.GetString("dialog_browse_savegames_desc")
            If SettingsNonUplayVersionChkBox.Checked = False Then
                'Select Uplay save games path if using the Uplay version of the game
                O.SelectedPath = UplayPath & "savegames"
            End If
            If O.ShowDialog = DialogResult.OK Then
                SavegamesLocTextBox.Text = O.SelectedPath
                Logger.Log("[INFO] Save games directory set to: " & O.SelectedPath)
            End If
        End Using
    End Sub

    Private Sub ExploreSavegamesLocBtn_Click(sender As Object, e As EventArgs) Handles ExploreSavegamesLocBtn.Click
        'Open the save games directory in Windows Explorer
        If SavegamesLocTextBox.Text <> "" AndAlso Directory.Exists(SavegamesLocTextBox.Text) Then
            Process.Start("explorer.exe", SavegamesLocTextBox.Text)
        Else
            Banner.Show(64, Localization.GetString("banner_savegames_folder_404_info"))
        End If
    End Sub

    Private Sub BrowseBackupLocBtn_Click(sender As Object, e As EventArgs) Handles BrowseBackupLocBtn.Click
        'Choose backup directory
        Using O As New FolderBrowserDialog
            O.Description = Localization.GetString("dialog_browse_backup_desc")
            If O.ShowDialog = DialogResult.OK Then
                BackupLocTextBox.Text = O.SelectedPath
                Logger.Log("[INFO] Backup directory set to: " & O.SelectedPath)

                'Detect latest backup timestamp
                LatestBackupHelpLabel.Text = Localization.GetString("ui_tasks_latest_loading")
                DetectLatestBackup()
            End If
        End Using
    End Sub

    Private Sub ExploreBackupLocBtn_Click(sender As Object, e As EventArgs) Handles ExploreBackupLocBtn.Click
        'Open backup directory in Windows Explorer
        If BackupLocTextBox.Text <> "" AndAlso Directory.Exists(BackupLocTextBox.Text) Then
            Process.Start("explorer.exe", BackupLocTextBox.Text)
        Else
            Banner.Show(64, Localization.GetString("banner_backup_folder_404_info"))
        End If
    End Sub
#End Region

#Region "Settings"
    Private Sub SettingsDisableCloudSyncChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles SettingsDisableCloudSyncChkBox.CheckedChanged
        If SettingsDisableCloudSyncChkBox.Checked = True Then
            SettingsEnableCloudSyncChkBox.Enabled = True
        Else
            SettingsEnableCloudSyncChkBox.Enabled = False
        End If
    End Sub

    Private Sub SettingsNonUplayVersionChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles SettingsNonUplayVersionChkBox.CheckedChanged
        If SettingsNonUplayVersionChkBox.Checked = False Then
            SettingsCustomExeTextBox.Enabled = False
            SettingsBrowseCustomExeBtn.Enabled = False
            SettingsOpenCustomExeFolderBtn.Enabled = False
        Else
            SettingsCustomExeTextBox.Enabled = True
            SettingsBrowseCustomExeBtn.Enabled = True
            SettingsOpenCustomExeFolderBtn.Enabled = True
        End If
    End Sub

    Private Sub SettingsBrowseCustomExeBtn_Click(sender As Object, e As EventArgs) Handles SettingsBrowseCustomExeBtn.Click
        'Choose Wildlands executable
        Using O As New OpenFileDialog
            O.Filter = String.Format(Localization.GetString("dialog_browse_customexe_filter"), "GRW.exe")
            O.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
            O.Title = Localization.GetString("dialog_browse_customexe_title")
            If O.ShowDialog = DialogResult.OK Then
                SettingsCustomExeTextBox.Text = O.FileName
                Logger.Log("[INFO] Custom Wildlands executable set: " & O.FileName)
            End If
        End Using
    End Sub

    Private Sub SettingsOpenCustomExeFolderBtn_Click(sender As Object, e As EventArgs) Handles SettingsOpenCustomExeFolderBtn.Click
        'Open custom Wildlands location in Windows Explorer if it exists
        If SettingsCustomExeTextBox.Text <> "" AndAlso Directory.Exists(Directory.GetParent(SettingsCustomExeTextBox.Text).ToString()) Then
            Process.Start(Directory.GetParent(SettingsCustomExeTextBox.Text).ToString())
        Else
            Banner.Show(64, Localization.GetString("banner_folder_404_info"))
        End If
    End Sub

    Private Sub SettingsWriteLogToFileChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles SettingsWriteLogToFileChkBox.CheckedChanged
        If SettingsWriteLogToFileChkBox.Checked = False Then
            SettingsLogFilePathTextBox.Enabled = False
            SettingsBrowseLogFileBtn.Enabled = False
            SettingsOpenLogBtn.Enabled = False
        Else
            SettingsLogFilePathTextBox.Enabled = True
            SettingsBrowseLogFileBtn.Enabled = True
            SettingsOpenLogBtn.Enabled = True
        End If
    End Sub

    Private Sub SettingsBrowseLogFileBtn_Click(sender As Object, e As EventArgs) Handles SettingsBrowseLogFileBtn.Click
        'Choose log file directory
        Using O As New FolderBrowserDialog
            O.Description = Localization.GetString("dialog_browse_log_destination_desc")
            If O.ShowDialog = DialogResult.OK Then
                SettingsLogFilePathTextBox.Text = O.SelectedPath & "\event.log"
                Logger.Log("[INFO] Log file path set to: " & SettingsLogFilePathTextBox.Text)
            End If
        End Using
    End Sub

    Private Sub SettingsOpenLogBtn_Click(sender As Object, e As EventArgs) Handles SettingsOpenLogBtn.Click
        'Open log file with the default text editor
        If SettingsLogFilePathTextBox.Text <> "" AndAlso File.Exists(SettingsLogFilePathTextBox.Text) Then
            Process.Start(SettingsLogFilePathTextBox.Text)
        Else
            Banner.Show(64, Localization.GetString("banner_log_file_404_info"))
        End If
    End Sub
#End Region

#Region "Logs"
    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        If LogTxtBox.SelectedText <> "" Then
            Clipboard.SetText(LogTxtBox.SelectedText)
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        LogTxtBox.SelectAll()
    End Sub

    Private Sub ExportLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportLogToolStripMenuItem.Click
        'Export log TextBox content to a text file
        Using S As New SaveFileDialog
            S.Title = Localization.GetString("dialog_save_log_title")
            S.InitialDirectory = Application.StartupPath
            S.FileName = "GHOSTbackup_" & Now.ToString("yyyyMMddHHmm")
            S.Filter = String.Format(Localization.GetString("dialog_save_log_filter"), "*.txt", "*.log")
            If S.ShowDialog = DialogResult.OK Then
                File.AppendAllText(S.FileName, LogTxtBox.Text)
                Logger.Log("[INFO] Log exported to " & S.FileName)
            End If
        End Using
    End Sub
#End Region

#Region "About"
    Private Sub WebsiteLink_Click(sender As Object, e As EventArgs) Handles WebsiteLink.Click
        Process.Start("https://strappazzon.xyz/GRW-GHOST-Buster")
    End Sub

    Private Sub SupportLink_Click(sender As Object, e As EventArgs) Handles SupportLink.Click
        Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/issues")
    End Sub

    Private Sub ChangelogLink_Click(sender As Object, e As EventArgs) Handles ChangelogLink.Click
        Process.Start("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/CHANGELOG.txt")
    End Sub

    Private Sub LicenseLink_Click(sender As Object, e As EventArgs) Handles LicenseLink.Click
        Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/blob/master/LICENSE.txt")
    End Sub
#End Region
End Class

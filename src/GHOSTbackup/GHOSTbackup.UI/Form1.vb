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
    Private Sub HelpToolTip_Draw(sender As Object, e As DrawToolTipEventArgs) Handles HelpToolTip.Draw
        'Draw tooltip with custom colors
        e.DrawBackground()
        'Don't draw the border
        'e.DrawBorder()
        e.DrawText()
    End Sub

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
        SettingsLabel.Location = New Point(HomePictureBtn.Location.X + HomePictureBtn.Width + 20, 20)
        LogLabel.Location = New Point(SettingsLabel.Location.X + SettingsLabel.Width + 20, 20)
        AlertDot.Location = New Point(LogLabel.Location.X + LogLabel.Width - 10, 22)
        AboutLabel.Location = New Point(LogLabel.Location.X + LogLabel.Width + 20, 20)
        BackupFreqTextBox.Location = New Point(BackupFreqHelp1Label.Location.X + BackupFreqHelp1Label.Width, 22)
        BackupFreqHelp2Label.Location = New Point(BackupFreqTextBox.Location.X + BackupFreqTextBox.Width, 23)
        LatestBackupHelpLabel.Location = New Point(BackupFreqHelp2Label.Location.X + BackupFreqHelp2Label.Width - 3, 23)

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
        If IsBackupRunning = True And ConfirmExitChkBox.Checked = True Then
            CustomMsgBox.Show(Localization.GetString("msgbox_confirm_exit"), Localization.GetString("msgbox_confirm_exit_title"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If CustomMsgBox.DialogResult = DialogResult.No OrElse CustomMsgBox.DialogResult = DialogResult.Cancel Then
                e.Cancel = True
            Else
                'Save settings before quitting
                Settings.Save()

                'Enable Uplay cloud save synchronization again before quitting
                If DisableCloudSyncChkBox.Checked = True AndAlso (EnableCloudSyncChkBox.Enabled = True And EnableCloudSyncChkBox.Checked = True) Then
                    EnableCloudSync()
                End If
            End If
        Else
            'Save settings before quitting
            Settings.Save()

            'Enable Uplay cloud save synchronization again before quitting
            If DisableCloudSyncChkBox.Checked = True AndAlso (EnableCloudSyncChkBox.Enabled = True And EnableCloudSyncChkBox.Checked = True) Then
                EnableCloudSync()
            End If
        End If
    End Sub

    Private Sub HomePictureBtn_Click(sender As Object, e As EventArgs) Handles HomePictureBtn.Click
        HomePictureBtn.Image = My.Resources.Home_Icon_White
        AboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        LogLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        SettingsLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        TasksTitleLabel.Visible = True
        TasksContainer.Visible = True
        FoldersTitleLabel.Visible = True
        FoldersContainer.Visible = True
        AboutContainer.Visible = False
        LogsContainer.Visible = False
        TitleLabel.Visible = False
        SettingsContainer.Visible = False
    End Sub

    Private Sub SettingsLabel_Click(sender As Object, e As EventArgs) Handles SettingsLabel.Click
        HomePictureBtn.Image = My.Resources.Home_Icon
        AboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        LogLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        SettingsLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        TasksTitleLabel.Visible = False
        TasksContainer.Visible = False
        FoldersTitleLabel.Visible = False
        FoldersContainer.Visible = False
        AboutContainer.Visible = False
        LogsContainer.Visible = False
        TitleLabel.Text = Localization.GetString("ui_title_settings")
        TitleLabel.Visible = True
        SettingsContainer.Visible = True
    End Sub

    Private Sub LogLabel_Click(sender As Object, e As EventArgs) Handles LogLabel.Click
        HomePictureBtn.Image = My.Resources.Home_Icon
        LogLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        AboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        SettingsLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        TasksTitleLabel.Visible = False
        TasksContainer.Visible = False
        FoldersTitleLabel.Visible = False
        FoldersContainer.Visible = False
        AboutContainer.Visible = False
        LogsContainer.Visible = True
        TitleLabel.Text = Localization.GetString("ui_title_logs")
        TitleLabel.Visible = True
        SettingsContainer.Visible = False
        AlertDot.Visible = False
        'Close the alert when switching to Logs tab
        Banner.CloseBanner_Click(sender, e)
        'Scroll to the last line when switching to the Logs tab
        LogTxtBox.ScrollToCaret()
    End Sub

    Private Sub AboutLabel_Click(sender As Object, e As EventArgs) Handles AboutLabel.Click
        HomePictureBtn.Image = My.Resources.Home_Icon
        LogLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        AboutLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        SettingsLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        TasksTitleLabel.Visible = False
        TasksContainer.Visible = False
        FoldersTitleLabel.Visible = False
        FoldersContainer.Visible = False
        AboutContainer.Visible = True
        LogsContainer.Visible = False
        TitleLabel.Text = Localization.GetString("ui_title_about")
        TitleLabel.Visible = True
        SettingsContainer.Visible = False
    End Sub

    Private Sub UplayPictureBtn_Click(sender As Object, e As EventArgs) Handles UplayPictureBtn.Click
        'Launch Uplay only if it's installed
        If UplayPath <> Nothing Then
            Process.Start(UplayPath & "Uplay.exe")
        Else
            Banner.Show(64, Localization.GetString("banner_uplay_not_installed"))
        End If
    End Sub

    Private Sub PlayGameBtn_Click(sender As Object, e As EventArgs) Handles PlayGameBtn.Click
        Process.Start(GamePath & "GRW.exe")
    End Sub

    Private Sub ConfirmExitChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles ConfirmExitChkBox.CheckedChanged
        If ConfirmExitChkBox.Checked = True Then
            ConfirmExitChkBox.ForeColor = Color.White
        Else
            ConfirmExitChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
        End If
    End Sub

    Private Sub ConfirmStopBackupChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles ConfirmStopBackupChkBox.CheckedChanged
        If ConfirmStopBackupChkBox.Checked = True Then
            ConfirmStopBackupChkBox.ForeColor = Color.White
        Else
            ConfirmStopBackupChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
        End If
    End Sub

    Private Sub DisplayNotificationChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles DisplayNotificationChkBox.CheckedChanged
        If DisplayNotificationChkBox.Checked = True Then
            DisplayNotificationChkBox.ForeColor = Color.White
        Else
            DisplayNotificationChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
        End If
    End Sub

    Private Sub DisableCloudSyncChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles DisableCloudSyncChkBox.CheckedChanged
        If DisableCloudSyncChkBox.Checked = True Then
            DisableCloudSyncChkBox.ForeColor = Color.White
            EnableCloudSyncChkBox.Enabled = True
        Else
            DisableCloudSyncChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
            EnableCloudSyncChkBox.Enabled = False
        End If
    End Sub

    Private Sub EnableCloudSyncChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles EnableCloudSyncChkBox.CheckedChanged
        If EnableCloudSyncChkBox.Checked = True Then
            EnableCloudSyncChkBox.ForeColor = Color.White
        Else
            EnableCloudSyncChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
        End If
    End Sub

    Private Sub CheckUpdatesChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckUpdatesChkBox.CheckedChanged
        If CheckUpdatesChkBox.Checked = True Then
            CheckUpdatesChkBox.ForeColor = Color.White
        Else
            CheckUpdatesChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
        End If
    End Sub

    Private Sub RememberFormPositionChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles RememberFormPositionChkBox.CheckedChanged
        If RememberFormPositionChkBox.Checked = True Then
            RememberFormPositionChkBox.ForeColor = Color.White
        Else
            RememberFormPositionChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
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

    Private Sub BackupBtn_Click(sender As Object, e As EventArgs) Handles BackupBtn.Click
        'Start the backup only if the value is > 0
        If BackupFreqTextBox.Text = "" OrElse BackupFreqTextBox.Text = "0" Then
            CustomMsgBox.Show(Localization.GetString("msgbox_backup_frequency_invalid"), Localization.GetString("msgbox_invalid_value_title"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
        Else
            PerformFirstBackup()
        End If
    End Sub

    Private Sub BackupFreqTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BackupFreqTextBox.KeyPress
        'Accept only numbers
        If (Not Char.IsNumber(e.KeyChar)) AndAlso (Not Char.IsControl(e.KeyChar)) Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub StopBtn_Click(sender As Object, e As EventArgs) Handles StopBtn.Click
        If ConfirmStopBackupChkBox.Checked = True Then
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
        ElseIf IsGameRunning = False And DisableCloudSyncChkBox.Checked = True Then
            'If the game is not running and "Let GHOST Buster disable cloud save synchronization" is checked
            'Disable Uplay cloud save synchronization
            DisableCloudSync()
        ElseIf IsGameRunning = False And DisableCloudSyncChkBox.Checked = False Then
            'If the game is not running and "Let GHOST Buster disable cloud save synchronization" is not checked
            'Start the backup process
            RestoreBackup()
        End If
    End Sub

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
End Class

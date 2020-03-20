﻿Imports System.IO
Imports GHOSTbackup.BackupHelper
Imports GHOSTbackup.Settings
Imports GHOSTbackup.Updater
Imports GHOSTbackup.UplayHelper
Imports GHOSTbackup.Var
Imports GHOSTbackup.WildlandsHelper

Public Class Form1
    Private Sub LoadFormPosition()
        If My.Settings.RememberFormPosition = True Then
            Dim FormLocation As Point = My.Settings.WindowLocation

            If (FormLocation.X = -1) And (FormLocation.Y = -1) Then
                Return
            End If

            Dim LocationVisible As Boolean = False
            For Each S As Screen In Screen.AllScreens
                If S.Bounds.Contains(FormLocation) Then
                    LocationVisible = True
                End If
            Next

            If Not LocationVisible Then
                Return
            End If

            StartPosition = FormStartPosition.Manual
            Location = FormLocation
        End If
    End Sub

    Private Sub HelpToolTip_Draw(sender As Object, e As DrawToolTipEventArgs) Handles HelpToolTip.Draw
        'Draw tooltip with custom colors
        e.DrawBackground()
        'Don't draw the border
        'e.DrawBorder()
        e.DrawText()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Migrate settings from the old version
        UpgradeSettings()

        'Load settings and set defaults
        LoadSettings()

        'Set window position
        LoadFormPosition()

        'Start logging session
        Logger.StartSession()

        'Get Wildlands installation directory
        GetWildlandsInstall()

        'Get Uplay installation directory
        GetUplayInstall()

        'Check if save games directory exists
        If SavegamesLocTextBox.Text <> "" AndAlso Not Directory.Exists(SavegamesLocTextBox.Text) Then
            Logger.Log("[WARNING] Wildlands save games folder " & SavegamesLocTextBox.Text & " no longer exists.")
            Banner.Show(48, "Wildlands save games folder no longer exists.")
            SavegamesLocTextBox.Text = ""
        End If

        'Check if backup directory exists
        If BackupLocTextBox.Text <> "" AndAlso Not Directory.Exists(BackupLocTextBox.Text) Then
            Logger.Log("[WARNING] Backup folder " & BackupLocTextBox.Text & " no longer exists.")
            Banner.Show(48, "Backup folder no longer exists.")
            BackupLocTextBox.Text = ""
        End If

        'Detect latest backup timestamp
        If BackupLocTextBox.Text <> "" Then
            LatestBackupHelpLabel.Text = "Latest backup: " & Environment.NewLine & "Please wait..."
            LatestBackupHelpLabel.Location = New Point(300, 14)
            DetectLatestBackup()
        End If

        'Check for updates
        CheckUpdates()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If IsBackupRunning = True And ConfirmExitChkBox.Checked = True Then
            CustomMsgBox.Show("{\rtf1 The backup process is still running. Do you want to {\b interrupt it and exit?}}", "Confirm exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If CustomMsgBox.DialogResult = DialogResult.No OrElse CustomMsgBox.DialogResult = DialogResult.Cancel Then
                e.Cancel = True
            Else
                SaveSettings()
            End If
        Else
            SaveSettings()
        End If
    End Sub

    Private Sub HomePictureBtn_Click(sender As Object, e As EventArgs) Handles HomePictureBtn.Click
        HomePictureBtn.Image = My.Resources.home_white
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
        HomePictureBtn.Image = My.Resources.home
        AboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        LogLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        SettingsLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        TasksTitleLabel.Visible = False
        TasksContainer.Visible = False
        FoldersTitleLabel.Visible = False
        FoldersContainer.Visible = False
        AboutContainer.Visible = False
        LogsContainer.Visible = False
        TitleLabel.Text = "Advanced Settings"
        TitleLabel.Visible = True
        SettingsContainer.Visible = True
    End Sub

    Private Sub LogLabel_Click(sender As Object, e As EventArgs) Handles LogLabel.Click
        HomePictureBtn.Image = My.Resources.home
        LogLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        AboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        SettingsLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        TasksTitleLabel.Visible = False
        TasksContainer.Visible = False
        FoldersTitleLabel.Visible = False
        FoldersContainer.Visible = False
        AboutContainer.Visible = False
        LogsContainer.Visible = True
        TitleLabel.Text = "Logs"
        TitleLabel.Visible = True
        SettingsContainer.Visible = False
        AlertDot.Visible = False
        'Close the alert when switching to Logs tab
        CloseAlertContainerIcon_Click(sender, e)
        'Scroll to the last line when switching to the Logs tab
        LogTxtBox.ScrollToCaret()
    End Sub

    Private Sub AboutLabel_Click(sender As Object, e As EventArgs) Handles AboutLabel.Click
        'Write application info. This is more convenient for me than using the Form Designer.
        AppInfoLabel.Text = "GHOST Buster v" & Version _
                            & Environment.NewLine & "Copyright (c) 2019 - 2020 Alberto Strappazzon" _
                            & Environment.NewLine & "This software is licensed under the MIT license." _
                            & Environment.NewLine & Environment.NewLine &
                            "This software uses assets from Tom Clancy's Ghost Recon(R) Wildlands" _
                            & Environment.NewLine & "Copyright (c) Ubisoft Entertainment. All Rights Reserved." _
                            & Environment.NewLine & Environment.NewLine &
                            "Some icons are taken from Icons8 (https://icons8.com)."

        HomePictureBtn.Image = My.Resources.home
        LogLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        AboutLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        SettingsLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        TasksTitleLabel.Visible = False
        TasksContainer.Visible = False
        FoldersTitleLabel.Visible = False
        FoldersContainer.Visible = False
        AboutContainer.Visible = True
        LogsContainer.Visible = False
        TitleLabel.Text = "About"
        TitleLabel.Visible = True
        SettingsContainer.Visible = False
    End Sub

    Private Sub UplayPictureBtn_Click(sender As Object, e As EventArgs) Handles UplayPictureBtn.Click
        'Launch Uplay only if it's installed
        If UplayPath <> Nothing Then
            Process.Start(UplayPath & "Uplay.exe")
        Else
            Banner.Show(64, "Uplay is not installed.")
        End If
    End Sub

    Private Sub CloseAlertContainerIcon_Click(sender As Object, e As EventArgs) Handles CloseAlertContainerIcon.Click
        AlertContainer.Visible = False
        LogoBigPictureBox.Location = New Point(12, 85)
        PlayGameBtn.Location = New Point(12, 150)
        ConfirmExitChkBox.Location = New Point(14, 230)
        ConfirmStopBackupChkBox.Location = New Point(14, 255)
        DisableCloudSyncChkBox.Location = New Point(14, 280)
        CheckUpdatesChkBox.Location = New Point(14, 305)
        RememberFormPositionChkBox.Location = New Point(14, 330)
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

    Private Sub DisableCloudSyncChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles DisableCloudSyncChkBox.CheckedChanged
        If DisableCloudSyncChkBox.Checked = True Then
            DisableCloudSyncChkBox.ForeColor = Color.White
        Else
            DisableCloudSyncChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
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
            O.Description = "Select Wildlands save games folder. If you don't know where it is, please consult PC Gaming Wiki."
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
            Banner.Show(64, "Wildlands save games folder no loger exists.")
        End If
    End Sub

    Private Sub BrowseBackupLocBtn_Click(sender As Object, e As EventArgs) Handles BrowseBackupLocBtn.Click
        'Choose backup directory
        Using O As New FolderBrowserDialog
            O.Description = "Select where you want to backup your save files to. Every backup will create a new ""yyyyMMdd HHmm"" subfolder."
            If O.ShowDialog = DialogResult.OK Then
                BackupLocTextBox.Text = O.SelectedPath
                Logger.Log("[INFO] Backup directory set to: " & O.SelectedPath)

                'Detect latest backup timestamp
                LatestBackupHelpLabel.Text = "Latest backup: " & Environment.NewLine & "Please wait..."
                LatestBackupHelpLabel.Location = New Point(300, 14)
                DetectLatestBackup()
            End If
        End Using
    End Sub

    Private Sub ExploreBackupLocBtn_Click(sender As Object, e As EventArgs) Handles ExploreBackupLocBtn.Click
        'Open backup directory in Windows Explorer
        If BackupLocTextBox.Text <> "" AndAlso Directory.Exists(BackupLocTextBox.Text) Then
            Process.Start("explorer.exe", BackupLocTextBox.Text)
        Else
            Banner.Show(64, "Backup folder no longer exists.")
        End If
    End Sub

    Private Sub BackupBtn_Click(sender As Object, e As EventArgs) Handles BackupBtn.Click
        PerformFirstBackup()
    End Sub

    Private Sub StopBtn_Click(sender As Object, e As EventArgs) Handles StopBtn.Click
        If ConfirmStopBackupChkBox.Checked = True Then
            CustomMsgBox.Show("{\rtf1 Are you sure you want to {\b interrupt the backup process?}}", "Backup interruption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
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
            Banner.Show(64, "You must specify both save games and backup folders.")
        ElseIf IsGameRunning = True Then
            Banner.Show(64, "You must quit Wildlands before restoring a backup.")
        ElseIf IsGameRunning = False And DisableCloudSyncChkBox.Checked = True Then
            'If the game is not running and "Let GHOST Buster disable cloud save synchronization" is checked
            'Check if Uplay is running or not before editing its settings file
            Dim UplayProc = Process.GetProcessesByName("upc")
            If UplayProc.Count > 0 Then
                CustomMsgBox.Show("{\rtf1 You must {\b quit Uplay before restoring a backup} because you chose to let GHOST Buster disable cloud save synchronization for you.}", "Cannot restore", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            Else
                'Disable Uplay cloud save synchronization
                Try
                    Dim UplayYAMLPath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Ubisoft Game Launcher\settings.yml"
                    Logger.Log("[INFO] Parsing and evaluating Uplay settings file: " & UplayYAMLPath)
                    Dim ParsedUplayYAML As String = File.ReadAllText(UplayYAMLPath)

                    If ParsedUplayYAML.Contains("syncsavegames: true") Then
                        'Backup Uplay settings file
                        Logger.Log("[INFO] Backing up Uplay settings file to " & UplayYAMLPath & ".bak")
                        File.Copy(UplayYAMLPath, UplayYAMLPath & ".bak", False) 'Don't overwrite the backup file in the future

                        'Set syncsavegames to false (Disable cloud save sync)
                        Dim ReplacedUplayYAML As String = ParsedUplayYAML.Replace("syncsavegames: true", "syncsavegames: false")
                        File.WriteAllText(UplayYAMLPath, ReplacedUplayYAML)
                        Logger.Log("[INFO] Uplay cloud save synchronization disabled.")

                        'Launch Uplay again...
                        If UplayPath <> Nothing Then
                            Process.Start(UplayPath & "Uplay.exe")
                        End If

                        '...and restore the backup
                        RestoreBackup()
                    ElseIf ParsedUplayYAML.Contains("syncsavegames: false") Then
                        'Don't replace anything if syncsavegames is already set to false
                        Logger.Log("[INFO] Uplay cloud synchronization is already disabled.")

                        'Launch Uplay again...
                        If UplayPath <> Nothing Then
                            Process.Start(UplayPath & "Uplay.exe")
                        End If

                        '...and restore the backup
                        RestoreBackup()
                    End If

                Catch ex As Exception
                    'Don't let GHOST Buster disable cloud save sync until the user enables the setting again...
                    DisableCloudSyncChkBox.Checked = False
                    '...notify the user about the error
                    Logger.Log("[ERROR] Parsing of ""settings.yml"" failed: " & ex.Message())
                    CustomMsgBox.Show("{\rtf1 ""Let GHOST Buster disable cloud save synchronization"" setting has been {\b disabled because an error occurred} while trying to parse Uplay settings file." _
                               & "\line\line Make sure to {\b DISABLE} cloud save synchronization from Uplay (Settings -> Untick ""Enable cloud save synchronization for supported games"") before launching Wildlands, otherwise the restored save games will be " _
                               & "{\b OVERWRITTEN} with the old ones from the cloud!",
                               "Parsing failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
                    '...and proceed with the restore process anyway
                    RestoreBackup()
                End Try
            End If
        ElseIf IsGameRunning = False And DisableCloudSyncChkBox.Checked = False Then
            'If the game is not running and "Let GHOST Buster disable cloud save synchronization" is not checked
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
            S.Title = "Save log as..."
            S.InitialDirectory = Application.StartupPath
            S.FileName = "GHOSTbackup_" & Now.ToString("yyyyMMddHHmm")
            S.Filter = "Text file|.txt|Log file|*.log"
            If S.ShowDialog = DialogResult.OK Then
                File.AppendAllText(S.FileName, LogTxtBox.Text)
                Logger.Log("[INFO] Log exported to " & S.FileName)
            End If
        End Using
    End Sub

    Private Sub WebsiteLabel_Click(sender As Object, e As EventArgs) Handles WebsiteLabel.Click
        Process.Start("https://strappazzon.xyz/GRW-GHOST-Buster")
    End Sub

    Private Sub SupportLabel_Click(sender As Object, e As EventArgs) Handles SupportLabel.Click
        Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/issues")
    End Sub

    Private Sub ChangelogLabel_Click(sender As Object, e As EventArgs) Handles ChangelogLabel.Click
        Process.Start("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/CHANGELOG.txt")
    End Sub

    Private Sub LicenseLabel_Click(sender As Object, e As EventArgs) Handles LicenseLabel.Click
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
            O.Description = "Select where you want to save the event log file to."
            If O.ShowDialog = DialogResult.OK Then
                SettingsLogFilePathTextBox.Text = O.SelectedPath & "\event.log"
                My.Settings.LogFilePath = SettingsLogFilePathTextBox.Text
                Logger.Log("[INFO] Log file path set to: " & SettingsLogFilePathTextBox.Text)
            End If
        End Using
    End Sub

    Private Sub SettingsOpenLogBtn_Click(sender As Object, e As EventArgs) Handles SettingsOpenLogBtn.Click
        'Open log file with the default text editor
        If SettingsLogFilePathTextBox.Text <> "" AndAlso File.Exists(SettingsLogFilePathTextBox.Text) Then
            Process.Start(SettingsLogFilePathTextBox.Text)
        Else
            Banner.Show(64, "The event log file does not exist.")
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
            O.Filter = "Wildlands executable (GRW.exe)|GRW.exe"
            O.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
            O.Title = "Select Wildlands executable"
            If O.ShowDialog = DialogResult.OK Then
                SettingsCustomExeTextBox.Text = O.FileName
                My.Settings.CustomExeLoc = SettingsCustomExeTextBox.Text
                Logger.Log("[INFO] Custom Wildlands executable set: " & O.FileName)
            End If
        End Using
    End Sub

    Private Sub SettingsOpenCustomExeFolderBtn_Click(sender As Object, e As EventArgs) Handles SettingsOpenCustomExeFolderBtn.Click
        'Open custom Wildlands location in Windows Explorer if it exists
        If SettingsCustomExeTextBox.Text <> "" AndAlso Directory.Exists(Directory.GetParent(SettingsCustomExeTextBox.Text).ToString()) Then
            Process.Start(Directory.GetParent(SettingsCustomExeTextBox.Text).ToString())
        Else
            Banner.Show(64, "The current folder does not exist.")
        End If
    End Sub
End Class

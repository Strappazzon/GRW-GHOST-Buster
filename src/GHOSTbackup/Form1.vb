Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Text
Imports Microsoft.Win32

Public Class Form1
    Public ReadOnly versionCode As Short = 11
    Public ReadOnly version As String = "1.4.2"
    Public isGameInstalled As Boolean
    Public isUplayInstalled As Boolean
    Public gamePath As String
    Public uplayPath As String
    Public isGameRunning As Boolean
    Public isBackupRunning As Boolean = False
    Public backupTimestamp As Date
    Public restoreTimestamp As Date
    Public latestBackupExists As Boolean = False
    Public secondToLastBackupExists As Boolean = False

    Sub upgradeSettings()
        'Unfortunately, settings migrate only if the new version is installed in the same directory as the old version
        '//bytes.com/topic/visual-basic-net/answers/854235-my-settings-upgrade-doesnt-upgrade#post3426232
        If My.Settings.MustUpgrade = True Then
            My.Settings.Upgrade()
            My.Settings.MustUpgrade = False
        End If
    End Sub

    Sub loadFormPosition()
        If My.Settings.RememberFormPosition = True Then
            Dim formLocation As Point = My.Settings.WindowLocation

            If (formLocation.X = -1) And (formLocation.Y = -1) Then
                Return
            End If

            Dim bLocationVisible As Boolean = False
            For Each S As Screen In Screen.AllScreens
                If S.Bounds.Contains(formLocation) Then
                    bLocationVisible = True
                End If
            Next

            If Not bLocationVisible Then
                Return
            End If

            StartPosition = FormStartPosition.Manual
            Location = formLocation
        End If
    End Sub

    Sub showAlert(alertType As Short, alertDesc As String, Optional dlButton As Boolean = False)
        If alertType = 48 Then
            'Warning
            alertIcon.Image = My.Resources.alert
            alertDot.Visible = True
        ElseIf alertType = 64 Then
            'Info
            alertIcon.Image = My.Resources.info
        End If

        'Download button used when a new version of GHOST Buster is available
        If dlButton = True Then
            dlBtnIcon.Visible = True
        Else
            dlBtnIcon.Visible = False
        End If

        'Alert message
        alertDescriptionLabel.Text = alertDesc

        logoBigPictureBox.Location = New Point(12, 115)
        playGameBtn.Location = New Point(12, 180)
        confirmExitChkBox.Location = New Point(14, 255)
        confirmStopBackupChkBox.Location = New Point(14, 280)
        updateCheckerChkBox.Location = New Point(14, 305)
        formPositionChkBox.Location = New Point(14, 330)
        alertDescriptionLabel.Location = New Point(alertContainer.Width / 2 - alertDescriptionLabel.Width / 2, alertContainer.Height / 2 - alertDescriptionLabel.Height / 2)
        alertIcon.Location = New Point(alertContainer.Width / 2 - alertDescriptionLabel.Width / 2 - 28, alertContainer.Height / 2 - alertIcon.Height / 2)
        alertContainer.Visible = True
    End Sub

    Sub log([event] As String)
        'Don't start the log file with an empty line if it's empty
        If logTxtBox.Text = "" Then
            logTxtBox.AppendText(Now.ToString("HH:mm:ss") & " " & [event])
        Else
            logTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm:ss") & " " & [event])
        End If

        If settingsWriteLogToFileChkBox.Checked = True Then
            Dim logToFile As New StringBuilder
            logToFile.AppendLine(Now.ToString("HH:mm:ss") & " " & [event])

            Try
                File.AppendAllText(settingsLogFilePathTextBox.Text, logToFile.ToString())

            Catch pathTooLong As PathTooLongException
                settingsWriteLogToFileChkBox.Checked = False
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm:ss") & " [ERROR] 'PathTooLongException', Log session to file interrupted.")
                showAlert(48, "Logging to file disabled (Path is too long).")
            Catch dirNotFound As DirectoryNotFoundException
                settingsWriteLogToFileChkBox.Checked = False
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm:ss") & " [ERROR] 'DirectoryNotFoundException', Log session to file interrupted.")
                showAlert(48, "Logging to file disabled (Directory not found).")
            Catch insufficentPermissions As UnauthorizedAccessException
                settingsWriteLogToFileChkBox.Checked = False
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm:ss") & " [ERROR] 'UnauthorizedAccessException', Log session to file interrupted.")
                showAlert(48, "Logging to file disabled (Insufficent permissions).")
            End Try
        End If
    End Sub

    Sub startBackup()
        backupTimer.Interval = freqSelectTimeUpDown.Value * 60000
        backupTimer.Start()
        isBackupRunning = True
        freqSelectTimeUpDown.Enabled = False
        backupBtn.Enabled = False
        stopBtn.Enabled = True
        restoreBtn.Enabled = False
        saveLocTextBox.Enabled = False
        browseSaveLocBtn.Enabled = False
        destLocTextBox.Enabled = False
        browseDestLocBtn.Enabled = False
    End Sub

    Sub stopBackup()
        backupTimer.Stop()
        isBackupRunning = False
        freqSelectTimeUpDown.Enabled = True
        backupBtn.Enabled = True
        stopBtn.Enabled = False
        restoreBtn.Enabled = True
        saveLocTextBox.Enabled = True
        browseSaveLocBtn.Enabled = True
        destLocTextBox.Enabled = True
        browseDestLocBtn.Enabled = True
    End Sub

    Sub restoreBackup()
        log("[INFO] Restore process started.")

        Dim saveLoc As String = saveLocTextBox.Text
        Dim backupLoc As String = destLocTextBox.Text
        Dim latestBackupLoc As String = destLocTextBox.Text & backupTimestamp.ToString("\\yyyyMMdd HHmm")
        Dim secToLastBackupLoc As String = destLocTextBox.Text & restoreTimestamp.ToString("\\yyyyMMdd HHmm")

        Try
            If settingsWhichBackupDropdownCombo.SelectedIndex = 0 And latestBackupExists = True Then
                'If "Latest" option is selected and the latest backup exists
                Dim choice As Short = MessageBox.Show("You are about to restore a backup. This will copy the save files over from your backup directory:" & Environment.NewLine & Environment.NewLine & latestBackupLoc _
                                      & Environment.NewLine & Environment.NewLine & "and will OVERWRITE the existing save files inside the game directory:" & Environment.NewLine & Environment.NewLine & saveLoc _
                                      & Environment.NewLine & Environment.NewLine & "THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?",
                                      "READ CAREFULLY",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If choice = DialogResult.Yes Then
                    Dim saveList As String() = Directory.GetFiles(latestBackupLoc, "*.save")
                    For Each F As String In saveList
                        Dim fName As String = F.Substring(latestBackupLoc.Length + 1)
                        File.Copy(Path.Combine(latestBackupLoc, fName), Path.Combine(saveLoc, fName), True)
                    Next

                    log("[INFO] Backup from " & latestBackupLoc & " restored.")
                    showAlert(64, "Backup restored successfully.")
                Else
                    log("[INFO] Restore process cancelled by the user.")
                End If
            ElseIf settingsWhichBackupDropdownCombo.SelectedIndex = 0 And latestBackupExists = False Then
                'If "Latest" option is selected and the latest backup doesn't exist
                log("[INFO] No backup found. Restore process aborted.")
                MessageBox.Show("You chose to restore the latest backup but you haven't backed up any save game yet. Backup at least once and try again.", "Backup doesn't exist", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ElseIf settingsWhichBackupDropdownCombo.SelectedIndex = 1 And secondToLastBackupExists = True Then
                'If "Second-to-last" option is selected and the second-to-last backup exists
                Dim choice As Short = MessageBox.Show("You are about to restore a backup. This will copy the save files over from your backup directory:" & Environment.NewLine & Environment.NewLine & secToLastBackupLoc _
                                      & Environment.NewLine & Environment.NewLine & "and will OVERWRITE the existing save files inside the game directory:" & Environment.NewLine & Environment.NewLine & saveLoc _
                                      & Environment.NewLine & Environment.NewLine & "THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?",
                                      "READ CAREFULLY",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If choice = DialogResult.Yes Then
                    Dim saveList As String() = Directory.GetFiles(secToLastBackupLoc, "*.save")
                    For Each F As String In saveList
                        Dim fName As String = F.Substring(secToLastBackupLoc.Length + 1)
                        File.Copy(Path.Combine(secToLastBackupLoc, fName), Path.Combine(saveLoc, fName), True)
                    Next

                    log("[INFO] Backup from " & secToLastBackupLoc & " restored.")
                    showAlert(64, "Backup restored successfully.")
                Else
                    log("[INFO] Restore process cancelled by the user.")
                End If
            ElseIf settingsWhichBackupDropdownCombo.SelectedIndex = 1 And secondToLastBackupExists = False And latestBackupExists = True Then
                'If "Second-to-last" option is selected and the second-to-last backup doesn't exist
                Dim choice As Short = MessageBox.Show("You chose to restore the second-to-last backup but it doesn't exist. Do you want to restore the latest backup instead? This will copy the save files over from your " _
                                      & "backup directory:" & Environment.NewLine & Environment.NewLine & latestBackupLoc _
                                      & Environment.NewLine & Environment.NewLine & "And will OVERWRITE the existing save files inside the game directory" & Environment.NewLine & Environment.NewLine & saveLoc _
                                      & Environment.NewLine & Environment.NewLine & "THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?",
                                      "Backup doesn't exist",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If choice = DialogResult.Yes Then
                    'This will restore the latest backup instead
                    Dim saveList As String() = Directory.GetFiles(latestBackupLoc, "*.save")
                    For Each F As String In saveList
                        Dim fName As String = F.Substring(latestBackupLoc.Length + 1)
                        File.Copy(Path.Combine(latestBackupLoc, fName), Path.Combine(saveLoc, fName), True)
                    Next

                    log("[INFO] Backup from " & latestBackupLoc & " restored.")
                    showAlert(64, "Backup restored successfully.")
                Else
                    log("[INFO] Restore process cancelled by the user.")
                End If
            ElseIf settingsWhichBackupDropdownCombo.SelectedIndex = 1 And secondToLastBackupExists = False And latestBackupExists = False Then
                'If "Second-to-last" option is selected and neither second-to-last nor latest backup exists
                MessageBox.Show("You chose to restore the second-to-last backup but neither second-to-last nor latest backup exist. Backup at least once and try again.", "Backup doesn't exist",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                log("[INFO] No backup found (secondToLast, latest). Restore process aborted.")
            ElseIf settingsWhichBackupDropdownCombo.SelectedIndex = 2 Then
                'If "Let me decide" option is selected the user will have to select a "yyyyMMdd HHmm" folder and switch back to the parent folder manually
                Dim choice As Short = MessageBox.Show("You are about to restore a backup. This will copy the save files over from your backup directory:" & Environment.NewLine & Environment.NewLine & destLocTextBox.Text _
                                      & Environment.NewLine & Environment.NewLine & "and will OVERWRITE the existing save files inside the game directory:" & Environment.NewLine & Environment.NewLine & saveLoc _
                                      & Environment.NewLine & Environment.NewLine & "THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?",
                                      "READ CAREFULLY",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If choice = DialogResult.Yes Then
                    Dim saveList As String() = Directory.GetFiles(backupLoc, "*.save")
                    For Each F As String In saveList
                        Dim fName As String = F.Substring(backupLoc.Length + 1)
                        File.Copy(Path.Combine(backupLoc, fName), Path.Combine(saveLoc, fName), True)
                    Next

                    log("[INFO] Backup from " & backupLoc & " restored.")
                    showAlert(64, "Backup restored successfully.")
                Else
                    log("[INFO] Restore process cancelled by the user.")
                End If
            End If

        Catch pathTooLong As PathTooLongException
            log("[ERROR] 'PathTooLongException', Couldn't restore the backup from " & destLocTextBox.Text & " to " & saveLocTextBox.Text)
            MessageBox.Show("The specified path cannot be handled because it's too long, as a result the restore process has been interrupted.", "Restore failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch dirNotFound As DirectoryNotFoundException
            log("[ERROR] 'DirectoryNotFoundException', Couldn't restore the backup from " & destLocTextBox.Text & " to " & saveLocTextBox.Text)
            MessageBox.Show("One or more folders no longer exist, as a result the restore process has been interrupted.", "Restore failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Migrate settings from the old version
        upgradeSettings()

        'Load settings and set defaults
        saveLocTextBox.Text = My.Settings.GameSavesDir
        destLocTextBox.Text = My.Settings.BackupDir
        freqSelectTimeUpDown.Value = My.Settings.BackupInterval
        confirmExitChkBox.Checked = My.Settings.ConfirmExit
        confirmStopBackupChkBox.Checked = My.Settings.ConfirmBackupInterruption
        updateCheckerChkBox.Checked = My.Settings.CheckUpdates
        settingsWriteLogToFileChkBox.Checked = My.Settings.WriteLogFile
        formPositionChkBox.Checked = My.Settings.RememberFormPosition
        If My.Settings.LogFilePath = "" Then
            settingsLogFilePathTextBox.Text = Application.StartupPath & "\event.log"
        Else
            settingsLogFilePathTextBox.Text = My.Settings.LogFilePath
        End If
        settingsDisableCloudSyncChkBox.Checked = My.Settings.DisableCloudSync
        settingsWhichBackupDropdownCombo.SelectedIndex = My.Settings.WhichBackup

        'Set window position
        loadFormPosition()

        'Start logging session
        log("[LOG SESSION] -------------------- START --------------------")
        log("[INFO] GHOST Buster version: " & version)

#If DEBUG Then
        log("[INFO] Environment is DEVELOPMENT")
#Else
        log("[INFO] Environment is PRODUCTION")
#End If

        'Retrieve Wildlands install directory
        Dim gameReg As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher\Installs\1771", False)

        Try
            'Replace forward slashes
            gamePath = TryCast(gameReg.GetValue("InstallDir"), String).Replace("/", "\")
            gameReg.Close()

            If gamePath <> Nothing Then
                isGameInstalled = True
                playGameBtn.Enabled = True
                log("[INFO] Wildlands is installed in: " & gamePath)
                processCheckTimer.Interval = 500
                processCheckTimer.Start()
            Else
                isGameInstalled = False
                playGameBtn.Text = "Ghost Recon Wildlands is not installed"
                log("[WARNING] Wildlands is not installed (""InstallDir"" is Null or Empty).")
            End If

        Catch nullValue As NullReferenceException
            isGameInstalled = False
            playGameBtn.Text = "Ghost Recon Wildlands is not installed"
            log("[WARNING] 'NullReferenceException' Wildlands is not installed.")
        End Try

        'Retrieve Uplay install directory
        Dim uplayReg As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher", False)

        Try
            uplayPath = uplayReg.GetValue("InstallDir")
            uplayReg.Close()

            If uplayPath <> Nothing Then
                isUplayInstalled = True
                log("[INFO] Uplay is installed in: " & uplayPath)
            Else
                isUplayInstalled = False
                log("[WARNING] Uplay is not installed (""InstallDir"" is Null or Empty). Uplay is required to launch and play Wildlands.")
            End If

        Catch nullValue As NullReferenceException
            log("[WARNING] 'NullReferenceException' Uplay is not installed. Uplay is required to launch and play Wildlands.")
        End Try

        'Check if save games directory exists
        If saveLocTextBox.Text <> "" Then
            If Not Directory.Exists(saveLocTextBox.Text) Then
                log("[WARNING] Wildlands save games folder " & saveLocTextBox.Text & " no longer exists.")
                showAlert(48, "Wildlands save games folder no longer exists.")
                saveLocTextBox.Text = ""
                My.Settings.GameSavesDir = ""
            End If
        End If

        'Check if backup directory exists
        If destLocTextBox.Text <> "" Then
            If Not Directory.Exists(destLocTextBox.Text) Then
                log("[WARNING] Backup folder " & destLocTextBox.Text & " no longer exists.")
                showAlert(48, "Backup folder no longer exists.")
                destLocTextBox.Text = ""
                My.Settings.BackupDir = ""
            End If
        End If
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Check for updates
        If updateCheckerChkBox.Checked = True Then
            Try
                Dim fetchedVer As Short

                Using updater As New WebClient
                    updater.Headers(HttpRequestHeader.UserAgent) = "GHOST Buster (+https://strappazzon.xyz/GRW-GHOST-Buster)"
                    updater.Headers(HttpRequestHeader.AcceptEncoding) = "gzip"
                    Using rs As New GZipStream(updater.OpenRead("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/version"), CompressionMode.Decompress)
                        fetchedVer = New StreamReader(rs).ReadToEnd()
                        rs.Dispose()
                    End Using
                    updater.Dispose()
                End Using

                'Compare downloaded GHOST Buster version number with the current one
                If fetchedVer = versionCode Then
                    log("[INFO] GHOST Buster is up to date.")
                ElseIf fetchedVer > versionCode Then
                    log("[INFO] New version of GHOST Buster is available.")
                    showAlert(64, "New version of GHOST Buster is available.", True)
                ElseIf fetchedVer < versionCode Then
                    log("[INFO] The version in use is greater than the one currently available.")
                End If

            Catch connectionFailed As WebException
                log("[WARNING] 'WebException' Connection failed.")
                showAlert(48, "Unable to check for updates: connection failed.")
            End Try
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If isBackupRunning = True And confirmExitChkBox.Checked = True Then
            Dim choice As Short = MessageBox.Show("The backup process is still running. Do you want to interrupt it and exit?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If choice = DialogResult.No Then
                e.Cancel = True
            End If
        End If

        'Save settings
        If confirmExitChkBox.CheckState <> My.Settings.ConfirmExit Then
            My.Settings.ConfirmExit = confirmExitChkBox.CheckState
        End If

        If confirmStopBackupChkBox.CheckState <> My.Settings.ConfirmBackupInterruption Then
            My.Settings.ConfirmBackupInterruption = confirmStopBackupChkBox.CheckState
        End If

        If freqSelectTimeUpDown.Value <> My.Settings.BackupInterval Then
            My.Settings.BackupInterval = freqSelectTimeUpDown.Value
        End If

        If formPositionChkBox.CheckState <> My.Settings.RememberFormPosition Then
            My.Settings.RememberFormPosition = formPositionChkBox.CheckState
            My.Settings.WindowLocation = Location
        End If

        If settingsWriteLogToFileChkBox.CheckState <> My.Settings.WriteLogFile Then
            My.Settings.WriteLogFile = settingsWriteLogToFileChkBox.CheckState
            My.Settings.LogFilePath = settingsLogFilePathTextBox.Text
        End If

        My.Settings.LogFilePath = settingsLogFilePathTextBox.Text

        If settingsDisableCloudSyncChkBox.CheckState <> My.Settings.DisableCloudSync Then
            My.Settings.DisableCloudSync = settingsDisableCloudSyncChkBox.CheckState
        End If

        If settingsWhichBackupDropdownCombo.SelectedIndex <> My.Settings.WhichBackup Then
            My.Settings.WhichBackup = settingsWhichBackupDropdownCombo.SelectedIndex
        End If

        log("[INFO] Settings saved.")
    End Sub

    Private Sub processCheckTimer_Tick(sender As Object, e As EventArgs) Handles processCheckTimer.Tick
        Dim wProc = Process.GetProcessesByName("GRW")
        If wProc.Count > 0 Then
            isGameRunning = True
            playGameBtn.Enabled = False
        Else
            isGameRunning = False
            playGameBtn.Enabled = True
            If isBackupRunning = True Then
                stopBackup()
                log("[WARNING] Wildlands has been closed or crashed. Backup interrupted.")
                MessageBox.Show("Wildlands has been closed or crashed, as a result the backup process has been interrupted.", "Wildlands is no longer running", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
            End If
        End If
    End Sub

    Private Sub HomePictureBtn_Click(sender As Object, e As EventArgs) Handles homePictureBtn.Click
        homePictureBtn.Image = My.Resources.home_white
        aboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        logLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        settingsLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        backupGroupBox.Visible = True
        pathsGroupBox.Visible = True
        aboutTitleLabel.Visible = False
        aboutContainer.Visible = False
        logTitleLabel.Visible = False
        logsContainer.Visible = False
        settingsTitleLabel.Visible = False
        settingsContainer.Visible = False
    End Sub

    Private Sub settingsLabel_Click(sender As Object, e As EventArgs) Handles settingsLabel.Click
        homePictureBtn.Image = My.Resources.home
        aboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        logLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        settingsLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        backupGroupBox.Visible = False
        pathsGroupBox.Visible = False
        aboutTitleLabel.Visible = False
        aboutContainer.Visible = False
        logTitleLabel.Visible = False
        logsContainer.Visible = False
        settingsTitleLabel.Visible = True
        settingsContainer.Visible = True
    End Sub

    Private Sub logLabel_Click(sender As Object, e As EventArgs) Handles logLabel.Click
        homePictureBtn.Image = My.Resources.home
        logLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        aboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        settingsLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        backupGroupBox.Visible = False
        pathsGroupBox.Visible = False
        aboutTitleLabel.Visible = False
        aboutContainer.Visible = False
        logTitleLabel.Visible = True
        logsContainer.Visible = True
        settingsTitleLabel.Visible = False
        settingsContainer.Visible = False
        alertDot.Visible = False
        closeAlertContainerIcon_Click(sender, e)
    End Sub

    Private Sub AboutLabel_Click(sender As Object, e As EventArgs) Handles aboutLabel.Click
        'Write application info. This is more convenient for me than using the Form Designer.
        appInfoLabel.Text = "GHOST Buster v" & version _
                            & Environment.NewLine & "Copyright (c) 2019 - 2020 Alberto Strappazzon" _
                            & Environment.NewLine & "This software is licensed under the MIT license." _
                            & Environment.NewLine & Environment.NewLine &
                            "This software uses assets from Tom Clancy's Ghost Recon(R) Wildlands" _
                            & Environment.NewLine & "Copyright (c) Ubisoft Entertainment. All Rights Reserved." _
                            & Environment.NewLine & Environment.NewLine &
                            "Some icons are taken from Icons8 (https://icons8.com)."

        homePictureBtn.Image = My.Resources.home
        logLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        aboutLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        settingsLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        backupGroupBox.Visible = False
        pathsGroupBox.Visible = False
        aboutContainer.Visible = True
        aboutTitleLabel.Visible = True
        logTitleLabel.Visible = False
        logsContainer.Visible = False
        settingsTitleLabel.Visible = False
        settingsContainer.Visible = False
    End Sub

    Private Sub uplayPictureBtn_Click(sender As Object, e As EventArgs) Handles uplayPictureBtn.Click
        If isUplayInstalled = True Then
            Process.Start(uplayPath & "Uplay.exe")
        Else
            showAlert(64, "Uplay is not installed.")
        End If
    End Sub

    Private Sub dlBtnIcon_Click(sender As Object, e As EventArgs) Handles dlBtnIcon.Click
        'Download button inside the alert
        Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest")
        closeAlertContainerIcon_Click(sender, e)
    End Sub

    Private Sub closeAlertContainerIcon_Click(sender As Object, e As EventArgs) Handles closeAlertContainerIcon.Click
        alertContainer.Visible = False
        logoBigPictureBox.Location = New Point(12, 85)
        playGameBtn.Location = New Point(12, 150)
        confirmExitChkBox.Location = New Point(14, 230)
        confirmStopBackupChkBox.Location = New Point(14, 255)
        updateCheckerChkBox.Location = New Point(14, 280)
        formPositionChkBox.Location = New Point(14, 305)
    End Sub

    Private Sub playGameBtn_Click(sender As Object, e As EventArgs) Handles playGameBtn.Click
        Process.Start(gamePath & "GRW.exe")
    End Sub

    Private Sub confirmExitChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles confirmExitChkBox.CheckedChanged
        If confirmExitChkBox.Checked = True Then
            confirmExitChkBox.ForeColor = Color.White
        Else
            confirmExitChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
        End If
    End Sub

    Private Sub confirmStopBackupChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles confirmStopBackupChkBox.CheckedChanged
        If confirmStopBackupChkBox.Checked = True Then
            confirmStopBackupChkBox.ForeColor = Color.White
        Else
            confirmStopBackupChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
        End If
    End Sub

    Private Sub updateCheckerChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles updateCheckerChkBox.CheckedChanged
        If updateCheckerChkBox.Checked = True Then
            updateCheckerChkBox.ForeColor = Color.White
            My.Settings.CheckUpdates = True
        Else
            updateCheckerChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
            My.Settings.CheckUpdates = False
        End If
    End Sub

    Private Sub formPositionChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles formPositionChkBox.CheckedChanged
        If formPositionChkBox.Checked = True Then
            formPositionChkBox.ForeColor = Color.White
        Else
            formPositionChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
        End If
    End Sub

    Private Sub browseSaveLocBtn_Click(sender As Object, e As EventArgs) Handles browseSaveLocBtn.Click
        'Choose save games directory
        Using O As New FolderBrowserDialog
            O.ShowNewFolderButton = False
            O.Description = "Select the Wildlands save games folder." & Environment.NewLine & "Uplay Game ID: 1771"
            'Default Uplay install directory
            O.SelectedPath = "C:\Program Files (x86)\Ubisoft\Ubisoft Game Launcher\savegames"
            If O.ShowDialog = DialogResult.OK Then
                saveLocTextBox.Text = O.SelectedPath
                My.Settings.GameSavesDir = saveLocTextBox.Text
                log("[INFO] Save games directory set to: " & O.SelectedPath)
                O.Dispose()
            End If
        End Using
    End Sub

    Private Sub ExploreSaveLocBtn_Click(sender As Object, e As EventArgs) Handles exploreSaveLocBtn.Click
        If saveLocTextBox.Text <> "" Then
            Process.Start("explorer.exe", saveLocTextBox.Text)
        End If
    End Sub

    Private Sub browseDestLocBtn_Click(sender As Object, e As EventArgs) Handles browseDestLocBtn.Click
        'Choose backup directory
        Using O As New FolderBrowserDialog
            O.Description = "Select where you want to backup your save files to. Every backup will create a new ""yyyyMMdd HHmm"" subfolder."
            If O.ShowDialog = DialogResult.OK Then
                destLocTextBox.Text = O.SelectedPath
                My.Settings.BackupDir = destLocTextBox.Text
                log("[INFO] Backup directory set to: " & O.SelectedPath)
                O.Dispose()
            End If
        End Using
    End Sub

    Private Sub ExploreDestLocBtn_Click(sender As Object, e As EventArgs) Handles exploreDestLocBtn.Click
        'Open backup directory in Windows Explorer
        If destLocTextBox.Text <> "" Then
            Process.Start("explorer.exe", destLocTextBox.Text)
        End If
    End Sub

    Private Sub backupBtn_Click(sender As Object, e As EventArgs) Handles backupBtn.Click
        If saveLocTextBox.Text = "" Or destLocTextBox.Text = "" Then
            showAlert(64, "You need to specify both save games and backup folders.")
        ElseIf isGameRunning = True Then 'Perform the first backup
            startBackup()

            'Store timestamp of this backup
            backupTimestamp = Now
            latestBackupExists = True

            'Write the timestamp of this backup on the main screen
            'And move it to the left so it won't be cut off
            latestBackupTimestampLabel.Text = backupTimestamp.ToString("MM/dd/yyyy hh:mm tt")
            latestBackupHelpLabel.Location = New Point(269, 23)
            latestBackupTimestampLabel.Location = New Point(357, 23)

            Dim saveLoc As String = saveLocTextBox.Text
            Dim destLoc As String = destLocTextBox.Text & backupTimestamp.ToString("\\yyyyMMdd HHmm")

            Try
                Dim saveList As String() = Directory.GetFiles(saveLoc, "*.save")

                For Each F As String In saveList
                    If Not Directory.Exists(destLoc) Then
                        Directory.CreateDirectory(destLoc)
                    End If
                    Dim fName As String = F.Substring(saveLoc.Length + 1)
                    File.Copy(Path.Combine(saveLoc, fName), Path.Combine(destLoc, fName), True)
                Next

                log("[INFO] Performed the first backup.")

            Catch pathTooLong As PathTooLongException
                stopBackup()
                log("[ERROR] 'PathTooLongException', Backup interrupted.")
                MessageBox.Show("The specified path cannot be handled because it's too long, as a result the backup process has been interrupted.", "Backup Interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch dirNotFound As DirectoryNotFoundException
                stopBackup()
                log("[ERROR] 'DirectoryNotFoundException', Backup interrupted.")
                MessageBox.Show("The specified folder no longer exists, as a result the backup process has been interrupted.", "Backup interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        ElseIf isGameRunning = False Then
            showAlert(64, "You need to launch Wildlands before starting the backup process.")
        End If
    End Sub

    Private Sub backupTimer_Tick(sender As Object, e As EventArgs) Handles backupTimer.Tick
        If isGameRunning = True Then
            'Store timestamp of this backup
            backupTimestamp = Now
            'Store the second-to-last backup timestamp
            '//stackoverflow.com/a/20849720
            restoreTimestamp = backupTimestamp.Subtract(TimeSpan.FromMinutes(freqSelectTimeUpDown.Value))
            secondToLastBackupExists = True

            'Write the timestamp of this backup on the main screen
            latestBackupTimestampLabel.Text = backupTimestamp.ToString("MM/dd/yyyy hh:mm tt")

            Dim saveLoc As String = saveLocTextBox.Text
            Dim destLoc As String = destLocTextBox.Text & backupTimestamp.ToString("\\yyyyMMdd HHmm")

            Try
                Dim saveList As String() = Directory.GetFiles(saveLoc, "*.save")

                For Each F As String In saveList
                    If Not Directory.Exists(destLoc) Then
                        Directory.CreateDirectory(destLoc)
                    End If
                    Dim fName As String = F.Substring(saveLoc.Length + 1)
                    File.Copy(Path.Combine(saveLoc, fName), Path.Combine(destLoc, fName), True)
                Next

                log("[INFO] Backup complete.")

            Catch pathTooLong As PathTooLongException
                stopBackup()
                log("[ERROR] 'PathTooLongException', Backup interrupted.")
                MessageBox.Show("The specified path cannot be handled because it's too long, as a result the backup process has been interrupted.", "Backup Interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch dirNotFound As DirectoryNotFoundException
                stopBackup()
                log("[ERROR] 'DirectoryNotFoundException', Backup interrupted.")
                MessageBox.Show("The specified folder no longer exists, as a result the backup process has been interrupted.", "Backup interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            stopBackup()
            log("[WARNING] Wildlands closed or crashed, Backup interrupted.")
            MessageBox.Show("Wildlands has been closed or crashed, as a result the backup process has been interrupted.", "Wildlands is no longer running", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub stopBtn_Click(sender As Object, e As EventArgs) Handles stopBtn.Click
        If confirmStopBackupChkBox.Checked = True Then
            Dim choice As Short = MessageBox.Show("Are you sure you want to interrupt the backup process?", "Confirm backup interruption", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If choice = DialogResult.Yes Then
                stopBackup()
                log("[INFO] Backup interrupted by the user.")
            End If
        Else
            stopBackup()
            log("[INFO] Backup interrupted by the user.")
        End If
    End Sub

    Private Sub restoreBtn_Click(sender As Object, e As EventArgs) Handles restoreBtn.Click
        If saveLocTextBox.Text = "" Or destLocTextBox.Text = "" Then
            showAlert(64, "You need to specify both save games and backup folders.")
        ElseIf isGameRunning = True Then
            showAlert(64, "You need to quit Wildlands before restoring a backup.")
        ElseIf isGameRunning = False And settingsDisableCloudSyncChkBox.Checked = True Then
            'Check if Uplay is running or not before editing its settings file
            Dim uProc = Process.GetProcessesByName("upc")
            If uProc.Count > 0 Then
                MessageBox.Show("You need to quit Uplay before restoring a backup because you chose to let GHOST Buster disable cloud save synchronization for you.", "Cannot restore", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                'Disable Uplay cloud save synchronization
                Try
                    Dim pathToYAML As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Ubisoft Game Launcher\settings.yml"
                    log("[INFO] Parsing and evaluating Uplay settings file: " & pathToYAML)
                    Dim parsedYAML As String = File.ReadAllText(pathToYAML)

                    If parsedYAML.Contains("syncsavegames: true") Then
                        'Backup Uplay settings file
                        log("[INFO] Backing up Uplay settings file to " & pathToYAML & ".bak")
                        File.Copy(pathToYAML, pathToYAML & ".bak", True)

                        'Set syncsavegames to false (Disable cloud save sync)
                        Dim replacedYAML As String = parsedYAML.Replace("syncsavegames: true", "syncsavegames: false")
                        File.WriteAllText(pathToYAML, replacedYAML)
                        log("[INFO] Uplay cloud save synchronization disabled")

                        'Launch Uplay again...
                        If isUplayInstalled = True Then
                            Process.Start(uplayPath & "Uplay.exe")
                        End If

                        '...and restore the backup
                        restoreBackup()
                    ElseIf parsedYAML.Contains("syncsavegames: false") Then
                        'Don't replace anything if syncsavegames is already set to false
                        log("[INFO] Uplay cloud synchronization is already disabled")

                        'Launch Uplay again...
                        If isUplayInstalled = True Then
                            Process.Start(uplayPath & "Uplay.exe")
                        End If

                        '...and restore the backup
                        restoreBackup()
                    End If

                Catch fileNotFound As FileNotFoundException
                    'Don't let GHOST Buster disable cloud save sync until the user enables the setting again...
                    settingsDisableCloudSyncChkBox.Checked = False
                    '...notify the user about the error
                    log("[ERROR] Parsing of ""settings.yml"" failed: File not found.")
                    MessageBox.Show("""Let GHOST Buster disable cloud save synchronization"" setting has been disabled because an error occurred while trying to parse Uplay settings file: File not found." _
                                    & Environment.NewLine & Environment.NewLine & "Make sure to DISABLE cloud save synchronization from Uplay (Settings -> Untick ""Enable cloud save synchronization for supported games"") " _
                                    & "before launching Wildlands, otherwise the restored save games will be OVERWRITTEN with the old ones from the cloud!",
                                    "Parsing failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '...and proceed with the restore process anyway
                    restoreBackup()
                Catch insufficentPermissions As UnauthorizedAccessException
                    'Don't let GHOST Buster disable cloud save sync until the user enables the setting again...
                    settingsDisableCloudSyncChkBox.Checked = False
                    '...notify the user about the error
                    log("[ERROR] Parsing of ""settings.yml"" failed: File is read only.")
                    MessageBox.Show("""Let GHOST Buster disable cloud save synchronization"" setting has been disabled because an error occurred while trying to parse Uplay settings file: File is read only." _
                                    & Environment.NewLine & Environment.NewLine & "Make sure to DISABLE cloud save synchronization from Uplay (Settings -> Untick ""Enable cloud save synchronization for supported games"") " _
                                    & "before launching Wildlands, otherwise the restored save games will be OVERWRITTEN with the old ones from the cloud!",
                                    "Parsing failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '...and proceed with the restore process anyway
                    restoreBackup()
                End Try
            End If
        ElseIf isGameRunning = False And settingsDisableCloudSyncChkBox.Checked = False Then
            restoreBackup()
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        If logTxtBox.SelectedText <> "" Then
            Clipboard.SetText(logTxtBox.SelectedText)
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        logTxtBox.SelectAll()
    End Sub

    Private Sub ExportLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportLogToolStripMenuItem.Click
        'Export log TextBox content to a text file
        Using S As New SaveFileDialog
            S.Title = "Save log as..."
            S.InitialDirectory = Application.StartupPath
            S.FileName = "GHOSTbackup_" & Now.ToString("yyyyMMddHHmm")
            S.Filter = "Text file|.txt|Log file|*.log"
            If S.ShowDialog = DialogResult.OK Then
                File.AppendAllText(S.FileName, logTxtBox.Text)
                log("[INFO] Log exported to " & S.FileName)
                S.Dispose()
            End If
        End Using
    End Sub

    Private Sub WebsiteLabel_Click(sender As Object, e As EventArgs) Handles websiteLabel.Click
        Process.Start("https://strappazzon.xyz/GRW-GHOST-Buster")
    End Sub

    Private Sub SupportLabel_Click(sender As Object, e As EventArgs) Handles supportLabel.Click
        Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/issues")
    End Sub

    Private Sub ChangelogLabel_Click(sender As Object, e As EventArgs) Handles changelogLabel.Click
        Process.Start("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/CHANGELOG.txt")
    End Sub

    Private Sub LicenseLabel_Click(sender As Object, e As EventArgs) Handles licenseLabel.Click
        Process.Start("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/LICENSE.txt")
    End Sub

    Private Sub settingsWriteLogToFileChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles settingsWriteLogToFileChkBox.CheckedChanged
        If settingsWriteLogToFileChkBox.Checked = False Then
            settingsLogFilePathTextBox.Enabled = False
            settingsBrowseLogFileBtn.Enabled = False
            settingsBrowseLogFolderBtn.Enabled = False
        Else
            settingsLogFilePathTextBox.Enabled = True
            settingsBrowseLogFileBtn.Enabled = True
            settingsBrowseLogFolderBtn.Enabled = True
        End If
    End Sub

    Private Sub settingsBrowseLogFileBtn_Click(sender As Object, e As EventArgs) Handles settingsBrowseLogFileBtn.Click
        'Choose log file directory
        Using O As New FolderBrowserDialog
            O.Description = "Select where you want to save the event log file to."
            If O.ShowDialog = DialogResult.OK Then
                settingsLogFilePathTextBox.Text = O.SelectedPath & "\event.log"
                My.Settings.LogFilePath = settingsLogFilePathTextBox.Text
                log("[INFO] Log file directory set to: " & O.SelectedPath)
                O.Dispose()
            End If
        End Using
    End Sub

    Private Sub settingsBrowseLogFolderBtn_Click(sender As Object, e As EventArgs) Handles settingsBrowseLogFolderBtn.Click
        'Open log file with the default text editor
        If settingsLogFilePathTextBox.Text <> "" And File.Exists(settingsLogFilePathTextBox.Text) Then
            Process.Start(settingsLogFilePathTextBox.Text)
        Else
            showAlert(64, "The event log file does not exist.")
        End If
    End Sub
End Class

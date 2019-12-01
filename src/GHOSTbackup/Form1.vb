Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports Microsoft.Win32

Public Class Form1
    Public ReadOnly versionCode As Integer = 8
    Public ReadOnly version As String = "1.3.4"
    Public isGameInstalled As Boolean
    Public gamePath As String
    Public uplayPath As String
    Public isGameRunning As Boolean
    Public isBackupRunning As Boolean = False

    Function loadFormPosition()
        If My.Settings.RememberFormPosition = True Then
            Dim formLocation As Point = My.Settings.WindowLocation

            If (formLocation.X = -1) And (formLocation.Y = -1) Then
                Return Nothing
            End If

            Dim bLocationVisible As Boolean = False
            For Each S As Screen In Screen.AllScreens
                If S.Bounds.Contains(formLocation) Then
                    bLocationVisible = True
                End If
            Next

            If Not bLocationVisible Then
                Return Nothing
            End If

            StartPosition = FormStartPosition.Manual
            Location = formLocation
        End If
    End Function

    Function showAlert(alertType As Integer, alertDesc As String, Optional dlButton As Boolean = False)
        If alertType = 48 Then
            alertIcon.Image = My.Resources.alert
            alertDot.Visible = True
        ElseIf alertType = 64 Then
            alertIcon.Image = My.Resources.info
        End If

        If dlButton = True Then
            dlBtnIcon.Visible = True
        Else
            dlBtnIcon.Visible = False
        End If

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
    End Function

    Function startBackup()
        backupTimer.Interval = freqSelectTimeUpDown.Value * 60000
        'backupTimer.Interval = 3000 'Debug
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
    End Function

    Function stopBackup()
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
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set window position
        formPositionChkBox.Checked = My.Settings.RememberFormPosition
        loadFormPosition()

        logTxtBox.AppendText("Init: Logging started: " & Now.ToString("MM/dd/yyyy HH:mm:ss"))
        logTxtBox.AppendText(Environment.NewLine & "Init: Version: " & version)

        'Get Uplay install dir
        Dim uplayReg As RegistryKey
        uplayReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher", False)

        Try
            uplayPath = uplayReg.GetValue("InstallDir")
            logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Uplay is installed in: " & uplayPath)
            uplayReg.Close()

        Catch nullValue As NullReferenceException
            logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " WARNING: 'NullReferenceException' Uplay appears to not be installed. Uplay is required to launch and play Wildlands.")
        End Try

        'Check if Wildlands is installed
        Dim gameReg As RegistryKey
        gameReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher\Installs\1771", False)

        Try
            gamePath = gameReg.GetValue("InstallDir")
            isGameInstalled = True
            playGameBtn.Enabled = True
            playGameBtn.Text = "Play Ghost Recon Wildlands"
            logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Wildlands is installed in: " & gamePath)
            gameReg.Close()

        Catch nullValue As NullReferenceException
            isGameInstalled = False
            playGameBtn.Text = "Ghost Recon Wildlands is not installed"
            logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " WARNING: 'NullReferenceException' Wildlands appears to not be installed.")
        End Try

        saveLocTextBox.Text = My.Settings.GameSavesDir
        destLocTextBox.Text = My.Settings.BackupDir
        freqSelectTimeUpDown.Value = My.Settings.BackupInterval
        confirmExitChkBox.Checked = My.Settings.ConfirmExit
        confirmStopBackupChkBox.Checked = My.Settings.ConfirmBackupInterruption
        updateCheckerChkBox.Checked = My.Settings.CheckUpdates
        'WriteLogToFileToolStripMenuItem.Checked = My.Settings.WriteLogFile
        If gamePath <> Nothing Then
            processCheckTimer.Interval = 500
            processCheckTimer.Start()
        End If
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If saveLocTextBox.Text <> "" Then
            If Not Directory.Exists(saveLocTextBox.Text) Then
                saveLocTextBox.Text = ""
                My.Settings.GameSavesDir = ""
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " WARNING: Wildlands save games folder no longer exists.")
                showAlert(48, "Wildlands save games folder no longer exists.")
            End If
        End If

        If destLocTextBox.Text <> "" Then
            If Not Directory.Exists(destLocTextBox.Text) Then
                destLocTextBox.Text = ""
                My.Settings.BackupDir = ""
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " WARNING: The backup folder no longer exists.")
                showAlert(48, "The backup folder no longer exists.")
            End If
        End If

        'Check for updates
        If updateCheckerChkBox.Checked = True Then
            Dim versionUri As New Uri("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/version")
            Dim fetchedVer As String

            Try
                Using updater As New WebClient
                    updater.Headers(HttpRequestHeader.AcceptEncoding) = "gzip"
                    Using rs As New GZipStream(updater.OpenRead(versionUri), CompressionMode.Decompress)
                        fetchedVer = New StreamReader(rs).ReadToEnd()
                        rs.Dispose()
                    End Using
                    updater.Dispose()
                End Using

                If fetchedVer = versionCode Then
                    logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: GHOST Buster is up to date.")
                ElseIf fetchedVer > versionCode Then
                    logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: New version of GHOST Buster is available.")
                    showAlert(64, "New version of GHOST Buster is available.", True)
                ElseIf fetchedVer < versionCode Then
                    logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: The version in use is greater than the one currently available.")
                End If

            Catch connectionFailed As WebException
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " WARNING: 'WebException' Connection failed.")
                showAlert(48, "Unable to check for updates. Connection failed.")
            End Try
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If stopBtn.Enabled = True And confirmExitChkBox.Checked = True Then
            Dim choice As Integer = MessageBox.Show("The backup process is still running." & Environment.NewLine & "Do you want to interrupt it and close the program?",
                                        "Confirm exit",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button2)
            If choice = DialogResult.No Then
                e.Cancel = True
            Else
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup interrupted by the user.")
                Application.Exit()
            End If
        End If

        If confirmExitChkBox.CheckState <> My.Settings.ConfirmExit Then
            My.Settings.ConfirmExit = confirmExitChkBox.CheckState
        End If

        If confirmStopBackupChkBox.CheckState <> My.Settings.ConfirmBackupInterruption Then
            My.Settings.ConfirmBackupInterruption = confirmStopBackupChkBox.CheckState
        End If

        If formPositionChkBox.CheckState <> My.Settings.RememberFormPosition Then
            My.Settings.RememberFormPosition = formPositionChkBox.CheckState
            My.Settings.WindowLocation = Location
        End If

        If freqSelectTimeUpDown.Value <> My.Settings.BackupInterval Then
            My.Settings.BackupInterval = freqSelectTimeUpDown.Value
            logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup interval saved. New interval: " & freqSelectTimeUpDown.Value & " minutes.")
        End If
    End Sub

    Private Sub processCheckTimer_Tick(sender As Object, e As EventArgs) Handles processCheckTimer.Tick
        For Each p As Process In Process.GetProcesses
            If p.ProcessName = "GRW" Then
                isGameRunning = True
                playGameBtn.Enabled = False
                Exit For
            Else
                isGameRunning = False
                playGameBtn.Enabled = True
            End If
        Next
    End Sub

    Private Sub HomePictureBtn_Click(sender As Object, e As EventArgs) Handles homePictureBtn.Click
        aboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        logLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        homePictureBtn.Image = My.Resources.home_white
        backupGroupBox.Visible = True
        pathsGroupBox.Visible = True
        aboutTitleLabel.Visible = False
        aboutContainer.Visible = False
        logTitleLabel.Visible = False
        logsContainer.Visible = False
    End Sub

    Private Sub UplayLabel_Click(sender As Object, e As EventArgs) Handles uplayLabel.Click
        If uplayPath <> Nothing Then
            Process.Start(uplayPath + "Uplay.exe")
        Else
            showAlert(64, "Uplay appears to not be installed.")
        End If
    End Sub

    Private Sub logLabel_Click(sender As Object, e As EventArgs) Handles logLabel.Click
        homePictureBtn.Image = My.Resources.home
        logLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        aboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        backupGroupBox.Visible = False
        pathsGroupBox.Visible = False
        aboutTitleLabel.Visible = False
        aboutContainer.Visible = False
        logTitleLabel.Visible = True
        logsContainer.Visible = True
        alertDot.Visible = False
        closeAlertContainerIcon_Click(sender, e)
    End Sub

    Private Sub AboutLabel_Click(sender As Object, e As EventArgs) Handles aboutLabel.Click
        appInfoLabel.Text = Me.Text & " v" & version _
            & Environment.NewLine & "Copyright (c) 2019 Alberto Strappazzon" _
            & Environment.NewLine & "This software is licensed under the MIT license." _
            & Environment.NewLine & Environment.NewLine &
            "This software uses assets from Ghost Recon(R) Wildlands" _
            & Environment.NewLine & "Copyright (c) Ubisoft Entertainment. All Rights Reserved." _
            & Environment.NewLine & Environment.NewLine &
            "Some icons are taken from Icons8 (https://icons8.com)."

        homePictureBtn.Image = My.Resources.home
        logLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        aboutLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        backupGroupBox.Visible = False
        pathsGroupBox.Visible = False
        aboutContainer.Visible = True
        aboutTitleLabel.Visible = True
        logTitleLabel.Visible = False
        logsContainer.Visible = False
    End Sub

    Private Sub dlBtnIcon_Click(sender As Object, e As EventArgs) Handles dlBtnIcon.Click
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
        Using O As New FolderBrowserDialog
            O.ShowNewFolderButton = False
            O.Description = "Select the location of Wildlands save games folder." & Environment.NewLine & "Uplay Game ID: 1771" ' & Environment.NewLine & "Steam App ID: 460930"
            O.SelectedPath = "C:\Program Files (x86)\Ubisoft\Ubisoft Game Launcher\savegames"
            If O.ShowDialog = Windows.Forms.DialogResult.OK Then
                saveLocTextBox.Text = O.SelectedPath
                My.Settings.GameSavesDir = saveLocTextBox.Text
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Savegames location set to: " & O.SelectedPath)
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
        Using O As New FolderBrowserDialog
            O.Description = "Select where you want to backup your save files to. Every backup will create a new ""yyyMMdd HHmm"" subfolder."
            If O.ShowDialog = Windows.Forms.DialogResult.OK Then
                destLocTextBox.Text = O.SelectedPath
                My.Settings.BackupDir = destLocTextBox.Text
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup location set to: " & O.SelectedPath)
                O.Dispose()
            End If
        End Using
    End Sub

    Private Sub ExploreDestLocBtn_Click(sender As Object, e As EventArgs) Handles exploreDestLocBtn.Click
        If destLocTextBox.Text <> "" Then
            Process.Start("explorer.exe", destLocTextBox.Text)
        End If
    End Sub

    Private Sub backupBtn_Click(sender As Object, e As EventArgs) Handles backupBtn.Click
        If saveLocTextBox.Text = "" Or destLocTextBox.Text = "" Then
            showAlert(64, "The working directories cannot be empty.")
        ElseIf isGameRunning = True Then
            startBackup()

            'Perform the first backup
            Dim saveLoc As String = saveLocTextBox.Text
            Dim destLoc As String = destLocTextBox.Text & "\" & Now.ToString("yyyyMMdd HHmm")

            Try
                Dim saveList As String() = Directory.GetFiles(saveLoc, "*.save")
                For Each f As String In saveList
                    Dim fName As String = f.Substring(saveLoc.Length + 1)
                    If Not Directory.Exists(destLoc) Then
                        Directory.CreateDirectory(destLoc)
                    End If
                    File.Copy(Path.Combine(saveLoc, fName), Path.Combine(destLoc, fName), True)
                Next

                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Performed the first backup.")

            Catch pathTooLong As PathTooLongException
                stopBackup()
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " ERROR: 'PathTooLongException', Backup interrupted.")
                MessageBox.Show("The path you specified cannot be handled because it is too long, as a result the backup process has been interrupted.", "Backup Interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Catch dirNotFound As DirectoryNotFoundException
                stopBackup()
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " ERROR: 'DirectoryNotFoundException', Backup interrupted.")
                MessageBox.Show("The specified directory no longer exists, as a result the backup process has been interrupted.", "Backup interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        ElseIf isGameRunning = False Then
            MessageBox.Show("You need to launch Wildlands before starting the backup process.", "Wildlands is not running", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If
    End Sub

    Private Sub backupTimer_Tick(sender As Object, e As EventArgs) Handles backupTimer.Tick
        If isGameRunning = True Then
            Dim saveLoc As String = saveLocTextBox.Text
            Dim destLoc As String = destLocTextBox.Text & "\" & Now.ToString("yyyyMMdd HHmm")

            Try
                Dim saveList As String() = Directory.GetFiles(saveLoc, "*.save")
                For Each f As String In saveList
                    Dim fName As String = f.Substring(saveLoc.Length + 1)
                    If Not Directory.Exists(destLoc) Then
                        Directory.CreateDirectory(destLoc)
                    End If
                    File.Copy(Path.Combine(saveLoc, fName), Path.Combine(destLoc, fName), True)
                Next

                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup complete.")

            Catch pathTooLong As PathTooLongException
                stopBackup()
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " ERROR: 'PathTooLongException', Backup interrupted.")
                MessageBox.Show("The path you specified cannot be handled because it is too long, as a result the backup process has been interrupted.", "Backup interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Catch dirNotFound As DirectoryNotFoundException
                stopBackup()
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " ERROR: 'DirectoryNotFoundException', Backup interrupted.")
                MessageBox.Show("The directory no longer exists, as a result the backup process has been interrupted.", "Backup interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            stopBackup()
            logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " WARNING: Wildlands closed or crashed, Backup interrupted.")
            MessageBox.Show("Wildlands has been closed or crashed, as a result the backup process has been interrupted.", "Wildlands is not running", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub stopBtn_Click(sender As Object, e As EventArgs) Handles stopBtn.Click
        If confirmStopBackupChkBox.Checked = True Then
            Dim choice As Integer = MessageBox.Show("Are you sure you want to interrupt the backup process?", "Confirm backup interruption", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If choice = DialogResult.Yes Then
                stopBackup()
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup interrupted by the user.")
            End If
        Else
            stopBackup()
            logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup interrupted by the user.")
        End If
    End Sub

    Private Sub restoreBtn_Click(sender As Object, e As EventArgs) Handles restoreBtn.Click
        If saveLocTextBox.Text = "" Or destLocTextBox.Text = "" Then
            showAlert(64, "The working directories cannot be empty.")
        ElseIf isGameRunning = True Then
            MessageBox.Show("You need to quit Wildlands before restoring the save games.", "Cannot restore", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Dim choice As Integer = MessageBox.Show("Restoring a backup WILL OVERWRITE the current save files." _
                                        & Environment.NewLine & Environment.NewLine &
                                        "This will copy the save files over from your backup directory:" _
                                        & Environment.NewLine & Environment.NewLine & destLocTextBox.Text & Environment.NewLine & Environment.NewLine &
                                        "and will OVERWRITE the existing save files inside the game directory:" _
                                        & Environment.NewLine & Environment.NewLine & saveLocTextBox.Text & Environment.NewLine & Environment.NewLine &
                                        "Make sure to disable cloud sync from Uplay (Settings -> Uncheck ""Enable cloud synchronization for supported games"") otherwise the restored save files will be replaced with the" _
                                        & "ones from the cloud as soon as you launch Wildlands!" _
                                        & Environment.NewLine & Environment.NewLine &
                                        "THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?",
                                        "READ CAREFULLY",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning,
                                        MessageBoxDefaultButton.Button2)
            If choice = DialogResult.Yes Then
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Restore process started.")

                Dim gameLoc As String = saveLocTextBox.Text
                Dim backupLoc As String = destLocTextBox.Text

                Try
                    Dim saveList As String() = Directory.GetFiles(backupLoc, "*.save")
                    For Each f As String In saveList
                        Dim fName As String = f.Substring(backupLoc.Length + 1)
                        File.Copy(Path.Combine(backupLoc, fName), Path.Combine(gameLoc, fName), True)
                    Next

                    logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup from " & destLocTextBox.Text & " restored.")
                    MessageBox.Show("The save files have been restored successfully." & Environment.NewLine & "Please select the backup folder again, without the ""yyyyMMdd HHMM"" folder, by clicking the ""Browse..."" button.",
                        "Restore succeeded",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk)

                Catch pathTooLong As PathTooLongException
                    logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " ERROR: 'PathTooLongException', Couldn't restore the backup from " & destLocTextBox.Text & " to " & saveLocTextBox.Text)
                    MessageBox.Show("The path you specified cannot be handled because it is too long, as a result the restore process has been interrupted.", "Restore failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Catch dirNotFound As DirectoryNotFoundException
                    logTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm") & " ERROR: 'DirectoryNotFoundException', Couldn't restore the backup from " & destLocTextBox.Text & " to " & saveLocTextBox.Text)
                    MessageBox.Show("One or more directories no longer exist, as a result the restore process has been interrupted.", "Restore failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
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

    Private Sub WriteLogToFileToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles WriteLogToFileToolStripMenuItem.CheckedChanged

    End Sub

    Private Sub ExportLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportLogToolStripMenuItem.Click
        Using S As New SaveFileDialog
            S.Title = "Save log as..."
            S.InitialDirectory = Application.StartupPath
            S.FileName = "GHOSTbackup_" + Now.ToString("yyyyMMddHHmmss")
            S.Filter = "Text file|.txt|Log file|*.log"
            If S.ShowDialog = DialogResult.OK Then
                My.Computer.FileSystem.WriteAllText(S.FileName.ToString(), logTxtBox.Text, False)
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Log exported as " + S.FileName)
                S.Dispose()
            End If
        End Using
    End Sub

    Private Sub WebsiteLabel_Click(sender As Object, e As EventArgs) Handles websiteLabel.Click
        Process.Start("https://strappazzon.github.io/GRW-GHOST-Buster")
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
End Class

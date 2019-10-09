Imports System.IO
Imports System.Net
Imports Microsoft.Win32
Imports Microsoft.WindowsAPICodePack.Taskbar

Public Class Form1
    Public Property versionCode As String = "2"
    Public Property version As String = "1.1.0"
    Public Property isGameInstalled As Boolean
    Public Property gamePath As String
    Public Property isGameRunning As Boolean
    Public Property isBackupRunning As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        logTxtBox.AppendText("Init: Logging started: " & Now.ToString("MM/dd/yyyy HH:mm:ss"))
        logTxtBox.AppendText(Environment.NewLine & "Init: Version: " & version.ToString())

        'Check if the game is installed
        Dim gameReg As RegistryKey
        gameReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher\Installs\1771", False)

        Try
            gamePath = gameReg.GetValue("InstallDir").ToString()
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
        updateCheckerChkBox.Checked = My.Settings.checkUpdates
        WriteLogToFileToolStripMenuItem.Checked = My.Settings.writeLogFile
        If gamePath <> Nothing Then
            processCheckTimer.Interval = 500
            processCheckTimer.Start()
        End If
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If saveLocTextBox.Text <> "" Then
            If Not Directory.Exists(saveLocTextBox.Text.ToString()) Then
                saveLocTextBox.Text = ""
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " WARNING: The save games folder no longer exists.")
            End If
        End If

        If destLocTextBox.Text <> "" Then
            If Not Directory.Exists(destLocTextBox.Text.ToString()) Then
                destLocTextBox.Text = ""
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " WARNING: The backup directory no longer exists.")
            End If
        End If

        'Check for updates
        If updateCheckerChkBox.Checked = True Then
            'Dim remoteUrl As String = "http://localhost/grw/" 'Debugging only
            Dim remoteUrl As String = "https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/"
            Dim fileName As String = "version"
            Dim remoteResource As String = remoteUrl + fileName
            Dim dl As Boolean

            Try
                Dim updater As New WebClient()
                updater.DownloadFile(remoteResource, fileName) 'This will overwrite the file if it already exists
                updater.Dispose()
                dl = True
            Catch WebException As Exception
                dl = False
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " WARNING: 'WebException' Connection failed.")
            End Try

            If dl <> False Then
                Try
                    Dim fetchedVersionCode As Integer = File.ReadAllText(Application.StartupPath + "\" + fileName)

                    If fetchedVersionCode = versionCode Then
                        logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: GHOST Buster is up to date.")
                    ElseIf fetchedVersionCode > versionCode Then
                        logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: New version of GHOST Buster is available.")
                        Dim choice As Integer = MessageBox.Show("New version of GHOST Buster is available. Press OK to visit the releases page and download the newest version.",
                                        "Update available",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Asterisk,
                                        MessageBoxDefaultButton.Button1)
                        If choice = DialogResult.OK Then
                            Dim dlPage As String = "https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest"
                            Process.Start(dlPage)
                        End If
                    ElseIf fetchedVersionCode < versionCode Then
                        logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: The version in use is greater than the one currently available.")
                    End If

                Catch pathTooLong As PathTooLongException
                    logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " ERROR: 'PathTooLongException', Version check failed.")

                Catch conversionError As InvalidCastException
                    logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " ERROR: 'InvalidCastException', Version check failed.")
                End Try
            End If
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

    Private Sub logoBigPictureBox_Click(sender As Object, e As EventArgs) Handles logoBigPictureBox.Click
        MessageBox.Show("GHOST Buster v" & version.ToString() _
            & Environment.NewLine &
            "This software is licensed under MIT." _
            & Environment.NewLine & Environment.NewLine &
            "Author: Strappazzon" _
            & Environment.NewLine &
            "Homepage: https://strappazzon.github.io/GRW-GHOST-Buster",
            "About GHOST Buster",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            0,
            "https://github.com/Strappazzon/GRW-GHOST-Buster/issues")
    End Sub

    Private Sub playGameBtn_Click(sender As Object, e As EventArgs) Handles playGameBtn.Click
        Process.Start(gamePath + "GRW.exe")
    End Sub

    Private Sub updateCheckerChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles updateCheckerChkBox.CheckedChanged
        If updateCheckerChkBox.Checked = True Then
            My.Settings.checkUpdates = True
        Else
            My.Settings.checkUpdates = False
        End If
    End Sub

    Private Sub browseSaveLocBtn_Click(sender As Object, e As EventArgs) Handles browseSaveLocBtn.Click
        Using O As New FolderBrowserDialog
            O.ShowNewFolderButton = False
            O.Description = "Select the location of Wildlands save games folder." & Environment.NewLine & "The game ID is 1771."
            O.SelectedPath = "C:\Program Files (x86)\Ubisoft\Ubisoft Game Launcher\savegames"
            If O.ShowDialog = Windows.Forms.DialogResult.OK Then
                saveLocTextBox.Text = O.SelectedPath
                My.Settings.GameSavesDir = saveLocTextBox.Text
                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Savegames location set to: " & O.SelectedPath)
                O.Dispose()
            End If
        End Using
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

    Private Sub backupBtn_Click(sender As Object, e As EventArgs) Handles backupBtn.Click
        If saveLocTextBox.Text = "" Or destLocTextBox.Text = "" Then
            MessageBox.Show("The working directories cannot be empty!",
                "Notice",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button1)
        ElseIf isGameRunning = True Then
            isBackupRunning = True
            restoreBtn.Enabled = False
            saveLocTextBox.Enabled = False
            browseSaveLocBtn.Enabled = False
            destLocTextBox.Enabled = False
            browseDestLocBtn.Enabled = False
            freqSelectTimeUpDown.Enabled = False
            backupTimer.Interval = freqSelectTimeUpDown.Value * 60000
            'backupTimer.Interval = 3000 'Debugging only

            backupTimer.Start()
            taskbarProgressTimer.Start()
            backupBtn.Enabled = False
            stopBtn.Enabled = True

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
                backupBtn.Enabled = True
                stopBtn.Enabled = False
                saveLocTextBox.Enabled = True
                browseSaveLocBtn.Enabled = True
                destLocTextBox.Enabled = True
                browseDestLocBtn.Enabled = True
                freqSelectTimeUpDown.Enabled = True
                restoreBtn.Enabled = True
                backupTimer.Stop()

                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " ERROR: 'PathTooLongException', Backup interrupted.")
                MessageBox.Show("The path you specified cannot be handled because it is too long, as a result the backup process has been interrupted.", "Backup Interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Catch dirNotFound As DirectoryNotFoundException
                backupBtn.Enabled = True
                stopBtn.Enabled = False
                saveLocTextBox.Enabled = True
                browseSaveLocBtn.Enabled = True
                destLocTextBox.Enabled = True
                browseDestLocBtn.Enabled = True
                freqSelectTimeUpDown.Enabled = True
                restoreBtn.Enabled = True
                backupTimer.Stop()

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

                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup completed.")

            Catch pathTooLong As PathTooLongException
                backupBtn.Enabled = True
                stopBtn.Enabled = False
                saveLocTextBox.Enabled = True
                browseSaveLocBtn.Enabled = True
                destLocTextBox.Enabled = True
                browseDestLocBtn.Enabled = True
                freqSelectTimeUpDown.Enabled = True
                restoreBtn.Enabled = True
                backupTimer.Stop()

                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " ERROR: 'PathTooLongException', Backup interrupted.")
                MessageBox.Show("The path you specified cannot be handled because it is too long, as a result the backup process has been interrupted.", "Backup interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Catch dirNotFound As DirectoryNotFoundException
                backupBtn.Enabled = True
                stopBtn.Enabled = False
                saveLocTextBox.Enabled = True
                browseSaveLocBtn.Enabled = True
                destLocTextBox.Enabled = True
                browseDestLocBtn.Enabled = True
                freqSelectTimeUpDown.Enabled = True
                restoreBtn.Enabled = True
                backupTimer.Stop()

                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " ERROR: 'DirectoryNotFoundException', Backup interrupted.")
                MessageBox.Show("The directory no longer exists, as a result the backup process has been interrupted.", "Backup interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            backupBtn.Enabled = True
            stopBtn.Enabled = False
            saveLocTextBox.Enabled = True
            browseSaveLocBtn.Enabled = True
            destLocTextBox.Enabled = True
            browseDestLocBtn.Enabled = True
            freqSelectTimeUpDown.Enabled = True
            restoreBtn.Enabled = True
            backupTimer.Stop()
            isBackupRunning = False
            taskbarProgressTimer.Stop()

            logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " WARNING: Wildlands closed or crashed, Backup interrupted.")
            MessageBox.Show("Wildlands has been closed or crashed, as a result the backup process has been interrupted.", "Wildlands is not running", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub taskbarProgressTimer_Tick(sender As Object, e As EventArgs) Handles taskbarProgressTimer.Tick
        'Progress bar in the taskbar icon. It resets every backup.
        If TaskbarManager.IsPlatformSupported And isBackupRunning = True Then
            TaskbarManager.Instance.SetProgressValue(Date.Now.ToString("mmss") + freqSelectTimeUpDown.Value * 60000 / 10000, freqSelectTimeUpDown.Value * 60000)
        End If
    End Sub

    Private Sub stopBtn_Click(sender As Object, e As EventArgs) Handles stopBtn.Click
        If confirmStopBackupChkBox.Checked = True Then
            Dim choice As Integer = MessageBox.Show("Are you sure you want to interrupt the backup process?", "Confirm backup interruption", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If choice = DialogResult.Yes Then
                backupBtn.Enabled = True
                stopBtn.Enabled = False
                saveLocTextBox.Enabled = True
                browseSaveLocBtn.Enabled = True
                destLocTextBox.Enabled = True
                browseDestLocBtn.Enabled = True
                freqSelectTimeUpDown.Enabled = True
                restoreBtn.Enabled = True
                backupTimer.Stop()
                isBackupRunning = False
                taskbarProgressTimer.Stop()

                logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup interrupted by the user.")
            End If
        Else
            isBackupRunning = False
            backupBtn.Enabled = True
            stopBtn.Enabled = False
            saveLocTextBox.Enabled = True
            browseSaveLocBtn.Enabled = True
            destLocTextBox.Enabled = True
            browseDestLocBtn.Enabled = True
            freqSelectTimeUpDown.Enabled = True
            restoreBtn.Enabled = True
            backupTimer.Stop()

            logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup interrupted by the user.")
        End If
    End Sub

    Private Sub restoreBtn_Click(sender As Object, e As EventArgs) Handles restoreBtn.Click
        If saveLocTextBox.Text = "" Or destLocTextBox.Text = "" Then
            MessageBox.Show("The working directories cannot be empty!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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
                                        & "ones from the cloud as soon as you launch the game!" _
                                        & Environment.NewLine & Environment.NewLine &
                                        "THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?",
                                        "READ CAREFULLY",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning,
                                        MessageBoxDefaultButton.Button2)
            If choice = DialogResult.Yes Then
                backupBtn.Enabled = True
                stopBtn.Enabled = False
                saveLocTextBox.Enabled = True
                browseSaveLocBtn.Enabled = True
                destLocTextBox.Enabled = True
                browseDestLocBtn.Enabled = True
                freqSelectTimeUpDown.Enabled = True
                backupTimer.Stop()

                Dim gameLoc As String = saveLocTextBox.Text
                Dim backupLoc As String = destLocTextBox.Text

                Try
                    Dim saveList As String() = Directory.GetFiles(backupLoc, "*.save")
                    For Each f As String In saveList
                        Dim fName As String = f.Substring(backupLoc.Length + 1)
                        File.Copy(Path.Combine(backupLoc, fName), Path.Combine(gameLoc, fName), True)
                    Next

                    logTxtBox.AppendText(Environment.NewLine & Now.ToString("[HH:mm]") & " INFO: Backup from " & destLocTextBox.Text & " restored.")
                    MessageBox.Show("The save files have been restored succefully." & Environment.NewLine & "Please select the backup folder again, without the ""yyyyMMdd HHMM"" folder, by clicking the ""Browse..."" button.",
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
            S.InitialDirectory = Application.StartupPath.ToString()
            S.FileName = "GHOSTbackup_" + Now.ToString("yyyyMMddHHmmss")
            S.Filter = "Text file|.txt|Log file|*.log"
            If S.ShowDialog = DialogResult.OK Then
                My.Computer.FileSystem.WriteAllText(S.FileName.ToString(), logTxtBox.Text, False)
                logTxtBox.AppendText(Environment.NewLine & "INFO: Log exported at " + S.FileName.ToString())
                S.Dispose()
            End If
        End Using
    End Sub
End Class

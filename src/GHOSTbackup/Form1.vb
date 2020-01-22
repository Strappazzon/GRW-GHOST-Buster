Imports System.IO
Imports System.Net
Imports System.Text
Imports Microsoft.Win32

Public Class Form1
    Public ReadOnly versionCode As Short = 13
    Public ReadOnly version As String = "1.6.0"
    Public isUplayInstalled As Boolean = False
    Public gamePath As String
    Public uplayPath As String
    Public isGameRunning As Boolean = False
    Public isBackupRunning As Boolean = False
    Public backupTimestamp As Date
    Public restoreTimestamp As Date
    Public latestBackupExists As Boolean = False
    Public secondToLastBackupExists As Boolean = False
    Public Shared backupDirs As List(Of String)

    Sub upgradeSettings()
        'Migrate settings to the new version
        'Unfortunately, settings migrate only if the new version is installed in the same directory as the old version
        '//bytes.com/topic/visual-basic-net/answers/854235-my-settings-upgrade-doesnt-upgrade#post3426232
        If My.Settings.MustUpgrade = True Then
            My.Settings.Upgrade()
            My.Settings.MustUpgrade = False
        End If
    End Sub

    Sub saveSettings()
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

        If settingsLogFilePathTextBox.Text <> My.Settings.LogFilePath Then
            My.Settings.LogFilePath = settingsLogFilePathTextBox.Text
        End If

        If disableCloudSyncChkBox.CheckState <> My.Settings.DisableCloudSync Then
            My.Settings.DisableCloudSync = disableCloudSyncChkBox.CheckState
        End If

        If settingsWhichBackupDropdownCombo.SelectedIndex <> My.Settings.WhichBackup Then
            My.Settings.WhichBackup = settingsWhichBackupDropdownCombo.SelectedIndex
        End If

        'If Wildlands executable location is empty don't save these settings
        If settingsNonUplayVersionChkBox.CheckState <> My.Settings.NoUplay AndAlso settingsCustomExeTextBox.Text <> "" Then
            My.Settings.NoUplay = settingsNonUplayVersionChkBox.CheckState
            My.Settings.CustomExeLoc = settingsCustomExeTextBox.Text
        End If

        log("[INFO] Settings saved.")
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

    Sub showAlert(alertType As Short, alertDesc As String)
        'Non-intrusive alert
        If alertType = 48 Then
            'Warning
            alertIcon.Image = My.Resources.alert
            alertDot.Visible = True
        ElseIf alertType = 64 Then
            'Info
            alertIcon.Image = My.Resources.info
        End If

        'Alert message
        alertDescriptionLabel.Text = alertDesc

        logoBigPictureBox.Location = New Point(12, 115)
        playGameBtn.Location = New Point(12, 180)
        confirmExitChkBox.Location = New Point(14, 255)
        confirmStopBackupChkBox.Location = New Point(14, 280)
        disableCloudSyncChkBox.Location = New Point(14, 305)
        updateCheckerChkBox.Location = New Point(14, 330)
        formPositionChkBox.Location = New Point(14, 355)
        alertDescriptionLabel.Location = New Point(alertContainer.Width / 2 - alertDescriptionLabel.Width / 2, alertContainer.Height / 2 - alertDescriptionLabel.Height / 2)
        alertIcon.Location = New Point(alertContainer.Width / 2 - alertDescriptionLabel.Width / 2 - 28, alertContainer.Height / 2 - alertIcon.Height / 2)
        alertContainer.Visible = True
    End Sub

    Sub showMsgBox(ByVal Message As String, ByVal Title As String, ByVal Buttons As MessageBoxButtons, ByVal Icon As MessageBoxIcon, Optional ByVal DefaultButton As MessageBoxDefaultButton = MessageBoxDefaultButton.Button2)
        'Custom MessageBox
        '//docs.microsoft.com/en-us/dotnet/api/system.windows.forms.form.dialogresult

        'Set Message and Message Title
        'The content of the message is written in Rich Text Format
        '//www.oreilly.com/library/view/rtf-pocket-guide/9781449302047/ch01.html
        'When printing a string variable that is a path or otherwise contains any backward slashes they MUST be escaped with yourVariable.Replace("\", "\\")
        CustomMsgBox.messageRTF.Rtf = Message
        CustomMsgBox.titleLabel.Text = Title

        If Buttons = MessageBoxButtons.OK OrElse Buttons = MessageBoxButtons.OKCancel Then
            '[OK] or [OK][Cancel] dialog
            CustomMsgBox.rButton.DialogResult = DialogResult.OK
            CustomMsgBox.cButton.DialogResult = DialogResult.Cancel
            'Hide [Yes] button and make [No] button the [OK] button
            CustomMsgBox.lButton.Visible = False
            CustomMsgBox.rButton.Text = "OK"
            CustomMsgBox.AcceptButton = CustomMsgBox.rButton
            CustomMsgBox.CancelButton = CustomMsgBox.cButton

            Select Case DefaultButton
                Case MessageBoxDefaultButton.Button1
                    '[OK] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.rButton
                Case MessageBoxDefaultButton.Button2, MessageBoxDefaultButton.Button3
                    '[Cancel] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.cButton
            End Select
        ElseIf Buttons = MessageBoxButtons.YesNo OrElse MessageBoxButtons.YesNoCancel Then
            '[Yes][No] or [Yes][No][Cancel] dialog
            CustomMsgBox.lButton.DialogResult = DialogResult.Yes
            CustomMsgBox.rButton.DialogResult = DialogResult.No
            CustomMsgBox.cButton.DialogResult = DialogResult.Cancel
            'Show [Yes] button and make [OK] button the [No] button
            CustomMsgBox.lButton.Visible = True
            CustomMsgBox.rButton.Text = "No"
            CustomMsgBox.AcceptButton = CustomMsgBox.lButton
            CustomMsgBox.CancelButton = CustomMsgBox.cButton

            Select Case DefaultButton
                Case MessageBoxDefaultButton.Button1
                    '[Yes] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.lButton
                Case MessageBoxDefaultButton.Button2
                    '[No] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.rButton
                Case MessageBoxDefaultButton.Button3
                    '[Cancel] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.cButton
            End Select
        End If

        Select Case Icon
            Case MessageBoxIcon.Error, MessageBoxIcon.Hand, MessageBoxIcon.Stop
                CustomMsgBox.iconPictureBox.Image = My.Resources.error_icon
            Case MessageBoxIcon.Exclamation, MessageBoxIcon.Warning
                CustomMsgBox.iconPictureBox.Image = My.Resources.alert_triangle
            Case MessageBoxIcon.Question
                CustomMsgBox.iconPictureBox.Image = My.Resources.question_icon
        End Select

        'Display the custom MessageBox as a modal
        CustomMsgBox.ShowDialog()
    End Sub

    Sub log([event] As String)
        'Don't start the log file with an empty line
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
        settingsNonUplayVersionChkBox.Enabled = False
        settingsCustomExeTextBox.Enabled = False
        settingsBrowseCustomExeBtn.Enabled = False
        settingsOpenCustomExeFolderBtn.Enabled = False
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
        settingsNonUplayVersionChkBox.Enabled = True
        settingsCustomExeTextBox.Enabled = True
        settingsBrowseCustomExeBtn.Enabled = True
        settingsOpenCustomExeFolderBtn.Enabled = True
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
                showMsgBox("{\rtf1 Restoring a backup will copy the save files over from the backup folder: " & latestBackupLoc.Replace("\", "\\") & "\line\line and will {\b OVERWRITE} the existing save files inside the game folder: " & saveLoc.Replace("\", "\\") _
                           & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                           "Backup restore",
                           MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If CustomMsgBox.DialogResult = DialogResult.Yes Then
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
                showMsgBox("{\rtf1 You chose to restore the latest backup but you haven't backed up any save game yet. Backup at least once and try again.}", "No backup found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ElseIf settingsWhichBackupDropdownCombo.SelectedIndex = 1 And secondToLastBackupExists = True Then
                'If "Second-to-last" option is selected and the second-to-last backup exists
                showMsgBox("{\rtf1 Restoring a backup will copy the save files over from the backup folder: " & secToLastBackupLoc.Replace("\", "\\") & "\line\line and will {\b OVERWRITE} the existing save files inside the game folder: " _
                           & saveLoc.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                           "Backup restore",
                           MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If CustomMsgBox.DialogResult = DialogResult.Yes Then
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
                showMsgBox("{\rtf1 You chose to restore the second-to-last backup but it doesn't exist. Do you want to restore the latest backup instead? This will copy the save files over from the backup folder: " & latestBackupLoc.Replace("\", "\\") _
                           & "\line\line and will {\b OVERWRITE} the existing save files inside the game folder: " & saveLoc.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                           "Backup doesn't exist",
                           MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If CustomMsgBox.DialogResult = DialogResult.Yes Then
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
                showMsgBox("{\rtf1 You chose to restore the second-to-last backup but neither second-to-last nor latest backup exist. Backup at least once and try again.}", "No backup found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                log("[INFO] No backup found (secondToLast, latest). Restore process aborted.")
            ElseIf settingsWhichBackupDropdownCombo.SelectedIndex = 2 Then
                'If "Let me decide" option is selected the user will be asked from what folder the backup should be restored from
                'Get backup directory subfolders
                '//docs.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratedirectories
                backupDirs = New List(Of String)(Directory.EnumerateDirectories(destLocTextBox.Text))

                'Reverse the order of directories list (Most recent backup first)
                '//docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.reverse
                backupDirs.Reverse()

                'Make the dropdown menu visible and set its max items
                CustomMsgBox.backupDirsDropdownCombo.Visible = True
                CustomMsgBox.backupDirsDropdownCombo.MaxDropDownItems = backupDirs.Count

                'Add all directories to CustomMsgBox dropdown menu
                For Each backupDir In backupDirs
                    CustomMsgBox.backupDirsDropdownCombo.Items.Add(backupDir.Substring(backupDir.LastIndexOf(Path.DirectorySeparatorChar) + 1))
                Next

                'Select the first folder on the list
                CustomMsgBox.backupDirsDropdownCombo.SelectedIndex = 0

                showMsgBox("{\rtf1 Restoring a backup will copy the save files over from the backup folder that you selected from the list below (which is inside " & backupLoc.Replace("\", "\\") _
                           & ")\line\line and will {\b OVERWRITE} the existing save files inside the game folder: " & saveLoc.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                           "Backup restore",
                           MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If CustomMsgBox.DialogResult = DialogResult.Yes Then
                    'Store selected backup subdirectory
                    Dim backupSubDir = backupLoc & "\" & CustomMsgBox.backupDirsDropdownCombo.SelectedItem
                    Dim saveList As String() = Directory.GetFiles(backupSubDir, "*.save")
                    For Each F As String In saveList
                        Dim fName As String = F.Substring(backupSubDir.Length + 1)
                        File.Copy(Path.Combine(backupSubDir, fName), Path.Combine(saveLoc, fName), True)
                    Next

                    'Empty subdirectories list to avoid adding duplicates in the next restore process
                    CustomMsgBox.backupDirsDropdownCombo.Items.Clear()
                    backupDirs = Nothing

                    log("[INFO] Backup from " & backupSubDir & " restored.")
                    showAlert(64, "Backup restored successfully.")
                Else
                    'Empty subdirectories list to avoid adding duplicates in the next restore process
                    CustomMsgBox.backupDirsDropdownCombo.Items.Clear()
                    backupDirs = Nothing

                    log("[INFO] Restore process cancelled by the user.")
                End If
            End If

        Catch pathTooLong As PathTooLongException
            'Empty subdirectories list to avoid adding duplicates in the next restore process
            CustomMsgBox.backupDirsDropdownCombo.Items.Clear()
            backupDirs = Nothing

            log("[ERROR] 'PathTooLongException', Couldn't restore the backup from " & destLocTextBox.Text & " to " & saveLocTextBox.Text)
            showMsgBox("{\rtf1 The specified path cannot be handled because it's too long, as a result the restore process has been interrupted.}", "Restore failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

        Catch dirNotFound As DirectoryNotFoundException
            'Empty subdirectories list to avoid adding duplicates in the next restore process
            CustomMsgBox.backupDirsDropdownCombo.Items.Clear()
            backupDirs = Nothing

            log("[ERROR] 'DirectoryNotFoundException', Couldn't restore the backup from " & destLocTextBox.Text & " to " & saveLocTextBox.Text)
            showMsgBox("{\rtf1 One or more folders no longer exist, as a result the restore process has been interrupted.}", "Restore failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub updater_DownloadStringCompleted(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
        If e.Error Is Nothing Then
            Dim fetchedVer As Short = e.Result

            'Compare downloaded GHOST Buster version number with the current one
            If fetchedVer = versionCode Then
                log("[INFO] GHOST Buster is up to date.")
            ElseIf fetchedVer > versionCode Then
                log("[INFO] New version of GHOST Buster is available.")
                showMsgBox("{\rtf1 A newer version of GHOST Buster is available. Do you want to visit the download page now?}", "Update available", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If CustomMsgBox.DialogResult = DialogResult.Yes Then
                    Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest")
                End If
            ElseIf fetchedVer < versionCode Then
                log("[INFO] The version in use is greater than the one currently available.")
            End If
        Else
            log("[ERROR] 'WebException' Unable to check for updates: " & (e.Error.Message & ".").Replace("..", "."))
            showAlert(48, "Unable to check for updates. See the logs for more details.")
        End If
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
        disableCloudSyncChkBox.Checked = My.Settings.DisableCloudSync
        settingsWhichBackupDropdownCombo.SelectedIndex = My.Settings.WhichBackup
        settingsNonUplayVersionChkBox.Checked = My.Settings.NoUplay
        settingsCustomExeTextBox.Text = My.Settings.CustomExeLoc

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
        If settingsNonUplayVersionChkBox.Checked = True Then
            If File.Exists(settingsCustomExeTextBox.Text) Then
                gamePath = Directory.GetParent(settingsCustomExeTextBox.Text).ToString() & "\"
                playGameBtn.Enabled = True
                log("[INFO] Wildlands is installed in: " & gamePath & " (Non-Uplay version).")
                processCheckTimer.Interval = 500
                processCheckTimer.Start()
            Else
                'Disable "I'm not using the Uplay version of Wildlands"
                settingsNonUplayVersionChkBox.Checked = False
                playGameBtn.Text = "Ghost Recon Wildlands not found"
                log("[WARNING] Custom Wildlands executable " & settingsCustomExeTextBox.Text & " not found.")
                showAlert(48, "The specified Wildlands executable could note be found.")
            End If
        Else
            Using gameReg As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher\Installs\1771", False)
                Try
                    'Replace forward slashes
                    gamePath = TryCast(gameReg.GetValue("InstallDir"), String).Replace("/", "\")
                    gameReg.Close()

                    If gamePath <> Nothing Then
                        playGameBtn.Enabled = True
                        log("[INFO] Wildlands is installed in: " & gamePath)
                        processCheckTimer.Interval = 500
                        processCheckTimer.Start()
                    Else
                        playGameBtn.Text = "Ghost Recon Wildlands is not installed"
                        log("[WARNING] Wildlands is not installed (""InstallDir"" is Null or Empty).")
                    End If

                Catch nullValue As NullReferenceException
                    playGameBtn.Text = "Ghost Recon Wildlands is not installed"
                    log("[WARNING] 'NullReferenceException' Wildlands is not installed.")
                End Try
            End Using
        End If

        'Retrieve Uplay install directory
        Using uplayReg As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher", False)
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
        End Using

        'Check if save games directory exists
        If saveLocTextBox.Text <> "" AndAlso Not Directory.Exists(saveLocTextBox.Text) Then
            log("[WARNING] Wildlands save games folder " & saveLocTextBox.Text & " no longer exists.")
            showAlert(48, "Wildlands save games folder no longer exists.")
            saveLocTextBox.Text = ""
            My.Settings.GameSavesDir = ""
        End If

        'Check if backup directory exists
        If destLocTextBox.Text <> "" AndAlso Not Directory.Exists(destLocTextBox.Text) Then
            log("[WARNING] Backup folder " & destLocTextBox.Text & " no longer exists.")
            showAlert(48, "Backup folder no longer exists.")
            destLocTextBox.Text = ""
            My.Settings.BackupDir = ""
        End If

        'Check for updates
        '//docs.microsoft.com/en-us/dotnet/api/system.net.downloadstringcompletedeventargs
        If updateCheckerChkBox.Checked = True Then
            Using updater As New WebClient
                updater.Headers.Add("User-Agent", "GHOST Buster (+https://strappazzon.xyz/GRW-GHOST-Buster)")
                Dim versionURI As New Uri("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/version")
                updater.DownloadStringAsync(versionURI)
                'Call updater_DownloadStringCompleted when the download completes
                AddHandler updater.DownloadStringCompleted, AddressOf updater_DownloadStringCompleted
            End Using
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If isBackupRunning = True And confirmExitChkBox.Checked = True Then
            showMsgBox("{\rtf1 The backup process is still running. Do you want to interrupt it and exit?}", "Confirm exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If CustomMsgBox.DialogResult = DialogResult.No OrElse CustomMsgBox.DialogResult = DialogResult.Cancel Then
                e.Cancel = True
            Else
                saveSettings()
            End If
        Else
            saveSettings()
        End If
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
                showMsgBox("{\rtf1 Wildlands has been closed or crashed, as a result the backup process has been interrupted.}", "Wildlands is no longer running", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
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
        aboutContainer.Visible = False
        logsContainer.Visible = False
        titleLabel.Visible = False
        settingsContainer.Visible = False
    End Sub

    Private Sub settingsLabel_Click(sender As Object, e As EventArgs) Handles settingsLabel.Click
        homePictureBtn.Image = My.Resources.home
        aboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        logLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        settingsLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        backupGroupBox.Visible = False
        pathsGroupBox.Visible = False
        aboutContainer.Visible = False
        logsContainer.Visible = False
        titleLabel.Text = "Advanced Settings"
        titleLabel.Visible = True
        settingsContainer.Visible = True
    End Sub

    Private Sub logLabel_Click(sender As Object, e As EventArgs) Handles logLabel.Click
        homePictureBtn.Image = My.Resources.home
        logLabel.ForeColor = Color.FromArgb(255, 255, 255, 255)
        aboutLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        settingsLabel.ForeColor = Color.FromArgb(255, 85, 170, 255)
        backupGroupBox.Visible = False
        pathsGroupBox.Visible = False
        aboutContainer.Visible = False
        logsContainer.Visible = True
        titleLabel.Text = "Logs"
        titleLabel.Visible = True
        settingsContainer.Visible = False
        alertDot.Visible = False
        'Close the alert when switching to Logs tab
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
        logsContainer.Visible = False
        titleLabel.Text = "About"
        titleLabel.Visible = True
        settingsContainer.Visible = False
    End Sub

    Private Sub uplayPictureBtn_Click(sender As Object, e As EventArgs) Handles uplayPictureBtn.Click
        'Attempt to launch Uplay only if it's installed
        If isUplayInstalled = True Then
            Process.Start(uplayPath & "Uplay.exe")
        Else
            showAlert(64, "Uplay is not installed.")
        End If
    End Sub

    Private Sub closeAlertContainerIcon_Click(sender As Object, e As EventArgs) Handles closeAlertContainerIcon.Click
        alertContainer.Visible = False
        logoBigPictureBox.Location = New Point(12, 85)
        playGameBtn.Location = New Point(12, 150)
        confirmExitChkBox.Location = New Point(14, 230)
        confirmStopBackupChkBox.Location = New Point(14, 255)
        disableCloudSyncChkBox.Location = New Point(14, 280)
        updateCheckerChkBox.Location = New Point(14, 305)
        formPositionChkBox.Location = New Point(14, 330)
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

    Private Sub disableCloudSyncChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles disableCloudSyncChkBox.CheckedChanged
        If disableCloudSyncChkBox.Checked = True Then
            disableCloudSyncChkBox.ForeColor = Color.White
        Else
            disableCloudSyncChkBox.ForeColor = Color.FromArgb(255, 85, 170, 255)
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
            O.Description = "Select Wildlands save games folder. If you don't know where it is, please consult PC Gaming Wiki."
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
        'Open the save games directory in Windows Explorer
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
                showMsgBox("{\rtf1 The specified path cannot be handled because it's too long, as a result the backup process has been interrupted.}", "Backup Interrupted", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
            Catch dirNotFound As DirectoryNotFoundException
                stopBackup()
                log("[ERROR] 'DirectoryNotFoundException', Backup interrupted.")
                showMsgBox("{\rtf1 The specified folder no longer exists, as a result the backup process has been interrupted.}", "Backup interrupted", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
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
                showMsgBox("{\rtf1 The specified path cannot be handled because it's too long, as a result the backup process has been interrupted.}", "Backup interrupted", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
            Catch dirNotFound As DirectoryNotFoundException
                stopBackup()
                log("[ERROR] 'DirectoryNotFoundException', Backup interrupted.")
                showMsgBox("{\rtf1 The specified folder no longer exists, as a result the backup process has been interrupted.}", "Backup interrupted", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
            End Try
        Else
            stopBackup()
            log("[WARNING] Wildlands closed or crashed, Backup interrupted.")
            showMsgBox("{\rtf1 Wildlands has been closed or crashed, as a result the backup process has been interrupted.}", "Wildlands is no longer running", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub stopBtn_Click(sender As Object, e As EventArgs) Handles stopBtn.Click
        If confirmStopBackupChkBox.Checked = True Then
            showMsgBox("{\rtf1 Are you sure you want to interrupt the backup process?}", "Backup interruption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If CustomMsgBox.DialogResult = DialogResult.Yes Then
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
        ElseIf isGameRunning = False And disableCloudSyncChkBox.Checked = True Then
            'If the game is not running and "Let GHOST Buster disable cloud save synchronization" is checked
            'Check if Uplay is running or not before editing its settings file
            Dim uProc = Process.GetProcessesByName("upc")
            If uProc.Count > 0 Then
                showMsgBox("{\rtf1 You need to quit Uplay before restoring a backup because you chose to let GHOST Buster disable cloud save synchronization for you.}", "Cannot restore", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            Else
                'Disable Uplay cloud save synchronization
                Try
                    Dim pathToYAML As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Ubisoft Game Launcher\settings.yml"
                    log("[INFO] Parsing and evaluating Uplay settings file: " & pathToYAML)
                    Dim parsedYAML As String = File.ReadAllText(pathToYAML)

                    If parsedYAML.Contains("syncsavegames: true") Then
                        'Backup Uplay settings file
                        log("[INFO] Backing up Uplay settings file to " & pathToYAML & ".bak")
                        File.Copy(pathToYAML, pathToYAML & ".bak", False) 'Don't overwrite the backup file in the future

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
                    disableCloudSyncChkBox.Checked = False
                    '...notify the user about the error
                    log("[ERROR] Parsing of ""settings.yml"" failed: File not found.")
                    showMsgBox("{\rtf1 ""Let GHOST Buster disable cloud save synchronization"" setting has been disabled because an error occurred while trying to parse Uplay settings file: {\b File not found.}" _
                               & "\line\line Make sure to {\b DISABLE} cloud save synchronization from Uplay (Settings -> Untick ""Enable cloud save synchronization for supported games"") before launching Wildlands, otherwise the restored save games will be " _
                               & "{\b OVERWRITTEN} with the old ones from the cloud!",
                               "Parsing failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
                    '...and proceed with the restore process anyway
                    restoreBackup()
                Catch insufficentPermissions As UnauthorizedAccessException
                    'Don't let GHOST Buster disable cloud save sync until the user enables the setting again...
                    disableCloudSyncChkBox.Checked = False
                    '...notify the user about the error
                    log("[ERROR] Parsing of ""settings.yml"" failed: File is read only.")
                    showMsgBox("{\rtf1 ""Let GHOST Buster disable cloud save synchronization"" setting has been disabled because an error occurred while trying to parse Uplay settings file: {\b File not found.}" _
                               & "\line\line Make sure to {\b DISABLE} cloud save synchronization from Uplay (Settings -> Untick ""Enable cloud save synchronization for supported games"") before launching Wildlands, otherwise the restored save games will be " _
                               & "{\b OVERWRITTEN} with the old ones from the cloud!",
                               "Parsing failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
                    '...and proceed with the restore process anyway
                    restoreBackup()
                End Try
            End If
        ElseIf isGameRunning = False And disableCloudSyncChkBox.Checked = False Then
            'If the game is not running and "Let GHOST Buster disable cloud save synchronization" is not checked
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
        Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/blob/master/LICENSE.txt")
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
        If settingsLogFilePathTextBox.Text <> "" AndAlso File.Exists(settingsLogFilePathTextBox.Text) Then
            Process.Start(settingsLogFilePathTextBox.Text)
        Else
            showAlert(64, "The event log file does not exist.")
        End If
    End Sub

    Private Sub settingsNonUplayVersionChkBox_CheckedChanged(sender As Object, e As EventArgs) Handles settingsNonUplayVersionChkBox.CheckedChanged
        If settingsNonUplayVersionChkBox.Checked = False Then
            settingsCustomExeTextBox.Enabled = False
            settingsBrowseCustomExeBtn.Enabled = False
            settingsOpenCustomExeFolderBtn.Enabled = False
        Else
            settingsCustomExeTextBox.Enabled = True
            settingsBrowseCustomExeBtn.Enabled = True
            settingsOpenCustomExeFolderBtn.Enabled = True
        End If
    End Sub

    Private Sub settingsBrowseCustomExeBtn_Click(sender As Object, e As EventArgs) Handles settingsBrowseCustomExeBtn.Click
        'Choose Wildlands executable
        Using O As New OpenFileDialog
            O.Filter = "Wildlands executable (GRW.exe)|GRW.exe"
            O.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
            O.Title = "Select Wildlands executable"
            If O.ShowDialog = DialogResult.OK Then
                settingsCustomExeTextBox.Text = O.FileName
                My.Settings.CustomExeLoc = settingsCustomExeTextBox.Text
                log("[INFO] Custom Wildlands executable set: " & O.FileName)
                O.Dispose()
            End If
        End Using
    End Sub

    Private Sub settingsOpenCustomExeFolderBtn_Click(sender As Object, e As EventArgs) Handles settingsOpenCustomExeFolderBtn.Click
        'Open custom Wildlands location in Windows Explorer...
        If settingsCustomExeTextBox.Text <> "" AndAlso Directory.Exists(Directory.GetParent(settingsCustomExeTextBox.Text).ToString()) Then
            Process.Start(Directory.GetParent(settingsCustomExeTextBox.Text).ToString())
        Else
            showAlert(64, "The specified folder does not exist.")
        End If
    End Sub
End Class

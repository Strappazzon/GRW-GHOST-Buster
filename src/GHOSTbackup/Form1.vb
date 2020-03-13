Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Text
Imports Microsoft.Win32

Public Class Form1
    Private ReadOnly VersionCode As Short = 16
    Private ReadOnly Version As String = "1.8.1"
    Private GamePath As String
    Private UplayPath As String
    Private IsGameRunning As Boolean = False
    Private IsBackupRunning As Boolean = False
    Private ReadOnly BackupDirs As New List(Of String)
    Private WithEvents DetectBackupTimestamp As New BackgroundWorker()

    Private Sub UpgradeSettings()
        'Migrate settings to the new version
        'Unfortunately, settings migrate only if the new version is installed in the same directory as the old version
        '//bytes.com/topic/visual-basic-net/answers/854235-my-settings-upgrade-doesnt-upgrade#post3426232
        If My.Settings.MustUpgrade = True Then
            My.Settings.Upgrade()
            My.Settings.MustUpgrade = False
        End If
    End Sub

    Private Sub SaveSettings()
        'Confirm exit (if backup is active)
        If ConfirmExitChkBox.CheckState <> My.Settings.ConfirmExit Then
            My.Settings.ConfirmExit = ConfirmExitChkBox.CheckState
        End If

        'Confirm backup interruption
        If ConfirmStopBackupChkBox.CheckState <> My.Settings.ConfirmBackupInterruption Then
            My.Settings.ConfirmBackupInterruption = ConfirmStopBackupChkBox.CheckState
        End If

        'Disable Uplay cloud save sync
        If DisableCloudSyncChkBox.CheckState <> My.Settings.DisableCloudSync Then
            My.Settings.DisableCloudSync = DisableCloudSyncChkBox.CheckState
        End If

        'Check for updates
        If CheckUpdatesChkBox.CheckState <> My.Settings.CheckUpdates Then
            My.Settings.CheckUpdates = CheckUpdatesChkBox.CheckState
        End If

        'Remember form position, Window location
        If RememberFormPositionChkBox.CheckState <> My.Settings.RememberFormPosition Then
            My.Settings.RememberFormPosition = RememberFormPositionChkBox.CheckState
            My.Settings.WindowLocation = Location
        End If

        'Backup frequency
        If BackupFreqUpDown.Value <> My.Settings.BackupInterval Then
            My.Settings.BackupInterval = BackupFreqUpDown.Value
        End If

        'Choose which backup will be restored
        If WhichBackupDropdownCombo.SelectedIndex <> My.Settings.WhichBackup Then
            My.Settings.WhichBackup = WhichBackupDropdownCombo.SelectedIndex
        End If

        'Wildlands save games folder
        If SavegamesLocTextBox.Text <> My.Settings.GameSavesDir Then
            My.Settings.GameSavesDir = SavegamesLocTextBox.Text
        End If

        'Backup folder
        If BackupLocTextBox.Text <> My.Settings.BackupDir Then
            My.Settings.BackupDir = BackupLocTextBox.Text
        End If

        'Write events to a log file
        If SettingsWriteLogToFileChkBox.CheckState <> My.Settings.WriteLogFile Then
            My.Settings.WriteLogFile = SettingsWriteLogToFileChkBox.CheckState
            My.Settings.LogFilePath = SettingsLogFilePathTextBox.Text
        End If

        'Log file location
        If SettingsLogFilePathTextBox.Text <> My.Settings.LogFilePath Then
            My.Settings.LogFilePath = SettingsLogFilePathTextBox.Text
        End If

        'I'm not using the Uplay version of Wildlands, Custom Wildlands executable location
        'If Wildlands executable location is empty don't save these settings
        If SettingsNonUplayVersionChkBox.CheckState <> My.Settings.NoUplay AndAlso SettingsCustomExeTextBox.Text <> "" Then
            My.Settings.NoUplay = SettingsNonUplayVersionChkBox.CheckState
            My.Settings.CustomExeLoc = SettingsCustomExeTextBox.Text
        End If

        Log("[INFO] Settings saved.")
    End Sub

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

    Private Sub ShowAlert(AlertType As Short, AlertDesc As String)
        'Non-intrusive alert
        If AlertType = 48 Then
            'Warning
            AlertIcon.Image = My.Resources.alert
            AlertDot.Visible = True
        ElseIf AlertType = 64 Then
            'Info
            AlertIcon.Image = My.Resources.info
        End If

        'Alert message
        AlertDescriptionLabel.Text = AlertDesc

        LogoBigPictureBox.Location = New Point(12, 115)
        PlayGameBtn.Location = New Point(12, 180)
        ConfirmExitChkBox.Location = New Point(14, 255)
        ConfirmStopBackupChkBox.Location = New Point(14, 280)
        DisableCloudSyncChkBox.Location = New Point(14, 305)
        CheckUpdatesChkBox.Location = New Point(14, 330)
        RememberFormPositionChkBox.Location = New Point(14, 355)
        AlertDescriptionLabel.Location = New Point(AlertContainer.Width / 2 - AlertDescriptionLabel.Width / 2, AlertContainer.Height / 2 - AlertDescriptionLabel.Height / 2)
        AlertIcon.Location = New Point(AlertContainer.Width / 2 - AlertDescriptionLabel.Width / 2 - 28, AlertContainer.Height / 2 - AlertIcon.Height / 2)
        AlertContainer.Visible = True
    End Sub

    Private Sub ShowMsgBox(ByVal Message As String, ByVal Title As String, ByVal Buttons As MessageBoxButtons, ByVal Icon As MessageBoxIcon, Optional ByVal DefaultButton As MessageBoxDefaultButton = MessageBoxDefaultButton.Button2)
        'Custom MessageBox
        '//docs.microsoft.com/en-us/dotnet/api/system.windows.forms.form.dialogresult

        'Set Message and Message Title
        'The content of the message is written in Rich Text Format
        '//www.oreilly.com/library/view/rtf-pocket-guide/9781449302047/ch01.html
        'When printing a string variable that is a path or otherwise contains any backward slashes they MUST be escaped with yourVariable.Replace("\", "\\")
        CustomMsgBox.MessageRTF.Rtf = Message
        CustomMsgBox.TitleLabel.Text = Title

        If Buttons = MessageBoxButtons.OK OrElse Buttons = MessageBoxButtons.OKCancel Then
            '[OK] or [OK][Cancel] dialog
            CustomMsgBox.RightButton.DialogResult = DialogResult.OK
            CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
            'Hide [Yes] button and make [No] button the [OK] button
            CustomMsgBox.LeftButton.Visible = False
            CustomMsgBox.RightButton.Text = "OK"
            CustomMsgBox.AcceptButton = CustomMsgBox.RightButton
            CustomMsgBox.CancelLabel = CustomMsgBox.CancelLabel

            Select Case DefaultButton
                Case MessageBoxDefaultButton.Button1
                    '[OK] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.RightButton
                Case MessageBoxDefaultButton.Button2, MessageBoxDefaultButton.Button3
                    '[Cancel] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.CancelLabel
            End Select
        ElseIf Buttons = MessageBoxButtons.YesNo OrElse MessageBoxButtons.YesNoCancel Then
            '[Yes][No] or [Yes][No][Cancel] dialog
            CustomMsgBox.LeftButton.DialogResult = DialogResult.Yes
            CustomMsgBox.RightButton.DialogResult = DialogResult.No
            CustomMsgBox.CancelLabel.DialogResult = DialogResult.Cancel
            'Show [Yes] button and make [OK] button the [No] button
            CustomMsgBox.LeftButton.Visible = True
            CustomMsgBox.RightButton.Text = "No"
            CustomMsgBox.AcceptButton = CustomMsgBox.LeftButton
            CustomMsgBox.CancelLabel = CustomMsgBox.CancelLabel

            Select Case DefaultButton
                Case MessageBoxDefaultButton.Button1
                    '[Yes] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.LeftButton
                Case MessageBoxDefaultButton.Button2
                    '[No] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.RightButton
                Case MessageBoxDefaultButton.Button3
                    '[Cancel] button
                    CustomMsgBox.ActiveControl = CustomMsgBox.CancelLabel
            End Select
        End If

        Select Case Icon
            Case MessageBoxIcon.Error, MessageBoxIcon.Hand, MessageBoxIcon.Stop
                CustomMsgBox.IconPictureBox.Image = My.Resources.error_icon
            Case MessageBoxIcon.Exclamation, MessageBoxIcon.Warning
                CustomMsgBox.IconPictureBox.Image = My.Resources.alert_triangle
            Case MessageBoxIcon.Question
                CustomMsgBox.IconPictureBox.Image = My.Resources.question_icon
        End Select

        'Display the custom MessageBox as a modal
        CustomMsgBox.ShowDialog()
    End Sub

    Private Sub HelpToolTip_Draw(sender As Object, e As DrawToolTipEventArgs) Handles HelpToolTip.Draw
        'Draw tooltip with custom colors
        e.DrawBackground()
        'Don't draw the border
        'e.DrawBorder()
        e.DrawText()
    End Sub

    Private Sub Log([Event] As String)
        'Don't start the log file with an empty line
        If LogTxtBox.Text = "" Then
            LogTxtBox.AppendText(Now.ToString("HH:mm:ss") & " " & [Event])
        Else
            LogTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm:ss") & " " & [Event])
        End If

        If SettingsWriteLogToFileChkBox.Checked = True Then
            Dim LogToFile As New StringBuilder
            LogToFile.AppendLine(Now.ToString("HH:mm:ss") & " " & [Event])

            Try
                File.AppendAllText(SettingsLogFilePathTextBox.Text, LogToFile.ToString())

            Catch ex As Exception
                SettingsWriteLogToFileChkBox.Checked = False
                LogTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm:ss") & " [ERROR] Log session to file interrupted: " & ex.Message())
                ShowAlert(48, "Logging to file disabled due to an error.")
            End Try
        End If
    End Sub

    Private Sub StartBackup()
        BackupTimer.Interval = BackupFreqUpDown.Value * 60000
        BackupTimer.Start()
        IsBackupRunning = True
        BackupFreqUpDown.Enabled = False
        BackupBtn.Enabled = False
        StopBtn.Enabled = True
        RestoreBtn.Enabled = False
        SavegamesLocTextBox.Enabled = False
        BrowseSavegamesLocBtn.Enabled = False
        BackupLocTextBox.Enabled = False
        BrowseBackupLocBtn.Enabled = False
        SettingsNonUplayVersionChkBox.Enabled = False
        SettingsCustomExeTextBox.Enabled = False
        SettingsBrowseCustomExeBtn.Enabled = False
        SettingsOpenCustomExeFolderBtn.Enabled = False
    End Sub

    Private Sub StopBackup()
        BackupTimer.Stop()
        IsBackupRunning = False
        BackupFreqUpDown.Enabled = True
        BackupBtn.Enabled = True
        StopBtn.Enabled = False
        RestoreBtn.Enabled = True
        SavegamesLocTextBox.Enabled = True
        BrowseSavegamesLocBtn.Enabled = True
        BackupLocTextBox.Enabled = True
        BrowseBackupLocBtn.Enabled = True
        SettingsNonUplayVersionChkBox.Enabled = True
        SettingsCustomExeTextBox.Enabled = True
        SettingsBrowseCustomExeBtn.Enabled = True
        SettingsOpenCustomExeFolderBtn.Enabled = True
    End Sub

    Private Sub RestoreBackup()
        Log("[INFO] Restore process started.")

        Try
            'Get all directories inside the current backup folder
            '//docs.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratedirectories
            Dim EnumeratedDirs = New List(Of String)(Directory.EnumerateDirectories(BackupLocTextBox.Text))

            'If the current backup folder is not empty proceed with the restore process
            If EnumeratedDirs.Count > 0 Then
                If WhichBackupDropdownCombo.SelectedIndex = 0 Then
                    'If "Latest" option is selected
                    'Loop through every directory in the list
                    For Each BackupDir In EnumeratedDirs
                        'Get every save file inside each subdirectory
                        Dim SavegamesList As String() = Directory.GetFiles(BackupDir, "*.save")
                        If SavegamesList.Length > 0 Then
                            'If a subdirectory contains save files add it to the list
                            BackupDirs.Add(BackupDir)
                        End If
                    Next

                    If BackupDirs.Count > 0 Then
                        'If at least one directory contains save files
                        'Ask the user before restoring the latest backup
                        ShowMsgBox("{\rtf1 Restoring a backup will copy the save files over from the backup folder: " & BackupDirs.Item(BackupDirs.Count - 1).Replace("\", "\\") & "\line\line and will {\b OVERWRITE} the existing save files inside the " _
                                   & "game folder: " & SavegamesLocTextBox.Text.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                                   "Backup restore",
                                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            Dim SavegamesList As String() = Directory.GetFiles(BackupDirs.Item(BackupDirs.Count - 1), "*.save")
                            For Each F As String In SavegamesList
                                Dim FileName As String = F.Substring(BackupDirs.Item(BackupDirs.Count - 1).Length + 1)
                                File.Copy(Path.Combine(BackupDirs.Item(BackupDirs.Count - 1), FileName), Path.Combine(SavegamesLocTextBox.Text, FileName), True)
                            Next

                            Log("[INFO] Backup from " & BackupDirs.Item(BackupDirs.Count - 1) & " restored.")
                            ShowAlert(64, "Backup successfully restored.")
                        Else
                            Log("[INFO] Restore process cancelled by the user.")
                        End If
                    Else
                        'If no directory contains any save files
                        ShowMsgBox("{\rtf1 The current {\b backup folder doesn't contain any backup.} Backup at least once and try again.}", "No backup found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        Log("[INFO] No valid backup found inside " & BackupLocTextBox.Text & ". Restore process aborted.")

                        LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
                        LatestBackupHelpLabel.Location = New Point(300, 22)
                    End If
                ElseIf WhichBackupDropdownCombo.SelectedIndex = 1 Then
                    'If "Second-to-last" option is selected
                    'Loop through every directory in the list
                    For Each BackupDir In EnumeratedDirs
                        'Get every save file inside each subdirectory
                        Dim SavegamesList As String() = Directory.GetFiles(BackupDir, "*.save")
                        If SavegamesList.Length > 0 Then
                            'If a subdirectory contains save files add it to the list
                            BackupDirs.Add(BackupDir)
                        End If
                    Next

                    If BackupDirs.Count >= 2 Then
                        'Restore second-to-last backup if at least two valid backup directories exist
                        'Ask the user before restoring the second-to-last backup
                        ShowMsgBox("{\rtf1 Restoring a backup will copy the save files over from the backup folder: " & BackupDirs.Item(BackupDirs.Count - 2).Replace("\", "\\") & "\line\line and will {\b OVERWRITE} the existing save files inside the " _
                                   & "game folder: " & SavegamesLocTextBox.Text.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                                   "Backup restore",
                                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            Dim SavegamesList As String() = Directory.GetFiles(BackupDirs.Item(BackupDirs.Count - 2), "*.save")
                            For Each F As String In SavegamesList
                                Dim FileName As String = F.Substring(BackupDirs.Item(BackupDirs.Count - 2).Length + 1)
                                File.Copy(Path.Combine(BackupDirs.Item(BackupDirs.Count - 2), FileName), Path.Combine(SavegamesLocTextBox.Text, FileName), True)
                            Next

                            Log("[INFO] Backup from " & BackupDirs.Item(BackupDirs.Count - 2) & " restored.")
                            ShowAlert(64, "Backup successfully restored.")
                        Else
                            Log("[INFO] Restore process cancelled by the user.")
                        End If
                    ElseIf BackupDirs.Count = 1 Then
                        'If only one valid backup directory exists ask the user to restore the latest backup instead
                        'Ask the user before restoring the latest backup
                        ShowMsgBox("{\rtf1 You chose to restore the second-to-last backup but {\b it doesn't exist.} Do you want to restore the latest backup instead? This will copy the save files over from the backup folder: " _
                                   & BackupDirs.Item(BackupDirs.Count - 1).Replace("\", "\\") & "\line\line And will {\b OVERWRITE} the existing save files inside the game folder: " & SavegamesLocTextBox.Text.Replace("\", "\\") _
                                   & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                                   "Backup doesn't exist",
                                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            Dim SavegamesList As String() = Directory.GetFiles(BackupDirs.Item(BackupDirs.Count - 1), "*.save")
                            For Each F As String In SavegamesList
                                Dim FileName As String = F.Substring(BackupDirs.Item(BackupDirs.Count - 1).Length + 1)
                                File.Copy(Path.Combine(BackupDirs.Item(BackupDirs.Count - 1), FileName), Path.Combine(SavegamesLocTextBox.Text, FileName), True)
                            Next

                            Log("[INFO] Backup from " & BackupDirs.Item(BackupDirs.Count - 1) & " restored.")
                            ShowAlert(64, "Backup successfully restored.")
                        Else
                            Log("[INFO] Restore process cancelled by the user.")
                        End If
                    Else
                        'If no valid backup directory is found (.Count = 0) display an error
                        ShowMsgBox("{\rtf1 The current {\b backup folder doesn't contain any backup.} Backup at least once and try again.}", "No backup found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        Log("[INFO] No valid backup found inside " & BackupLocTextBox.Text & ". Restore process aborted.")

                        LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
                        LatestBackupHelpLabel.Location = New Point(300, 22)
                    End If
                ElseIf WhichBackupDropdownCombo.SelectedIndex = 2 Then
                    'If "Let me decide" option is selected
                    'Reverse the order of the directories list (most recent backup first)
                    '//docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.reverse
                    EnumeratedDirs.Reverse()

                    'Loop through every directory in the list
                    For Each BackupDir In EnumeratedDirs
                        'Get every save file inside each subdirectory
                        Dim SavegamesList As String() = Directory.GetFiles(BackupDir, "*.save")
                        If SavegamesList.Length > 0 Then
                            'If a subdirectory contains save files add it to the list and to the backup restore dialog's dropdown menu
                            'Also append their creation date and time, and if a backup was created less than 1 hour ago display "Created X minutes ago" instead
                            BackupDirs.Add(BackupDir)
                            CustomMsgBox.BackupDirsDropdownCombo.Items.Add(BackupDir.Substring(BackupDir.LastIndexOf(Path.DirectorySeparatorChar) + 1) & " - Created " &
                                If(Directory.GetCreationTime(BackupDir) > Now.AddHours(-1),
                                (Now.Subtract(Directory.GetCreationTime(BackupDir)).ToString("mm") & " minutes ago").Replace("00 minutes ago", "less than a minute ago"), Directory.GetCreationTime(BackupDir).ToString("MMMM dd yyyy \a\t HH:mm")))
                        End If
                    Next

                    If BackupDirs.Count > 0 Then
                        'If at least one directory contains save files
                        'Display the dropdown menu and select the first folder on the list
                        CustomMsgBox.BackupDirsDropdownCombo.Visible = True
                        CustomMsgBox.BackupDirsDropdownCombo.SelectedIndex = 0

                        'Ask the user from which folder the backup should be restored from
                        ShowMsgBox("{\rtf1 Restoring a backup will copy the save files over from the backup folder that you selected from the list below (which is inside " & BackupLocTextBox.Text.Replace("\", "\\") _
                                       & ")\line\line and will {\b OVERWRITE} the existing save files inside the game folder: " & SavegamesLocTextBox.Text.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                                       "Backup restore",
                                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            'Store the selected backup subdirectory and truncate "Created at..." with .Substring(startIndex, lenght)
                            Dim BackupSubDir = BackupLocTextBox.Text & "\" & CustomMsgBox.BackupDirsDropdownCombo.SelectedItem.ToString().Substring(0, 13)
                            Dim SavegamesList As String() = Directory.GetFiles(BackupSubDir, "*.save")
                            For Each F As String In SavegamesList
                                Dim FileName As String = F.Substring(BackupSubDir.Length + 1)
                                File.Copy(Path.Combine(BackupSubDir, FileName), Path.Combine(SavegamesLocTextBox.Text, FileName), True)
                            Next

                            Log("[INFO] Backup from " & BackupSubDir & " restored.")
                            ShowAlert(64, "Backup successfully restored.")
                        Else
                            Log("[INFO] Restore process cancelled by the user.")
                        End If
                    Else
                        'If no directory contains any save files
                        ShowMsgBox("{\rtf1 The current {\b backup folder doesn't contain any backup.} Backup at least once and try again.}", "No backup found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        Log("[INFO] No valid backup found inside " & BackupLocTextBox.Text & ". Restore process aborted.")

                        LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
                        LatestBackupHelpLabel.Location = New Point(300, 22)
                    End If
                End If
            Else
                ShowMsgBox("{\rtf1 The current {\b backup folder is empty.} Backup at least once and try again.}", "No backup found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                Log("[INFO] No backup found inside " & BackupLocTextBox.Text & ". Restore process aborted.")

                LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
                LatestBackupHelpLabel.Location = New Point(300, 22)
            End If

        Catch ex As Exception
            Log("[ERROR] Could not restore the backup: " & ex.Message())
            ShowMsgBox("{\rtf1 The restore process has been {\b interrupted due to an error.} Please check the logs for more details.}", "Restore failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

        Finally
            'Empty subdirectories list to avoid adding duplicates in the next restore process
            CustomMsgBox.BackupDirsDropdownCombo.Items.Clear()
            'Empty backup directories list to avoid ArgumentOutOfRangeException when attempting to restore a backup from an empty directory (or directory with no valid backup) that previously contained valid backups
            BackupDirs.Clear()
        End Try
    End Sub

    Private Sub DetectBackupTimestamp_DoWork(sender As Object, e As DoWorkEventArgs) Handles DetectBackupTimestamp.DoWork
        'Detect and display the latest backup timestamp
        Try
            'Loop through every directory in the current backup directory
            For Each BackupDir In Directory.EnumerateDirectories(BackupLocTextBox.Text)
                'Get every save file inside each subdirectory
                Dim SavegamesList As String() = Directory.GetFiles(BackupDir, "*.save")
                If SavegamesList.Length > 0 Then
                    'If a subdirectory contains save files add it to the list
                    BackupDirs.Add(BackupDir)
                End If
            Next

            'If the current backup directory contains at least one valid backup
            If BackupDirs.Count >= 1 Then
                'Store the timestamp of the last directory in the list
                e.Result = Directory.GetCreationTime(BackupDirs.Item(BackupDirs.Count - 1)).ToString("yyyy-MM-dd HH:mm")
            Else
                'No valid directory found, set Result to Nothing
                e.Result = Nothing
            End If
        Catch ex As Exception
            'Store the exception message
            e.Result = ex.Message()
        Finally
            'Empty backup directories list
            BackupDirs.Clear()
        End Try
    End Sub

    Private Sub DetectBackupTimestamp_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles DetectBackupTimestamp.RunWorkerCompleted
        'An hack to match a valid timestamp because I couldn't get Regex.Match() to work
        '//stackoverflow.com/a/29062802
        If e.Result() <> Nothing AndAlso (Mid(e.Result().ToString(), 14, 1) = ":" And Mid(e.Result().ToString(), 5, 1) = "-" And Mid(e.Result().ToString(), 8, 1) = "-") Then
            LatestBackupHelpLabel.Text = "Latest backup:" & Environment.NewLine & e.Result()
            LatestBackupHelpLabel.Location = New Point(300, 14)
        ElseIf e.Result() Is Nothing Then
            Log("[INFO] No valid backup found inside the current backup directory.")

            LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
            LatestBackupHelpLabel.Location = New Point(300, 22)
        Else
            Log("[ERROR] An error occurred while enumerating backup directories: " & e.Result())
            ShowAlert(48, "Unable to get latest backup timestamp. Please check the logs for more details.")

            LatestBackupHelpLabel.Text = "Latest backup: Error."
            LatestBackupHelpLabel.Location = New Point(300, 22)
        End If
    End Sub

    Private Sub Updater_DownloadStringCompleted(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
        If e.Error Is Nothing Then
            Dim FetchedVer As Short = e.Result

            'Compare downloaded GHOST Buster version number with the current one
            If FetchedVer = VersionCode Then
                Log("[INFO] GHOST Buster is up to date.")
            ElseIf FetchedVer > VersionCode Then
                Log("[INFO] New version of GHOST Buster is available.")
                ShowMsgBox("{\rtf1 A newer version of GHOST Buster is available. Do you want to {\b visit the download page} now?}", "Update available", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If CustomMsgBox.DialogResult = DialogResult.Yes Then
                    Process.Start("https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest")
                End If
            ElseIf FetchedVer < VersionCode Then
                Log("[INFO] The version in use is greater than the one currently available.")
            End If
        Else
            Log("[ERROR] Unable to check for updates: " & e.Error.Message())
            ShowAlert(48, "Unable to check for updates. Please check the logs for more details.")
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Migrate settings from the old version
        UpgradeSettings()

        'Load settings and set defaults
        SavegamesLocTextBox.Text = My.Settings.GameSavesDir
        BackupLocTextBox.Text = My.Settings.BackupDir
        BackupFreqUpDown.Value = My.Settings.BackupInterval
        ConfirmExitChkBox.Checked = My.Settings.ConfirmExit
        ConfirmStopBackupChkBox.Checked = My.Settings.ConfirmBackupInterruption
        CheckUpdatesChkBox.Checked = My.Settings.CheckUpdates
        SettingsWriteLogToFileChkBox.Checked = My.Settings.WriteLogFile
        RememberFormPositionChkBox.Checked = My.Settings.RememberFormPosition
        If My.Settings.LogFilePath = "" Then
            SettingsLogFilePathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\event.log"
        Else
            SettingsLogFilePathTextBox.Text = My.Settings.LogFilePath
        End If
        DisableCloudSyncChkBox.Checked = My.Settings.DisableCloudSync
        WhichBackupDropdownCombo.SelectedIndex = My.Settings.WhichBackup
        SettingsNonUplayVersionChkBox.Checked = My.Settings.NoUplay
        SettingsCustomExeTextBox.Text = My.Settings.CustomExeLoc

        'Set window position
        LoadFormPosition()

        'Start logging session
        Log("[LOG SESSION] -------------------- START --------------------")
        Log("[INFO] GHOST Buster version: " & Version)

#If DEBUG Then
        Log("[INFO] Environment is DEVELOPMENT")
#Else
        Log("[INFO] Environment is PRODUCTION")
#End If

        'Retrieve Wildlands install directory
        If SettingsNonUplayVersionChkBox.Checked = True Then
            If File.Exists(SettingsCustomExeTextBox.Text) Then
                GamePath = Directory.GetParent(SettingsCustomExeTextBox.Text).ToString() & "\"
                PlayGameBtn.Enabled = True
                Log("[INFO] Wildlands is installed in: " & GamePath & " (Non-Uplay version).")
                ProcessCheckTimer.Interval = 500
                ProcessCheckTimer.Start()
            Else
                'Disable "I'm not using the Uplay version of Wildlands"
                SettingsNonUplayVersionChkBox.Checked = False
                PlayGameBtn.Text = "Ghost Recon Wildlands not found"
                Log("[WARNING] Custom Wildlands executable " & SettingsCustomExeTextBox.Text & " not found.")
                ShowAlert(48, "The selected Wildlands executable could not be found.")
            End If
        Else
            Using GameRegKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher\Installs\1771", False)
                Try
                    GamePath = TryCast(GameRegKey.GetValue("InstallDir"), String).Replace("/", "\") 'Replace any forward slashes with backward slashes

                    If GamePath <> Nothing Then
                        PlayGameBtn.Enabled = True
                        Log("[INFO] Wildlands is installed in: " & GamePath)
                        ProcessCheckTimer.Interval = 500
                        ProcessCheckTimer.Start()
                    Else
                        PlayGameBtn.Text = "Ghost Recon Wildlands is not installed"
                        Log("[WARNING] Wildlands is not installed (""InstallDir"" is Null or Empty).")
                    End If

                Catch ex As Exception
                    PlayGameBtn.Text = "Ghost Recon Wildlands is not installed"
                    Log("[ERROR] Wildlands is not installed: " & ex.Message())
                End Try
            End Using
        End If

        'Retrieve Uplay install directory
        Using UplayRegKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Ubisoft\Launcher", False)
            Try
                UplayPath = UplayRegKey.GetValue("InstallDir")

                If UplayPath <> Nothing Then
                    Log("[INFO] Uplay is installed in: " & UplayPath)
                Else
                    Log("[WARNING] Uplay is not installed (""InstallDir"" is Null or Empty). Uplay is required to launch and play Wildlands.")
                End If

            Catch ex As Exception
                Log("[ERROR] Uplay is not installed: " & ex.Message().TrimEnd(".") & "." & " Uplay is required to launch and play Wildlands.")
            End Try
        End Using

        'Check if save games directory exists
        If SavegamesLocTextBox.Text <> "" AndAlso Not Directory.Exists(SavegamesLocTextBox.Text) Then
            Log("[WARNING] Wildlands save games folder " & SavegamesLocTextBox.Text & " no longer exists.")
            ShowAlert(48, "Wildlands save games folder no longer exists.")
            SavegamesLocTextBox.Text = ""
        End If

        'Check if backup directory exists
        If BackupLocTextBox.Text <> "" AndAlso Not Directory.Exists(BackupLocTextBox.Text) Then
            Log("[WARNING] Backup folder " & BackupLocTextBox.Text & " no longer exists.")
            ShowAlert(48, "Backup folder no longer exists.")
            BackupLocTextBox.Text = ""
        End If

        'Detect latest backup timestamp
        If BackupLocTextBox.Text <> "" Then
            LatestBackupHelpLabel.Text = "Latest backup: " & Environment.NewLine & "Please wait..."
            LatestBackupHelpLabel.Location = New Point(300, 14)
            DetectBackupTimestamp.RunWorkerAsync()
        End If

        'Check for updates
        '//docs.microsoft.com/en-us/dotnet/api/system.net.downloadstringcompletedeventargs
        If CheckUpdatesChkBox.Checked = True Then
            Using Updater As New WebClient
                Updater.Headers.Add("User-Agent", "GHOST Buster (+https://strappazzon.xyz/GRW-GHOST-Buster)")
                Dim VersionURI As New Uri("https://raw.githubusercontent.com/Strappazzon/GRW-GHOST-Buster/master/version")
                Updater.DownloadStringAsync(VersionURI)
                'Call updater_DownloadStringCompleted when the download completes
                AddHandler Updater.DownloadStringCompleted, AddressOf Updater_DownloadStringCompleted
            End Using
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If IsBackupRunning = True And ConfirmExitChkBox.Checked = True Then
            ShowMsgBox("{\rtf1 The backup process is still running. Do you want to {\b interrupt it and exit?}}", "Confirm exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If CustomMsgBox.DialogResult = DialogResult.No OrElse CustomMsgBox.DialogResult = DialogResult.Cancel Then
                e.Cancel = True
            Else
                SaveSettings()
            End If
        Else
            SaveSettings()
        End If
    End Sub

    Private Sub ProcessCheckTimer_Tick(sender As Object, e As EventArgs) Handles ProcessCheckTimer.Tick
        Dim WildlandsProc = Process.GetProcessesByName("GRW")
        If WildlandsProc.Count > 0 Then
            IsGameRunning = True
            PlayGameBtn.Enabled = False
        Else
            IsGameRunning = False
            PlayGameBtn.Enabled = True
            If IsBackupRunning = True Then
                StopBackup()
                Log("[INFO] Wildlands has been closed or crashed. Backup interrupted.")
                ShowMsgBox("{\rtf1 Wildlands {\b has been closed or crashed}, as a result the backup process has been interrupted.}", "Wildlands is no longer running", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            End If
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
            ShowAlert(64, "Uplay is not installed.")
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
                Log("[INFO] Save games directory set to: " & O.SelectedPath)
            End If
        End Using
    End Sub

    Private Sub ExploreSavegamesLocBtn_Click(sender As Object, e As EventArgs) Handles ExploreSavegamesLocBtn.Click
        'Open the save games directory in Windows Explorer
        If SavegamesLocTextBox.Text <> "" AndAlso Directory.Exists(SavegamesLocTextBox.Text) Then
            Process.Start("explorer.exe", SavegamesLocTextBox.Text)
        Else
            ShowAlert(64, "Wildlands save games folder no loger exists.")
        End If
    End Sub

    Private Sub BrowseBackupLocBtn_Click(sender As Object, e As EventArgs) Handles BrowseBackupLocBtn.Click
        'Choose backup directory
        Using O As New FolderBrowserDialog
            O.Description = "Select where you want to backup your save files to. Every backup will create a new ""yyyyMMdd HHmm"" subfolder."
            If O.ShowDialog = DialogResult.OK Then
                BackupLocTextBox.Text = O.SelectedPath
                Log("[INFO] Backup directory set to: " & O.SelectedPath)

                'Detect latest backup timestamp
                LatestBackupHelpLabel.Text = "Latest backup: " & Environment.NewLine & "Please wait..."
                LatestBackupHelpLabel.Location = New Point(300, 14)
                DetectBackupTimestamp.RunWorkerAsync()
            End If
        End Using
    End Sub

    Private Sub ExploreBackupLocBtn_Click(sender As Object, e As EventArgs) Handles ExploreBackupLocBtn.Click
        'Open backup directory in Windows Explorer
        If BackupLocTextBox.Text <> "" AndAlso Directory.Exists(BackupLocTextBox.Text) Then
            Process.Start("explorer.exe", BackupLocTextBox.Text)
        Else
            ShowAlert(64, "Backup folder no longer exists.")
        End If
    End Sub

    Private Sub BackupBtn_Click(sender As Object, e As EventArgs) Handles BackupBtn.Click
        If SavegamesLocTextBox.Text = "" Or BackupLocTextBox.Text = "" Then
            ShowAlert(64, "You must specify both save games and backup folders.")
        ElseIf IsGameRunning = True Then
            StartBackup()

            'Perform the first backup
            Try
                'Store latest backup timestamp and subdirectory
                Dim BackupTimestamp As Date = Now
                Dim BackupDirectory As String = BackupLocTextBox.Text & BackupTimestamp.ToString("\\yyyyMMdd HHmm")

                Dim SavegamesList As String() = Directory.GetFiles(SavegamesLocTextBox.Text, "*.save")
                For Each F As String In SavegamesList
                    If Not Directory.Exists(BackupDirectory) Then
                        Directory.CreateDirectory(BackupDirectory)
                    End If
                    Dim FileName As String = F.Substring(SavegamesLocTextBox.Text.Length + 1)
                    File.Copy(Path.Combine(SavegamesLocTextBox.Text, FileName), Path.Combine(BackupDirectory, FileName), True)
                Next

                'Write the timestamp of this backup on the main screen
                LatestBackupHelpLabel.Text = "Latest backup:" & Environment.NewLine & BackupTimestamp.ToString("yyyy-MM-dd HH:mm")
                LatestBackupHelpLabel.Location = New Point(300, 14)

                Log("[INFO] Performed the first backup " & "(" & SavegamesList.Length & " files copied to " & BackupDirectory & ").")

            Catch ex As Exception
                StopBackup()
                Log("[ERROR] Backup interrupted: " & ex.Message())
                ShowMsgBox("{\rtf1 The backup process has been {\b interrupted due to an error.} Please check the logs for more details.}", "Backup interrupted", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
            End Try
        ElseIf IsGameRunning = False Then
            ShowAlert(64, "You must launch Wildlands before starting the backup process.")
        End If
    End Sub

    Private Sub BackupTimer_Tick(sender As Object, e As EventArgs) Handles BackupTimer.Tick
        If IsGameRunning = True Then
            Try
                'Store latest backup timestamp and subdirectory
                Dim BackupTimestamp As Date = Now
                Dim BackupDirectory As String = BackupLocTextBox.Text & BackupTimestamp.ToString("\\yyyyMMdd HHmm")

                Dim SavegamesList As String() = Directory.GetFiles(SavegamesLocTextBox.Text, "*.save")
                For Each F As String In SavegamesList
                    If Not Directory.Exists(BackupDirectory) Then
                        Directory.CreateDirectory(BackupDirectory)
                    End If
                    Dim FileName As String = F.Substring(SavegamesLocTextBox.Text.Length + 1)
                    File.Copy(Path.Combine(SavegamesLocTextBox.Text, FileName), Path.Combine(BackupDirectory, FileName), True)
                Next

                'Write the timestamp of this backup on the main screen
                LatestBackupHelpLabel.Text = "Latest backup:" & Environment.NewLine & BackupTimestamp.ToString("yyyy-MM-dd HH:mm")
                LatestBackupHelpLabel.Location = New Point(300, 14)

                Log("[INFO] Backup complete " & "(" & SavegamesList.Length & " files copied to " & BackupDirectory & ").")

            Catch ex As Exception
                StopBackup()
                Log("[ERROR] Backup interrupted: " & ex.Message())
                ShowMsgBox("{\rtf1 The backup process has been {\b interrupted due to an error.} Please check the logs for more details.}", "Backup interrupted", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
            End Try
        Else
            StopBackup()
            Log("[INFO] Wildlands closed or crashed, Backup interrupted.")
            ShowMsgBox("{\rtf1 Wildlands {\b has been closed or crashed}, as a result the backup process has been interrupted.}", "Wildlands is no longer running", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub StopBtn_Click(sender As Object, e As EventArgs) Handles StopBtn.Click
        If ConfirmStopBackupChkBox.Checked = True Then
            ShowMsgBox("{\rtf1 Are you sure you want to {\b interrupt the backup process?}}", "Backup interruption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If CustomMsgBox.DialogResult = DialogResult.Yes Then
                StopBackup()
                Log("[INFO] Backup interrupted by the user.")
            End If
        Else
            StopBackup()
            Log("[INFO] Backup interrupted by the user.")
        End If
    End Sub

    Private Sub RestoreBtn_Click(sender As Object, e As EventArgs) Handles RestoreBtn.Click
        If SavegamesLocTextBox.Text = "" Or BackupLocTextBox.Text = "" Then
            ShowAlert(64, "You must specify both save games and backup folders.")
        ElseIf IsGameRunning = True Then
            ShowAlert(64, "You must quit Wildlands before restoring a backup.")
        ElseIf IsGameRunning = False And DisableCloudSyncChkBox.Checked = True Then
            'If the game is not running and "Let GHOST Buster disable cloud save synchronization" is checked
            'Check if Uplay is running or not before editing its settings file
            Dim UplayProc = Process.GetProcessesByName("upc")
            If UplayProc.Count > 0 Then
                ShowMsgBox("{\rtf1 You must {\b quit Uplay before restoring a backup} because you chose to let GHOST Buster disable cloud save synchronization for you.}", "Cannot restore", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            Else
                'Disable Uplay cloud save synchronization
                Try
                    Dim UplayYAMLPath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Ubisoft Game Launcher\settings.yml"
                    Log("[INFO] Parsing and evaluating Uplay settings file: " & UplayYAMLPath)
                    Dim ParsedUplayYAML As String = File.ReadAllText(UplayYAMLPath)

                    If ParsedUplayYAML.Contains("syncsavegames: true") Then
                        'Backup Uplay settings file
                        Log("[INFO] Backing up Uplay settings file to " & UplayYAMLPath & ".bak")
                        File.Copy(UplayYAMLPath, UplayYAMLPath & ".bak", False) 'Don't overwrite the backup file in the future

                        'Set syncsavegames to false (Disable cloud save sync)
                        Dim ReplacedUplayYAML As String = ParsedUplayYAML.Replace("syncsavegames: true", "syncsavegames: false")
                        File.WriteAllText(UplayYAMLPath, ReplacedUplayYAML)
                        Log("[INFO] Uplay cloud save synchronization disabled.")

                        'Launch Uplay again...
                        If UplayPath <> Nothing Then
                            Process.Start(UplayPath & "Uplay.exe")
                        End If

                        '...and restore the backup
                        RestoreBackup()
                    ElseIf ParsedUplayYAML.Contains("syncsavegames: false") Then
                        'Don't replace anything if syncsavegames is already set to false
                        Log("[INFO] Uplay cloud synchronization is already disabled.")

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
                    Log("[ERROR] Parsing of ""settings.yml"" failed: " & ex.Message())
                    ShowMsgBox("{\rtf1 ""Let GHOST Buster disable cloud save synchronization"" setting has been {\b disabled because an error occurred} while trying to parse Uplay settings file." _
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
                Log("[INFO] Log exported to " & S.FileName)
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
                Log("[INFO] Log file path set to: " & SettingsLogFilePathTextBox.Text)
            End If
        End Using
    End Sub

    Private Sub SettingsOpenLogBtn_Click(sender As Object, e As EventArgs) Handles SettingsOpenLogBtn.Click
        'Open log file with the default text editor
        If SettingsLogFilePathTextBox.Text <> "" AndAlso File.Exists(SettingsLogFilePathTextBox.Text) Then
            Process.Start(SettingsLogFilePathTextBox.Text)
        Else
            ShowAlert(64, "The event log file does not exist.")
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
                Log("[INFO] Custom Wildlands executable set: " & O.FileName)
            End If
        End Using
    End Sub

    Private Sub SettingsOpenCustomExeFolderBtn_Click(sender As Object, e As EventArgs) Handles SettingsOpenCustomExeFolderBtn.Click
        'Open custom Wildlands location in Windows Explorer if it exists
        If SettingsCustomExeTextBox.Text <> "" AndAlso Directory.Exists(Directory.GetParent(SettingsCustomExeTextBox.Text).ToString()) Then
            Process.Start(Directory.GetParent(SettingsCustomExeTextBox.Text).ToString())
        Else
            ShowAlert(64, "The current folder does not exist.")
        End If
    End Sub
End Class

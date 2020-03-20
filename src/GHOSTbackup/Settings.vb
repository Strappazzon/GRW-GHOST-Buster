Public Class Settings
    Public Shared Sub UpgradeSettings()
        'Migrate settings to the new version
        'Unfortunately, settings migrate only if the new version is installed in the same directory as the old version
        '//bytes.com/topic/visual-basic-net/answers/854235-my-settings-upgrade-doesnt-upgrade#post3426232
        If My.Settings.MustUpgrade = True Then
            My.Settings.Upgrade()
            My.Settings.MustUpgrade = False
        End If
    End Sub

    Public Shared Sub LoadSettings()
        'Load settings and set defaults
        Form1.SavegamesLocTextBox.Text = My.Settings.GameSavesDir
        Form1.BackupLocTextBox.Text = My.Settings.BackupDir
        Form1.BackupFreqUpDown.Value = My.Settings.BackupInterval
        Form1.ConfirmExitChkBox.Checked = My.Settings.ConfirmExit
        Form1.ConfirmStopBackupChkBox.Checked = My.Settings.ConfirmBackupInterruption
        Form1.CheckUpdatesChkBox.Checked = My.Settings.CheckUpdates
        Form1.SettingsWriteLogToFileChkBox.Checked = My.Settings.WriteLogFile
        Form1.RememberFormPositionChkBox.Checked = My.Settings.RememberFormPosition
        If My.Settings.LogFilePath = "" Then
            Form1.SettingsLogFilePathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\event.log"
        Else
            Form1.SettingsLogFilePathTextBox.Text = My.Settings.LogFilePath
        End If
        Form1.DisableCloudSyncChkBox.Checked = My.Settings.DisableCloudSync
        Form1.WhichBackupDropdownCombo.SelectedIndex = My.Settings.WhichBackup
        Form1.SettingsNonUplayVersionChkBox.Checked = My.Settings.NoUplay
        Form1.SettingsCustomExeTextBox.Text = My.Settings.CustomExeLoc
    End Sub

    Public Shared Sub SaveSettings()
        'Confirm exit (if backup is active)
        If Form1.ConfirmExitChkBox.CheckState <> My.Settings.ConfirmExit Then
            My.Settings.ConfirmExit = Form1.ConfirmExitChkBox.CheckState
        End If

        'Confirm backup interruption
        If Form1.ConfirmStopBackupChkBox.CheckState <> My.Settings.ConfirmBackupInterruption Then
            My.Settings.ConfirmBackupInterruption = Form1.ConfirmStopBackupChkBox.CheckState
        End If

        'Disable Uplay cloud save sync
        If Form1.DisableCloudSyncChkBox.CheckState <> My.Settings.DisableCloudSync Then
            My.Settings.DisableCloudSync = Form1.DisableCloudSyncChkBox.CheckState
        End If

        'Check for updates
        If Form1.CheckUpdatesChkBox.CheckState <> My.Settings.CheckUpdates Then
            My.Settings.CheckUpdates = Form1.CheckUpdatesChkBox.CheckState
        End If

        'Remember form position, Window location
        If Form1.RememberFormPositionChkBox.CheckState <> My.Settings.RememberFormPosition Then
            My.Settings.RememberFormPosition = Form1.RememberFormPositionChkBox.CheckState
            My.Settings.WindowLocation = Form1.Location
        End If

        'Backup frequency
        If Form1.BackupFreqUpDown.Value <> My.Settings.BackupInterval Then
            My.Settings.BackupInterval = Form1.BackupFreqUpDown.Value
        End If

        'Choose which backup will be restored
        If Form1.WhichBackupDropdownCombo.SelectedIndex <> My.Settings.WhichBackup Then
            My.Settings.WhichBackup = Form1.WhichBackupDropdownCombo.SelectedIndex
        End If

        'Wildlands save games folder
        If Form1.SavegamesLocTextBox.Text <> My.Settings.GameSavesDir Then
            My.Settings.GameSavesDir = Form1.SavegamesLocTextBox.Text
        End If

        'Backup folder
        If Form1.BackupLocTextBox.Text <> My.Settings.BackupDir Then
            My.Settings.BackupDir = Form1.BackupLocTextBox.Text
        End If

        'Write events to a log file
        If Form1.SettingsWriteLogToFileChkBox.CheckState <> My.Settings.WriteLogFile Then
            My.Settings.WriteLogFile = Form1.SettingsWriteLogToFileChkBox.CheckState
            My.Settings.LogFilePath = Form1.SettingsLogFilePathTextBox.Text
        End If

        'Log file location
        If Form1.SettingsLogFilePathTextBox.Text <> My.Settings.LogFilePath Then
            My.Settings.LogFilePath = Form1.SettingsLogFilePathTextBox.Text
        End If

        'I'm not using the Uplay version of Wildlands, Custom Wildlands executable location
        'If Wildlands executable location is empty don't save these settings
        If Form1.SettingsNonUplayVersionChkBox.CheckState <> My.Settings.NoUplay AndAlso Form1.SettingsCustomExeTextBox.Text <> "" Then
            My.Settings.NoUplay = Form1.SettingsNonUplayVersionChkBox.CheckState
            My.Settings.CustomExeLoc = Form1.SettingsCustomExeTextBox.Text
        End If

        Logger.Log("[INFO] Settings saved.")
    End Sub
End Class

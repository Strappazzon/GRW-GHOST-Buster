#Region "Copyright (c) 2019 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster"
''
'' GHOST Buster - Ghost Recon Wildlands backup utility
''
'' Copyright (c) 2019 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster
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

Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports GHOSTbackup.ProcessHelper
Imports GHOSTbackup.UI

Public Class BackupHelper
    Private Shared WithEvents DetectBackupTimestamp As New BackgroundWorker()
    Private Shared WithEvents RetrieveBackups As New BackgroundWorker()
    Private Shared WithEvents BackupTimer As New Timer()
    Public Shared ReadOnly BackupDirs As New List(Of String)
    Private Shared Property ErrorMessage As String = Nothing
    Public Shared Property IsBackupRunning As Boolean = False

    Public Shared Sub DetectLatestBackup()
        'Pass the backup folder path to the background worker
        '//stackoverflow.com/a/4807200
        DetectBackupTimestamp.RunWorkerAsync(Form1.BackupLocTextBox.Text)
    End Sub

    Public Shared Sub PopulateBackupsGrid()
        'Pass the backup folder path to the background worker
        '//stackoverflow.com/a/4807200
        RetrieveBackups.RunWorkerAsync(Form1.BackupLocTextBox.Text)
    End Sub

#Region "Async Subroutines"
    Private Shared Sub DetectBackupTimestamp_DoWork(sender As Object, e As DoWorkEventArgs) Handles DetectBackupTimestamp.DoWork
        'Detect and display the latest backup timestamp
        Try
            'Loop through every directory in the current backup directory
            For Each BackupDir In Directory.EnumerateDirectories(e.Argument())
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
                e.Result = Directory.GetCreationTime(BackupDirs.Item(BackupDirs.Count - 1)).ToString("G", CultureInfo.CurrentUICulture)
            Else
                'No valid directory found, set Result to Nothing
                e.Result = Nothing
            End If
        Catch ex As Exception
            ErrorMessage = ex.Message()
        Finally
            'Empty backup directories list
            BackupDirs.Clear()
        End Try
    End Sub

    Private Shared Sub DetectBackupTimestamp_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles DetectBackupTimestamp.RunWorkerCompleted
        If ErrorMessage = Nothing Then
            If e.Result = Nothing Then
                Logger.Log("[INFO] No valid backup found inside the current backup directory.")

                Form1.LatestBackupHelpLabel.Text = Localization.GetString("ui_tasks_latest_none")
            Else
                Form1.LatestBackupHelpLabel.Text = Localization.GetString("ui_tasks_latest") & e.Result()
            End If
        Else
            Logger.Log("[ERROR] An error occurred while enumerating backup directories: " & ErrorMessage)
            Banner.Show(Localization.GetString("banner_timestamp_error"), BannerIcon.Warning)

            Form1.LatestBackupHelpLabel.Text = Localization.GetString("ui_tasks_latest_error")

            ErrorMessage = Nothing
        End If
    End Sub

    Private Shared Sub RetrieveBackups_DoWork(sender As Object, e As DoWorkEventArgs) Handles RetrieveBackups.DoWork
        Try
            'Create backups table
            '//exceptionshub.com/how-to-create-a-datatable-in-c-and-how-to-add-rows.html
            Using DT As DataTable = New DataTable()
                'Add columns
                'Folder name, Size, Creation date
                DT.Columns.Add(Localization.GetString("ui_manage_table_column_folder"))
                DT.Columns.Add(String.Format(Localization.GetString("ui_manage_table_column_size"), "MB"))
                DT.Columns.Add(Localization.GetString("ui_manage_table_column_timestamp"))

                'Loop through every directory in the current backup directory
                For Each BackupDir In Directory.EnumerateDirectories(e.Argument())
                    'Get every save file inside each subdirectory
                    Dim SavegamesList As String() = Directory.GetFiles(BackupDir, "*.save")
                    If SavegamesList.Length > 0 Then
                        'If a subdirectory contains save files add it to the table
                        'Create a new row
                        Dim DR As DataRow = DT.NewRow()
                        'If a subdirectory contains save files add it to the table
                        'Add only the directory name, not the full path
                        DR(0) = BackupDir.Substring(BackupDir.LastIndexOf(Path.DirectorySeparatorChar) + 1)
                        'Get backup folder size
                        'Sum bytes of each save file because the .NET Framework doesn't provide a method to calculate a directory size
                        Dim L As Long = 0
                        For Each F In SavegamesList
                            Dim FI As FileInfo = New FileInfo(F)
                            L += FI.Length()
                        Next
                        'Add calculated folder size (MB)
                        'Preserve trailing zeroes
                        '//stackoverflow.com/a/47066466
                        DR(1) = Math.Round(L / 1048576, 2).ToString("0.00")
                        'Add folder creation timestamp
                        DR(2) = Directory.GetCreationTime(BackupDir).ToString("f", CultureInfo.CurrentUICulture)
                        'Add row with folder name, size and timestamp to the table
                        DT.Rows.Add(DR)
                    End If
                Next

                'Return the table
                e.Result = DT
            End Using

        Catch ex As Exception
            ErrorMessage = ex.Message()
        End Try
    End Sub

    Private Shared Sub RetrieveBackups_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles RetrieveBackups.RunWorkerCompleted
        If ErrorMessage = Nothing Then
            Form1.BackupsDataGrid.DataSource = e.Result()
            'Sort backups (Most recent first)
            '//docs.microsoft.com/en-us/dotnet/api/system.windows.forms.datagridview.sortorder
            Form1.BackupsDataGrid.Sort(Form1.BackupsDataGrid.Columns(0), ListSortDirection.Descending)
        Else
            Logger.Log("[ERROR] An error occurred while retrieving backups: " & ErrorMessage)
            Banner.Show(Localization.GetString("banner_backups_table_retrieve_error"), BannerIcon.Warning)

            ErrorMessage = Nothing
        End If
    End Sub
#End Region

#Region "Backup"
    Public Shared Sub StartBackup()
        BackupTimer.Interval = Form1.BackupFreqUpDown.Value * 60000
        BackupTimer.Start()
        IsBackupRunning = True
        Form1.BackupFreqUpDown.Enabled = False
        Form1.BackupBtn.Enabled = False
        Form1.StopBtn.Enabled = True
        Form1.RestoreBtn.Enabled = False
        Form1.SavegamesLocTextBox.Enabled = False
        Form1.BrowseSavegamesLocBtn.Enabled = False
        Form1.BackupLocTextBox.Enabled = False
        Form1.BrowseBackupLocBtn.Enabled = False
        Form1.RestoreManageContextMenuItem.Enabled = False
        Form1.SettingsNonUplayVersionChkBox.Enabled = False
        Form1.SettingsCustomExeTextBox.Enabled = False
        Form1.SettingsBrowseCustomExeBtn.Enabled = False
        Form1.SettingsOpenCustomExeFolderBtn.Enabled = False
    End Sub

    Public Shared Sub StopBackup()
        BackupTimer.Stop()
        IsBackupRunning = False
        Form1.BackupFreqUpDown.Enabled = True
        Form1.BackupBtn.Enabled = True
        Form1.StopBtn.Enabled = False
        Form1.RestoreBtn.Enabled = True
        Form1.SavegamesLocTextBox.Enabled = True
        Form1.BrowseSavegamesLocBtn.Enabled = True
        Form1.BackupLocTextBox.Enabled = True
        Form1.BrowseBackupLocBtn.Enabled = True
        Form1.RestoreManageContextMenuItem.Enabled = True
        Form1.SettingsNonUplayVersionChkBox.Enabled = True
        Form1.SettingsCustomExeTextBox.Enabled = True
        Form1.SettingsBrowseCustomExeBtn.Enabled = True
        Form1.SettingsOpenCustomExeFolderBtn.Enabled = True
    End Sub

    Public Shared Sub PerformFirstBackup()
        If Form1.SavegamesLocTextBox.Text = "" OrElse Form1.BackupLocTextBox.Text = "" Then
            Banner.Show(Localization.GetString("banner_specify_folders_info"), BannerIcon.Information)
        Else
            If IsGameRunning = True Then
                StartBackup()

                'Perform the first backup
                Try
                    'Store latest backup timestamp and subdirectory
                    Dim BackupTimestamp As Date = Now
                    Dim BackupDirectory As String = Form1.BackupLocTextBox.Text & BackupTimestamp.ToString("\\yyyyMMdd HHmm")

                    Dim SavegamesList As String() = Directory.GetFiles(Form1.SavegamesLocTextBox.Text, "*.save")
                    For Each F In SavegamesList
                        'Create backup directory
                        If Not Directory.Exists(BackupDirectory) Then
                            Directory.CreateDirectory(BackupDirectory)
                        End If
                        'Copy each save file
                        Dim FileName As String = F.Substring(Form1.SavegamesLocTextBox.Text.Length + 1)
                        File.Copy(Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), Path.Combine(BackupDirectory, FileName), True)
                    Next

                    'Write the timestamp of this backup on the main screen
                    Form1.LatestBackupHelpLabel.Text = Localization.GetString("ui_tasks_latest") & BackupTimestamp.ToString("G", CultureInfo.CurrentUICulture)

                    Logger.Log("[INFO] Performed the first backup " & "(" & SavegamesList.Length & " files copied to " & BackupDirectory & ").")
                    If Form1.SettingsDisplayNotificationChkBox.Checked = True Then
                        Notification.Show(Localization.GetString("notification_msg_first_backup"))
                    End If

                Catch ex As Exception
                    StopBackup()
                    Logger.Log("[ERROR] Backup interrupted: " & ex.Message())
                    If Form1.SettingsDisplayNotificationChkBox.Checked = True Then
                        Notification.Show(Localization.GetString("notification_msg_backup_error"))
                    End If
                    CustomMsgBox.Show(
                        Localization.GetString("msgbox_backup_error"),
                        Localization.GetString("msgbox_backup_error_title"),
                        CustomMsgBoxButtons.OKCancel, CustomMsgBoxIcon.Error
                    )
                End Try
            Else
                Banner.Show(Localization.GetString("banner_launch_before_backup_info"), BannerIcon.Information)
            End If
        End If
    End Sub

    Private Shared Sub BackupTimer_Tick(sender As Object, e As EventArgs) Handles BackupTimer.Tick
        If IsGameRunning = True Then
            Try
                'Store latest backup timestamp and subdirectory
                Dim BackupTimestamp As Date = Now
                Dim BackupDirectory As String = Form1.BackupLocTextBox.Text & BackupTimestamp.ToString("\\yyyyMMdd HHmm")

                Dim SavegamesList As String() = Directory.GetFiles(Form1.SavegamesLocTextBox.Text, "*.save")
                For Each F In SavegamesList
                    'Create backup directory
                    If Not Directory.Exists(BackupDirectory) Then
                        Directory.CreateDirectory(BackupDirectory)
                    End If
                    'Copy each save file
                    Dim FileName As String = F.Substring(Form1.SavegamesLocTextBox.Text.Length + 1)
                    File.Copy(Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), Path.Combine(BackupDirectory, FileName), True)
                Next

                'Write the timestamp of this backup on the main screen
                Form1.LatestBackupHelpLabel.Text = Localization.GetString("ui_tasks_latest") & BackupTimestamp.ToString("G", CultureInfo.CurrentUICulture)

                Logger.Log("[INFO] Backup complete " & "(" & SavegamesList.Length & " files copied to " & BackupDirectory & ").")
                If Form1.SettingsDisplayNotificationChkBox.Checked = True Then
                    Notification.Show(Localization.GetString("notification_msg_backup_complete"))
                End If

            Catch ex As Exception
                StopBackup()
                Logger.Log("[ERROR] Backup interrupted: " & ex.Message())
                If Form1.SettingsDisplayNotificationChkBox.Checked = True Then
                    Notification.Show(Localization.GetString("notification_msg_backup_error"))
                End If
                CustomMsgBox.Show(
                    Localization.GetString("msgbox_backup_error"),
                    Localization.GetString("msgbox_backup_error_title"),
                    CustomMsgBoxButtons.OKCancel,
                    CustomMsgBoxIcon.Error
                )
            End Try
        Else
            StopBackup()
            Logger.Log("[INFO] Wildlands closed or crashed, Backup interrupted.")
            CustomMsgBox.Show(
                Localization.GetString("msgbox_wildlands_closed_crashed"),
                Localization.GetString("msgbox_wildlands_closed_crashed_title"),
                CustomMsgBoxButtons.OKCancel,
                CustomMsgBoxIcon.Warning
            )
        End If
    End Sub
#End Region

#Region "Backup Restore"
    Public Shared Overloads Sub RestoreBackup()
        Logger.Log("[INFO] Restore process started.")

        Try
            'Get all directories inside the current backup folder
            '//docs.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratedirectories
            Dim EnumeratedDirs = New List(Of String)(Directory.EnumerateDirectories(Form1.BackupLocTextBox.Text))

            'If the current backup folder is not empty proceed with the restore process
            If EnumeratedDirs.Count > 0 Then
                If Form1.WhichBackupDropdown.SelectedIndex = 0 Then
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
                        CustomMsgBox.Show(
                            Strings.Format(Localization.GetString("msgbox_backup_restore"), BackupDirs.Item(BackupDirs.Count - 1).Replace("\", "\\"), Form1.SavegamesLocTextBox.Text.Replace("\", "\\")),
                            Localization.GetString("msgbox_backup_restore_title"),
                            CustomMsgBoxButtons.YesNoCancel,
                            CustomMsgBoxIcon.Warning,
                            CustomMsgBoxDefaultButton.Button2
                        )
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            Dim SavegamesList As String() = Directory.GetFiles(BackupDirs.Item(BackupDirs.Count - 1), "*.save")
                            For Each F In SavegamesList
                                Dim FileName As String = F.Substring(BackupDirs.Item(BackupDirs.Count - 1).Length + 1)
                                File.Copy(Path.Combine(BackupDirs.Item(BackupDirs.Count - 1), FileName), Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), True)
                            Next
                            Logger.Log("[INFO] Backup from " & BackupDirs.Item(BackupDirs.Count - 1) & " restored.")
                            Banner.Show(Localization.GetString("banner_backup_restored"), BannerIcon.Information)
                        Else
                            Logger.Log("[INFO] Restore process cancelled by the user.")
                        End If
                    Else
                        'If no directory contains any save files
                        CustomMsgBox.Show(
                            Localization.GetString("msgbox_invalid_backup_folder"),
                            Localization.GetString("msgbox_invalid_backup_folder_title"),
                            CustomMsgBoxButtons.OKCancel,
                            CustomMsgBoxIcon.Warning
                        )
                        Logger.Log("[INFO] No valid backup found inside " & Form1.BackupLocTextBox.Text & ". Restore process aborted.")

                        Form1.LatestBackupHelpLabel.Text = Localization.GetString("ui_tasks_latest_none")
                    End If
                Else
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
                        CustomMsgBox.Show(
                            Strings.Format(Localization.GetString("msgbox_backup_restore"), BackupDirs.Item(BackupDirs.Count - 2).Replace("\", "\\"), Form1.SavegamesLocTextBox.Text.Replace("\", "\\")),
                            Localization.GetString("msgbox_backup_restore_title"),
                            CustomMsgBoxButtons.YesNoCancel,
                            CustomMsgBoxIcon.Warning,
                            CustomMsgBoxDefaultButton.Button2
                        )
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            Dim SavegamesList As String() = Directory.GetFiles(BackupDirs.Item(BackupDirs.Count - 2), "*.save")
                            For Each F In SavegamesList
                                Dim FileName As String = F.Substring(BackupDirs.Item(BackupDirs.Count - 2).Length + 1)
                                File.Copy(Path.Combine(BackupDirs.Item(BackupDirs.Count - 2), FileName), Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), True)
                            Next

                            Logger.Log("[INFO] Backup from " & BackupDirs.Item(BackupDirs.Count - 2) & " restored.")
                            Banner.Show(Localization.GetString("banner_backup_restored"), BannerIcon.Information)
                        Else
                            Logger.Log("[INFO] Restore process cancelled by the user.")
                        End If
                    ElseIf BackupDirs.Count = 1 Then
                        'If only one valid backup directory exists ask the user to restore the latest backup instead
                        'Ask the user before restoring the latest backup
                        CustomMsgBox.Show(
                            Strings.Format(Localization.GetString("msgbox_backup_restore_no_sectolast"), BackupDirs.Item(BackupDirs.Count - 1).Replace("\", "\\"), Form1.SavegamesLocTextBox.Text.Replace("\", "\\")),
                            Localization.GetString("msgbox_backup_404_title"),
                            CustomMsgBoxButtons.YesNoCancel,
                            CustomMsgBoxIcon.Warning,
                            CustomMsgBoxDefaultButton.Button2
                        )
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            Dim SavegamesList As String() = Directory.GetFiles(BackupDirs.Item(BackupDirs.Count - 1), "*.save")
                            For Each F In SavegamesList
                                Dim FileName As String = F.Substring(BackupDirs.Item(BackupDirs.Count - 1).Length + 1)
                                File.Copy(Path.Combine(BackupDirs.Item(BackupDirs.Count - 1), FileName), Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), True)
                            Next

                            Logger.Log("[INFO] Backup from " & BackupDirs.Item(BackupDirs.Count - 1) & " restored.")
                            Banner.Show(Localization.GetString("banner_backup_restored"), BannerIcon.Information)
                        Else
                            Logger.Log("[INFO] Restore process cancelled by the user.")
                        End If
                    Else
                        'If no valid backup directory is found (.Count = 0) display an error
                        CustomMsgBox.Show(
                            Localization.GetString("msgbox_invalid_backup_folder"),
                            Localization.GetString("msgbox_invalid_backup_folder_title"),
                            CustomMsgBoxButtons.OKCancel,
                            CustomMsgBoxIcon.Warning
                        )
                        Logger.Log("[INFO] No valid backup found inside " & Form1.BackupLocTextBox.Text & ". Restore process aborted.")

                        Form1.LatestBackupHelpLabel.Text = Localization.GetString("ui_tasks_latest_none")
                    End If
                End If
            Else
                CustomMsgBox.Show(
                    Localization.GetString("msgbox_empty_backup_folder"),
                    Localization.GetString("msgbox_empty_backup_folder_title"),
                    CustomMsgBoxButtons.OKCancel,
                    CustomMsgBoxIcon.Warning
                )
                Logger.Log("[INFO] No backup found inside " & Form1.BackupLocTextBox.Text & ". Restore process aborted.")

                Form1.LatestBackupHelpLabel.Text = Localization.GetString("ui_tasks_latest_none")
            End If
        Catch ex As Exception
            Logger.Log("[ERROR] Could not restore the backup: " & ex.Message())
            CustomMsgBox.Show(
                Localization.GetString("msgbox_restore_error"),
                Localization.GetString("msgbox_restore_error_title"),
                CustomMsgBoxButtons.OKCancel,
                CustomMsgBoxIcon.Error
            )
        Finally
            'Empty backup directories list to avoid ArgumentOutOfRangeException when attempting to restore a backup from an empty directory (or directory with no valid backup) that previously contained valid backups
            BackupDirs.Clear()
        End Try
    End Sub

    Public Shared Overloads Sub RestoreBackup(subDir As String)
        Logger.Log("[INFO] Restore process started (subDir).")

        Try
            Dim BackupDirectory As String = Form1.BackupLocTextBox.Text & "\" & subDir

            'Ask the user before restoring the backup
            CustomMsgBox.Show(
                Strings.Format(Localization.GetString("msgbox_backup_restore"), BackupDirectory.Replace("\", "\\"), Form1.SavegamesLocTextBox.Text.Replace("\", "\\")),
                Localization.GetString("msgbox_backup_restore_title"),
                CustomMsgBoxButtons.YesNoCancel,
                CustomMsgBoxIcon.Warning,
                CustomMsgBoxDefaultButton.Button2
            )
            If CustomMsgBox.DialogResult = DialogResult.Yes Then
                For Each F In Directory.GetFiles(BackupDirectory, "*.save")
                    File.Copy(F, F.Replace(BackupDirectory, Form1.SavegamesLocTextBox.Text), True)
                Next
                Logger.Log("[INFO] Backup from " & BackupDirectory & " restored.")
                Banner.Show(Localization.GetString("banner_backup_restored"), BannerIcon.Information)
            Else
                Logger.Log("[INFO] Restore process cancelled by the user.")
            End If
        Catch ex As Exception
            Logger.Log("[ERROR] Could not restore the backup: " & ex.Message())
            CustomMsgBox.Show(
                Localization.GetString("msgbox_restore_error"),
                Localization.GetString("msgbox_restore_error_title"),
                CustomMsgBoxButtons.OKCancel,
                CustomMsgBoxIcon.Error
            )
        End Try
    End Sub
#End Region

#Region "Backup Deletion"
    Public Shared Overloads Sub DeleteBackup()
        Logger.Log("[INFO] Deletion process started.")

        Try
            'Ask the user before deleting the backup
            CustomMsgBox.Show(
                Strings.Format(Localization.GetString("msgbox_backup_delete_all"), Form1.BackupLocTextBox.Text.Replace("\", "\\")),
                Localization.GetString("msgbox_backup_delete_title"),
                CustomMsgBoxButtons.YesNoCancel, CustomMsgBoxIcon.Warning,
                CustomMsgBoxDefaultButton.Button2
            )
            If CustomMsgBox.DialogResult = DialogResult.Yes Then
                'Delete all directories inside the backup location recursively
                For Each D In Directory.EnumerateDirectories(Form1.BackupLocTextBox.Text)
                    Directory.Delete(D, True)
                Next
                'Delete all rows
                '//stackoverflow.com/a/19959099
                Form1.BackupsDataGrid.DataSource = Nothing
                'Detect latest backup
                DetectLatestBackup()
                Logger.Log("[INFO] Backup " & Form1.BackupLocTextBox.Text & " deleted.")
                Banner.Show(Localization.GetString("banner_backup_deleted_all"), BannerIcon.Information)
            Else
                Logger.Log("[INFO] Deletion process cancelled by the user.")
            End If
        Catch ex As Exception
            Logger.Log("[ERROR] Could not delete backups: " & ex.Message())
            CustomMsgBox.Show(
                Localization.GetString("msgbox_delete_all_error"),
                Localization.GetString("msgbox_delete_error_title"),
                CustomMsgBoxButtons.OKCancel,
                CustomMsgBoxIcon.Error
            )
        End Try
    End Sub

    Public Shared Overloads Sub DeleteBackup(subDir As String)
        Logger.Log("[INFO] Deletion process started (subDir).")

        Try
            Dim BackupDirectory As String = Form1.BackupLocTextBox.Text & "\" & subDir

            Select Case Form1.BackupsDataGrid.Rows.Count
                'If there is only one row, let the user know that they're deleting the only backup available
                Case 1
                    'Ask the user before deleting the backup
                    CustomMsgBox.Show(
                        Strings.Format(Localization.GetString("msgbox_backup_delete_latest"), BackupDirectory.Replace("\", "\\")),
                        Localization.GetString("msgbox_backup_delete_title"),
                        CustomMsgBoxButtons.YesNoCancel,
                        CustomMsgBoxIcon.Warning,
                        CustomMsgBoxDefaultButton.Button2
                    )
                    If CustomMsgBox.DialogResult = DialogResult.Yes Then
                        'Delete backup directory and all files inside of it (recursive)
                        Directory.Delete(BackupDirectory, True)
                        'Delete row
                        Form1.BackupsDataGrid.Rows.Remove(Form1.BackupsDataGrid.CurrentRow)
                        'Detect latest backup
                        DetectLatestBackup()

                        Logger.Log("[INFO] Backup " & BackupDirectory & " deleted.")
                        Banner.Show(Localization.GetString("banner_backup_deleted"), BannerIcon.Information)
                    Else
                        Logger.Log("[INFO] Deletion process cancelled by the user.")
                    End If
                Case Else
                    'Ask the user before deleting the backup
                    CustomMsgBox.Show(
                        Strings.Format(Localization.GetString("msgbox_backup_delete"), BackupDirectory.Replace("\", "\\")),
                        Localization.GetString("msgbox_backup_delete_title"),
                        CustomMsgBoxButtons.YesNoCancel,
                        CustomMsgBoxIcon.Warning,
                        CustomMsgBoxDefaultButton.Button2
                    )
                    If CustomMsgBox.DialogResult = DialogResult.Yes Then
                        'Delete backup directory and all files inside of it (recursive)
                        Directory.Delete(BackupDirectory, True)
                        'Delete row
                        Form1.BackupsDataGrid.Rows.Remove(Form1.BackupsDataGrid.CurrentRow)
                        'Detect latest backup
                        DetectLatestBackup()
                        Logger.Log("[INFO] Backup " & BackupDirectory & " deleted.")
                        Banner.Show(Localization.GetString("banner_backup_deleted"), BannerIcon.Information)
                    Else
                        Logger.Log("[INFO] Deletion process cancelled by the user.")
                    End If
            End Select
        Catch ex As Exception
            Logger.Log("[ERROR] Could not delete the backup: " & ex.Message())
            CustomMsgBox.Show(Localization.GetString("msgbox_delete_error"), Localization.GetString("msgbox_delete_error_title"), CustomMsgBoxButtons.OKCancel, CustomMsgBoxIcon.Error)
        End Try
    End Sub
#End Region
End Class

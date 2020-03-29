﻿#Region "Copyright (c) 2019 - 2020 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster"
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

Imports System.ComponentModel
Imports System.IO
Imports GHOSTbackup.Var

Public Class BackupHelper
    Private Shared WithEvents DetectBackupTimestamp As New BackgroundWorker()
    Private Shared Property ErrorMessage As String = Nothing
    Private Shared WithEvents BackupTimer As New Timer()
    Public Shared ReadOnly BackupDirs As New List(Of String)

    Public Shared Sub DetectLatestBackup()
        'Pass the backup folder path to the background worker
        '//stackoverflow.com/a/4807200
        DetectBackupTimestamp.RunWorkerAsync(Form1.BackupLocTextBox.Text)
    End Sub

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
                e.Result = Directory.GetCreationTime(BackupDirs.Item(BackupDirs.Count - 1)).ToString("yyyy-MM-dd HH:mm")
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

                Form1.LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
            Else
                Form1.LatestBackupHelpLabel.Text = "Latest backup: " & e.Result()
            End If
        Else
            Logger.Log("[ERROR] An error occurred while enumerating backup directories: " & ErrorMessage)
            Banner.Show(48, "Unable to get latest backup timestamp. Please check the logs for more details.")

            Form1.LatestBackupHelpLabel.Text = "Latest backup: Error."

            ErrorMessage = Nothing
        End If
    End Sub

    Public Shared Sub StartBackup()
        BackupTimer.Interval = Integer.Parse(Form1.BackupFreqTextBox.Text) * 60000
        BackupTimer.Start()
        IsBackupRunning = True
        Form1.BackupFreqTextBox.Enabled = False
        Form1.BackupBtn.Enabled = False
        Form1.StopBtn.Enabled = True
        Form1.RestoreBtn.Enabled = False
        Form1.SavegamesLocTextBox.Enabled = False
        Form1.BrowseSavegamesLocBtn.Enabled = False
        Form1.BackupLocTextBox.Enabled = False
        Form1.BrowseBackupLocBtn.Enabled = False
        Form1.SettingsNonUplayVersionChkBox.Enabled = False
        Form1.SettingsCustomExeTextBox.Enabled = False
        Form1.SettingsBrowseCustomExeBtn.Enabled = False
        Form1.SettingsOpenCustomExeFolderBtn.Enabled = False
    End Sub

    Public Shared Sub StopBackup()
        BackupTimer.Stop()
        IsBackupRunning = False
        Form1.BackupFreqTextBox.Enabled = True
        Form1.BackupBtn.Enabled = True
        Form1.StopBtn.Enabled = False
        Form1.RestoreBtn.Enabled = True
        Form1.SavegamesLocTextBox.Enabled = True
        Form1.BrowseSavegamesLocBtn.Enabled = True
        Form1.BackupLocTextBox.Enabled = True
        Form1.BrowseBackupLocBtn.Enabled = True
        Form1.SettingsNonUplayVersionChkBox.Enabled = True
        Form1.SettingsCustomExeTextBox.Enabled = True
        Form1.SettingsBrowseCustomExeBtn.Enabled = True
        Form1.SettingsOpenCustomExeFolderBtn.Enabled = True
    End Sub

    Public Shared Sub PerformFirstBackup()
        If Form1.SavegamesLocTextBox.Text = "" Or Form1.BackupLocTextBox.Text = "" Then
            Banner.Show(64, "You must specify both save games and backup folders.")
        ElseIf IsGameRunning = True Then
            StartBackup()

            'Perform the first backup
            Try
                'Store latest backup timestamp and subdirectory
                Dim BackupTimestamp As Date = Now
                Dim BackupDirectory As String = Form1.BackupLocTextBox.Text & BackupTimestamp.ToString("\\yyyyMMdd HHmm")

                Dim SavegamesList As String() = Directory.GetFiles(Form1.SavegamesLocTextBox.Text, "*.save")
                For Each F As String In SavegamesList
                    If Not Directory.Exists(BackupDirectory) Then
                        Directory.CreateDirectory(BackupDirectory)
                    End If
                    Dim FileName As String = F.Substring(Form1.SavegamesLocTextBox.Text.Length + 1)
                    File.Copy(Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), Path.Combine(BackupDirectory, FileName), True)
                Next

                'Write the timestamp of this backup on the main screen
                Form1.LatestBackupHelpLabel.Text = "Latest backup: " & BackupTimestamp.ToString("yyyy-MM-dd HH:mm")

                Logger.Log("[INFO] Performed the first backup " & "(" & SavegamesList.Length & " files copied to " & BackupDirectory & ").")
                Notification.Show("Performed first backup.")

            Catch ex As Exception
                StopBackup()
                Logger.Log("[ERROR] Backup interrupted: " & ex.Message())
                Notification.Show("Backup interrupted because an error occurred.")
                CustomMsgBox.Show("{\rtf1 The backup process has been {\b interrupted due to an error.} Please check the logs for more details.}", "Backup interrupted", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
            End Try
        ElseIf IsGameRunning = False Then
            Banner.Show(64, "You must launch Wildlands before starting the backup process.")
        End If
    End Sub

    Private Shared Sub BackupTimer_Tick(sender As Object, e As EventArgs) Handles BackupTimer.Tick
        If IsGameRunning = True Then
            Try
                'Store latest backup timestamp and subdirectory
                Dim BackupTimestamp As Date = Now
                Dim BackupDirectory As String = Form1.BackupLocTextBox.Text & BackupTimestamp.ToString("\\yyyyMMdd HHmm")

                Dim SavegamesList As String() = Directory.GetFiles(Form1.SavegamesLocTextBox.Text, "*.save")
                For Each F As String In SavegamesList
                    If Not Directory.Exists(BackupDirectory) Then
                        Directory.CreateDirectory(BackupDirectory)
                    End If
                    Dim FileName As String = F.Substring(Form1.SavegamesLocTextBox.Text.Length + 1)
                    File.Copy(Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), Path.Combine(BackupDirectory, FileName), True)
                Next

                'Write the timestamp of this backup on the main screen
                Form1.LatestBackupHelpLabel.Text = "Latest backup: " & BackupTimestamp.ToString("yyyy-MM-dd HH:mm")

                Logger.Log("[INFO] Backup complete " & "(" & SavegamesList.Length & " files copied to " & BackupDirectory & ").")
                Notification.Show("Backup complete.")

            Catch ex As Exception
                StopBackup()
                Logger.Log("[ERROR] Backup interrupted: " & ex.Message())
                Notification.Show("Backup interrupted because an error occurred.")
                CustomMsgBox.Show("{\rtf1 The backup process has been {\b interrupted due to an error.} Please check the logs for more details.}", "Backup interrupted", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
            End Try
        Else
            StopBackup()
            Logger.Log("[INFO] Wildlands closed or crashed, Backup interrupted.")
            CustomMsgBox.Show("{\rtf1 Wildlands {\b has been closed or crashed}, as a result the backup process has been interrupted.}", "Wildlands is no longer running", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
        End If
    End Sub

    Public Shared Sub RestoreBackup()
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
                        CustomMsgBox.Show("{\rtf1 Restoring a backup will copy the save files over from the backup folder: " & BackupDirs.Item(BackupDirs.Count - 1).Replace("\", "\\") & "\line\line and will {\b OVERWRITE} the existing save files inside the " _
                                          & "game folder: " & Form1.SavegamesLocTextBox.Text.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                                          "Backup restore",
                                          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            Dim SavegamesList As String() = Directory.GetFiles(BackupDirs.Item(BackupDirs.Count - 1), "*.save")
                            For Each F As String In SavegamesList
                                Dim FileName As String = F.Substring(BackupDirs.Item(BackupDirs.Count - 1).Length + 1)
                                File.Copy(Path.Combine(BackupDirs.Item(BackupDirs.Count - 1), FileName), Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), True)
                            Next

                            Logger.Log("[INFO] Backup from " & BackupDirs.Item(BackupDirs.Count - 1) & " restored.")
                            Banner.Show(64, "Backup successfully restored.")
                        Else
                            Logger.Log("[INFO] Restore process cancelled by the user.")
                        End If
                    Else
                        'If no directory contains any save files
                        CustomMsgBox.Show("{\rtf1 The current {\b backup folder doesn't contain any backup.} Backup at least once and try again.}", "No backup found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        Logger.Log("[INFO] No valid backup found inside " & Form1.BackupLocTextBox.Text & ". Restore process aborted.")

                        Form1.LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
                    End If
                ElseIf Form1.WhichBackupDropdown.SelectedIndex = 1 Then
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
                        CustomMsgBox.Show("{\rtf1 Restoring a backup will copy the save files over from the backup folder: " & BackupDirs.Item(BackupDirs.Count - 2).Replace("\", "\\") & "\line\line and will {\b OVERWRITE} the existing save files inside the " _
                                          & "game folder: " & Form1.SavegamesLocTextBox.Text.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                                          "Backup restore",
                                          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            Dim SavegamesList As String() = Directory.GetFiles(BackupDirs.Item(BackupDirs.Count - 2), "*.save")
                            For Each F As String In SavegamesList
                                Dim FileName As String = F.Substring(BackupDirs.Item(BackupDirs.Count - 2).Length + 1)
                                File.Copy(Path.Combine(BackupDirs.Item(BackupDirs.Count - 2), FileName), Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), True)
                            Next

                            Logger.Log("[INFO] Backup from " & BackupDirs.Item(BackupDirs.Count - 2) & " restored.")
                            Banner.Show(64, "Backup successfully restored.")
                        Else
                            Logger.Log("[INFO] Restore process cancelled by the user.")
                        End If
                    ElseIf BackupDirs.Count = 1 Then
                        'If only one valid backup directory exists ask the user to restore the latest backup instead
                        'Ask the user before restoring the latest backup
                        CustomMsgBox.Show("{\rtf1 You chose to restore the second-to-last backup but {\b it doesn't exist.} Do you want to restore the latest backup instead? This will copy the save files over from the backup folder: " _
                                          & BackupDirs.Item(BackupDirs.Count - 1).Replace("\", "\\") & "\line\line And will {\b OVERWRITE} the existing save files inside the game folder: " & Form1.SavegamesLocTextBox.Text.Replace("\", "\\") _
                                          & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                                          "Backup doesn't exist",
                                          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            Dim SavegamesList As String() = Directory.GetFiles(BackupDirs.Item(BackupDirs.Count - 1), "*.save")
                            For Each F As String In SavegamesList
                                Dim FileName As String = F.Substring(BackupDirs.Item(BackupDirs.Count - 1).Length + 1)
                                File.Copy(Path.Combine(BackupDirs.Item(BackupDirs.Count - 1), FileName), Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), True)
                            Next

                            Logger.Log("[INFO] Backup from " & BackupDirs.Item(BackupDirs.Count - 1) & " restored.")
                            Banner.Show(64, "Backup successfully restored.")
                        Else
                            Logger.Log("[INFO] Restore process cancelled by the user.")
                        End If
                    Else
                        'If no valid backup directory is found (.Count = 0) display an error
                        CustomMsgBox.Show("{\rtf1 The current {\b backup folder doesn't contain any backup.} Backup at least once and try again.}", "No backup found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        Logger.Log("[INFO] No valid backup found inside " & Form1.BackupLocTextBox.Text & ". Restore process aborted.")

                        Form1.LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
                    End If
                ElseIf Form1.WhichBackupDropdown.SelectedIndex = 2 Then
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
                        CustomMsgBox.Show("{\rtf1 Restoring a backup will copy the save files over from the backup folder: " & Form1.BackupLocTextBox.Text.Replace("\", "\\") & "\\" & CustomMsgBox.BackupDirsDropdownCombo.SelectedItem.ToString().Substring(0, 13) _
                                          & "\line\line and will {\b OVERWRITE} the existing save files inside the game folder: " & Form1.SavegamesLocTextBox.Text.Replace("\", "\\") & "\line\line {\b THIS CANNOT BE UNDONE. ARE YOU SURE YOU WANT TO PROCEED?}}",
                                          "Backup restore",
                                          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        If CustomMsgBox.DialogResult = DialogResult.Yes Then
                            'Store the selected backup subdirectory and truncate "Created at..." with .Substring(startIndex, lenght)
                            Dim BackupSubDir = Form1.BackupLocTextBox.Text & "\" & CustomMsgBox.BackupDirsDropdownCombo.SelectedItem.ToString().Substring(0, 13)
                            Dim SavegamesList As String() = Directory.GetFiles(BackupSubDir, "*.save")
                            For Each F As String In SavegamesList
                                Dim FileName As String = F.Substring(BackupSubDir.Length + 1)
                                File.Copy(Path.Combine(BackupSubDir, FileName), Path.Combine(Form1.SavegamesLocTextBox.Text, FileName), True)
                            Next

                            Logger.Log("[INFO] Backup from " & BackupSubDir & " restored.")
                            Banner.Show(64, "Backup successfully restored.")
                        Else
                            Logger.Log("[INFO] Restore process cancelled by the user.")
                        End If
                    Else
                        'If no directory contains any save files
                        CustomMsgBox.Show("{\rtf1 The current {\b backup folder doesn't contain any backup.} Backup at least once and try again.}", "No backup found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                        Logger.Log("[INFO] No valid backup found inside " & Form1.BackupLocTextBox.Text & ". Restore process aborted.")

                        Form1.LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
                    End If
                End If
            Else
                CustomMsgBox.Show("{\rtf1 The current {\b backup folder is empty.} Backup at least once and try again.}", "No backup found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                Logger.Log("[INFO] No backup found inside " & Form1.BackupLocTextBox.Text & ". Restore process aborted.")

                Form1.LatestBackupHelpLabel.Text = "Latest backup: No backup yet."
            End If

        Catch ex As Exception
            Logger.Log("[ERROR] Could not restore the backup: " & ex.Message())
            CustomMsgBox.Show("{\rtf1 The restore process has been {\b interrupted due to an error.} Please check the logs for more details.}", "Restore failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

        Finally
            'Empty subdirectories list to avoid adding duplicates in the next restore process
            CustomMsgBox.BackupDirsDropdownCombo.Items.Clear()
            'Empty backup directories list to avoid ArgumentOutOfRangeException when attempting to restore a backup from an empty directory (or directory with no valid backup) that previously contained valid backups
            BackupDirs.Clear()
        End Try
    End Sub
End Class

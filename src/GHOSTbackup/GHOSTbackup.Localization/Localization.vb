﻿#Region "Copyright (c) 2019 Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster"
''
'' GHOST Buster - Ghost Recon Wildlands backup utility
''
'' Copyright (c) 2019 Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster
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

Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Resources
Imports System.Threading

Public Class Localization
    'Custom Localization Class
    '//www.codeproject.com/Articles/5447/NET-Localization-using-Resource-file

    'TODO: Handle missing localization files at launch

    Private Shared ReadOnly LocalizationManager As ResourceManager = ResourceManager.CreateFileBasedResourceManager("Strings", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) & "\Languages\", Nothing)

    Private Shared Function GetLanguage() As CultureInfo
        Select Case Form1.SettingsInterfaceLangDropdown.SelectedIndex
            Case 0
                Return New CultureInfo("en")
            Case 1
                Return New CultureInfo("it")
            Case Else
                Return New CultureInfo("en")
        End Select
    End Function

    Public Shared Sub Load()
        'Set interface language
        Thread.CurrentThread.CurrentUICulture = GetLanguage()

        'Localize form elements
        Form1.PlayGameBtn.Text = GetString("ui_play")
        Form1.SidemenuTasks.Text = "  " & GetString("ui_title_tasks")
        Form1.SidemenuManage.Text = "  " & GetString("ui_manage")
        Form1.SidemenuSettings.Text = "  " & GetString("ui_title_settings")
        Form1.TitleLabel.Text = GetString("ui_title_tasks")
        'Top menu
        Form1.LogLabel.Text = GetString("topmenu_logs")
        Form1.AboutLabel.Text = GetString("topmenu_about")
        'Tasks
        Form1.BackupBtn.Text = GetString("ui_tasks_backup")
        Form1.StopBtn.Text = GetString("ui_tasks_stop")
        Form1.RestoreBtn.Text = GetString("ui_tasks_restore")
        Form1.BackupFreqHelp1Label.Text = GetString("ui_tasks_freq")
        Form1.BackupFreqHelp2Label.Text = GetString("ui_tasks_freq_minutes")
        Form1.LatestBackupHelpLabel.Text = GetString("ui_tasks_latest_none")
        Form1.WhichBackupLabel.Text = GetString("ui_tasks_restore_choice")
        Form1.WhichBackupDropdown.Items(Form1.WhichBackupDropdown.FindStringExact(Form1.WhichBackupDropdown.Items(0)).ToString()) = GetString("ui_tasks_dropdown_latest")
        Form1.WhichBackupDropdown.Items(Form1.WhichBackupDropdown.FindStringExact(Form1.WhichBackupDropdown.Items(1)).ToString()) = GetString("ui_tasks_dropdown_second")
        'Folders
        Form1.FoldersTitleLabel.Text = GetString("ui_title_folders")
        Form1.SavegamesLocHelpLabel.Text = GetString("ui_folders_wildlands")
        Form1.BrowseSavegamesLocBtn.Text = GetString("ui_btn_browse")
        Form1.ExploreSavegamesLocBtn.Text = GetString("ui_btn_open")
        Form1.BackupLocHelpLabel.Text = GetString("ui_folders_backup")
        Form1.BrowseBackupLocBtn.Text = GetString("ui_btn_browse")
        Form1.ExploreBackupLocBtn.Text = GetString("ui_btn_open")
        'Settings
        Form1.SettingsInterfaceLang.Text = GetString("ui_settings_lang_choice")
        Form1.SettingsLangRestartLabel.Text = GetString("ui_settings_restart")
        Form1.SettingsConfirmExitChkBox.Text = GetString("ui_settings_confirm_exit")
        Form1.SettingsConfirmStopBackupChkBox.Text = GetString("ui_settings_confirm_backup_interruption")
        Form1.SettingsDisplayNotificationChkBox.Text = GetString("ui_settings_backup_notification")
        Form1.SettingsDisableCloudSyncChkBox.Text = GetString("ui_settings_disable_sync")
        Form1.SettingsEnableCloudSyncChkBox.Text = GetString("ui_settings_enable_sync")
        Form1.SettingsCheckUpdatesChkBox.Text = GetString("ui_settings_updates")
        Form1.SettingsRememberFormPositionChkBox.Text = GetString("ui_settings_coordinates")
        Form1.SettingsNonUplayVersionChkBox.Text = GetString("ui_settings_nouplay")
        Form1.SettingsBrowseCustomExeBtn.Text = GetString("ui_btn_browse")
        Form1.SettingsOpenCustomExeFolderBtn.Text = GetString("ui_btn_open_folder")
        Form1.SettingsNonUplayVersionRestartLabel.Text = GetString("ui_settings_restart")
        Form1.SettingsWriteLogToFileChkBox.Text = GetString("ui_settings_log_file")
        Form1.SettingsBrowseLogFileBtn.Text = GetString("ui_btn_browse")
        Form1.SettingsOpenLogBtn.Text = GetString("ui_btn_open")
        'Manage Backups
        Form1.RestoreManageContextMenuItem.Text = GetString("ui_manage_context_restore")
        Form1.DeleteManageContextMenuItem.Text = GetString("ui_manage_context_delete")
        Form1.RefreshManageContextMenuItem.Text = GetString("ui_manage_context_refresh")
        Form1.DeleteAllManageContextMenuItem.Text = GetString("ui_manage_context_deleteall")
        Form1.ExplorerManageContextMenuItem.Text = GetString("ui_manage_context_explorer")
        'Logs
        Form1.CopyLogsContextMenuItem.Text = GetString("ui_logs_context_copy")
        Form1.SelectAllLogsContextMenuItem.Text = GetString("ui_logs_context_selectall")
        Form1.SaveLogsContextMenuItem.Text = GetString("ui_logs_context_export")
        'About
        Form1.AppInfoLabel.Text = String.Format(GetString("ui_about_about"), Updater.VersionString, My.Application.Info.Copyright)
        Form1.WebsiteLink.Text = GetString("ui_about_www")
        Form1.SupportLink.Text = GetString("ui_about_issues")
        Form1.ChangelogLink.Text = GetString("ui_about_changelog")
        Form1.LicenseLink.Text = GetString("ui_about_legal")
        'Tooltips
        Form1.HelpToolTip.SetToolTip(Form1.UplayBtn, GetString("tooltip_uplay"))
        Form1.HelpToolTip.SetToolTip(
            Form1.BackupFreqUpDown,
            String.Format(GetString("tooltip_backup_frequency"), Form1.BackupFreqUpDown.Minimum, Form1.BackupFreqUpDown.Maximum)
        )
        Form1.HelpToolTip.SetToolTip(Form1.SettingsConfirmExitChkBox, GetString("tooltip_confirm_exit"))
        Form1.HelpToolTip.SetToolTip(Form1.SettingsConfirmStopBackupChkBox, GetString("tooltip_confirm_backup_interruption"))
        Form1.HelpToolTip.SetToolTip(Form1.SettingsDisplayNotificationChkBox, GetString("tooltip_backup_notification"))
        Form1.HelpToolTip.SetToolTip(Form1.SettingsDisableCloudSyncChkBox, GetString("tooltip_disable_sync"))
        Form1.HelpToolTip.SetToolTip(Form1.SettingsEnableCloudSyncChkBox, GetString("tooltip_enable_sync"))
        Form1.HelpToolTip.SetToolTip(Form1.SettingsCheckUpdatesChkBox, GetString("tooltip_updates"))
        Form1.HelpToolTip.SetToolTip(Form1.SettingsRememberFormPositionChkBox, GetString("tooltip_coordinates"))
        Form1.CopyLogsContextMenuItem.ToolTipText = GetString("ui_logs_context_copy_tooltip")
        Form1.SelectAllLogsContextMenuItem.ToolTipText = GetString("ui_logs_context_selectall_tooltip")
        Form1.SaveLogsContextMenuItem.ToolTipText = GetString("ui_logs_context_export_tooltip")
        Form1.RestoreManageContextMenuItem.ToolTipText = GetString("ui_manage_context_restore_tooltip")
        Form1.DeleteManageContextMenuItem.ToolTipText = GetString("ui_manage_context_delete_tooltip")
        Form1.RefreshManageContextMenuItem.ToolTipText = GetString("ui_manage_context_refresh_tooltip")
        Form1.DeleteAllManageContextMenuItem.ToolTipText = GetString("ui_manage_context_deleteall_tooltip")
        Form1.ExplorerManageContextMenuItem.ToolTipText = GetString("ui_manage_context_explorer_tooltip")

        'Localize Message Box
        'Buttons
        CustomMsgBox.CancelLabel.Text = GetString("msgbox_btn_cancel")
        CustomMsgBox.LeftButton.Text = GetString("msgbox_btn_yes")
    End Sub

    Public Shared Function GetString(ByVal stringId As String) As String
        'Get localized string from its ID
        Return LocalizationManager.GetString(stringId, CultureInfo.CurrentUICulture).Replace("\n", Environment.NewLine)
    End Function
End Class

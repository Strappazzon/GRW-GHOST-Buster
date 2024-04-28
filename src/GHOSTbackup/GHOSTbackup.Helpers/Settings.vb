#Region "Copyright (c) 2019 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster"
''
'' GHOST Buster - Ghost Recon Wildlands backup utility
''
'' Copyright (c) 2019 - 2021 Alberto Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster
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

Imports System.IO
Imports System.Text.RegularExpressions
Imports IniParser
Imports IniParser.Model

Public Class Settings
    Private Shared ReadOnly SettingsFile As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\ghostbackup.cfg"
    Private Shared Config As IniData
    Private Shared ConfigData As IniData
    Private Shared ReadOnly ConfigParser As FileIniDataParser = New FileIniDataParser()

    Public Shared Sub Init()
        If Not File.Exists(SettingsFile) Then
            'Create directory
            Directory.CreateDirectory(SettingsFile.Replace("\ghostbackup.cfg", ""))

            'Create default settings
            Config = New IniData()
            'GHOST Buster
            Config("GHOSTbackup")("Language") = "0" 'See Localization.GetLanguage() function
            Config("GHOSTbackup")("ConfirmExit") = "True"
            Config("GHOSTbackup")("ConfirmBackupInterruption") = "False"
            Config("GHOSTbackup")("CheckForUpdates") = "False"
            Config("GHOSTbackup")("RememberFormPosition") = "False"
            Config("GHOSTbackup")("FormPosition") = "{X=-1,Y=-1}"
            'Logging
            Config("Logging")("WriteEventsToFile") = "False"
            Config("Logging")("LogFilePath") = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\event.log"
            'Backup
            Config("Backup")("SavegamesDirectory") = Nothing
            Config("Backup")("BackupDirectory") = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\Savegames"
            Config("Backup")("BackupFrequency") = "5"
            Config("Backup")("DisplayNotification") = "False"
            '0=Latest, 1=Second-to-Last
            Config("Backup")("WhichBackupToRestore") = "0"
            'Uplay
            Config("Uplay")("DisableCloudSyncOnRestore") = "False"
            Config("Uplay")("EnableCloudSyncOnQuit") = "True"
            Config("Uplay")("NoUplay") = "False"
            Config("Uplay")("WildlandsCustomPath") = Nothing

            'Write default settings to file
            File.WriteAllText(SettingsFile, Config.ToString())

            'Set default settings
            'GHOST Buster
            Form1.SettingsInterfaceLangDropdown.SelectedIndex = 0
            'Backup
            'Create default backup directory
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\Savegames")
            Form1.BackupLocTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\Savegames"
            Form1.WhichBackupDropdown.SelectedIndex = 0
            Form1.BackupFreqUpDown.Value = Decimal.Round(5, 0)
            'Logging
            Form1.SettingsLogFilePathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\event.log"
        Else
            'Load settings

            'GHOST Buster
            Form1.SettingsInterfaceLangDropdown.SelectedIndex = Language()
            Form1.SettingsConfirmExitChkBox.Checked = ConfirmExit()
            Form1.SettingsConfirmStopBackupChkBox.Checked = ConfirmBackupInterruption()
            Form1.SettingsCheckUpdatesChkBox.Checked = CheckForUpdates()
            Form1.SettingsRememberFormPositionChkBox.Checked = RememberFormPosition()
            'Logging
            Form1.SettingsWriteLogToFileChkBox.Checked = WriteEventsToFile()
            Form1.SettingsLogFilePathTextBox.Text = LogFilePath()
            'Backup
            Form1.SavegamesLocTextBox.Text = SavegamesDirectory()
            Form1.BackupLocTextBox.Text = BackupDirectory()
            Form1.BackupFreqUpDown.Value = BackupFrequency()
            Form1.SettingsDisplayNotificationChkBox.Checked = DisplayNotification()
            Form1.WhichBackupDropdown.SelectedIndex = WhichBackupToRestore()
            'Uplay
            Form1.SettingsDisableCloudSyncChkBox.Checked = DisableCloudSyncOnRestore()
            Form1.SettingsEnableCloudSyncChkBox.Checked = EnableCloudSyncOnQuit()
            Form1.SettingsNonUplayVersionChkBox.Checked = NoUplay()
            Form1.SettingsCustomExeTextBox.Text = WildlandsCustomPath()
        End If
    End Sub

    Public Shared Sub Save()
        'Save Settings

        'Get settings
        Config = New IniData()
        'GHOST Buster
        Config("GHOSTbackup")("Language") = Form1.SettingsInterfaceLangDropdown.SelectedIndex
        Config("GHOSTbackup")("ConfirmExit") = Form1.SettingsConfirmExitChkBox.Checked
        Config("GHOSTbackup")("ConfirmBackupInterruption") = Form1.SettingsConfirmStopBackupChkBox.Checked
        Config("GHOSTbackup")("CheckForUpdates") = Form1.SettingsCheckUpdatesChkBox.Checked
        Config("GHOSTbackup")("RememberFormPosition") = Form1.SettingsRememberFormPositionChkBox.Checked
        Config("GHOSTbackup")("FormPosition") = If(Form1.SettingsRememberFormPositionChkBox.Checked = True, Form1.Location.ToString(), "{X=-1,Y=-1}")
        'Logging
        Config("Logging")("WriteEventsToFile") = Form1.SettingsWriteLogToFileChkBox.Checked
        Config("Logging")("LogFilePath") = Form1.SettingsLogFilePathTextBox.Text
        'Backup
        Config("Backup")("SavegamesDirectory") = Form1.SavegamesLocTextBox.Text
        Config("Backup")("BackupDirectory") = Form1.BackupLocTextBox.Text
        Config("Backup")("BackupFrequency") = Form1.BackupFreqUpDown.Value
        Config("Backup")("DisplayNotification") = Form1.SettingsDisplayNotificationChkBox.Checked
        Config("Backup")("WhichBackupToRestore") = Form1.WhichBackupDropdown.SelectedIndex
        'Uplay
        Config("Uplay")("DisableCloudSyncOnRestore") = Form1.SettingsDisableCloudSyncChkBox.Checked
        Config("Uplay")("EnableCloudSyncOnQuit") = Form1.SettingsEnableCloudSyncChkBox.Checked
        Config("Uplay")("NoUplay") = If(Form1.SettingsNonUplayVersionChkBox.Checked = True AndAlso Form1.SettingsCustomExeTextBox.Text <> "", Form1.SettingsNonUplayVersionChkBox.Checked, False)
        Config("Uplay")("WildlandsCustomPath") = If(Form1.SettingsNonUplayVersionChkBox.Checked = True AndAlso Form1.SettingsCustomExeTextBox.Text <> "", Form1.SettingsCustomExeTextBox.Text, Nothing)

        'Recreate directory if it's been deleted
        If Not Directory.Exists(SettingsFile.Replace("\ghostbackup.cfg", "")) Then
            Directory.CreateDirectory(SettingsFile.Replace("\ghostbackup.cfg", ""))
        End If

        'Write settings to file
        File.WriteAllText(SettingsFile, Config.ToString())
        Logger.Log("[INFO] Settings saved.")
    End Sub

#Region "GHOST Buster"
    Private Shared Function Language() As Integer
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("GHOSTbackup")("Language")
        Return If(Value <> Nothing, Integer.Parse(Value), 0)
    End Function

    Private Shared Function ConfirmExit() As Boolean
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("GHOSTbackup")("ConfirmExit")
        Return If(Value <> Nothing, Boolean.Parse(Value), True)
    End Function

    Private Shared Function ConfirmBackupInterruption() As Boolean
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("GHOSTbackup")("ConfirmBackupInterruption")
        Return If(Value <> Nothing, Boolean.Parse(Value), False)
    End Function

    Private Shared Function CheckForUpdates() As Boolean
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("GHOSTbackup")("CheckForUpdates")
        Return If(Value <> Nothing, Boolean.Parse(Value), False)
    End Function

    Public Shared Function RememberFormPosition() As Boolean
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("GHOSTbackup")("RememberFormPosition")
        Return If(Value <> Nothing, Boolean.Parse(Value), False)
    End Function

    Public Shared Function FormPosition() As Point
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Coordinates As String = ConfigData("GHOSTbackup")("FormPosition")
        Dim C = Regex.Replace(Coordinates, "[\{\}a-zA-Z=]", "").Split(",")

        If Coordinates <> Nothing Then
            'Convert from String ("C" RegEx) to System.Drawing.Point
            '//stackoverflow.com/a/10366689
            Return New Point(Integer.Parse(C(0)), Integer.Parse(C(1)))
        Else
            Return New Point(-1, -1)
        End If
    End Function
#End Region

#Region "Logging"
    Private Shared Function WriteEventsToFile() As Boolean
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Logging")("WriteEventsToFile")
        Return If(Value <> Nothing, Boolean.Parse(Value), False)
    End Function

    Private Shared Function LogFilePath() As String
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Logging")("LogFilePath")
        Return If(Value <> "", Value, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\event.log")
    End Function
#End Region

#Region "Backup"
    Private Shared Function SavegamesDirectory() As String
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Backup")("SavegamesDirectory")
        Return Value
    End Function

    Private Shared Function BackupDirectory() As String
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Backup")("BackupDirectory")
        Return Value
    End Function

    Private Shared Function BackupFrequency() As Decimal
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Backup")("BackupFrequency")
        Return If(Value <> Nothing, Decimal.Round(Decimal.Parse(Value), 0), Decimal.Round(5, 0))
    End Function

    Private Shared Function DisplayNotification() As Boolean
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Backup")("DisplayNotification")
        Return If(Value <> Nothing, Boolean.Parse(Value), False)
    End Function

    Private Shared Function WhichBackupToRestore() As Integer
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Backup")("WhichBackupToRestore")
        Return If(Value <> Nothing, Integer.Parse(Value), 0)
    End Function
#End Region

#Region "Uplay"
    Private Shared Function DisableCloudSyncOnRestore() As Boolean
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Uplay")("DisableCloudSyncOnRestore")
        Return If(Value <> Nothing, Boolean.Parse(Value), False)
    End Function

    Private Shared Function EnableCloudSyncOnQuit() As Boolean
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Uplay")("EnableCloudSyncOnQuit")
        Return If(Value <> Nothing, Boolean.Parse(Value), True)
    End Function

    Private Shared Function NoUplay() As Boolean
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Uplay")("NoUplay")
        Return If(Value <> Nothing, Boolean.Parse(Value), False)
    End Function

    Private Shared Function WildlandsCustomPath() As String
        ConfigData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Uplay")("WildlandsCustomPath")
        Return Value
    End Function
#End Region
End Class

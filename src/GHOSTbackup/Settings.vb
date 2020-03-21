Imports System.IO
Imports System.Text.RegularExpressions
Imports IniParser
Imports IniParser.Model

Public Class Settings
    Private Shared ReadOnly SettingsFile As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\ghostbackup.cfg"
    Private Shared ReadOnly ConfigParser = New FileIniDataParser()

    Public Shared Sub Init()
        If Not File.Exists(SettingsFile) Then
            'Create directory
            Directory.CreateDirectory(SettingsFile.Replace("\ghostbackup.cfg", ""))

            'Create default settings
            Dim ConfigData As IniData = New IniData()
            'GHOST Buster
            ConfigData("GHOSTbackup")("ConfirmExit") = "True"
            ConfigData("GHOSTbackup")("ConfirmBackupInterruption") = "False"
            ConfigData("GHOSTbackup")("CheckForUpdates") = "False"
            ConfigData("GHOSTbackup")("RememberFormPosition") = "False"
            ConfigData("GHOSTbackup")("FormPosition") = "{X=-1,Y=-1}"
            'Logging
            ConfigData("Logging")("WriteEventsToFile") = "False"
            ConfigData("Logging")("LogFilePath") = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\event.log"
            'Backup
            ConfigData("Backup")("SavegamesDirectory") = Nothing
            ConfigData("Backup")("BackupDirectory") = Nothing
            ConfigData("Backup")("BackupFrequency") = "5"
            ConfigData("Backup")("WhichBackupToRestore") = "0" '0=Latest, 1=Second-to-Last, 2=Let me decide
            'Uplay
            ConfigData("Uplay")("DisableCloudSyncOnRestore") = "False"
            ConfigData("Uplay")("NoUplay") = "False"
            ConfigData("Uplay")("WildlandsCustomPath") = Nothing

            'Write default settings to file
            File.WriteAllText(SettingsFile, ConfigData.ToString())

            'Set default settings
            Form1.SettingsLogFilePathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\event.log"
            Form1.WhichBackupDropdownCombo.SelectedIndex = 0
        Else
            'Load settings

            'GHOST Buster
            Form1.ConfirmExitChkBox.Checked = ConfirmExit()
            Form1.ConfirmStopBackupChkBox.Checked = ConfirmBackupInterruption()
            Form1.CheckUpdatesChkBox.Checked = CheckForUpdates()
            Form1.RememberFormPositionChkBox.Checked = RememberFormPosition()
            'Logging
            Form1.SettingsWriteLogToFileChkBox.Checked = WriteEventsToFile()
            Form1.SettingsLogFilePathTextBox.Text = LogFilePath()
            'Backup
            Form1.SavegamesLocTextBox.Text = SavegamesDirectory()
            Form1.BackupLocTextBox.Text = BackupDirectory()
            Form1.BackupFreqUpDown.Value = BackupFrequency()
            Form1.WhichBackupDropdownCombo.SelectedIndex = WhichBackupToRestore()
            'Uplay
            Form1.DisableCloudSyncChkBox.Checked = DisableCloudSyncOnRestore()
            Form1.SettingsNonUplayVersionChkBox.Checked = NoUplay()
            Form1.SettingsCustomExeTextBox.Text = WildlandsCustomPath()
        End If
    End Sub

    Public Shared Sub Save()
        'Save Settings

        'Get settings
        Dim ConfigData As IniData = New IniData()
        'GHOST Buster
        ConfigData("GHOSTbackup")("ConfirmExit") = Form1.ConfirmExitChkBox.Checked
        ConfigData("GHOSTbackup")("ConfirmBackupInterruption") = Form1.ConfirmStopBackupChkBox.Checked
        ConfigData("GHOSTbackup")("CheckForUpdates") = Form1.CheckUpdatesChkBox.Checked
        ConfigData("GHOSTbackup")("RememberFormPosition") = Form1.RememberFormPositionChkBox.Checked
        ConfigData("GHOSTbackup")("FormPosition") = If(Form1.RememberFormPositionChkBox.Checked = True, Form1.Location.ToString(), "{X=-1,Y=-1}")
        'Logging
        ConfigData("Logging")("WriteEventsToFile") = Form1.SettingsWriteLogToFileChkBox.Checked
        ConfigData("Logging")("LogFilePath") = Form1.SettingsLogFilePathTextBox.Text
        'Backup
        ConfigData("Backup")("SavegamesDirectory") = Form1.SavegamesLocTextBox.Text
        ConfigData("Backup")("BackupDirectory") = Form1.BackupLocTextBox.Text
        ConfigData("Backup")("BackupFrequency") = Form1.BackupFreqUpDown.Value
        ConfigData("Backup")("WhichBackupToRestore") = Form1.WhichBackupDropdownCombo.SelectedIndex
        'Uplay
        ConfigData("Uplay")("DisableCloudSyncOnRestore") = Form1.DisableCloudSyncChkBox.Checked
        ConfigData("Uplay")("NoUplay") = If(Form1.SettingsNonUplayVersionChkBox.Checked = True AndAlso Form1.SettingsCustomExeTextBox.Text <> "", Form1.SettingsNonUplayVersionChkBox.Checked, False)
        ConfigData("Uplay")("WildlandsCustomPath") = If(Form1.SettingsNonUplayVersionChkBox.Checked = True AndAlso Form1.SettingsCustomExeTextBox.Text <> "", Form1.SettingsCustomExeTextBox.Text, Nothing)

        'Recreate directory if it's been deleted
        If Not Directory.Exists(SettingsFile.Replace("\ghostbackup.cfg", "")) Then
            Directory.CreateDirectory(SettingsFile.Replace("\ghostbackup.cfg", ""))
        End If

        'Write settings to file
        File.WriteAllText(SettingsFile, ConfigData.ToString())

        Logger.Log("[INFO] Settings saved.")
    End Sub

#Region "GHOST Buster"
    Public Shared Function ConfirmExit() As Boolean
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("GHOSTbackup")("ConfirmExit")
        If Value <> Nothing Then
            Return Boolean.Parse(Value)
        Else
            Return True
        End If
    End Function

    Public Shared Function ConfirmBackupInterruption() As Boolean
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("GHOSTbackup")("ConfirmBackupInterruption")
        If Value <> Nothing Then
            Return Boolean.Parse(Value)
        Else
            Return False
        End If
    End Function

    Public Shared Function CheckForUpdates() As Boolean
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("GHOSTbackup")("CheckForUpdates")
        If Value <> Nothing Then
            Return Boolean.Parse(Value)
        Else
            Return False
        End If
    End Function

    Public Shared Function RememberFormPosition() As Boolean
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("GHOSTbackup")("RememberFormPosition")
        If Value <> Nothing Then
            Return Boolean.Parse(Value)
        Else
            Return False
        End If
    End Function

    Public Shared Function FormPosition() As Point
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Coordinates As String = ConfigData("GHOSTbackup")("FormPosition")
        Dim C = Regex.Replace(Coordinates, "[\{\}a-zA-Z=]", "").Split(",")

        If Coordinates <> Nothing Then
            '//stackoverflow.com/a/10366689
            Return New Point(Integer.Parse(C(0)), Integer.Parse(C(1)))
        Else
            Return New Point(-1, -1)
        End If
    End Function
#End Region

#Region "Logging"
    Public Shared Function WriteEventsToFile() As Boolean
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Logging")("WriteEventsToFile")
        If Value <> Nothing Then
            Return Boolean.Parse(Value)
        Else
            Return False
        End If
    End Function

    Public Shared Function LogFilePath() As String
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Logging")("LogFilePath")
        If Value <> "" Then
            Return Value
        Else
            Return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\GHOSTbackup\event.log"
        End If
    End Function
#End Region

#Region "Backup"
    Public Shared Function SavegamesDirectory() As String
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Backup")("SavegamesDirectory")
        Return Value
    End Function

    Public Shared Function BackupDirectory() As String
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Backup")("BackupDirectory")
        Return Value
    End Function

    Public Shared Function BackupFrequency() As Decimal
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Backup")("BackupFrequency")
        If Value <> Nothing Then
            Return Decimal.Round(Decimal.Parse(Value), 0)
        Else
            Return Decimal.Round(5, 0)
        End If
    End Function

    Public Shared Function WhichBackupToRestore() As Integer
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Backup")("WhichBackupToRestore")
        If Value <> Nothing Then
            Return Integer.Parse(Value)
        Else
            Return 0
        End If
    End Function
#End Region

#Region "Uplay"
    Public Shared Function DisableCloudSyncOnRestore() As Boolean
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Uplay")("DisableCloudSyncOnRestore")
        If Value <> Nothing Then
            Return Boolean.Parse(Value)
        Else
            Return False
        End If
    End Function

    Public Shared Function NoUplay() As Boolean
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Uplay")("NoUplay")
        If Value <> Nothing Then
            Return Boolean.Parse(Value)
        Else
            Return False
        End If
    End Function

    Public Shared Function WildlandsCustomPath() As String
        Dim ConfigData As IniData = ConfigParser.ReadFile(SettingsFile)

        Dim Value As String = ConfigData("Uplay")("WildlandsCustomPath")
        Return Value
    End Function
#End Region
End Class

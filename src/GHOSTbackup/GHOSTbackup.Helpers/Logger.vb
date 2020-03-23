Imports System.IO
Imports System.Text
Imports GHOSTbackup.Var

Public Class Logger
    Public Shared Sub StartSession()
        Log("[LOG SESSION] -------------------- START --------------------")
        Log("[INFO] GHOST Buster version: " & Version)

#If DEBUG Then
        Log("[INFO] Environment is DEVELOPMENT")
#Else
        Log("[INFO] Environment is PRODUCTION")
#End If
    End Sub

    Public Shared Sub Log([Event] As String)
        'Don't start the log file with an empty line
        If Form1.LogTxtBox.Text = "" Then
            Form1.LogTxtBox.AppendText(Now.ToString("HH:mm:ss") & " " & [Event])
        Else
            Form1.LogTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm:ss") & " " & [Event])
        End If

        If Form1.SettingsWriteLogToFileChkBox.Checked = True Then
            Dim LogToFile As New StringBuilder
            LogToFile.AppendLine(Now.ToString("HH:mm:ss") & " " & [Event])

            Try
                File.AppendAllText(Form1.SettingsLogFilePathTextBox.Text, LogToFile.ToString())

            Catch ex As Exception
                Form1.SettingsWriteLogToFileChkBox.Checked = False
                Form1.LogTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm:ss") & " [ERROR] Log session to file interrupted: " & ex.Message())
                Banner.Show(48, "Logging to file disabled due to an error.")
            End Try
        End If
    End Sub
End Class

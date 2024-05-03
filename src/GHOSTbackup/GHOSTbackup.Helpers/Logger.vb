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

Imports System.IO
Imports System.Text
Imports GHOSTbackup.UI

Public Class Logger
    Public Shared Sub StartSession()
        Log("[LOG SESSION] -------------------- START --------------------")
        Log("[INFO] GHOST Buster version: " & Updater.VersionString)

#If DEBUG Then
        Log("[INFO] Environment is DEVELOPMENT")
#Else
        Log("[INFO] Environment is PRODUCTION")
#End If
    End Sub

#Region "Log Methods"
    Public Shared Overloads Sub Log(ByVal [event] As String)
        'Don't start the log session with an empty line
        If Form1.LogTxtBox.Text = "" Then
            Form1.LogTxtBox.AppendText(Now.ToString("HH:mm:ss") & " " & [event])
        Else
            Form1.LogTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm:ss") & " " & [event])
        End If

        'Log event to file
        If Form1.SettingsWriteLogToFileChkBox.Checked = True Then
            Dim LogToFile As New StringBuilder
            LogToFile.AppendLine(Now.ToString("HH:mm:ss") & " " & [event])

            Try
                File.AppendAllText(Form1.SettingsLogFilePathTextBox.Text, LogToFile.ToString())
            Catch ex As Exception
                'Disable logging to file to avoid further errors
                Form1.SettingsWriteLogToFileChkBox.Checked = False

                Form1.LogTxtBox.AppendText(Environment.NewLine & Now.ToString("HH:mm:ss") & " [ERROR] Log session to file interrupted: " & ex.Message())
                Banner.Show(Localization.GetString("banner_log_error"), BannerIcon.Warning)
            End Try
        End If
    End Sub
#End Region
End Class

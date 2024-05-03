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

Imports System.Text

Public Class Strings

#Region "Format"
    'Custom String.Format functions
    '//stackoverflow.com/a/1321343

    Public Shared Overloads Function Format(ByVal input As String, ByVal arg0 As Object) As String
        Dim Sb As StringBuilder = New StringBuilder(input)

        Sb.Replace("<0>", arg0)
        Return Sb.ToString()
    End Function

    Public Shared Overloads Function Format(ByVal input As String, ByVal arg0 As Object, ByVal arg1 As Object) As String
        Dim Sb As StringBuilder = New StringBuilder(input)

        Sb.Replace("<0>", arg0)
        Sb.Replace("<1>", arg1)
        Return Sb.ToString()
    End Function

    Public Shared Overloads Function Format(ByVal input As String, ByVal arg0 As Object, ByVal arg1 As Object, ByVal arg2 As Object) As String
        Dim Sb As StringBuilder = New StringBuilder(input)

        Sb.Replace("<0>", arg0)
        Sb.Replace("<1>", arg1)
        Sb.Replace("<2>", arg2)
        Return Sb.ToString()
    End Function
#End Region
End Class

#Region "Copyright (c) 2019 Strappazzon, https://strappazzon.xyz/GRW-GHOST-Buster"
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

Namespace UI

#Region "CustomMsgBox"
    Public Enum CustomMsgBoxButtons
        OK = 0
        OKCancel = 0
        YesNo = 1
        YesNoCancel = 1
    End Enum

    Public Enum CustomMsgBoxIcon
        Hand = 16
        [Stop] = 16
        [Error] = 16
        Question = 32
        Exclamation = 48
        Warning = 48
    End Enum

    Public Enum CustomMsgBoxDefaultButton
        Button1 = 0
        Button2 = 256
        Button3 = 512
    End Enum
#End Region

#Region "Banner"
    Public Enum BannerIcon
        Exclamation = 48
        Warning = 48
        Information = 64
    End Enum
#End Region
End Namespace

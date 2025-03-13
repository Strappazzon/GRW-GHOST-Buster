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

Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.Controls
    <Description("A simple separator.")>
    <DesignerCategory("Code")>
    <ToolboxBitmap(GetType(Splitter))>
    Public Class Separator
        Inherits Control
        'Separator Control
        '//stackoverflow.com/a/42985091

        Private _BackColor As Color = Color.Transparent
        Private _LineColor As Color = Color.FromArgb(255, 100, 100, 100)
        Private _LinePen As Pen = Nothing
        Private _Text As String = ""

#Region "Properties"
        <Browsable(False)>
        Public Overrides Property BackColor As Color
            Get
                Return _BackColor
            End Get
            Set
                _BackColor = _BackColor
                Me.Invalidate(True)
            End Set
        End Property

        <Description("The color of the component.")>
        Public Property LineColor As Color
            Get
                Return _LineColor
            End Get
            Set(ByVal value As Color)
                _LineColor = value
                _LinePen = New Pen(LineColor, 1) With {
                    .Alignment = PenAlignment.Inset
                }

                Me.Refresh()
            End Set
        End Property

        <Browsable(False)>
        Public Overrides Property Text As String
            Get
                Return _Text
            End Get
            Set
                _Text = _Text
                Me.Invalidate(True)
            End Set
        End Property
#End Region

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso _LinePen IsNot Nothing Then
                _LinePen.Dispose()
                _LinePen = Nothing
            End If

            MyBase.Dispose(disposing)
        End Sub

        Public Sub New()
            MyBase.New()

            Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)

            'Default properties
            LineColor = Color.FromArgb(255, 100, 100, 100)
            TabStop = False
        End Sub

#Region "Paint Methods"
        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim CoordX As Integer = Width / 2

            e.Graphics.DrawLine(_LinePen, CoordX, 0, CoordX, Height)

            MyBase.OnPaint(e)
        End Sub
#End Region
    End Class
End Namespace

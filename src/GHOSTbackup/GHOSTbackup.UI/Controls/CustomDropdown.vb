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
'' Portions of this code are (c) 2013 Jens Thiel, https://thielj.github.io/MetroFramework
''
#End Region

Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.Controls
    <Description("Dropdown control that (tries to) mimics the Uplay design.")>
    <DesignerCategory("Code")>
    <ToolboxBitmap(GetType(ComboBox))>
    Public Class CustomDropdown
        Inherits ComboBox
        'Custom ComboBox Control
        '//stackoverflow.com/a/34886006

#If DEBUG Then
#Const DEBUG_DRAW = False
#End If

        Private Const WM_PAINT As Integer = 15

        Private _BackColor As Color = Color.FromArgb(255, 17, 20, 25)
        Private _BorderColor As Color = Color.FromArgb(255, 160, 160, 160)
        Private _ForeColor As Color = Color.White
        Private _ItemFont As Font = Nothing
        Private _Padding As Padding = New Padding(5, 0, 0, 0)

#Region "Properties"
        Public Overrides Property BackColor As Color
            Get
                Return _BackColor
            End Get
            Set(ByVal value As Color)
                _BackColor = value
                Me.Invalidate(True)
            End Set
        End Property

        <Description("The border color of the component.")>
        Public Property BorderColor As Color
            Get
                Return _BorderColor
            End Get
            Set(ByVal value As Color)
                _BorderColor = value
                Me.Invalidate(True)
            End Set
        End Property

        Public Overrides Property ForeColor As Color
            Get
                Return _ForeColor
            End Get
            Set(ByVal value As Color)
                _ForeColor = value
                Me.Invalidate(True)
            End Set
        End Property

        <Browsable(False)>
        <Description("The font used to display text in the dropdown items.")>
        Public Property ItemFont As Font
            Get
                Return _ItemFont
            End Get
            Set(ByVal value As Font)
                _ItemFont = value
                Me.Invalidate(True)
            End Set
        End Property

        <Browsable(True)>
        <Description("Specifies space between this control and another element's margin inside of it.")>
        Public Overloads Property Padding As Padding
            Get
                Return _Padding
            End Get
            Set(ByVal value As Padding)
                _Padding = value
                Me.Invalidate(True)
            End Set
        End Property
#End Region

        Public Sub New()
            Me.SetStyle(ControlStyles.UserPaint, True)

            'Default properties
            DrawMode = DrawMode.OwnerDrawFixed
            DropDownStyle = ComboBoxStyle.DropDownList
            FlatStyle = FlatStyle.Flat

            AddHandler Me.DrawItem, AddressOf Me.CustomDropdown_DrawItem
        End Sub

#Region "Paint Methods"
        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            If m.Msg = WM_PAINT AndAlso DropDownStyle <> ComboBoxStyle.Simple Then
                Using G As Graphics = Graphics.FromHwnd(Handle)
                    Using P As Pen = New Pen(BorderColor)
                        G.DrawRectangle(P, 0, 0, Width - 1, Height - 1)
                    End Using
                End Using
            End If
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)

            Dim ButtonWidth As Integer = SystemInformation.VerticalScrollBarWidth
            Dim ItemRect As Rectangle = New Rectangle(Width - ButtonWidth, 0, ButtonWidth, Height)
            Dim FillColor As Color = SystemColors.ControlLightLight

            'Background
            Using FillBrush As Brush = New SolidBrush(BackColor)
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                'e.Graphics.FillRectangle(FillBrush, Bounds)
                e.Graphics.FillRectangle(FillBrush, ItemRect)
            End Using

            'Arrow
            '//github.com/thielj/MetroFramework/blob/fb0d0c3/MetroFramework/Controls/MetroComboBox.cs#L107
            '//www.experts-exchange.com/questions/27055001/Change-Combobox-Dropdown-Arrow-Image.html#answer35831737-20
            Using ArrowBrush As SolidBrush = New SolidBrush(BorderColor)
                Dim Points As Point() = New Point(2) {
                    New Point(Width - 18, (Height / 2) - 3),
                    New Point(Width - 7, (Height / 2) - 3),
                    New Point(Width - 13, (Height / 2) + 3)
                }

                e.Graphics.FillPolygon(ArrowBrush, Points)
            End Using

            'Text
            '//docs.microsoft.com/en-us/dotnet/framework/winforms/controls/overriding-the-onpaint-method
            If SelectedIndex <> -1 Then
                Dim ItemText As String = Items(SelectedIndex).ToString()
                'Dim TextColor As Color = If(_ForeColor = Color.White, Color.Black, Color.White)
                Using TextBrush As Brush = New SolidBrush(ForeColor)
                    'Item text coordinates
                    Dim TextPosX As Single = Padding.Left
                    Dim TextPosY As Single = (Height - e.Graphics.MeasureString(ItemText, Font).Height) / 2

                    e.Graphics.DrawString(
                        ItemText,
                        Font,
                        TextBrush,
                        TextPosX,
                        TextPosY
                    )

                    TextBrush.Dispose()
                End Using
            End If
        End Sub
#End Region

#Region "Draw Methods"
        Private Sub CustomDropdown_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs)
            e.DrawBackground()

            If e.Index >= 0 Then
#If DEBUG_DRAW Then
                Console.WriteLine("[" & Name & "] DrawItem event triggered for index: " & e.Index)
                Console.WriteLine("[" & Name & "] Drawing bounds: " & e.Bounds.ToString())

                Logger.Log("[DEBUG] " & "[" & Name & "] DrawItem event triggered for index: " & e.Index)
                Logger.Log("[DEBUG] " & "[" & Name & "] Drawing bounds: " & e.Bounds.ToString())
#End If

                Dim ItemText As String = Items(e.Index).ToString()
                'Dim TextColor As Color = If(_ForeColor = Color.White, Color.Black, Color.White)
                Dim TextBrush As Brush = New SolidBrush(ForeColor)

                'e.Graphics.SetClip(e.Bounds)

                e.Graphics.DrawString(ItemText, Font, TextBrush, e.Bounds)

                TextBrush.Dispose()

                'Check if the item being drawn is the selected item
                If (e.State And DrawItemState.Selected = True) = DrawItemState.Selected Then
                    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds)
                    e.Graphics.DrawString(ItemText, Font, SystemBrushes.HighlightText, e.Bounds)
                End If
            End If

            e.DrawFocusRectangle()
        End Sub
#End Region
    End Class
End Namespace

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

Imports System.ComponentModel
Imports System.Runtime.InteropServices

Namespace UI.Controls
    <Description("NumericUpDown that (tries to) mimics the Uplay design.")>
    <DesignerCategory("Code")>
    <ToolboxBitmap(GetType(NumericUpDown))>
    Public Class CustomNumericUpDown
        Inherits NumericUpDown
        'Custom NumericUpDown Control
        '//stackoverflow.com/a/65879068
        '//github.com/r-aghaei/FlatNumericUpDownExample

        Private Const WS_EX_COMPOSITED As Long = 33554432
        Private Const WM_ERASEBKGND As Integer = 20
        Private Const WM_PAINT As Integer = 15

        Private _BorderColor As Color = Color.FromArgb(255, 160, 160, 160)

#Region "Properties"
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

        Protected ReadOnly Overrides Property CreateParams As CreateParams
            Get
                Dim CP As CreateParams = MyBase.CreateParams

                CP.ExStyle = CP.ExStyle Or WS_EX_COMPOSITED
                Return CP
            End Get
        End Property
#End Region

        Public Sub New()
            MyBase.New()

            Dim Renderer As Renderer = New Renderer(Controls(0))

            'Default properties
            BackColor = Color.FromArgb(255, 17, 20, 25)
            BorderStyle = BorderStyle.FixedSingle
            ContextMenuStrip = New ContextMenuStrip()
            'DoubleBuffered = True
            ForeColor = Color.White
            Increment = 5
            Minimum = 1
            Maximum = 180
            TextAlign = HorizontalAlignment.Center
        End Sub

#Region "Events"
        Protected Overrides Sub OnMouseWheel(ByVal e As MouseEventArgs)
            Dim MouseWheel As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)

            'Prevent scrolling
            MouseWheel.Handled = True
        End Sub
#End Region

        Private Class Renderer
            Inherits NativeWindow
            'Custom renderer for the Up and Down buttons control

            Private ReadOnly NumericUpDownControl As Control

            '//learn.microsoft.com/en-us/windows/win32/api/Winuser/ns-winuser-paintstruct
            <StructLayout(LayoutKind.Sequential)>
            Public Structure PAINTSTRUCT
                Public hdc As IntPtr
                Public fErase As Boolean
                Public rcPaint_left As Integer
                Public rcPaint_top As Integer
                Public rcPaint_right As Integer
                Public rcPaint_bottom As Integer
                Public fRestore As Boolean
                Public fIncUpdate As Boolean
                Public reserved1 As Integer
                Public reserved2 As Integer
                Public reserved3 As Integer
                Public reserved4 As Integer
                Public reserved5 As Integer
                Public reserved6 As Integer
                Public reserved7 As Integer
                Public reserved8 As Integer
            End Structure

#Region "Functions"
            <DllImport("user32.dll", EntryPoint:="BeginPaint", ExactSpelling:=True, CharSet:=CharSet.Auto)>
            Private Shared Function IntBeginPaint(ByVal hWnd As IntPtr, <[In], Out> ByRef lpPaint As PAINTSTRUCT) As IntPtr
            End Function

            <DllImport("user32.dll", EntryPoint:="EndPaint", ExactSpelling:=True, CharSet:=CharSet.Auto)>
            Private Shared Function IntEndPaint(ByVal hWnd As IntPtr, ByRef lpPaint As PAINTSTRUCT) As Boolean
            End Function

            Private Function GetDownArrow(ByVal r As Rectangle) As Point()
                Dim MiddlePoint As Point = New Point(r.Left + (r.Width / 2), r.Top + (r.Height / 2))

                Return New Point() {
                New Point(MiddlePoint.X - 3, MiddlePoint.Y - 2),
                New Point(MiddlePoint.X + 4, MiddlePoint.Y - 2),
                New Point(MiddlePoint.X, MiddlePoint.Y + 2)
            }
            End Function

            Private Function GetUpArrow(ByVal r As Rectangle) As Point()
                Dim MiddlePoint As Point = New Point(r.Left + (r.Width / 2), r.Top + (r.Height / 2))

                Return New Point() {
                New Point(MiddlePoint.X - 4, MiddlePoint.Y + 2),
                New Point(MiddlePoint.X + 4, MiddlePoint.Y + 2),
                New Point(MiddlePoint.X, MiddlePoint.Y - 3)
            }
            End Function
#End Region

            Public Sub New(ByVal c As Control)
                NumericUpDownControl = c

                If NumericUpDownControl.IsHandleCreated Then
                    Me.AssignHandle(NumericUpDownControl.Handle)
                Else
                    AddHandler NumericUpDownControl.HandleCreated,
                    Sub(s, e)
                        Me.AssignHandle(NumericUpDownControl.Handle)
                    End Sub
                End If
            End Sub

#Region "Paint Methods"
            Protected Overrides Sub WndProc(ByRef m As Message)
                If m.Msg = WM_PAINT AndAlso CType(NumericUpDownControl.Parent, CustomNumericUpDown).BorderStyle = BorderStyle.FixedSingle Then
                    Dim S As PAINTSTRUCT = New PAINTSTRUCT()

                    Call IntBeginPaint(NumericUpDownControl.Handle, S)

                    Using G As Graphics = Graphics.FromHdc(S.hdc)
                        'Arrow background
                        Using ArrowBackgroundBrush As SolidBrush = New SolidBrush(CType(NumericUpDownControl.Parent, CustomNumericUpDown).BackColor)
                            G.FillRectangle(ArrowBackgroundBrush, NumericUpDownControl.ClientRectangle)
                        End Using

                        'Arrows
                        Using ArrowBrush As SolidBrush = New SolidBrush(CType(NumericUpDownControl.Parent, CustomNumericUpDown).BorderColor)
                            Dim ArrUp As Rectangle = New Rectangle(0, 0, NumericUpDownControl.Width, NumericUpDownControl.Height / 2)
                            Dim ArrDw As Rectangle = New Rectangle(0, NumericUpDownControl.Height / 2, NumericUpDownControl.Width, (NumericUpDownControl.Height / 2) + 1)

                            G.FillPolygon(ArrowBrush, Me.GetUpArrow(ArrUp))
                            G.FillPolygon(ArrowBrush, Me.GetDownArrow(ArrDw))
                        End Using
                    End Using

                    m.Result = CType(1, IntPtr)

                    MyBase.WndProc(m)

                    Call IntEndPaint(NumericUpDownControl.Handle, S)
                ElseIf m.Msg = WM_ERASEBKGND Then
                    Using G As Graphics = Graphics.FromHdcInternal(m.WParam)
                        Using BackgroundBrush As SolidBrush = New SolidBrush(CType(NumericUpDownControl.Parent, CustomNumericUpDown).BackColor)
                            G.FillRectangle(BackgroundBrush, NumericUpDownControl.ClientRectangle)
                        End Using
                    End Using

                    m.Result = CType(1, IntPtr)
                Else
                    MyBase.WndProc(m)
                End If
            End Sub
#End Region
        End Class
    End Class
End Namespace

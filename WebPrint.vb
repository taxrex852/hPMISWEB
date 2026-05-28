Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Diagnostics

Public Class WebPrint
    Dim MyBrowser As WebBrowser
    Dim Height As Integer
    Dim Width As Integer
    Dim URL As String
    Dim PanelTop As Integer

    Sub New(ByVal url As String, ByVal width As Integer, ByVal height As Integer, ByVal paneltop As Integer)
        Me.Height = height
        Me.Width = width
        Me.URL = url
        Me.PanelTop = paneltop
        MyBrowser = New WebBrowser
        MyBrowser.ScrollBarsEnabled = False
        MyBrowser.Size = New Size(Me.Width, Me.Height)
    End Sub

    Function TakeShot() As Bitmap
        Dim myBitmap As New Bitmap(Width, Height)
        Dim DrawRect As New Rectangle(0, 0, Width, Height)

        MyBrowser.Navigate(Me.URL)
        'Waiting for ready
        While MyBrowser.ReadyState <> WebBrowserReadyState.Complete
            Application.DoEvents()
        End While

        'Waiting for ajax
        While MyBrowser.Document.GetElementById("ph_lblShowNow_header").InnerText Is Nothing
            Application.DoEvents()
        End While

        MyBrowser.Document.GetElementById("panel1").ScrollTop = Me.PanelTop
        MyBrowser.DrawToBitmap(myBitmap, DrawRect)
        Return myBitmap
    End Function

End Class

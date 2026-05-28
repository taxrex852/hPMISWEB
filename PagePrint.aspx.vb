Imports System.Drawing.Imaging

Partial Public Class PagePrint
    Inherits System.Web.UI.Page

    Private x As System.Drawing.Bitmap
    Private bDone As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim NewTh As New Threading.Thread(AddressOf PrintIt)
            Dim Requeststring(1) As String
            Dim ParseString As String()

            If Request.QueryString("s") IsNot Nothing Then
                bDone = False
                ParseString = Request.QueryString("s").Split("x")
                If ParseString.Length = 3 Then
                    Requeststring(0) = ParseString(0).Insert(4, "/") & "x" & ParseString(1)
                    Requeststring(1) = ParseString(2)
                    NewTh.SetApartmentState(Threading.ApartmentState.STA)
                    NewTh.Start(Requeststring)

                    While bDone = False
                        Threading.Thread.Sleep(100)
                    End While

                    Response.ContentType = "image/jpeg"
                    'x.GetThumbnailImage(512, 550, Nothing, Nothing).Save(Response.OutputStream, ImageFormat.Jpeg)
                    x.Save(Response.OutputStream, ImageFormat.Jpeg)
                End If
            End If

        End If
    End Sub

    Sub PrintIt(ByVal RequestString As Object)
        Try
            Dim PagePrint As New WebPrint("http://192.168.149.137:8080/hPMIS/3101.aspx?s=" & RequestString(0), 1024, 1100, RequestString(1))
            x = PagePrint.TakeShot()
            bDone = True
        Catch ex As Exception

        End Try
    End Sub

End Class
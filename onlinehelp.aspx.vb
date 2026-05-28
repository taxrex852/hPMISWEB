Imports System.Data.SqlClient

Partial Public Class onlinehelp
    Inherits System.Web.UI.Page
    Private g_sConn As SqlConnection
    Private g_sCmd As SqlCommand
    Private g_strSQL As String = Nothing
    Private g_aDataSet() As String
    Private Const color_idx As Integer = 2


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim HELP_PAGE As String
        If Page.IsPostBack = False Then
            HELP_PAGE = Request("pid")
            Page.Title = "Onlinehelp - " + HELP_PAGE
            g_strSQL = "SELECT page_name FROM sys_page WHERE pid = '" + HELP_PAGE + "' "
            GetData(1)
            lblPageID.Text = HELP_PAGE + " " + g_aDataSet(0)

            ShowMenu(HELP_PAGE)
        End If
    End Sub

    Private Sub GetData(ByVal intCount As Integer, Optional ByVal redimArray As Boolean = True, Optional ByVal startIdx As Integer = 0)
        Dim rd As SqlDataReader = Nothing
        If redimArray Then : ReDim g_aDataSet(intCount) : End If

        Try
            g_sConn = New SqlConnection(getConnStr(Application("ConnStr")))

            g_sCmd = New SqlCommand(g_strSQL, g_sConn)
            g_sConn.Open()
            rd = g_sCmd.ExecuteReader()

            If rd.HasRows Then
                While rd.Read
                    For i As Integer = 0 To rd.FieldCount - 1 Step 1
                        g_aDataSet(i + startIdx) = rd.Item(i).ToString
                    Next
                End While
            End If

            g_sConn.Close()

        Catch sErr As SqlException
            'MsgBox(sErr.Message)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "alert(""" + sErr.Message + """);", True)
        End Try

    End Sub

    Private Sub ShowMenu(ByVal HELP_PAGE As String)
        '列出menu:mtr_name
        Dim dt As DataTable
        Dim dr As DataRow
        Dim dReader As SqlDataReader = Nothing
        Try

            '建立DataTable
            dt = New DataTable
            'dt.Columns.Add(New DataColumn("sn")) 'sn
            dt.Columns.Add(New DataColumn("description")) 'description
            dt.Columns.Add(New DataColumn("formula")) 'formula
            dt.Columns.Add(New DataColumn("backcolor")) 'formula

            '建立連線
            g_sConn = New SqlConnection(getConnStr(Application("ConnStr")))
            g_sConn.Open()
            '產生SQL Statement
            g_strSQL = "SELECT description, formula, backcolor FROM sys_online_help WHERE pid = '" + HELP_PAGE + "' "

            '產生SQL Cmmand
            g_sCmd = New SqlCommand(g_strSQL, g_sConn)
            '執行SQL Command
            dReader = g_sCmd.ExecuteReader()

            If dReader.HasRows Then

                While dReader.Read
                    dr = dt.NewRow

                    'dr("sn") = dReader.GetValue(0).ToString 'sn
                    dr("description") = dReader.GetValue(0).ToString 'description
                    dr("formula") = dReader.GetValue(1).ToString 'formula
                    dr("backcolor") = dReader.GetValue(2).ToString 'backcolor
                    dt.Rows.Add(dr)

                End While
            Else
                dr = dt.NewRow
                For i As Integer = 0 To dt.Columns.Count - 1
                    dr(i) = ""
                Next

                dt.Rows.Add(dr)
            End If
            g_sConn.Close()

            gvOnlinehelp.DataSource = dt
            gvOnlinehelp.DataBind()

            With gvOnlinehelp
                For Each gvr As GridViewRow In .Rows
                    If gvr.Cells(color_idx).Text <> "&nbsp;" And gvr.Cells(color_idx).Text.Trim <> "" Then
                        'If IsNumeric(gvr.Cells(color_idx).Text) Then
                        gvr.BackColor = System.Drawing.Color.FromArgb(Integer.Parse(gvr.Cells(color_idx).Text, System.Globalization.NumberStyles.HexNumber))
                        'End If
                    End If
                Next
            End With

        Catch sErr As SqlException
            showMsgbox(sErr.Message, Me)
        End Try

    End Sub

    Private Sub gvOnlinehelp_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOnlinehelp.RowCreated
        Dim i As Short = 0

        ' Header row
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(color_idx).Visible = False
        End If

        ' 假如資料列的型別是可繫結資料的資料列
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' 該資料列的每一格儲存格
            For Each cell As TableCell In e.Row.Cells
                If i = color_idx Then
                    cell.Visible = False
                End If
                i += 1
            Next
        End If
    End Sub
End Class
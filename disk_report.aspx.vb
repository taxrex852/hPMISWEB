Imports System.Data.SqlClient

Partial Public Class zabbix_report
    Inherits Page
    Protected WithEvents GridView1 As Global.System.Web.UI.WebControls.GridView


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Not IsPostBack Then



        End If






    End Sub
    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dataItem = e.Row.DataItem
            Dim drives As String() = {"C", "D", "E", "F", "G"}

            For Each d As String In drives
                ' 變色條件：剩餘容量少於 10GB 紅色 / 20GB 黃色
                Dim lblGB As Label = CType(e.Row.FindControl("lbl" & d & "FreeGB"), Label)
                If lblGB IsNot Nothing Then
                    Dim valGB As Decimal = SafeDecimal(DataBinder.Eval(dataItem, d & "_Free_space"))
                    ApplyColor(lblGB, valGB, 10, 20)
                End If

                ' 變色條件：剩餘空間% 少於 10% 紅色 / 20% 黃色
                Dim lblPercent As Label = CType(e.Row.FindControl("lbl" & d & "FreePercent"), Label)
                If lblPercent IsNot Nothing Then
                    Dim valPercent As Decimal = SafeDecimal(DataBinder.Eval(dataItem, d & "_Free_Percentage"))
                    ApplyColor(lblPercent, valPercent, 10, 20)
                End If
            Next

            ' 更新日期變色：與現在日期 +- 30分鐘紅色
            Dim lblTime As Label = CType(e.Row.FindControl("lblLastCheck"), Label)
            If lblTime IsNot Nothing Then
                Dim lastCheck As DateTime = SafeDateTime(DataBinder.Eval(dataItem, "LastCheckTime"))
                If lastCheck <> DateTime.MinValue Then
                    Dim hoursDiff As Double = (DateTime.Now - lastCheck).TotalHours
                    Dim MinuteDiff As Double = (DateTime.Now - lastCheck).TotalMinutes
                    If MinuteDiff > 30 Then
                        lblTime.BackColor = System.Drawing.Color.Red
                        lblTime.ForeColor = System.Drawing.Color.White
                    ElseIf MinuteDiff < -30 Then
                        lblTime.BackColor = System.Drawing.Color.Red
                        lblTime.ForeColor = System.Drawing.Color.White
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ApplyColor(lbl As Label, value As Decimal, redLim As Decimal, yellowLim As Decimal)
        If value <= 0 Then Return ' 沒資料不處理
        If value < redLim Then
            lbl.BackColor = System.Drawing.Color.Red
            lbl.ForeColor = System.Drawing.Color.White
        ElseIf value < yellowLim Then
            lbl.BackColor = System.Drawing.Color.Yellow
            lbl.ForeColor = System.Drawing.Color.Black
        End If
    End Sub

    Private Function SafeDecimal(val As Object) As Decimal
        If val Is Nothing OrElse IsDBNull(val) Then Return 0
        Return Convert.ToDecimal(val)
    End Function

    Private Function SafeDateTime(val As Object) As DateTime
        If val Is Nothing OrElse IsDBNull(val) Then Return DateTime.MinValue
        Return Convert.ToDateTime(val)
    End Function

    ''' <summary>
    ''' 當 GridView 資料全數綁定完成後執行儲存格合併
    ''' </summary>
    Protected Sub GridView1_DataBound(sender As Object, e As EventArgs)
        ' 從倒數第二行開始向上比對 (i 為行索引)
        For i As Integer = GridView1.Rows.Count - 2 To 0 Step -1
            Dim currRow As GridViewRow = GridView1.Rows(i)     ' 當前行
            Dim nextRow As GridViewRow = GridView1.Rows(i + 1) ' 下一行

            ' 判斷第一欄 (Cells(0)) 的文字是否相同，且不為空字串
            ' 注意：如果您使用的是 TemplateField 內的 Label，請改用 FindControl 抓取 Label 的 Text
            Dim currText As String = GetCellText(currRow.Cells(0))
            Dim nextText As String = GetCellText(nextRow.Cells(0))

            If currText = nextText AndAlso currText <> "" Then
                ' 1. 設定當前行儲存格的 RowSpan
                ' 如果下一行已經有 RowSpan，則累加；否則設定為 2
                If nextRow.Cells(0).RowSpan < 2 Then
                    currRow.Cells(0).RowSpan = 2
                Else
                    currRow.Cells(0).RowSpan = nextRow.Cells(0).RowSpan + 1
                End If

                ' 2. 隱藏下一行的儲存格 (避免表格破格)
                nextRow.Cells(0).Visible = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' 輔助函式：自動判斷是 BoundField 還是 TemplateField 並取得文字
    ''' </summary>
    Private Function GetCellText(cell As TableCell) As String
        ' 先嘗試找 Label (針對您之前改好的 TemplateField)
        For Each ctrl As Control In cell.Controls
            If TypeOf ctrl Is Label Then
                Return DirectCast(ctrl, Label).Text
            End If
        Next
        ' 如果沒 Label，則視為 BoundField 直接取 Text
        Return cell.Text.Trim()
    End Function
End Class
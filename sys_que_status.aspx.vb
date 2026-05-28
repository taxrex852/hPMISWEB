Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls

Partial Public Class sys_que_status
    Inherits Page
    Protected WithEvents rptQueueStatus As Global.System.Web.UI.WebControls.Repeater
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Dim dtCombined As New DataTable()

        ' === 1. 取得 HPMIS (HSM) 資料 ===
        Using HSMConn As New SqlConnection("Data Source=10.108.20.21;Initial Catalog=HPMIS;User ID=sa;Password=Y6P2!@#")
            ' 直接使用已優化的 View 查詢
            Dim cmdHSM As New SqlCommand("SELECT * FROM vw_sys_questatus WITH (NOLOCK)", HSMConn)
            Dim daHSM As New SqlDataAdapter(cmdHSM)
            daHSM.Fill(dtCombined)
        End Using

        ' === 2. 取得 HBMPMIS (HBM) 資料 ===
        ' 使用與先前相同的 CTE 優化語法，確保欄位名稱與 HSM 完全一致
        Dim sqlHBM As String = "
            WITH QueueStats AS (
                SELECT 'di_pdi_hbmmil' AS QueueName,
                       (SELECT COUNT(*) FROM [dbo].[di_pdi_hbmmil] WITH (NOLOCK) WHERE STATUS = 'N') AS sys_status,
                       (SELECT TOP 1 NULLIF(PROCESSTIME, '') FROM [dbo].[di_pdi_hbmmil] WITH (NOLOCK) ORDER BY PROCESSTIME DESC) AS LatestProcesstime,
                       (SELECT TOP 1 NULLIF(PROCESSTIME, '') FROM [dbo].[di_pdi_hbmmil] WITH (NOLOCK) WHERE STATUS = 'N' ORDER BY PROCESSTIME DESC) AS LatestProcesstime_N
                UNION ALL
                SELECT 'di_pdo_hbmmil',
                       (SELECT COUNT(*) FROM [dbo].[di_pdo_hbmmil] WITH (NOLOCK) WHERE STATUS = 'N'),
                       (SELECT TOP 1 NULLIF(PROCESSTIME, '') FROM [dbo].[di_pdo_hbmmil] WITH (NOLOCK) ORDER BY PROCESSTIME DESC),
                       (SELECT TOP 1 NULLIF(PROCESSTIME, '') FROM [dbo].[di_pdo_hbmmil] WITH (NOLOCK) WHERE STATUS = 'N' ORDER BY PROCESSTIME DESC)
            )
            SELECT 
                QueueName,
                sys_status,
                LatestProcesstime,
                DATEDIFF(MINUTE, CONVERT(DATETIME, LEFT(LatestProcesstime, 8) + ' ' + SUBSTRING(LatestProcesstime, 9, 2) + ':' + SUBSTRING(LatestProcesstime, 11, 2) + ':' + RIGHT(LatestProcesstime, 2)), GETDATE()) AS Datetimediff,
                ISNULL(DATEDIFF(MINUTE, CONVERT(DATETIME, LEFT(LatestProcesstime_N, 8) + ' ' + SUBSTRING(LatestProcesstime_N, 9, 2) + ':' + SUBSTRING(LatestProcesstime_N, 11, 2) + ':' + RIGHT(LatestProcesstime_N, 2)), GETDATE()), 0) AS DatetimediffForStatusN
            FROM QueueStats;"

        Using HBMConn As New SqlConnection("Data Source=10.108.20.11;Initial Catalog=HBMPMIS;User ID=sa;Password=Y6P2!@#")
            Dim cmdHBM As New SqlCommand(sqlHBM, HBMConn)
            Dim daHBM As New SqlDataAdapter(cmdHBM)
            Dim dtHBM As New DataTable()
            daHBM.Fill(dtHBM)

            ' 將 HBM 兩筆資料合併到同一張 DataTable
            dtCombined.Merge(dtHBM)
        End Using

        ' === 3. 綁定至前端 Repeater ===
        rptQueueStatus.DataSource = dtCombined
        rptQueueStatus.DataBind()
    End Sub

    ' Repeater 逐列綁定時觸發：用來判斷警告顏色與隱藏數值 0
    Protected Sub rptQueueStatus_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' 取得當前列的資料
            Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)

            Dim sysStatusCount As Integer = 0
            Dim diffStatusN As Integer = 0

            Integer.TryParse(drv("sys_status").ToString(), sysStatusCount)
            Integer.TryParse(drv("DatetimediffForStatusN").ToString(), diffStatusN)

            ' 尋找前端控制項
            Dim trRow As HtmlTableRow = CType(e.Item.FindControl("trRow"), HtmlTableRow)
            Dim litStatusN As Literal = CType(e.Item.FindControl("litStatusN"), Literal)

            ' 邏輯 1：如果狀態為 N 的時間差為 0，則畫面不顯示數字（保持空白）
            If diffStatusN = 0 Then
                litStatusN.Text = ""
            End If

            ' 邏輯 2：若 N數量 >= 10，或 N的時間差 >= 10，整行亮紅燈警告
            If sysStatusCount >= 10 OrElse diffStatusN >= 10 Then
                ' 加入 Bootstrap 5 的紅色警示 Class
                trRow.Attributes("class") = "table-danger text-danger fw-bold"
            End If

        End If
    End Sub

End Class
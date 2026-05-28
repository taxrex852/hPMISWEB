Partial Public Class Home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'btn1tnrl_Produce.Enabled = False
        'btn2tnrl_Produce.Enabled = False
        If Page.IsPostBack = False Then
            setTitle(Me, "0004")

            btn3tnrl_Process.Enabled = False
            btn3tnrl_Produce.Enabled = False
            btn3tnrl_Defect.Enabled = False
            btn3tnrl_Delay.Enabled = False
            btn3tnrl_Production.Enabled = False

            btnSteel_Process.Enabled = False
            btnSteel_Produce.Enabled = False
            btnSteel_Defect.Enabled = False
            btnSteel_Delay.Enabled = False
            btnSteel_Production.Enabled = False

            btnPol_Process.Enabled = False
            btnPol_Produce.Enabled = False
            btnPol_Defect.Enabled = False
            btnPol_Delay.Enabled = False
            btnPol_Production.Enabled = False
        End If
    End Sub

    Private Sub linkURL(ByVal pid As Integer)
        Dim aURL As String = System.Web.Configuration.WebConfigurationManager.AppSettings("hPmisUrl").ToString
        Dim bURL As String = ""

        Select Case pid
            Case 1110
                bURL = "110A.aspx"
            Case 1000
                bURL = "Working.aspx"
            Case Else
                bURL = pid.ToString + ".aspx"
        End Select

        Response.Redirect(aURL + bURL)
    End Sub

    Protected Sub btnHsm_Process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHsm_Process.Click
        Response.Redirect("3501.aspx")
    End Sub

    Protected Sub btn1tnrl_Process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1tnrl_Process.Click
        Response.Redirect("3502.aspx")
    End Sub

    Protected Sub btn2tnrl_Process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2tnrl_Process.Click
        Response.Redirect("3503.aspx")
    End Sub

    Protected Sub btnHsm_Produce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHsm_Produce.Click
        Response.Redirect("3101.aspx")
    End Sub

    Protected Sub btn1tnrl_Produce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1tnrl_Produce.Click
        Response.Redirect("3102.aspx")
    End Sub

    Protected Sub btn2tnrl_Produce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2tnrl_Produce.Click
        Response.Redirect("3103.aspx")
    End Sub

    Protected Sub btnHsm_Defect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHsm_Defect.Click
        Response.Redirect("3201.aspx")
    End Sub

    Protected Sub btn1tnrl_Defect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1tnrl_Defect.Click
        Response.Redirect("3202.aspx")
    End Sub

    Protected Sub btn2tnrl_Defect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2tnrl_Defect.Click
        Response.Redirect("3203.aspx")
    End Sub

    Protected Sub btnHsm_Delay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHsm_Delay.Click
        Response.Redirect("3301.aspx")
    End Sub

    Protected Sub btn1tnrl_Delay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1tnrl_Delay.Click
        Response.Redirect("3302.aspx")
    End Sub

    Protected Sub btn2tnrl_Delay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2tnrl_Delay.Click
        Response.Redirect("3303.aspx")
    End Sub

    Protected Sub btnHsm_Production_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHsm_Production.Click
        Response.Redirect("3401.aspx")
    End Sub

    Protected Sub btn1tnrl_Production_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1tnrl_Production.Click
        Response.Redirect("3402.aspx")
    End Sub

    Protected Sub btn2tnrl_Production_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2tnrl_Production.Click
        Response.Redirect("3403.aspx")
    End Sub

    Protected Sub lbtn3101_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3101.Click
        linkURL(3101)
    End Sub

    Protected Sub lbtn3201_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3201.Click
        linkURL(3201)
    End Sub

    'Protected Sub lbtn3301_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3301.Click
    '    linkURL(3301)
    'End Sub

    Protected Sub lbtn3401_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3401.Click
        linkURL(3401)
    End Sub

    Protected Sub lbtn3501_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3501.Click
        linkURL(3501)
    End Sub

    Protected Sub lbtn3102_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3102.Click
        linkURL(3102)
    End Sub

    Protected Sub lbtn3202_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3202.Click
        linkURL(3202)
    End Sub

    'Protected Sub lbtn3302_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3302.Click
    '    linkURL(3302)
    'End Sub

    Protected Sub lbtn3402_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3402.Click
        linkURL(3402)
    End Sub

    Protected Sub lbtn3502_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3502.Click
        linkURL(3502)
    End Sub

    Protected Sub lbtn3103_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3103.Click
        linkURL(3103)
    End Sub

    Protected Sub lbtn3203_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3203.Click
        linkURL(3203)
    End Sub

    'Protected Sub lbtn3303_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3303.Click
    '    linkURL(3303)
    'End Sub

    Protected Sub lbtn3403_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3403.Click
        linkURL(3403)
    End Sub

    Protected Sub lbtn3503_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3503.Click
        linkURL(3503)
    End Sub

    Protected Sub lbtn3601_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3601.Click
        linkURL(3601)
    End Sub

    Protected Sub lbtn3602_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3602.Click
        linkURL(3602)
    End Sub

    Private Sub lbtn3104_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn3104.Click
        linkURL(3104)
    End Sub

    Private Sub lbtn3204_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn3204.Click
        linkURL(3204)
    End Sub

    Private Sub lbtn3404_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn3404.Click
        linkURL(3404)
    End Sub

    Private Sub lbtn3504_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn3504.Click
        linkURL(3504)
    End Sub

    Protected Sub lbtn3105_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3105.Click
        linkURL(3105)
    End Sub

    Protected Sub lbtn3701_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3701.Click
        linkURL(3701)
        'linkURL(1000)
    End Sub

    Protected Sub lbtn3702_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3702.Click
        linkURL(3702)
        'linkURL(1000)
    End Sub

    Protected Sub lbtn3703_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3703.Click
        linkURL(3703)
        'linkURL(1000)
    End Sub

    Protected Sub lbtnout1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnout1.Click
        'Response.Redirect("http://10.102.150.1/dsccenter/")
        Response.Redirect("http://w2mes.dsc.com.tw//dsccenter/Info01_1280.aspx")
    End Sub

    Protected Sub lbtnout2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnout2.Click
        'Response.Redirect("http://10.101.81.10/production.php")
        'Response.Redirect("http://prod.cdgs.com.tw/erp/wk/jsp/wkjjCDGSDRO.jsp")  'Updated by Jimmy Chang 102.09.24 
        Response.Redirect("http://prod.cdgs.com.tw/erp/ds/jsp/dsjjIpSignOn.jsp?infoId=WKJJCDGSDRO&infoType=A")  'Updated by Jimmy Chang 102.10.16 
    End Sub


    Protected Sub LinkButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        Response.Redirect("http://W2MES.DSC.COM.TW/110B.ASPX")
    End Sub


    Private Sub lbtn3106_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn3106.Click
        linkURL(3106)
    End Sub

    Private Sub lbtn3206_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn3206.Click
        linkURL(3206)
    End Sub

    Private Sub lbtn3406_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn3406.Click
        linkURL(3406)
    End Sub

    Private Sub lbtn3506_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtn3506.Click
        linkURL(3506)
    End Sub

    Protected Sub lbtn3107_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3107.Click
        linkURL(3107)
    End Sub

    Protected Sub lbtn3207_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3207.Click
        linkURL(3207)
    End Sub

    Protected Sub lbtn3407_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3407.Click
        linkURL(3407)
    End Sub

    Protected Sub lbtn3507_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtn3507.Click
        linkURL(3507)
    End Sub
End Class
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Orders

Partial  Class Instruction
    Inherits CWebControl
    Protected WithEvents InstructCell As HtmlTableCell

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strPage As String = Request.Url.ToString ' CurrentWebPage()

        If (strPage.ToLower().IndexOf("search.aspx") > -1) Then
            If (strPage.ToLower.IndexOf("advanced=1") > -1) Then
                strPage = "Search.aspx?Advanced=1"
            Else
                strPage = "Search.aspx"
            End If
        Else
            If (strPage.IndexOf("?") > -1) Then
                strPage = strPage.Substring(0, strPage.LastIndexOf("?") + 1)
                strPage = strPage.Substring(strPage.LastIndexOf("/") + 1, strPage.LastIndexOf("?") - 1 - strPage.LastIndexOf("/"))
            Else
                strPage = strPage.Substring(strPage.LastIndexOf("/") + 1)
            End If
        End If

        If (strPage.ToLower().IndexOf("confirm.aspx") <> -1) Then
            If (IsNothing(Session("PFInstruct")) = False) Then
                strPage = "Confirm.aspx(PF)"
                Session("PFInstruct") = Nothing
            End If
        End If

        Instruct.Text = m_objInstructions.GetXMLInstructions(strPage)
        If (Instruct.Text.Trim.Length = 0) Then
            Me.Visible = False
        End If
        Dim objDesign As New CDesign(dom.Item("SiteProducts").Item("SiteDesign").Item("Instruction"))
        InstructCell.Align = objDesign.HorizontalAlignment
    End Sub
    Public Sub Set_LabelText(ByVal str As String)
        Instruct.Text = str
    End Sub
End Class


Public Class Geography
    Inherits CWebPage
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Geography As GeographyControl
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
        CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
        CType(Geography.FindControl("cmdAddCountryToActive"), LinkButton).Attributes.Add("onclick", "return SetValidationAddCountry();")
        CType(Geography.FindControl("cmdAddStateToActive"), LinkButton).Attributes.Add("onclick", "return SetValidationAddState();")
        'Put user code to initialize the page here
    End Sub

End Class

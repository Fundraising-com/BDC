Imports StoreFront.SystemBase
Partial Class Geography
    Inherits CWebPage
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
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(tasks.localization) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
        CType(Geography.FindControl("cmdAddCountryToActive"), LinkButton).Attributes.Add("onclick", "return SetValidationAddCountry();")
        CType(Geography.FindControl("cmdAddStateToActive"), LinkButton).Attributes.Add("onclick", "return SetValidationAddState();")
        'Put user code to initialize the page here
    End Sub

End Class

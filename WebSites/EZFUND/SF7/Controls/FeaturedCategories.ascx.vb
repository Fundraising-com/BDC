Imports StoreFront.BusinessRule

Partial Class FeaturedCategories
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents grdFeaturedCats As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.Visible = False Then Exit Sub

        BindDatagrid()

    End Sub

    Private Sub BindDatagrid()
        Dim ar As New ArrayList
        Dim objAccess As New CCategories

        ar = objAccess.GetFeaturedCategories
        If ar.Count > 0 Then
            dlFeaturedCats.DataSource = ar
            dlFeaturedCats.DataBind()

            dlFeaturedCats.ItemStyle.Width = New Unit("30%")
        Else
            lblFCatError.Text = "There are no Featured Categories at this time."
            dlFeaturedCats.Visible = False
        End If


    End Sub
End Class

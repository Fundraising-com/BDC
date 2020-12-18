Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule

Partial Public Class AdvancedOptions
    Inherits CWebPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' pass 0 into the restrict pages method so only the built in administrator can access this page
        If MyBase.RestrictedPages(0) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
        btnDelProds.Attributes.Add("OnClick", "javascript:return confirm('All product related data including bundles will be removed, are you sure?');")
        btnDelBundles.Attributes.Add("OnClick", "javascript:return confirm('All bundle related data will be removed, are you sure?');")
        btnDelCats.Attributes.Add("OnClick", "javascript:return confirm('All category related data will be removed, are you sure?');")
    End Sub

    Private Sub btnDelProds_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelProds.Click
        Dim objProdMng As New CProductManagement
        If objProdMng.DeleteAllProducts Then
            ErrorMessage.Text = "All product related data has been deleted successfully."
            ErrorMessage.Visible = True
        Else
            ErrorMessage.Text = "There has been an error deleting product related data, please contact your system administrator."
            ErrorMessage.Visible = True
        End If
    End Sub

    Private Sub btnDelBundles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelBundles.Click
        Dim objProdBundle As New CProductBundle
        If objProdBundle.DeleteAllBundles Then
            ErrorMessage.Text = "All bundle related data has been deleted successfully."
            ErrorMessage.Visible = True
        Else
            ErrorMessage.Text = "There has been an error deleting bundle related data, please contact your system administrator."
            ErrorMessage.Visible = True
        End If
    End Sub

    Private Sub btnDelCats_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelCats.Click
        Dim objCat As New CCategories
        If objCat.DeleteAllCategories Then
            ErrorMessage.Text = "All category related data has been deleted successfully."
            ErrorMessage.Visible = True
        Else
            ErrorMessage.Text = "There has been an error deleting category related data, please contact your system administrator."
            ErrorMessage.Visible = True
        End If
    End Sub
End Class
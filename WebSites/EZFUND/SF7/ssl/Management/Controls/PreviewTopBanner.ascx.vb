Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class PreviewTopBanner
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
        'Put user code to initialize the page here
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllLayoutPreviewByAreaName("TopBanner")
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)("DisplayStyle") = DisplayStyle.None Then
                StoreImage.Visible = False
                StoreName.Visible = False
            Else
                If ds.Tables(0).Rows(0)("DisplayStyle") = DisplayStyle.PageName OrElse IsDBNull(ds.Tables(0).Rows(0)("ImageURL")) Then
                    StoreName.Text = StoreFrontConfiguration.StoreName
                    StoreImage.Visible = False
                    StoreName.Visible = True
                Else
                    StoreImage.ImageUrl = ds.Tables(0).Rows(0)("ImageURL")
                    StoreImage.Visible = True
                    StoreName.Visible = False
                End If
            End If
        End If
    End Sub

End Class

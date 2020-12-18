Imports StoreFront.BusinessRule

Partial Class PreviewFooterNav
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
    Protected WithEvents CMenuBar1 As PreviewMenuBar

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        CMenuBar1.StyleClass = "FooterText"
        CMenuBar1.CurrentRegion = "FooterNav"
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllLayoutPreviewByAreaName("Footer")
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("Visible")) AndAlso ds.Tables(0).Rows(0)("Visible") = 1 Then
            End If
        Else
            CMenuBar1.Visible = False
        End If

        ds = myDesignManager.GetAllImagesPreviewByName("LagardeLogo")
        Me.imgLagardeLogo.ImageUrl = ds.Tables(0).Rows(0)("Filename")
    End Sub

End Class

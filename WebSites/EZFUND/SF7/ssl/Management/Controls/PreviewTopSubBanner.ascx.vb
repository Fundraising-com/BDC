Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class PreviewTopSubBanner
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
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllLayoutPreviewByAreaName("TopSubBanner")
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)("DisplayStyle") = DisplayStyle.None Then
                PageImage.Visible = False
                CMenuBar1.Visible = False
                Label.Visible = False
            ElseIf ds.Tables(0).Rows(0)("DisplayStyle") = DisplayStyle.Image Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("ImageURL")) AndAlso CType(ds.Tables(0).Rows(0)("ImageURL"), String).ToLower.StartsWith("http") Then
                    PageImage.ImageUrl = ds.Tables(0).Rows(0)("ImageURL")
                Else
                    PageImage.ImageUrl = StoreFrontConfiguration.SSLPath & "images/" & ds.Tables(0).Rows(0)("ImageURL")
                End If
                PageImage.Visible = True
                CMenuBar1.Visible = False
                Label.Visible = False
            ElseIf ds.Tables(0).Rows(0)("DisplayStyle") = DisplayStyle.NavLinks Then
                    PageImage.Visible = False
                    Label.Visible = False
                    CMenuBar1.Visible = True
                    CMenuBar1.StyleClass = "TopSubBannerText"
                    CMenuBar1.CurrentRegion = "TopSubBanner"
            Else
                    Label.Visible = True
                    PageImage.Visible = False
                    CMenuBar1.Visible = False
                End If
        End If
    End Sub

End Class

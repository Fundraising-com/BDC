'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports System.Xml

Public Class ProductSalesSelect
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents DateInfo As System.Web.UI.WebControls.Label
    Protected WithEvents ddProducts As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtProd As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddCat As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents cmdSubmit As System.Web.UI.WebControls.LinkButton

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

        Try
            cmdSubmit.Attributes.Add("onclick", "return SetValidation();")
            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("Managementdefault.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)

            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            If Not IsPostBack Then

                Dim iDateRange As Integer = Request.QueryString("DateType")
                Dim sTo As String = Request.QueryString("To")
                Dim sFrom As String = Request.QueryString("From")
                If iDateRange = 0 Then
                    Exit Sub
                Else
                    Dim objDate As New CSearchDate(iDateRange, sFrom, sTo)
                    DateInfo.Text = objDate.DateDisplay
                    Dim objReport As New CSFReports(objDate)
                    loadProducts(objReport)
                    LoadCats()
                End If
            End If
        Catch ex As Exception
            Session("DetailError") = "Class ProductSelect Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub loadProducts(ByVal oReport As CSFReports)


        ddProducts.DataValueField = "Id"
        ddProducts.DataTextField = "Name"

        Dim ar As ArrayList
        ar = oReport.ProductsSold
        Dim oDD As New CGenericDDContainer()

        oDD.ID = "-1"
        oDD.Name = "Product ID - Product Name"


        ar.Insert(0, oDD)

        ddProducts.DataSource = ar
        ddProducts.DataBind()




    End Sub

    Private Sub LoadCats()
        'Dim objCategoryAccess As CXMLCategoryAccess = StoreFrontConfiguration.CategoryAccess
        Dim arList As ArrayList
        Dim objCategoryAccess As New CCategories()

        arList = objCategoryAccess.Categories(0, , True)

        Dim objCategory As New CCategory()
        objCategory.ID = -1
        objCategory.Name = "Category Name"

        arList.Insert(0, objCategory)

        ddCat.DataSource = arList
        ddCat.DataBind()

    End Sub

    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        Dim sAction As String

        'Validation
        If txtProd.Text = "" And ddProducts.SelectedItem.Value = "" And ddCat.SelectedItem.Value = "" Then
            Err.Description = m_objMessages.GetXMLMessage("ProductSelect.aspx", "ViewProductSales", "NoSelection")
        End If
        'End Validation

        If txtProd.Text <> "" Then
            sAction = "FreeText=" & txtProd.Text
        ElseIf ddProducts.SelectedItem.Value <> "-1" Then
            sAction = "Product=" & ddProducts.SelectedItem.Value
        ElseIf ddCat.SelectedItem.Value <> "-1" Then
            sAction = "Category=" & ddCat.SelectedItem.Value
        End If
        If sAction <> "" Then
            Dim sCriteria As String
            Dim i As Integer
            For i = 0 To Request.QueryString.Count - 1
                sCriteria = sCriteria & "&" & Request.QueryString.Keys(i) & "=" & Request.QueryString.Item(i).ToString
            Next
            Response.Redirect("ProductSales.aspx?" & sAction & sCriteria)      '& "&DateType=" & iDateRange)
        End If
    End Sub

    Private Sub ddProducts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddProducts.SelectedIndexChanged
        If sender.SelectedItem.Value <> "-1" Then
            txtProd.Text = ""
            ddCat.SelectedIndex = 0
        End If
    End Sub

    Private Sub ddCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddCat.SelectedIndexChanged
        If sender.SelectedItem.Value <> "-1" Then
            txtProd.Text = ""
            ddProducts.SelectedIndex = 0
        End If
    End Sub

End Class

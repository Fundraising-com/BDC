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

Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public Class multiselect
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents btnBack As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.LinkButton
    Protected WithEvents StandardSearchControl1 As standardsearchcontrol2
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected arrChecked As ArrayList

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
    Private ProdID As Long
    'TODO Need Images
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            'Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("selectlistpage.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)

            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            'Session("ApplyTo") = 1
            'arrChecked = New ArrayList()
            'arrChecked.Add(CLng(1))
            'arrChecked.Add(CLng(2))
            'arrChecked.Add(CLng(3))
            'arrChecked.Add(CLng(4))
            'arrChecked.Add(CLng(5))
            'Session("ArrChecked") = arrChecked

            'If Session("ArrChecked").contains(CLng(1)) Then
            '    Session("ApplyTo") = 1
            'Else
            '    Session("ApplyTo") = 100
            'End If
            ProdID = Session("ProdID")
            If (IsPostBack = False) Then
                If (Session("ApplyTo") = "2") Then
                    LoadCategory()
                ElseIf (Session("ApplyTo") = "1") Then
                    LoadProducts()
                ElseIf (Session("ApplyTo") = "3") Then
                    LoadVendor()
                ElseIf (Session("ApplyTo") = "4") Then
                    LoadManufacturer()
                ElseIf (Session("ApplyTo") = "5") Then
                    LoadGetRelatedProductsSelection(ProdID)
                ElseIf (Session("ApplyTo") = "100") Then
                    LoadCustomers()
                End If
            End If
        Catch ex As Exception
            Session("DetailError") = "Class MultiSelect Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Sub BackClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Session("ApplyToID") = 0
        Session("ArrChecked") = Nothing
        Response.Redirect(Session("ReturnPage"))
    End Sub

    Public Sub SubmitClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Response.Redirect(Session("ReturnPage"))
    End Sub

    Private Sub LoadCustomers()
        Dim objStorage As New CSearchControlStorage()

        ' Set the display properties
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        'objStorage.SortList = arSort
        objStorage.DataSource = m_objCustomer.GetCustomers()
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Select Customer"

        'Add the Columns
        Dim ar As New ArrayList()
        ar.Add("LastName")
        ar.Add("FirstName")
        ar.Add("Email")
        objStorage.ColumnList = ar

        StandardSearchControl1.CategoryNodes = False
        StandardSearchControl1.StorageClass = objStorage
    End Sub

    Private Sub LoadProducts()
        Dim objStorage As New CSearchControlStorage()
        Dim objProducts As New CStoreProducts()

        ' Set the display properties
        objStorage.ButtonID = "ProductID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        objStorage.DataSource = objProducts.GetAllProducts()
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Select Product"

        ' Add the Columns
        Dim ar As New ArrayList()
        ar.Add("Name")
        ar.Add("ProductCode")

        objStorage.ColumnList = ar

        StandardSearchControl1.CategoryNodes = False
        StandardSearchControl1.StorageClass = objStorage
    End Sub

    Private Sub LoadCategory()
        Dim objStorage As New CSearchControlStorage()
        Dim objCategories As New CCategories()

        ' Set the display properties
        objStorage.NoSort = True
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        objStorage.DataSource = objCategories.GetAllCategories
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Select Category"

        ' Add the Columns
        Dim ar As New ArrayList()
        ar.Add("Name")

        objStorage.ColumnList = ar

        StandardSearchControl1.CategoryNodes = True
        StandardSearchControl1.StorageClass = objStorage
    End Sub

    Private Sub LoadVendor()
        Dim objStorage As New CSearchControlStorage()
        Dim objVen As New CVendor()

        ' Set the display properties
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        objStorage.DataSource = objVen.GetAllVendors()
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Select Vendor"

        ' Add the Columns
        Dim ar As New ArrayList()
        ar.Add("Company")

        objStorage.ColumnList = ar

        StandardSearchControl1.CategoryNodes = False
        StandardSearchControl1.StorageClass = objStorage
    End Sub

    Private Sub LoadManufacturer()
        Dim objStorage As New CSearchControlStorage()
        Dim objMan As New CManufacturer()

        ' Set the display properties
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        objStorage.DataSource = objMan.GetAllManufacturers()
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Select Manufacturer"

        ' Add the Columns
        Dim ar As New ArrayList()
        ar.Add("Company")

        objStorage.ColumnList = ar

        StandardSearchControl1.CategoryNodes = False
        StandardSearchControl1.StorageClass = objStorage
    End Sub

    Private Sub LoadGetRelatedProductsSelection(ByVal uid)
        Dim objStorage As New CSearchControlStorage()
        Dim objProducts As New CStoreProducts()

        ' Set the display properties
        objStorage.ButtonID = "ProductID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        objStorage.DataSource = objProducts.GetRelatedProductsSelection(uid)
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Select Product"

        ' Add the Columns
        Dim ar As New ArrayList()
        ar.Add("Name")
        ar.Add("ProductCode")

        objStorage.ColumnList = ar

        StandardSearchControl1.CategoryNodes = False
        StandardSearchControl1.StorageClass = objStorage
    End Sub

    Private Sub StandardSearchControl1_CheckClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.CheckClick
        Session("ApplyToID") = sender
    End Sub

End Class

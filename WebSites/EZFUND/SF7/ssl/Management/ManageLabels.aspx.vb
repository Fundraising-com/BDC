Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

'------------------------------------------------------------------------
'Class Summary
'------------------------------------------------------------------------
'Allows the viewing and editing of the stores labels stored in the labels
'table.
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Partial Class ManageLabels
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable

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
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

       
    End Sub

    Public Sub Bind()
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Binds the control fields to the the Labels table columns they represent 
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllLabels()
        Dim dt As DataTable = ds.Tables(0)

        ProductCode.Text = dt.Rows(0).Item("ProductCode")
        CategoryPlural.Text = dt.Rows(0).Item("CategoryPlural")
        If StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "se" Then
            trStockStatus.Visible = False
        Else
            trStockStatus.Visible = True
            Stock.Text = dt.Rows(0).Item("Stock")
        End If
        VendorPlural.Text = dt.Rows(0).Item("VendorPlural")
        Vendor.Text = dt.Rows(0).Item("Vendor")
        Description.Text = dt.Rows(0).Item("Description")
        SalePrice.Text = dt.Rows(0).Item("SalePrice")
        Manufacturer.Text = dt.Rows(0).Item("Manufacturer")
        VolumePrice.Text = dt.Rows(0).Item("VolumePrice")
        Price.Text = dt.Rows(0).Item("Price")
        Category.Text = dt.Rows(0).Item("Category")
        ManufacturerPlural.Text = dt.Rows(0).Item("ManufacturerPlural")
        MoreInfo.Text = dt.Rows(0).Item("MoreInfo")
        ProductName.Text = dt.Rows(0).Item("ProductName")
        'Tee 9/17/2007 clearance activation
        txtClearance.Text = dt.Rows(0).Item("ClearancePrice")
        'end Tee
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        'Populate form if this is not a post back
        If Not IsPostBack Then
            Me.Bind()
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdSave.Click
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Saves changes made to form fields back to the corresponding column in the
        'database.  All labels are columns in only one row.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllLabels()
        Dim dt As DataTable = ds.Tables(0)

        dt.Rows(0).Item("ProductCode") = ProductCode.Text
        dt.Rows(0).Item("CategoryPlural") = CategoryPlural.Text
        If trStockStatus.Visible = True Then
            dt.Rows(0).Item("Stock") = Stock.Text
        End If
        dt.Rows(0).Item("VendorPlural") = VendorPlural.Text
        dt.Rows(0).Item("Vendor") = Vendor.Text
        dt.Rows(0).Item("Description") = Description.Text
        dt.Rows(0).Item("SalePrice") = SalePrice.Text
        dt.Rows(0).Item("Manufacturer") = Manufacturer.Text
        dt.Rows(0).Item("VolumePrice") = VolumePrice.Text
        dt.Rows(0).Item("Price") = Price.Text
        dt.Rows(0).Item("Category") = Category.Text
        dt.Rows(0).Item("ManufacturerPlural") = ManufacturerPlural.Text
        dt.Rows(0).Item("MoreInfo") = MoreInfo.Text
        dt.Rows(0).Item("ProductName") = ProductName.Text
        'Tee 9/17/2007 clearance activation
        dt.Rows(0).Item("ClearancePrice") = txtClearance.Text
        'end Tee
        myDesignManager.UpdateLabels(ds)
        Me.ErrorMessage.Visible = True
        Me.ErrorMessage.Text = "Your changes have been saved"
    End Sub
End Class

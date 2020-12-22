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
'Allows viewing and editing of the settings for the stores search results
'or Catalog pages.
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Partial Class CatalogPages
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAttributes As System.Web.UI.WebControls.Label
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
            cmdSave.Attributes.Add("onclick", "javascript:return Validate();")
            Me.ErrorMessage.Visible = False
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
        Bind()
    End Sub
    Public Sub Bind()
        If Not IsPostBack Then
            Me.BindTemplate()
        End If
    End Sub

    Public Sub BindTemplate()

        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Binds the fields to the template marked as active
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        'Fill datatable we use as datasource for the control
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet
        Dim dt As New DataTable
        ds = myDesignManager.GetAllActiveSearchResults()
        dt = ds.Tables(0)
        Select Case CInt(dt.Rows(0).Item("Type"))
            Case 1
                Me.rbtemplate1.Checked = True
            Case 2
                Me.rbTemplate2.Checked = True
            Case 3
                Me.rbTemplate3.Checked = True
        End Select
        ControlVisibility()

        Me.txtRows.Text = dt.Rows(0).Item("NumOfRows")
        If Me.txtProducts.Visible = True Then
            Me.txtProducts.Text = dt.Rows(0).Item("ProductsPerRow")
        End If

        Dim dr As DataRow = dt.Rows(0)
        'Do the rest
        Me.chkProductID.Checked = dr.Item("DisplayProductCode")
        Me.chkProductName.Checked = dr.Item("DisplayProductName")
        If Me.chkShortDescription.Visible = True Then
            Me.chkShortDescription.Checked = dr.Item("DisplayShortDescription")
        End If
        If Me.chkSmallImage.Visible = True Then
            Me.chkSmallImage.Checked = dr.Item("DisplayImage")
        End If
        If Me.chkStockInfo.Visible = True Then
            Me.chkStockInfo.Checked = dr.Item("DisplayStockInfo")
        End If
        If Me.chkVendor.Visible = True Then
            Me.chkVendor.Checked = dr.Item("DisplayVendor")
        End If
        Me.chkWishList.Checked = dr.Item("DisplaySavedCartWishList")
        Me.chkAddToCart.Checked = dr.Item("DisplayAddToCart")
        Me.chkDetailLink.Checked = dr.Item("DisplayMoreInfo")
        Me.chkEmailAFriend.Checked = dr.Item("DisplayEmailFriend")
        If Me.chkManufacturer.Visible = True Then
            Me.chkManufacturer.Checked = dr.Item("DisplayManufacturer")
        End If
        If Me.chkVolumePricing.Visible = True Then
            Me.chkVolumePricing.Checked = dr.Item("DisplayVolumePricing")
        End If
        Me.chkPCodeLink.Checked = dr.Item("LinkProductCode")
        Me.chkPNameLink.Checked = dr.Item("LinkProductName")
        Me.chkPrice.Checked = dr.Item("DisplayPriceSalePrice")
        If Me.ddlAlignment.Visible = True Then
            Me.ddlAlignment.SelectedValue = dr.Item("Alignment")
        End If
        If Me.chkLabels.Visible = True Then
            Me.chkLabels.Checked = dr.Item("DisplayLabels")
        End If
        If Me.chkSImageLink.Visible = True Then
            Me.chkSImageLink.Checked = dr.Item("LinkImage")
        End If
        Me.rblAttributeType.SelectedValue = dr.Item("AttributeDisplay")
        BindAddToCart(CType(dr.Item("DisplayQty"), Boolean))
    End Sub

    Public Sub BindTemplate(ByVal templateType As Long)
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Binds the control fields to the template which has the passed type value
        'OPTIONS:
        '1 = SearchTemplate1
        '2 = SearchTemplate2
        '3 = SearchTemplate3
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------

        'Fill datatable we use as datasource for the control
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet
        Dim dt As New DataTable
        ds = myDesignManager.GetAllSearchResultsByType(templateType)
        dt = ds.Tables(0)
        'Select the correct RadioButton based on value in the datarow
        Select Case CInt(dt.Rows(0).Item("Type"))
            Case 1
                Me.rbtemplate1.Checked = True
            Case 2
                Me.rbTemplate2.Checked = True
            Case 3
                Me.rbTemplate3.Checked = True
        End Select

        ControlVisibility()

        Me.txtRows.Text = dt.Rows(0).Item("NumOfRows")
        If Me.txtProducts.Visible = True Then
            Me.txtProducts.Text = dt.Rows(0).Item("ProductsPerRow")
        End If

        'Get a DataRow to decrease typing when assigning values
        Dim dr As DataRow = dt.Rows(0)

        'Do the rest of the form fields
        Me.chkProductID.Checked = dr.Item("DisplayProductCode")
        Me.chkProductName.Checked = dr.Item("DisplayProductName")
        If Me.chkShortDescription.Visible = True Then
            Me.chkShortDescription.Checked = dr.Item("DisplayShortDescription")
        End If
        If Me.chkVolumePricing.Visible = True Then
            Me.chkVolumePricing.Checked = dr.Item("DisplayVolumePricing")
        End If
        If Me.chkSmallImage.Visible = True Then
            Me.chkSmallImage.Checked = dr.Item("DisplayImage")
        End If
        If Me.ddlAlignment.Visible = True Then
            'begin: GJV - 2/5/2008 - # 722 - error selecting alignment due to deprecated value

            'Me.ddlAlignment.SelectedValue = dr.Item("Alignment")

            'note: ive added a statement to the dbupdate to clean up any bad alignment data; 
            '      this should resolve the issue but just in case, lets safely step through 
            '      each list item ensuring our selection is valid
            Dim sValue As String = dr.Item("Alignment").ToString
            For Each li As ListItem In ddlAlignment.Items
                li.Selected = (li.Value.Equals(sValue))
            Next li

            'end: GJV - 2/5/2008 - # 722
        End If
        If Me.chkStockInfo.Visible = True Then
            Me.chkStockInfo.Checked = dr.Item("DisplayStockInfo")
        End If

        If Me.chkVendor.Visible = True Then
            Me.chkVendor.Checked = dr.Item("DisplayVendor")
        End If
        Me.chkWishList.Checked = dr.Item("DisplaySavedCartWishList")
        Me.chkAddToCart.Checked = dr.Item("DisplayAddToCart")
        Me.chkDetailLink.Checked = dr.Item("DisplayMoreInfo")
        Me.chkEmailAFriend.Checked = dr.Item("DisplayEmailFriend")
        If Me.chkManufacturer.Visible = True Then
            Me.chkManufacturer.Checked = dr.Item("DisplayManufacturer")
        End If
        Me.chkPCodeLink.Checked = dr.Item("LinkProductCode")
        Me.chkPNameLink.Checked = dr.Item("LinkProductName")
        Me.chkPrice.Checked = dr.Item("DisplayPriceSalePrice")
        If Me.chkLabels.Visible = True Then
            Me.chkLabels.Checked = dr.Item("DisplayLabels")
        End If
        If Me.chkSImageLink.Visible = True Then
            Me.chkSImageLink.Checked = dr.Item("LinkImage")
        End If
        BindAddToCart(CType(dr.Item("DisplayQty"), Boolean))
        Me.rblAttributeType.SelectedValue = dr.Item("AttributeDisplay")
    End Sub

    Private Sub rbtemplate1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtemplate1.CheckedChanged
        'Bind fields to Template1 values
        If rbtemplate1.Checked = True Then
            Me.BindTemplate(1)
        End If
    End Sub

    Private Sub rbTemplate2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTemplate2.CheckedChanged
        'Bind fields to Template2 values
        If Me.rbTemplate2.Checked = True Then
            Me.BindTemplate(2)
        End If
    End Sub

    Private Sub rbTemplate3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTemplate3.CheckedChanged
        'Bind fields to Template3 values
        If Me.rbTemplate3.Checked = True Then
            Me.BindTemplate(3)
        End If
    End Sub

    Public Sub MakeActive(ByVal TemplateType As Long)
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Make a template active which has the passed type value and mark all others
        'as not active
        'OPTIONS:
        '1 = SearchTemplate1
        '2 = SearchTemplate2
        '3 = SearchTemplate3
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Dim myDesignManager As New DesignManager
        myDesignManager.MakeSearchTemplateActive(TemplateType)
    End Sub
    Public Sub Save()
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Saves changes made in form controls back to the database.
        'Note:  This saves all data to the currently active template.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------

        'Fill datatable we use
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllActiveSearchResults
        Dim dt As DataTable = ds.Tables(0)

        dt.Rows(0).Item("NumOfRows") = CType(Me.txtRows.Text, Integer)
        If Me.txtProducts.Visible = True Then
            dt.Rows(0).Item("ProductsPerRow") = CType(Me.txtProducts.Text, Integer)
        End If

        Dim dr As DataRow = dt.Rows(0)
        'Do the rest of the controls
        dr.Item("DisplayProductCode") = Me.chkProductID.Checked
        dr.Item("DisplayProductName") = Me.chkProductName.Checked
        If Me.chkShortDescription.Visible = True Then
            dr.Item("DisplayShortDescription") = Me.chkShortDescription.Checked
        End If
        If Me.chkSmallImage.Visible = True Then
            dr.Item("DisplayImage") = Me.chkSmallImage.Checked
        End If
        If Me.chkStockInfo.Visible = True Then
            dr.Item("DisplayStockInfo") = Me.chkStockInfo.Checked
        End If
        If Me.chkVendor.Visible = True Then
            dr.Item("DisplayVendor") = Me.chkVendor.Checked
        End If
        dr.Item("DisplaySavedCartWishList") = Me.chkWishList.Checked
        dr.Item("DisplayAddToCart") = Me.chkAddToCart.Checked
        dr.Item("DisplayMoreInfo") = Me.chkDetailLink.Checked
        dr.Item("DisplayEmailFriend") = Me.chkEmailAFriend.Checked
        If Me.chkManufacturer.Visible = True Then
            dr.Item("DisplayManufacturer") = Me.chkManufacturer.Checked
        End If
        If Me.chkVolumePricing.Visible = True Then
            dr.Item("DisplayVolumePricing") = Me.chkVolumePricing.Checked
        End If
        If Me.ddlAlignment.Visible = True Then
            dr.Item("Alignment") = Me.ddlAlignment.SelectedValue
        End If
        dr.Item("LinkProductCode") = Me.chkPCodeLink.Checked
        dr.Item("LinkProductName") = Me.chkPNameLink.Checked
        dr.Item("DisplayPriceSalePrice") = Me.chkPrice.Checked
        If Me.chkLabels.Visible = True Then
            dr.Item("DisplayLabels") = Me.chkLabels.Checked
        End If
        If Me.chkSImageLink.Visible = True Then
            dr.Item("LinkImage") = Me.chkSImageLink.Checked
        End If
        dr.Item("AttributeDisplay") = Me.rblAttributeType.SelectedValue
        If Me.chkDisplayQty.Visible = True Then
            dr.Item("DisplayQty") = Me.chkDisplayQty.Checked
        End If

        myDesignManager.UpdateSearchResults(ds)
        'att type
        'TODO:  Attribute type radio Button
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdSave.Click
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Sets the currently active template then saves changes to control values to
        'the database
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        If Me.txtRows.Text = String.Empty Then
            Me.ErrorMessage.Visible = True
            Me.ErrorMessage.Text = "Please Enter Number of Rows Per Page"
            Exit Sub
        End If
        Try
            Dim s As Integer = CType(Me.txtRows.Text, Integer)
        Catch ex As Exception
            Me.ErrorMessage.Visible = True
            Me.ErrorMessage.Text = "Please Enter Integers for Number of Rows Per Page"
            Exit Sub
        End Try
        If Me.txtProducts.Visible = True Then
            If Me.txtProducts.Text = String.Empty Then
                Me.ErrorMessage.Visible = True
                Me.ErrorMessage.Text = "Please Enter Number of Products Per Row"
                Exit Sub
            End If
            Try
                Dim s As Integer = CType(Me.txtProducts.Text, Integer)
            Catch ex As Exception
                Me.ErrorMessage.Visible = True
                Me.ErrorMessage.Text = "Please Enter Integers for Number of Products Per Row"
                Exit Sub
            End Try
        End If
        If Me.rbtemplate1.Checked = True Then
            Me.MakeActive(1)
        End If

        If Me.rbTemplate2.Checked = True Then
            Me.MakeActive(2)
        End If

        If Me.rbTemplate3.Checked = True Then
            Me.MakeActive(3)
        End If

        'Save changes in values
        Me.Save()
        Me.ErrorMessage.Visible = True
        Me.ErrorMessage.Text = "Your changes have been saved"
    End Sub

    Private Sub BindAddToCart(Optional ByVal displayQuantity As Boolean = False)
        If Me.chkAddToCart.Checked Then
            Me.chkDisplayQty.Enabled = True
            If Me.chkDisplayQty.Visible = True Then
                Me.chkDisplayQty.Checked = displayQuantity
            End If
        Else
            If Me.chkDisplayQty.Visible = True Then
                Me.chkDisplayQty.Checked = False
                Me.chkDisplayQty.Enabled = False
            End If
        End If
    End Sub

    Private Sub chkAddToCart_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAddToCart.CheckedChanged
        BindAddToCart()
    End Sub

    Private Sub ControlVisibility()
        If Me.rbTemplate2.Checked = True OrElse Me.rbTemplate3.Checked = True Then
            Me.lblProdPerRow.Visible = False
            Me.txtProducts.Visible = False
        Else
            Me.lblProdPerRow.Visible = True
            Me.txtProducts.Visible = True
        End If

        If Me.rbtemplate1.Checked = True Then
            Me.chkDisplayQty.Visible = False
        Else
            Me.chkDisplayQty.Visible = True
        End If

        If Me.rbTemplate3.Checked = True Then
            If StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae" Then
                Me.chkVolumePricing.Visible = True
            Else
                Me.chkVolumePricing.Visible = False
            End If
            Me.lblAlignment.Visible = True
            Me.ddlAlignment.Visible = True
        Else
            Me.chkVolumePricing.Visible = False
            Me.lblAlignment.Visible = False
            Me.ddlAlignment.Visible = False
        End If

        If Me.rbTemplate2.Checked = True Then
            Me.pnlNotTemplate2.Visible = False
            Me.pnlShortDescription.Visible = False
            Me.pnlSmallImage.Visible = False
            Me.chkSmallImage.Visible = False
            Me.chkShortDescription.Visible = False
            Me.chkManufacturer.Visible = False
            Me.chkVendor.Visible = False
            Me.chkLabels.Visible = False
            Me.chkDetailLink.Visible = False
            Me.chkSImageLink.Visible = False
        Else
            Me.pnlNotTemplate2.Visible = True
            Me.pnlShortDescription.Visible = True
            Me.pnlSmallImage.Visible = True
            Me.chkSmallImage.Visible = True
            Me.chkShortDescription.Visible = True
            Me.chkManufacturer.Visible = True
            Me.chkVendor.Visible = True
            Me.chkLabels.Visible = True
            Me.chkDetailLink.Visible = True
            Me.chkSImageLink.Visible = True
        End If
        'Control visibility depending on AE/SE version
        If StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae" Then
            Me.chkWishList.Text = "Wish List:"
            Me.chkStockInfo.Visible = True
        ElseIf StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "se" Then
            Me.chkWishList.Text = "Saved Cart:"
            Me.chkStockInfo.Visible = False
        End If


    End Sub
End Class

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
'Allows viewing and editing of settings for the stores detail pages.
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Partial Class ProductPages
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
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
        Me.Bind()
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
        'Binds the control fields to the Product Template which is currently active 
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        'Bind the currently active template
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllActiveProductDetails()
        Dim dt As DataTable = ds.Tables(0)
        'Select the correct RadioButton based on value in the datarow
        Select Case CInt(dt.Rows(0).Item("Type"))
            Case 1
                Me.rbtemplate1.Checked = True
            Case 2
                Me.rbTemplate2.Checked = True
        End Select

        ControlVisibility()
        Dim dr As DataRow = dt.Rows(0)
        'Do the rest of the form fields
        Me.chkProductID.Checked = dr.Item("DisplayProductCode")
        Me.chkProductName.Checked = dr.Item("DisplayProductName")
        Me.chkSmallImage.Checked = dr.Item("DisplayImage")
        If Me.chkStockInfo.Visible = True Then
            Me.chkStockInfo.Checked = dr.Item("DisplayStockInfo")
        End If
        Me.chkAddToCart.Checked = dr.Item("DisplayAddToCart")
        If Me.chkVolumePricing.Visible = True Then
            Me.chkVolumePricing.Checked = dr.Item("DisplayVolumePricing")
        End If
        Me.chkVendor.Checked = dr.Item("DisplayVendor")
        Me.chkWishList.Checked = dr.Item("DisplaySavedCartWishList")
        Me.chkEmailAFriend.Checked = dr.Item("DisplayEmailFriend")
        Me.chkManufacturer.Checked = dr.Item("DisplayManufacturer")
        Me.ChkCategory.Checked = dr.Item("DisplayCategory")
        Me.chkPrice.Checked = dr.Item("DisplayPriceSalePrice")
        Me.chkVendor.Checked = dr.Item("DisplayVendor")
        Me.chkLabels.Checked = dr.Item("DisplayLabels")
        Me.rblAttributeType.SelectedValue = dr.Item("AttributeDisplay")
        If dr.Item("ImageSize") = 1 Then
            Me.rbLargeImage.Checked = True
        Else
            Me.rbSmallImage.Checked = True
        End If
        If dr.Item("DisplayShortDescription") = 1 Or dr.Item("DisplaylongDescription") = 1 Then
            Me.chkDescription.Checked = True
            If dr.Item("DisplayShortDescription") = 1 Then
                Me.rbShortDescription.Checked = True
            Else
                Me.rbLargeDescription.Checked = True
            End If
        Else
            Me.chkDescription.Checked = False
            Me.rbShortDescription.Checked = False
            Me.rbLargeDescription.Checked = False
        End If
        Me.chkDisplayRecommendedItems.Checked = dr.Item("DisplayRecommendedProducts")
        Me.txtRecommendedTitle.Text = dr.Item("RecommendedTitle")
        Me.chkRecommendedProductCode.Checked = dr.Item("DisplayRecommendedCode")
        Me.chkProductIdLink.Checked = dr.Item("LinkProductCode")
        Me.chkRecommendedProductName.Checked = dr.Item("DisplayRecommendedName")
        Me.chkRecommendedSmallImage.Checked = dr.Item("DisplayRecommendedImage")
        Me.chkRecommendedShortDescription.Checked = dr.Item("DisplayRecommendedShortDescription")
        Me.chkRecommendedPrice.Checked = dr.Item("DisplayRecommendedPrice")
        Me.chkProductNameLink.Checked = dr.Item("LinkProductName")
        Me.chkLinkSmallImage.Checked = dr.Item("LinkImage")
        'Tee 11/07/2007 recommended items per row
        For Each ddlItem As ListItem In ddlItemPerRow.Items
            ddlItem.Selected = False
            If ddlItem.Value = dr.Item("RecommendedItemsPerRow").ToString Then
                ddlItem.Selected = True
            End If
        Next
        'end Tee
        BindAddToCart(CType(dr.Item("DisplayQty"), Boolean))
    End Sub

    Public Sub BindTemplate(ByVal TemplateType As Long)
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Binds the control fields to the Product Template which has the passed type 
        'value
        'OPTIONS:
        '1 = ProductDetail1
        '2 = ProductDetail2
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------

        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllProductDetailsByType(TemplateType)
        Dim dt As DataTable = ds.Tables(0)

        'Select the correct RadioButton based on value in the datarow
        Select Case CInt(dt.Rows(0).Item("Type"))
            Case 1
                Me.rbtemplate1.Checked = True
            Case 2
                Me.rbTemplate2.Checked = True
        End Select

        ControlVisibility()
        Dim dr As DataRow = dt.Rows(0)

        'Do the rest of the form fields
        Me.chkProductID.Checked = dr.Item("DisplayProductCode")
        Me.chkProductName.Checked = dr.Item("DisplayProductName")

        Me.chkSmallImage.Checked = dr.Item("DisplayImage")
        If Me.chkStockInfo.Visible = True Then
            Me.chkStockInfo.Checked = dr.Item("DisplayStockInfo")
        End If
        If Me.chkVolumePricing.Visible = True Then
            Me.chkVolumePricing.Checked = dr.Item("DisplayVolumePricing")
        End If
        Me.chkVendor.Checked = dr.Item("DisplayVendor")
        Me.chkWishList.Checked = dr.Item("DisplaySavedCartWishList")
        Me.chkAddToCart.Checked = dr.Item("DisplayAddToCart")
        Me.chkEmailAFriend.Checked = dr.Item("DisplayEmailFriend")
        Me.chkManufacturer.Checked = dr.Item("DisplayManufacturer")
        Me.ChkCategory.Checked = dr.Item("DisplayCategory")
        Me.chkPrice.Checked = dr.Item("DisplayPriceSalePrice")
        Me.chkVendor.Checked = dr.Item("DisplayVendor")
        Me.chkLabels.Checked = dr.Item("DisplayLabels")

        Me.rblAttributeType.SelectedValue = dr.Item("AttributeDisplay")
        If dr.Item("ImageSize") = 1 Then
            Me.rbLargeImage.Checked = True
        Else
            Me.rbSmallImage.Checked = True
        End If
        If dr.Item("DisplayShortDescription") Or dr.Item("DisplaylongDescription") = 1 Then
            Me.chkDescription.Checked = True
            If dr.Item("DisplayShortDescription") = 1 Then
                Me.rbShortDescription.Checked = True
            Else
                Me.rbLargeDescription.Checked = True
            End If
        Else
            Me.chkDescription.Checked = False
            Me.rbShortDescription.Checked = False
            Me.rbLargeDescription.Checked = False
        End If
        Me.chkDisplayRecommendedItems.Checked = dr.Item("DisplayRecommendedProducts")
        Me.txtRecommendedTitle.Text = dr.Item("RecommendedTitle")
        Me.chkRecommendedProductCode.Checked = dr.Item("DisplayRecommendedCode")
        Me.chkProductIdLink.Checked = dr.Item("LinkProductCode")
        Me.chkRecommendedProductName.Checked = dr.Item("DisplayRecommendedName")
        Me.chkRecommendedSmallImage.Checked = dr.Item("DisplayRecommendedImage")
        Me.chkRecommendedShortDescription.Checked = dr.Item("DisplayRecommendedShortDescription")
        Me.chkRecommendedPrice.Checked = dr.Item("DisplayRecommendedPrice")
        Me.chkProductNameLink.Checked = dr.Item("LinkProductName")
        Me.chkLinkSmallImage.Checked = dr.Item("LinkImage")
        'Tee 11/07/2007 recommended items per row
        For Each ddlItem As ListItem In ddlItemPerRow.Items
            ddlItem.Selected = False
            If ddlItem.Value = dr.Item("RecommendedItemsPerRow").ToString Then
                ddlItem.Selected = True
            End If
        Next
        'end Tee
        BindAddToCart(CType(dr.Item("DisplayQty"), Boolean))
    End Sub

    Private Sub rbtemplate1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtemplate1.CheckedChanged
        If rbtemplate1.Checked = True Then
            Me.BindTemplate(1)
        End If
    End Sub

    Private Sub rbTemplate2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTemplate2.CheckedChanged
        If Me.rbTemplate2.Checked = True Then
            Me.BindTemplate(2)
        End If
    End Sub

    Public Sub MakeActive(ByVal TemplateType As Long)
        Dim myDesignManager As New DesignManager
        myDesignManager.MakeProductDetailTemplateActive(TemplateType)
    End Sub
    Public Sub Save()
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllActiveProductDetails
        Dim dt As DataTable = ds.Tables(0)
        Dim dr As DataRow = dt.Rows(0)
        'Do the rest
        dr.Item("DisplayProductCode") = Me.chkProductID.Checked
        dr.Item("DisplayProductName") = Me.chkProductName.Checked
        dr.Item("DisplayImage") = Me.chkSmallImage.Checked
        If Me.chkStockInfo.Visible = True Then
            dr.Item("DisplayStockInfo") = Me.chkStockInfo.Checked
        End If
        dr.Item("DisplayAddToCart") = Me.chkAddToCart.Checked
        dr.Item("DisplayQty") = Me.chkDisplayQty.Checked
        If Me.chkVolumePricing.Visible = True Then
            dr.Item("DisplayVolumePricing") = Me.chkVolumePricing.Checked
        End If
        dr.Item("DisplayVendor") = Me.chkVendor.Checked
        dr.Item("DisplaySavedCartWishList") = Me.chkWishList.Checked
        dr.Item("DisplayEmailFriend") = Me.chkEmailAFriend.Checked
        dr.Item("DisplayManufacturer") = Me.chkManufacturer.Checked
        dr.Item("DisplayCategory") = Me.ChkCategory.Checked
        dr.Item("DisplayPriceSalePrice") = Me.chkPrice.Checked
        dr.Item("DisplayVendor") = Me.chkVendor.Checked
        dr.Item("DisplayLabels") = Me.chkLabels.Checked
        dr.Item("AttributeDisplay") = Me.rblAttributeType.SelectedValue
        If Me.rbLargeImage.Checked = True Then
            dr.Item("ImageSize") = 1
        Else
            dr.Item("ImageSize") = 0
        End If
        If Me.chkDescription.Checked = True Then

            If Me.rbShortDescription.Checked = True Then
                dr.Item("DisplayShortDescription") = 1
                dr.Item("DisplaylongDescription") = 0
            Else
                dr.Item("DisplaylongDescription") = 1
                dr.Item("DisplayShortDescription") = 0
            End If
        Else
            dr.Item("DisplaylongDescription") = 0
            dr.Item("DisplayShortDescription") = 0

        End If

        dr.Item("DisplayRecommendedProducts") = Me.chkDisplayRecommendedItems.Checked
        dr.Item("RecommendedTitle") = Me.txtRecommendedTitle.Text
        dr.Item("DisplayRecommendedCode") = Me.chkRecommendedProductCode.Checked
        dr.Item("LinkProductCode") = Me.chkProductIdLink.Checked
        dr.Item("DisplayRecommendedName") = Me.chkRecommendedProductName.Checked
        dr.Item("DisplayRecommendedImage") = Me.chkRecommendedSmallImage.Checked
        dr.Item("DisplayRecommendedShortDescription") = Me.chkRecommendedShortDescription.Checked
        dr.Item("DisplayRecommendedPrice") = Me.chkRecommendedPrice.Checked
        dr.Item("LinkProductName") = Me.chkProductNameLink.Checked
        dr.Item("LinkImage") = Me.chkLinkSmallImage.Checked
        'Tee 11/07/2007 recommended items per row
        dr.Item("RecommendedItemsPerRow") = ddlItemPerRow.SelectedItem.Value
        'end Tee
        myDesignManager.UpdateProductDetails(ds)
        'att type
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdSave.Click
        If Me.rbtemplate1.Checked = True Then
            Me.MakeActive(1)
        End If

        If Me.rbTemplate2.Checked = True Then
            Me.MakeActive(2)
        End If
        Me.Save()
        Me.ErrorMessage.Visible = True
        Me.ErrorMessage.Text = "Your changes have been saved."
    End Sub

    Private Sub BindAddToCart(Optional ByVal displayQuantity As Boolean = False)
        If Me.chkAddToCart.Checked Then
            Me.chkDisplayQty.Enabled = True
            Me.chkDisplayQty.Checked = displayQuantity
        Else
            Me.chkDisplayQty.Checked = False
            Me.chkDisplayQty.Enabled = False
        End If
    End Sub

    Private Sub chkAddToCart_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAddToCart.CheckedChanged
        BindAddToCart()
    End Sub

    Private Sub ControlVisibility()
        If StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae" Then
            Me.chkWishList.Text = "Wish List:"
            Me.chkStockInfo.Visible = True
            Me.chkVolumePricing.Visible = True
        ElseIf StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "se" Then
            Me.chkWishList.Text = "Saved Cart:"
            Me.chkStockInfo.Visible = False
            Me.chkVolumePricing.Visible = False
        End If
    End Sub

End Class

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

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

Partial Class EditEMail
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents AdminTabControl1 As AdminTabControl
    Protected WithEvents P1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents editemaildynamic1 As editemaildynamic
    Protected strTextBody, strHtmlBody, strEmailType As String
    Protected strSubject As String

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

    Private objCXMLEMailContent As New CXMLEMailContent()
    Private m_strSSLPath, strRootPath As String
    Private m_objEMail As CEMailContent
    Private m_objCXMLEmailContent As CXMLEMailContent
    Private m_arr As ArrayList
    Private m_bNetscape As Boolean = False

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.EMail) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            If Request.Browser.Browser.ToUpper().IndexOf("IE") = -1 Then
                m_bNetscape = True
            End If
            Me.sslPath = StoreFrontConfiguration.SSLPath
            Me.RootPath = StoreFrontConfiguration.SiteURL
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            'Put user code to initialize the page here
            Dim ar As New ArrayList
            ar.Add("Settings")
            ar.Add("E-Mail A Friend")
            ar.Add("Forgot Password")
            ar.Add("Confirm")
            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value <> "SE") Then
                ar.Add("WishList")
                ar.Add("Low Stock Notice")
            End If
            'adds the ebay tab if the Ebay Checkout Notification Type exists
            Dim emailContent As New CEMailContent
            Dim emailContentCollection As ArrayList = emailContent.GetAllEMailContent
            For Each emailContent In emailContentCollection
                If emailContent.Type = EMailContentTypes.Ebay_Checkout Then
                    ar.Add("eBay")
                    Exit For
                End If
            Next
            AdminTabControl1.BorderClass = "ContentTable"
            AdminTabControl1.TabItemClass = "Content"
            AdminTabControl1.TabStringArray = ar

            ' all secondary buttons false
            pnlSubmenu.Visible = False
            lnkWishList.Visible = False
            lnkWishListComponents.Visible = False
            lnkCustomer.Visible = False
            lnkVendor.Visible = False
            lnkMerchant.Visible = False
            lnkProducts.Visible = False
            lnkBilling.Visible = False
            lnkShipping.Visible = False
            lnkOrderTotal.Visible = False
            btnSave.Attributes.Add("OnClick", "return Save();")
            ' Text Panel invisible
            lblPanel.Visible = False

            objCXMLEMailContent = New CXMLEMailContent

            If Not Page.IsPostBack Then
                If Request.QueryString("Type") <> "" Then
                    Dim type As Long = Request.QueryString("Type")
                    objCXMLEMailContent = EditByType(type)
                    Session("EmailContentObject") = objCXMLEMailContent
                    If (type = 2) Then
                        AdminTabControl1.SelectedTabIndex = 4
                        pnlSubmenu.Visible = True
                        lnkWishList.Visible = True
                        lnkWishListComponents.Visible = True
                        lnkCustomer.Visible = False
                        lnkVendor.Visible = False
                        lnkMerchant.Visible = False
                        lnkProducts.Visible = False
                        lnkBilling.Visible = False
                        lnkShipping.Visible = False
                        lnkOrderTotal.Visible = False
                    ElseIf (type = 4) Then
                        AdminTabControl1.SelectedTabIndex = 3
                        pnlSubmenu.Visible = True
                        lnkWishList.Visible = False
                        lnkWishListComponents.Visible = False
                        lnkCustomer.Visible = True

                        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "AE") Then
                            lnkVendor.Visible = True
                        Else
                            lnkVendor.Visible = False
                        End If

                        lnkMerchant.Visible = True
                        lnkProducts.Visible = True
                        lnkBilling.Visible = True
                        lnkShipping.Visible = True
                        lnkOrderTotal.Visible = True
                    ElseIf (type = 0) Then
                        AdminTabControl1.SelectedTabIndex = 1
                    ElseIf (type = 1) Then
                        AdminTabControl1.SelectedTabIndex = 2
                    ElseIf (type = 11) Then
                        AdminTabControl1.SelectedTabIndex = 5
                    ElseIf (type = 12) Then
                        AdminTabControl1.SelectedTabIndex = 6 'Tab for Ebay Checkout Notification
                    End If
                Else
                    Response.Redirect("ManageEMail.aspx")
                End If
            Else
                Dim strAction As String
                strAction = Request.QueryString("action")
                If (ddFormat.SelectedIndex = 0) Then  'TEXTBOX
                    lblPanel.Visible = True
                End If
            End If
            Me.HTMLBody = hdnText.Value
            Me.EMailType = hdnEMailType.Value

            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
                lnkVendor.Visible = False
            End If

            DataBind()
        Catch ex As Exception
            Session("DetailError") = "Class EditEMail Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public ReadOnly Property BaseURL() As String
        Get
            Return StoreFrontConfiguration.SSLPath & "management/"
        End Get
    End Property

    Public ReadOnly Property BaseURLNew() As String
        Get
            Return StoreFrontConfiguration.SiteURL
        End Get
    End Property

    Public Property sslPath() As String
        Get
            Return m_strSSLPath
        End Get
        Set(ByVal Value As String)
            m_strSSLPath = Value
        End Set
    End Property

    Public Property RootPath() As String
        Get
            Return strRootPath
        End Get
        Set(ByVal Value As String)
            strRootPath = Value
        End Set
    End Property

    Public Property TextBody() As String
        Get
            Return strTextBody
        End Get
        Set(ByVal Value As String)
            strTextBody = Value
        End Set
    End Property

    Public Property HTMLBody() As String
        Get
            Return strHtmlBody
        End Get
        Set(ByVal Value As String)
            strHtmlBody = Value
        End Set
    End Property

    Public Property Subject() As String
        Get
            Return strSubject
        End Get
        Set(ByVal Value As String)
            strSubject = Value
        End Set
    End Property

    Public Property EMailType() As String
        Get
            Return strEmailType
        End Get
        Set(ByVal Value As String)
            strEmailType = Value
        End Set
    End Property

    Public ReadOnly Property Format() As String
        Get
            Return ddFormat.SelectedItem.Value
        End Get
    End Property

#Region "Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.TabClick"
    Private Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdminTabControl1.TabClick
        Dim arr As New ArrayList

        If (sender = "0") And Page.IsPostBack Then
            Response.Redirect("ManageEMail.aspx")
        ElseIf (sender = "1") Then
            objCXMLEMailContent = EditByType(0) ' EMail a friend
            hdnEMailType.Value = 0
        ElseIf (sender = "2") Then
            objCXMLEMailContent = EditByType(1) ' Forgot Password
            hdnEMailType.Value = 1
        ElseIf (sender = "4") Then
            objCXMLEMailContent = EditByType(2) ' Wish List
            pnlSubmenu.Visible = True
            lnkWishList.Visible = True
            lnkWishListComponents.Visible = True
            lnkCustomer.Visible = False
            lnkVendor.Visible = False
            lnkMerchant.Visible = False
            lnkProducts.Visible = False
            lnkBilling.Visible = False
            lnkShipping.Visible = False
            lnkOrderTotal.Visible = False
            hdnEMailType.Value = 2
        ElseIf (sender = "3") Then
            objCXMLEMailContent = EditByType(4) ' Confirm
            pnlSubmenu.Visible = True
            lnkWishList.Visible = False
            lnkWishListComponents.Visible = False
            lnkCustomer.Visible = True
            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "AE") Then
                lnkVendor.Visible = True
            Else
                lnkVendor.Visible = False
            End If
            lnkMerchant.Visible = True
            lnkProducts.Visible = True
            lnkBilling.Visible = True
            lnkShipping.Visible = True
            lnkOrderTotal.Visible = True
            hdnEMailType.Value = 4
        ElseIf (sender = "5") Then
            objCXMLEMailContent = EditByType(11) ' Low Stock
            hdnEMailType.Value = 11
        ElseIf (sender = "6") Then
            objCXMLEMailContent = EditByType(12) 'Ebay Checkout Notification
            hdnEMailType.Value = 12
        End If

        Session("EmailContentObject") = objCXMLEMailContent

        If objCXMLEMailContent.Format = "HTML" Then
            Me.HTMLBody = objCXMLEMailContent.Body
            DataBind()
            'txtMessage.Text = objCXMLEMailContent.Body
        End If

        Message.Visible = False
        ErrorMessage.Visible = False
    End Sub
#End Region

#Region "lnkWishList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkWishList.Click"
    Public Sub lnkWishList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkWishList.Click
        objCXMLEMailContent = EditByType(2)
        If objCXMLEMailContent.Format = "HTML" Then ' Need to populate textbox field as well
            Dim objCXMLEMail As New CXMLEMailContent
            Dim objEmail2 As New CEMailContent
            objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(2, "text"))
            txtMessage.Text = objCXMLEMail.Body
            hdnText.Value = objCXMLEMailContent.Body
            Me.HTMLBody = objCXMLEMailContent.Body
            DataBind()
        End If
        pnlSubmenu.Visible = True
        hdnEMailType.Value = 2
        Session("EmailContentObject") = objCXMLEMailContent
        lnkWishList.Visible = True
        lnkWishListComponents.Visible = True
        lnkCustomer.Visible = False
        lnkVendor.Visible = False
        lnkMerchant.Visible = False
        lnkProducts.Visible = False
        lnkBilling.Visible = False
        lnkShipping.Visible = False
        lnkOrderTotal.Visible = False
    End Sub
#End Region

#Region "lnkWishListComponent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkWishList.Click"
    Public Sub lnkWishListComponent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkWishListComponents.Click
        objCXMLEMailContent = EditByType(3)
        If objCXMLEMailContent.Format = "HTML" Then ' Need to populate textbox field as well
            Dim objCXMLEMail As New CXMLEMailContent
            Dim objEmail2 As New CEMailContent
            objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(3, "text"))
            txtMessage.Text = objCXMLEMail.Body
            hdnText.Value = objCXMLEMailContent.Body
            Me.HTMLBody = objCXMLEMailContent.Body
            DataBind()
        End If
        pnlSubmenu.Visible = True
        hdnEMailType.Value = 3
        Session("EmailContentObject") = objCXMLEMailContent
        lnkWishList.Visible = True
        lnkWishListComponents.Visible = True
        lnkCustomer.Visible = False
        lnkVendor.Visible = False
        lnkMerchant.Visible = False
        lnkProducts.Visible = False
        lnkBilling.Visible = False
        lnkShipping.Visible = False
        lnkOrderTotal.Visible = False
    End Sub
#End Region

#Region "lnkCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCustomer.Click"
    Public Sub lnkCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCustomer.Click
        objCXMLEMailContent = EditByType(4)
        Session("EmailContentObject") = objCXMLEMailContent
        If objCXMLEMailContent.Format = "HTML" Then ' Need to populate textbox field as well
            Dim objCXMLEMail As New CXMLEMailContent
            Dim objEmail2 As New CEMailContent
            objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(4, "text"))
            txtMessage.Text = objCXMLEMail.Body
            hdnText.Value = objCXMLEMailContent.Body
            Me.HTMLBody = objCXMLEMailContent.Body
            DataBind()
        End If
        pnlSubmenu.Visible = True
        hdnEMailType.Value = 4
        lnkWishList.Visible = False
        lnkWishListComponents.Visible = False
        lnkCustomer.Visible = True
        lnkVendor.Visible = True
        lnkMerchant.Visible = True
        lnkProducts.Visible = True
        lnkBilling.Visible = True
        lnkShipping.Visible = True
        lnkOrderTotal.Visible = True
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            lnkVendor.Visible = False
        End If
    End Sub
#End Region

#Region "lnkVendor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkVendor.Click"
    Public Sub lnkVendor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkVendor.Click
        objCXMLEMailContent = EditByType(5)
        Session("EmailContentObject") = objCXMLEMailContent
        If objCXMLEMailContent.Format = "HTML" Then ' Need to populate textbox field as well
            Dim objCXMLEMail As New CXMLEMailContent
            Dim objEmail2 As New CEMailContent
            objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(5, "text"))
            txtMessage.Text = objCXMLEMail.Body
            hdnText.Value = objCXMLEMailContent.Body
            Me.HTMLBody = objCXMLEMailContent.Body
            DataBind()
        End If
        pnlSubmenu.Visible = True
        hdnEMailType.Value = 5
        lnkWishList.Visible = False
        lnkWishListComponents.Visible = False
        lnkCustomer.Visible = True
        lnkVendor.Visible = True
        lnkMerchant.Visible = True
        lnkProducts.Visible = True
        lnkBilling.Visible = True
        lnkShipping.Visible = True
        lnkOrderTotal.Visible = True
    End Sub
#End Region

#Region "lnkMerchant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkMerchant.Click"
    Public Sub lnkMerchant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkMerchant.Click
        objCXMLEMailContent = EditByType(6)
        Session("EmailContentObject") = objCXMLEMailContent
        If objCXMLEMailContent.Format = "HTML" Then ' Need to populate textbox field as well
            Dim objCXMLEMail As New CXMLEMailContent
            Dim objEmail2 As New CEMailContent
            objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(6, "text"))
            txtMessage.Text = objCXMLEMail.Body
            hdnText.Value = objCXMLEMailContent.Body
            Me.HTMLBody = objCXMLEMailContent.Body
            DataBind()
        End If
        pnlSubmenu.Visible = True
        hdnEMailType.Value = 6
        lnkWishList.Visible = False
        lnkWishListComponents.Visible = False
        lnkCustomer.Visible = True
        lnkVendor.Visible = True
        lnkMerchant.Visible = True
        lnkProducts.Visible = True
        lnkBilling.Visible = True
        lnkShipping.Visible = True
        lnkOrderTotal.Visible = True
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            lnkVendor.Visible = False
        End If
    End Sub
#End Region

#Region "lnkProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkProducts.Click"
    Public Sub lnkProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkProducts.Click
        objCXMLEMailContent = EditByType(7)
        Session("EmailContentObject") = objCXMLEMailContent
        If objCXMLEMailContent.Format = "HTML" Then ' Need to populate textbox field as well
            Dim objCXMLEMail As New CXMLEMailContent
            Dim objEmail2 As New CEMailContent
            objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(7, "text"))
            txtMessage.Text = objCXMLEMail.Body
            hdnText.Value = objCXMLEMailContent.Body
            Me.HTMLBody = objCXMLEMailContent.Body
            DataBind()
        End If
        pnlSubmenu.Visible = True
        hdnEMailType.Value = 7
        lnkWishList.Visible = False
        lnkWishListComponents.Visible = False
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae") Then
            lnkVendor.Visible = True
        Else
            lnkVendor.Visible = False
        End If
        lnkCustomer.Visible = True
        lnkMerchant.Visible = True
        lnkProducts.Visible = True
        lnkBilling.Visible = True
        lnkShipping.Visible = True
        lnkOrderTotal.Visible = True
    End Sub
#End Region

#Region "lnkBilling_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBilling.Click"
    Public Sub lnkBilling_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBilling.Click
        objCXMLEMailContent = EditByType(8)
        Session("EmailContentObject") = objCXMLEMailContent
        If objCXMLEMailContent.Format = "HTML" Then ' Need to populate textbox field as well
            Dim objCXMLEMail As New CXMLEMailContent
            Dim objEmail2 As New CEMailContent
            objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(8, "text"))
            txtMessage.Text = objCXMLEMail.Body
            hdnText.Value = objCXMLEMailContent.Body
            Me.HTMLBody = objCXMLEMailContent.Body
            DataBind()
        End If
        pnlSubmenu.Visible = True
        hdnEMailType.Value = 8
        lnkWishList.Visible = False
        lnkWishListComponents.Visible = False
        lnkCustomer.Visible = True
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae") Then
            lnkVendor.Visible = True
        Else
            lnkVendor.Visible = False
        End If
        lnkMerchant.Visible = True
        lnkProducts.Visible = True
        lnkBilling.Visible = True
        lnkShipping.Visible = True
        lnkOrderTotal.Visible = True
    End Sub
#End Region

#Region "lnkShipping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBilling.Click"
    Public Sub lnkShipping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkShipping.Click
        objCXMLEMailContent = EditByType(9)
        Session("EmailContentObject") = objCXMLEMailContent
        If objCXMLEMailContent.Format = "HTML" Then ' Need to populate textbox field as well
            Dim objCXMLEMail As New CXMLEMailContent
            Dim objEmail2 As New CEMailContent
            objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(9, "text"))
            txtMessage.Text = objCXMLEMail.Body
            hdnText.Value = objCXMLEMailContent.Body
            Me.HTMLBody = objCXMLEMailContent.Body
            DataBind()
        End If
        pnlSubmenu.Visible = True
        hdnEMailType.Value = 9
        lnkWishList.Visible = False
        lnkWishListComponents.Visible = False
        lnkCustomer.Visible = True
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae") Then
            lnkVendor.Visible = True
        Else
            lnkVendor.Visible = False
        End If
        lnkMerchant.Visible = True
        lnkProducts.Visible = True
        lnkBilling.Visible = True
        lnkShipping.Visible = True
        lnkOrderTotal.Visible = True
    End Sub
#End Region

#Region "lnkOrderTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkOrderTotal.Click"
    Public Sub lnkOrderTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkOrderTotal.Click
        objCXMLEMailContent = EditByType(10)
        Session("EmailContentObject") = objCXMLEMailContent
        If objCXMLEMailContent.Format = "HTML" Then ' Need to populate textbox field as well
            Dim objCXMLEMail As New CXMLEMailContent
            Dim objEmail2 As New CEMailContent
            objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(10, "text"))
            txtMessage.Text = objCXMLEMail.Body
            hdnText.Value = objCXMLEMailContent.Body
            Me.HTMLBody = objCXMLEMailContent.Body
            DataBind()
        End If
        pnlSubmenu.Visible = True
        hdnEMailType.Value = 10
        lnkWishList.Visible = False
        lnkWishListComponents.Visible = False
        lnkCustomer.Visible = True
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae") Then
            lnkVendor.Visible = True
        Else
            lnkVendor.Visible = False
        End If
        lnkMerchant.Visible = True
        lnkProducts.Visible = True
        lnkBilling.Visible = True
        lnkShipping.Visible = True
        lnkOrderTotal.Visible = True
    End Sub
#End Region


#Region "Sub SetVisibility(ByVal mailtype As String)"
    Public Sub SetVisibility(ByVal mailtype As String)
        If ((mailtype = "2") Or (mailtype = "3")) Then
            ' Wish List
            pnlSubmenu.Visible = True
            lnkWishList.Visible = True
            lnkWishListComponents.Visible = True
            lnkCustomer.Visible = False
            lnkVendor.Visible = False
            lnkMerchant.Visible = False
            lnkProducts.Visible = False
            lnkBilling.Visible = False
            lnkShipping.Visible = False
            lnkOrderTotal.Visible = False
        ElseIf ((mailtype = "4") Or (mailtype = "5") Or (mailtype = "6") Or (mailtype = "7") Or (mailtype = "8") Or (mailtype = "9") Or (mailtype = "10")) Then
            ' Confirm
            pnlSubmenu.Visible = True
            lnkWishList.Visible = False
            lnkWishListComponents.Visible = False
            lnkCustomer.Visible = True
            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "AE") Then
                lnkVendor.Visible = True
            Else
                lnkVendor.Visible = False
            End If
            lnkMerchant.Visible = True
            lnkProducts.Visible = True
            lnkBilling.Visible = True
            lnkShipping.Visible = True
            lnkOrderTotal.Visible = True
            hdnEMailType.Value = 4
        End If

    End Sub
#End Region

    'Public Sub reload()
    '    Dim objEmail As New CXMLEMailContent()
    '    objEmail = Session("EmailContentObject")
    '    If objEmail.Format.Equals("TEXT") Then
    '        ddFormat.SelectedIndex = 0
    '    Else
    '        Me.HTMLBody = Server.UrlDecode(hdnText.Value)
    '        ddFormat.SelectedIndex = 1
    '    End If
    '    Me.EMailType = objEmail.LongType
    '    hdnEMailType.Value = objEmail.LongType
    '    lblMessage.Visible = False
    '    ErrorMessage.Visible = False
    '    DataBind()
    'End Sub

    Public Sub SaveEmail()
        'Dim strFormat As String
        Dim arr As New ArrayList
        Dim arr2 As New ArrayList
        Dim objEmail As New CXMLEMailContent
        objEmail = Session("EmailContentObject")
        hdnEMailType.Value = objEmail.LongType

        ' If the objEmail does not match the ddFormat type, then build the correct corresponding object
        If objEmail.Format = "HTML" And ddFormat.SelectedIndex = 0 Then ' trying to save text
            Dim newObjEMail As New CXMLEMailContent
            newObjEMail.Subject = txtSubject.Text
            newObjEMail.Body = txtMessage.Text
            newObjEMail.Format = "TEXT"
            newObjEMail.IsActive = 1
            newObjEMail.Type = objEmail.Type
            newObjEMail.ID = GetEMailContentUID(objEmail.Type, "text")
            objEmail = newObjEMail
        ElseIf objEmail.Format = "TEXT" And ddFormat.SelectedIndex = 1 Then 'trying to save html
            Dim newObjEMail As New CXMLEMailContent
            newObjEMail.Subject = txtSubject.Text
            newObjEMail.Body = hdnText.Value
            newObjEMail.Format = "HTML"
            newObjEMail.IsActive = 1
            newObjEMail.Type = objEmail.Type
            newObjEMail.ID = GetEMailContentUID(objEmail.Type, "html")
            objEmail = newObjEMail
        End If

        Dim objEmailCompare As CEMailContent
        Dim objEMailAccess As New CEMailContent

        arr2 = objEMailAccess.GetAllEMailContent()

        For Each objEmailCompare In arr2
            If (objEmail.LongType = objEmailCompare.LongType And objEmail.Format = objEmailCompare.Format) Then

                ' Matched and now updating
                If ddFormat.SelectedIndex = 0 Then ' TEXT format
                    objEmail.Body = txtMessage.Text
                    objEmail.Subject = txtSubject.Text
                    objEmail.IsActive = 1
                Else ' HTML
                    objEmail.Body = Server.UrlDecode(hdnText.Value)
                    objEmail.Subject = txtSubject.Text
                    objEmail.IsActive = 1
                End If

                ' Update
                Dim objCAccess As New CEMailContent(objEmail)
                Try
                    objCAccess.UpdateEmailContent(objEmail)
                    Message.Text = "E-Mail changes saved"
                    Message.Visible = True
                Catch objError As Exception
                    ErrorMessage.Text = objError.Message
                    ErrorMessage.Visible = True
                End Try

                ' make sure other choice is not active
                If objEmail.Format.Equals("TEXT") Then
                    Dim objEmail2 As New CEMailContent(objEmail)
                    Dim objCXMLEMail As New CXMLEMailContent
                    objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(objEmail.Type, "html"))
                    objCXMLEMail.IsActive = False
                    Try
                        objEmail2.UpdateEmailContent(objCXMLEMail)
                    Catch objError As Exception
                        ErrorMessage.Text = objError.Message
                        ErrorMessage.Visible = True
                    End Try
                ElseIf objEmail.Format.Equals("HTML") Then
                    Dim objEmail2 As New CEMailContent(objEmail)
                    Dim objCXMLEMail As New CXMLEMailContent
                    objCXMLEMail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(objEmail.Type, "text"))
                    objCXMLEMail.IsActive = False
                    Try
                        objEmail2.UpdateEmailContent(objCXMLEMail)
                    Catch objError As Exception
                        ErrorMessage.Text = objError.Message
                        ErrorMessage.Visible = True
                    End Try
                End If
            End If
        Next

        Session("EmailContentObject") = objEmail
    End Sub


    'Public Sub Activate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActivate.Click
    '    Dim objEmail As New CXMLEMailContent()
    '    objEmail = Session("EmailContentObject")
    '    Dim objEmail2 As New CEMailContent()
    '    objEmail = objEmail2.GetCXMLEMailContent(GetEMailContentUID(objEmail.LongType, ddFormat.SelectedItem.Value))

    '    If objEmail.IsActive = 1 Then
    '        objEmail.IsActive = 0
    '        btnActivate.Text = "Activate"
    '    Else
    '        objEmail.IsActive = 1
    '        btnActivate.Text = "Deactivate"
    '    End If

    '    ' Update
    '    Dim objCAccess As New CEMailContent(objEmail)
    '    Try
    '        objCAccess.UpdateEmailContent(objEmail)
    '    Catch objError As Exception
    '        ErrorMessage.Text = objError.Message
    '        ErrorMessage.Visible = True
    '    End Try

    '    ' Work other format
    '    Dim objEmailOther As New CXMLEMailContent()
    '    If objEmail.LongType <> 5 And objEmail.LongType <> 6 And objEmail.LongType <> 11 Then ' non-optional e-mails
    '        If objEmail.Format = "TEXT" Then
    '            objEmailOther = objEmail2.GetCXMLEMailContent(GetEMailContentUID(objEmail.LongType, "HTML"))
    '        Else
    '            objEmailOther = objEmail2.GetCXMLEMailContent(GetEMailContentUID(objEmail.LongType, "TEXT"))
    '        End If
    '        objEmailOther.IsActive = 0
    '        Try
    '            objCAccess.UpdateEmailContent(objEmailOther)
    '        Catch objError As Exception
    '            ErrorMessage.Text = objError.Message
    '            ErrorMessage.Visible = True
    '        End Try
    '    End If
    'End Sub

    Public Sub EmailType_ChangeClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddFormat.SelectedIndexChanged
        Dim objEmail As New CXMLEMailContent
        objEmail = Session("EmailContentObject")

        If (ddFormat.SelectedIndex = 0) Then  'TEXTBOX
            txtMessage.Visible = True
            editemaildynamic1.Visible = False
            lblPanel.Visible = True

            ' populate textbox correctly

            If objEmail.Format = "TEXT" Then
                txtMessage.Text = objEmail.Body
                lblPanel.Text = getText(hdnEMailType.Value)
            Else
                Me.HTMLBody = objEmail.Body ' get it in first
                Dim objEMailContent As New CEMailContent
                objEmail = objEMailContent.GetCXMLEMailContent(GetEMailContentUID(objEmail.LongType, "TEXT"))
                txtMessage.Text = objEmail.Body
            End If
            Me.EMailType = objEmail.LongType
            'If objEmail.IsActive = 1 Then
            '    btnActivate.Text = "Deactivate"
            'Else
            '    btnActivate.Text = "Activate"
            'End If

        ElseIf (ddFormat.SelectedIndex = 1) Then 'HTML
            If (m_bNetscape = False) Then
                txtMessage.Visible = False
                editemaildynamic1.Visible = True
                lblPanel.Visible = False
            Else
                txtMessage.Visible = True
                editemaildynamic1.Visible = False
                lblPanel.Visible = True
            End If

            ' populate correctly the dynamic box            
            If objEmail.Format = "HTML" Then
                hdnText.Value = objEmail.Body
                Me.HTMLBody = objEmail.Body
                Me.EMailType = objEmail.LongType
            Else
                Dim objEMailContent As New CEMailContent
                objEmail = objEMailContent.GetCXMLEMailContent(GetEMailContentUID(objEmail.Type, "HTML"))
                hdnText.Value = objEmail.Body
                Me.HTMLBody = objEmail.Body
                Me.EMailType = objEmail.LongType
            End If

            If (m_bNetscape = True) Then
                txtMessage.Text = objEmail.Body
            End If
            'If objEmail.IsActive = 1 Then
            '    btnActivate.Text = "Deactivate"
            'Else
            '    btnActivate.Text = "Activate"
            'End If
        End If
        If objEmail.LongType = 2 Or objEmail.LongType = 3 Then
            lnkWishList.Visible = True
            lnkWishListComponents.Visible = True
            lnkCustomer.Visible = False
            lnkVendor.Visible = False
            lnkMerchant.Visible = False
            lnkProducts.Visible = False
            lnkBilling.Visible = False
            lnkShipping.Visible = False
            lnkOrderTotal.Visible = False
        ElseIf objEmail.LongType = 4 Or objEmail.LongType = 5 Or objEmail.LongType = 6 Or objEmail.LongType = 7 Or objEmail.LongType = 8 Or objEmail.LongType = 9 Or objEmail.LongType = 10 Then
            lnkWishList.Visible = False
            lnkWishListComponents.Visible = False
            lnkCustomer.Visible = True
            lnkVendor.Visible = True
            lnkMerchant.Visible = True
            lnkProducts.Visible = True
            lnkBilling.Visible = True
            lnkShipping.Visible = True
            lnkOrderTotal.Visible = True
        End If
        DataBind()
        SetVisibility(Me.hdnEMailType.Value)
    End Sub

    Public Function EditByType(ByVal type As Long) As CXMLEMailContent
        Dim objEMailContent As CEMailContent
        Dim objEMailAccess As New CEMailContent
        Dim arr As New ArrayList
        Dim arr2 As New ArrayList
        arr = objEMailAccess.GetAllEMailContent()
        Dim objEmail As CXMLEMailContent = Nothing
        Dim exists As Boolean = False

        For Each objEMailContent In arr
            If (objEMailContent.Type = type And objEMailContent.IsActive) Then
                ' Types 
                ' (0) ' EMail a friend
                ' (1) ' Forgot Password
                ' (2) ' Wish List
                ' (4) ' Confirm
                ' (11) ' Low Stock Notice
                If (type = 0 Or type = 1 Or type = 2 Or type = 4 Or type = 5 Or type = 6 Or type = 11 Or type = 12) Then  ' No submenus to worry about
                    If objEMailContent.Format.Equals("TEXT") Then
                        ddFormat.SelectedIndex = 0
                        txtMessage.Text = objEMailContent.Body
                        txtMessage.Visible = True

                        'Get Html value in hdntext field
                        Dim objEMailContent2 As New CEMailContent
                        For Each objEMailContent2 In arr
                            If (objEMailContent2.Type = type And objEMailContent2.Format = "HTML") Then
                                Me.HTMLBody = objEMailContent2.Body
                                hdnText.Value = objEMailContent2.Body
                                Exit For
                            End If
                        Next
                        editemaildynamic1.Visible = False
                        lblPanel.Visible = True
                        objEmail = objEMailContent.GetCXMLEMailContent(GetEMailContentUID(type, "text"))
                    ElseIf objEMailContent.Format.Equals("HTML") Then
                        ddFormat.SelectedIndex = 1
                        Me.HTMLBody = objEMailContent.Body
                        Me.Subject = objEMailContent.Subject
                        hdnText.Value = objEMailContent.Body

                        If (m_bNetscape = False) Then
                            txtMessage.Visible = False
                            editemaildynamic1.Visible = True
                            lblPanel.Visible = False
                        Else
                            txtMessage.Text = objEMailContent.Body
                            txtMessage.Visible = True
                            editemaildynamic1.Visible = False
                            lblPanel.Visible = True
                        End If

                        'Get Text value
                        Dim objEMailContent2 As New CEMailContent
                        For Each objEMailContent2 In arr
                            If (objEMailContent2.Type = type And objEMailContent2.Format = "Text") Then
                                txtMessage.Text = objEMailContent2.Body
                                Exit For
                            End If
                        Next
                        objEmail = objEMailContent.GetCXMLEMailContent(GetEMailContentUID(type, "html"))
                    End If
                    lblSubject.Text = "Subject"
                    lblSubject.Visible = True
                    txtSubject.Text = objEMailContent.Subject
                    lblEmailType.Text = objEMailContent.Type.ToString.Replace("_", " ")
                    txtSubject.Enabled = True
                    hdnEMailType.Value = objEMailContent.LongType
                    lblPanel.Text = getText(objEMailContent.LongType)
                    'btnActivate.Text = "Deactivate"
                    exists = True
                    Exit For
                Else 'Smaller Subsection of e-mails -- subject disabled
                    If objEMailContent.Format.Equals("TEXT") Then
                        ddFormat.SelectedIndex = 0
                        txtMessage.Text = objEMailContent.Body
                        editemaildynamic1.Visible = False
                        txtMessage.Visible = True
                        lblPanel.Visible = True
                        objEmail = objEMailContent.GetCXMLEMailContent(GetEMailContentUID(type, "text"))
                    ElseIf objEMailContent.Format.Equals("HTML") Then
                        ddFormat.SelectedIndex = 1

                        If (m_bNetscape = False) Then
                            txtMessage.Visible = False
                            editemaildynamic1.Visible = True
                            lblPanel.Visible = False
                            editemaildynamic1.TextBody = Server.UrlEncode(objEMailContent.Body)
                        Else
                            txtMessage.Visible = True
                            editemaildynamic1.Visible = False
                            lblPanel.Visible = True
                            txtMessage.Text = objEMailContent.Body
                        End If
                        objEmail = objEMailContent.GetCXMLEMailContent(GetEMailContentUID(type, "html"))
                        hdnText.Value = objEMailContent.Body
                        Me.HTMLBody = objEmail.Body
                    End If
                    txtSubject.Text = objEMailContent.Type.ToString.Replace("_", " ")
                    lblSubject.Text = objEmail.Subject
                    txtSubject.Visible = True
                    txtSubject.Enabled = False
                    lblEmailType.Text = "Type"
                    Me.EMailType = objEMailContent.LongType
                    hdnEMailType.Value = objEMailContent.LongType
                    lblPanel.Text = getText(objEMailContent.LongType)
                    'btnActivate.Text = "Deactivate"
                    exists = True
                    Exit For
                End If
            End If
        Next
        'If exists = False Then ' deactivated -- give textbox as default
        '    For Each objEMailContent In arr
        '        If (objEMailContent.Type = type And (Not objEMailContent.IsActive) And objEMailContent.Format = "TEXT") Then
        '            ddFormat.SelectedIndex = 0
        '            txtMessage.Text = objEMailContent.Body
        '            txtSubject.Text = objEMailContent.Subject
        '            txtMessage.Visible = True
        '            hdnEMailType.Value = objEMailContent.LongType
        '            lblPanel.Visible = True
        '            lblPanel.Text = getText(hdnEMailType.Value)

        '            'Get Html value in hdntext field
        '            Dim objEMailContent2 As New CEMailContent()
        '            For Each objEMailContent2 In arr
        '                If (objEMailContent2.Type = type And objEMailContent2.Format = "HTML") Then
        '                    Me.HTMLBody = objEMailContent2.Body
        '                    hdnText.Value = objEMailContent2.Body
        '                    Exit For
        '                End If
        '            Next
        '            editemaildynamic1.Visible = False
        '            objEmail = objEMailContent.GetCXMLEMailContent(GetEMailContentUID(type, "text"))
        '        End If
        '    Next
        '    'btnActivate.Text = "Activate"
        'End If
        Return objEmail
    End Function

    Public Function GetEMailContentUID(ByVal type As Integer, ByVal format As String) As Long
        Dim objEmailContent As New CEMailContent
        m_objCXMLEmailContent = objEmailContent.GetEMailContentUID(type, format)
        Return m_objCXMLEmailContent.ID
    End Function

    Public Function getText(ByVal type As Integer) As String
        Dim str As String = ""
        If type = 0 Then
            str = "[RecipientName]" & "<br>" & "[SenderName]" & "<br>" & "[ProductLink]" & "<br>" & "[ProductName]" & "<br>" & "[ProductDescription]" & "<br>" & "[ProductImage]" & "<br>" & "[PersonalMessage]" & "<br>" & "[StoreName]" & "<br>" & "[StoreURL]"
        ElseIf type = 1 Then
            str = "[RecipientFirstName]" & "<br>" & "[RecipientLastName]" & "<br>" & "[RecipientEmailAddress]" & "<br>" & "[Password]" & "<br>" & "[StoreName]" & "<br>" & "[StoreURL]"
        ElseIf type = 2 Then
            str = "[RecipientName]" & "<br>" & "[SenderName]" & "<br>" & "[WishList]" & "<br>" & "[PersonalMessage]" & "<br>" & "[StoreName]" & "<br>" & "[StoreURL]"
        ElseIf type = 3 Then
            str = "[ProductID]" & "<br>" & "[ProductName]" & "<br>" & "[ProductDescription]" & "<br>" & "[ProductAttributes]" & "<br>" & "[Price]" & "<br>" & "[SalePrice]" & "<br>" & "[ProductLink]"
        ElseIf type = 4 Then
            str = "[ProductID]" & "<br>" & "[CustomerFirstName]" & "<br>" & "[CustomerLastName]" & "<br>" & "[OrderID]" & "<br>" & "[OrderTotal]" & "<br>" & "[BillingInfo]" & "<br>" & "[ProductsShippingInfo]" & "<br>" & "[OrderDetailsLink]" & "<br>" & "[StoreName]" & "<br>" & "[StoreURL]"
        ElseIf type = 5 Then
            str = "[OrderID]" & "<br>" & "[OrderTotal]" & "<br>" & "[BillingInfo]" & "<br>" & "[ProductsShippingInfo]" & "<br>" & "[StoreName]" & "<br>" & "[StoreURL]"
        ElseIf type = 6 Then
            str = "[OrderID]" & "<br>" & "[OrderTotal]" & "<br>" & "[BillingInfo]" & "<br>" & "[ProductsShippingInfo]" & "<br>" & "[StoreName]" & "<br>" & "[StoreURL]"
            'Verisign Recurring Billing
        ElseIf type = 7 Then
            str = "[ProductID]" & "<br>" & "[ProductName]" & "<br>" & "[ProductAttributes]" & "<br>" & "[ProductPrice]" & "<br>" & "[ProductQuantity]" & "<br>" & "[RecurringAmount]" & "<br>" & "[Payperiod]" & "<br>" & "[Term]"
            'Verisign Recurring Billing
        ElseIf type = 8 Then
            str = "[BillingName]" & "<br>" & "[BillingCompany]" & "<br>" & "[BillingAddress1]" & "<br>" & "[BillingAddress2]" & "<br>" & "[BillingCity]" & "<br>" & "[BillingState]" & "<br>" & "[BillingZip]" & "<br>" & "[BillingCountry]" & "<br>" & "[BillingPhone]" & "<br>" & "[BillingFax]" & "<br>" & "[BillingEMail]" & "<br>" & "[BillingPaymentMethod]"
        ElseIf type = 9 Then
            ' #1314  SV 
            str = "[ShippingName]" & "<br>" & "[ShippingCompany]" & "<br>" & "[ShippingAddress1]" & "<br>" & "[ShippingAddress2]" & "<br>" & "[ShippingCity]" & "<br>" & "[ShippingState]" & "<br>" & "[ShippingZip]" & "<br>" & "[ShippingCountry]" & "<br>" & "[ShippingPhone]" & "<br>" & "[ShippingFax]" & "<br>" & "[ShippingEMail]" & "<br>" & "[ShippingMethod]" & "<br>" & "[ShippingProducts]" & "<br>" & "[ShippingInstructions]"
            ' #1314  SV
        ElseIf type = 10 Then
            str = "[OrderMerchandiseTotal]" & "<br>" & "[OrderDiscounts]" & "<br>" & "[OrderSubtotal]" & "<br>" & "[OrderLocalTax]" & "<br>" & "[OrderStateProvinceTax]" & "<br>" & "[OrderCountryTax]" & "<br>" & "[OrderShipping]" & "<br>" & "[OrderHandling]" & "<br>" & "[OrderTotal]" & "<br>" & "[OrderGiftCertificate]" & "<br>" & "[OrderGrandTotal]"
        ElseIf type = 11 Then
            str = "[ProductID]" & "<br>" & "[ProductCode]" & "<br>" & "[ProductName]" & "<br>" & "[ManufacturerName]" & "<br>" & "[VendorName]" & "<br>" & "[ProductLink]" & "<br>" & "[ManufacturerName]" & "<br>" & "[ProductInventoryCount]" & "<br>" & "[SoreURL]" & "<br>" & "[StoreName]"
        ElseIf type = 12 Then 'Ebay Checkout Notification
            str = "[ListingNumber]" & "<br>" & "[ProductName]" & "<br>" & "[BuyerUserID]" & "<br>" & "[Price]" & "<br>" & "[QuantitySold]" & "<br>" & "[SubTotal]" & "<br>" & "[MerchantEmailAddress]" & "<br>" & "[StoreName]" & "<br>" & "[CheckoutUrl]"
        End If
        str = "<b>Dynamic Data Tag Menu</b><p>" & vbCrLf & str
        Return str
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveEmail()
        SetVisibility(Me.hdnEMailType.Value)
    End Sub
End Class


#Region "EmailTypeContainer"
Public Class EmailTypeContainer
    Private m_uid As Long
    Private m_TypeID As Long
    Private m_Subject As String
    Private m_Body As String
    Private m_Format As String
    Private m_isActive As Integer

#Region "Constructors"
    Public Sub New()

    End Sub

    Public Sub New(ByVal UID As Long, ByVal TypeID As Long, ByVal Subject As String, ByVal Body As String, ByVal Format As String, ByVal isActive As Integer)
        m_uid = UID
        m_TypeID = TypeID
        m_Subject = Subject
        m_Body = Body
        m_Format = Format
        m_isActive = isActive
    End Sub
#End Region


    Public Property uid() As Long
        Get
            Return m_uid
        End Get
        Set(ByVal Value As Long)
            m_uid = Value
        End Set
    End Property

    Public Property TypeID() As Long
        Get
            Return m_TypeID
        End Get
        Set(ByVal Value As Long)
            m_TypeID = Value
        End Set
    End Property

    Public Property Subject() As String
        Get
            Return m_Subject
        End Get
        Set(ByVal Value As String)
            m_Subject = Value
        End Set
    End Property

    Public Property Body() As String
        Get
            Return m_Body
        End Get
        Set(ByVal Value As String)
            m_Body = Value
        End Set
    End Property

    Public Property Format() As String
        Get
            Return m_Format
        End Get
        Set(ByVal Value As String)
            m_Format = Value
        End Set
    End Property

    Public Property IsActive() As Integer
        Get
            Return m_isActive
        End Get
        Set(ByVal Value As Integer)
            m_isActive = Value
        End Set
    End Property

End Class
#End Region





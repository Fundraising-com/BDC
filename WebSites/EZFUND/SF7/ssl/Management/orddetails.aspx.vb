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

Imports System.Text
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.UITools
Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Partial Class orddetails
    Inherits CWebPage
    Protected BillingAddress As AddressLabel
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Email As System.Web.UI.WebControls.Label
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
   
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
    Private m_objOrderHistory As BusinessRule.Orders.COrders
    Private m_objOrder As BusinessRule.Orders.COrder
    Private m_objPaymentMethodAccess As CXMLPaymentMethodAccess

    Private m_objShipMethodAccess As CXMLShipMethodAccess

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.SalesReports) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            Dim objOrder As COrder = Nothing
            If (IsPostBack = False) Then
                CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
                TotalDisplay1.OandaISO = Session("ConvertISO")
                TotalDisplay1.OandaRate = CDec(Session("OandaRate"))
                If (IsNothing(Session("OrderHistory")) = True) And (IsNothing(Request.QueryString("OrderID")) = False) Then
                    objOrder = New COrder(CLng(Request.QueryString("OrderId")))
                    If objOrder.OrderAddresses.Count = 0 Then
                        objOrder.LoadOrderAddresses()
                    End If
                    m_objOrder = objOrder
                    lblOrderID.Text = m_objOrder.OrderNumber
                    lblOrderDate.Text = m_objOrder.OrderDate
                Else
                    m_objOrderHistory = Session("OrderHistory")

                    For Each objOrder In m_objOrderHistory.Orders
                        If (objOrder.UID = Request.QueryString("OrderID")) Then
                            m_objOrder = objOrder
                            If objOrder.OrderAddresses.Count = 0 Then
                                objOrder.LoadOrderAddresses()
                            End If
                            lblOrderID.Text = m_objOrder.OrderNumber
                            lblOrderDate.Text = m_objOrder.OrderDate
                            Exit For
                        End If
                    Next
                End If
                BillingAddress.AddressSource = m_objOrder.BillAddress
                lblEmail.Text = m_objOrder.BillAddress.EMail
                m_objPaymentMethodAccess = StoreFrontConfiguration.PaymentMethodAccess
                lblPaymethod.Text = m_objPaymentMethodAccess.GetPaymentMethodName(objOrder.PaymentMethod())

                Try
                    objOrder = New COrder(m_objOrder.OrderNumber)
                    Me.lblAccountNumber.Text = Me.GetDecryptedValue(objOrder.OrderPayment, objOrder.OrderPayment.AccountNumber)
                    Me.lblBankName.Text = objOrder.OrderPayment.BankName
                    Me.lblCCNumber.Text = GetCreditCardNumber(objOrder.OrderPayment)
                    Me.lblCCType.Text = objOrder.OrderPayment.CardType
                    Me.lblExpireDate.Text = objOrder.OrderPayment.ExpireMonth & "/" & objOrder.OrderPayment.ExpireYear
                    Me.lblPONumber.Text = objOrder.OrderPayment.PONumber
                    Me.lblRoutingNumber.Text = Me.GetDecryptedValue(objOrder.OrderPayment, objOrder.OrderPayment.RoutingNumber)
                Catch ex As System.Security.Cryptography.CryptographicException
                    ErrorMessage.Visible = True
                    ErrorMessage.Text = "Error Occured during Decryption ." & ex.Message
                End Try


                If (lblPaymethod.Text.ToLower = "paypal" Or lblCCType.Text.ToLower = "paypal transaction") Then
                    tblCreditCard.Visible = False
                    tblECheck.Visible = False
                    tblPurchaseOrder.Visible = False
                End If
                If lblPaymethod.Text.ToLower = "echeck" Then
                    'PrintEcheck.Visible = True
                    tblCreditCard.Visible = False
                    tblECheck.Visible = True
                    tblPurchaseOrder.Visible = False
                End If
                If lblPaymethod.Text.ToLower = "credit card" Then
                    tblCreditCard.Visible = True
                    tblECheck.Visible = False
                    tblPurchaseOrder.Visible = False
                End If
                If lblPaymethod.Text.ToLower = "po" Then
                    tblECheck.Visible = False
                    tblCreditCard.Visible = False
                    tblPurchaseOrder.Visible = True
                End If
                If lblPaymethod.Text.ToLower = "phonefax" Then
                    tblECheck.Visible = False
                    If (lblPONumber.Text.Trim.Length > 0) Then
                        tblCreditCard.Visible = False
                        tblPurchaseOrder.Visible = True
                    Else
                        tblCreditCard.Visible = True
                        tblPurchaseOrder.Visible = False
                    End If
                End If
                If lblPaymethod.Text.ToLower = "cod" Then
                    tblCreditCard.Visible = False
                    tblECheck.Visible = False
                    tblPurchaseOrder.Visible = False
                End If
                If (m_objOrder.OrderAddresses.Count = 1) Then
                    TotalDisplay1.DisplayMerchandiseTotal = True
                    TotalDisplay1.DisplayDiscountTotal = True
                    TotalDisplay1.DisplaySubTotal = True
                    TotalDisplay1.DisplayLocalTaxTotal = True
                    TotalDisplay1.DisplayStateTaxTotal = True
                    TotalDisplay1.DisplayCountryTaxTotal = True
                    TotalDisplay1.DisplayShippingTotal = True
                    TotalDisplay1.DisplayHandlingTotal = True
                    TotalDisplay1.DisplayOrderTotal = False
                    TotalDisplay1.DisplayShipmentTotal = False
                Else
                    TotalDisplay1.DisplayMerchandiseTotal = False
                    TotalDisplay1.DisplayDiscountTotal = False
                    TotalDisplay1.DisplaySubTotal = False
                    TotalDisplay1.DisplayLocalTaxTotal = False
                    TotalDisplay1.DisplayStateTaxTotal = False
                    TotalDisplay1.DisplayCountryTaxTotal = False
                    TotalDisplay1.DisplayShippingTotal = False
                    TotalDisplay1.DisplayHandlingTotal = False
                    TotalDisplay1.DisplaySubHandlingTotal = False
                    TotalDisplay1.DisplayOrderTotal = False
                End If

                Datalist2.DataSource = m_objOrder.OrderAddresses
                Datalist2.DataBind()
                ' begin: JDB - Advanced Coupon Functionality
                Me.rDiscounts.DataSource = Me.m_objOrder.GetOrderDiscounts()
                Me.rDiscounts.DataBind()
                ' end: JDB - Advanced Coupon Functionality
                TotalDisplay1.DataSource = objOrder
                TotalDisplay1.DataBind()
            End If

            Dim con As DataListItem
            For Each con In Datalist2.Items
            	'BEGIN: GJV - 8/23/2007 - OSP merge
                CType(con.FindControl("imgTrack"), System.Web.UI.WebControls.Image).ImageUrl = "images/track.jpg" '& dom.Item("SiteProducts").Item("SiteImages").Item("Track").Attributes("Filename").Value()
                'END: GJV - 8/23/2007 - OSP merge
            Next
        Catch ex As Exception
            Session("DetailError") = "Class OrdDetails Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    ' begin: JDB - Advanced Coupon Functionality
    Protected DiscountTotal As Decimal = 0
    Protected Function HandleDiscountAmount(ByVal dDiscountAmount As Decimal) As Decimal
        Me.DiscountTotal += dDiscountAmount
        Return dDiscountAmount
    End Function
    ' end: JDB - Advanced Coupon Functionality

    Public ReadOnly Property OrderID() As Long
        Get
            Return m_objOrder.UID()
        End Get
    End Property

    Public ReadOnly Property DownloadDate() As Date
        Get
            If (m_objOrder.DownloadDate = "") Then
                Return Nothing
            Else
                Return CDate(m_objOrder.DownloadDate())
            End If
        End Get
    End Property

    Private Sub Datalist2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles Datalist2.ItemCreated
        If (IsPostBack = False) Then
            If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
                If (m_objOrder.OrderAddresses.Count = 1) Then
                    CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).Visible = False
                Else
                    If (IsNothing(e.Item.DataItem) = False) Then
                        CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).DataSource = e.Item.DataItem
                        CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).DataBind()
                        CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).OandaISO = Session("ConvertISO")
                        CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).OandaRate = CDec(Session("OandaRate"))
                    End If
                End If
                If (IsNothing(e.Item.DataItem) = False) Then
                    If (IsNothing(m_objShipMethodAccess) = True) Then
                        m_objShipMethodAccess = StoreFrontConfiguration.ShipMethodAccess
                    End If
                    CType(e.Item.FindControl("ShipMethod"), Label).Text = m_objShipMethodAccess.GetShipMethodName(CType(e.Item.DataItem, COrderAddress).Address.ShipMethod())
                End If
            End If
        End If
        If (Not (IsNothing(e.Item.FindControl("DynaCartDisplay2")))) Then
            CType(e.Item.FindControl("DynaCartDisplay2"), DynamicCartDisplay).RemoveImg = dom.Item("SiteProducts").Item("SiteImages").Item("Remove").Attributes("Filename").Value
            CType(e.Item.FindControl("DynaCartDisplay2"), DynamicCartDisplay).SavedCartImg = dom.Item("SiteProducts").Item("SiteImages").Item("SaveCart").Attributes("Filename").Value
            CType(e.Item.FindControl("DynaCartDisplay2"), DynamicCartDisplay).ReOrderImg = dom.Item("SiteProducts").Item("SiteImages").Item("ReOrder").Attributes("Filename").Value
            CType(e.Item.FindControl("DynaCartDisplay2"), DynamicCartDisplay).GiftWrapImg = dom.Item("SiteProducts").Item("SiteImages").Item("GiftWrap").Attributes("Filename").Value
        End If
    End Sub

    Public Sub BtnTrackClick(ByVal source As Object, ByVal e As System.EventArgs)
        Response.Redirect("TrackShipment.aspx?" & source.CommandArgument)
    End Sub
    Private Function GetCreditCardNumber(ByVal mOrderPayment As COrderPayment) As String
        Dim sReturn As String
        sReturn = Me.GetDecryptedValue(mOrderPayment, mOrderPayment.CreditCardNumber)
        If sReturn.Length <= 0 Then
            Return "************" & mOrderPayment.LastFourDigits
        Else
            Return sReturn
        End If
    End Function
    Private Function GetDecryptedValue(ByVal mOrderPayment As COrderPayment, ByVal Value As String) As String
        Try
            If Not StoreFrontConfiguration.ConvertedFrom3DES Then
                Dim objKey(7) As Byte
                Dim objIV(7) As Byte
                Dim sReturn As String = String.Empty
                If (Not Value Is Nothing) AndAlso Value.Trim.Length > 0 Then
                    sReturn = Value
                    'sp8 - # 2821
                    If Not lblPaymethod.Text.Equals("eCheck") Then
                        decryptIt(objKey, objIV, sReturn)
                    End If
                    Return sReturn
                End If
                Return String.Empty
            End If
        Catch ex As Exception
            ErrorMessage.Visible = True
            ErrorMessage.Text = "Error Decrypting value ." & ex.Message
            Return Value
        End Try


        Try
            If Not Session(FulFillmentControl.ENCRYPTIONKEY) Is Nothing AndAlso _
              Not mOrderPayment.isDirty AndAlso _
              Value.Trim.Length > 0 Then
                Return (New StoreFrontSecurity.StoreFrontRSACrypto).Decrypt(Session(FulFillmentControl.ENCRYPTIONKEY).ToString, Value)
            End If
            Return String.Empty
        Catch ex As Exception
            ErrorMessage.Visible = True
            ErrorMessage.Text = "Error Decrypting value ." & ex.Message
            Return String.Empty
        End Try
    End Function
#Region "Private Sub decryptIt(ByRef objKey As Byte(), ByRef objIV As Byte(),byRef strValue as string)"
    Private Sub decryptIt(ByRef objKey As Byte(), ByRef objIV As Byte(), ByRef strValue As String)
        Dim st() As String
        Dim i As Long, j As Long, x As Long, k As Long, y As Long
        For i = 0 To objKey.GetUpperBound(0)
            objKey.SetValue(Nothing, i)
        Next
        For i = 0 To objIV.GetUpperBound(0)
            objIV.SetValue(Nothing, i)
        Next

        st = Split(strValue)

        For i = 0 To objKey.GetUpperBound(0)
            objKey.SetValue(CByte(CInt(st.GetValue(i))), i)
            j = i
        Next
        j = j + 1
        For i = j To j + objIV.GetUpperBound(0)
            objIV.SetValue(CByte(CInt(st.GetValue(i))), x)
            k = i
            x = x + 1
        Next
        k = k + 1
        Dim arEncrypt(st.GetUpperBound(0) - k) As Byte
        For i = k To st.GetUpperBound(0)
            arEncrypt.SetValue(CByte(CInt(st.GetValue(i))), y)
            y = y + 1
        Next

        Dim obj As New StoreFrontSecurity.CStoreFrontCrypto2(arEncrypt)
        obj.Type = StoreFrontSecurity.CryptoType.Decrypt
        strValue = obj.GetData(objKey, objIV)
        obj = Nothing
    End Sub
#End Region
End Class

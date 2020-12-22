' -----------------------------------------------------------------------------
' SP7 - PayPal/VeriSign Integration
' -----------------------------------------------------------------------------
' <summary>This page handles all paypal express requests.
' </summary>
' <remarks>
' </remarks>
' <history>AB Code 
'		Created On:	05/11/2005
'		Last Revised on :
' </history>
' -----------------------------------------------------------------------------
'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders

Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Partial Class HandlePayPalExpress
    Inherits CWebPage

#Region "Members"
    Private mPayPalExpress As Processors.CPayPalExpress
    Private mFrom As String = String.Empty
#End Region


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
        mFrom = Request.QueryString("From")
        mPayPalExpress = New Processors.CPayPalExpress

        mPayPalExpress.SessionID = MyBase.m_objCustomer.GetSessionID
        Try
            mPayPalExpress.LoadProfile(Server.MapPath(Me.TemplateSourceDirectory))
        Catch ex As Exception
            Session("ProfileError") = ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "ShoppingCart.aspx?WebID=" & mPayPalExpress.SessionID & "&ProfileError=True")
        End Try
        Select Case mFrom

            Case "PP" ' from PayPal
                mPayPalExpress.PayPalTokenID = Request.QueryString("token")
                Session("PayPalTokenID") = mPayPalExpress.PayPalTokenID
                Try
                    mPayPalExpress.Order = Session("Order")
                    mPayPalExpress.GetExpressCheckout()
                    If IsNothing(Request.QueryString("Method")) = False AndAlso Request.QueryString("Method").ToLower.Equals("paypal") Then
                        ' if paypal is the payment method
                        mPayPalExpress.Order.IsPayPalOrder = True
                        mPayPalExpress.SetBillingAddress()
                        Session("Order") = mPayPalExpress.Order
                        Response.Redirect("Payment.aspx?WebID=" & mPayPalExpress.SessionID)
                    Else
                        'this is express checkout 
                        mPayPalExpress.ValidateUser(m_objCustomer)
                        If mPayPalExpress.PayPalUserExists Then
                            'auto login
                            m_objCustomer = mPayPalExpress.Customer
                            m_objXMLCart.CustomerGroup = m_objCustomer.CustomerGroup
                            m_objXMLCart.LoadFromDB()
                            MyBase.CheckCart()
                            mPayPalExpress.CreateOrder(m_objXMLCart)
                            Session("Order") = mPayPalExpress.Order
                            Response.Redirect("ShipSummary.aspx?WebID=" & mPayPalExpress.SessionID)
                        Else
                            'customer could not be identified - proceed to Sign In Page
                            Session("PayPalEmail") = mPayPalExpress.PayPalEmail
                            Response.Redirect("CustSignInCheckout.aspx?ReturnPage=HandlePayPalExpress.aspx")
                        End If
                    End If

                Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
                    Response.Redirect(StoreFrontConfiguration.SiteURL & "ShoppingCart.aspx?WebID=" & mPayPalExpress.SessionID & "&PayPalError=True")
                End Try

            Case "SC" ' from Shopping Cart
                m_objXMLCart.CustomerGroup = m_objCustomer.CustomerGroup
                m_objXMLCart.LoadFromDB()
                Dim strReferrer As String
                strReferrer = Request.QueryString("Affiliate")
                Try
                    If IsNothing(strReferrer) = False Then
                        mPayPalExpress.SetExpressCheckout(MyBase.m_objXMLCart.OrderTotal, MyBase.m_objCustomer.GetSessionID, strReferrer)
                    Else
                        mPayPalExpress.SetExpressCheckout(MyBase.m_objXMLCart.OrderTotal, MyBase.m_objCustomer.GetSessionID)
                    End If
                    Session.Add("PayPalTokenID", mPayPalExpress.PayPalTokenID)
                    ' Response.Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" & mPayPalExpress.PayPalTokenID)
                    Response.Redirect(mPayPalExpress.URI & "?cmd=_express-checkout&token=" & mPayPalExpress.PayPalTokenID)
                Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
                    Response.Redirect(StoreFrontConfiguration.SiteURL & "ShoppingCart.aspx?WebID=" & mPayPalExpress.SessionID & "&PayPalError=True")
                End Try

            Case "CS" ' from CustSignInCheckOut
                Try
                    mPayPalExpress.PayPalTokenID = Session("PayPalTokenID")
                    mPayPalExpress.GetExpressCheckout()
                    m_objXMLCart.CustomerGroup = m_objCustomer.CustomerGroup
                    mPayPalExpress.Customer = m_objCustomer
                    m_objXMLCart.LoadFromDB()
                    MyBase.CheckCart()
                    mPayPalExpress.CreateOrder(m_objXMLCart)
                    Session("Order") = mPayPalExpress.Order
                    Response.Redirect("ShipSummary.aspx?WebID=" & mPayPalExpress.SessionID)
                Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
                    Response.Redirect(StoreFrontConfiguration.SiteURL & "ShoppingCart.aspx?WebID=" & mPayPalExpress.SessionID & "&PayPalError=True")
                End Try

            Case "SS" 'from ShipSummary
                Dim mShipToEdit As String
                mShipToEdit = Request.QueryString("Edit")
                mPayPalExpress.PayPalTokenID = Session("PayPalTokenID")
                mPayPalExpress.Customer = m_objCustomer
                mPayPalExpress.Order = Session("Order")
                If IsNothing(mShipToEdit) = False AndAlso mShipToEdit.ToLower = "true" Then
                    'customer wants to edit ShipTo Address
                    Try
                        mPayPalExpress.EditShipTo = True
                        If IsNothing(m_objCustomer.Referer) = False Then
                            mPayPalExpress.SetExpressCheckout(MyBase.m_objXMLCart.OrderTotal, MyBase.m_objCustomer.GetSessionID, m_objCustomer.Referer)
                        Else
                            mPayPalExpress.SetExpressCheckout(MyBase.m_objXMLCart.OrderTotal, MyBase.m_objCustomer.GetSessionID)
                        End If
                        m_objXMLCart.SaveToDB()
                        'Response.Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" & mPayPalExpress.PayPalTokenID)
                        Response.Redirect(mPayPalExpress.URI & "?cmd=_express-checkout&token=" & mPayPalExpress.PayPalTokenID)
                    Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
                        mPayPalExpress.Order.IsPayPalOrder = False
                        Session("Order") = mPayPalExpress.Order
                        Session("PayPalError") = "We are sorry. Your order could not be completed using PayPal. <br> Please choose another form of payment."
                        Response.Redirect("ShipSummary.aspx?WebID=" & mPayPalExpress.SessionID & "&PayPalError=True")
                    End Try
                ElseIf IsNothing(Request.QueryString("Method")) = False AndAlso Request.QueryString("Method").ToLower.Equals("paypal") Then
                    'paypal as payment method
                    mPayPalExpress.PayPalTokenID = Session("PayPalTokenID")
                    mPayPalExpress.AddressOverride = True
                    mPayPalExpress.Customer = m_objCustomer
                    mPayPalExpress.Order = Session("Order")
                    Try
                        If IsNothing(m_objCustomer.Referer) = False Then
                            mPayPalExpress.SetExpressCheckout(MyBase.m_objXMLCart.OrderTotal, MyBase.m_objCustomer.GetSessionID, m_objCustomer.Referer)
                        Else
                            mPayPalExpress.SetExpressCheckout(MyBase.m_objXMLCart.OrderTotal, MyBase.m_objCustomer.GetSessionID)
                        End If
                        ' Response.Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" & mPayPalExpress.PayPalTokenID)
                        Response.Redirect(mPayPalExpress.URI & "?cmd=_express-checkout&token=" & mPayPalExpress.PayPalTokenID)
                    Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
                        mPayPalExpress.Order.IsPayPalOrder = False
                        Session("Order") = mPayPalExpress.Order
                        Session("PayPalError") = "We are sorry. Your order could not be completed using PayPal. <br> Please choose another form of payment."
                        Response.Redirect("ShipSummary.aspx?WebID=" & mPayPalExpress.SessionID & "&PayPalError=True")
                    End Try
                Else
                    'final checkout - ExpressCheckout
                    Try
                        mPayPalExpress.DoExpressCheckout()
                        Response.Redirect("Confirm.aspx?WebID=" & mPayPalExpress.SessionID)
                    Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
                        mPayPalExpress.Order.IsPayPalOrder = False
                        Session("Order") = mPayPalExpress.Order
                        Session("PayPalError") = "We are sorry. Your order could not be completed using PayPal. <br> Please choose another form of payment."
                        Response.Redirect("ShipSummary.aspx?WebID=" & mPayPalExpress.SessionID & "&PayPalError=True")
                    End Try
                End If
            Case "Payment"
                'from Payment.aspx page
                mPayPalExpress.PayPalTokenID = Session("PayPalTokenID")
                mPayPalExpress.Customer = m_objCustomer
                mPayPalExpress.Order = Session("Order")
                Try
                    mPayPalExpress.DoExpressCheckout()
                    Response.Redirect("Confirm.aspx?WebID=" & mPayPalExpress.SessionID)
                Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
                    mPayPalExpress.Order.IsPayPalOrder = False
                    Session("Order") = mPayPalExpress.Order
                    Session("PayPalError") = "We are sorry. Your order could not be completed using PayPal. <br> Please choose another form of payment."
                    '  Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
                    Response.Redirect("ShipSummary.aspx?WebID=" & mPayPalExpress.SessionID & "&PayPalError=True")
                End Try
            Case Else

        End Select

    End Sub


End Class
'End SP7
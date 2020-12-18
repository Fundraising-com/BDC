'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Xml

Imports System.Math

Imports System.Text
Imports System.Net
Imports System.IO

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.UITools
Imports StoreFront.StoreFront.FrameworkExceptions

Public Class CWebControl
    Inherits UserControl

    Protected WithEvents SimpleSearch1 As SimpleSearch
    Protected m_objCustomer As CCustomer
    Protected m_objXMLCart As CCart
    Protected m_objXMLAccess As CXMLProductAccess
    Protected m_arEMailContent As ArrayList
    'Protected m_XMLMessages As CXMLMessages
    Protected m_objInstructions As CXMLInstructions
    Protected m_objMessages As CXMLMessages
    Protected m_objCartList As CartList
    Protected dom As XmlDocument
    Protected m_Affiliate As CAffiliate

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dom = StoreFrontConfiguration.XMLDocument
        'm_XMLMessages = StoreFrontConfiguration.XMLMessages
        m_objInstructions = StoreFrontConfiguration.InstructionsAccess

        m_objXMLCart = Session("XMLShoppingCart")
        m_objCustomer = Session("Customer")
        m_objXMLAccess = StoreFrontConfiguration.ProductAccess
        m_arEMailContent = Session("EMailContent")
        m_objMessages = StoreFrontConfiguration.MessagesAccess
        m_Affiliate = Session("Affiliate")

    End Sub

    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
        Trace.IsEnabled = True
        Dim objError As New CStoreFrontWebError(Err)
        objError.TrackInfo(Me.GetType().ToString(), "Page_Error")
    End Sub

    Public Property CartListControl() As CartList
        Get
            Return m_objCartList
        End Get
        Set(ByVal Value As CartList)
            m_objCartList = Value
        End Set
    End Property

#Region "Function CurrentWebPage"
    Public Function CurrentWebPage() As String
        Dim sTemp As String
        sTemp = Request.Url.ToString
        If InStr(sTemp, "?") > 0 Then
            Return sTemp.Substring(0, InStr(sTemp, "?") - 1)
        Else
            Return sTemp
        End If

    End Function
#End Region

#Region "UpdateXMLQuantity"
    Public Overridable Sub UpdateXMLQuantity(ByVal Cart As DynamicCartDisplay, ByVal Total As Label)
        Dim i As Integer
        Dim objItem As CCartItem 'CXMLShoppingCartItem
        Dim obj As CCartItem
        Dim item As RepeaterItem
        Dim objText As TextBox
        Dim objLabel As Label
        Dim ar As New ArrayList()
        Dim lngID As Long = 1
        Dim objTemp As CCartItem
        Dim arOverInventory As New ArrayList()
        Dim objBizBase As New CBusinessBase()
        Cart.DataSource = m_objXMLCart.GetCartItems
        Cart.DataBind()

        For Each obj In m_objXMLCart.GetCartItems
            objText = CType(Cart.FindControl("Qty" & lngID), TextBox)
            If (CLng(objText.Text) = 0) Then
                ar.Add(obj)
            ElseIf (CLng(objText.Text) > 0) Then

                If obj.Inventory.InventoryTracked Then
                    If obj.Inventory.ItemsAreStocked(obj.Attributes, CLng(objText.Text)) Then
                        obj.Quantity = CLng(objText.Text)
                        objBizBase.UpdateQty(obj, Cart, objLabel, objText, lngID)
                        m_objXMLCart.UpdateItem(obj, lngID)
                    Else
                        objTemp = New CCartItem(obj, CLng(objText.Text), obj.Attributes, obj.CustomerGroup)
                        If Not (CLng(objText.Text) = obj.Quantity) Then
                            objTemp.Quantity = CLng(objText.Text) - obj.Quantity
                            arOverInventory.Add(objTemp)
                        End If
                    End If
                Else
                    obj.Quantity = CLng(objText.Text)

                    objBizBase.UpdateQty(obj, Cart, objLabel, objText, lngID)
                    m_objXMLCart.UpdateItem(obj, lngID)
                End If



                '#If AE = True Then
                '                If obj.IsGiftWrapable Then


                '                    objLabel = CType(Cart.FindControl("GiftQty" & lngID), Label)
                '                    If (CLng(objLabel.Text) > CLng(objText.Text)) Then
                '                        obj.GiftWrapQty = CLng(objText.Text)
                '                    End If
                '                End If
                '#End If

            End If
            lngID = lngID + 1
        Next
        lngID = 1
        For Each obj In ar
            m_objXMLCart.DeleteItem(obj, lngID)
            lngID = lngID + 1
        Next
        If arOverInventory.Count > 0 Then
            m_objXMLCart.UpdateItems(arOverInventory)
        End If
        If (m_objXMLCart.GetCartItems.Count = 0) Then
            Cart.Visible = False
        ElseIf (ar.Count > 0) Then
            '    Cart.IsUpdate = True
            Cart.NotPostBack = True
            Cart.DataSource = m_objXMLCart.GetCartItems
            Cart.DataBind()
        End If
    End Sub
#End Region

    Public Sub PriceDisplay(ByVal dPrice As Decimal)
        If (StoreFrontConfiguration.OandaID <> "") Then
            If (IsNothing(Session("OandaRate"))) Then
                Response.Write("<script>document.write('" & Format(dPrice, "c") & " ' + OandaConvert());</script>")
            Else
                Response.Write(Format(dPrice, "c") & " (" & Format(Round(CDec(Session("OandaRate")) * dPrice, 2), "F") & " <script>document.write(OandaConvert('" & Session("ConvertISO") & "'));</script>)")
            End If
        Else
            Response.Write(Format(dPrice, "c"))
        End If

    End Sub

    Public Function PriceDisplay2(ByVal dPrice As Decimal) As String

        If (StoreFrontConfiguration.OandaID <> "") Then
            If (IsNothing(Session("OandaRate"))) Then
                Return "<script>document.write('" & Format(dPrice, "c") & " ' + OandaConvert());</script>"
            Else
                Return Format(dPrice, "c") & " (" & Format(Round(CDec(Session("OandaRate")) * dPrice, 2), "F") & " <script>document.write(OandaConvert('" & Session("ConvertISO") & "'));</script>)"
            End If
        Else
            Return Format(dPrice, "c")
        End If

    End Function

    Public Function PriceDisplay3(ByVal dPrice As Decimal) As String

        If (StoreFrontConfiguration.OandaID <> "") Then
            If (IsNothing(Session("OandaRate"))) Then
                Return Format(dPrice, "c")
            Else
                Return Format(dPrice, "c") & " (" & Format(Round(CDec(Session("OandaRate")) * dPrice, 2), "F") & " " & Session("ConvertISO") & ")"
            End If
        Else
            Return Format(dPrice, "c")
        End If

        End Function

    Private Sub SimpleSearch1_SimpleSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SimpleSearch1.SimpleSearch_Click
        Response.Redirect(sender)
        Response.End()
    End Sub

    Protected Sub LabelText(ByVal objContainer As Object)
        Dim objLabel As Label
        Dim arLabels As New ArrayList()
        Dim strID As String

        arLabels.Add(New String("lblProductCode"))
        arLabels.Add(New String("lblDescription"))
        arLabels.Add(New String("lblPrice"))
        arLabels.Add(New String("lblVolumePrice"))
        arLabels.Add(New String("lblStockInfo"))
        arLabels.Add(New String("lblCategory"))
        arLabels.Add(New String("lblCategorys"))
        arLabels.Add(New String("lblManufacturer"))
        arLabels.Add(New String("lblManufacturers"))
        arLabels.Add(New String("lblVendor"))
        arLabels.Add(New String("lblVendors"))
        arLabels.Add(New String("lblProductName"))
        arLabels.Add(New String("lblMoreInfo"))
        arLabels.Add(New String("lblSalePrice"))

        For Each strID In arLabels
            objLabel = objContainer.FindControl(strID)
            If (IsNothing(objLabel) = False) Then
                If strID <> "lblMoreInfo" Then
                    If (StoreFrontConfiguration.Labels.Item(strID).InnerText().Trim.Length = 0) Then
                        objLabel.Text = ""
                    Else
                        objLabel.Text = StoreFrontConfiguration.Labels.Item(strID).InnerText() & ":&nbsp;"
                    End If
                Else
                    objLabel.Text = StoreFrontConfiguration.Labels.Item(strID).InnerText()
                End If
            End If
            objLabel = objContainer.FindControl(strID & "2")
            If (IsNothing(objLabel) = False) Then
                If (StoreFrontConfiguration.Labels.Item(strID).InnerText().Trim.Length = 0) Then
                    objLabel.Text = ""
                Else
                    objLabel.Text = StoreFrontConfiguration.Labels.Item(strID).InnerText() & ":&nbsp;"
                End If
            End If
        Next
    End Sub

    Protected Sub SetLabelVisible(ByVal objContainer As Object, ByVal bVisible As Boolean)
        Dim objLabel As Label
        Dim arLabels As New ArrayList()
        Dim strID As String

        arLabels.Add(New String("lblProductCode"))
        arLabels.Add(New String("lblDescription"))
        arLabels.Add(New String("lblPrice"))
        arLabels.Add(New String("lblVolumePrice"))
        arLabels.Add(New String("lblStockInfo"))
        arLabels.Add(New String("lblCategory"))
        arLabels.Add(New String("lblCategorys"))
        arLabels.Add(New String("lblManufacturer"))
        arLabels.Add(New String("lblManufacturers"))
        arLabels.Add(New String("lblVendor"))
        arLabels.Add(New String("lblVendors"))
        arLabels.Add(New String("lblProductName"))
        'arLabels.Add(New String("lblMoreInfo"))
        arLabels.Add(New String("lblSalePrice"))

        For Each strID In arLabels
            objLabel = objContainer.FindControl(strID)
            If (IsNothing(objLabel) = False) Then
                objLabel.Visible = bVisible
            End If
            objLabel = objContainer.FindControl(strID & "2")
            If (IsNothing(objLabel) = False) Then
                objLabel.Visible = bVisible
            End If
        Next
    End Sub
End Class


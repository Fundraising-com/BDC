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

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.UITools

Public Class CShoppingCartControlBase
    Inherits CWebControl

    Private m_objDynaCart As DynamicCartDisplay
    Private m_objTotalLabel As Label

#Region "UpdateQuantity"
    Public Sub UpdateQuantity()
        ' New XML function
        UpdateXMLQuantity(DynaCart, Total)
    End Sub
#End Region

    Protected Property Total() As Label
        Get
            Return m_objTotalLabel
        End Get
        Set(ByVal Value As Label)
            m_objTotalLabel = Value
        End Set
    End Property

    Public Property DynaCart() As DynamicCartDisplay
        Get
            Return m_objDynaCart
        End Get
        Set(ByVal Value As DynamicCartDisplay)
            m_objDynaCart = Value
        End Set
    End Property
End Class

Public Class CWishListSavedCartBase
    Inherits CWebPage

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        If (StoreFrontConfiguration.XMLDocument.Item("SiteProducts").Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            CType(Me.FindControl("btnEMailList"), System.Web.UI.WebControls.LinkButton).Visible = False
            CType(Me.FindControl("imgEMailList"), System.Web.UI.WebControls.Image).Visible = False
        End If
    End Sub

    Public Sub EMailList(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("EMailWishList.aspx")
    End Sub

End Class

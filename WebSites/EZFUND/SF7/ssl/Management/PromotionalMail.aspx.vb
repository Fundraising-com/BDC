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

Imports StoreFront.BusinessRule
Imports StoreFront.Systembase
Imports StoreFront.StoreFront.Email
Imports StoreFront.BusinessRule.Management
Imports System.Text.RegularExpressions

Partial Class PromotionalMail
    Inherits CWebPage
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ddProducts As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddDate As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddSelection As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents ddMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddMonth2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddDate2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddYear2 As System.Web.UI.WebControls.DropDownList
    Protected arrProducts As ArrayList
    Protected WithEvents editemaildynamic1 As editemaildynamic

    Protected strTextBody As String
    Dim sEMailType As String

    Dim objProd As CStoreProducts
    Protected WithEvents hdnEmailServer As System.Web.UI.HtmlControls.HtmlInputHidden
    Private m_bNetscape As Boolean = False
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
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.PromotionalMail) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            'Tee 10/17/2007 bug 160 fix
            ddFormat.Attributes.Add("onChange", "ChangeFormat()")
            'end Tee
            If Request.Browser.Browser.ToUpper().IndexOf("IE") = -1 Then
                m_bNetscape = True
            End If
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            hdnEMailType.Value = "PromoMail"

            ' Text Panel invisible
            lblPanel.Visible = False
            lblPanel.Text = getText(0)

            If IsPostBack Then
                If (m_bNetscape = False) Then
                    If editemaildynamic1.Visible Then
                        Me.TextBody = hdnText.Value
                    Else
                        ' Anything here?
                        Me.TextBody = txtBody.Text
                        lblPanel.Visible = True
                    End If
                Else
                    editemaildynamic1.Visible = False
                    Me.TextBody = txtBody.Text
                    lblPanel.Visible = True

                End If

                ' send
                If hdnAction.Value = "send" Or hdnAction.Value = "send2" Then
                    If ddFormat.SelectedIndex = 0 Then
                        Me.TextBody = txtBody.Text
                        lblPanel.Visible = True
                    Else
                        If (m_bNetscape) Then
                            lblPanel.Visible = True
                            Me.TextBody = txtBody.Text
                        Else
                            lblPanel.Visible = False
                            Me.TextBody = hdnText.Value
                        End If
                    End If

                    Me.Send(hdnAction.Value)


                    hdnAction.Value = ""
                End If

            End If

            If Not IsPostBack Then
                Dim strReturn As String
                strReturn = Request.QueryString("Edit")
                Dim myAdmin As New CAdminGeneralManagement
                txtPromoMailServer.Text = myAdmin.PromoMailServer
                hdnPromoServer.Value = myAdmin.EmailServer
                If strReturn = "TEXT" And strReturn <> "" Then
                    Me.TextBody = Session("PromoEMail")
                    editemaildynamic1.Visible = False
                    lblPanel.Visible = True
                    txtBody.Visible = True
                    txtBody.Text = Me.TextBody
                    ddFormat.SelectedIndex = 0
                ElseIf strReturn = "HTML" And strReturn <> "" Then
                    If (m_bNetscape) Then
                        lblPanel.Visible = True
                        Me.TextBody = Session("PromoEMail")
                        txtBody.Text = hdnText.Value
                        editemaildynamic1.Visible = False
                        txtBody.Visible = True
                    Else
                        lblPanel.Visible = False
                        Me.TextBody = Session("PromoEMail")
                        editemaildynamic1.TextBody = hdnText.Value
                        editemaildynamic1.Visible = True
                        txtBody.Visible = False
                    End If
                    ddFormat.SelectedIndex = 1
                Else
                    editemaildynamic1.Visible = False
                    Me.TextBody = "Dear [CustomerFirstName] [CustomerLastName], " & vbCrLf & vbCrLf & _
                    "You previously purchased [OrderedProducts] from [StoreName]. We thought you would be interested in this new product. " & vbCrLf & vbCrLf & _
                    "Sincerely, [StoreName]" & vbCrLf & _
                    "You are receiving this email because you are subscribed to the mailing list of [StoreName] as : [CustomerEMail]. Go to [UnsubscribeURL] to unsubscribe."
                    lblPanel.Visible = True
                End If
                Session("PromoEMail") = Nothing
                trFromToRow.Visible = False

            End If
            DataBind()

            Me.lnkPromoServerSave.Attributes.Add("onclick", "return ValidatePromoMail()")

        Catch ex As Exception
            Session("DetailError") = "Class PromotionalMail Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Property EMailType() As String
        Get
            Return sEMailType
        End Get
        Set(ByVal Value As String)
            sEMailType = Value
        End Set
    End Property

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

    Public Property TextBody() As String
        Get
            Return strTextBody
        End Get
        Set(ByVal Value As String)
            strTextBody = Value
        End Set
    End Property

    Private Function CheckDate() As Boolean
        Dim strDateRange As String = ddDateRange.SelectedItem.Value

        If strDateRange = "0" Then
            Dim temp As Date
            Try
                temp = CDate(Me.txtFrom.Text)
                temp = CDate(Me.txtTo.Text)
                Return True
            Catch
                ErrorMessage.Text = "Date not in correct format"
                ErrorMessage.Visible = True
                Return False
            End Try
        Else
            Return True
        End If

    End Function

    Private Sub Send(ByVal strValue As String)
        ' check error
        Dim dtStart As Date
        Dim dtEnd As Date
        If Not CheckDate() Then
            Exit Sub
        End If
        If ddDateRange.SelectedItem.Value = "0" Then
            dtStart = CDate(txtFrom.Text)
            dtEnd = CDate(txtTo.Text)
        Else
            dtStart = DateAdd(DateInterval.Day, -(ddDateRange.SelectedItem.Value), Today)
            dtEnd = Today()
        End If

        If txtSubject.Text = "" Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("promomail", "Error", "NoSubject")
            ErrorMessage.Visible = True
        ElseIf txtBody.Text = "" And hdnText.Value = "" Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("promomail", "Error", "NoBody")
            ErrorMessage.Visible = True
        Else
            Dim arProdListFinal As New ArrayList
            Dim arProdList As New ArrayList
            Dim arCustomersList As New ArrayList
            Dim objManagement As New CManagement
            Dim objPMAPID As PromoMailAddrProdID
            Dim bIsBuilt As Boolean = False
            If strValue = "send2" Then
                Dim dr As DataRow
                Dim ds As DataSet
                ds = objManagement.GetSubscribed()
                For Each dr In ds.Tables(0).Rows
                    arCustomersList.Add(New PromoMailAddrProdID(0, dr("uid"), dr("FirstName"), dr("LastName"), dr("Email"), Nothing))
                Next

            ElseIf (IsNothing(Session("ArrChecked")) = False) Then
                arProdListFinal = Session("ArrChecked")
                Dim dsTotal As DataSet = objManagement.GetPromoMailInfo(arProdListFinal, dtStart, dtEnd)
                Dim dr As DataRow
                If dsTotal.Tables(0).Rows.Count > 0 Then
                    Dim intID As Integer = CLng(dsTotal.Tables(0).Rows(0)("CustomerID"))
                    'update #2188
                    objPMAPID = New PromoMailAddrProdID(dsTotal.Tables(0).Rows(0)("uid"), dsTotal.Tables(0).Rows(0)("CustomerID"), dsTotal.Tables(0).Rows(0)("FirstName"), dsTotal.Tables(0).Rows(0)("LastName"), dsTotal.Tables(0).Rows(0)("Email"))
                    arCustomersList.Add(objPMAPID)
                    For Each dr In dsTotal.Tables(0).Rows
                        'update #2188
                        If (intID <> CLng(dr("CustomerID").ToString)) Then
                            objPMAPID = New PromoMailAddrProdID(dr("uid"), dr("CustomerID"), dr("FirstName"), dr("LastName"), dr("Email"))
                        End If
                        If (intID = CLng(dr("CustomerID").ToString)) Then
                            If Not (arProdList.Contains(New CProductManagement(CLng(dr("ProductID"))).Name)) Then
                                arProdList.Add(New CProductManagement(CLng(dr("ProductID"))).Name)
                                objPMAPID.Products = arProdList 'update #2188
                            End If
                        Else
                            objPMAPID.Products = arProdList
                            arCustomersList.Add(objPMAPID)
                            arProdList.Clear()
                            arProdList.Add(New CProductManagement(CLng(dr("ProductID"))).Name)
                        End If
                        intID = CLng(dr("CustomerID").ToString)
                    Next
                End If
            Else
                Dim ds As DataSet
                Dim dr As DataRow
                ds = objManagement.GetPromoMailInfo(dtStart, dtEnd)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim intID As Integer = CLng(ds.Tables(0).Rows(0)("CustomerID"))
                    'update #2188
                    objPMAPID = New PromoMailAddrProdID(ds.Tables(0).Rows(0)("uid"), ds.Tables(0).Rows(0)("CustomerID"), ds.Tables(0).Rows(0)("FirstName"), ds.Tables(0).Rows(0)("LastName"), ds.Tables(0).Rows(0)("Email"))
                    arCustomersList.Add(objPMAPID)
                    For Each dr In ds.Tables(0).Rows
                        'update #2188
                        If (intID <> CLng(dr("CustomerID").ToString)) Then
                            objPMAPID = New PromoMailAddrProdID(dr("uid"), dr("CustomerID"), dr("FirstName"), dr("LastName"), dr("Email"))
                        End If
                        If (intID = CLng(dr("CustomerID").ToString)) Then
                            If Not (arProdList.Contains(New CProductManagement(CLng(dr("ProductID"))).Name)) Then
                                arProdList.Add(New CProductManagement(CLng(dr("ProductID"))).Name)
                                objPMAPID.Products = arProdList 'update #2188
                            End If
                        Else
                            objPMAPID.Products = arProdList
                            arCustomersList.Add(objPMAPID)
                            arProdList.Clear()
                            arProdList.Add(New CProductManagement(CLng(dr("ProductID"))).Name)
                        End If
                        intID = CLng(dr("CustomerID").ToString)
                    Next
                End If
            End If
            Dim objPromoMail As New CPromoMail
            Try
                arProdListFinal.TrimToSize()
                objPromoMail.SendPromoMail(txtSubject, Me.TextBody, ddFormat.SelectedItem.ToString, arCustomersList)
            Catch objError As Exception
                ErrorMessage.Text = objError.Message
                ErrorMessage.Visible = True
                Session("ArrChecked") = Nothing
                Session("ApplyTo") = Nothing
                Session("ReturnPage") = Nothing
            End Try
        End If
        Session("ArrChecked") = Nothing
        Session("ApplyTo") = Nothing
        Session("ReturnPage") = Nothing
    End Sub

    Public Sub FormatChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddFormat.SelectedIndexChanged
        If (ddFormat.SelectedIndex = 0) Then
            txtBody.Visible = True
            lblPanel.Visible = True
            txtBody.Text = Me.TextBody
            editemaildynamic1.Visible = False
        ElseIf (ddFormat.SelectedIndex = 1) Then
            If (m_bNetscape) Then
                txtBody.Visible = True
                lblPanel.Visible = True
                txtBody.Text = Me.TextBody
                editemaildynamic1.Visible = False
            Else
                lblPanel.Visible = False
                txtBody.Visible = False
                editemaildynamic1.TextBody = Me.TextBody
                editemaildynamic1.Visible = True
            End If
        End If
    End Sub

    Public Function getText(ByVal type As Integer) As String
        Dim str As String
        str = "[CustomerFirstName]" & "<br>" & "[CustomerLastName]" & "<br>" & "[OrderedProducts]" & "<br>" & "[StoreName]" & "<br>" & "[CustomerEMail]" & "<br>" & "[StoreURL]" & "<br>" & "[UnsubscribeURL]"
        str = "<b>Dynamic Data Tag Menu</b><p>" & vbCrLf & str
        Return str
    End Function

#Region "Date functions"
    Public Function FillDate() As ArrayList
        Dim arr As New ArrayList
        arr.Add("")
        arr.Add("01")
        arr.Add("02")
        arr.Add("03")
        arr.Add("04")
        arr.Add("05")
        arr.Add("06")
        arr.Add("07")
        arr.Add("08")
        arr.Add("09")
        arr.Add("10")
        arr.Add("11")
        arr.Add("12")
        arr.Add("13")
        arr.Add("14")
        arr.Add("15")
        arr.Add("16")
        arr.Add("17")
        arr.Add("18")
        arr.Add("19")
        arr.Add("20")
        arr.Add("21")
        arr.Add("22")
        arr.Add("23")
        arr.Add("24")
        arr.Add("25")
        arr.Add("26")
        arr.Add("27")
        arr.Add("28")
        arr.Add("29")
        arr.Add("30")
        arr.Add("31")
        Return arr
    End Function

    Public Function FillMonth() As ArrayList
        Dim arr As New ArrayList
        arr.Add("")
        arr.Add("Jan")
        arr.Add("Feb")
        arr.Add("Mar")
        arr.Add("Apr")
        arr.Add("May")
        arr.Add("Jun")
        arr.Add("Jul")
        arr.Add("Aug")
        arr.Add("Sep")
        arr.Add("Oct")
        arr.Add("Nov")
        arr.Add("Dec")
        Return arr
    End Function

    Public Function FillYear() As ArrayList
        Dim arr As New ArrayList
        arr.Add("")
        arr.Add("2000")
        arr.Add("2001")
        arr.Add("2002")
        arr.Add("2003")
        arr.Add("2004")
        arr.Add("2005")
        Return arr
    End Function

    Public Function ReturnIntMonth(ByVal strMonth As String) As Integer
        If (strMonth.Equals("Jan")) Then
            Return 1
        ElseIf (strMonth.Equals("Feb")) Then
            Return 2
        ElseIf (strMonth.Equals("Mar")) Then
            Return 3
        ElseIf (strMonth.Equals("Apr")) Then
            Return 4
        ElseIf (strMonth.Equals("May")) Then
            Return 5
        ElseIf (strMonth.Equals("Jun")) Then
            Return 6
        ElseIf (strMonth.Equals("Jul")) Then
            Return 7
        ElseIf (strMonth.Equals("Aug")) Then
            Return 8
        ElseIf (strMonth.Equals("Sep")) Then
            Return 9
        ElseIf (strMonth.Equals("Oct")) Then
            Return 10
        ElseIf (strMonth.Equals("Nov")) Then
            Return 11
        ElseIf (strMonth.Equals("Dec")) Then
            Return 12
        Else
            Return 0
        End If
    End Function

    Public Function ConvertToDate(ByVal strDate As String) As Date
        If (strDate.ToString <> "") Then
            'Dim arr As String() = strDate.Split("/")
            'Dim dDate As Date = New Date(CType(arr(2), Integer), CType(arr(0), Integer), CType(arr(1), Integer))
            Return StoreFrontConfiguration.ConvertToLocalDate(strDate)
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region "Sub btnSelectProducts_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectProducts.Click"
    Public Sub btnSelectProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectProducts.Click
        If IsNothing(Session("ArrChecked")) Then
            Session("ArrChecked") = New ArrayList
        End If
        Session("ApplyTo") = "1"
        Session("ReturnPage") = "PromotionalMail.aspx?Edit=" & ddFormat.SelectedItem.Value

        If ddFormat.SelectedIndex = 0 Then
            Session("PromoEMail") = txtBody.Text
            Me.TextBody = txtBody.Text
        ElseIf ddFormat.SelectedIndex = 1 Then
            If (m_bNetscape) Then
                Session("PromoEMail") = txtBody.Text
                Me.TextBody = txtBody.Text
            Else
                Session("PromoEMail") = hdnText.Value
                Me.TextBody = hdnText.Value
            End If
        Else
            Session("PromoEMail") = txtBody.Text
            Me.TextBody = txtBody.Text
        End If
        Response.Redirect("multiselect.aspx")
    End Sub
#End Region


    Private Sub ddDateRange_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddDateRange.SelectedIndexChanged
        If ddDateRange.SelectedItem.Value = "0" Then
            trFromToRow.Visible = True
        Else
            trFromToRow.Visible = False
        End If
    End Sub
    Private Sub lnkPromoServerSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkPromoServerSave.Click
        Dim myAdmin As New CAdminGeneralManagement
        myAdmin.PromoMailServer = txtPromoMailServer.Text
        myAdmin.update()
    End Sub
End Class

#Region "PromoMailAddrProdID"
Public Class PromoMailAddrProdID
    Private m_OrderId As Long
    Private m_CustID As Long
    Private m_CustFirstName As String
    Private m_CustLastName As String
    Private m_Products As ArrayList
    Private m_CustEmailAddress As String

#Region "Constructors"
    Public Sub New()

    End Sub

    Public Sub New(ByVal odrID As Long, ByVal cstId As Long, ByVal cstFirstName As String, ByVal cstLastName As String, ByVal email As String)
        OrderID = odrID
        CustID = cstId
        CustFirstName = cstFirstName
        CustLastName = cstLastName
        CustEmail = email
        m_Products = New ArrayList
    End Sub
    Public Sub New(ByVal odrID As Long, ByVal cstId As Long, ByVal cstFirstName As String, ByVal cstLastName As String, ByVal email As String, ByVal arProds As ArrayList)
        OrderID = odrID
        CustID = cstId
        CustFirstName = cstFirstName
        CustLastName = cstLastName
        CustEmail = email
        m_Products = New ArrayList
        If (IsNothing(arProds) = False) Then
            Dim strTemp As String
            For Each strTemp In arProds
                m_Products.Add(strTemp)
            Next
        End If

    End Sub

#End Region

    Public Property Products() As ArrayList
        Get
            Return m_Products
        End Get
        Set(ByVal Value As ArrayList)
            m_Products.Clear()
            Dim strTemp As String
            For Each strTemp In Value
                m_Products.Add(strTemp)
            Next
        End Set

    End Property
    Public Property OrderID() As Long
        Get
            Return m_OrderId
        End Get
        Set(ByVal Value As Long)
            m_OrderId = Value
        End Set
    End Property

    Public Property CustID() As Long
        Get
            Return m_CustID
        End Get
        Set(ByVal Value As Long)
            m_CustID = Value
        End Set
    End Property

    Public Property CustFirstName() As String
        Get
            Return m_CustFirstName
        End Get
        Set(ByVal Value As String)
            m_CustFirstName = Value
        End Set
    End Property

    Public Property CustLastName() As String
        Get
            Return m_CustLastName
        End Get
        Set(ByVal Value As String)
            m_CustLastName = Value
        End Set
    End Property

    Public Property CustEmail() As String
        Get
            Return m_CustEmailAddress
        End Get
        Set(ByVal Value As String)
            m_CustEmailAddress = Value
        End Set
    End Property

End Class
#End Region

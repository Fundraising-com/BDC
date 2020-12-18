Imports system.Math

Partial  Class AdminTabControl
    Inherits CWebControl

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

    Private m_arTabList As New ArrayList()
    Private m_strBorderClass As String
    Private m_strTabItemClass As String

    Event TabClick As EventHandler

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Public Property TabItemClass() As String
        Get
            Return m_strTabItemClass
        End Get
        Set(ByVal Value As String)
            m_strTabItemClass = Value
        End Set
    End Property

    Public Property BorderClass() As String
        Get
            Return m_strBorderClass
        End Get
        Set(ByVal Value As String)
            m_strBorderClass = Value
        End Set
    End Property

    Public Property TabStringArray() As ArrayList
        Get
            Return m_arTabList
        End Get
        Set(ByVal Value As ArrayList)
            m_arTabList = Value

            'Add the Columns
            Dim _img As System.Web.UI.WebControls.Image
            Dim _tr As HtmlTableRow = FindControl("TabRow")
            Dim strList As String
            Dim objLinkButton As LinkButton
            Dim _td As HtmlTableCell
            Dim nWidth As Decimal = Round((100 / m_arTabList.Count)) + 1
            Dim nCounter As Long = 0

            SetDesignElements()

            _td = New HtmlTableCell()
            _td.Width = "1"
            _td.Attributes.Add("Class", "ContentTable")

            _img = New System.Web.UI.WebControls.Image()
            _img.ImageUrl = "images/clear.gif"
            _img.Width = New Unit("1")
            _img.Height = New Unit("1")

            _td.Controls.Add(_img)

            _tr.Cells.Add(_td)

            For Each strList In m_arTabList
                _td = New HtmlTableCell()
                _td.Width = nWidth & "%"
                _td.Attributes.Add("Class", m_strTabItemClass)
                _td.Align = "center"
                _td.NoWrap = True

                objLinkButton = New LinkButton()
                objLinkButton.ID = "lnk" & nCounter

                If (nCounter = 0) Then
                    objLinkButton.Text = "<b>*&nbsp;" & strList & "&nbsp;*</b>"
                Else
                    objLinkButton.Text = strList
                End If
                objLinkButton.CausesValidation = True
                objLinkButton.CommandName = "" & nCounter

                _td.Controls.Add(objLinkButton)

                _tr.Cells.Add(_td)

                _td = New HtmlTableCell()
                _td.Width = "1"
                _td.Attributes.Add("Class", m_strBorderClass)

                _img = New System.Web.UI.WebControls.Image()
                _img.ImageUrl = "images/clear.gif"
                _img.Width = New Unit("1")
                _img.Height = New Unit("1")

                _td.Controls.Add(_img)

                _tr.Cells.Add(_td)

                nCounter = nCounter + 1
            Next

            If (IsPostBack = False) Then
                Session("AdminTabIndex") = 0
                RaiseEvent TabClick("0", EventArgs.Empty)
            End If
        End Set
    End Property

    Protected Overrides Function OnBubbleEvent(ByVal source As Object, ByVal args As System.EventArgs) As Boolean
        Dim lnkBtn As LinkButton = source
        Dim strList As String
        Dim nCounter As Long = 0
        Dim nIndex As Long = CLng(lnkBtn.CommandName)

        Session("AdminTabIndex") = lnkBtn.CommandName
        RaiseEvent TabClick(lnkBtn.CommandName, EventArgs.Empty)

        For Each strList In m_arTabList
            lnkBtn = FindControl("lnk" & nCounter)
            lnkBtn.Text = strList
            nCounter = nCounter + 1
        Next

        lnkBtn = source
        lnkBtn.Text = "<b>*&nbsp;" & lnkBtn.Text & "&nbsp;*</b>"

    End Function

    Private Sub SetDesignElements()
        'Dim _td As HtmlTableCell

        ' Set Border Classes
        '_td = FindControl("Bor1")
        '_td.Attributes.Item("Class") = m_strBorderClass
        '_td = FindControl("Bor2")
        '_td.Attributes.Item("Class") = m_strBorderClass
        '_td = FindControl("Bor3")
        '_td.Attributes.Item("Class") = m_strBorderClass
        '_td = FindControl("Bor4")
        '_td.Attributes.Item("Class") = m_strBorderClass

        '' Set Colspan on seperators
        '_td = FindControl("Sep1")
        '_td.ColSpan = (m_arTabList.Count * 2) - 1
        '_td = FindControl("Sep2")
        '_td.ColSpan = (m_arTabList.Count * 2) - 1

    End Sub

    Public Property SelectedTabIndex() As Long
        Get
            Return Session("AdminTabIndex")
        End Get
        Set(ByVal Value As Long)
            If (Value > m_arTabList.Count - 1) Then
                Exit Property
            ElseIf (Value < 0) Then
                Exit Property
            End If

            Session("AdminTabIndex") = Value
            Dim lnkBtn As LinkButton
            Dim strList As String
            Dim nCounter As Long = 0

            For Each strList In m_arTabList
                lnkBtn = FindControl("lnk" & nCounter)
                If (nCounter = Value) Then
                    lnkBtn.Text = "<b>*&nbsp;" & lnkBtn.Text & "&nbsp;*</b>"
                Else
                    lnkBtn.Text = strList
                End If
                nCounter = nCounter + 1
            Next
        End Set
    End Property
End Class

Imports System.Threading
Imports System.IO
Imports StoreFront.BusinessRule.management

Public MustInherit Class UploadProductFileControl
    Inherits System.Web.UI.UserControl

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
    Public m_sFileName As String
    Private m_sLongFileName As String
    Public m_sFileType As String
    Public m_sDBTFileName As String
    Public m_sDelimiter As String
    Private savePath As String
    Private objImport As CProductImport
    Public arrOutput As ArrayList

    Protected WithEvents DBase As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents SelectFile As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Delimiter As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents FileType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents FileDelimiter As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Continue As System.Web.UI.WebControls.LinkButton
    Protected WithEvents OtherDelimiter As System.Web.UI.WebControls.TextBox
    Protected WithEvents ImportFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents ActivateAll As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ActivateShipping As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ActivateStateTax As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ActivateCountryTax As System.Web.UI.WebControls.CheckBox
    Protected WithEvents tblDataFile As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblImport As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ImportType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmdImport As System.Web.UI.WebControls.LinkButton
    Protected WithEvents DataFields As System.Web.UI.WebControls.Repeater
    Protected WithEvents SFFields As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DBTFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents ImportFileName As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents DBTFileName As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents DelimiterToUse As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ColumnHeadings As System.Web.UI.WebControls.CheckBox
    Protected WithEvents tblResults As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ActivateLocalTax As System.Web.UI.WebControls.CheckBox
    Protected WithEvents LinkAll As System.Web.UI.WebControls.CheckBox
    Protected WithEvents FileTypeToUse As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ResultsHidden As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ResultsShown As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents LBResultsHidden As System.Web.UI.WebControls.LinkButton
    Protected WithEvents LBResultsShown As System.Web.UI.WebControls.LinkButton
    Protected WithEvents LBImagesHidden As System.Web.UI.WebControls.LinkButton
    Protected WithEvents LBImagesShown As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Results As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ImagesHidden As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ImagesShown As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents PResultsHidden As System.Web.UI.WebControls.Panel
    Protected WithEvents PResultsShown As System.Web.UI.WebControls.Panel
    Protected WithEvents hResultsSummary As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hResultsDetails As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hResultsImages As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Images As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tblPleaseWait As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents imgContinue As System.Web.UI.WebControls.Image
    Protected WithEvents imgImport As System.Web.UI.WebControls.Image
    Private m_bRefresh As Boolean
    Private objError As Label


    Private ReadOnly Property InputFile() As String
        Get
            Return m_sLongFileName

        End Get
    End Property


    Public ReadOnly Property Refresh() As Boolean
        Get
            Return m_bRefresh
        End Get
    End Property

    'TODO Need Images
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objError = CType(Me.Parent.FindControl("ErrorMessage"), Label)
        objError.Text = ""
        objError.Visible = False
        m_bRefresh = False
        'Put user code to initialize the page here

        If Application("ThreadImport") Is Nothing Then
            objImport = New CProductImport()
            tblDataFile.Visible = True
            tblImport.Visible = False
            tblPleaseWait.Visible = False
            tblResults.Visible = False
            savePath = Me.MapPath("..\Temp\")
            If (IsPostBack = False) Then
                DBase.Visible = False
                SelectFile.Visible = False
                Delimiter.Visible = False
                Continue.Visible = False
                imgContinue.Visible = False
                CreateFileTypeDD()
                CreateDelimiterDD()
            End If
        Else

            Dim obj As ThreadWork
            obj = Application("ThreadImport")
            If (obj.threadImport.IsAlive = False) Then
                m_bRefresh = False
                arrOutput = obj.Output
                Application("ThreadImport") = Nothing
                tblResults.Visible = True
                tblPleaseWait.Visible = False
                tblImport.Visible = False
                tblDataFile.Visible = False
                hResultsSummary.Value = arrOutput(0)
                hResultsDetails.Value = arrOutput(1)
                hResultsImages.Value = arrOutput(2)
                DataBind()
                ResultsHidden.Visible = True
                ResultsShown.Visible = False
                Results.Visible = False
                If (arrOutput(2) = "") Then
                    ImagesHidden.Visible = False
                    ImagesShown.Visible = False
                    Images.Visible = False
                Else
                    ImagesHidden.Visible = True
                    ImagesShown.Visible = False
                    Images.Visible = False

                End If
            Else
                m_bRefresh = True
                tblDataFile.Visible = False
                tblImport.Visible = False
                tblPleaseWait.Visible = True
                tblResults.Visible = False
            End If
        End If

    End Sub

    Private Sub CreateFileTypeDD()
        FileType.DataSource = objImport.GetFileTypeDT()
        FileType.DataValueField = "ID"
        FileType.DataTextField = "Display"
        FileType.DataBind()
    End Sub

    Private Sub CreateDelimiterDD()
        FileDelimiter.DataSource = objImport.GetDelimiterDT()
        FileDelimiter.DataValueField = "ID"
        FileDelimiter.DataTextField = "Display"
        FileDelimiter.DataBind()
    End Sub

    Private Sub CreateImportTypeDD()
        ImportType.DataSource = objImport.getImportTypeDT
        ImportType.DataValueField = "ID"
        ImportType.DataTextField = "Display"
        ImportType.DataBind()
    End Sub

    Private Sub CreateFields(ByRef item As DropDownList)
        item.DataSource = objImport.getSFFieldsDT()
        item.DataValueField = "ID"
        item.DataTextField = "Display"
        item.DataBind()
    End Sub

    Private Sub BindDataFields()
        DataFields.DataSource = objImport.getImportFieldsDT(m_sFileName, m_sFileType, m_sDelimiter, savePath, 0)
        DataFields.DataBind()
    End Sub


    Private Sub Continue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Continue.Click
        Dim objError As Label
        Dim strExt As String
        Dim bError As Boolean
        bError = False
        m_sFileType = FileType.SelectedItem.Value
        m_sDelimiter = FileDelimiter.SelectedItem.Value
        If m_sFileType = "QB then" Then
            m_sDelimiter = "Tab"
        End If
        If (m_sDelimiter = "Other") Then
            m_sDelimiter = OtherDelimiter.Text
        End If

        objError = CType(Me.Parent.FindControl("ErrorMessage"), Label)

        If (Not Directory.Exists(savePath)) Then
            Directory.CreateDirectory(savePath)
        End If

        If Not (ImportFile.PostedFile Is Nothing) Then
            If (m_sFileType = "Text" And m_sDelimiter = "") Then
                objError.Text = "Please select a file delimiter."
                objError.Visible = True
                bError = True
            Else
                Try
                    Dim postedFile = ImportFile.PostedFile
                    Dim contentType As String = postedFile.ContentType
                    Dim contentLength As Integer = postedFile.ContentLength
                    m_sFileName = Path.GetFileName(postedFile.FileName)
                    m_sLongFileName = postedFile.filename
                    If (m_sFileName = "" Or m_sFileName.Length < 5) Then
                        objError.Text = "Please select a file to import."
                        objError.Visible = True
                        bError = True
                    Else
                        strExt = Right(m_sFileName, 4).ToLower
                        If (strExt = ".txt" Or strExt = ".prn" Or strExt = ".tab" Or strExt = ".csv" Or strExt = ".dbf" Or strExt = ".iif") Then
                            postedFile.SaveAs(savePath & m_sFileName)
                        Else
                            objError.Text = "Product file type is invalid.  Valid file types are (.txt, .prn, .tab, .csv, .dbt, .dbf, .iif)"
                            objError.Visible = True
                            bError = True
                        End If
                    End If
                Catch exc As Exception
                    objError.Text = "Failed uploading file."
                    objError.Visible = True
                    bError = True
                End Try
            End If
        End If

        If (bError = False And m_sFileType = "DBase") Then
            If Not (DBTFile.PostedFile Is Nothing) Then
                Try
                    Dim postedFile = DBTFile.PostedFile
                    Dim contentType As String = postedFile.ContentType
                    Dim contentLength As Integer = postedFile.ContentLength


                    m_sDBTFileName = Path.GetFileName(postedFile.FileName)
                    If (m_sDBTFileName <> "" And m_sDBTFileName.Length > 4) Then
                        strExt = Right(m_sDBTFileName, 4)
                        If (strExt = ".dbt") Then
                            postedFile.SaveAs(savePath & m_sDBTFileName)
                        Else
                            objError.Text = "File must be of type .dbt"
                            objError.Visible = True
                            bError = True
                        End If
                    End If
                Catch exc As Exception
                    objError.Text = "Failed uploading .dbt file."
                    objError.Visible = True
                    bError = True
                End Try
            End If
        End If

        If (bError = False) Then
            objImport = New CProductImport()
            CreateImportTypeDD()
            BindDataFields()
            tblDataFile.Visible = False
            tblPleaseWait.Visible = False
            tblImport.Visible = True
            tblResults.Visible = False
            ImportFileName.Value = m_sFileName
            DBTFileName.Value = m_sDBTFileName
            DelimiterToUse.Value = m_sDelimiter
            FileTypeToUse.Value = m_sFileType
            DataBind()
            Dim item As RepeaterItem
            For Each item In DataFields.Items
                Dim Fields As DropDownList = CType(item.FindControl("SFFields"), DropDownList)
                CreateFields(Fields)
            Next
        End If
    End Sub

    Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImport.Click
        Dim arrSelectedFields(DataFields.Items.Count) As String
        'fill array with field selections
        Dim item As RepeaterItem
        Dim bError As Boolean
        Dim x As Integer
        '        Dim objError As Label
        Dim y As Integer
        bError = False
        '       objError = CType(Me.Parent.FindControl("ErrorMessage"), Label)
        x = 0
        For Each item In DataFields.Items
            Dim Fields As DropDownList = CType(item.FindControl("SFFields"), DropDownList)
            For y = 0 To x - 1
                If arrSelectedFields(y) = Fields.SelectedItem.Value And Fields.SelectedItem.Value <> "skip" Then
                    objError.Text = "Each StroreFront field must only be selected once."
                    objError.Visible = True
                    tblDataFile.Visible = False
                    tblPleaseWait.Visible = False
                    tblImport.Visible = True
                    tblResults.Visible = False
                    Exit Sub
                End If
            Next
            arrSelectedFields(x) = Fields.SelectedItem.Value
            x = x + 1
        Next
        Dim objThread As New ThreadWork()
        objThread.ActivateAll = ActivateAll.Checked
        objThread.ActivateCountryTax = ActivateCountryTax.Checked()
        objThread.ActivateLocalTax = ActivateLocalTax.Checked()
        objThread.ActivateShipping = ActivateShipping.Checked()
        objThread.ActivateStateTax = ActivateStateTax.Checked()
        objThread.ColumnHeadings = ColumnHeadings.Checked()
        objThread.DelimiterToUse = DelimiterToUse.Value
        objThread.FieldArr = arrSelectedFields
        objThread.FileName = ImportFileName.Value
        objThread.FileTypeToUse = FileTypeToUse.Value
        objThread.ImportType = CInt(ImportType.SelectedItem.Value)
        objThread.LinkAll = LinkAll.Checked()
        objThread.savePath = savePath
        Dim otter As New ThreadStart(AddressOf objThread.DoWork)
        objThread.threadImport.Name = "StoreFront 6 Product Import"
        objThread.threadImport.Start()
        'Dim oThread As New Thread(otter)
        'oThread.Name = "StoreFront 6 Product Import"
        'oThread.Start()
        Application("ThreadImport") = objThread
        m_bRefresh = True
        tblDataFile.Visible = False
        tblImport.Visible = False
        tblPleaseWait.Visible = True
        tblResults.Visible = False
    End Sub

    Private Sub LBResultsShown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LBResultsShown.Click
        tblDataFile.Visible = False
        tblImport.Visible = False
        tblPleaseWait.Visible = False
        tblResults.Visible = True

        arrOutput = New ArrayList()

        arrOutput.Add(hResultsSummary.Value)
        arrOutput.Add(hResultsDetails.Value)
        arrOutput.Add(hResultsImages.Value)
        ResultsHidden.Visible = True
        ResultsShown.Visible = False
        Results.Visible = False

    End Sub


    Private Sub LBResultsHidden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LBResultsHidden.Click
        tblDataFile.Visible = False
        tblImport.Visible = False
        tblPleaseWait.Visible = False
        tblResults.Visible = True
        arrOutput = New ArrayList()

        arrOutput.Add(hResultsSummary.Value)
        arrOutput.Add(hResultsDetails.Value)
        arrOutput.Add(hResultsImages.Value)

        ResultsHidden.Visible = False
        ResultsShown.Visible = True
        Results.Visible = True
    End Sub

    Private Sub LBImagesShown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LBImagesShown.Click
        tblDataFile.Visible = False
        tblImport.Visible = False
        tblPleaseWait.Visible = False
        tblResults.Visible = True
        arrOutput = New ArrayList()

        arrOutput.Add(hResultsSummary.Value)
        arrOutput.Add(hResultsDetails.Value)
        arrOutput.Add(hResultsImages.Value)

        ImagesHidden.Visible = True
        ImagesShown.Visible = False
        Images.Visible = False

    End Sub


    Private Sub LBImagesHidden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LBImagesHidden.Click
        tblDataFile.Visible = False
        tblImport.Visible = False
        tblPleaseWait.Visible = False
        tblResults.Visible = True
        arrOutput = New ArrayList()

        arrOutput.Add(hResultsSummary.Value)
        arrOutput.Add(hResultsDetails.Value)
        arrOutput.Add(hResultsImages.Value)

        ImagesHidden.Visible = False
        ImagesShown.Visible = True
        Images.Visible = True
    End Sub


    Class ThreadWork
        Private objthreadImport As Thread
        Private arrOutput As ArrayList
        Private objImport As New CProductImport()
        Private strFileName As String
        Private strFileTypeToUse As String
        Private strDelimiterToUse As String
        Private strsavePath As String
        Private iImportType As Integer
        Private strFieldArr() As String
        Private bColumnHeadings As Boolean
        Private bActivateAll As Boolean
        Private bActivateShipping As Boolean
        Private bActivateStateTax As Boolean
        Private bActivateCountryTax As Boolean
        Private bActivateLocalTax As Boolean
        Private bLinkAll As Boolean

        Public Sub New()
            objthreadImport = New Thread(AddressOf Me.DoWork)

        End Sub

        Public Property threadImport() As Thread
            Get
                Return objthreadImport
            End Get
            Set(ByVal Value As Thread)
                objthreadImport = Value
            End Set
        End Property


        Public Property ColumnHeadings() As Boolean
            Get
                Return bColumnHeadings
            End Get
            Set(ByVal Value As Boolean)
                bColumnHeadings = Value
            End Set
        End Property

        Public Property ActivateAll() As Boolean
            Get
                Return bActivateAll
            End Get
            Set(ByVal Value As Boolean)
                bActivateAll = Value
            End Set
        End Property

        Public Property ActivateShipping() As Boolean
            Get
                Return bActivateShipping
            End Get
            Set(ByVal Value As Boolean)
                bActivateShipping = Value
            End Set
        End Property

        Public Property ActivateStateTax() As Boolean
            Get
                Return bActivateStateTax
            End Get
            Set(ByVal Value As Boolean)
                bActivateStateTax = Value
            End Set
        End Property

        Public Property ActivateCountryTax() As Boolean
            Get
                Return bActivateCountryTax
            End Get
            Set(ByVal Value As Boolean)
                bActivateCountryTax = Value
            End Set
        End Property

        Public Property ActivateLocalTax() As Boolean
            Get
                Return bActivateLocalTax
            End Get
            Set(ByVal Value As Boolean)
                bActivateLocalTax = Value
            End Set
        End Property

        Public Property LinkAll() As Boolean
            Get
                Return bLinkAll
            End Get
            Set(ByVal Value As Boolean)
                bLinkAll = Value
            End Set
        End Property

        Public Property FieldArr() As String()
            Get
                Return strFieldArr
            End Get
            Set(ByVal Value As String())
                strFieldArr = Value
            End Set
        End Property

        Public Property ImportType() As Integer
            Get
                Return iImportType
            End Get
            Set(ByVal Value As Integer)
                iImportType = Value
            End Set
        End Property

        Public Property FileTypeToUse() As String
            Get
                Return strFileTypeToUse
            End Get
            Set(ByVal Value As String)
                strFileTypeToUse = Value
            End Set
        End Property

        Public Property DelimiterToUse() As String
            Get
                Return strDelimiterToUse
            End Get
            Set(ByVal Value As String)
                strDelimiterToUse = Value
            End Set
        End Property

        Public Property savePath() As String
            Get
                Return strsavePath
            End Get
            Set(ByVal Value As String)
                strsavePath = Value
            End Set
        End Property

        Public Property FileName() As String
            Get
                Return strFileName
            End Get
            Set(ByVal Value As String)
                strFileName = Value
            End Set
        End Property

        Public Property Output() As ArrayList
            Get
                Return arrOutput
            End Get
            Set(ByVal Value As ArrayList)
                arrOutput = Value
            End Set
        End Property

        Sub DoWork()
            arrOutput = objImport.import(strFileName, FileTypeToUse, DelimiterToUse, savePath, ImportType, strFieldArr, ColumnHeadings, ActivateAll, ActivateShipping, ActivateStateTax, ActivateCountryTax, ActivateLocalTax, LinkAll)
        End Sub
    End Class

    Private Sub FileType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileType.SelectedIndexChanged
        m_sFileType = FileType.SelectedItem.Value
        m_sDelimiter = FileDelimiter.SelectedItem.Value
        If (m_sDelimiter = "Other") Then
            m_sDelimiter = OtherDelimiter.Text
        End If

        If (m_sFileType = "NONE") Then
            SelectFile.Visible = False
            DBase.Visible = False
            Delimiter.Visible = False
            Continue.Visible = False
            imgContinue.Visible = False
        ElseIf (m_sFileType = "DBase") Then
            SelectFile.Visible = True
            DBase.Visible = True
            Delimiter.Visible = False
            Continue.Visible = True
            imgContinue.Visible = True
        ElseIf (m_sFileType = "Text") Then
            SelectFile.Visible = True
            DBase.Visible = False
            Delimiter.Visible = True
            Continue.Visible = True
            imgContinue.Visible = True
        ElseIf (m_sFileType = "QB") Then
            SelectFile.Visible = True
            DBase.Visible = False
            Delimiter.Visible = False
            Continue.Visible = True
            imgContinue.Visible = True
        End If
    End Sub
End Class

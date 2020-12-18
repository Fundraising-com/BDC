Imports StoreFront.BusinessRule.WebRequest
Imports StoreFront.BusinessRule.WebRequestType
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase
Imports System.IO
Imports System.Threading

Partial  Class UploadControl
    Inherits CWebControl
    Protected WithEvents saveSpan As System.Web.UI.WebControls.Label
    Private bReverse As Boolean = False
    Private _CurrentText As String = ""
    Private _DisplayText As String = ""
    Public mLabelText As String = ""
    Private uploadFolder As String = ""
    '  Public submitText As String = "Save "
    Enum m_FileType
        DownLoad = 0
        Image = 1
        Pem = 2
    End Enum

    Public FileType As m_FileType = m_FileType.Image


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
        If Not IsPostBack Then
            lblCurrent.Text = FileText

            lblLabel.Text = LabelDisplay
        End If
        status.Text = ""

        ' #1438 MS Start
        'If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value <> "SE") Then
        If bReverse Then
            pnlfile.Visible = True
            pnlCurrent.Visible = False
            Me.CmdCancel.Visible = False
            Me.imgCancel.Visible = False
        Else
            Me.pnlfile.Visible = False
            Me.pnlCurrent.Visible = True
            cmdNew.Visible = True
            imgNew.Visible = True
        End If
        'Else
        'Me.pnlfile.Visible = False
        'pnlCurrent.Visible = False
        'Me.CmdCancel.Visible = False
        'Me.imgCancel.Visible = False
        'End If
        ' #1438 MS End

    End Sub

    Public Property FileText() As String
        Get
          
            _CurrentText = lblCurrent.Text
            Return _CurrentText
        End Get

        Set(ByVal Value As String)
            _CurrentText = Value
            Session("_CurrentText") = Value
            lblCurrent.Text = Value
        End Set
    End Property

    Public Property LabelDisplay() As String
        Get
            Return mLabelText
        End Get
        Set(ByVal Value As String)
            mLabelText = Value

            If mLabelText = "" Then
                lblLabel.Visible = False
                lblspcr.Visible = False
            Else
                lblLabel.Text = Value
                lblspcr.Visible = True
                lblLabel.Visible = True
            End If

        End Set
    End Property

    Public Property Reverse() As Boolean
        Get
            Return bReverse
        End Get
        Set(ByVal Value As Boolean)
            bReverse = Value
        End Set
    End Property
    Public Sub uploadBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadBtn.Click
        Me.pnlfile.Visible = True
        cmdNew.Visible = False
        imgNew.Visible = False
        If Not IsNothing(filename.PostedFile) Then
            'If filename.PostedFile.FileName.IndexOf("http://") = -1 Then
            Dim sPath As String = uploadFolder
            Dim sFileInfo As String = ""
            Try
                Dim sFile As String
                sFile = Trim(GetFileName(filename.PostedFile.FileName))
                If sFile = "" Then
                    Me.pnlfile.Visible = False
                    Me.pnlCurrent.Visible = True
                    FileText = sFile
                    lblCurrent.Text = FileText
                    Exit Sub
                End If
                If ISOk(sFile) Then

                    'If FolderExist() Then
                    '    filename.PostedFile.SaveAs(uploadFolder & "\" & sFile)
                    'Else
                    '    SendWebFile(sFile)
                    'End If
                    Dim fileExtension As String
                    fileExtension = sFile.Substring(sFile.LastIndexOf("."))

                    If (FileType = m_FileType.Pem) Then
                        If (fileExtension.ToLower() = ".exe" Or _
                            fileExtension.ToLower() = ".vbs") Then
                            Return
                        End If

                        uploadFolder = Request.PhysicalPath
                        uploadFolder = uploadFolder.Substring(0, uploadFolder.ToLower().IndexOf("management"))

                        If (uploadFolder.EndsWith("\") = False) Then
                            uploadFolder = uploadFolder & "\"
                        End If

                        If (Directory.Exists(uploadFolder & "_Proc") = False) Then
                            Directory.CreateDirectory(uploadFolder & "_Proc")
                        End If

                        filename.PostedFile.SaveAs(uploadFolder & "_Proc\" & sFile)
                    Else
                        SendWebFile(sFile)
                    End If


                    status.Text = "File uploaded successfully." & sFileInfo
                    If bReverse Then
                        pnlfile.Visible = True
                        pnlCurrent.Visible = False
                        Me.CmdCancel.Visible = False
                        Me.imgCancel.Visible = False
                    Else
                        Me.pnlfile.Visible = False
                        cmdNew.Visible = True
                        imgNew.Visible = True
                    End If
                    FileText = sFile ' 
                    lblCurrent.Text = FileText
                Else
                    status.Text = "Invalid File"
                    pnlCurrent.Visible = False
                End If

            Catch ERR As SystemException
                status.Text = "Error saving file" _
                 & sFileInfo & "<br>" & ERR.Message
            End Try
            'End If
        Else
        	Me.pnlfile.Visible = False
        	cmdNew.Visible = True
        	imgNew.Visible = True
        	FileText = filename.Value
        	lblCurrent.Text = FileText
        End If
    End Sub

#Region "Private Function GetFileName(ByVal strPath As String) As String"

    Private Function GetFileName(ByVal strPath As String) As String
        Dim ar() As String
        strPath = Trim(strPath)

        If Right(strPath, 1) = "\" Then
            strPath = Left(strPath, Len(strPath) - 1)
        End If
        ar = Split(strPath, "\")
        If ar(UBound(ar)) <> "" Then
            Return ar(UBound(ar))
        Else
            Return ""
        End If
    End Function

#End Region

    Private Sub SendWebFile(ByVal sFile As String)
        Dim aBuffer(filename.PostedFile.InputStream.Length) As Byte
        ' Read the file into the byte array.
        filename.PostedFile.InputStream.Read(aBuffer, 0, filename.PostedFile.InputStream.Length)
        'update #1567, 1912
        'Dim mChar(filename.PostedFile.InputStream.Length) As Char
        'Dim Loop1 As Integer
        ' Copy the byte array into a Char Array.
        'For Loop1 = 0 To filename.PostedFile.InputStream.Length - 1
        'mChar(Loop1) = Convert.ToChar(aBuffer(Loop1))
        'Next Loop1
        'Dim s As New String(mChar)

        Dim s As String = System.Convert.ToBase64String(aBuffer)
        Dim objWebRequest As New CWebRequest
        objWebRequest.Type = WEBPOST
        objWebRequest.AddNameValuePair("Bytes", CStr(Me.FileType))

        objWebRequest.AddNameValuePair("FileBytes", s)

        objWebRequest.AddNameValuePair("FileName", sFile)
        '2641
        Dim adminID As New CAdminGeneralManagement
        objWebRequest.AddNameValuePair("AdminGuid", adminID.AdminGuid)
        objWebRequest.URI = StoreFrontConfiguration.SiteURL & "sfupload.aspx"

        Dim objThread As New WebRequestThread()
        objThread.threadImport.Name = "SF6 GetClientConnection"
        objThread.WebRequest = objWebRequest
        '    objWebRequest.SendRequest()

        Application("objThread") = objThread


        objThread.threadImport.Start()
        ' Dim i As Integer = 1
        'While objThread.threadImport.IsAlive
        ' Thead.Sleep(500)
        'status.Text = i
        'i = i + 1
        'End While
        'status.Text = objThread.RequestResponse
        FileText = sFile





    End Sub

#Region "Private Function ISOk(ByVal sFile As String) As Boolean"

    Private Function ISOk(ByVal sFile As String) As Boolean
        sFile = Trim(sFile)
        Dim fileExtension As String
        fileExtension = sFile.Substring(sFile.LastIndexOf("."))

        If fileExtension = "" Then
            Return False
        Else
            Select Case FileType
                Case m_FileType.DownLoad
                    Return True
                Case m_FileType.Image
                    Return Image_Ok(fileExtension)
                Case m_FileType.Pem
                    If LCase(fileExtension) <> ".exe" Then
                        If LCase(fileExtension) <> ".vbs" Then
                            Return True
                        End If
                    End If
            End Select
        End If
        Return False
    End Function

#End Region


    Private Function FolderExist() As Boolean

        uploadFolder = Request.PhysicalApplicationPath
        Dim SSLIsApp As Boolean = False
        If InStr(uploadFolder.ToLower, "ssl") > 0 Then
            SSLIsApp = True
            If FileType <> m_FileType.Pem Then
                Return False
            End If
        End If

        Select Case FileType
            Case m_FileType.DownLoad
                uploadFolder = uploadFolder & "Download"
            Case m_FileType.Image
                uploadFolder = uploadFolder & "Images"
            Case m_FileType.Pem
                If SSLIsApp = True Then
                    uploadFolder = uploadFolder & "_Proc"
                Else
                    uploadFolder = uploadFolder & "ssl\_Proc"
                End If
                uploadFolder = uploadFolder & "ssl\_Proc"
                If Directory.Exists(uploadFolder) = False Then
                    Try
                        Directory.CreateDirectory(uploadFolder)
                    Catch

                    End Try

                End If
        End Select


        Return Directory.Exists(uploadFolder)




    End Function



#Region "Private Function Image_Ok(ByVal sFileName$) As Boolean"

    Private Function Image_Ok(ByVal sFileName$) As Boolean
        'Dim ext$
        If Trim(sFileName) = "" Then
            Return False
        End If
        sFileName = LCase(sFileName)
        If InStr(1, sFileName, ".bmp", vbTextCompare) > 0 Or _
        InStr(1, sFileName, ".gif", vbTextCompare) > 0 Or _
        InStr(1, sFileName, ".jpeg", vbTextCompare) > 0 Or _
        InStr(1, sFileName, ".jpg", vbTextCompare) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles cmdNew.Click"

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Me.pnlCurrent.Visible = False
        cmdNew.Visible = False
        Me.pnlfile.Visible = True
    End Sub

#End Region


End Class

Imports System.IO
Imports System.Collections.Specialized
Imports StoreFront.BusinessRule.Management
Imports System.Threading
Imports StoreFront.SystemBase
Imports System.Net

Partial Class SFExpressUploadControl
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Private sFile As String
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Public Event ImageUpload As EventHandler

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Public Property UploadPath() As String
        Get
            Dim strUploadPath As String = StoreFrontConfiguration.ThemesPath & "images/"
            If Not ViewState("UploadPath") Is Nothing Then
                strUploadPath = ViewState("UploadPath")
            End If
            Return strUploadPath
        End Get
        Set(ByVal Value As String)
            ViewState("UploadPath") = Value
        End Set
    End Property

    Public Sub uploadBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadBtn.Click
        If Not IsNothing(filename.PostedFile) Then
            Try
                sFile = Trim(GetFileName(filename.PostedFile.FileName))
                If sFile = "" Then
                    status.Text = "Please Select a file"
                    status.Visible = True
                    Exit Sub
                End If
                If filename.PostedFile.ContentType.IndexOf("image") >= 0 Then
                    'Tee 2/5/2008 bug 1096 fix
                    Dim f As New IO.FileInfo(MapPath(UploadPath & sFile))
                    If f.Exists Then
                        File.Delete(MapPath(UploadPath & sFile))
                    End If
                    'end Tee
                    DoWork()

                    Dim strCommandName As String = String.Format("{0}{1}", Me.UploadPath, sFile)
                    strCommandName = strCommandName.Replace(StoreFrontConfiguration.ThemesPath & "images/", String.Empty)
                    'strCommandName = strCommandName.Replace("\", "/")

                    Me.uploadBtn.CommandName = strCommandName
                    Me.status.Text = ""
                    Me.status.Visible = False
                    Me.pnlfile.Visible = False
                    RaiseEvent ImageUpload(sFile, e)
                Else
                    status.Text = "Invalid File"
                End If
            Catch ERR As SystemException
                status.Text = "Error saving file<br>" & ERR.Message
                Me.status.Visible = True
            End Try
        End If
    End Sub

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

    Private Sub DoWork()
        Dim objClient As New WebClient
        objClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
        Dim responseArray As Byte()
        Dim aBuffer(filename.PostedFile.InputStream.Length) As Byte
        ' Read the file into the byte array.
        filename.PostedFile.InputStream.Read(aBuffer, 0, filename.PostedFile.InputStream.Length)
        Dim s As String = System.Convert.ToBase64String(aBuffer)
        Dim objWebRequest As New NameValueCollection
        objWebRequest.Add("Type", 1)
        objWebRequest.Add("FileBytes", s)
        objWebRequest.Add("FileName", String.Format("{0}{1}", Me.UploadPath, sFile))
        Dim adminID As New CAdminGeneralManagement
        objWebRequest.Add("AdminGuid", adminID.AdminGuid)
        'objWebRequest.Add("InstallSsl", "true")
        If StoreFrontConfiguration.SiteURL.EndsWith("/") Then
            responseArray = objClient.UploadValues(StoreFrontConfiguration.SiteURL & "SFExpressUpload.aspx", "POST", objWebRequest)
        Else
            responseArray = objClient.UploadValues(StoreFrontConfiguration.SiteURL & "/SFExpressUpload.aspx", "POST", objWebRequest)
        End If
        If StoreFrontConfiguration.SSLPath.EndsWith("/") Then
            responseArray = objClient.UploadValues(StoreFrontConfiguration.SSLPath & "SFExpressSslUpload.aspx", "POST", objWebRequest)
        Else
            responseArray = objClient.UploadValues(StoreFrontConfiguration.SSLPath & "/SFExpressSslUpload.aspx", "POST", objWebRequest)
        End If
    End Sub

    Private Sub CmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancel.Click
        Me.uploadBtn.CommandName = String.Empty
        Me.pnlfile.Visible = False
        Me.status.Text = ""
        Me.status.Visible = False
        RaiseEvent ImageUpload(String.Empty, e)
    End Sub

    Public Sub PanelVisible()
        Me.pnlfile.Visible = True
    End Sub
End Class

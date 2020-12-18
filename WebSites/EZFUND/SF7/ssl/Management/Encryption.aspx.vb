Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFrontSecurity

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

Partial Class Encryption
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Private Const OPTIONKEY As String = "option"
    Private Const OLDPRIVATEKEY As String = "downloadedfile"
    Private Const NEWPRIVATEKEY As String = "privatekey"
    Private Const NEWPUBLICKEY As String = "publickey"


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
        If MyBase.RestrictedPages(Tasks.Encryption) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            rbReplaceKey.Enabled = StoreFrontConfiguration.ConvertedFrom3DES
            pnlUploadKey.Visible = StoreFrontConfiguration.ConvertedFrom3DES
            ErrorMessage.Visible = False
        Catch ex As Exception
            Session("DetailError") = "Class PaymentMethods Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub lnkSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSubmit.Click
        If Not ValidateAndSetOptions() Then Return
        EnableDownloadKeyPanel()
        pnlKeyOptions.Visible = False
    End Sub
    Private Function ValidateAndSetOptions() As Boolean

        If Not chkconfirm.Checked Then
            ErrorMessage.Visible = True
            ErrorMessage.Text = " Please Check the confirmation box to continue. "
            Return False
        End If

        If rbNewKey.Checked Then
            viewstate("option") = "new"
            SetNewKeys()
            Return True
        End If

        If rbReplaceKey.Enabled And rbReplaceKey.Checked Then
            If Not filename.PostedFile Is Nothing AndAlso filename.PostedFile.ContentLength > 0 Then
                viewstate(OPTIONKEY) = "replace"
                Dim myfBuffer(filename.PostedFile.ContentLength) As Byte
                filename.PostedFile.InputStream.Read(myfBuffer, 0, filename.PostedFile.ContentLength)
                Session(Encryption.OLDPRIVATEKEY) = System.Text.UTF8Encoding.UTF8.GetString(myfBuffer)
                SetNewKeys()
                Return True
            Else
                ErrorMessage.Visible = True
                ErrorMessage.Text = " Previous Key is required to Replace the Existing Key"
                Return False
            End If
        End If


            ErrorMessage.Visible = True
            ErrorMessage.Text = " Please select one option !"
        Return False
    End Function
    Private Sub lnkdownloadkey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkdownloadkey.Click
        Try
            Dim myPrivateKey As String = Session(Encryption.NEWPRIVATEKEY)
            Response.AppendHeader("Content-Disposition", "attachment; filename=store.key")
            Response.ContentType = "text"
            Response.Write(myPrivateKey)
            Response.Flush()
            Response.End()
        Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
            ErrorMessage.Visible = True
            ErrorMessage.Text = " Failed To Generate New Key : " & ex.Message
        End Try
    End Sub
    Private Sub RefreshConfiguration()
        Try
            'Dim mUser As String = Request.ServerVariables("AUTH_USER")
            'Dim mPAss As String = Request.ServerVariables("AUTH_PASSWORD")
            'Dim myRequest As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(StoreFrontConfiguration.SiteURL & "ReloadXml.aspx?ssl=1")
            'myRequest.Credentials = New System.Net.NetworkCredential(mUser, mPAss)

            '    Dim myResponse As System.Net.HttpWebResponse = myRequest.GetResponse
            '    Dim aBuffer() As Byte
            '    aBuffer = New Byte(myResponse.ContentLength - 1) {}
            '    myResponse.GetResponseStream.Read(aBuffer, 0, myResponse.ContentLength)
            Dim objWeb As New BusinessRule.WebRequest.CWebRequest
            objWeb.Type = 1
            objWeb.URI = New String(StoreFrontConfiguration.SSLPath() & "ReloadXML.aspx?SSL=0")
            objWeb.SendRequest2(Request.ServerVariables("AUTH_USER"), Request.ServerVariables("AUTH_PASSWORD"))
            Catch ex As Exception
                ErrorMessage.Visible = True
                ErrorMessage.Text = " Failed to Reload XML. " & ex.Message
            End Try
    End Sub
    Private Sub SetNewKeys()
        Dim myCrypto As New StoreFrontRSACrypto
        myCrypto.GenerateKeyPair()
        Session(Encryption.NEWPRIVATEKEY) = myCrypto.RSAPrivateKey
        Session(Encryption.NEWPUBLICKEY) = myCrypto.RSAPublicKey
    End Sub
    Private Sub lnkConfirmKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConfirmKey.Click
        Dim mykeyManage As New SecurityKeyManagement
        Dim myPrivateKey As String = Session(Encryption.NEWPRIVATEKEY)
        Dim myPublickey As String = Session(Encryption.NEWPUBLICKEY)
        Dim myOldPrivatekey As String = Session(Encryption.OLDPRIVATEKEY)

        If viewstate(OPTIONKEY) = "replace" Then
            Try
                myPrivateKey = mykeyManage.ReplaceKeyPair(Session(Encryption.OLDPRIVATEKEY), myPublickey)
            Catch ex As Exception
                SetReplaceKeyError(ex)
                Return
            End Try
        ElseIf viewstate(OPTIONKEY) = "new" Then
            Try
                myPrivateKey = mykeyManage.SetNewPrivateKey(myPublickey)
            Catch ex As Exception
                SetNewKeyError(ex)
                Return
            End Try
        End If
        'Tee 2/26/2008 force reloadxml
        Dim reload As New StoreFront.ReloadXML.ThreadWork
        reload.DoWork()
        'end Tee
        RefreshConfiguration()
        SetOptionsPanel()
        If Not ErrorMessage.Visible Then
            ErrorMessage.Visible = True
            ErrorMessage.Text = "Successfully Converted Data"
        End If


        'Session.Remove(Me.NEWPRIVATEKEY)
        'Session.Remove(Me.NEWPUBLICKEY)
        'Session.Remove(Me.OLDPRIVATEKEY)
        'viewstate.Remove(Me.OPTIONKEY)
        ''this would redirect and remove the old viewstate
        'Response.Redirect("Encryption.aspx?", True)
    End Sub
    Private Sub EnableDownloadKeyPanel()
        pnlDownloadKey.Visible = True
        txtKey.Text = Session(Encryption.NEWPRIVATEKEY)
    End Sub
    Private Sub SetOptionsPanel()
        pnlDownloadKey.Visible = False
        pnlKeyOptions.Visible = True
        rbReplaceKey.Enabled = StoreFrontConfiguration.ConvertedFrom3DES
        pnlUploadKey.Visible = StoreFrontConfiguration.ConvertedFrom3DES
        txtKey.Text = String.Empty
        Session.Remove(Encryption.NEWPRIVATEKEY)
        Session.Remove(Encryption.NEWPUBLICKEY)
        Session.Remove(Encryption.OLDPRIVATEKEY)
        ViewState.Remove(Encryption.OPTIONKEY)
    End Sub
    Private Sub SetNewKeyError(ByVal ex As Exception)
        SetOptionsPanel()
        ErrorMessage.Visible = True
        ErrorMessage.Text = "Failed to Convert data for new Key " & ex.Message
    End Sub
    Private Sub SetReplaceKeyError(ByVal ex As Exception)
        SetOptionsPanel()
        ErrorMessage.Visible = True
        ErrorMessage.Text = "Failed to Convert data for Replaced Key " & ex.Message
    End Sub
    Private Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
        SetOptionsPanel()
    End Sub
End Class

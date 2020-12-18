'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System
Imports System.Data
Imports System.Text
Imports System.Collections
Imports System.Collections.Specialized
Imports System.IO
Imports System.CodeDom.Compiler
Imports System.Reflection
Imports Microsoft.VisualBasic

Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

#Region "Class VBCError"
Public Class VBCError
    Private m_str As String
    Public Property ErrorText() As String
        Get
            Return m_str
        End Get
        Set(ByVal Value As String)
            m_str = Value
        End Set
    End Property
End Class
#End Region

Partial Class CompileSite
    Inherits System.Web.UI.Page


    Private m_arFileList As New ArrayList()
    Private o As CompilerResults
    Private dt As New DataTable()
    Private m_strRoot As String

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
        CompileWeb()

        If (Request.Form("XMLView") <> "") Then
            DisplayXML()
        Else
            DisplayHTML()
        End If

    End Sub

#Region "Sub DisplayXML()"
    Private Sub DisplayXML()
        Dim ms As New MemoryStream()
        Dim _response As String

        Dim ds As New DataSet()
        ds.Tables.Add(dt)

        ds.WriteXml(ms, XmlWriteMode.WriteSchema)

        ms.Flush()

        ms.Position = 0

        Dim buf(ms.Length) As Byte

        ms.Read(buf, 0, ms.Length)

        '_response = ds.GetXmlSchema() & "|||" & ds.GetXml() & "|||" & Encoding.ASCII.GetString(buf)
        _response = Encoding.ASCII.GetString(buf)

        Response.ContentType = "text/xml"

        Response.Write(_response)
        Response.End()
    End Sub
#End Region

#Region "Sub CompileWeb()"
    Private Sub CompileWeb()
        Dim prov As CodeDomProvider = Nothing
        prov = New VBCodeProvider
        '// Set input params for the compiler
        Dim co As New CompilerParameters()
        Dim strB As New StringBuilder()
        Dim strDir As String = Server.MapPath("")
        m_strRoot = strDir
        ProcessDirectory(strDir)

        '#2853
        If Not IsNothing(Request.QueryString("debug")) AndAlso (Request.QueryString("debug").ToLower = "true") Then
            co.IncludeDebugInformation = True
            Response.Write("<font color='red'>By setting debug=true, you have chosen to compile the site in debug mode.  This is not advised unless you are troubleshooting, as it will seriously impact performance on your webserver.  <br>When you are done debugging, to compile the site in normal mode, view compilesite.aspx without debug=true</font>")
        End If


        co.OutputAssembly = strDir & "\bin\StoreFront.dll"

        strB.Append("/rootnamespace:StoreFront.StoreFront ")

        strB.Append("/r:System.dll,")
        'Tee 10/2/2007 added for 1.1 to 2.0 transition
        strB.Append("System.configuration.dll,")
        'end Tee
        strB.Append("System.XML.dll,")
        strB.Append("System.Web.dll,")
        strB.Append("System.Web.Services.dll,")
        strB.Append("Microsoft.VisualBasic.dll,")
        strB.Append(strDir & "\bin\SystemBase.dll,")
        strB.Append(strDir & "\bin\BusinessRule.dll,")
        strB.Append(strDir & "\bin\UITools.dll,")
        strB.Append(strDir & "\bin\StoreFrontSecurity.dll,")
        'begin: GJV - 7/30/2007 - CSR
        strB.Append(strDir & "\bin\MagicAjax.dll,")
        strB.Append(strDir & "\bin\CSRBusinessRule.dll,")
        strB.Append(strDir & "\bin\CSRSystemBase.dll,")
        strB.Append(strDir & "\bin\CSRDataAccess.dll,")
        'end: GJV - 7/30/2007 - CSR
        'begin: Integration 9/4/2007
        strB.Append(strDir & "\bin\StoreFront.Integration.dll,")
        strB.Append(strDir & "\bin\RMSBusinessRule.dll,")
        strB.Append(strDir & "\bin\DataAccess.dll,")
        'end: Integration 9/4/2007
        strB.Append(strDir & "\bin\UltimateEditor.dll,")
        strB.Append(strDir & "\bin\UltimateSpell.dll,")
        strB.Append("System.Drawing.dll,")
        'update #2304
        strB.Append("System.ServiceProcess.dll,")
        strB.Append("System.Data.dll ")
        strB.Append("/imports:")
        strB.Append("System.Collections,")
        strB.Append("Microsoft.VisualBasic,")
        strB.Append("System.Web,")
        strB.Append("System.Web.UI,")
        strB.Append("System.Web.UI.WebControls,")
        strB.Append("System.Web.UI.HTMLControls,")
        strB.Append("System.Web.Services,")
        strB.Append("System.Web.Services.WebService,")
        strB.Append("System.Data,")
        strB.Append("System,")
        'update #2304
        'Tee 10/2/2007 added for 1.1 to 2.0 transition
        strB.Append("System.configuration,")
        'end Tee
        strB.Append("System.ServiceProcess,")
        strB.Append("System.Drawing")

        co.CompilerOptions = strB.ToString()

        '// we want to genereate a DLL
        co.GenerateExecutable = False

        Dim strFile As String
        Dim i As Long = 0
        Dim strFileList(m_arFileList.Count - 1) As String

        For Each strFile In m_arFileList
            strFileList.SetValue(strFile, i)
            i = i + 1
        Next

        o = prov.CompileAssemblyFromFile(co, strFileList)

        Dim objVBC As New ArrayList
        'Dim objEr As VBCError
        Dim dr As DataRow

        dt.Columns.Add(New DataColumn("Line"))
        dt.Columns.Add(New DataColumn("ErrorNumber"))
        dt.Columns.Add(New DataColumn("Error"))
        dt.Columns.Add(New DataColumn("FileName"))

        If o.Errors.HasErrors = True Then
            Dim str As CompilerError

            For Each str In o.Errors
                dr = dt.NewRow()
                dr("Line") = str.Line
                dr("ErrorNumber") = str.ErrorNumber
                dr("Error") = str.ErrorText
                dr("FileName") = str.FileName
                dt.Rows.Add(dr)
            Next
        End If

        If (Request.Form("XMLView") <> "") Then
            If (o.Output.Count > 0 And o.Errors.HasErrors = False) Then
                Dim strOutput As String

                For Each strOutput In o.Output
                    dr = dt.NewRow
                    dr("Line") = "N/A"
                    dr("ErrorNumber") = "VBC Error"
                    dr("Error") = strOutput.ToString()
                    dr("FileName") = "N/A"
                    dt.Rows.Add(dr)
                Next
            End If
        End If

    End Sub
#End Region

#Region "Sub DisplayHTML()"
    Private Sub DisplayHTML()
        Dim objVBC As New ArrayList()
        Dim objEr As VBCError

        If o.Errors.HasErrors = True Then
            Dim strOutput As String

            For Each strOutput In o.Output
                objEr = New VBCError()
                objEr.ErrorText = stroutput.ToString()
                objVBC.Add(objEr)
            Next

            DataGrid1.DataSource = dt
            DataList2.DataSource = objVBC
            DataGrid1.DataBind()
            DataList2.DataBind()

            Label3.Text = "There Are Errors"
            Exit Sub
        Else
            Dim strOutput As String
            If (o.Output.Count > 0) Then
                Label3.Text = "There Are Errors"
            Else
                Label3.Text = "Compile Success"
            End If
            For Each strOutput In o.Output
                objEr = New VBCError()
                objEr.ErrorText = stroutput.ToString()
                objVBC.Add(objEr)
            Next
            DataList2.DataSource = objVBC
            DataList2.DataBind()
        End If
    End Sub
#End Region

#Region "Sub ProcessDirectory(ByVal sourceDirectory As String)"
    Private Sub ProcessDirectory(ByVal sourceDirectory As String)
        If (sourceDirectory.Substring(sourceDirectory.LastIndexOf("\")).IndexOf("\bin") <> -1) Then
            Exit Sub
        ElseIf (sourceDirectory.Substring(sourceDirectory.LastIndexOf("\")).IndexOf("\_vti") <> -1) Then
            Exit Sub
        End If

        Dim fileEntries As String() = Directory.GetFiles(sourceDirectory)

        ' Process the list of files found in the directory
        Dim fileName As String
        For Each fileName In fileEntries
            ProcessFile(fileName)
        Next fileName

        Dim subdirectoryEntries As String() = Directory.GetDirectories(sourceDirectory)

        ' Recurse into subdirectories of this directory
        Dim subdirectory As String
        For Each subdirectory In subdirectoryEntries
            If (subdirectory.ToLower() = m_strRoot.ToLower() & "\commoncontrols" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\controls" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl\management" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl\csr" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl\csr\controls" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl\management\authentication" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl\management\commoncontrols" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl\management\controls" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl\management\integration" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl\management\quickbooks" Or _
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl\management\quickbooks\controls") Then
                ProcessDirectory(subdirectory)
            End If
        Next subdirectory

    End Sub 'ProcessDirectory
#End Region

#Region "Sub ProcessFile(ByVal strFileName As String)"
    Private Sub ProcessFile(ByVal strFileName As String)
        If (strFileName.IndexOf("\site.vb") <> -1) Then
            Exit Sub
        ElseIf (strFileName.LastIndexOf(".") < 0) Then
            Exit Sub
        ElseIf (strFileName.Substring(strFileName.Length - 3) <> ".vb") Then
            Exit Sub
        End If
        m_arFileList.Add(strFileName)
    End Sub
#End Region
End Class


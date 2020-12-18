'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

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

Public Class CompileSite
    Inherits System.Web.UI.Page

    Protected WithEvents DataList2 As System.Web.UI.WebControls.DataList
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label

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
        Dim csc As New VBCodeProvider()
        Dim icc As ICodeCompiler = csc.CreateCompiler()

        '// Set input params for the compiler
        Dim co As New CompilerParameters()
        Dim strB As New StringBuilder()
        Dim strDir As String = Server.MapPath("")
        m_strRoot = strDir
        ProcessDirectory(strDir)

        co.OutputAssembly = strDir & "\bin\StoreFront.dll"

        strB.Append("/rootnamespace:StoreFront.StoreFront ")

        strB.Append("/r:System.dll,")
        strB.Append("System.XML.dll,")
        strB.Append("System.Web.dll,")
        strB.Append("Microsoft.VisualBasic.dll,")
        strB.Append(strDir & "\bin\SystemBase.dll,")
        strB.Append(strDir & "\bin\BusinessRule.dll,")
        strB.Append(strDir & "\bin\UITools.dll,")
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
        strB.Append("System.Data,")
        strB.Append("System,")
        'update #2304
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

        o = icc.CompileAssemblyFromFileBatch(co, strFileList)

        Dim objVBC As New ArrayList()
        Dim objEr As VBCError
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
                subdirectory.ToLower() = m_strRoot.ToLower() & "\ssl\management\controls" Or _
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


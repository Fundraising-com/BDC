<%@ Language=VBScript %>
<%
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: filelisting.asp
	
'@FILEVERSION: 1.0.1

'@VERSIONDATETIME: 2/21/01

'@DESCRIPTION: displays store files 

'@STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2000, 2001 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
Dim objFso, objFolders, objFiles, objSubFolders, sMapPath
sMapPath = Request.ServerVariables("PATH_INFO")
sMapPath = Replace(sMapPath, "/ssl/admin/filelisting.asp", "/images")
Set objFso = Server.CreateObject("Scripting.FileSystemObject")
Set objFolders = objFso.GetFolder(Server.MapPath(sMapPath))

Function getFiles(objFolders)
	For Each objFiles in objFolders.Files
		If Instr(objFiles.Path, "_")=0 Then
			sLPath = "http://"& Request.ServerVariables("LOCAL_ADDR") & Request.ServerVariables("PATH_INFO")
			sLPath = Left(sLPath, Instr(sLPath, "ssl")-1)
			sRPath = cStr(objFiles.Path)
			sRemove = Left(sRPath, Instr(sRPath, "images")-1)
			sRPath = Replace(sRPath, sRemove, "")
			sRPath = Replace(sRPath, "\", "/")
			sPath = sLPath & sRPath
			Response.Write "<option value=""" & sPath & """>" & objFiles.Name & "</option>"
		End If
	Next
	For Each objSubFolders in objFolders.SubFolders
		getFiles(objSubFolders)
	Next
End Function

%>
<SCRIPT language="javascript">
function fileSel() {
	var i
	for (i=0; i<document.frmFileSelect.selFiles.length; i++){
		if (document.frmFileSelect.selFiles[i].selected){
			document.frmFileSelect.txtFile.value = document.frmFileSelect.selFiles[i].value
			document.images[0].src = document.frmFileSelect.selFiles[i].value
		}
	}
}
function loadPic() {
	document.images[0].src = document.frmFileSelect.selFiles[0].value
}
function choice() {
	if (window.name == "txtInput1") {
		opener.document.openerForm.txtInput1.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput2") {
		opener.document.openerForm.txtInput2.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput3") {
		opener.document.openerForm.txtInput3.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput4") {
		opener.document.openerForm.txtInput4.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput5") {
		opener.document.openerForm.txtInput5.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput6") {
		opener.document.openerForm.txtInput6.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput7") {
		opener.document.openerForm.txtInput7.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput8") {
		opener.document.openerForm.txtInput8.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput9") {
		opener.document.openerForm.txtInput9.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput10") {
		opener.document.openerForm.txtInput10.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput11") {
		opener.document.openerForm.txtInput11.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput12") {
		opener.document.openerForm.txtInput12.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput13") {
		opener.document.openerForm.txtInput13.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput14") {
		opener.document.openerForm.txtInput14.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput15") {
		opener.document.openerForm.txtInput15.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput16") {
		opener.document.openerForm.txtInput16.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput17") {
		opener.document.openerForm.txtInput17.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput18") {
		opener.document.openerForm.txtInput18.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput19") {
		opener.document.openerForm.txtInput19.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput20") {
		opener.document.openerForm.txtInput20.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput21") {
		opener.document.openerForm.txtInput21.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput22") {
		opener.document.openerForm.txtInput22.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput23") {
		opener.document.openerForm.txtInput23.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput24") {
		opener.document.openerForm.txtInput24.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput28") {
		opener.document.openerForm.txtInput28.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput33") {
		opener.document.openerForm.txtInput33.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput38") {
		opener.document.openerForm.txtInput38.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput43") {
		opener.document.openerForm.txtInput43.value = document.frmFileSelect.txtFile.value
	}
	if (window.name == "txtInput48") {
		opener.document.openerForm.txtInput48.value = document.frmFileSelect.txtFile.value
	}
	window.close();
}
</SCRIPT>
<HTML>
<HEAD>
<title>SF Filelisting Popup Window</title>
</HEAD>
<BODY onload="javascript:loadPic()">
<form name="frmFileSelect">

<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td>
<center><select name="selFiles" size=10 onChange="javascript:fileSel()">
<%=getFiles(objFolders)%>
</select></center><br>
<center><input type="button" name="btn1" value="OK" onclick="javascript:choice()"></center>
</td>
<td>
<center><img src=""></center>
</td>
</tr>
<tr>
<td>
</td>
<td>
<input type="text" name="txtFile" value="" size=55>
</td>
</tr>
</table>
</form>
</BODY>
</HTML>
<%

Set objFso = Nothing
Set objFolders = Nothing

%>
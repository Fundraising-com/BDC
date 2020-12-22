<%@ Language=VBScript %>
<%
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: color.asp
	
'@FILEVERSION: 1.0.1

'@VERSIONDATETIME: 2/21/01

'@DESCRIPTION: set store color 

'@STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2000, 2001 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO


%>


<script language="javascript">
function choose(txtColor) {
	document.form1.colorChoice.value = "#" + txtColor;
}
function change() {
	if (window.name == "txtInput2") {
		opener.document.openerForm.txtInput2.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput4") {
		opener.document.openerForm.txtInput4.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput5") {
		opener.document.openerForm.txtInput5.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput6") {
		opener.document.openerForm.txtInput6.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput8") {
		opener.document.openerForm.txtInput8.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput10") {
		opener.document.openerForm.txtInput10.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput12") {
		opener.document.openerForm.txtInput12.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput14") {
		opener.document.openerForm.txtInput14.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput17") {
		opener.document.openerForm.txtInput17.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput19") {
		opener.document.openerForm.txtInput19.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput22") {
		opener.document.openerForm.txtInput22.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput24") {
		opener.document.openerForm.txtInput24.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput27") {
		opener.document.openerForm.txtInput27.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput29") {
		opener.document.openerForm.txtInput29.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput32") {
		opener.document.openerForm.txtInput32.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput34") {
		opener.document.openerForm.txtInput34.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput37") {
		opener.document.openerForm.openerForm.txtInput37.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput39") {
		opener.document.openerForm.txtInput39.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput42") {
		opener.document.openerForm.txtInput42.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput44") {
		opener.document.openerForm.txtInput44.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput47") {
		opener.document.openerForm.txtInput47.value = document.form1.colorChoice.value
	}
	if (window.name == "txtInput49") {
		opener.document.openerForm.txtInput49.value = document.form1.colorChoice.value
	}
	window.close()
}
</script>
<HTML>
<HEAD>
<title>Color Palette Popup Window</title>
</HEAD>
<BODY>
<form name="form1">
<table width = "100%" border=0 cellpadding=0 cellspacing=0>
<%
base1 = "00"
base2 = "00"
base3 = "00"
For i=0 to 5
	If base1 = "00" Then
		r1 = base1
	Else
		r1 = hex(base1)
	End If 
	For j=0 to 5
		If j > 2 Then
			fontC = "#000000"
		Else
			fontC = "#ffffff"
		End If 
		Response.Write "<tr>"
		For k=0 to 5
			If base3 = "00" Then
				b1 = "00"
			Else
				b1 = hex(base3)
			End If
			If base2 = "00" Then
				g1 = "00"
			Else
				g1 = hex(base2)
			End If
			onClickChoise = "javascript:choose('" & r1 & g1 & b1 & "')"
			response.Write "<td align=center bgcolor=""#" & r1 & g1 & b1 & """><a href=""" & onClickChoise & ";""><font size = ""2"" color=""" & fontC & """>#" & r1 & g1 & b1 & "</font></td>"
			If base3 = 255 Then
				Base3 = "00"
			Else
				base3 = base3 + 51
			End If 
		Next
		Response.Write "</tr>"
		If base2 = 255 Then
			base2 = "00"
		Else
			base2 = base2 + 51
		End If 
	Next
	base1 = base1 + 51
Next
%>
</table>
<table width = "100%" border=0 cellpadding=0 cellspacing=0>
<tr>
<td align="middle"><input type="text" name="colorChoice"></td> 
</tr>
<tr>
<td align="middle"><input type="button" name="btn1" value="OK" onClick="javascript:change()"></td> 
</tr>
</table>
</form>
</BODY>
</HTML>

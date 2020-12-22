<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CloseUp.aspx.vb" Inherits="StoreFront.StoreFront.CloseUp" %>
<%@ Register TagPrefix="uc1" TagName="Swatch" Src="controls/Swatch.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Image Close Up</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.1

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<script language="javascript">
//browser check
var ie = (document.all) ? true : false
var ns = (document.layers) ? true : false

function CloseMe()
{
window.close()
}

function chgImg(imgField,newImg,UID) {
			if (ns) {eval('window.document.CloseUp.images[imgField].src = '+newImg+'.src')}
			else {window.document.CloseUp[imgField].src = eval(newImg+'.src')}
			
}

function preload(imgObj,imgSrc) {
	if (document.images) {
		eval(imgObj+' = new Image()'); 
		eval(imgObj+'.src = "'+imgSrc+'"');
	}
}

		</script>
	</HEAD>
	<body class="GeneralPage">
		<form id="CloseUp" Runat="server">
			<TABLE class="GeneralTable" cellSpacing="0" width="100%">
				<TR>
					<TD id="ContentCell" vAlign="top" align="middle"><!-- Content Start -->
						<uc1:Swatch id="Swatches" runat="server"></uc1:Swatch>
						<br>
						<a href="Javascript:CloseMe()">Close Window</a>
						<!-- Content End --></TD>
				</TR>
			</TABLE>
			</TD></TR></TABLE>
		</form>
	</body>
</HTML>

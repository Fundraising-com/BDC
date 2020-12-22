<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OnlineChat.aspx.vb" Inherits="StoreFront.StoreFront.OnlineChat" %>
<%@ Register TagPrefix="uc1" TagName="OnlineChatControl" Src="Controls/OnlineChatControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.0.0

'@STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>

<HTML>
  <HEAD>
		<title><% writeTitle() %>  - Merchant Tools</title>
		
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
<table cellspacing="0" class="GeneralTable">
<tr>
<td class="TopBanner" colspan="3">
	<!-- Top Banner Start -->
	<table width="100%" cellpadding="0" cellspacing="0">
	<tr>
	<td class="TopBanner2" width="20%"><img src="images/sflogo.jpg"></td>
	        <td class="TopBanner">&nbsp;&nbsp;Merchant Tools</td>
	</tr>
	</table>
	<!-- Top Banner End -->
</td>
</tr>

<tr>
<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
	<!-- Top Sub Banner Start -->Online 
                  Chat
	<!-- Top Sub Banner End --></td>
</tr>
<tr>
<td class="LeftColumn" id="LeftColumnCell">
	<!-- Left Column Start -->
	<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
	<!-- Left Column End --></td>
<td class="Content" vAlign="top">
	<!-- Content Start -->
	<table width="100%" cellSpacing="3" cellPadding="5" border="0">
	<tr>
		<td class="content" align=middle>
			<P id="ErrorAlignment" runat="server" align=center>
				<font color=#ff0000>
					<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages">
					</asp:label>
				</font>
			</P>
		</td>
	</tr>
	<tr>
	<td class="Content" align="right">
		<!-- Help Button -->
		<a href="javascript: doHelp(' http://support.storefront.net/mtdocs/onlinechat.asp ')">
		<img src="images/help.jpg" border="0"></a>
		<!-- End Help Button --></td>
	</tr>
	<tr>
	<td class="Content">
		<!-- Instruction Start -->
		<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
		<!-- Instruction End --></td>
	</tr>
	<tr>
	<td class="content" vAlign="top" align="middle" colSpan="2">
		<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
		<td class="ContentTable" colSpan="9" height="1">
			<img height="1" src="images/clear.gif"></td>
		</tr>
		<tr>
		<td class="ContentTableHeader" width="1">
			<img src="images/clear.gif" width="1"></td>
		<td class="content" vAlign="top" align="middle" colSpan="6">
			<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
			<tr>
			<td class="content" align="middle">
				<uc1:OnlineChatControl id="OnlineChat" runat="server">
				</uc1:OnlineChatControl>
			</td>
			</tr>
			</table>
		</td>
		<td class="ContentTableHeader" width="1">
			<img src="images/clear.gif" width="1"></td>
		</tr>
		<tr>
		<td class="ContentTable" colSpan="8" height="1">
			<img height="1" src="images/clear.gif"></td>
		</tr>
		</table>
	</td>
	</tr>
	</table>
	<!-- Content End --></td>
</tr>
</table></FORM>	</body>
</HTML>

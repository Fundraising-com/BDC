<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OrderStatus.aspx.vb" Inherits="StoreFront.StoreFront.OrderStatus"%>
<%@ Register TagPrefix="uc1" TagName="OrderStatusControl" Src="Controls/OrderStatusControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<HTML>
  <HEAD>
		<title><% writeTitle() %>  - Merchant Tools</title>

<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.0

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
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
	<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
</td>
</tr>

<tr>
<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Order Status"></uc1:TopSubBanner>
</td>
</tr>
<tr>
<td class="LeftColumn" id="LeftColumnCell">
	<!-- Left Column Start -->
	<uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
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
		<a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/orderman_ov.asp ')">
		<img src="images/help.jpg" border="0"></a>
		<!-- End Help Button --></td>
	</tr>
	<tr>
	<td class="Content">
		<!-- Instruction Start -->
		<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
		<!-- Instruction End --></td>
	</tr>
	<tr class="Headings" vAlign="top">
	<td colspan="2" class="Headings">&nbsp;Order Status </td>
	</tr>
	<tr>
	<td colspan="2" valign="top" class="content" align="middle">&nbsp;
		<uc1:OrderStatusControl id="OrderStatusControl1" runat="server">
		</uc1:OrderStatusControl>
	</td>
	</tr>
	</table>
	<!-- Content End --></td>
</tr>
</table></form>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="PackingSlipAddress" Src="Controls/PackingSlipAddress.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PackingSlip.aspx.vb" Inherits="StoreFront.StoreFront.PackingSlip"%>
<%@ Register TagPrefix="uc1" TagName="PackingSlipProducts" Src="Controls/PackingSlipProducts.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

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
	<table cellspacing="0" class="GeneralTable" width=100%>
	<tr>
	<td class="content" align="middle">
		<p id="ErrorAlignment" runat="server" align="center"><font color="#ff0000">
		<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
		</font></p>
	</td>
	</tr>
	<tr>
	<td class="content" vAlign="top" align="middle">
		<table id="HistoryTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
		<tr>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		<td class="ContentTableHeader" noWrap align="left" width="50%">&nbsp;&nbsp;Order ID:&nbsp;<asp:label id="lblOrderID" Runat="server" CssClass="ContentTableHeader"></asp:label></td>
		<td class="ContentTableHeader"></td>
		<td class="ContentTableHeader"></td>
		<td class="ContentTableHeader"></td>
		<td class="ContentTableHeader" noWrap align="right" width="50%">Order Date:&nbsp;<asp:label id="lblOrderDate" Runat="server" CssClass="ContentTableHeader"></asp:label>&nbsp;</td>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		</tr>
		<tr>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		<td class="content" colSpan="5">&nbsp; </td>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		</tr>
		<tr>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		<td class="content" colSpan="5" align="middle">
			<uc1:PackingSlipAddress id="PackingSlipAddress1" runat="server"></uc1:PackingSlipAddress>
		</td>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		</tr>
		<tr>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		<td class="content" colSpan="5">&nbsp; </td>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		</tr>
		<tr>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		<td class="content" align="middle" colspan="5">
			<table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
			<tr>
			<td class="content" colspan="5" align="middle">
				<uc1:PackingSlipProducts id="PackingSlipProducts1" runat="server"></uc1:PackingSlipProducts>
			</td>
			</tr>
			</table>
		</td>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		</tr>
		<tr>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		<td class="content" align="middle" colspan="5">&nbsp; </td>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		</tr>
		<tr>
		<td class="ContentTable" colSpan="7" height="1"><img height="1" src="images/clear.gif"></td>
		</tr>
		</table>
	</td>
	</tr>
	</table>
</form>


	</body>
</HTML>

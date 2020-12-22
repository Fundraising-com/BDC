<%@ Register TagPrefix="uc1" TagName="OrderResultsControl" Src="Controls/OrderResultsControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OrderResults.aspx.vb" Inherits="StoreFront.StoreFront.OrderResults"%>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
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
						<form id="Form2" method="post" runat="server" >
<table cellspacing="0" class="GeneralTable">
<tr>
<td class="TopBanner" colspan="3">
	<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
</td>
</tr>
											<tr>
												<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Order Results"></uc1:TopSubBanner>
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
															<td colspan="2" width="100%" class="Content" align="right">
																<!-- Help Button -->
																<a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/orderman_ov.asp ')">
																	<img src="images/help.jpg" border="0"></a> 
																<!-- End Help Button --></td>
														</tr>
														<tr>
															<td colspan="2" class="Content">
																<!-- Instruction Start -->
																<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
																<!-- Instruction End --></td>
														</tr>
														<tr>
															<td class="content"  colspan="2">
																<p id="ErrorAlignment" runat="server">
																	<font color=#ff0000>
																		<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages">
																		</asp:label>
																	</font>
																</p>
															</td>
														</tr>
														<tr class="Headings" vAlign="top" id="trOrderSummary" runat="server">
															<td colspan="2" class="Headings">&nbsp;Order Summary
															</td>
														</tr>
														<tr>
															<td height="15" class="Content" valign="bottom" width="50%" align="left" nowrap>
																<asp:label id="Status" CssClass="Content" Runat="server"></asp:label>
															</td>
															<td height="15" class="Content" valign="bottom" width="50%" align="right" nowrap>
																<asp:label id="DateInfo" CssClass="Content" Runat="server"></asp:label>
															</td>
														</tr>
														<tr>
															<td colspan="2" valign="top" class="content" align="middle" width="100%">&nbsp;
																<uc1:OrderResultsControl id="OrderResultsControl1" runat="server" width="100%"></uc1:OrderResultsControl>
															</td>
														</tr>
													</table>
													<!-- Content End --></td>
											</tr>
										</table></TD></TR></TD></TR></TABLE></form></TD></TR></TBODY></TABLE>
	</body>
</HTML>

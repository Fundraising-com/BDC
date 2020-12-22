<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StoreReports.aspx.vb" Inherits="StoreFront.StoreFront.StoreReports"%>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Store Reports</title>
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
		<script language="javascript">
			<!--
			function SetValidation()
			{
				ResetForm(window.document.Form2);
				
				if (window.document.Form2.elements["ddDateRange"].options[window.document.Form2.elements["ddDateRange"].options.selectedIndex].value=="5")
				{window.document.Form2.elements["txtFrom"].required=true
				window.document.Form2.elements["txtTo"].required=true
				}
				else
				{window.document.Form2.elements["txtFrom"].required=false
				window.document.Form2.elements["txtTo"].required=false
				}
				window.document.Form2.elements["txtFrom"].date=true
				window.document.Form2.elements["txtTo"].date=true
				window.document.Form2.elements["txtFrom"].title="From"
				window.document.Form2.elements["txtTo"].title="To"
				
				return ValidateForm(window.document.Form2)
			}
			
		

			//-->
		</script>
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
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Sales Reports"></uc1:TopSubBanner>
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
															<td class="Content" align="right">
																<!-- Help Button -->
																<a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/reports_ov.asp ')">
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
															<td class="Content">
																<p id="ErrorAlignment" runat="server" align="center">
																	<font color="#ff0000">
																		<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label>
																	</font>
																</p>
																<p id="MessageAlignment" runat="server" align="center">
																	<font color="#ff0000">
																		<asp:Label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:Label>
																	</font>
																</p>
															</td>
														</tr>
														<tr>
															<td class="Content">
																<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<tr>
																		<td class="ContentTable" colSpan="9" height="1">
																			<img height="1" src="images/clear.gif"></td>
																	</tr>
																	<tr>
																		<td class="ContentTableHeader" width="1">
																			<img src="images/clear.gif" width="1"></td>
																		<td class="content" vAlign="top" align="middle" colSpan="6">
																			<table width="100%" cellSpacing="0" cellPadding="0" border="0">
																				<tr>
																					<td class="ContentTableHeader" valign="center" colspan="7">&nbsp;Sales Reports
																					</td>
																				</tr>
																				<tr>
																					<td class="content" noWrap align="left" colSpan="7">&nbsp;</td>
																				</tr>
																				<tr>
																					<td class="content" noWrap align="right">Date Range:&nbsp;
																					</td>
																					<td class="content" align="left">
																						<asp:dropdownlist CssClass="content" id="ddDateRange" runat="server" AutoPostBack="True">
																							<asp:ListItem Selected="True" Value="1">Today</asp:ListItem>
																							<asp:ListItem Value="2">Week To Date</asp:ListItem>
																							<asp:ListItem Value="3">Month To Date</asp:ListItem>
																							<asp:ListItem Value="4">Year To Date</asp:ListItem>
																							<asp:ListItem Value="5">Enter a Date Range</asp:ListItem>
																						</asp:dropdownlist>
																					</td>
																					<td>&nbsp;</td>
																					<td class="content" align="right">From:&nbsp;
																					</td>
																					<td class="content" align="left">
																						<asp:textbox CssClass="content" id="txtFrom" runat="server" MaxLength="12" Enabled="False" Width="97px"></asp:textbox>
																					</td>
																					<td class="content" align="right">To:&nbsp;
																					</td>
																					<td class="content" align="left">
																						<asp:textbox id="txtTo" CssClass="content" runat="server" MaxLength="12" Enabled="False" Width="97px"></asp:textbox>
																					</td>
																				</tr>
																				<tr>
																					<td class="content" noWrap align="left" colSpan="7">&nbsp;</td>
																				</tr>
																				<tr>
																					<td class="content" noWrap align="left" colSpan="7">&nbsp;</td>
																				</tr>
																				<tr class="Content">
																					<td class="content" noWrap align="right">Report Type:&nbsp;
																					</td>
																					<td class="content" align="left" colSpan="7">
																						<asp:dropdownlist id="ddReportType" CssClass="content" runat="server">
																							<asp:ListItem Selected="True" Value="SalesDetails.aspx">Sales 
				Details</asp:ListItem>
																							<asp:ListItem Value="SalesSummary.aspx">Sales Summary</asp:ListItem>
																							<asp:ListItem Value="ProductSelect.aspx">Product Sales</asp:ListItem>
																							<asp:ListItem Value="TransactionService.aspx">Transaction 
				Service Reports</asp:ListItem>
																							<asp:ListItem Value="CouponSales.aspx">Coupon Sales</asp:ListItem>
																							<asp:ListItem Value="SalesTax.aspx">Sales Tax</asp:ListItem>
																						</asp:dropdownlist>
																					</td>
																				</tr>
																				<tr>
																					<td class="content" noWrap align="left" colSpan="7">&nbsp;</td>
																				</tr>
																				<tr>
																					<td class="content" align="right" colSpan="7">
																						<asp:LinkButton ID="cmdSubmit" Runat="server">
																							<asp:Image BorderWidth="0" ID="imgSubmit" runat="server" ImageUrl="images/submit.jpg" AlternateText="Submit"></asp:Image>
																						</asp:LinkButton>
																						&nbsp;</td>
																				</tr>
																				<tr>
																					<td class="content" align="right" colSpan="7">&nbsp;</td>
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
			</table>
		</form>
	</body>
</HTML>

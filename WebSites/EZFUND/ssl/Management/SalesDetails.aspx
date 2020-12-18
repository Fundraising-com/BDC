<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesDetails.aspx.vb" Inherits="StoreFront.StoreFront.SalesDetails"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.0.0

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
	<body class="GeneralPage" id="BodyTag" runat="server">
		<form id="Form2" method="post" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<tr>
					<td class="TopBanner" colSpan="3">
						<!-- Top Banner Start -->
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td class="TopBanner2" width="20%"><IMG src="images/sflogo.jpg"></td>
								<td class="TopBanner">&nbsp;&nbsp;Merchant Tools</td>
							</tr>
						</table>
						<!-- Top Banner End --></td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<!-- Top Sub Banner Start --> Sales Reports 
						<!-- Top Sub Banner End --></td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table cellSpacing="3" cellPadding="5" width="100%" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs/reports_sd.asp ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="Content" align="middle">
									<p id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></font></p>
								</td>
							</tr>
							<tr class="Headings" vAlign="top">
								<td class="Headings">&nbsp;Sales Details
								</td>
							</tr>
							<tr>
								<td class="Content"><asp:label id="DateInfo" CssClass="Content" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td><asp:datagrid id="DataGrid1" runat="server" PageSize="10" AutoGenerateColumns="False" ShowHeader="False" AllowPaging="True" Width="100%" BorderWidth="0px" CellPadding="0">
										<Columns>
											<asp:TemplateColumn>
												<ItemTemplate>
													<table runat="server" id="HistoryTable" border="0" cellpadding="0" cellspacing="0" width="100%">
														<tr>
															<td class="ContentTableHeader" width="1">
																<img src="images/clear.gif" width="1"></td>
															<td class="ContentTableHeader">&nbsp;</td>
															<td class="ContentTableHeader" align="center" valign="middle" width="20%" nowrap>
																Order ID&nbsp;</td>
															<td class="ContentTableHeader" align="center" valign="middle" width="20%" noWrap>
																Order Date&nbsp;</td>
															<td class="ContentTableHeader" align="center" valign="middle" width="20%" noWrap>
																Customer Name&nbsp;
															</td>
															<td class="ContentTableHeader" align="center" valign="middle" width="20%" noWrap>
																Order Total&nbsp;
															</td>
															<td class="ContentTableHeader" align="center" valign="middle" width="20%" noWrap>
																Action</td>
															<td class="ContentTableHeader">&nbsp;</td>
															<td class="ContentTableHeader" width="1">
																<img src="images/clear.gif" width="1"></td>
														</tr>
														<tr>
															<td class="ContentTable" width="1">
																<img src="images/clear.gif" width="1"></td>
															<td class="Content" colspan="7">&nbsp;</td>
															<td class="ContentTable" width="1">
																<img src="images/clear.gif" width="1"></td>
														</tr>
														<tr>
															<td class="ContentTable" width="1">
																<img src="images/clear.gif" width="1"></td>
															<td class="Content">&nbsp;</td>
															<td class="Content" align="center" valign="Top" width="20%" noWrap>
																<%#DataBinder.Eval(Container.DataItem,"OrderNumber")%>
															</td>
															<td class="Content" align="center" valign="Top" width="20%" noWrap>
																<%#DataBinder.Eval(Container.DataItem,"OrderDate")%>
															</td>
															<td class="Content" align="center" valign="Top" width="20%" noWrap>
																<%#DataBinder.Eval(Container.DataItem,"CustomerName")%>
															</td>
															<td class="Content" align="center" valign="Top" width="20%" noWrap>
																<%#Format(DataBinder.Eval(Container.DataItem,"GrandTotal"), "c")%>
															</td>
															<td class="Content" align="center" valign="middle" width="20%">
																<asp:LinkButton ID="cmdViewDetail" OnClick="ViewDetailClick" Runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"uid")%>' CommandName='<%# DataBinder.Eval(Container.DataItem,"CustomerID")%>'>
																	<asp:Image BorderWidth="0" ID="imgViewDetail" runat="server" ImageUrl="images/view.jpg" AlternateText="View"></asp:Image>
																</asp:LinkButton>
															</td>
															<td class="Content">&nbsp;</td>
															<td class="ContentTable" width="1">
																<img src="images/clear.gif" width="1"></td>
														</tr>
														<tr>
															<td class="ContentTable" width="1">
																<img src="images/clear.gif" width="1"></td>
															<td class="Content" colspan="7">&nbsp;</td>
															<td class="ContentTable" width="1">
																<img src="images/clear.gif" width="1"></td>
														</tr>
														<tr>
															<td class="ContentTable" colSpan="9" height="1">
																<img height="1" src="images/clear.gif"></td>
														</tr>
													</table>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Right" Position="TopAndBottom" CssClass="Content" Wrap="False" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
	</body>
</HTML>

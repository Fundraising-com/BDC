<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="GeneralControl" Src="Controls/GeneralControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="HomePage.aspx.vb" Inherits="StoreFront.StoreFront.HomePage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.1

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
					<td class="TopBanner" colSpan="3"><uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner></td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3"><uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Layout Templates"></uc1:TopSubBanner></td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table cellSpacing="3" cellPadding="5" width="100%" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/home_page.asp  ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<TR>
							<tr>
								<td class="content" align="center">
									<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></font></P>
								</td>
							</tr>
							<TR>
								<TD class="content" vAlign="top" align="center">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
											<td class="ContentTable" colSpan="6" height="1"><table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR id="Add" runat="server">
														<TD class="ContentTableHeader" align="left" width="100%" colSpan="17"></TD>
													</TR>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center"><A class="content" href="CatalogGeneral.aspx">General
														</A></td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Contentr" align="center"><A class="content" href="HomePage.aspx"><b>Home Page</b></A>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Contentr" align="center"><A class="content" href="CatalogPages.aspx">Catalog 
																Pages</A>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center"><A class="content" href="ProductPages.aspx">Product 
																Pages</A>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center"><A class="content" href="ManageLabels.aspx">Labels</A>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<TR id="Tr1" runat="server">
														<TD class="ContentTableHeader" align="left" width="100%" colSpan="17"></TD>
													</TR>
												</table>
											</td>
											<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<TR>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<TD class="content" vAlign="top" align="center" colSpan="6">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="center">
															<TABLE class="ContentTable" id="Table1" cellSpacing="0" cellPadding="0" width="100%">
																<TR>
																	<TD class="ContentTableHeader" colSpan="4">&nbsp;Home Page Template</TD>
																</TR>
																<tr>
																	<td class="ContentTable" width="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="content">&nbsp;</td>
																	<td class="ContentTable" width="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
																	<TD class="Content" width="100%">
																		<table class="content" cellpadding="0" cellspacing="0" width="100%">
																			<tr>
																				<td width="33%" align="center">
																					<img src="images/homepagedetail1.jpg"><br>
																					<asp:RadioButton id="rdoTemplate1" Runat="server" GroupName="HomePageTemplate" Text="Template 1" />
																				</td>
																				<td width="33%" align="center">
																					<img src="images/homepagedetail2.jpg"><br>
																					<asp:RadioButton id="rdoTemplate2" Runat="server" GroupName="HomePageTemplate" Text="Template 2" />
																				</td>
																				<td width="33%" align="center">
																					<img src="images/homepagedetail3.jpg"><br>
																					<asp:RadioButton id="rdoTemplate3" Runat="server" GroupName="HomePageTemplate" Text="Template 3" />
																				</td>
																			</tr>
																		</table>
																	</TD>
																	<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
																</tr>
																<tr>
																	<td class="ContentTable" width="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="content">&nbsp;</td>
																	<td class="ContentTable" width="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<TR>
																	<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
																</TR>
															</TABLE>
														</td>
													</tr>
													<tr>
														<td class="content" align="right"><asp:imagebutton id="cmdSave" runat="server" ImageUrl="images/save.jpg"></asp:imagebutton></td>
													</tr>
												</table>
											</TD>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</TR>
										<tr>
											<td class="ContentTable" colSpan="8" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</TD>
							</TR>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
	</body>
</HTML>

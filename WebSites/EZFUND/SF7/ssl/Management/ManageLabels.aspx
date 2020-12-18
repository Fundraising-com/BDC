<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ManageLabels.aspx.vb" Inherits="StoreFront.StoreFront.ManageLabels" %>
<%@ Register TagPrefix="uc1" TagName="GeneralControl" Src="Controls/GeneralControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
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
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/catalog_labels.asp  ')"><IMG src="images/help.jpg" border="0"></A>
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
													<TR id="Tr2" runat="server">
														<TD class="ContentTableHeader" align="left" width="100%" colSpan="17"></TD>
													</TR>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center"><A class="content" href="CatalogGeneral.aspx">General</A>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center">
															<a class="content" href="HomePage.aspx">Home Page</a>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center"><A class="content" href="CatalogPages.aspx">Catalog 
																Pages</A>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center"><A class="content" href="ProductPages.aspx">Product 
																Pages</A>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center"><B>Labels</B>
														</td>
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
															<table class="ContentTable" cellSpacing="0" cellPadding="0" width="100%">
																<tr>
																	<td class="ContentTableHeader" colSpan="4">Labels&nbsp;
																	</td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Product ID:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="ProductCode" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Product Name:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="ProductName" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Descriptions:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="Description" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Price:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="Price" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Sale Price:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="SalePrice" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<%-- Tee 9/17/2007 clearance activation --%>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Clearance Price:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="txtClearance" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<%-- end Tee --%>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Category Name Singular:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="Category" runat="server"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Category Name Plural:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="CategoryPlural" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Manufacturer Name Singular:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="Manufacturer" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="13"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%" height="13">Manufacturer Name 
																		Plural:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%" height="13"><asp:textbox id="ManufacturerPlural" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="13"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Vendor Name Singular:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="Vendor" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Vendor Name Plural:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="VendorPlural" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">More Info:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="MoreInfo" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Volume Pricing:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="VolumePrice" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr id="trStockStatus" runat="server">
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="right" width="50%">Stock Status:&nbsp;
																	</td>
																	<td class="Content" align="left" width="50%"><asp:textbox id="Stock" runat="server" MaxLength="50"></asp:textbox></td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td align="right">
															<asp:ImageButton id="cmdSave" runat="server" ImageUrl="images/save.jpg"></asp:ImageButton>
														</td>
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

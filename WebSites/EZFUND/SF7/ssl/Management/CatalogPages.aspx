<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="GeneralControl" Src="Controls/GeneralControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CatalogPages.aspx.vb" Inherits="StoreFront.StoreFront.CatalogPages" %>
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
		<script language="javascript">
		function Validate(){
			if (!document.Form2.chkProductID.checked){
				if (!document.Form2.chkProductName.checked){
					if (!document.Form2.chkSmallImage.checked){
						if (!document.Form2.chkShortDescription.checked){
							alert('One of the following must be selected: Product ID, Product Name, Small Image or Short Description.');
							return false;
						}	
					}	
				}	
			}
		}
		</script>
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
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/catalog_pages.asp  ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button -->
								</td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End -->
								</td>
							</tr>
							<tr>
								<td class="content" align="center">
									<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></P>
								</td>
							</tr>
							<tr>
								<td class="content" vAlign="top" align="center">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
											<td class="ContentTable" colSpan="6" height="1">
												<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr id="Tr2" runat="server">
														<td class="ContentTableHeader" align="left" width="100%" colSpan="10"></td>
													</tr>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center"><A class="content" href="CatalogGeneral.aspx">General</A></td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center">
															<a class="content" href="HomePage.aspx">Home Page</a>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>														
														<td class="Content" align="center"><b>Catalog Pages</b></td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center"><A class="content" href="ProductPages.aspx">Product 
																Pages</A></td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center"><A class="content" href="ManageLabels.aspx">Labels</A></td>
													</tr>
													<TR id="Tr1" runat="server">
														<TD class="ContentTableHeader" align="left" width="100%" colSpan="10"></TD>
													</TR>
												</table>
											</td>
											<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<TR>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<TD class="content" vAlign="top" align="center" colSpan="6">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="center">
															<table class="ContentTable" cellSpacing="0" cellPadding="0" width="100%" align="center">
																<tr>
																	<td class="ContentTableHeader" align="center" colSpan="3">Catalog Page Template
																	</td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<td class="Content" align="center" width="100%"><BR>
																		<table width="100%" align="center" border="0">
																			<tr align="center">
																				<td><asp:image id="Image1" runat="server" ImageUrl="images/catalog1.jpg"></asp:image></td>
																				<td><asp:image id="Image2" runat="server" ImageUrl="images/catalog2.jpg"></asp:image></td>
																				<td><asp:image id="Image3" runat="server" ImageUrl="images/catalog3.jpg"></asp:image></td>
																			</tr>
																			<tr align="center">
																				<td><asp:radiobutton id="rbtemplate1" runat="server" CssClass="Content" GroupName="CatTemplates" Text="Template 1"
																						AutoPostBack="True"></asp:radiobutton></td>
																				<td><asp:radiobutton id="rbTemplate2" runat="server" CssClass="Content" GroupName="CatTemplates" Text="Template 2"
																						AutoPostBack="True"></asp:radiobutton></td>
																				<td><asp:radiobutton id="rbTemplate3" runat="server" CssClass="Content" GroupName="CatTemplates" Text="Template 3"
																						AutoPostBack="True"></asp:radiobutton></td>
																			</tr>
																		</table>
																		<BR>
																		Configure the following to determine how many products are displayed per page<BR>
																		<table>
																			<tr>
																				<td class="Content" align="right">Number of Rows per Page:
																				</td>
																				<td align="left"><asp:textbox id="txtRows" runat="server" Columns="2"></asp:textbox></td>
																			</tr>
																			<tr>
																				<td class="Content" align="right"><asp:Label Runat="server" ID="lblProdPerRow">Number of&nbsp;Products per Row:</asp:Label>
																				</td>
																				<td align="left"><asp:textbox id="txtProducts" runat="server" Columns="2"></asp:textbox></td>
																			</tr>
																			<tr>
																				<td class="Content" align="right"><asp:Label Runat="server" ID="lblAlignment">Image Alignment:</asp:Label>
																				<td class="Content" align="center">
																					<asp:DropDownList Runat="server" ID="ddlAlignment">
																						<asp:ListItem Value="0">Left</asp:ListItem>
																						<asp:ListItem Value="1">Right</asp:ListItem>
																						<%--<asp:ListItem Value="2">Center</asp:ListItem>--%>
																						<asp:ListItem Value="3">Alternating</asp:ListItem>
																					</asp:DropDownList>
																				</td>
																			</tr>
																		</table>
																	</td>
																	<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
															</table>
															<BR>
															<TABLE class="ContentTable" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
																<TR>
																	<TD class="ContentTableheader" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="ContentTableheader">&nbsp;</TD>
																	<TD class="ContentTableHeader">
																		Product Elements to Display
																	</TD>
																	<TD class="ContentTableheader">&nbsp;</TD>
																	<TD class="ContentTableheader" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="Content" align="right"><asp:checkbox id="chkProductID" runat="server" Text="Product ID:" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="center">&nbsp;
																		<asp:checkbox id="chkPCodeLink" runat="server" Text="Link to Product Detail" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="left">&nbsp;</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="Content" align="right"><asp:checkbox id="chkProductName" runat="server" Text="Product Name:" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="center">&nbsp;
																		<asp:checkbox id="chkPNameLink" runat="server" Text="Link to Product Detail" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="left">&nbsp;</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<asp:Panel ID="pnlSmallImage" Runat="server">
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:checkbox id="chkSmallImage" runat="server" Text="Small Image:" TextAlign="Left"></asp:checkbox></TD>
																		<TD class="Content" align="center">&nbsp;
																			<asp:checkbox id="chkSImageLink" runat="server" Text="Link to Product Detail" TextAlign="Left"></asp:checkbox></TD>
																		<TD class="Content" align="left">&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																</asp:Panel>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="Content" align="right"><asp:checkbox id="chkAddToCart" runat="server" Text="Add to Cart:" TextAlign="Left" AutoPostBack="True"></asp:checkbox></TD>
																	<TD class="Content" align="center"><asp:checkbox id="chkDisplayQty" runat="server" Text="Display Quantity" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="left">&nbsp;</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<asp:Panel ID="pnlShortDescription" Runat="server">
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:checkbox id="chkShortDescription" runat="server" Text="Short Description:" TextAlign="Left"></asp:checkbox></TD>
																		<TD class="Content" align="right">&nbsp;
																		</TD>
																		<TD class="Content" align="left">&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																</asp:Panel>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="Content" align="right"><asp:checkbox id="chkPrice" runat="server" Text="Price:" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="right">&nbsp;
																	</TD>
																	<TD class="Content" align="left">&nbsp;</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<asp:Panel ID="pnlNotTemplate2" Runat="server">
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:checkbox id="chkManufacturer" runat="server" Text="Manufacturer:" TextAlign="Left"></asp:checkbox></TD>
																		<TD class="Content" align="right">&nbsp;
																		</TD>
																		<TD class="Content" align="left">&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:checkbox id="chkVendor" runat="server" Text="Vendor:" TextAlign="Left"></asp:checkbox></TD>
																		<TD class="Content" align="right">&nbsp;
																		</TD>
																		<TD class="Content" align="left">&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:checkbox id="chkLabels" runat="server" Text="Labels:" TextAlign="Left"></asp:checkbox></TD>
																		<TD class="Content" align="right">&nbsp;
																		</TD>
																		<TD class="Content" align="left">&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																</asp:Panel>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="Content" align="right"><asp:checkbox id="chkEmailAFriend" runat="server" Text="E-Mail a Friend:" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="center">&nbsp;Attributes Display as:
																		<asp:radiobuttonlist id="rblAttributeType" runat="server" CssClass="Content" RepeatColumns="2" RepeatDirection="Horizontal">
																			<asp:ListItem Value="1">Radio Buttons</asp:ListItem>
																			<asp:ListItem Value="0">Drop-Downs</asp:ListItem>
																		</asp:radiobuttonlist></TD>
																	<TD class="Content" align="left">&nbsp;</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="Content" align="right"><asp:checkbox id="chkWishList" runat="server" Text="Wish List:" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="right">&nbsp;
																	</TD>
																	<TD class="Content" align="left">&nbsp;</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="Content" align="right"><asp:checkbox id="chkStockInfo" runat="server" Text="Stock Info:" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="right">&nbsp;
																	</TD>
																	<TD class="Content" align="left">&nbsp;</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="Content" align="right"><asp:checkbox id="chkDetailLink" runat="server" Text="Product Detail Link:" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="right">&nbsp;
																	</TD>
																	<TD class="Content" align="left">&nbsp;</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="Content" align="right"><asp:checkbox id="chkVolumePricing" runat="server" Text="Volume Pricing:" TextAlign="Left"></asp:checkbox></TD>
																	<TD class="Content" align="right">&nbsp;
																	</TD>
																	<TD class="Content" align="left">&nbsp;</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<tr>
																	<td class="ContentTable" colSpan="5" width="1"><IMG src="images/clear.gif"></td>
																</tr>
															</TABLE>
															<table align="right">
																<tr>																	
																	<br><td align="right"><asp:imagebutton id="cmdSave" runat="server" ImageUrl="images/save.jpg"></asp:imagebutton></td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</TD>
											<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
										</TR>
										<tr>
											<td class="ContentTable" height="1" colspan="8"><IMG src="images/clear.gif"></td>
										</tr>
									</table>
									<!-- Content End --></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

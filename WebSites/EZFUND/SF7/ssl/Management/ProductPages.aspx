<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductPages.aspx.vb" Inherits="StoreFront.StoreFront.ProductPages" %>
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
		<script language="javascript">
		function Validate(){
			
			if (!document.Form2.chkProductID.checked){
				if (!document.Form2.chkProductName.checked){
					if (!document.Form2.chkSmallImage.checked){
						if (!document.Form2.chkDescription.checked){
							alert('One of the following must be selected: Product ID, Product Name, Small Image or Short Description.');
							return false;
						}	
					}	
				}	
			}
			return ValidateRecommendedItems();
		}
		
		function ValidateRecommendedItems(){
		
			if (document.Form2.chkDisplayRecommendedItems.checked)
			{
				if (!document.Form2.chkRecommendedProductCode.checked){
					if (!document.Form2.chkRecommendedProductName.checked){
						if (!document.Form2.chkRecommendedSmallImage.checked){
							if (!document.Form2.chkRecommendedShortDescription.checked){
								alert('One of the following must be selected from the Recommended Items: Product ID, Product Name, Small Image or Short Description.');
								return false;
							}	
						}	
					}	
				}
			}
		}	
				
		</script>
	</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colSpan="3"><uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner></td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3"><uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Layout Templates"></uc1:TopSubBanner></td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End -->
					</td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/catalog_product.asp  ')">
										<img src="images/help.jpg" border="0"></A> 
									<!-- End Help Button -->
								</td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End -->
								</td>
							</tr>
							<tr>
								<td class="content" align="center">
									<P id="ErrorAlignment" runat="server" align="center">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</P>
								</td>
							</tr>
							<TR>
								<TD class="content" vAlign="top" align="center">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></td>
											<td class="ContentTable" colSpan="6" height="1">
												<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR id="Tr1" runat="server">
														<TD class="ContentTableHeader" align="left" colSpan="17" width="100%"></TD>
													</TR>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center">
															<a class="content" href="CatalogGeneral.aspx">General</a>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center">
															<a class="content" href="HomePage.aspx">Home Page</a>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center">
															<a class="content" href="CatalogPages.aspx">Catalog Pages</a>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center">
															<b>Product Pages</b>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="center">
															<a class="content" href="ManageLabels.aspx">Labels</a>
														</td>
													</tr>
													<TR id="Tr2" runat="server">
														<TD class="ContentTableHeader" align="left" colSpan="17" width="100%"></TD>
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
															<table class="ContentTable" width="100%" cellpadding="0" cellspacing="0" align="center">
																<tr>
																	<td class="ContentTableHeader" colspan="3" align="center">
																		Product Page Template
																	</td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="1" width="1"><IMG src="images/clear.gif"></td>
																	<td class="Content" width="100%" align="center"><BR>
																		<table align="center" border="0" width="100%">
																			<tr align="center">
																				<td>
																					<asp:Image id="Image1" runat="server" ImageUrl="images/detail1.jpg"></asp:Image>
																				</td>
																				<td>
																					<asp:Image id="Image2" runat="server" ImageUrl="images/detail2.jpg"></asp:Image>
																				</td>
																				<td>
																				</td>
																			</tr>
																			<tr align="center">
																				<td>
																					<asp:RadioButton id="rbtemplate1" runat="server" CssClass="Content" Text="Template 1" GroupName="CatTemplates"
																						AutoPostBack="True"></asp:RadioButton>
																				</td>
																				<td>
																					<asp:RadioButton id="rbTemplate2" runat="server" CssClass="Content" Text="Template 2" GroupName="CatTemplates"
																						AutoPostBack="True"></asp:RadioButton>
																				</td>
																				<td>
																				</td>
																			</tr>
																		</table>
																	</td>
																	<td class="ContentTable" colSpan="1" width="1"><IMG src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
																</tr>
															</table>
															<BR>
															<TABLE class="ContentTable" id="Table1" cellSpacing="0" cellPadding="0" width="100%">
																<TBODY>
																	<TR>
																		<TD class="ContentTableHeader" colSpan="5">Product Elements to Display
																		</TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="chkProductID" runat="server" Text="Product ID:" TextAlign="Left"></asp:CheckBox></TD>
																		<TD class="Content" align="right">&nbsp;</TD>
																		<TD class="Content" align="left">
																			&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="chkProductName" runat="server" Text="Product Name:" TextAlign="Left"></asp:CheckBox></TD>
																		<TD class="Content" align="right">&nbsp;</TD>
																		<TD class="Content" align="left">
																			&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="chkSmallImage" runat="server" Text="Image:" TextAlign="Left"></asp:CheckBox></TD>
																		<TD class="Content" width="66%" align="center">&nbsp;Image Type:
																			<asp:RadioButton id="rbSmallImage" runat="server" GroupName="imagetype" Text="Small"></asp:RadioButton>
																			<asp:RadioButton id="rbLargeImage" runat="server" GroupName="imagetype" Text="Large"></asp:RadioButton></TD>
																		<TD class="Content" align="left">
																			&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<tr>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="chkDescription" runat="server" Text="Description" TextAlign="Left"></asp:CheckBox></TD>
																		<TD class="Content" width="66%" align="center">Description Type:&nbsp;
																			<asp:RadioButton id="rbShortDescription" runat="server" GroupName="descriptiontype" Text="Short"></asp:RadioButton>
																			<asp:RadioButton id="rbLargeDescription" runat="server" GroupName="descriptiontype" Text="Long"></asp:RadioButton>
																		</TD>
																		<TD class="Content" align="left">
																			&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</tr>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="chkAddToCart" runat="server" Text="Add To Cart:" TextAlign="Left" AutoPostBack="True"></asp:CheckBox></TD>
																		<TD class="Content" align="center">
																			<asp:CheckBox id="chkDisplayQty" runat="server" Text="Display Quantity:" TextAlign="Left"></asp:CheckBox></TD>
																		<TD class="Content" align="left">
																			&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="chkPrice" runat="server" Text="Price:" TextAlign="Left"></asp:CheckBox></TD>
																		<TD class="Content" align="right">&nbsp;
																		</TD>
																		<TD class="Content" align="left">
																			&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="ChkCategory" runat="server" Text="Category:" TextAlign="Left"></asp:CheckBox></TD>
																		<TD class="Content" align="right">&nbsp;
																		</TD>
																		<TD class="Content" align="left">
																			&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="chkManufacturer" runat="server" Text="Manufacturer:" TextAlign="Left"></asp:CheckBox></TD>
																		<TD class="Content" align="right">&nbsp;
																		</TD>
																		<TD class="Content" align="left">
																			&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="chkVendor" runat="server" Text="Vendor:" TextAlign="Left"></asp:CheckBox></TD>
																		<TD class="Content" align="right">&nbsp;
																		</TD>
																		<TD class="Content" align="left">
																			&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="chkLabels" runat="server" Text="Labels:" TextAlign="Left"></asp:CheckBox></TD>
																		<TD class="Content" align="right">&nbsp;
																		</TD>
																		<TD class="Content" align="left">
																			&nbsp;</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																		<TD class="Content" align="right">
																			<asp:CheckBox id="chkEmailAFriend" runat="server" Text="E-Mail a Friend:" TextAlign="Left"></asp:CheckBox></TD>
														</td>
														<TD class="Content" width="66%" align="center">&nbsp;Attributes Display as:
															<asp:RadioButtonList id="rblAttributeType" runat="server" CssClass="Content" RepeatDirection="Horizontal"
																RepeatColumns="2">
																<asp:ListItem Value="1">Radio Buttons</asp:ListItem>
																<asp:ListItem Value="0">Drop-Downs</asp:ListItem>
															</asp:RadioButtonList>
														</TD>
														<TD class="Content" align="left">
															&nbsp;</TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
													</tr>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
														<TD class="Content" align="right">
															<asp:CheckBox id="chkWishList" runat="server" Text="Wish List:" TextAlign="Left"></asp:CheckBox></TD>
														<TD class="Content" align="right">&nbsp;
														</TD>
														<TD class="Content" align="left">
															&nbsp;</TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
														<TD class="Content" align="right">
															<asp:CheckBox id="chkVolumePricing" runat="server" Text="Volume Pricing:" TextAlign="Left"></asp:CheckBox></TD>
														<TD class="Content" align="right">&nbsp;
														</TD>
														<TD class="Content" align="left">
															&nbsp;</TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
														<TD class="Content" align="right">
															<asp:CheckBox id="chkStockInfo" runat="server" Text="Stock Info:" TextAlign="Left"></asp:CheckBox></TD>
														<TD class="Content" align="right">&nbsp;
														</TD>
														<TD class="Content" align="left">
															&nbsp;</TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
														<TD class="Content" align="right"></TD>
														<TD class="Content" align="right">&nbsp;
														</TD>
														<TD class="Content" align="left">
															&nbsp;</TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
													</TR>
													<tr>
														<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</tr>
												</table>
												<br>
												<TABLE class="ContentTable" id="tblRecommededItems" cellSpacing="0" cellPadding="0" width="100%">
													<TR>
														<TD class="ContentTable" colSpan="1" width="1"><IMG src="images/clear.gif"></TD>
														<TD class="ContentTableHeader" colSpan="3">Recommended Items
														</TD>
														<TD class="ContentTable" colSpan="1" width="1"><IMG src="images/clear.gif"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
														<TD class="Content" align="right">
															<asp:CheckBox id="chkDisplayRecommendedItems" runat="server" Text="Display Recommended Items"
																TextAlign="Left"></asp:CheckBox></TD>
														<TD class="Content" width="66%" align="center" colspan="2">Labels:
															<asp:TextBox id="txtRecommendedTitle" runat="server" Text="Recommended Items:"></asp:TextBox></TD>
														<TD class="ContentTableheight=" colSpan="1"><IMG height="1" src="images/clear.gif"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
														<TD class="Content" align="right">
															<asp:CheckBox id="chkRecommendedProductCode" runat="server" Text="Product ID:" TextAlign="Left"></asp:CheckBox></TD>
														<TD class="Content" width="66%" align="center" colspan="2">
															<asp:CheckBox ID="chkProductIdLink" Runat="server" Text="Link (Product ID) To Product Detail:"
																TextAlign="Left"></asp:CheckBox>
														</TD>
														<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
														<TD class="Content" align="right">
															<asp:CheckBox id="chkRecommendedProductName" runat="server" Text="Product Name:" TextAlign="Left"></asp:CheckBox></TD>
														<TD class="Content" width="66%" align="center" colspan="2">
															<asp:CheckBox ID="chkProductNameLink" Runat="server" Text="Link (Product Name) To Product Detial:"
																TextAlign="Left"></asp:CheckBox>
														</TD>
													</TR>
													<TR>
														<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
														<TD class="Content" align="right">
															<asp:CheckBox id="chkRecommendedSmallImage" runat="server" Text="Small Image:" TextAlign="Left"></asp:CheckBox></TD>
														<TD class="Content" width="66%" align="center" colspan="2">
															<asp:CheckBox ID="chkLinkSmallImage" Runat="server" Text="Link (Small Image) To Product Detail"
																TextAlign="Left"></asp:CheckBox>
														</TD>
														<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
														<TD class="Content" align="right">
															<asp:CheckBox id="chkRecommendedShortDescription" runat="server" Text="Short Description:" TextAlign="Left"></asp:CheckBox></TD>
														<TD class="Content" align="center" colspan="2">
															<%-- Tee 11/07/2007 added additional configuration --%>
															Items per row:&nbsp;
															<asp:DropDownList ID="ddlItemPerRow" runat="server">
																<asp:ListItem Text="3" Value="3"></asp:ListItem>
																<asp:ListItem Text="5" Value="5"></asp:ListItem>
															</asp:DropDownList>
															<%-- end Tee --%>
														</TD>
														<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
														<TD class="Content" align="right">
															<asp:CheckBox id="chkRecommendedPrice" runat="server" Text="Price:" TextAlign="Left"></asp:CheckBox></TD>
														<TD class="Content" align="right">&nbsp;
														</TD>
														<TD class="Content" align="left">
															&nbsp;</TD>
														<TD class="ContentTable" colSpan="1" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</TR>
													<tr>
														<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</tr>
												</TABLE>
												<table align="right">
													<tr>
														<td><br>
															<asp:ImageButton id="cmdSave" runat="server" ImageUrl="images/save.jpg"></asp:ImageButton>
														</td>
													</tr>
												</table>
											</TD>
										</TR>
									</table>
								</TD>
								<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
							</TR>
							<tr>
								<td class="ContentTable" colSpan="8" height="1"><IMG height="1" src="images/clear.gif"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!-- Content End --> </TD></TR></TBODY></TABLE></TD></TR></TABLE>
		</form>
		</TR></TABLE></TR></TABLE></TR></TABLE></TR></TABLE></TR></TABLE></FORM>
	</body>
</HTML>

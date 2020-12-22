<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="Controls/AddressLabel.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OrderDetail.aspx.vb" Inherits="StoreFront.StoreFront.OrderDetail"  TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0"%>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Order Detail</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.1

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
	</HEAD>
	<body class="generalpage" id="BodyTag" runat="server">
		<form id="Form2" method="post" runat="server">
			<input id="myhiddenfield" type="hidden" name="myhiddenfield" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1"
							runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start --><uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
									<!-- Top Banner End --></td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start --><uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
									<!-- Top Sub Banner End --></td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell">
									<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
									<!-- Left Column End --></td>
								<td class="Content" id="ContentCell" vAlign="top">
									<!-- Content Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<p class="PageNavigation">
										<a href="CustEdit.aspx">Edit My Profile</a> <span>|</span>
										<a href="OrderHistory.aspx">View Order Status and History</a> <span>|</span>
										<a href="SavedCart.aspx">Access My Wish List</a> <span>|</span>
										<a href="CustAddressBook.aspx">Manage Address Book</a>
									</p>
									<h1 class="Headings">Order Detail</h1>
									<P id="ErrorAlignment" runat="server" align="center"><asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
									<P id="P1" runat="server" align="center"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
												<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<td class="ContentTableHeader">&nbsp;</td>
														<TD class="ContentTableHeader" noWrap align="left" width="50%">Order ID:
															<asp:label id="lblOrderID" runat="server" CssClass="ContentTableHeader">Label</asp:label></TD>
														<TD class="ContentTableHeader" noWrap align="right" width="50%">Order Date:
															<asp:label id="lblOrderDate" runat="server" CssClass="ContentTableHeader">Label</asp:label></TD>
														<td class="ContentTableHeader">&nbsp;</td>
													</TR>
												</TABLE>
												<br>
												<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
														<TD class="ContentTableHeader">&nbsp;</TD>
														<TD class="ContentTableHeader" colSpan="3">Billing Information</TD>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
													</TR>
													<TR>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
														<TD class="Content" colSpan="4">&nbsp;</TD>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
													</TR>
													<TR>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
														<TD class="Content">&nbsp;</TD>
														<TD class="Content" vAlign="top" width="50%"><uc1:addresslabel id="BillingAddress" runat="server"></uc1:addresslabel><br>
															<asp:label id="lblEmail" CssClass="content" Runat="server"></asp:label></TD>
														<TD><IMG height="1" src="images/clear.gif" width="10"></TD>
														<TD class="Content" vAlign="top" width="50%">
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td class="Content">Payment Information</td>
																</tr>
																<tr>
																	<td class="Content">&nbsp;</td>
																</tr>
																<tr>
																	<td class="Content"><asp:label id="lblPaymentMethod" runat="server"></asp:label></td>
																</tr>
															</table>
														</TD>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
													</TR>
													<TR>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
														<TD class="Content" colSpan="4">&nbsp;</TD>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
													</TR>
													<TR>
														<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
														<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
														<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
													</TR>
												</TABLE>
												<br>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TBODY>
														<tr>
															<td colSpan="3"><asp:datalist id="Datalist2" runat="server" Width="100%">
																	<FooterTemplate>
																		<table border="0" cellpadding="0" cellspacing="0" width="100%">
																			<tr>
																				<td class="ContentTable" height="1"><IMG src="images/clear.gif" width="1" height="1"></td>
																			</tr>
																		</table>
																	</FooterTemplate>
																	<ItemTemplate>
																		<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<TR>
																				<TD vAlign="top" width="50%">
																					<TABLE id="Table10" cellSpacing="0" cellPadding="0" width="100%" border="0">
																						<TR>
																							<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																							<TD class="ContentTableHeader">&nbsp;</TD>
																							<TD class="ContentTableHeader">Ship To:
																								<asp:Label id="Label2" runat="server" CssClass="ContentTableHeader">
																									<%# DataBinder.Eval(Container.DataItem.Address,"NickName") %>
																								</asp:Label></TD>
																							<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						</TR>
																						<TR>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																							<TD class="Content">&nbsp;</TD>
																							<TD class="Content">&nbsp;</TD>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						</TR>
																						<TR>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																							<TD class="Content">&nbsp;</TD>
																							<TD class="Content">
																								<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																									<TR>
																										<TD class="Content" colSpan="2">
																											<asp:TextBox id=txtShipID Runat="server" Text='<%# DataBinder.Eval(Container.DataItem.Address,"ID") %>' Visible="False">
																											</asp:TextBox>
																											<uc1:AddressLabel id=Addresslabel1 runat="server" AddressSource='<%# DataBinder.Eval(Container.DataItem,"Address") %>'>
																											</uc1:AddressLabel></TD>
																									</TR>
																									<TR>
																										<TD class="Content" colSpan="2">&nbsp;</TD>
																									</TR>
																									<TR>
																										<TD class="Content" noWrap>
																											<asp:Label id="ShipMethod" Runat="server"></asp:Label></TD>
																										<TD class="Content" align="right">
																											<asp:LinkButton ID="BtnTrack" onclick=BtnTrackClick Runat="server" CommandArgument='<%# "OrderID=" & DataBinder.Eval(Me, "OrderID") & "&AddressID=" & DataBinder.Eval(Container.DataItem.Address,"ID") %>'>
																												<asp:Image BorderWidth="0" ID="imgTrack" Runat="server" AlternateText="Track"></asp:Image>
																											</asp:LinkButton>
																										</TD>
																									</TR>
																								</TABLE>
																							</TD>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						</TR>
																						<TR>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																							<TD class="Content">&nbsp;</TD>
																							<TD class="Content">&nbsp;</TD>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						</TR>
																						<TR>
																							<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
																							<TD class="ContentTable" style="HEIGHT: 1px" colSpan="2"><IMG height="1" src="images/clear.gif" width="1"></TD>
																							<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
																						</TR>
																					</TABLE>
																					<BR>
																					<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																						<TR>
																							<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																							<TD class="ContentTableHeader">&nbsp;</TD>
																							<TD class="ContentTableHeader">Special Instructions</TD>
																							<TD class="ContentTableHeader">&nbsp;</TD>
																							<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						</TR>
																						<TR>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																							<TD class="Content" colSpan="3">&nbsp;</TD>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						</TR>
																						<TR>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																							<TD class="Content">&nbsp;</TD>
																							<TD class="Content" align="left">
																								<asp:Label id="lblSpecialInstruction" Runat="server">
																									<%# DataBinder.Eval(Container.DataItem.Address,"Instructions") %>
																								</asp:Label></TD>
																							<TD class="Content">&nbsp;</TD>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						</TR>
																						<TR>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																							<TD class="Content" colSpan="3">&nbsp;</TD>
																							<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						</TR>
																						<TR>
																							<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
																							<TD class="ContentTable" style="HEIGHT: 1px" colSpan="2"><IMG height="1" src="images/clear.gif" width="1"></TD>
																							<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
																						</TR>
																					</TABLE>
																					<BR>
																					<cc1:DownloadDisplay id=DownloadDisplay1 runat="server" TableBorderStyle="ContentTable" HeadingClass="ContentTableHeader" DownloadDate='<%# DataBinder.Eval(Me, "DownloadDate") %>' OrderID='<%# DataBinder.Eval(Me, "OrderID") %>' DataSource='<%# DataBinder.Eval(Container.DataItem,"OrderItems") %>' ItemClass="Content" HeadingLabel="Download(s)">
																					</cc1:DownloadDisplay></TD>
																				<TD><IMG height="1" src="images/clear.gif" width="10"></TD>
																				<TD vAlign="top" width="50%">
																					<cc1:DynamicCartDisplay id=DynamicCartDisplay2 runat="server" Width="100%" HeadingClass="ContentTableHeader" DataSource='<%# DataBinder.Eval(Container.DataItem,"OrderItems") %>' GiftWrapDetail="True" DisplayGiftWrapRow="True" TotalColumnDisplay="False" OptionsColumnDisplay="true" ReOrderBtnDisplay="True" RemoveBtnDisplay="False" BuyNowBtnDisplay="False" GiftWrapBtnDisplay="False" BundledProductsDisplay="True" SavedCartBtnDisplay="False" StatusColumnDisplay="False" DesignCount="2" BorderClass="ContentTable" HorizontalClass="ContentTableHorizontal" OptionsLabel="Options" PriceLabel="Price" ProductLabel="Product" QuantityLabel="Qty" StatusLabel="Status" TotalLabel="Total" >
																					</cc1:DynamicCartDisplay><BR>
																					<BR>
																					<cc1:TotalDisplay id="ShipmentTotalDisplay1" runat="server" HandlingTotalLabel="Handling:" ShippingTotalLabel="Shipping:"
																						SubTotalLabel="Subtotal:" HorizontalBorderStyle="ContentTableHorizontal" DisplayPaymentMethod="False"
																						TableBorderStyle="ContentTable" HeadingClass="ContentTableHeader" DisplayOrderTotal="False" DisplayTaxShipNotIncluded="False"
																						HeadingString="Shipment Total" ShipmentTotalLabel="Shipment Total:" ShipmentTotalStyle="subHeadings"
																						DisplayGrandTotal="False" DisplayGiftCertificateTotal="False"></cc1:TotalDisplay></TD>
																			</TR>
																		</TABLE>
																	</ItemTemplate>
																	<SeparatorTemplate>
																		<table border="0" cellpadding="0" cellspacing="0" width="100%">
																			<tr>
																				<td class="ContentTable" height="1"><IMG src="images/clear.gif" width="1" height="1"></td>
																			</tr>
																		</table>
																	</SeparatorTemplate>
																</asp:datalist></td>
														</tr>
														<tr>
															<td class="Content" colSpan="3">&nbsp;</td>
														</tr>
														<tr>
<%-- begin: JDB - Advanced Coupon Functionality --%>
												<td class="Content" width="50%" valign="top">
												<asp:repeater id="rDiscounts" runat="server">
													<HeaderTemplate>
													<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="ContentTableHeader">&nbsp;</TD>
															<TD class="ContentTableHeader" colspan="2">Coupons and Discounts</TD>
															<TD class="ContentTableHeader">&nbsp;</TD>
															<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="Content" colSpan="4">&nbsp;</TD>
															<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
													</HeaderTemplate>
													<ItemTemplate>
														<tr>
															<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="Content">&nbsp;</TD>
															<TD class="Content" align="left"><%#DataBinder.Eval(Container.DataItem,"Description")%></td>
															<TD class="Content" style="text-align:right"><%#Format(HandleDiscountAmount(DataBinder.Eval(Container.DataItem,"DiscountAmount")), "c")%>&nbsp;&nbsp;&nbsp;</TD>
															<TD class="Content">&nbsp;</TD>
															<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
													</ItemTemplate>
													<FooterTemplate>
														<TR>
															<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="Content" colSpan="4">&nbsp;</TD>
															<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="Content">&nbsp;</TD>
															<TD class="subHeadings" align="left">Total:</td>
															<TD class="subHeadings" style="text-align:right"><%#Format(Me.DiscountTotal, "c")%>&nbsp;&nbsp;&nbsp;</TD>
															<TD class="Content">&nbsp;</TD>
															<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="Content" colSpan="4">&nbsp;</TD>
															<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
															<TD class="ContentTable" style="HEIGHT: 1px" colSpan="4"><IMG height="1" src="images/clear.gif" width="1"></TD>
															<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
														</TR>
													</TABLE>
													</FooterTemplate>
												</asp:repeater>
												</td>
												<td class="Content"><IMG height="1" src="images/clear.gif" width="10"></td>
												<td class="Content" align="right" width="50%" valign="top">
<%-- end: JDB - Advanced Coupon Functionality --%>
															<cc1:totaldisplay id="TotalDisplay1" runat="server" HandlingTotalLabel="Handling:" ShippingTotalLabel="Shipping:"
																	SubTotalLabel="Subtotal:" HorizontalBorderStyle="ContentTableHorizontal" DisplayPaymentMethod="False" TableBorderStyle="ContentTable" HeadingClass="ContentTableHeader"
																	GrandTotalStyle="subHeadings" DisplaySubTotal="False" DisplayStateTaxTotal="False" DisplayShippingTotal="False" DisplayMerchandiseTotal="False"
																	DisplayLocalTaxTotal="False" DisplayHandlingTotal="False" DisplayDiscountTotal="False" DisplayCountryTaxTotal="False" DisplayOrderTotal="False"
																	DisplayTaxShipNotIncluded="False"></cc1:totaldisplay></td>
														</tr>
													</TBODY></table>

									<!-- Content End --></td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start --><uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
									<!-- Right Column End --></td>
							</tr>
							<tr>
								<td class="Footer" id="FooterCell" colSpan="3">
									<!-- Footer Start --><uc1:footer id="Footer1" runat="server"></uc1:footer>
									<!-- Footer End --></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

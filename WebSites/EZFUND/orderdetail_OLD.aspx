<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OrderDetail.aspx.vb" Inherits="StoreFront.StoreFront.OrderDetail"  TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0"%>
<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="Controls/AddressLabel.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Order Detail</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.1.0

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
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
	</HEAD>
	<body id="BodyTag" runat="server" class="generalpage">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1"
							runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start -->
									<uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
									<!-- Top Banner End -->
								</td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start -->
									<uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
									<!-- Top Sub Banner End -->
								</td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell">
									<!-- Left Column Start -->
									<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
									<!-- Left Column End -->
								</td>
								<td class="Content" vAlign="top" id="ContentCell">
									<!-- Content Start -->
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="Instructions">
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="content">
												<P id="ErrorAlignment" runat="server" align="center">
													<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label>
												</P>
												<P id="MessageAlignment" runat="server" align="center">
													<asp:Label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:Label>
												</P>
											</td>
										</tr>
										<tr>
											<td class="Content">
												<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<td class="ContentTableHeader">&nbsp;</td>
														<TD class="ContentTableHeader" align="left" width="50%" nowrap>Order ID:
															<asp:label id="lblOrderID" runat="server" CssClass="ContentTableHeader">Label</asp:label></TD>
														<TD class="ContentTableHeader" align="right" width="50%" nowrap>Order Date:
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
														<TD class="Content" vAlign="top" width="50%"><uc1:addresslabel id="BillingAddress" runat="server"></uc1:addresslabel>
															<br>
															<asp:Label Runat="server" ID="lblEmail" CssClass="content"></asp:Label>
														</TD>
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
																	<td class="Content">
																		<asp:Label id="lblPaymentMethod" runat="server"></asp:Label></td>
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
															<td colSpan="3">
																<asp:datalist id="Datalist2" runat="server" Width="100%">
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
																					<cc1:DynamicCartDisplay id=DynamicCartDisplay2 runat="server" Width="100%" HeadingClass="ContentTableHeader" DataSource='<%# DataBinder.Eval(Container.DataItem,"OrderItems") %>' GiftWrapDetail="True" DisplayGiftWrapRow="True" TotalColumnDisplay="False" OptionsColumnDisplay="true" ReOrderBtnDisplay="True" StatusColumnDisplay="False" DesignCount="2" BorderClass="ContentTable" HorizontalClass="ContentTableHorizontal" OptionsLabel="Options" PriceLabel="Price" ProductLabel="Product" QuantityLabel="Qty" StatusLabel="Status" TotalLabel="Total">
																					</cc1:DynamicCartDisplay><BR>
																					<BR>
																					<cc1:TotalDisplay id="ShipmentTotalDisplay1" runat="server" HandlingTotalLabel="Handling:" ShippingTotalLabel="Shipping:"
																						SubTotalLabel="Subtotal:" HorizontalBorderStyle="ContentTableHorizontal" DisplayPaymentMethod="False"
																						TableBorderStyle="ContentTable" HeadingClass="ContentTableHeader" DisplayOrderTotal="False" DisplayTaxShipNotIncluded="False"
																						HeadingString="Shipment Total" ShipmentTotalLabel="Shipment Total:" ShipmentTotalStyle="Headings"
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
																</asp:datalist>
															</td>
														</tr>
														<tr>
															<td class="Content" colSpan="3">&nbsp;</td>
														</tr>
														<tr>
															<td class="Content" width="50%">&nbsp;</td>
															<td class="Content"><IMG height="1" src="images/clear.gif" width="10"></td>
															<td class="Content" align="right" width="50%"><cc1:totaldisplay id="TotalDisplay1" runat="server" DisplayTaxShipNotIncluded="False" DisplayOrderTotal="False"
																	DisplayCountryTaxTotal="False" DisplayDiscountTotal="False" DisplayHandlingTotal="False" DisplayLocalTaxTotal="False" DisplayMerchandiseTotal="False"
																	DisplayShippingTotal="False" DisplayStateTaxTotal="False" DisplaySubTotal="False" GrandTotalStyle="Headings" HeadingClass="ContentTableHeader"
																	TableBorderStyle="ContentTable" DisplayPaymentMethod="False" HorizontalBorderStyle="ContentTableHorizontal" SubTotalLabel="Subtotal:" ShippingTotalLabel="Shipping:"
																	HandlingTotalLabel="Handling:"></cc1:totaldisplay></td>
														</tr>
													</TBODY>
												</table>
											</td>
										</tr>
									</table>
									<!-- Content End -->
								</td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start -->
									<uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
									<!-- Right Column End -->
								</td>
							</tr>
							<tr>
								<td colSpan="3" class="Footer" id="FooterCell">
									<!-- Footer Start -->
									<uc1:footer id="Footer1" runat="server"></uc1:footer>
									<!-- Footer End -->
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script type="text/javascript">
		var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
		document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
		</script>
		<script type="text/javascript">
		var pageTracker = _gat._getTracker("UA-1536999-1");
		pageTracker._initData();
		pageTracker._trackPageview();
		</script>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OrderHistory.aspx.vb" Inherits="StoreFront.StoreFront.OrderHistory"  TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Order History</title>
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
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
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
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<p class="PageNavigation">
										<a href="CustEdit.aspx">Edit My Profile</a> <span>|</span>
										<a href="OrderHistory.aspx">View Order Status and History</a> <span>|</span>
										<a href="SavedCart.aspx">Access My Wish List</a> <span>|</span>
										<a href="CustAddressBook.aspx">Manage Address Book</a>
									</p>
									<h1 class="Headings">My Orders</h1>
									<P id="ErrorAlignment" runat="server"><asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
									<P id="P1" runat="server"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>

												<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0" width="100%">
													<TR>
														<TD><asp:datagrid id="DataGrid1" runat="server" CellPadding="0" BorderWidth="0" Width="100%" AllowPaging="True"
																ShowHeader="False" AutoGenerateColumns="False">
																<Columns>
																	<asp:TemplateColumn>
																		<ItemTemplate>
																			<table runat="server" id="HistoryTable" border="0" cellpadding="0" cellspacing="0" width="100%">
																				<tr id="HeaderRow">
																					<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
																					<TD class="ContentTableHeader">&nbsp;</TD>
																					<TD class="ContentTableHeader" align="center" valign="middle" width="20%" nowrap>Order 
																						ID</TD>
																					<TD class="ContentTableHeader" align="center" valign="middle" width="20%" noWrap>Order 
																						Date</TD>
																					<TD class="ContentTableHeader" align="center" valign="middle" width="20%" noWrap>Order 
																						Total</TD>
																					<TD class="ContentTableHeader" align="center" valign="middle" width="20%" noWrap>Details</TD>
																					<TD class="ContentTableHeader" align="center" valign="middle" width="20%" noWrap>Shipment</TD>
																					<TD class="ContentTableHeader">&nbsp;</TD>
																					<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
																				</tr>
																				<tr>
																					<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																					<TD class="Content" colspan="7">&nbsp;</TD>
																					<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																				</tr>
																				<tr>
																					<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																					<TD class="Content">&nbsp;</TD>
																					<TD class="Content" align="center" valign="middle" width="20%" noWrap><%# DataBinder.Eval(Container.DataItem,"OrderNumber") %></TD>
																					<TD class="Content" align="center" valign="middle" width="20%" noWrap><%# DataBinder.Eval(Container.DataItem,"OrderDate") %></TD>
																					<TD class="Content" align="center" valign="middle" width="20%" noWrap><%# PriceDisplay2(DataBinder.Eval(Container.DataItem,"GrandTotal")) %></TD>
																					<TD class="Content" align="center" valign="middle" width="20%" noWrap>
																						<asp:LinkButton ID="btnViewDetail" Runat="server" OnClick=ViewDetailClick CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>'>
																							<asp:Image BorderWidth="0" ID="imgView" Runat="server" AlternateText="View"></asp:Image>
																						</asp:LinkButton>
																					</TD>
																					<TD class="Content" align="center" valign="middle" width="20%" noWrap>
																						<asp:LinkButton ID="btnTrackShipment" Runat="server" OnClick=TrackShipmentClick CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>' >
																							<asp:Image BorderWidth="0" ID="imgTrack" Runat="server" AlternateText="Track"></asp:Image>
																						</asp:LinkButton>
																					</TD>
																					<TD class="Content">&nbsp;</TD>
																					<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																				</tr>
																				<tr>
																					<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																					<TD class="Content" colspan="7">&nbsp;</TD>
																					<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																				</tr>
																				<tr>
																					<td class="ContentTable" height="1"><img src="images/clear.gif" height="1"></td>
																					<TD class="ContentTableHorizontal" colspan="7" height="1"><img src="images/clear.gif" height="1"></TD>
																					<td class="ContentTable" height="1"><img src="images/clear.gif" height="1"></td>
																				</tr>
																			</table>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																</Columns>
																<PagerStyle HorizontalAlign="Right" Position="Top" CssClass="Content" Wrap="False" Mode="NumericPages"></PagerStyle>
															</asp:datagrid></TD>
													</TR>
												</TABLE>
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
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CouponSales.aspx.vb" Inherits="StoreFront.StoreFront.CouponSales" %>
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
			- Merchant Tools</title>
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
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server">
		<form id="Form2" method="post" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<tr>
					<td class="TopBanner" colSpan="3">
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
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table cellSpacing="3" cellPadding="5" width="100%" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/reports_ac.asp ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="Content" align="center">
									<p id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></font></p>
								</td>
							</tr>
							<tr class="Headings" vAlign="top">
								<td class="Headings">&nbsp;Coupon Sales
								</td>
							</tr>
							<tr>
								<td class="Content"><asp:label id="DateInfo" CssClass="Content" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="ContentTableHeader">&nbsp;</td>
											<td class="ContentTableHeader" align="center" valign="middle" width="8%" nowrap>&nbsp;</td>
											<td class="ContentTableHeader" align="left" valign="middle" width="38%" nowrap>Coupon Code&nbsp;</td>
											<td class="ContentTableHeader" align="center" style="text-align: center;" valign="middle" width="18%" noWrap>Discounts Applied&nbsp;</td>
											<td class="ContentTableHeader" align="center" style="text-align: center;" valign="middle" width="18%" noWrap>Order Total&nbsp;</td>
											<td class="ContentTableHeader" align="center" style="text-align: center;" valign="middle" width="18%" noWrap># Orders&nbsp;</td>
											<td class="ContentTableHeader">&nbsp;</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" colspan="7">&nbsp;</td>
											<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
									</table>
									<asp:datagrid id="DataGrid1" runat="server" PageSize="10" AutoGenerateColumns="False" ShowHeader="False" AllowPaging="True" Width="100%" BorderWidth="0px" CellPadding="0">
											<Columns>
												<asp:TemplateColumn>
													<ItemTemplate>
														<table border="0" cellpadding="0" cellspacing="0" width="100%">
															<tr>
																<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																<td class="Content">&nbsp;</td>
																<td class="Content" align="center" valign="Top" width="8%" noWrap>
																	<asp:linkbutton id="cmdShowDetail" Runat="server" CommandName="ShowDetails" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"DiscountID")%>'>
																		<asp:Image BorderWidth="0" ID="imgShowDetail" runat="server" ImageUrl="images/down.gif" AlternateText="Show Detail"></asp:Image>
																	</asp:linkbutton>
																</td>
																<td class="Content" align="left" valign="Top" width="38%" noWrap>
																	<%#DataBinder.Eval(Container.DataItem,"Description")%>
																</td>
																<td class="Content" align="center" valign="Top" width="18%" noWrap>
																	<%#Format(DataBinder.Eval(Container.DataItem,"DiscountTotal"), "c")%>
																</td>
																<td class="Content" align="center" valign="Top" width="18%" noWrap>
																	<%#Format(DataBinder.Eval(Container.DataItem,"OrderTotal"), "c")%>
																</td>
																<td class="Content" align="center" valign="Top" width="18%" noWrap>
																	<%#DataBinder.Eval(Container.DataItem,"OrderCount")%>
																</td>
																<td class="Content">&nbsp;</td>
																<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
															</tr>
															<tr id="trDetailSpacer" runat="server" visible="false">
																<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																<td class="Content" colspan="7">&nbsp;</td>
																<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
															</tr>
															<tr id="trDetail" runat="server" visible="false">
																<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																<td class="Content" colspan="7">
																<asp:Repeater id="rDetail" runat="server">
																	<ItemTemplate>
																		<table border="0" cellpadding="0" cellspacing="0" width="100%">
																			<tr>
																				<td class="Content" align="center" width="15%">&nbsp;</td>
																				<td class="Content" align="left" width="31%"><a href="orddetails.aspx?OrderID=<%#DataBinder.Eval(Container.DataItem,"OrderNumber")%>"><%#DataBinder.Eval(Container.DataItem,"OrderNumber")%></a></td>
																				<td class="Content" align="center" width="18%"><%#Format(DataBinder.Eval(Container.DataItem,"DiscountTotal"), "c")%></td>
																				<td class="Content" align="center" width="18%"><%#Format(DataBinder.Eval(Container.DataItem,"GrandTotal"), "c")%></td>
																				<td class="Content" align="center" width="18%">&nbsp;</td>
																			</tr>
																			<tr>
																				<td class="Content" width="1"><img src="images/clear.gif" width="1"></td>
																				<td class="Content" colspan="7">&nbsp;</td>
																				<td class="Content" width="1"><img src="images/clear.gif" width="1"></td>
																			</tr>
																		</table>
																	</ItemTemplate>
																</asp:Repeater>
																</td>
																<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
															</tr>
															<tr>
																<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																<td class="Content" colspan="7">&nbsp;</td>
																<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
															</tr>
														</table>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Position="TopAndBottom" CssClass="Content" Wrap="False"
												Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
										<table border="0" cellpadding="0" cellspacing="0" width="100%">
											<tr>
												<td class="ContentTable" colSpan="9" height="1">
													<img height="1" src="images/clear.gif"></td>
											</tr>
										</table>
								</td>
							</tr>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
	</body>
</HTML>

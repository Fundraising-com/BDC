<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Confirm.aspx.vb" Inherits="StoreFront.StoreFront.Confirm1"%>
<%@ Register TagPrefix="uc1" TagName="COrderDetailControl" Src="Controls/COrderDetailControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Confirm</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.1.0

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
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%= Me.StyleLinkURL %>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="<%= Me.StyleLinkURL %>general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
		function FormClick(objForm)
		{
			objForm.action = "<%= Me.StyleLinkURL %>confirm.aspx";
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" class="GeneralPage" runat="server" id="BodyTag">
		<table id="PageTable" width="100%" cellSpacing="0" runat="server">
			<tr>
				<td id="PageCell">
					<table class="GeneralTable" id="PageSubTable" runat="server" cellspacing="0">
						<tr>
							<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
								<!-- Top Banner Start -->
								<uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
								<!-- Top Banner End -->
							</td>
						</tr>
						<tr align="middle">
							<td class="TopSubBanner" id="TopSubBannerCell" align="middle" width="100%" colSpan="3">
								<!-- Top Sub Banner Start -->
								<uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
								<!-- Top Sub Banner End -->
							</td>
						</tr>
						<tr>
							<td class="LeftColumn" id="LeftColumnCell" vAlign="top" align="left">
								<!-- Left Column Start -->
								<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
								<!-- Left Column End -->
							</td>
							<td class="Content" vAlign="top" id="ContentCell">
								<!-- Content Start -->
								<table cellSpacing="3" cellPadding="5" width="100%" border="0">
									<tr>
										<td>
											<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
											<!-- Instruction End --></td>
									</tr>
									<tr>
										<td class="Content">
											<P id="ErrorAlignment" runat="server" align="center">
												<font color="#ff0000">
													<asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label>
												</font>
											</P>
											<P>
												<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
													<TR>
														<TD class="Headings" colSpan="2" id="HeadRow">Order Number&nbsp;
															<asp:label id="lblOrderNumber" runat="server"></asp:label>&nbsp;Has 
															Successfully Been Completed!
														</TD>
													</TR>
													<TR>
														<TD class="ContentHeading" colSpan="2">&nbsp;
															<table id="WorldPayTable" cellSpacing="0" cellPadding="0" border="0" runat="server">
																<tr>
																	<td class="Content">&nbsp;</td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
																	<TD class="ContentTableHeader">&nbsp;</TD>
																	<TD class="ContentTableHeader">WorldPay</TD>
																	<TD class="ContentTableHeader">&nbsp;</TD>
																	<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																	<TD class="Content">&nbsp;</TD>
																	<td class="Content" width="100%"><%= ProcessorDisplay %></td>
																	<TD class="Content">&nbsp;</TD>
																	<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" height="1" colspan="5"><img src="images/clear.gif" height="1"></td>
																</tr>
																<tr>
																	<td class="Content">&nbsp;</td>
																</tr>
															</table>
														</TD>
													</TR>
													<TR>
														<TD class="Content" width="50%"><cc1:downloaddisplay id=DownloadDisplay1 runat="server" OrderID='<%# DataBinder.Eval(Me, "OrderID") %>' DownloadDate='<%# DataBinder.Eval(Me, "DownloadDate") %>' HeadingClass="Headings" HeadingLabel="Download Now:" ItemClass="Content" TableBorderStyle="Content">
															</cc1:downloaddisplay></TD>
														<td class="Content" width="50%">&nbsp;</td>
													</TR>
													<TR>
														<TD class="ContentHeading" colSpan="2">&nbsp;</TD>
													</TR>
													<TR>
														<TD align="right" colSpan="2">&nbsp;
															<form id="Form2" onsubmit="javascript: FormClick(this);" method="post" runat="server">
																<input type="hidden" name="OrderID" runat="server" id="txtOrderID">
																<asp:ImageButton ID="btnView" Runat="server" AlternateText="View and Print Receipt"></asp:ImageButton>
															</form>
														</TD>
													</TR>
													<TR>
														<TD class="Content" align="right" colSpan="2">&nbsp;</TD>
													</TR>
													<tr id="PhoneFaxRow">
														<td>
															<TABLE id="tblCreditCard" visible="false" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
																<TR>
																	<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	<TD class="ContentTableHeader">&nbsp;Credit Card Information</TD>
																	<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	<TD>
																		<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<TR>
																				<TD class="Content" noWrap align="right">&nbsp;</TD>
																				<TD class="Content" noWrap align="right" colSpan="7">&nbsp;&nbsp;</TD>
																				<TD class="Content" align="left">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="Content" noWrap align="right">&nbsp;</TD>
																				<TD class="Content" noWrap align="right">Card Type:&nbsp;</TD>
																				<TD class="Content" align="left" colSpan="4" vAlign="top">&nbsp;
																					<asp:label id="lblCreditCardType" Runat="server"></asp:label>
																				</TD>
																				<TD class="Content" noWrap align="right">Card Number:&nbsp;</TD>
																				<TD class="Content" align="left" vAlign="center"><asp:Label id="lblCardNumber" Runat="server"></asp:Label></TD>
																				<TD class="Content" align="left">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="Content" noWrap align="right">&nbsp;</TD>
																				<TD class="Content" noWrap align="right" colSpan="7">&nbsp;</TD>
																				<TD class="Content" align="left">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="Content" noWrap align="right">&nbsp;</TD>
																				<TD class="Content" noWrap align="right">Expiration Date:&nbsp;</TD>
																				<TD class="Content" align="middle">Month&nbsp;</TD>
																				<TD class="Content" noWrap align="left">
																					<asp:Label id="lblMonth" Runat="server"></asp:Label></TD>
																				<TD class="Content" noWrap align="right" vAlign="center">
																					Year&nbsp;</TD>
																				<TD class="Content">
																					<asp:label id="lblYear" runat="server"></asp:label></TD>
																				<td class="Content" noWrap align="right">&nbsp;&nbsp;Security Code:&nbsp;</td>
																				<td class="Content">
																					<asp:label id="lblSecureCode" runat="server"></asp:label></td>
																				<TD class="Content" align="left">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="Content" noWrap align="right">&nbsp;</TD>
																				<TD class="Content" noWrap align="right" colSpan="7">&nbsp;&nbsp;</TD>
																				<TD class="Content" noWrap align="left">&nbsp;</TD>
																			</TR>
																		</TABLE>
																	</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<tr>
																	<td class="Content" colspan="3">&nbsp;</td>
																</tr>
															</TABLE>
															<TABLE id="tblPurchaseOrder" visible="false" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
																<TR>
																	<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	<TD class="ContentTableHeader" width="100%">&nbsp;Purchase Order Information</TD>
																	<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	<TD>
																		<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<TBODY>
																				<TR>
																					<TD class="Content" noWrap align="right" colSpan="3">&nbsp;</TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">&nbsp;</TD>
																					<TD class="Content" valign="center" align="middle" width="100%" noWrap>Purchase 
																						Order Number:&nbsp;<asp:label id="lblPONumber" runat="server"></asp:label></TD>
																					<TD class="Content" noWrap align="left">&nbsp;</TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right" colSpan="3">&nbsp;</TD>
																				</TR>
																			</TBODY>
																		</TABLE>
																	</TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<tr>
																	<td class="Content" colspan="3">&nbsp;</td>
																</tr>
															</TABLE>
														</td>
													</tr>
													<TR>
														<TD class="Content" align="middle" colSpan="2">
															<uc1:COrderDetailControl id="COrderDetailControl1" runat="server"></uc1:COrderDetailControl></TD>
													</TR>
												</TABLE>
											</P>
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
							<td class="Footer" id="FooterCell" colSpan="3">
								<!-- Footer Start -->
								<uc1:footer id="Footer1" runat="server"></uc1:footer>
								<!-- Footer End -->
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="affiliatepayments.aspx.vb" Inherits="StoreFront.StoreFront.affiliatepayments"%>
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
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<!-- Top Banner Start -->
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td class="TopBanner2" width="20%"><img src="images/sflogo.jpg"></td>
								<td class="TopBanner">&nbsp;&nbsp;Merchant Tools</td>
							</tr>
						</table>
						<!-- Top Banner End -->
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<!-- Top Sub Banner Start --> Affiliates 
						<!-- Top Sub Banner End -->
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End -->
					</td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<A href="javascript: doHelp(' http://support.storefront.net/mtdocs/afftpp_pp.asp ')">
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
								<td class="content" align="middle">
									<P id="ErrorAlignment" runat="server" align="center">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</P>
								</td>
							</tr>
							<TR>
								<TD class="content" vAlign="top" align="middle">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="9" height="1" width="100%"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle"><A class="content" href="affiliatepayments.aspx"><b>Manage 
														Payments</b></A></td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle"><A class="content" href="affiliatelist.aspx">Manage 
													Affiliates</A></td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle"><A class="content" href="addaffliate.aspx">Add 
													Affiliates</A></td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle"><A class="content" href="affliatesettings.aspx">Settings</A></td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<TR>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<TD class="content" vAlign="top" align="middle" colSpan="7">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" width="100%">
															<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
																<TR>
																	<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;View Payments
																	</TD>
																	<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<tr>
																	<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	<TD class="Content" colSpan="3">
																		<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
																			<TR>
																				<td class="content" colSpan="5">&nbsp;</td>
																			</TR>
																			<TR>
																				<td class="content">&nbsp;</td>
																				<td class="content" noWrap>For Affiliates:
																				</td>
																				<td class="content" noWrap><asp:dropdownlist id="ddaffliates" CssClass="content" Runat="server" DataValueField="Id" DataTextField="Name"></asp:dropdownlist></td>
																				<td class="content" noWrap><asp:checkbox id="chkPending" CssClass="content" Text="Only Show Affiliates With Pending Payments" Runat="server" AutoPostBack="True"></asp:checkbox></td>
																				<td class="content">&nbsp;</td>
																			</TR>
																			<TR>
																				<td class="content" colSpan="5">&nbsp;</td>
																			</TR>
																			<TR>
																				<td class="content" align="right" colSpan="5">
																					<asp:LinkButton ID="cmdSubmit" Runat="server">
																						<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="images/submit.jpg" AlternateText="Submit"></asp:Image>
																					</asp:LinkButton>
																					&nbsp;</td>
																			</TR>
																			<TR>
																				<td class="content" colSpan="5">&nbsp;</td>
																			</TR>
																		</TABLE>
																	</TD>
																	<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																</tr>
																<TR>
																	<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
																</TR>
															</TABLE>
														</td>
													</tr>
												</table>
											</TD>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</TR>
										<tr>
											<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</TD>
							</TR>
						</table>
						<!-- Content End -->
					</td>
				</tr>
			</table>
			</TD></TR></TABLE></TD></TR></TABLE>
		</form>
	</body>
</HTML>

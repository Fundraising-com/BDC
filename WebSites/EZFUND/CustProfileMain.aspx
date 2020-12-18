<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustProfileMain.aspx.vb" Inherits="StoreFront.StoreFront.CustProfileMain"%>
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
			- Customer Profile Main Menu</title>
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
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
	</HEAD>
	<body id="BodyTag" runat="server" class="generalpage">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1" runat="server">
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
											<td class="Content" align="left">
												<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<td class="content">
															<P id="ErrorAlignment" runat="server" align="center">
																<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label>
															</P>
														</td>
													<TR>
														<TD class="Headings">My Account</TD>
													</TR>
													<tr>
														<td class="Content">&nbsp;</td>
													</tr>
													<TR>
														<TD class="Content"><a class="Content" href="CustEdit.aspx"><u>Edit My Profile</u></a></TD>
													</TR>
													<tr>
														<td class="Content">&nbsp;</td>
													</tr>
													<TR>
														<TD class="Content"><a class="Content" href="OrderHistory.aspx"><u>View Order Status and 
																	History</u></a></TD>
													</TR>
													<tr>
														<td class="Content">&nbsp;</td>
													</tr>
													<TR>
														<TD class="Content"><a class="Content" href="SavedCart.aspx"><u>Access My Wish List</u></a></TD>
													</TR>
													<tr>
														<td class="Content">&nbsp;</td>
													</tr>
													<TR>
														<TD class="Content"><a class="Content" href="CustAddressBook.aspx"><u>Manage Address Book</u></a></TD>
													</TR>
													<tr>
														<td class="Content">&nbsp;</td>
													</tr>
													<TR>
														<TD class="Content">
															<asp:LinkButton ID="btnSignOut" Runat="server">
																<asp:Image BorderWidth="0" ID="imgSignOut" Runat="server" AlternateText="Sign Out"></asp:Image>
															</asp:LinkButton>
														</TD>
													</TR>
												</TABLE>
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
	</body>
</HTML>

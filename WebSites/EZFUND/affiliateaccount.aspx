<%@ Page Language="vb" AutoEventWireup="false" Codebehind="affiliateaccount.aspx.vb" Inherits="StoreFront.StoreFront.affiliateaccount"%>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>My Affiliate Account</title>
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
												<!-- Instruction Start -->
												<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End -->
											</td>
										</tr>
										<tr>
											<td class="Content">
												<P id="ErrorAlignment" runat="server">
													<asp:Label id="lblErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
												<P id="MessageAlignment" runat="server">
													<asp:Label id="lblMessage" runat="server" CssClass="Messages" Visible="False"></asp:Label>
													<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD class="Headings">&nbsp;My Affiliate Account</TD>
														</TR>
														<tr>
															<td class="Content">&nbsp;</td>
														</tr>
														<TR>
															<TD class="Content"><a class="Content" href="affiliateregister.aspx"><u>Edit My Affiliate 
																		Information</u></a></TD>
														</TR>
														<tr>
															<td class="Content">&nbsp;</td>
														</tr>
														<TR>
															<TD class="Content"><a class="Content" href="affiliatecommissions.aspx"><u>View Commissions</u></a></TD>
														</TR>
														<tr>
															<td class="Content">&nbsp;</td>
														</tr>
														<TR>
															<TD class="Content"><a class="Content" href="affiliatelinks.aspx"><u>Create Affiliate Links</u></a></TD>
														</TR>
														<tr>
															<td class="Content">&nbsp;</td>
														</tr>
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

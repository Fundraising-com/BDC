<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="affiliatecommissions.aspx.vb" Inherits="StoreFront.StoreFront.affiliatecommissions"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
<HTML>
	<HEAD>
		<title>Affiliate Commissions</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="General.js"></script>
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
								<td class="LeftColumn" id="LeftColumnCell" vAlign="top" align="left" width="10%">
									<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
									<!-- Left Column End --> &nbsp;
								</td>
								<td class="Content" vAlign="top" id="ContentCell">
									<!-- Content Start -->
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="Instructions">
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<asp:panel id="Commissions" Runat="server">
											<TR>
												<TD class="Headings">&nbsp;Commissions</TD>
											</TR>
											<TR>
												<TD class="content" vAlign="top" align="middle">
													<P id="ErrorAlignment" runat="server">
														<asp:Label id="lblErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
													<TABLE cellSpacing="0" cellPadding="0" width="100%">
														<TR>
															<TD class="content" vAlign="top" align="left">Commissions:</TD>
															<TD class="content" vAlign="top" align="left">
																<asp:Label id="lblCommisionType" Runat="server" CssClass="content"></asp:Label></TD>
															<TD class="content" vAlign="top" align="right">Current Earnings:&nbsp;&nbsp;</TD>
															<TD class="content" vAlign="top" align="left">
																<asp:Label id="lblCurrentEarings" Runat="server" CssClass="content"></asp:Label></TD>
														</TR>
														<TR>
															<TD width="100%" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
														</TR>
														<asp:panel id="LastPayment" Runat="server">
															<TR>
																<TD class="content" vAlign="top" align="left">&nbsp;</TD>
																<TD class="content" vAlign="top" align="left">&nbsp;</TD>
																<TD class="content" vAlign="top" align="right">Last Payment:&nbsp;&nbsp;</TD>
																<TD class="content" vAlign="top" align="left">
																	<asp:Label id="lblLastPayment" Runat="server" CssClass="content"></asp:Label></TD>
															</TR>
														</asp:panel></TABLE>
													<P></P>
												</TD>
											</TR>
										</asp:panel><asp:panel id="Terms" Runat="server">
											<TR>
												<TD class="Headings">&nbsp;Terms</TD>
											</TR>
											<TR>
												<TD class="content" vAlign="top" align="left">Terms:</TD>
											</TR>
											<TR>
												<TD class="content" vAlign="top" align="left" width="100%">
													<asp:Label id="lblTerms" Runat="server" CssClass="content"></asp:Label></TD>
											</TR>
										</asp:panel>
										<P></P>
									</table>
									<!-- Content End -->
								</td>
								<td class="RightColumn" id="RightColumnCell" vAlign="top" align="left" width="10%">
									<!-- Right Column Start --><uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
									<!-- Right Column End --></td>
							</tr>
							<tr>
								<td colSpan="3" class="Footer" id="FooterCell">
									<!-- Footer Start --><uc1:footer id="Footer1" runat="server"></uc1:footer>
									<!-- Footer End --></td>
							</tr>
							</TBODY>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE>
		</form>
	</body>
</HTML>

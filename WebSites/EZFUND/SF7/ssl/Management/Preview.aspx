<%@ Register TagPrefix="uc1" TagName="PreviewFooterNav" Src="Controls/PreviewFooterNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PreviewRightColumnNav" Src="Controls/PreviewRightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PreviewLeftColumnNav" Src="Controls/PreviewLeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PreviewTopSubBanner" Src="Controls/PreviewTopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PreviewTopBanner" Src="Controls/PreviewTopBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Preview.aspx.vb" Inherits="StoreFront.StoreFront.Preview" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Preview Page</title>
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
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<style id="PreviewStyle" runat="Server"></style>
		<LINK href="<%=StylesheetPath()%>Styles.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td id="PageCell" align="left" runat="server">
						<table class="GeneralTable" id="PageSubTable" cellSpacing="0" width="100%" border="0" runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start -->
									<uc1:previewtopbanner id="TopBanner1" runat="server"></uc1:previewtopbanner>
									<!-- Top Banner End -->
								</td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start -->
									<uc1:PreviewTopSubBanner id="TopSubBanner1" runat="server"></uc1:PreviewTopSubBanner>
									<!-- Top Sub Banner End -->
								</td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell" runat="server">
									<!-- Left Column Start -->
									<uc1:PreviewLeftColumnNav id="LeftColumnNav1" runat="server"></uc1:PreviewLeftColumnNav>
									<!-- Left Column End -->
								</td>
								<td class="Content" vAlign="top" width="100%" id="ContentCell" runat="server">
									<!-- Content Start -->
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="Instructions" width="100%">Instructions
											</td>
										</tr>
										<tr>
											<td class="Messages">Messages
											</td>
										</tr>
										<tr>
											<td class="ErrorMessages">Error Messages
											</td>
										</tr>
										<tr>
											<td class="Headings">Headings
											</td>
										</tr>
										<tr>
											<td class="Content">Content
											</td>
										</tr>
										<tr>
											<td class="Content">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</tr>
													<tr>
														<TD class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></TD>
														<TD class="ContentTableHeader" colSpan="1">Content Table Header</TD>
														<TD class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</tr>
													<tr>
														<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</tr>
													<tr>
														<TD class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></TD>
														<TD class="Content" width="100%" colSpan="1">Content</TD>
														<TD class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</tr>
													<tr>
														<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<!-- Content End --></td>
								<td class="RightColumn" id="RightColumnCell" runat="server">
									<!-- Right Column Start -->
									<uc1:PreviewRightColumnNav id="RightColumnNav1" runat="server"></uc1:PreviewRightColumnNav>
									<!-- Right Column End -->
								</td>
							</tr>
							<tr>
								<td colSpan="3" class="Footer" id="FooterCell">
									<!-- Footer Start -->
									<uc1:PreviewFooterNav id="Footer1" runat="server"></uc1:PreviewFooterNav>
									<!-- Footer End -->
								</td>
							</tr>
						</table>
				<tr>
					<td align="center">
						&nbsp;
					</td>
				</tr>
				<tr>
					<td align="center">
						<input type="image" src="images/close.jpg" align="middle" onclick="window.close();">
					</td>
				</tr>
				</td></tr></table>
		</form>
	</body>
</HTML>

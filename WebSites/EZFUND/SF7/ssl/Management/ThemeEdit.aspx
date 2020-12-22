<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ThemeEdit.aspx.vb" Inherits="StoreFront.StoreFront.ThemeEdit" %>
<%@ Register TagPrefix="uc1" TagName="PageSettings" Src="Controls/PageSettings.ascx" %>
<%@ Register TagPrefix="uc1" TagName="GeneralControl" Src="Controls/GeneralControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBannerContent" Src="Controls/TopBannerContent.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBannerContent" Src="Controls/TopSubBannerContent.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Content" Src="Controls/Content.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DesignSettings" Src="Controls/DesignSettings.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LayoutGuide" Src="Controls/LayoutGuide.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.1

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
					<td class="TopBanner" colSpan="3"><uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner></td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3"><uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Themes"></uc1:TopSubBanner></td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="Content" align="right" colspan="3">
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/theme_edit.asp  ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content" colspan="3">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="content" align="center" colspan="3">
									<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></P>
								</td>
							</tr>
							<tr>
								<td class="Content" colspan="3">
									&nbsp;
								</td>
							</tr>
							<TR>
								<td colspan="3">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR id="Tr2" runat="server">
											<TD class="ContentTableHeader" align="left" width="100%" colSpan="9"></TD>
										</TR>
										<tr>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<td class="Content" align="center"><A class="content" href="ManageThemes.aspx">Theme 
													Gallery</A>
											</td>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<td class="Content" align="center"><b>Edit Current Theme</b>
											</td>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<td class="Content" align="center"><A class="content" href="EditButtons.aspx">Edit 
													Current Buttons</A>
											</td>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
										</tr>
										<TR id="Tr1" runat="server">
											<TD class="ContentTableHeader" align="left" width="100%" colSpan="9"></TD>
										</TR>
									</table>
								</td>
							</TR>
							<tr>
								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
								<td class="Content">&nbsp;</td>
								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
							</tr>
							<TR>
								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
								<TD class="content" vAlign="top" align="center">
									<table class="content" cellSpacing="0" cellPadding="0" width="97%" border="0">
										<tr>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="ContentTableHeader" colSpan="7" height="1">
												<asp:Label ID="lblEditTheme" Runat="server">Edit Theme</asp:Label></td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<TR>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<TD class="content" vAlign="top" align="center" colSpan="7">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="center">
															<TABLE id="Table1" width="100%">
																<TR>
																	<TD class="Content" noWrap width="180" align="center" valign="top">Click on a 
																		section to edit.<br>
																		<br>
																		<uc1:layoutguide id="LayoutGuide1" runat="server"></uc1:layoutguide></TD>
																	<TD vAlign="top" align="center" width="100%"><asp:label id="lblSelectedRegion" runat="server" CssClass="Messages"></asp:label><br>
																		<uc1:designsettings Visible="False" id="DesignSettings1" runat="server"></uc1:designsettings><BR>
																		<uc1:PageSettings Visible="False" id="PageSettings1" runat="server"></uc1:PageSettings><br>
																		<uc1:TopBannerContent Visible="False" id="TopBannerContent1" runat="server"></uc1:TopBannerContent>
																		<uc1:TopSubBannerContent Visible="False" id="TopSubBannerContent1" runat="server"></uc1:TopSubBannerContent>
																		<uc1:Content Visible="False" id="Content1" runat="server"></uc1:Content>
																		<%-- <uc1:CustomHtml Visible="False" id="CustomHtml1" runat="server"></uc1:CustomHtml> --%><br>
																	</TD>
																</TR>
																<tr>
																	<td align="left">
																		<asp:ImageButton id="cmdDelete" runat="server" ImageUrl="images/delete.jpg"></asp:ImageButton>
																	</td>
																	<td align="right">
																		<asp:ImageButton id="cmdPreview" runat="server" ImageUrl="images/preview.jpg"></asp:ImageButton>
																		<asp:ImageButton id="cmdSave" runat="server" ImageUrl="images/save.jpg"></asp:ImageButton>
																	</td>
																</tr>
															</TABLE>
														</td>
													</tr>
												</table>
											</TD>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</TR>
										<tr>
											<td class="ContentTable" colSpan="8" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</TD>
								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
							</TR>
							<tr>
								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
								<td class="Content">&nbsp;</td>
								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
							</tr>
							<tr>
								<TD class="ContentTable" colspan="3" width="1"><IMG src="images/clear.gif" width="1"></TD>
							</tr>
						</table>
						<!-- Content End --></td>
				</tr>
			</table></form>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EditButtons.aspx.vb" Inherits="StoreFront.StoreFront.EditButtons" %>
<%@ Register TagPrefix="uc1" TagName="SFExpressUploadControl" Src="Controls/SFExpressUploadControl.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
		<%
		'SFExpress
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
		<script language="javascript">
		//Function for Image Upload Control
		
		
		</script>
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
					<td class="Content" vAlign="top" colSpan="2">
						<!-- Content Start -->
						<table id="tblButtons" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="Content" align="right" colspan=3>
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/theme_editbuttons.asp  ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="content" colspan=3>
									<p id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></font></p>
								</td>
							</tr>
							<TR>
								<td colspan=3>
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<td class="Content" align="center"><A class="content" href="ManageThemes.aspx">Theme 
													Gallery</A>
											</td>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<td class="Content" align="center"><A class="content" href="ThemeEdit.aspx">Edit 
													Current Theme</A>
											</td>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<td class="Content" align="center"><b>Edit Current Buttons</b>
											</td>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif"></TD>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
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
											<td class="ContentTableHeader" align="left" colSpan="7">Current Buttons</td>
										</tr>
										<tr>
											<td align="center" colSpan="7">
												<table class="content" cellSpacing="0" cellPadding="0" border="0">
													<asp:datalist id="dlButtonSet" runat="server" CellPadding="0" Width="100%" ShowHeader="False"
														ShowFooter="False">
														<ItemTemplate>
															<tr>
																<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																<td class="Content" align="left">&nbsp;&nbsp;
																	<asp:Label id="ButtonLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'>
																	</asp:Label>&nbsp;&nbsp;</td>
																<td class="Content" align="left">&nbsp;&nbsp;
																	<asp:Image id="LabelImage" runat="server" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Filename")%>'>
																	</asp:Image>&nbsp;&nbsp;</td>
																<td class="Content" align="left">&nbsp;&nbsp;
																	<asp:ImageButton ID="btnBrowse" OnClick="SaveButton" ImageUrl="Images/icon_browse.gif" Runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem,"uid") %>'>
																	</asp:ImageButton>
																	&nbsp;&nbsp;<br>
																	<uc1:SFExpressUploadControl id="ucUploadImage" runat="server" Visible="False"></uc1:SFExpressUploadControl></td>
																<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
															</tr>
															<tr>
																<td colspan="5" class="ContentTableHeader"><IMG src="images/clear.gif" width="1"></td>
															</tr>
														</ItemTemplate>
													</asp:datalist></table>
											</td>
										</tr>
										<tr>
											<td class="Content" colSpan="7">&nbsp;</td>
										</tr>
									</table>
								</td>
								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			</FORM>
	</body>
</HTML>

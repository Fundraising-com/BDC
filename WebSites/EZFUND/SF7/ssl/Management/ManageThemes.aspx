<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ManageThemes.aspx.vb" Inherits="StoreFront.StoreFront.ManageThemes" %>
<%@ Register TagPrefix="uc1" TagName="GeneralControl" Src="Controls/GeneralControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
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
		<script language="javascript">
		function Validate()
		{
			if(document.Form2.txtThemeName.value == "")
			{
				alert('Please Enter a Theme Name');
				return false;
			}
		}
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
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/themes_gallery.asp  ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<TR>
							<tr>
								<td class="content" align="center">
									<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></P>
								</td>
							</tr>
							<tr>
								<td class="content" align="center">&nbsp;
								</td>
							</tr>
							<TR>
								<td>
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR id="Tr2" runat="server">
											<TD class="ContentTableHeader" align="left" width="100%" colSpan="9"></TD>
										</TR>
										<tr>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<td class="Content" align="center"><b>Theme Gallery</b>
											</td>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<td class="Content" align="center"><asp:LinkButton CssClass="Content" ID="btnEdit" runat="server">Edit Current Theme</asp:LinkButton>
											</td>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<td class="Content" align="center"><A class="content" href="EditButtons.aspx">Edit 
													Current Buttons</A>
											</td>
											<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
										</tr>
									</table>
								</td>
							</TR>
							<TR>
								<TD class="content" vAlign="top" align="center">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<TR>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<TD class="content" vAlign="top" align="center" colSpan="6">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="center">
															<TABLE class="Content" id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center"
																border="0">
																<TR class="ContentTableHeader">
																	<TD align="left" colSpan="3">Current Theme:&nbsp;<asp:label id="lblThemeName" runat="server"></asp:label></TD>
																</TR>
																<TR>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																	<TD>&nbsp;</TD>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																</TR>
																<TR class="Content">
																	<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
																	<TD align="center" width="100%"><asp:imagebutton id="imgCurrentTheme" runat="server"></asp:imagebutton><BR>
																		<br>
																		<asp:imagebutton id="CurrentThemeEditButton" runat="server" ImageUrl="images/edit.jpg"></asp:imagebutton></TD>
																	<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
																</TR>
																<TR>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																	<TD>&nbsp;</TD>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																</TR>
																<TR class="ContentTable">
																	<td colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
																</TR>
															</TABLE>
															<BR>
															<TABLE class="Content" id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center"
																border="0">
																<TR class="ContentTableHeader">
																	<TD align="left" colSpan="3" height="15">Theme Gallery:</TD>
																</TR>
																<TR>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																	<TD>&nbsp;</TD>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																</TR>
																<TR>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																	<TD class="Content">&nbsp;Browse by Category:&nbsp;<asp:dropdownlist id="ddlCats" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																</TR>
																<TR>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																	<TD>&nbsp;</TD>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																</TR>
																<TR class="ContentTable">
																	<td colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
																</TR>
																<TR>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																	<TD>&nbsp;</TD>
																	<td class="ContentTable" width="1"><IMG src="images/clear.gif"></td>
																</TR>
																<TR>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																	<TD class="Content" align="center" width="100%">
																		<asp:datagrid id="DataGrid1" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
																			PageSize="9" ShowHeader="False" BorderWidth="0px" EnableViewState="False">
																			<Columns>
																				<asp:TemplateColumn>
																					<ItemTemplate>
																						<asp:datalist id="DataList1" runat="server" CssClass="Content" RepeatDirection="Horizontal" HorizontalAlign="Center"
																							RepeatColumns="4">
																							<HeaderTemplate>
																								Click on Thumbnail to preview theme.<BR>
																							</HeaderTemplate>
																							<ItemTemplate>
																								<div align="center">
																									<asp:ImageButton id=imgThumbNail runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "Thumbnail") %>' OnClick="PreviewTheme">
																									</asp:ImageButton><BR>
																									<asp:Label id=lblTheme runat="server" CssClass="Content" text='<%#DataBinder.Eval(Container.DataItem, "Name") %>'>
																									</asp:Label><BR>
																									<asp:Panel ID="InstalledButtons" Runat="server">
																										<asp:ImageButton id="cmdApply" runat="server" ImageUrl="images/select.jpg" CommandName="Apply" OnClick="ApplyTheme"></asp:ImageButton>
																										<%--<asp:ImageButton id="cmdEdit" runat="server" ImageUrl="images/edit.jpg" CommandName="Edit" OnClick="EditTheme"></asp:ImageButton>--%>
																									</asp:Panel>
																									<br>
																									<br>
																								</div>
																							</ItemTemplate>
																							<SeparatorTemplate>
																								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																							</SeparatorTemplate>
																						</asp:datalist>
																					</ItemTemplate>
																				</asp:TemplateColumn>
																			</Columns>
																			<PagerStyle CssClass="ContentTable" Mode="NumericPages" Font-Name="Verdana" Font-Size="10" Font-Bold="True"
																				ForeColor="Black"></PagerStyle>
																		</asp:datagrid></TD>
																	<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
																</TR>
																<TR>
																	<TD class="Content" colSpan="3" height="1" align="center">
																		<asp:ImageButton id="lnkCheckCustom" runat="server" ImageUrl="images/installnewthemes.jpg" OnClick="lnkCheckCustom_Click"></asp:ImageButton>
																	</TD>
																</TR>
																<TR>
																	<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
																</TR>
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
							</TR>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
	</body>
</HTML>

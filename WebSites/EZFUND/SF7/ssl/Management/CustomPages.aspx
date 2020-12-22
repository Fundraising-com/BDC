<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustomPages.aspx.vb" Inherits="StoreFront.StoreFront.CustomPages"%>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
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

'@APPVERSION: 7.0.2

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
		function ConfirmDelete()
		{
			return ConfirmCancel('Are You Sure You Want to delete this item ?');	
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout" class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Custom Pages"></uc1:TopSubBanner>
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/pages_ov.asp ')">
										<img src="images/help.jpg" border="0"></a> 
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="content" vAlign="top" align="middle" colSpan="2">
									<p id="ErrorAlignment" runat="server" align="center"><font color="#ff0000">
											<asp:label id="lblErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</p>
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colspan="3" height="1"><img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="left">
												<table class="Content" cellpadding="5" width="100%">
													<tr>
														<td class="Content">
<table cellSpacing="0" cellPadding="0" width="100%" class="content">
	<tr>
		<td class="ContentTableHeader" align="left" colSpan="10">&nbsp;Custom Pages</td>
	</tr>
	<tr>
		<td>
			<asp:DataGrid id="dg1" runat="server" ShowHeader="False" AutoGenerateColumns="False" AllowPaging="True" Width="100%" ItemStyle-Width="100%" BorderWidth="0px" CellPadding="0" GridLines="Horizontal">
				<ItemStyle Width="100%"></ItemStyle>
				<PagerStyle HorizontalAlign="Right" CssClass="ContentTableHeader" Mode="NumericPages"></PagerStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<table width="100%" cellpadding="0" cellspacing="0" border="0">
								<tr>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="Content" width="1">&nbsp;</TD>
									<td class="content" noWrap align="left">
									<td class="Content">
										<%# Container.DataItem.PageTitle %>
									</td>
									<td class="Content" align="right">
										<asp:HyperLink ID="hlnkEdit" Runat="server" ImageUrl="images/icon_edit.gif" NavigateUrl='<%# String.Concat("ManageCustomPages.aspx?Edit=1&Id=",Container.DataItem.Id)%>'>
										</asp:HyperLink>
										&nbsp;
										<asp:LinkButton id="lnkDelete" Runat="server" OnClick="lnkDelete_Click" CommandArgument="<%# Container.DataItem.ID %>">
											<img src="images/icon_delete.gif" border="none" />
										</asp:LinkButton>
									<TD class="Content" width="1">&nbsp;</TD>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
								</tr>
								<tr>
									<TD class="ContentTable" width="1" colspan="7"><IMG src="images/clear.gif" width="1"></TD>
								</tr>
							</table>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</td>
	</tr>
</table>
														</td>
													</tr>
													<tr>
														<td class="Content">
															<asp:HyperLink ID="hlnkAddNew" Runat="server" NavigateUrl="ManageCustomPages.aspx" ImageUrl="images/add_new.jpg"></asp:HyperLink>
														</td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										<tr>
											<td class="ContentTable" colspan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					<!-- Content End -->
					</td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TaxTypes.aspx.vb" Inherits="StoreFront.StoreFront.TaxTypes"%>
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
				<TBODY>
					<tr>
						<td class="TopBanner" colSpan="3"><uc1:topbanner id="TopBanner2" runat="server"></uc1:topbanner></td>
					</tr>
					<tr>
						<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
							<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="RMS EDI Configuration"></uc1:TopSubBanner>
						</td>
					</tr>
					<tr>
						<td class="LeftColumn" id="LeftColumnCell">
							<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
							<!-- Left Column End --></td>
						<td class="Content" vAlign="top">
							<!-- Content Start -->
							<table cellSpacing="3" cellPadding="5" width="100%" border="0">
								<TBODY>
									<tr>
										<td class="Content">
											<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
											<!-- Instruction End --></td>
									</tr>
									<tr>
										<td class="content" align="center">
											<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></font></P>
										</td>
									</tr>
									<!-- Row for displaying the added tax type -->
									<TR>
										<TD class="content" vAlign="top" align="center">
											<asp:Panel ID="listPanel" Runat="server">
												<TABLE class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="ContentTableHeader" noWrap align="left" width="30%">
															<asp:label id="lblTitle" Runat="server">Tax Type</asp:label></TD>
														<TD class="ContentTableHeader" noWrap align="left" width="70%" colSpan="2">
															<asp:label id="Label2" Runat="server">Equivalent in StoreFront</asp:label></TD>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" align="center" colSpan="3">
															<asp:datalist id="DLTaxTypes" Runat="server" Width="100%" ShowHeader="False" ShowFooter="False">
																<ItemTemplate>
																	<table cellSpacing="0" cellPadding="0" width="100%">
																		<tr>
																			<td class="content" colSpan="3">&nbsp;</td>
																		</tr>
																		<tr>
																			<td class="content" noWrap align="left" width="30%">&nbsp;<b>
																					<asp:label id="lblColor" Runat="server" CssClass="content">
																						<%# DataBinder.Eval(Container.DataItem,"TaxType") %>
																					</asp:label></b>
																			</td>
																			<td class="content" noWrap align="left" width="70%" colspan="2">&nbsp;<b>
																					<asp:label id="Label3" Runat="server" CssClass="content">
																						<%# DataBinder.Eval(Container.DataItem,"SFTaxType") %>
																					</asp:label></b>
																			</td>
																			<td class="content" noWrap align="middle">&nbsp;
																				<asp:LinkButton ID="btnDelete" Runat="server" OnClick=DeleteItem CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>'>
																					<asp:Image BorderWidth="0" ID="imgDelete" runat="server" ImageUrl="images/delete.jpg" AlternateText="Delete"></asp:Image>
																				</asp:LinkButton>
																			</td>
																		</tr>
																	</table>
																</ItemTemplate>
															</asp:datalist></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</TR>
												</TABLE>
												<BR>
											</asp:Panel>
											<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<TD class="ContentTableHeader" width="1" height="15"><IMG src="images/clear.gif" width="1"></TD>
													<td class="ContentTableHeader" noWrap align="left" width="100%" height="15"><asp:label id="lblAddNew" runat="server">Add a new Tax Type</asp:label></td>
													<TD class="ContentTableHeader" width="1" height="15"><IMG src="images/clear.gif" width="1"></TD>
												</tr>
												<tr>
													<td class="ContentTable" width="1" height="16"><IMG height="1" src="images/clear.gif"></td>
													<TD class="Content" vAlign="middle" noWrap align="center" width="100%" height="16"></TD>
													<td class="ContentTable" width="1" height="16"><IMG height="1" src="images/clear.gif"></td>
												</tr>
												<tr>
													<td class="ContentTable" width="1"><IMG height="1" src="images/clear.gif"></td>
													<TD class="Content" vAlign="middle" noWrap align="center" width="100%">Tax&nbsp;Type:&nbsp;
														<asp:textbox id="txtTaxType" Visible="True" Runat="server" Wrap="False"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StoreFront 
														Tax Type:
														<asp:dropdownlist id="SFTaxTypes" CssClass="Content" Runat="server">
															<asp:ListItem Value="1">Local Tax</asp:ListItem>
															<asp:ListItem Value="2">State Tax</asp:ListItem>
															<asp:ListItem Value="3">Country Tax</asp:ListItem>
														</asp:dropdownlist><br>
														<br>
														<asp:linkbutton id="btnAdd" Runat="server">
															<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="images/add.jpg" AlternateText="Add"></asp:Image>
														</asp:linkbutton>
													</TD>
													<td class="ContentTable" width="1"><IMG height="1" src="images/clear.gif"></td>
												</tr>
												<tr>
													<td class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
												</tr>
											</table>
										</TD>
									</TR>
								</TBODY></table>
							<!-- Content End --></td>
					</tr>
				</TBODY></table>
		</form>
	</body>
</HTML>

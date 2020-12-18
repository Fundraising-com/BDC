<%@ Page Language="vb" AutoEventWireup="false" Codebehind="upsregistration.aspx.vb" Inherits="StoreFront.StoreFront.upsregistration"%>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
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
		
			function SetValidation()
			{
			
				window.document.Form2.elements["License"][0].required=true;
				window.document.Form2.elements["License"][0].title="Accept License Agreement";
				window.document.Form2.elements["License"][1].required=true;
				window.document.Form2.elements["License"][1].title="Accept License Agreement";
				window.document.Form2.elements["Name"].required=true;
				window.document.Form2.elements["Title"].required=true;
				window.document.Form2.elements["Company"].required=true;
				window.document.Form2.elements["Address"].required=true;
				window.document.Form2.elements["City"].required=true;
				window.document.Form2.elements["PostalCode"].required=true;
				window.document.Form2.elements["Phone"].required=true;
				window.document.Form2.elements["Phone"].phonenumber=true;
				window.document.Form2.elements["WebSiteURL"].required=true;
				window.document.Form2.elements["Email"].required=true;
				window.document.Form2.elements["Email"].email=true;
				window.document.Form2.elements["Contact"][0].required=true;
				window.document.Form2.elements["Contact"][0].title="Contacted By UPS";
				window.document.Form2.elements["Contact"][1].required=true;
				window.document.Form2.elements["Contact"][1].title="Contacted By UPS";
				
				return ValidateForm(window.document.Form2)
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" class="GeneralPage" runat="server" id="BodyTag">
		<TABLE height="1336" cellSpacing="0" cellPadding="0" width="752" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="752" height="1336">
					<form id="Form2" method="post" runat="server">
						<TABLE height="750" cellSpacing="0" cellPadding="0" width="1273" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="1273" height="750">
									<table cellspacing="0" class="GeneralTable" height="749" width="1272">
										<tr>
											<td class="TopBanner" colspan="3">
												<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
											</td>
										</tr>
										<tr>
											<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Shipping"></uc1:TopSubBanner>
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
															<a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/shipping_cb.asp ')">
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
															<p id="ErrorAlignment" runat="server">
																<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
															</p>
															<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
																<tr>
																	<td class="ContentTable" colSpan="9" height="1">
																		<img height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1">
																		<img src="images/clear.gif" width="1"></td>
																	<td class="Content" align="middle">
																		<a class="content" href="ShippingHandling.aspx">Shipping/Handling</a>
																	</td>
																	<td class="ContentTableHeader" width="1">
																		<img src="images/clear.gif" width="1"></td>
																	<td class="Content" align="middle">
																		<a class="content" href="ShippingCarriers.aspx"><b>Carrier Based Shipping</b></a>
																	</td>
																	<td class="ContentTableHeader" width="1">
																		<img src="images/clear.gif" width="1"></td>
																	<td class="Content" align="middle">
																		<a class="content" href="ShippingValueBased.aspx">Value Based Shipping</a>
																	</td>
																	<td class="ContentTableHeader" width="1">
																		<img src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="9" height="1">
																		<img height="1" src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1">
																		<img src="images/clear.gif" width="1"></td>
																	<td class="content" vAlign="top" align="middle" colSpan="6">
																		<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
																			<tr>
																				<td class="content" align="middle">
																					<asp:Panel ID="pnlAgreement" Runat="server">
																						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																							<TR>
																								<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
																							</TR>
																							<TR>
																								<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="ContentTableHeader" noWrap align="left" colSpan="2">&nbsp;&nbsp;UPS 
																									Registration&nbsp;
																								</TD>
																								<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" vAlign="top" align="middle" colSpan="2"><IMG src="images/LOGO_S.gif">
																									<asp:TextBox id="Agreement" runat="server" TextMode="MultiLine" Columns="60" Rows="15" ReadOnly="True"></asp:TextBox><BR>
																									<asp:RadioButton id="Agree" runat="server" GroupName="License" Text="Yes, I Agree"></asp:RadioButton>&nbsp; 
																									&nbsp;
																									<asp:RadioButton id="Disagree" runat="server" GroupName="License" Text="No, I Do Not Agree" Checked="False"></asp:RadioButton><BR>
																									<A href="UPSLicense.aspx" target="_blank">Printable Version</A>
																									<BR>
																								</TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">Contact Name:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:textbox id="Name" runat="server" MaxLength="30"></asp:textbox></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">Title:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:textbox id="Title" runat="server" MaxLength="35"></asp:textbox></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">Company Name:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:textbox id="Company" runat="server" MaxLength="35"></asp:textbox></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">Street Address:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:textbox id="Address" runat="server" MaxLength="50"></asp:textbox></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">City:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:textbox id="City" runat="server" MaxLength="50"></asp:textbox></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">State:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:dropdownlist id="State" runat="server"></asp:dropdownlist></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">Country:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:dropdownlist id="Country" runat="server"></asp:dropdownlist></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">Postal Code:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:textbox id="PostalCode" runat="server" MaxLength="11"></asp:textbox></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">Phone Number:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:textbox id="Phone" runat="server" MaxLength="25"></asp:textbox></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">Web Site URL:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:textbox id="WebSiteURL" runat="server" MaxLength="254"></asp:textbox></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">E-mail Address:*</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:textbox id="Email" runat="server" MaxLength="50"></asp:textbox></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="right" colSpan="1">UPS Account Number:</TD>
																								<TD class="content" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;
																									<asp:textbox id="UPSAccountNumber" runat="server" MaxLength="10"></asp:textbox></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" noWrap align="middle" colSpan="2">To open a UPS Account, click <A href="https://www.ups.com/servlet/login?returnto=http%3a//www.ups.com/using/custserv/accountsetup/registration.html&amp;reasonCode=-1&amp;appid=OPENACCT">
																										here</A> or call 1-800-PICK-UPS</TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" align="middle" colSpan="2">I would like a UPS Sales 
																									Representative to contact me about opening a UPS shipping<BR>
																									account or to answer questions about UPS services<BR>
																									<asp:RadioButton id="Yes" runat="server" GroupName="Contact" Text="Yes"></asp:RadioButton>&nbsp; 
																									&nbsp;
																									<asp:RadioButton id="No" runat="server" GroupName="Contact" Text="No" Checked="False"></asp:RadioButton></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" align="right" colSpan="2">
																									<asp:LinkButton id="cmdSubmit" Runat="server">
																										<asp:Image BorderWidth="0" ID="imgSubmit" runat="server" ImageUrl="images/submit.jpg" AlternateText="Submit"></asp:Image>
																									</asp:LinkButton></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" align="middle" colSpan="2">UPS®, UPS &amp; Shield Design® and 
																									UNITED PARCEL SERVICE® are<BR>
																									registered trademarks of United Parcel Service of America, Inc.
																								</TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
																							</TR>
																						</TABLE>
																					</asp:Panel>
																					<asp:Panel ID="pnlSuccess" Runat="server">
																						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
																							<TR>
																								<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
																							</TR>
																							<TR>
																								<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="ContentTableHeader" noWrap align="left" colSpan="2">&nbsp;&nbsp;UPS 
																									Registration&nbsp;Successful
																								</TD>
																								<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" align="middle" colSpan="2">Registration Successful!<BR>
																									<BR>
																									Thank you for registering to use the UPS OnLine® Tools.<BR>
																									<BR>
																									To learn more about the UPS OnLine Tools, please visit <A href="http://www.ec.ups.com">
																										www.ec.ups.com</A>.<BR>
																									<BR>
																									To learn more or to begin using UPS Internet Shipping, click <A href="http://ups.com/bussol/solutions/internetship.html">
																										here</A>.
																								</TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" align="right" colSpan="2"><A href="shippingcarriers.aspx"></A></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																								<TD class="content" align="middle" colSpan="4">UPS®, UPS &amp; Shield Design® and 
																									UNITED PARCEL SERVICE® are<BR>
																									registered trademarks of United Parcel Service of America, Inc.
																								</TD>
																								<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																							</TR>
																							<TR>
																								<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
																							</TR>
																						</TABLE>
																					</asp:Panel>
																				</td>
																			</tr>
																		</table>
																	</td>
																	<td class="ContentTableHeader" width="1">
																		<img src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTable" colSpan="8" height="1">
																		<img height="1" src="images/clear.gif"></td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
												<!-- Content End --></td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</form>
				</TD>
			</TR>
			</TD></TR></TBODY></TABLE>
		</TABLE></TABLE>
	</body>
</HTML>

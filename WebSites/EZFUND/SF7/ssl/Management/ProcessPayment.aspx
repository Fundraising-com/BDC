<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProcessPayment.aspx.vb" Inherits="StoreFront.StoreFront.ProcessPayment"%>
<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="../Controls/AddressLabel.ascx" %>
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
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Process Payments "></uc1:TopSubBanner>
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
									<!-- Help Button --><a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/orderman_chgpaystat.asp ')"><img src="images/help.jpg" border="0"></a>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr class="Headings" vAlign="top">
								<td class="Headings" colSpan="2">&nbsp;Process Payment
								</td>
							</tr>
							<tr>
								<td class="content" colspan="2" align="middle">
									<p id="ErrorAlignment" runat="server"><font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</p>
								</td>
							</tr>
							<tr>
								<td class="content" vAlign="top" align="middle" colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="100%">
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="ContentTableHeader" align="left" width="50%">&nbsp;Order ID:<asp:Label CssClass="ContentTableHeader" Runat="server" ID="lblOrderID"></asp:Label></td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="ContentTableHeader" align="right" width="50%">Order Date:<asp:Label CssClass="ContentTableHeader" Runat="server" ID="lblDate"></asp:Label>&nbsp;
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="left" width="50%">
												<table cellSpacing="0" cellPadding="0" width="100%">
													<tr>
														<td width="100%"><img height="5" src="images/clear.gif"></td>
													</tr>
													<tr>
														<td class="Headings">&nbsp;Payment Details
														</td>
													</tr>
													<tr>
														<td width="100%"><img height="5" src="images/clear.gif"></td>
													</tr>
													<tr>
														<td class="content" align="middle">
															<uc1:AddressLabel id="AddressLabel1" runat="server"></uc1:AddressLabel>
															<br>
															<asp:Label Runat="server" ID="lblEmail" CssClass="content"></asp:Label>
															
														</td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="right" width="50%">
												<table class="content" cellpadding="0" cellspacing="0" width="100%">
													<tr>
														<td width="50%" class="Headings">&nbsp;</td>
														<td class="headings" width="50%">&nbsp;</td>
													</tr>
													<tr>
														<td class="content" align="right">&nbsp;&nbsp;Payment Method:</td>
														<td class="Content" align="left">&nbsp;<asp:Label Runat="server" ID="lblPaymethod" CssClass="content"></asp:Label>
														</td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="100%" id="tblCreditCard" runat="server">
													<tr>
														<td class="content" align="right">&nbsp;&nbsp;
														</td>
													</tr>
													<tr>
														<td class="Content" align="right" width="50%">&nbsp;&nbsp;Credit&nbsp;Card&nbsp;Number:</td>
														<td class="Content" align="left" width="50%">&nbsp;<asp:Label ID="lblCCNumber" Runat="server" CssClass="Content"></asp:Label></td>
													</tr>
													<tr>
														<td class="content" align="right" width="50%">&nbsp;&nbsp;Credit&nbsp;Card&nbsp;Type:</td>
														<td class="Content" align="left" width="50%">&nbsp;<asp:Label ID="lblCCType" Runat="server"></asp:Label></td>
													</tr>
													<tr>
														<td class="Content" align="right" width="50%">&nbsp;&nbsp;Expiration&nbsp;Date:</td>
														<td class="Content" align="left" width="50%">&nbsp;<asp:Label ID="lblExpireDate" Runat="server"></asp:Label></td>
													</tr>
													<tr>
														<td class="content" align="right">&nbsp;
														</td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="100%" id="tblPurchaseOrder" runat="server">
													<tr>
														<td width="50%" class="Headings">&nbsp;</td>
														<td class="headings" width="50%">&nbsp;</td>
													</tr>
													<tr>
														<td class="Content" align="right" width="50%">&nbsp;&nbsp;PO&nbsp;Number:</td>
														<td class="Content" align="left" width="50%">&nbsp;<asp:Label ID="lblPONumber" Runat="server"></asp:Label></td>
													</tr>
													<tr>
														<td class="content" align="right">&nbsp;</td>
													</tr>
												</table>
												<table cellpadding="0" cellspacing="0" width="100%" id="tblECheck" runat="server">
													<tr>
														<td width="50%" class="Headings">&nbsp;</td>
														<td class="headings" width="50%">&nbsp;</td>
													</tr>
													<tr>
														<td class="Content" align="right" width="50%">&nbsp;&nbsp;Bank&nbsp;Name:</td>
														<td class="Content" align="left" width="50%">&nbsp;<asp:Label ID="lblBankName" Runat="server"></asp:Label></td>
													</tr>
													<tr>
														<td class="Content" align="right" width="50%">&nbsp;&nbsp;Routing&nbsp;Number:</td>
														<td class="Content" align="left" width="50%">&nbsp;<asp:Label ID="lblRoutingNumber" Runat="server"></asp:Label></td>
													</tr>
													<tr>
														<td class="Content" align="right" width="50%">&nbsp;&nbsp;Account&nbsp;Number:</td>
														<td class="Content" align="left" width="50%">&nbsp;<asp:Label ID="lblAccountNumber" Runat="server"></asp:Label></td>
													</tr>
													<tr>
														<td class="content" align="right">&nbsp;</td>
													</tr>
													<tr>
														<td class="content" align="right" width="50%">
															<asp:LinkButton ID="printEcheck" text="Print E-Check" Runat="server"></asp:LinkButton>
														</td>
														<td class="Content" width="50%">&nbsp;
														</td>
													</tr>
												</table>
												<table cellpadding="0" cellspacing="0" width="100%">
													<tr>
														<td class="content" width="100%" align="right">
															<asp:LinkButton ID="cmdProcess" Runat="server">
																<asp:Image BorderWidth="0" ID="imgProcess" runat="server" ImageUrl="images/process.jpg" AlternateText="Process"></asp:Image>
															</asp:LinkButton>
														</td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="6" height="1"><img height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

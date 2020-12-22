<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="ShippingCarriersControl" Src="Controls/ShippingCarriersControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ShippingCarriers.aspx.vb" Inherits="StoreFront.StoreFront.ShippingCarriers"%>
<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.0.0

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
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
		<script language="javascript">
		
			function SetValidation()
			{
				
				
				if (window.document.Form2.elements["ShippingCarriers:CPActive"].checked==true)
					{
					window.document.Form2.elements["ShippingCarriers:CPUserName"].title="CanadaPost User Name";
					window.document.Form2.elements["ShippingCarriers:CPUserName"].required=true;
					}
				else
					{
					window.document.Form2.elements["ShippingCarriers:CPUserName"].required=false;
					}
				if (window.document.Form2.elements["ShippingCarriers:FEActive"].checked==true)
					{
					window.document.Form2.elements["ShippingCarriers:FedExUserName"].title="FedEx Account Number";
					window.document.Form2.elements["ShippingCarriers:FedExUserName"].required=true;
					}
				else
					{
					window.document.Form2.elements["ShippingCarriers:FedExUserName"].required=false;
					}
				if (window.document.Form2.elements["ShippingCarriers:USPSActive"].checked==true)
					{
					window.document.Form2.elements["ShippingCarriers:USPSUserName"].title="USPS User Name";
					window.document.Form2.elements["ShippingCarriers:USPSUserName"].required=true;
					window.document.Form2.elements["ShippingCarriers:USPSPassword"].title="USPS Password";
					window.document.Form2.elements["ShippingCarriers:USPSPassword"].required=true;
					}
				else
					{
					window.document.Form2.elements["ShippingCarriers:USPSUserName"].required=false;
					window.document.Form2.elements["ShippingCarriers:USPSPassword"].required=false;
					}

				if (window.document.Form2.elements["ShippingCarriers:LTLActive"].checked==true)
					{
					window.document.Form2.elements["ShippingCarriers:LTLUserName"].title="FreightQuote User Name";
					window.document.Form2.elements["ShippingCarriers:LTLUserName"].required=true;
					window.document.Form2.elements["ShippingCarriers:LTLPassword"].title="FreightQuote Password";
					window.document.Form2.elements["ShippingCarriers:LTLPassword"].required=true;
					}
				else
					{
					window.document.Form2.elements["ShippingCarriers:LTLUserName"].required=false;
					window.document.Form2.elements["ShippingCarriers:LTLPassword"].required=false;
					}
				return ValidateForm(window.document.Form2)
				
			}
			
		</script>
	</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<!-- Top Banner Start -->
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td class="TopBanner2" width="20%"><img src="images/sflogo.jpg"></td>
								<td class="TopBanner">&nbsp;&nbsp;Merchant Tools</td>
							</tr>
						</table>
						<!-- Top Banner End -->
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<!-- Top Sub Banner Start --> Shipping 
						<!-- Top Sub Banner End -->
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs/shipping_cb.asp ')">
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
								<td class="content" align="middle">
									<p id="ErrorAlignment" runat="server">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
											<asp:label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:label>
										</font>
									</p>
								</td>
							</tr>
							<tr>
								<td class="content" vAlign="top" align="middle" colSpan="2">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="9" height="1" width="100%">
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
									</table>
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content" vAlign="top" align="middle" colSpan="6">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="middle">
															<uc1:ShippingCarriersControl id="ShippingCarriers" runat="server"></uc1:ShippingCarriersControl>
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
		</form>
	</body>
</HTML>

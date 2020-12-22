<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ShippingHandling.aspx.vb" Inherits="StoreFront.StoreFront.ShippingHandling" %>
<%@ Register TagPrefix="uc1" TagName="ShippingHandlingControl" Src="Controls/ShippingHandlingControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Import Namespace="Storefront.systembase"%>
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
		<script language="javascript">
		
			function SetValidation()
			{
				
				
				window.document.Form2.elements["ShippingHandlingControl1:ShippingMinimum"].number=true;
				if (window.document.Form2.elements["ShippingHandlingControl1:ShippingMinimum"].value=="")
					{window.document.Form2.elements["ShippingHandlingControl1:ShippingMinimum"].value="0"}
				window.document.Form2.elements["ShippingHandlingControl1:ShippingMinimum"].title="Minimum Shipping Charge";
				window.document.Form2.elements["ShippingHandlingControl1:HandlingCharge"].number=true;
				if (window.document.Form2.elements["ShippingHandlingControl1:HandlingCharge"].value=="")
					{window.document.Form2.elements["ShippingHandlingControl1:HandlingCharge"].value="0"}
				window.document.Form2.elements["ShippingHandlingControl1:HandlingCharge"].title="Handling Charge";
				window.document.Form2.elements["ShippingHandlingControl1:MultipleShipToCharge"].number=true;
				if (window.document.Form2.elements["ShippingHandlingControl1:MultipleShipToCharge"].value=="")
					{window.document.Form2.elements["ShippingHandlingControl1:MultipleShipToCharge"].value="0"}
				window.document.Form2.elements["ShippingHandlingControl1:MultipleShipToCharge"].title="Fee Per Additional Address";
				window.document.Form2.elements["ShippingHandlingControl1:PremShipCharge"].number=true;
				if (window.document.Form2.elements["ShippingHandlingControl1:PremShipCharge"].value=="")
					{window.document.Form2.elements["ShippingHandlingControl1:PremShipCharge"].value="0"}
				window.document.Form2.elements["ShippingHandlingControl1:PremShipCharge"].title="Premium Shipping Charge";
			
				if (window.document.Form2.elements["ShippingHandlingControl1:ActivatePremium"].checked==true)
					{
					window.document.Form2.elements["ShippingHandlingControl1:PremShipName"].title="Premium Shipping Name";
					window.document.Form2.elements["ShippingHandlingControl1:PremShipName"].required=true;
					}
				else
					{
					window.document.Form2.elements["ShippingHandlingControl1:PremShipName"].required=false;
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
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/shipping_genset.asp ')">
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
								<td class="content" align="center">
									<p id="ErrorAlignment" runat="server">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
											<asp:label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:label>
										</font>
									</p>
								</td>
							</tr>
							<tr>
								<td class="content" vAlign="top" align="center" colSpan="2">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="9" height="1" width="100%">
												<img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="center"><a class="content" href="ShippingHandling.aspx"><b>Shipping/Handling</b></a>
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">
											<a class=content href='ShippingCarriers.aspx'>&nbsp;Carrier Based Shipping&nbsp;</a>
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="center"><a class=content href='ShippingValueBased.aspx'>&nbsp;Value Based Shipping&nbsp;</a>
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
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
											<td class="content" vAlign="top" align="center" colSpan="6">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="center">
															<uc1:ShippingHandlingControl id="ShippingHandlingControl1" runat="server"></uc1:ShippingHandlingControl>
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
			</TD></TR></TD></TR></TABLE></TABLE></form>
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>

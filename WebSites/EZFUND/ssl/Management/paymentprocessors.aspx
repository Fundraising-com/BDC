<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PaymentProcessors.aspx.vb" Inherits="StoreFront.StoreFront.PaymentProcessors"%>
<%@ Register TagPrefix="uc1" TagName="PaymentProcessorsControl" Src="Controls/PaymentProcessorsControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
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
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
				<script language="javascript">
		
			function SetValidation()
			{
				if (window.document.Form2.elements["PaymentProcessors:SFPassword"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:SFPassword"].required=true;
					window.document.Form2.elements["PaymentProcessors:SFPassword"].title="Account Token";
					if((window.document.Form2.elements["PaymentProcessors:SFPassword"].value.length % 2)!= 0)
						{alert("The token that you entered is not valid");
						return false;
						}
					}
			
			    if (window.document.Form2.elements["PaymentProcessors:PDPassword"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:PDPassword"].required=true;
					window.document.Form2.elements["PaymentProcessors:PDPassword"].title="Account Token";
					if((window.document.Form2.elements["PaymentProcessors:PDPassword"].value.length % 2)!= 0)
						{alert("The token that you entered is not valid");
						return false;
						}
					}
			    
				if (window.document.Form2.elements["PaymentProcessors:CSMerchantID"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:CSMerchantID"].required=true;
					window.document.Form2.elements["PaymentProcessors:CSMerchantID"].title="Merchant ID";
					window.document.Form2.elements["PaymentProcessors:CSAuth"].required=true;
					window.document.Form2.elements["PaymentProcessors:CSAuth"].title="Authorize/Capture";
					}
					
					
				if (window.document.Form2.elements["PaymentProcessors:FPMerchantID"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:FPMerchantID"].required=true;
					window.document.Form2.elements["PaymentProcessors:FPMerchantID"].title="Merchant ID";
					window.document.Form2.elements["PaymentProcessors:FPUserID"].required=true;
					window.document.Form2.elements["PaymentProcessors:FPUserID"].title="User ID";
					}

				if (window.document.Form2.elements["PaymentProcessors:LPMerchantID"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:LPMerchantID"].required=true;
					window.document.Form2.elements["PaymentProcessors:LPMerchantID"].title="Merchant ID";
					window.document.Form2.elements["PaymentProcessors:LPAuth"].required=true;
					window.document.Form2.elements["PaymentProcessors:LPAuth"].title="Authorize/Capture";
					}

				if (window.document.Form2.elements["PaymentProcessors:PGMerchantID"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:PGMerchantID"].required=true;
					window.document.Form2.elements["PaymentProcessors:PGMerchantID"].title="Merchant ID";
					window.document.Form2.elements["PaymentProcessors:PGAuth"].required=true;
					window.document.Form2.elements["PaymentProcessors:PGAuth"].title="Authorize/Capture";
					}

				if (window.document.Form2.elements["PaymentProcessors:SPMerchantID"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:SPMerchantID"].required=true;
					window.document.Form2.elements["PaymentProcessors:SPMerchantID"].title="Merchant ID";
					}

				if (window.document.Form2.elements["PaymentProcessors:VSMerchantID"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:VSMerchantID"].required=true;
					window.document.Form2.elements["PaymentProcessors:VSMerchantID"].title="Partner ID";
					window.document.Form2.elements["PaymentProcessors:VSUserName"].required=true;
					window.document.Form2.elements["PaymentProcessors:VSUserName"].title="User Name";
					window.document.Form2.elements["PaymentProcessors:VSPassword"].required=true;
					window.document.Form2.elements["PaymentProcessors:VSPassword"].title="Password";
					window.document.Form2.elements["PaymentProcessors:VSAuth"].required=true;
					window.document.Form2.elements["PaymentProcessors:VSAuth"].title="Authorize/Capture";
					}

				if (window.document.Form2.elements["PaymentProcessors:BAMerchantID"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:BAMerchantID"].required=true;
					window.document.Form2.elements["PaymentProcessors:BAMerchantID"].title="Merchant ID";
					window.document.Form2.elements["PaymentProcessors:BAUserName"].required=true;
					window.document.Form2.elements["PaymentProcessors:BAUserName"].title="User ID";
					window.document.Form2.elements["PaymentProcessors:BAPassword"].required=true;
					window.document.Form2.elements["PaymentProcessors:BAPassword"].title="Password";
					window.document.Form2.elements["PaymentProcessors:BAAuth"].required=true;
					window.document.Form2.elements["PaymentProcessors:BAAuth"].title="Authorize/Capture";
					}					

				if (window.document.Form2.elements["PaymentProcessors:IGLogin"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:IGLogin"].required=true;
					window.document.Form2.elements["PaymentProcessors:IGLogin"].title="Login";
					}

				if (window.document.Form2.elements["PaymentProcessors:PPMerchantID"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:PPMerchantID"].required=true;
					window.document.Form2.elements["PaymentProcessors:PPMerchantID"].title="Business";
					}

				if (window.document.Form2.elements["PaymentProcessors:WPMerchantID"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:WPMerchantID"].required=true;
					window.document.Form2.elements["PaymentProcessors:WPMerchantID"].title="Installation ID";
					}

				if (window.document.Form2.elements["PaymentProcessors:SSUserName"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:SSUserName"].required=true;
					window.document.Form2.elements["PaymentProcessors:SSUserName"].title="Merchant ID";
					window.document.Form2.elements["PaymentProcessors:SSPassword"].required=true;
					window.document.Form2.elements["PaymentProcessors:SSPassword"].title="Password";
					window.document.Form2.elements["PaymentProcessors:SSAuth"].required=true;
					window.document.Form2.elements["PaymentProcessors:SSAuth"].title="Authorize/Capture";
					}
				
				if (window.document.Form2.elements["PaymentProcessors:PPUserName"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:PPUserName"].required=true;
					window.document.Form2.elements["PaymentProcessors:PPUserName"].title="Client ID";
					window.document.Form2.elements["PaymentProcessors:PPPassword"].required=true;
					window.document.Form2.elements["PaymentProcessors:PPPassword"].title="Password";
					window.document.Form2.elements["PaymentProcessors:PPAuth"].required=true;
					window.document.Form2.elements["PaymentProcessors:PPAuth"].title="Authorize/Capture";
					}
					
				if (window.document.Form2.elements["PaymentProcessors:ANUserName"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:ANUserName"].required=true;
					window.document.Form2.elements["PaymentProcessors:ANUserName"].title="Client ID";
					window.document.Form2.elements["PaymentProcessors:ANPassword"].required=true;
					window.document.Form2.elements["PaymentProcessors:ANPassword"].title="Password";
					window.document.Form2.elements["PaymentProcessors:ANAuth"].required=true;
					window.document.Form2.elements["PaymentProcessors:ANAuth"].title="Authorize/Capture";
					}
				
				if (window.document.Form2.elements["PaymentProcessors:QCUserName"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:QCUserName"].required=true;
					window.document.Form2.elements["PaymentProcessors:QCUserName"].title="Merchant Login";
					window.document.Form2.elements["PaymentProcessors:QCPassword"].required=true;
					window.document.Form2.elements["PaymentProcessors:QCPassword"].title="Password";
					window.document.Form2.elements["PaymentProcessors:QCAuth"].required=true;
					window.document.Form2.elements["PaymentProcessors:QCAuth"].title="Authorize/Capture";
					}
					
				if (window.document.Form2.elements["PaymentProcessors:BCUserName"] != null)
					{
					window.document.Form2.elements["PaymentProcessors:BCUserName"].required=true;
					window.document.Form2.elements["PaymentProcessors:BCUserName"].title="Client ID";
					window.document.Form2.elements["PaymentProcessors:BCPassword"].required=true;
					window.document.Form2.elements["PaymentProcessors:BCPassword"].title="Password";
					window.document.Form2.elements["PaymentProcessors:BCAuth"].required=true;
					window.document.Form2.elements["PaymentProcessors:BCAuth"].title="Authorize/Capture";
					}
				return ValidateForm(window.document.Form2)
			}
			
			
		</script>

	</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" enctype="multipart/form-data" method="post" runat="server">
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
						<!-- Top Sub Banner Start --> Payments 
						<!-- Top Sub Banner End --></td>
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
								<td class="content" align="middle">
									<p id="ErrorAlignment" runat="server">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</p>
								</td>
							</tr>
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs/olpayproc_ov.asp ')">
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
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="5" height="1" width="100%">
												<img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<a class="content" href="PaymentMethods.aspx">Payment Methods</a>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<a class="content" href="PaymentProcessors.aspx"><b>Online Processing</b></a>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="5" height="1">
												<img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content" vAlign="top" align="middle" colSpan="3">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="middle">
															<uc1:PaymentProcessorsControl id="PaymentProcessors" runat="server"></uc1:PaymentProcessorsControl>
														</td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="5" height="1">
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

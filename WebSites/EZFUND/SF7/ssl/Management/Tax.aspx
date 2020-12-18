<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Tax.aspx.vb" Inherits="StoreFront.StoreFront.Tax" %>
<%@ Register TagPrefix="uc1" TagName="TaxControl" Src="Controls/TaxControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
		
			function SetValidationAddCountry()
			{
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["TaxControl1:NewCountryRate"].number=true;
				window.document.Form2.elements["TaxControl1:NewCountryRate"].required=true;
				window.document.Form2.elements["TaxControl1:NewCountryRate"].title="Tax Rate";
			
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationAddState()
			{
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["TaxControl1:NewStateRate"].number=true;
				window.document.Form2.elements["TaxControl1:NewStateRate"].required=true;
				window.document.Form2.elements["TaxControl1:NewStateRate"].title="Tax Rate";
			
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationAddLocal()
			{
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["TaxControl1:NewLocalRate"].number=true;
				window.document.Form2.elements["TaxControl1:NewLocalRate"].required=true;
				window.document.Form2.elements["TaxControl1:NewLocalRate"].title="Tax Rate";
				window.document.Form2.elements["TaxControl1:NewLocalCode"].required=true;
				window.document.Form2.elements["TaxControl1:NewLocalCode"].title="Local Postal Code";
			
				return ValidateForm(window.document.Form2)
			}
			function SetValidationSave()
			{
				ResetForm(window.document.Form2);
				
				for (var i = 0; i < window.document.Form2.length; i++) 
				{
					e = window.document.Form2.elements[i]
					
					if (e.type=="text" && e.name.indexOf("Rate")>-1 && e.name != "TaxControl1:NewCountryRate" && e.name != "TaxControl1:NewStateRate"  && e.name != "TaxControl1:NewLocalRate")
					{
						e.number=true;
						e.required=true;
						e.title="Tax Rate";
						
					}
				}
				
				return ValidateForm(window.document.Form2)
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Tax"></uc1:TopSubBanner>
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
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/tax_ov.asp ')"><img src="images/help.jpg" border="0"></a>
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
										</font>
									</p>
								</td>
							</tr>
							<tr>
								<td class="content" vAlign="top" align="middle" colSpan="2">
									<table class="content" cellspacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="content" vAlign="top" align="middle" colSpan="6">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="middle">
															<uc1:TaxControl id="TaxControl1" runat="server"></uc1:TaxControl>
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
						<!-- Content End -->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

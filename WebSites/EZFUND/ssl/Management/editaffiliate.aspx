<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AffiliateAddressCtrl" Src="Controls/AffiliateAddressCtrl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="editaffiliate.aspx.vb" Inherits="StoreFront.StoreFront.editaffiliate"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
			
			window.document.Form2.elements["AffiliateAddressCtrl1:FirstName"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:FirstName"].title="First Name";
			window.document.Form2.elements["AffiliateAddressCtrl1:LastName"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:LastName"].title="Last Name";
			window.document.Form2.elements["AffiliateAddressCtrl1:Address1"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:Address1"].title="Address";
			window.document.Form2.elements["AffiliateAddressCtrl1:City"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:City"].title="City";
			
			window.document.Form2.elements["AffiliateAddressCtrl1:State"].required=false;
			window.document.Form2.elements["AffiliateAddressCtrl1:Zip"].required=false;
			if(window.document.Form2.elements["AffiliateAddressCtrl1:Country"].value=="US")
				{
				window.document.Form2.elements["AffiliateAddressCtrl1:State"].title="State";
				window.document.Form2.elements["AffiliateAddressCtrl1:State"].required=true;
				window.document.Form2.elements["AffiliateAddressCtrl1:Zip"].title="Postal Code";
				window.document.Form2.elements["AffiliateAddressCtrl1:Zip"].required=true;
				}
			
			if(window.document.Form2.elements["AffiliateAddressCtrl1:Country"].value=="CA")
				{
				window.document.Form2.elements["AffiliateAddressCtrl1:Zip"].title="Postal Code";
				window.document.Form2.elements["AffiliateAddressCtrl1:Zip"].required=true;
				}
			
			
			window.document.Form2.elements["AffiliateAddressCtrl1:Phone"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:Phone"].title="Phone";
			window.document.Form2.elements["AffiliateAddressCtrl1:Phone"].phonenumber=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:Fax"].phonenumber=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:Fax"].title="Fax";
			window.document.Form2.elements["AffiliateAddressCtrl1:WebSite"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:WebSite"].title="Web Site Address";
			window.document.Form2.elements["AffiliateAddressCtrl1:EMail"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:EMail"].title="E-Mail";
			window.document.Form2.elements["AffiliateAddressCtrl1:EMail"].email=true;
			//window.document.Form2.elements["AffiliateAddressCtrl1:password"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:password"].title="Password";
			window.document.Form2.elements["AffiliateAddressCtrl1:password"].password=true;
			//window.document.Form2.elements["AffiliateAddressCtrl1:Confirmpassword"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:Confirmpassword"].title="Confirm Password";
			window.document.Form2.elements["AffiliateAddressCtrl1:Confirmpassword"].password=true;
			//window.document.Form2.elements["AffiliateAddressCtrl1:txtPercent"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:txtPercent"].title="Commission Percent";
			window.document.Form2.elements["AffiliateAddressCtrl1:txtPercent"].number=true;
			if (window.document.Form2.elements["AffiliateAddressCtrl1:txtPercent"].value=="")
			{window.document.Form2.elements["AffiliateAddressCtrl1:txtPercent"].value="0"}
			//window.document.Form2.elements["AffiliateAddressCtrl1:txtFlatFee"].required=true;
			window.document.Form2.elements["AffiliateAddressCtrl1:txtFlatFee"].title="Commission Flat Fee";
			window.document.Form2.elements["AffiliateAddressCtrl1:txtFlatFee"].number=true;
			if (window.document.Form2.elements["AffiliateAddressCtrl1:txtFlatFee"].value=="")
			{window.document.Form2.elements["AffiliateAddressCtrl1:txtFlatFee"].value="0"}
			
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
						<!-- Top Sub Banner Start --> Affiliates 
						<!-- Top Sub Banner End -->
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End -->
					</td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="content" align="middle">
									<P id="ErrorAlignment" runat="server" align="center">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</P>
								</td>
							</tr>
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<A href="javascript: doHelp(' http://support.storefront.net/mtdocs/afftpp_ed.asp ')">
										<img src="images/help.jpg" border="0"></A> 
									<!-- End Help Button -->
								</td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End -->
								</td>
							</tr>
							<TR>
								<TD class="content" vAlign="top" align="middle">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="9" height="1" width="100%"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<A class="content" href="affiliatepayments.aspx">Manage Payments</A>
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<A class="content" href="affiliatelist.aspx"><b>Manage Affiliates</b></A>
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<A class="content" href="addaffliate.aspx">Add Affiliates</A>
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<A class="content" href="affliatesettings.aspx">Settings</A>
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<TR>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<TD class="content" vAlign="top" align="middle" colSpan="7">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="middle">
															<uc1:AffiliateAddressCtrl id="AffiliateAddressCtrl1" runat="server"></uc1:AffiliateAddressCtrl>
														</td>
													</tr>
												</table>
											</TD>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</TR>
										<tr>
											<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</TD>
							</TR>
						</table>
						<!-- Content End -->
					</td>
				</tr>
			</table>
			</TD></TR></TABLE>
		</form>
	</body>
</HTML>

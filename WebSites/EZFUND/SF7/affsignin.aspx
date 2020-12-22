<%@ Page Language="vb" AutoEventWireup="false" Codebehind="affsignin.aspx.vb" Inherits="StoreFront.StoreFront.affsignin"%>
<%@ Register TagPrefix="uc1" TagName="Affiliatesignin" Src="Controls/Affiliatesignin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HEAD>
	<title>
		<% writeTitle() %>
		- Affiliate Signin</title>
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
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
	<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="general.js"></script>
	<% Me.PageHeader %>
	<script language="javascript">
		<!--
			function SetValidationSignIn()
			{
			
				ResetForm(window.document.Form2);

				window.document.Form2.elements["Affiliatesignin1:txtSIEMail"].required=true;
				window.document.Form2.elements["Affiliatesignin1:txtSIEMail"].email=true;
				window.document.Form2.elements["Affiliatesignin1:txtSIEMail"].title="E-Mail Address";
				window.document.Form2.elements["Affiliatesignin1:txtSIPassword"].required=true;
				window.document.Form2.elements["Affiliatesignin1:txtSIPassword"].title="Password";
				return ValidateForm(window.document.Form2);
				
			}
			
			function SetValidationNew()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["Affiliatesignin1:txtCAEMail"].required=true;
				window.document.Form2.elements["Affiliatesignin1:txtCAEMail"].email=true;
				window.document.Form2.elements["Affiliatesignin1:txtCAEMail"].title="E-Mail Address";
				window.document.Form2.elements["Affiliatesignin1:txtCAPassword"].required=true;
				window.document.Form2.elements["Affiliatesignin1:txtCAPassword"].password=true;
				window.document.Form2.elements["Affiliatesignin1:txtCAPassword"].title="Password";
				window.document.Form2.elements["Affiliatesignin1:txtCAConfirmPassword"].required=true;
				window.document.Form2.elements["Affiliatesignin1:txtCAConfirmPassword"].password=true;
				window.document.Form2.elements["Affiliatesignin1:txtCAConfirmPassword"].title="Confirmation Password";
				return ValidateForm(window.document.Form2)
			}
			function onReturn()
			{
				if (event.keyCode == 13)
					document.all["btnSignIn"].click();
			}
		
			//-->
	</script>
</HEAD>
<body id="BodyTag" runat="server" class="generalpage">
	<form id="Form2" method="post" runat="server">
		<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
			<tr>
				<td id="PageCell">
					<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1"
						runat="server">
						<tr>
							<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
								<!-- Top Banner Start -->
								<uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
								<!-- Top Banner End -->
							</td>
						</tr>
						<tr>
							<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
								<!-- Top Sub Banner Start -->
								<uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
								<!-- Top Sub Banner End -->
							</td>
						</tr>
						<tr>
							<td class="LeftColumn" id="LeftColumnCell">
								<!-- Left Column Start -->
								<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
								<!-- Left Column End -->
							</td>
							<td class="Content" vAlign="top" id="ContentCell">
								<!-- Content Start -->
								<table cellSpacing="3" cellPadding="5" width="100%" border="0">
									<tr>
										<td class="Instructions">
											<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
											<!-- Instruction End --></td>
									</tr>
									<tr>
										<td class="content">
											<P id="ErrorAlignment" runat="server" align="center">
												<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label>
											</P>
										</td>
									</tr>
									<tr>
										<td class="Content" align="left">
											<uc1:Affiliatesignin id="Affiliatesignin1" runat="server"></uc1:Affiliatesignin>
										</td>
									</tr>
								</table>
								<!-- Content End -->
							</td>
							<td class="RightColumn" id="RightColumnCell">
								<!-- Right Column Start -->
								<uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
								<!-- Right Column End -->
							</td>
						</tr>
						<tr>
							<td colSpan="3" class="Footer" id="FooterCell">
								<!-- Footer Start -->
								<uc1:footer id="Footer1" runat="server"></uc1:footer>
								<!-- Footer End -->
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		</TD></TR></TABLE>
	</form>
	</TD></TR></TABLE>
</body>

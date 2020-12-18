<%@ Register TagPrefix="uc1" TagName="AdminLists" Src="Controls/AdminLists.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddEditAdmin" Src="Controls/AddEditAdmin.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ManageAdministrators.aspx.vb" Inherits="StoreFront.StoreFront.ManageAdministrators" %>
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
		<script language="javascript">
			String.prototype.trim = function() {
					return this.replace(/^\s+|\s+$/g,"");
			}
			
			function SetValidation()
			{
			if(window.document.Form2.elements["AddEditAdmin1:txtPassword"].value.trim() == "")
			{
				window.document.Form2.elements["AddEditAdmin1:txtPassword"].value=""
			}
			window.document.Form2.elements["AddEditAdmin1:txtFName"].required=true;
			window.document.Form2.elements["AddEditAdmin1:txtFName"].title="First Name";
			window.document.Form2.elements["AddEditAdmin1:txtLName"].required=true;
			window.document.Form2.elements["AddEditAdmin1:txtLName"].title="Last Name";
			window.document.Form2.elements["AddEditAdmin1:txtUsername"].required=true;
			window.document.Form2.elements["AddEditAdmin1:txtUsername"].title="User Name";
			window.document.Form2.elements["AddEditAdmin1:txtPassword"].required=true;
			window.document.Form2.elements["AddEditAdmin1:txtPassword"].title="Password";
			window.document.Form2.elements["AddEditAdmin1:ddlRoles"].required=true;
			window.document.Form2.elements["AddEditAdmin1:ddlRoles"].title="Roles";
			 return ValidateForm(window.document.Form2)
			
			}
			
		
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server" MS_POSITIONING="FlowLayout">
		<form id="Form2" method="post" runat="server">
			<input id="txtGroupIDHidden" type="hidden" name="txtGroupIDHidden" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<tr>
					<td class="TopBanner" colspan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Manage Administrators"></uc1:TopSubBanner>
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table cellSpacing="3" cellPadding="5" width="100%" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/admin_ov.asp ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="content">
									<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="lblErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></P>
								</td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<TR>
								<TD class="content" vAlign="top" align="center" colSpan="2">
									<table cellSpacing="0" cellPadding="0" align=center width="100%" border="0">
										<tr>
											<td class="contenttable" width="1"><img src="images/clear.gif"></td>
											<td class="contenttable"><img src="images/clear.gif"></td>
											<td class="contenttable" width="1"><img src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="contenttable"><img src="images/clear.gif"></td>
											<td width="100%" class="content">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td width="100%"><uc1:AdminLists id="AdminLists1" runat="server"></uc1:AdminLists></td>
													</tr>
													<tr>
														<td width="100%"><uc1:AddEditAdmin id="AddEditAdmin1" runat="server"></uc1:AddEditAdmin></td>
													</tr>
												</table>
											</td>
											<td class="contenttable"><img src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="contenttable" height="1"></td>
											<td class="contenttable" width="100%" height="1"></td>
											<td class="contenttable" height="1"></td>
										</tr>
									</table>
								</TD>
							</TR>
						</table>
						<!-- Content End -->
					</td>
				</tr>
			</table>
		<INPUT id="hdnAdminId" type="hidden" value="0" name="hdnAdminId" runat="server"></form>
	</body>
</HTML>

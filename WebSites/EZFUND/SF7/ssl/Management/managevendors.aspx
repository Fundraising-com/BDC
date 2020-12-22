<%@ Page Language="vb" AutoEventWireup="false" Codebehind="managevendors.aspx.vb" Inherits="StoreFront.StoreFront.managevendors"%>
<%@ Register TagPrefix="ucl" TagName="addvendorcontrol" Src="Controls/addvendorcontrol.ascx" %>
<%@ Register TagPrefix="ucl" TagName="editvendorcontrol" Src="Controls/editvendorcontrol.ascx" %>
<%@ Register TagPrefix="ucl" TagName="StandardSearchControl" Src="Controls/StandardSearchControl.ascx" %>
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
		<script language="javascript">
		
			function SetValidationAdd()
			{
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["AddVendorControl:txtVendorName"].required=true;
				window.document.Form2.elements["AddVendorControl:txtVendorName"].title="Vendor Name";
				if (window.document.Form2.elements["AddVendorControl:txtEMail"] != null)
					{window.document.Form2.elements["AddVendorControl:txtEMail"].email=true;
					window.document.Form2.elements["AddVendorControl:txtEMail"].title="E-Mail Address";
					}
			
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationEdit()
			{
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["EditVendorControl:txtVendorName"].required=true;
				window.document.Form2.elements["EditVendorControl:txtVendorName"].title="Vendor Name";
				if (window.document.Form2.elements["EditVendorControl:txtEMail"] != null)
					{window.document.Form2.elements["EditVendorControl:txtEMail"].email=true;
					window.document.Form2.elements["EditVendorControl:txtEMail"].title="E-Mail Address";
					}
				
				return ValidateForm(window.document.Form2)
			}
			
			
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout" class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Vendors"></uc1:TopSubBanner>
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
									<!-- Help Button --><a href="javascript: doHelp(' http://support.storefront.net/mtdocs/orginv_manven_ov.asp ')"><img src="images/help.jpg" border="0"></a>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction2" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="content" vAlign="top" align="middle" colSpan="2">
									<p id="ErrorAlignment" runat="server" align="center"><font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</p>
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colspan="3" height="1"><img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="left">
												<table class="Content" cellpadding="5" width="100%">
													<tr>
														<td class="Content">
															<ucl:standardsearchcontrol id="StandardSearchControl1" runat="server"></ucl:standardsearchcontrol>
															<ucl:editvendorcontrol id="EditVendorControl" runat="server"></ucl:editvendorcontrol>
															<ucl:addvendorcontrol id="AddVendorControl" runat="server"></ucl:addvendorcontrol>
														</td>
													</tr>
													<tr>
														<td class="Content">
															<asp:LinkButton ID="btnAdd" Runat="server">
																<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="images/add_new.jpg" AlternateText="Add"></asp:Image>
															</asp:LinkButton>
														</td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colspan="3" height="1"><img height="1" src="images/clear.gif"></td>
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

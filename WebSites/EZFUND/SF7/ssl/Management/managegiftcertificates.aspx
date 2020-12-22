<%@ Register TagPrefix="uc1" TagName="addgiftcertificates" Src="Controls/addgiftcertificates.ascx" %>
<%@ Register TagPrefix="uc1" TagName="editgiftcertificates" Src="Controls/editgiftcertificates.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="managegiftcertificates.aspx.vb" Inherits="StoreFront.StoreFront.managegiftcertificates"%>
<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StandardSearchControl" Src="Controls/StandardSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
			<!--
			function SetValidationAdd()
			{
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["Addgiftcertificates1:txtGiftCode"].required=true
				window.document.Form2.elements["Addgiftcertificates1:txtGiftCode"].title="Gift Certificate Code"
				window.document.Form2.elements["Addgiftcertificates1:txtAmount"].required=true
				window.document.Form2.elements["Addgiftcertificates1:txtAmount"].number=true
				window.document.Form2.elements["Addgiftcertificates1:txtAmount"].title="Amount"
				
				if (window.document.Form2.elements["Addgiftcertificates1:DropDownList1"].options[window.document.Form2.elements["Addgiftcertificates1:DropDownList1"].options.selectedIndex].value != "Never")
				{window.document.Form2.elements["Addgiftcertificates1:txtDate"].required=true
				}
				else
				{window.document.Form2.elements["Addgiftcertificates1:txtDate"].required=false
				}
				window.document.Form2.elements["Addgiftcertificates1:txtDate"].date=true
				window.document.Form2.elements["Addgiftcertificates1:txtDate"].title="Expiration Date"

				
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationEdit()
			{
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["Editgiftcertificates1:txtGiftCode"].required=true
				window.document.Form2.elements["Editgiftcertificates1:txtGiftCode"].title="Gift Certificate Code"
				window.document.Form2.elements["Editgiftcertificates1:txtAmount"].required=true
				window.document.Form2.elements["Editgiftcertificates1:txtAmount"].number=true
				window.document.Form2.elements["Editgiftcertificates1:txtAmount"].title="Amount"
				
				if (window.document.Form2.elements["Editgiftcertificates1:DropDownList1"].options[window.document.Form2.elements["Editgiftcertificates1:DropDownList1"].options.selectedIndex].value != "Never")
				{window.document.Form2.elements["Editgiftcertificates1:txtDate"].required=true
				}
				else
				{window.document.Form2.elements["Editgiftcertificates1:txtDate"].required=false
				}
				window.document.Form2.elements["Editgiftcertificates1:txtDate"].date=true
				window.document.Form2.elements["Editgiftcertificates1:txtDate"].title="Expiration Date"

				
				return ValidateForm(window.document.Form2)
			}

		

			//-->
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
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Gift Certificates"></uc1:TopSubBanner>
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
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/giftcert_ov.asp  ')">
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
								<td class="Content">
									<p id="P1" runat="server">
										<asp:label id="lblErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label>
									</p>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td class="ContentTable" colspan="3" height="1"><img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="left">
												<table class="Content" cellpadding="5" width="100%">
													<tr>
														<td class="Content">
															<uc1:StandardSearchControl id="Standardsearchcontrol1" runat="server"></uc1:StandardSearchControl>
															<uc1:editgiftcertificates id="Editgiftcertificates1" runat="server" visible="false"></uc1:editgiftcertificates>
															<uc1:addgiftcertificates id="Addgiftcertificates1" runat="server" visible="false"></uc1:addgiftcertificates></td>
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
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										<tr>
											<td class="ContentTable" colspan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
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

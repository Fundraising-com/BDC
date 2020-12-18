<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="marketplaces.aspx.vb" Inherits="StoreFront.StoreFront.marketplaces"%>
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
			if (window.document.Form2.elements["txtUserName"].value != "" || window.document.Form2.elements["txtPassword"].value != "")
			{window.document.Form2.elements["txtUserName"].required=true;
			window.document.Form2.elements["txtPassword"].required=true;
			window.document.Form2.elements["txtUserName"].title="User Name";
			window.document.Form2.elements["txtPassword"].title="Password";
			}
			else
			{window.document.Form2.elements["txtUserName"].required=false;
			window.document.Form2.elements["txtPassword"].required=false;
			}
			
			return ValidateForm(window.document.Form2)
			}
			
		
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Marketplaces"></uc1:TopSubBanner>
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End -->
					</td>
					<td class="Content" vAlign="top" width="100%">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="content" align="center">
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
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/mplaces_ov.asp ')">
										<img src="images/help.jpg" border="0"></A> 
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td>
									<asp:label id="lblMessage" CssClass="ErrorMessages" Runat="server" Visible="False"></asp:label>
								</td>
							</tr>
							<tr>
								<td class="content" vAlign="top" align="center" colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="100%">
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="ContentTableHeader" align="left" colSpan="2">&nbsp;DealTime<asp:label id="Label1" CssClass="ContentTableHeader" Runat="server"></asp:label></td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" colSpan="2" height="10"></td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td align="center" class="Content"><img src="images/dealtime.jpg"></td>
											<td align="left" class="Content">Reach millions of shoppers through DealTime now 
												owned by Shopping.com! <a href="https://merchant.shopping.com/enroll/app?service=page/PartnerWelcome" target="_blank">
													Click Here</a> to sign up for a shopping.com merchant account!&nbsp;</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="4" height="1"><img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" colSpan="2" height="10"></td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" colSpan="2" height="10">&nbsp;<asp:linkbutton id="btnCreateFile" onclick="cmdCreateFile_Click" Runat="server" CausesValidation="False">Generate&nbsp;DealTime&nbsp;Data&nbsp;File</asp:linkbutton></td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" colSpan="2" height="10"></td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td colSpan="2"><asp:label id="lblDTMessage" CssClass="ErrorMessages" Runat="server" Visible="False"></asp:label></td>
											<td class="ContentTableHeader" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" colSpan="2" height="10"></td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="4" height="1"><img height="1" src="images/clear.gif"></td>
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

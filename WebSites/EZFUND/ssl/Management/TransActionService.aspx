<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TransActionService.aspx.vb" Inherits="StoreFront.StoreFront.TransActionService"%>
<%@ Register TagPrefix="uc1" TagName="TransactionControl" Src="Controls/TransactionControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
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
		<title><% writeTitle() %>  - Merchant Tools</title>
		
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" class="GeneralPage" runat="server" id="BodyTag">

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
						<!-- Top Sub Banner Start -->
						Sales Reports 
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
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs/reports_ts.asp ')">
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
								<td class="Content" align="middle">
									<p id="ErrorAlignment" runat="server" align=center>
										<font color=ff0000>
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</p>
								</td>
							</tr>
							<tr class="Headings" vAlign="top">
								<td class="Headings">&nbsp;Transaction Services Report
								</td>
							</tr>
							<tr>
								<td class="Content">
									<asp:label id="DateInfo" Runat="server" CssClass="Content"></asp:label>
								</td>
							</tr>
							<tr>
								<td class="Content" align="middle">
									<uc1:TransactionControl id="TransactionControl1" runat="server"></uc1:TransactionControl>
								</td>
							</tr>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
			</td> </tr> </table>
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register TagPrefix="uc1" TagName="Top" Src="controls/CSRTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="confirm" Src="controls/CSRconfirmControl.ascx" %>
<%@ Page validaterequest="false" Language="vb" AutoEventWireup="false" Codebehind="CSRConfirm.aspx.vb" Inherits="StoreFront.StoreFront.CSRConfirm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Confirm</title>
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
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">

		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server" MS_POSITIONING="GridLayout">
		<form id="Form" runat="server">
			<table id="PageTable" cellSpacing="0" width="100%" runat="server">
				<tr>
					<td id="PageCell">
						<table class="GeneralTable" cellSpacing="0" width="100%" runat="server" ID="Table1">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start -->
									<!-- Top Banner End --></td>
							</tr>
							<tr align="center">
								<td class="TopSubBanner" id="TopSubBannerCell" align="center" width="100%" colSpan="3">
									<!-- Top Sub Banner Start --><ajax:ajaxpanel id="Ajaxpanel3" runat="server" width="100%">
										<uc1:Top id="Top1" runat="server" width="100%"></uc1:Top>
									</ajax:ajaxpanel>
									<!-- Top Sub Banner End --></td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell" vAlign="top" align="left">
									<!-- Left Column Start -->
									<!-- Left Column End --></td>
								<td class="Content" id="ContentCell" vAlign="top" align="center">
									<!-- Content Start -->
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
								            <td class="Content" align="right">
									            <!-- Help Button -->
									            <a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/csr_confirm.asp  ')">
										            <img src="images/help.jpg" border="0"></a> 
									            <!-- End Help Button --></td>
							            </tr>
							            <tr>
											<td>
												<!-- Instruction Start -->
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content" width="100%">
												<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></font></P>
												<ajax:ajaxpanel id="AjaxPanel1" runat="server" width="100%">
													<uc1:confirm id="Confirm1" runat="server" width="100%"></uc1:confirm>
												</ajax:ajaxpanel>
											</td>
										</tr>
									</table>
									<!-- Content End --></td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start -->
									<!-- Right Column End --></td>
							</tr>
							<tr>
								<td class="Footer" id="FooterCell" colSpan="3">
									<!-- Footer Start -->
									<!-- Footer End --></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

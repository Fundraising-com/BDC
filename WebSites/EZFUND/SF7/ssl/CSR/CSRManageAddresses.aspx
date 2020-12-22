<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CSRManageAddresses.aspx.vb" Inherits="StoreFront.StoreFront.CSRManageAddresses" %>
<%@ Register TagPrefix="uc1" TagName="CSRManageAddressesControl" Src="Controls/CSRManageAddressesControl.ascx" %>
<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			</title>
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
		function ClosePage()
{

window.opener.Form.submit();
window.close();
}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form" runat="server">
			<table id="PageTable" width="100%" cellSpacing="0" runat="server">
				<tr>
					<td id="PageCell">
						<table class="GeneralTable" id="PageSubTable" runat="server" cellspacing="0" width="100%">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start -->
									<!-- Top Banner End -->
								</td>
							</tr>
							<tr align="center">
								<td class="TopSubBanner" id="TopSubBannerCell" align="center" width="100%" colSpan="3">
									<!-- Top Sub Banner Start -->
									<!-- Top Sub Banner End -->
								</td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell" vAlign="top" align="left">
									<!-- Left Column Start -->
									<!-- Left Column End -->
								</td>
								<td class="Content" vAlign="top" id="ContentCell">
									<!-- Content Start -->
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
								            <td class="Content" align="right">
									            <!-- Help Button -->
									            <a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/csr_manageaddress.asp  ')">
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
												<P id="ErrorAlignment" runat="server" align="center">
													<font color="#ff0000">
														<asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label>
													</font>
												</P>
												<ajax:AjaxPanel id="AjaxPanel1" width="100%" runat="server">
													<uc1:CSRManageAddressesControl id="CSRManageAddressesControl1" runat="server" width="100%"></uc1:CSRManageAddressesControl>
												</ajax:AjaxPanel>
											</td>
										</tr>
									</table>
									<!-- Content End -->
								</td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start -->
									<!-- Right Column End -->
								</td>
							</tr>
							<tr>
								<td class="Footer" id="FooterCell" colSpan="3">
									<!-- Footer Start -->
									<!-- Footer End -->
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

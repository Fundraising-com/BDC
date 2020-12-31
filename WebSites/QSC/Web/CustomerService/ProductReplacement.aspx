<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl"  %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="uc1" TagName="ControlerProductMultiSelect" Src="ControlerProductMultiSelect.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerCampaignsForProductReplacement" Src="ControlerCampaignsForProductReplacement.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerProductReplacement" Src="ControlerProductReplacement.ascx" %>
<%@ Page language="c#" Codebehind="ProductReplacement.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.ProductReplacement" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Gift Replacement</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" id="BodyTag" runat="server">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<!--#include file="fctjavascriptall.js"-->
			<br>
			<table width="100%">
				<tr>
					<td width="10">&nbsp;</td>
					<td width="*">
						<asp:label id="lblPageTitle" runat="server" cssclass="CSPageTitle">Gift Replacement</asp:label><br>
						<br>
						<asp:label id="lblInstructions" runat="server" cssclass="CSPlainText"></asp:label><br>
						<br>
						<br>
						<uc1:controlercampaignsforproductreplacement id="ctrlControlerCampaignsForProductReplacement" runat="server"></uc1:controlercampaignsforproductreplacement>
						<uc1:controlerproductmultiselect id="ctrlControlerProductMultiSelect" runat="server" producttype="2" visible="false"></uc1:controlerproductmultiselect>
						<uc1:controlerproductreplacement id="ctrlControlerProductReplacement" runat="server"></uc1:controlerproductreplacement>
						<br>
					</td>
					<td width="10">&nbsp;</td>
				</tr>
			</table>
			<div style="TEXT-ALIGN: center"><asp:label id="lblConfirmation" runat="server" cssclass="CSPlainText" visible="False" font-bold="True"
					font-size="Larger"></asp:label></div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
			<cc2:enhancedsmartnavigationcontrol id="ctrlEnhancedSmartNavigationControl" runat="server"></cc2:enhancedsmartnavigationcontrol>
		</form>
		<!--#include file="errorwindow.js"-->
	</body>
</html>

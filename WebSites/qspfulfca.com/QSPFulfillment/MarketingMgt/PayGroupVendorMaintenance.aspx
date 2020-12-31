<%@ Register TagPrefix="uc1" TagName="VendorSiteMaintenanceControl" Src="Control/VendorSiteMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PayGroupMaintenanceControl" Src="Control/PayGroupMaintenanceControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page language="c#" Codebehind="PayGroupVendorMaintenance.aspx.cs" AutoEventWireup="True" enableSessionState="true" Inherits="QSPFulfillment.MarketingMgt.PayGroupVendorMaintenance" %>
<html>
	<head>
		<title>Pay Group / Vendor Maintenance</title>
		<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	</head>
	<body topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginheight="0" marginwidth="0"
		onload="return window_onunload()">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<div style="MARGIN: 3%; WIDTH: 97%; HEIGHT: 97%">
				<h3>Pay Group / Vendor Maintenance</h3>
				<br>
				<table>
					<tr valign="top">
						<td>
							<uc1:paygroupmaintenancecontrol id="ctrlPayGroupMaintenanceControl" runat="server"></uc1:paygroupmaintenancecontrol>
						</td>
						<td width="175">&nbsp;</td>
						<td>
							<uc1:vendorsitemaintenancecontrol id="ctrlVendorSiteMaintenanceControl" runat="server"></uc1:vendorsitemaintenancecontrol>
						</td>
					</tr>
				</table>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

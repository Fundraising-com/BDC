<%@ Page language="c#" Codebehind="PremiumMaintenance.aspx.cs" AutoEventWireup="True" enableSessionState="true" Inherits="QSPFulfillment.MarketingMgt.PremiumMaintenance" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="uc1" TagName="PremiumMaintenanceControl" Src="Control/PremiumMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PremiumSearchControl" Src="Control/PremiumSearchControl.ascx" %>
<html>
	<head>
		<title>Premium Maintenance</title>
		<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	</head>
	<body topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginheight="0" marginwidth="0"
		onload="return window_onunload()">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<div style="MARGIN: 3%; WIDTH: 97%; HEIGHT: 97%">
				<h3>Premium Maintenance</h3>
				<br>
				<asp:label id="lblInstructions" runat="server" cssclass="csPlainText" font-bold="True">Please select a Premium from the list:</asp:label>
				<br>
				<br>
				<uc1:premiumsearchcontrol id="ctrlPremiumSearchControl" runat="server"></uc1:premiumsearchcontrol>
				<uc1:premiummaintenancecontrol id="ctrlPremiumMaintenanceControl" runat="server" visible="false"></uc1:premiummaintenancecontrol>
				<br>
				<div align="left">
					<asp:button id="btnCreateNew" runat="server" text="Create New Premium" cssclass="boxlook" onclick="btnCreateNew_Click"></asp:button>
				</div>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

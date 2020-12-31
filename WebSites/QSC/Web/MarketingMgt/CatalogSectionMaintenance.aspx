<%@ Page language="c#" Codebehind="CatalogSectionMaintenance.aspx.cs" AutoEventWireup="True" enableSessionState="true" Inherits="QSPFulfillment.MarketingMgt.CatalogSectionMaintenance" %>
<%@ Register TagPrefix="uc1" TagName="CatalogSectionMaintenanceControl" Src="Control/CatalogSectionMaintenanceControl.ascx" %>
<html>
	<head>
		<title>Catalog Section Maintenance</title>
		<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	</head>
	<body topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginheight="0" marginwidth="0"
		onload="return window_onunload()">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<div style="MARGIN: 3%; WIDTH: 97%; HEIGHT: 97%">
				<h3>Catalog Section Maintenance</h3>
				<br>
				<br>
				<uc1:catalogsectionmaintenancecontrol id="ctrlCatalogSectionMaintenanceControl" runat="server"></uc1:catalogsectionmaintenancecontrol>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

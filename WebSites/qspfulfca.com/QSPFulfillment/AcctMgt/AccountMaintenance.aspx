<%@ Register TagPrefix="uc1" TagName="AccountMaintenanceControl" Src="Control/AccountMaintenanceControl.ascx" %>
<%@ Page language="c#" Codebehind="AccountMaintenance.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.AcctMgt.AccountMaintenance" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<html>
	<head>
		<title>Group Maintenance</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body id="BodyTag" runat="server" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0"
		marginwidth="0" marginheight="0">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<cc2:enhancedsmartnavigationcontrol id="ctrlEnhancedSmartNavigationControl" runat="server"></cc2:enhancedsmartnavigationcontrol>
			<div style="PADDING-LEFT: 5%; WIDTH: 95%"><br>
				<br>
				<h3>Group Maintenance</h3>
				<br>
				<uc1:accountmaintenancecontrol id="ctrlAccountMaintenanceControl" runat="server"></uc1:accountmaintenancecontrol></div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
			<input type="hidden" id="hidDataBind" runat="server" value="0" name="hidDataBind">
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

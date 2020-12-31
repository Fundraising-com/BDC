<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Page language="c#" debug="true" Codebehind="AccountContactListMaintenance.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.AcctMgt.AccountContactListMaintenance" %>
<%@ Register TagPrefix="uc1" TagName="AccountContactListMaintenanceControl" Src="Control/AccountContactListMaintenanceControl.ascx" %>
<html>
	<head>
		<title>Group Contacts Maintenance</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body id="BodyTag" runat="server" bottommargin="20" leftmargin="30" topmargin="20" rightmargin="30"
		marginwidth="0" marginheight="0">
		<form id="Form1" runat="server">
			<!--#include file="../CustomerService/fctjavascriptall.js"-->
			<cc2:enhancedsmartnavigationcontrol id="ctrlEnhancedSmartNavigationControl" runat="server"></cc2:enhancedsmartnavigationcontrol>
			<div>
				<br>
				<h3>Group Contacts Maintenance</h3>
				<br>
				<uc1:accountcontactlistmaintenancecontrol id="ctrlAccountContactListMaintenanceControl" runat="server"></uc1:accountcontactlistmaintenancecontrol>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

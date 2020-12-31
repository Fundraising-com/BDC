<%@ Page language="c#" Codebehind="ProductCreate.aspx.cs" AutoEventWireup="True" enableSessionState="true" Inherits="QSPFulfillment.MarketingMgt.ProductCreate" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<html>
	<head>
		<title>Create Product</title>
		<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	</head>
	<body id="BodyTag" runat="server" topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0"
		marginheight="0" marginwidth="0">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<cc1:enhancedsmartnavigationcontrol id="ctrlEnhancedSmartNavigationControl" runat="server"></cc1:enhancedsmartnavigationcontrol>
			<div style="MARGIN: 3%; WIDTH: 97%; HEIGHT: 97%">
				<h3>Create Product</h3>
				<br>
				<asp:placeholder id="plhProductMaintenanceControl" runat="server"></asp:placeholder>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

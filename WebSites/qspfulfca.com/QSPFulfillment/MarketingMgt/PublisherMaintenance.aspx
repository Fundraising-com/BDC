<%@ Register TagPrefix="uc1" TagName="PublisherSearchControl" Src="Control/PublisherSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PublisherMaintenanceControl" Src="Control/PublisherMaintenanceControl.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page language="c#" Codebehind="PublisherMaintenance.aspx.cs" AutoEventWireup="True" enableSessionState="true" Inherits="QSPFulfillment.MarketingMgt.PublisherMaintenance" %>
<html>
	<head>
		<title>Publisher Maintenance</title>
		<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	</head>
	<body id="BodyTag" runat="server" topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0"
		marginheight="0" marginwidth="0">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<cc2:enhancedsmartnavigationcontrol id="ctrlEnhancedSmartNavigationControl" runat="server"></cc2:enhancedsmartnavigationcontrol>
			<div style="MARGIN: 3%; WIDTH: 97%; HEIGHT: 97%">
				<h3>Publisher Maintenance</h3>
				<br>
				<br>
				<asp:label id="lblInstructions" runat="server" cssclass="csPlainText" font-bold="True">Please select a publisher from the list:</asp:label>
				<br>
				<br>
				<uc1:publishersearchcontrol id="ctrlPublisherSearchControl" runat="server"></uc1:publishersearchcontrol>
				<uc1:publishermaintenancecontrol id="ctrlPublisherMaintenanceControl" runat="server" visible="false"></uc1:publishermaintenancecontrol>
				<br>
				<div align="left">
					<asp:button id="btnCreateNew" runat="server" text="Create New Publisher" cssclass="boxlook" onclick="btnCreateNew_Click"></asp:button>
				</div>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

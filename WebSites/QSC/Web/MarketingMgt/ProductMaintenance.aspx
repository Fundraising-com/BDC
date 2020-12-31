<%@ Page language="c#" Codebehind="ProductMaintenance.aspx.cs" AutoEventWireup="True" enableSessionState="true" Inherits="QSPFulfillment.MarketingMgt.ProductMaintenance" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<html>
	<head>
		<title>Product Maintenance</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body id="BodyTag" runat="server" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0"
		marginwidth="0" marginheight="0">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<cc2:enhancedsmartnavigationcontrol id="ctrlEnhancedSmartNavigationControl" runat="server"></cc2:enhancedsmartnavigationcontrol>
			<div style="MARGIN: 3%; WIDTH: 97%; HEIGHT: 97%">
				<h3>Product Maintenance</h3>
				<asp:label id="lblInstructions" runat="server" cssclass="csPlainText" font-bold="True">Please select a product from the list:</asp:label>
				<br>
				<asp:placeholder id="plhProductControl" runat="server"></asp:placeholder>
				<br>
				<div align="left">
					<asp:button id="btnCreateNew" runat="server" text="Create New Product" cssclass="boxlook" onclick="btnCreateNew_Click"></asp:button>
				</div>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary></form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

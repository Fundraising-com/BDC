<%@ Page language="c#" Codebehind="ProductCategoryMaintenance.aspx.cs" AutoEventWireup="True" enableSessionState="true" Inherits="QSPFulfillment.MarketingMgt.ProductCategoryMaintenance" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="uc1" TagName="ProductCategorySearchControl" Src="Control/ProductCategorySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProductCategoryMaintenanceControl" Src="Control/ProductCategoryMaintenanceControl.ascx" %>
<html>
	<head>
		<title>Product Category Maintenance</title>
		<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	</head>
	<body topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginheight="0" marginwidth="0"
		onload="return window_onunload()">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<div style="MARGIN: 3%; WIDTH: 97%; HEIGHT: 97%">
				<h3>Product Category Maintenance</h3>
				<br>
				<br>
				<asp:label id="lblInstructions" runat="server" cssclass="csPlainText" font-bold="True">Please select a product category from the list:</asp:label>
				<br>
				<br>
				<uc1:productcategorysearchcontrol id="ctrlProductCategorySearchControl" runat="server"></uc1:productcategorysearchcontrol>
				<uc1:productcategorymaintenancecontrol id="ctrlProductCategoryMaintenanceControl" runat="server" visible="false"></uc1:productcategorymaintenancecontrol>
				<br>
				<div align="left">
					<asp:button id="btnCreateNew" runat="server" text="Create New Product Category" cssclass="boxlook" onclick="btnCreateNew_Click"></asp:button>
				</div>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

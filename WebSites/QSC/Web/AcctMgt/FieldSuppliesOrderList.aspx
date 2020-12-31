<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Page language="c#" Codebehind="FieldSuppliesOrderList.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.AcctMgt.FieldSuppliesOrderList" %>
<%@ Register TagPrefix="uc1" TagName="FieldSuppliesOrderListControl" Src="Control/FieldSuppliesOrderListControl.ascx" %>
<html>
	<head>
		<title>Group List</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" marginwidth="0" marginheight="0"
		onload="window_onunload();">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<div style="PADDING-LEFT: 5%; WIDTH: 95%"><br>
				<br>
				<h3>Field Supplies Order</h3>
				<uc1:fieldsuppliesorderlistcontrol id="ctrlFieldSuppliesOrderListControl" runat="server"></uc1:fieldsuppliesorderlistcontrol><br>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showsummary="False" showmessagebox="True"></asp:validationsummary></form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

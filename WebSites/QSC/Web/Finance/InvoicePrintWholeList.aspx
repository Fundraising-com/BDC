<%@ Page language="c#" Codebehind="InvoicePrintWholeList.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Finance.InvoicePrintWholeList" %>
<!doctype html public "-//w3c//dtd html 4.0 transitional//en" >
<html>
	<head>
		<title>Invoice List</title>
		<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	</head>
	<body onload="return window_onunload()" topmargin="0" bottommargin="0" leftmargin="0"
		rightmargin="0" marginheight="0" marginwidth="0">
		<form id="Form1" method="post" runat="server">
			<!--#include file="../CustomerService/fctjavascriptall.js"-->
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

<%@ Page language="c#" Codebehind="ProductSearch.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.MarketingMgt.ProductSearch" %>
<%@ Register TagPrefix="uc1" TagName="MagazineSearchControl" Src="Control/MagazineSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Product Search</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginheight="0" marginwidth="0"
		onload="return window_onunload()">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<div style="MARGIN: 3%; WIDTH: 97%; HEIGHT: 97%">
				<h3>Product Search</h3>
				<asp:label id="lblInstructions" runat="server" cssclass="csPlainText" font-bold="True">Please select a product from the list:</asp:label>
				<br>
				<uc1:MagazineSearchControl id="ctrlMagazineSearchControl" runat="server" showselect="true" showdelete="false" showcheckboxes="false"></uc1:MagazineSearchControl>
				<br>
				<br>
				<div align="right">
					<input type="button" id="btnCancel" name="btnCancel" onclick="self.close();" value="Cancel">
				</div>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary></form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

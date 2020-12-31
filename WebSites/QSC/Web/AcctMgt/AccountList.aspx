<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Page language="c#" Codebehind="AccountList.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.AcctMgt.AccountList" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="uc1" TagName="AccountListControl" Src="Control/AccountListControl.ascx" %>
<html>
	<head>
		<title>Group List</title>
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
				<h3>Group List</h3>
				<uc1:accountlistcontrol id="ctrlAccountListControl" runat="server"></uc1:accountlistcontrol><br>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showsummary="False" showmessagebox="True"></asp:validationsummary></form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

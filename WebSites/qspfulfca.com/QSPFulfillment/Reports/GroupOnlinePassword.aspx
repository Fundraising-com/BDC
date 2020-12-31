<%@ Register TagPrefix="uc1" TagName="ControlerGroupOnlinePassword" Src="ControlerGroupOnlinePassword.ascx" %>
<%@ Page language="c#" Codebehind="GroupOnlinePassword.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Reports.GroupOnlinePassword" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Group Online Password Report</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	</HEAD>
	<body onload="return window_onunload()" topmargin="0" bottommargin="0" leftmargin="0"
		rightmargin="0" marginheight="0" marginwidth="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<!--#include file="../CustomerService/fctjavascriptall.js"-->
			<br>
			<uc1:ControlerGroupOnlinePassword id="ctrlGroupOnlinePassword" runat="server"></uc1:ControlerGroupOnlinePassword>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<p>&nbsp;</p>
		<p>
		</p>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</HTML>

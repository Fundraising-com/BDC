<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Page language="c#" Codebehind="incidentmanagementreport.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.incidentmanagementreport" %>
<%@ Register TagPrefix="uc1" TagName="ControlerIncidentsManagementReport" Src="ControlerIncidentsManagementReport.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Incident Management Report</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
	</HEAD>
	<body onload="return window_onunload()" topmargin="0" bottommargin="0" rightmargin="0"
		leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<!--#include file="fctjavascriptall.js"-->
			<uc1:ControlerIncidentsManagementReport id="ctrlControlerIncidentsManagementReport" runat="server"></uc1:ControlerIncidentsManagementReport>
			<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
		</form>
		<!--#include file="errorwindow.js"-->
	</body>
</HTML>

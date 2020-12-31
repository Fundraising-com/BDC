<%@ Page language="c#" Codebehind="SummaryFormsReport.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Reports.SummaryFormsReport" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="UC" TagName="Date" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="UC" TagName="FMddl" Src="../Common/FieldManagerDDL.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryFormsReportControl" Src="SummaryFormsReportControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Summary Forms Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<!--#include file="../CustomerService/fctjavascriptall.js"--><br>
			<uc1:SummaryFormsReportControl id="ctrlSummaryFormsReportControl" runat="server"></uc1:SummaryFormsReportControl>
			<br>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>			
		</form>
	</body>
</HTML>

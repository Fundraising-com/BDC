<%@ Register TagPrefix="uc1" TagName="CampaignBatchReportsControl" Src="CampaignBatchReportsControl.ascx" %>
<%@ Page language="c#" Codebehind="CampaignBatchReports.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Reports.CampaignBatchReports" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<html>
	<head>
		<title>CA Batch Reports</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" onload="return window_onunload()"
		rightmargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<!--#include file="../CustomerService/fctjavascriptall.js"--><br>
			<uc1:campaignbatchreportscontrol id="ctrlCampaignBatchReportsControl" runat="server"></uc1:campaignbatchreportscontrol>
			<br>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary></form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

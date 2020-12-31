<%@ Page language="c#" Codebehind="CampaignContactListMaintenance.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.AcctMgt.CampaignContactListMaintenance" %>
<%@ Register TagPrefix="uc1" TagName="CampaignContactListMaintenanceControl" Src="Control/CampaignContactListMaintenanceControl.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<html>
	<head>
		<title>Campaign Contacts Maintenance</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body id="BodyTag" runat="server" bottommargin="20" leftmargin="10" topmargin="20" rightmargin="10"
		marginwidth="0" marginheight="0">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<cc2:enhancedsmartnavigationcontrol id="ctrlEnhancedSmartNavigationControl" runat="server"></cc2:enhancedsmartnavigationcontrol>
			<h3>Campaign Contacts Maintenance</h3>
			<br>
			<uc1:campaigncontactlistmaintenancecontrol id="ctrlCampaignContactListMaintenanceControl" runat="server"></uc1:campaigncontactlistmaintenancecontrol>
			<br>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

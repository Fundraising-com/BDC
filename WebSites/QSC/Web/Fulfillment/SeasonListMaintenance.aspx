<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SeasonListMaintenanceControl" Src="Control/SeasonListMaintenanceControl.ascx" %>
<%@ Page language="c#" Codebehind="SeasonListMaintenance.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Fulfillment.SeasonListMaintenance" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head>
		<title>Season Maintenance</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<BODY topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginheight="0" marginwidth="0"
		onload="return window_onunload()">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<div style="MARGIN: 3%; WIDTH: 97%; HEIGHT: 97%">
				<br>
				<h3>Season Maintenance</h3>
				<br>
				<uc1:SeasonListMaintenanceControl id="ctrlSeasonListMaintenanceControl" runat="server"></uc1:SeasonListMaintenanceControl>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</HTML>

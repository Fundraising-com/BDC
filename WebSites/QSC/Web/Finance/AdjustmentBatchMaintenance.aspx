<%@ Register TagPrefix="uc1" TagName="AdjustmentBatchMaintenanceControl" Src="Control/AdjustmentBatchMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AdjustmentBatchListControl" Src="Control/AdjustmentBatchListControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Page language="c#" Codebehind="AdjustmentBatchMaintenance.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Finance.AdjustmentBatchMaintenance" %>
<html>
	<head>
		<title>Adjustment Batch Maintenance</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet"></link>
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" marginwidth="0" marginheight="0"
		onload="window_onunload();">
		<script src="../Includes/fctjavascriptall.js" type="text/javascript" language="javascript"></script>
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<div style="PADDING-LEFT: 5%; WIDTH: 95%">
				<br>
				<br>
				<h3>Adjustment Batch Maintenance</h3>
				<br>
				<uc1:adjustmentbatchlistcontrol id="ctrlAdjustmentBatchListControl" runat="server"></uc1:adjustmentbatchlistcontrol>
				<uc1:adjustmentbatchmaintenancecontrol id="ctrlAdjustmentBatchMaintenanceControl" runat="server" visible="False"></uc1:adjustmentbatchmaintenancecontrol>
				<br>
				<div id="divTaskStatus" runat="server" visible="false" enableviewstate="False">
					<asp:label id="lblTaskStatus" runat="server" cssclass="CSPlainText" font-bold="true" enableviewstate="False"></asp:label>
					<br>
					<br>
				</div>
				<asp:button id="btnCreateNew" runat="server" text="Create New Adjustment Batch" cssclass="boxlook" onclick="btnCreateNew_Click"></asp:button>
				<br>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../Includes/ErrorWindow.inc"-->
	</body>
</html>

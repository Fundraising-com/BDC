<%@ Register TagPrefix="uc1" TagName="DefaultLetterBatchMaintenanceControl" Src="DefaultLetterBatchMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LetterBatchGenerationControl" Src="LetterBatchGenerationControl.ascx" %>
<%@ Page language="c#" Codebehind="LetterBatchMaintenance.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.CustomerService.LetterBatchMaintenance" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc3" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Letter Batch Maintenance</title>
		<LINK href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY bottomMargin="0" leftMargin="0" topMargin="0" onload="return window_onunload()"
		rightMargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<!--#include file="fctjavascriptall.js"-->
			<div style="PADDING-RIGHT: 15px; PADDING-LEFT: 15px; PADDING-BOTTOM: 15px; PADDING-TOP: 15px"><br>
				<div style="TEXT-ALIGN: center"><asp:label id="lblTitle" runat="server" cssclass="CSPageTitle">Letter Batch Maintenance</asp:label></div>
				<br>
				<br>
				<uc1:letterbatchgenerationcontrol id="ctrlLetterBatchGenerationControl" runat="server"></uc1:letterbatchgenerationcontrol><br>
				<asp:PlaceHolder ID="plhLetterBatchMaintenanceControl" Runat="server"></asp:PlaceHolder>
				<cc3:rsgeneration id="rsGenerationLetterBatch" runat="server" Mode="PopUp"></cc3:rsgeneration>
				<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></div>
		</form>
		<!--#include file="errorwindow.js"-->
	</BODY>
</HTML>

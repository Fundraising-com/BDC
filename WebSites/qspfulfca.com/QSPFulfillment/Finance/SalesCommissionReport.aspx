<%@ Register TagPrefix="UC" TagName="FMddl" Src="../Common/FieldManagerDDL.ascx" %>
<%@ Register TagPrefix="UC" TagName="DMddl" Src="../Common/DivisionManagerDDL.ascx" %>
<%@ Register TagPrefix="UC" TagName="Date" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Page debug="true" language="c#" Codebehind="SalesCommissionReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.Rpt.SalesCommissionReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Sales Commission Report</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
		<LINK href="../AcctMgt/AcctMgt.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="return window_onunload()" marginwidth="0" marginheight="0">
		<form id="SalesCommissionReport" method="post" runat="server">
			<!--#include file="../Includes/Menu.inc"-->
			<!--#include file="../CustomerService/fctjavascriptall.js"-->
			<div align="center">
				<table cellSpacing="0" cellPadding="0" width="90%" border="0">
					<tr>
						<td align="left" colSpan="4"><br>
							<div class="CSPageTitle">QSP Sales Commission Report
								<asp:validationsummary id="ValSummary_TOP" runat="server" showsummary="False" showmessagebox="True"></asp:validationsummary></div>
							<br>
						</td>
					</tr>
					<tr class="CSTableItems">
						<td vAlign="middle" align="center" colSpan="5"><asp:button id="btSubmit" runat="server" text="Request the report" cssclass="fields2"></asp:button></td>
					</tr>
				</table>
				<table class="boxlook" cellSpacing="0" cellPadding="2" width="654" border="0" style="WIDTH: 654px; HEIGHT: 112px">
					<tr>
						<td class="CSTitle" align="left" colSpan="4">Report Details</td>
					</tr>
					<tr class="CSTableItems">
						<td vAlign="top" noWrap align="left" width="25%"><b>FM</b></td>
						<td vAlign="top" noWrap align="left" width="25%"><uc:fmddl id="ucFMddl" runat="server"></uc:fmddl><asp:label id="lblFieldManager" cssclass="csPlainText" runat="server" visible="False"></asp:label></td>
						<td vAlign="top" noWrap align="left" width="25%"><b>DM</b></td>
						<td vAlign="top" noWrap align="left" width="25%"><uc:dmddl id="ucDMddl" runat="server"></uc:dmddl></td>
					</tr>
					<tr class="CSTableItems">
						<td vAlign="top" noWrap align="left"><b>Start Date:&nbsp;</b></td>
						<td><uc:date id="ucStartDate" runat="server" required="True"></uc:date></td>
						<td vAlign="top" noWrap align="left"><b>End Date:&nbsp;</b></td>
						<td><uc:date id="ucEndDate" runat="server" required="True"></uc:date></td>
					</tr>
					<tr class="CSTableItems">
						<td vAlign="top" noWrap align="left"><b>Section Type</b></td>
						<td vAlign="top" noWrap align="left"><asp:dropdownlist id="ddlSectionType" runat="server">
								<asp:listitem value="" selected="True" text="" />
								<asp:listitem value="1" text="1" />
								<asp:listitem value="2" text="2" />
								<asp:listitem value="5" text="5" />
								<asp:listitem value="6" text="6" />
							</asp:dropdownlist></td>
						<td colSpan="2">&nbsp;</td>
					</tr>
				</table>
			</div>
			<cc2:rsgeneration id="rsGenerationSalesCommissionReport" runat="server" reportname="SalesCommissionReport"></cc2:rsgeneration>
			<!--#include file="../CustomerService/errorwindow.js"--></form>
	</body>
</HTML>

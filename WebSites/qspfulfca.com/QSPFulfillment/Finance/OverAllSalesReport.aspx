<%@ Page debug="true" language="c#" Codebehind="OverAllSalesReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.Rpt.OverAllSalesReport" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="UC" TagName="Date" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="UC" TagName="DMddl" Src="../Common/DivisionManagerDDL.ascx" %>
<%@ Register TagPrefix="UC" TagName="FMddl" Src="../Common/FieldManagerDDL.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Over All Sales Report</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
		<link href="../AcctMgt/AcctMgt.css" type="text/css" rel="stylesheet">
	</head>
	<body leftmargin="0" topmargin="0" onload="return window_onunload()" marginheight="0"
		marginwidth="0">
		<form id="OverAllSalesReport" method="post" runat="server">
			<!--#include file="../Includes/Menu.inc"-->
			<!--#include file="../CustomerService/fctjavascriptall.js"-->
			<div align="center">
				<table cellspacing="0" cellpadding="0" width="90%" border="0">
					<tr>
						<td align="left" colspan="4"><br>
							<div class="CSPageTitle">QSP Over All Sales Report
								<asp:validationsummary id="ValSummary_TOP" showmessagebox="True" showsummary="False" runat="server"></asp:validationsummary></div>
							<br>
						</td>
					</tr>
					<tr class="CSTableItems">
						<td valign="middle" align="center" colspan="5"><asp:button id="btSubmit" runat="server" cssclass="fields2" text="Request the report"></asp:button></td>
					</tr>
				</table>
				<table class="boxlook" cellspacing="0" cellpadding="2" width="90%" border="0">
					<tr>
						<td class="CSTitle" align="left" colspan="4">Report Details</td>
					</tr>
					<tr class="CSTableItems">
						<td valign="top" nowrap align="left" width="25%"><b>FM</b></td>
						<td valign="top" nowrap align="left" width="25%">
							<uc:fmddl id="ucFMddl" runat="server" />
						</td>
						<td valign="top" nowrap align="left" width="25%"><b>DM</b></td>
						<td valign="top" nowrap align="left" width="25%">
							<uc:dmddl id="ucDMddl" runat="server" />
						</td>
					</tr>
					<tr class="CSTableItems">
						<td valign="top" nowrap align="left"><b>Start Date:&nbsp;</b></td>
						<td><uc:date id="ucStartDate" runat="server" required="True" /></td>
						<td valign="top" nowrap align="left"><b>End Date:&nbsp;</b></td>
						<td><uc:date id="ucEndDate" runat="server" required="True" /></td>
					</tr>
				</table>
			</div>
			<cc2:rsgeneration id="rsGenerationOverAllSalesReport" runat="server" reportname="OverAllSalesReport"></cc2:rsgeneration>
			<!--#include file="../CustomerService/errorwindow.js"-->
		</form>
	</body>
</html>

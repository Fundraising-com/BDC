<%@ Page language="c#" Codebehind="AgingReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Reports.AgingReport" %>
<%@ Register TagPrefix="UC" TagName="Date" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Aging Report</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">
			A { COLOR: blue; TEXT-DECORATION: none }
			UL { LIST-STYLE-POSITION: inside; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 0px; LIST-STYLE-TYPE: circle }
		</style>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
		<link href="../AcctMgt/AcctMgt.css" type="text/css" rel="stylesheet">
	</head>
	<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
		<form method="post" runat="server" id="AgingReport">
			<!--#include file="../Includes/Menu.inc"-->
			<!--#include file="../CustomerService/fctjavascriptall.js"-->
			<table cellspacing="2" cellpadding="2" class="boxlook" border="0">
				<tr>
					<td class="CSTitle" align="left" colspan="2">Aging Report</td>
				</tr>
				<tr class="CSTableItems">
					<td nowrap align="left" valign="middle">As of Date</td>
					<td nowrap align="left" valign="middle">
						<uc:date id="ucAsOfDate" runat="server" required="True" />
					</td>
				</tr>
				<tr class="CSTableItems">
					<td></td>
					<td nowrap align="left" valign="middle">
						<asp:button id="btViewRpt" runat="server" text="View Report" cssclass="fields2" />
					</td>
				</tr>
			</table>
			<cc2:rsgeneration id="rsGenerationAgingReport" runat="server" reportname="AgingReport"></cc2:rsgeneration>
		</form>
	</body>
</html>

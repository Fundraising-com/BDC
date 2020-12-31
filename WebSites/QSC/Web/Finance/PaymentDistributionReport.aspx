<%@ Register TagPrefix="UC" TagName="AccountLookUp" Src="../Common/AccountLookUp.ascx" %>
<%@ Register TagPrefix="UC" TagName="Date" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Page debug="true" language="c#" Codebehind="PaymentDistributionReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.Reports.PaymentDistributionReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Payment Distribution Report</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
		<link href="../AcctMgt/AcctMgt.css" type="text/css" rel="stylesheet">
	</head>
	<body leftmargin="0" topmargin="0" onload="return window_onunload()" marginheight="0"
		marginwidth="0">
		<form id="PaymentDistributionReport" method="post" runat="server">
			<!--#include file="../Includes/Menu.inc"-->
			<!--#include file="../CustomerService/fctjavascriptall.js"-->
			<div align="center">
				<table cellspacing="0" cellpadding="0" width="90%" border="0">
					<tr>
						<td align="left" colspan="4"><br>
							<div class="CSPageTitle">QSP Payment Distribution Report
								<asp:validationsummary id="ValSummary_TOP" showmessagebox="True" showsummary="False" runat="server"></asp:validationsummary></div>
							<br>
						</td>
					</tr>
					<tr class="CSTableItems">
						<td valign="middle" align="center" colspan="5"><asp:button id="btSubmit" runat="server" cssclass="fields2" text="View the report"></asp:button></td>
					</tr>
				</table>
				<table class="boxlook" cellspacing="0" cellpadding="2" width="90%" border="0">
					<tr>
						<td class="CSTitle" align="left" colspan="4">Report Details</td>
					</tr>
					<tr class="CSTableItems">
						<td valign="top" nowrap align="left" width="25%"><b>Account Type</b></td>
						<td valign="top" nowrap align="left" width="25%">
							<asp:dropdownlist id="ddlAccountType" runat="server">
								<asp:listitem value="" selected="True" text="" />
								<asp:listitem value="50601" text="Regular (Group)" />
								<asp:listitem value="50602" text="FM" />
								<asp:listitem value="50603" text="Account (Group)" />
								<asp:listitem value="50604" text="Employee" />
								<asp:listitem value="50605" text="Certificate" />
								<asp:listitem value="50606" text="Certificate Re-Issue" />
							</asp:dropdownlist>
						</td>
						<td valign="top" nowrap align="left" width="25%"><b>Group Number</b></td>
						<td valign="top" nowrap align="left" width="25%">
							<uc:accountlookup id="ucAccountNumber" runat="server" required="False" />
						</td>
					</tr>
					<tr class="CSTableItems">
						<td valign="top" nowrap align="left"><b>Start Date:&nbsp;</b></td>
						<td><uc:date id="ucPaymentStartDate" runat="server" required="True" /></td>
						<td valign="top" nowrap align="left"><b>End Date:&nbsp;</b></td>
						<td><uc:date id="ucPaymentEndDate" runat="server" required="True" /></td>
					</tr>
					<tr class="CSTableItems">
						<td valign="top" nowrap align="left"><b>Order ID</b></td>
						<td valign="top" nowrap align="left">
							<asp:textbox id="tbOrderID" runat="server" />
						</td>
						<td valign="top" nowrap align="left"><b>Payment Method</b></td>
						<td valign="top" nowrap align="left">
							<asp:dropdownlist id="ddlPaymentMethod" runat="server">
								<asp:listitem value="" selected="True" text="" />
								<asp:listitem value="50001" text="Other" />
								<asp:listitem value="50002" text="Cheque/Cash" />
								<asp:listitem value="50003" text="Visa" />
								<asp:listitem value="50004" text="Master Card" />
							</asp:dropdownlist>
						</td>
					</tr>
				</table>
			</div>
			<cc2:rsgeneration id="rsGenerationPaymentDistributionReport" runat="server" reportname="PaymentDistributionReport"></cc2:rsgeneration>
			<!--#include file="../CustomerService/errorwindow.js"--></form>
	</body>
</html>

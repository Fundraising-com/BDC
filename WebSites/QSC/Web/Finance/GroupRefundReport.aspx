<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Page language="c#" Codebehind="GroupRefundReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.GroupRefundReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>GroupRefundReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body ms_positioning="GridLayout" topmargin="0" leftmargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<asp:label id="lblDeadOrderReport" style="Z-INDEX: 100; LEFT: 48px; POSITION: absolute; TOP: 32px"
				runat="server" font-names="Verdana" font-size="Large" width="264px">Group Refund Report</asp:label>
			<table id="Table3" style="Z-INDEX: 104; LEFT: 48px; WIDTH: 816px; POSITION: absolute; TOP: 80px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="816" bgcolor="gainsboro" border="0">
				<tr>
					<td>
						<asp:label id="Label1" runat="server" width="232px" font-size="X-Small" font-names="Verdana"
							font-bold="True" height="8px">Report Detail</asp:label></td>
				</tr>
			</table>
			<table id="Table1" style="Z-INDEX: 101; LEFT: 48px; WIDTH: 816px; POSITION: absolute; TOP: 104px; HEIGHT: 136px"
				cellspacing="1" cellpadding="1" width="816" border="0">
				<tr>
					<td style="WIDTH: 142px; HEIGHT: 30px">
						<asp:label id="Label2" runat="server" width="184px" font-size="X-Small" font-names="Verdana"
							font-bold="True">Create Date (From)</asp:label></td>
					<td style="WIDTH: 193px; HEIGHT: 30px">
						<uc1:dateentry id="ucCreateDateFrom" runat="server"></uc1:dateentry></td>
					<td style="WIDTH: 204px; HEIGHT: 30px">
						<asp:label id="Label3" runat="server" width="184px" font-size="X-Small" font-names="Verdana"
							font-bold="True">Create Date (To)</asp:label></td>
					<td style="HEIGHT: 30px">
						<uc1:dateentry id="ucCreateDateTo" runat="server"></uc1:dateentry></td>
				</tr>
				<tr>
					<td style="WIDTH: 142px; HEIGHT: 24px">
						<asp:label id="lblAmountFrom" runat="server" font-names="Verdana" font-size="X-Small" width="120px"
							font-bold="True">Amount From</asp:label></td>
					<td style="WIDTH: 193px; HEIGHT: 24px">
						<asp:textbox id="tbAmountFrom" runat="server"></asp:textbox></td>
					<td style="WIDTH: 204px; HEIGHT: 24px">
						<asp:label id="lblAmountTo" runat="server" font-names="Verdana" font-size="X-Small" width="117px"
							font-bold="True">Amount To</asp:label></td>
					<td style="HEIGHT: 24px">
						<asp:textbox id="tbAmountTo" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 142px; HEIGHT: 18px">
						<asp:label id="lblAccountId" runat="server" font-names="Verdana" font-size="X-Small" width="120px"
							font-bold="True">Account ID</asp:label></td>
					<td style="WIDTH: 193px; HEIGHT: 18px">
						<asp:textbox id="tbAccountId" runat="server"></asp:textbox></td>
					<td style="WIDTH: 204px; HEIGHT: 18px">
						<asp:label id="lblCampaignId" runat="server" font-names="Verdana" font-size="X-Small" width="117px"
							font-bold="True">Campaign ID</asp:label></td>
					<td style="HEIGHT: 18px">
						<asp:textbox id="tbCampaignId" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 142px; HEIGHT: 23px">
						&nbsp;</td>
					<td style="WIDTH: 193px; HEIGHT: 23px">
						&nbsp;</td>
					<td style="WIDTH: 204px; HEIGHT: 23px">
						<asp:label id="lblSortBy" runat="server" font-names="Verdana" font-size="X-Small" width="117px"
							font-bold="True">Sort By</asp:label></td>
					<td style="HEIGHT: 23px">
						<asp:dropdownlist id="ddlSortBy" runat="server" width="120px" autopostback="True">
							<asp:listitem value="Name">Name</asp:listitem>
							<asp:listitem value="AMOUNT">Amount</asp:listitem>
							<asp:listitem value="DATE">Date</asp:listitem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="WIDTH: 142px"></td>
					<td style="WIDTH: 193px"></td>
					<td style="WIDTH: 204px">
						<asp:button id="PrintButton" runat="server" width="128px" text="Print / Preview"></asp:button></td>
					<td></td>
				</tr>
			</table>
			<table id="Table2" style="Z-INDEX: 103; LEFT: 48px; WIDTH: 816px; POSITION: absolute; TOP: 240px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="816" border="0">
				<tr>
					<td style="WIDTH: 313px"></td>
					<td>
						<asp:label id="lblErrorMessage" runat="server" font-names="Verdana" font-size="XX-Small" width="344px"
							font-bold="True" forecolor="Red"></asp:label></td>
				</tr>
			</table>
			<cc2:rsgeneration id="rsGenerationGroupRefundReport" runat="server" reportname="GroupRefundReport"></cc2:rsgeneration>
		</form>
	</body>
</html>

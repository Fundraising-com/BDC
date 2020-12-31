<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<%@ Page language="c#" Codebehind="CreditBalanceReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.CreditBalanceReport" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>CreditBalanceReport</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body ms_positioning="GridLayout" leftmargin="0" rightmargin="0" bottommargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<asp:label id="lblCreditBalanceReport" style="Z-INDEX: 101; LEFT: 48px; POSITION: absolute; TOP: 32px"
				runat="server" font-size="Large" font-names="Verdana" width="288px">Credit Balance Report</asp:label>
			<table id="Table4" style="Z-INDEX: 104; LEFT: 48px; WIDTH: 864px; POSITION: absolute; TOP: 192px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="864" border="0">
				<tr>
					<td style="WIDTH: 278px" valign="top" align="center"></td>
					<td valign="top" align="left">
						<asp:label id="lblErrorMessage" runat="server" font-size="XX-Small" font-names="Verdana" width="288px"
							font-bold="True" height="16px" forecolor="Red"></asp:label></td>
				</tr>
			</table>
			<table id="Table3" style="Z-INDEX: 102; LEFT: 48px; WIDTH: 864px; POSITION: absolute; TOP: 80px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="864" bgcolor="gainsboro" border="0">
				<tr>
					<td>
						<asp:label id="Label2" runat="server" font-size="X-Small" font-names="Verdana" width="232px"
							font-bold="True" height="8px">Report Detail</asp:label></td>
				</tr>
			</table>
			<table id="Table1" style="Z-INDEX: 103; LEFT: 48px; WIDTH: 624px; POSITION: absolute; TOP: 104px; HEIGHT: 88px"
				cellspacing="1" cellpadding="1" width="624" border="0">
				<tr>
					<td>
						<asp:label id="lblAsOfDate" runat="server" font-size="X-Small" font-names="Verdana" width="104px"
							font-bold="True">As Of  </asp:label></td>
					<td style="WIDTH: 155px">
						<uc1:dateentry id="AsOf" runat="server"></uc1:dateentry></td>
					<td>
						<asp:label id="lblAccount" runat="server" font-size="X-Small" font-names="Verdana" width="104px"
							font-bold="True" forecolor="#404040">Account ID</asp:label></td>
					<td>
						<asp:textbox id="tbAccountId" runat="server" width="135px"></asp:textbox></td>
				</tr>
				<tr>
					<td>
						<asp:label id="lblSortBy" runat="server" font-size="X-Small" font-names="Verdana" width="96px"
							font-bold="True" forecolor="#404040">Sort By</asp:label></td>
					<td style="WIDTH: 155px">
						<asp:dropdownlist id="ddlSortBy" runat="server" width="136px">
							<asp:listitem value="ACCOUNT">Account ID</asp:listitem>
							<asp:listitem value="NAME">Account Name</asp:listitem>
							<asp:listitem value="AMOUNT">Balance Amount</asp:listitem>
						</asp:dropdownlist></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td></td>
					<td style="WIDTH: 155px"></td>
					<td>
						<asp:button id="Button1" runat="server" width="102px" text="Print / Preview"></asp:button></td>
					<td></td>
				</tr>
			</table>
			<cc2:rsgeneration id="rsGenerationCreditBalanceReport" runat="server" reportname="CreditBalanceReport"></cc2:rsgeneration>
		</form>
	</body>
</html>

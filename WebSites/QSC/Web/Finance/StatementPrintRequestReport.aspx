<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="uc1" TagName="FiscalYearSelectControl" Src="../Common/FiscalYearSelectControl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="StatementRunSelectControl" Src="~/Finance/Control/StatementRunSelectControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Page language="c#" Codebehind="StatementPrintRequestReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.StatementPrintRequestReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Statement Print Request Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body ms_positioning="GridLayout" topmargin="0" leftmargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<asp:label id="lblStatementPrintRequestReport" style="Z-INDEX: 100; LEFT: 48px; POSITION: absolute; TOP: 32px; width: 511px;"
				runat="server" font-names="Verdana" font-size="Large">Statement Print Request Report</asp:label>
			<table id="Table3" style="Z-INDEX: 104; LEFT: 48px; WIDTH: 816px; POSITION: absolute; TOP: 80px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="816" bgcolor="gainsboro" border="0">
				<tr>
					<td>
						<asp:label id="Label1" runat="server" width="232px" font-size="X-Small" font-names="Verdana"
							font-bold="True" height="8px"></asp:label></td>
				</tr>
			</table>
			<table id="Table1" style="Z-INDEX: 101; LEFT: 48px; WIDTH: 816px; POSITION: absolute; TOP: 104px; HEIGHT: 136px"
				cellspacing="1" cellpadding="1" width="816" border="0">
				<tr>
					<td style="WIDTH: 142px; HEIGHT: 30px">
						<asp:label id="Label2" runat="server" width="184px" font-size="X-Small" font-names="Verdana"
							font-bold="True">Campaign ID</asp:label></td>
					<td style="WIDTH: 193px; HEIGHT: 30px">
						<asp:textbox id="tbCampaignId" runat="server"></asp:textbox></td>
					<td style="WIDTH: 204px; HEIGHT: 30px">
						<asp:label id="lblAccountId" runat="server" font-names="Verdana" 
                            font-size="X-Small" width="120px"
							font-bold="True">Account ID</asp:label></td>
                    <td class="style2">
						<asp:textbox id="tbAccountId" runat="server"></asp:textbox>
                    </td>

                    </td>
				</tr>
				<tr>
					<td style="WIDTH: 142px; HEIGHT: 24px">
						<asp:label id="lblFieldManager" runat="server" font-names="Verdana" 
                            font-size="X-Small" width="120px"
							font-bold="True" Height="16px">Field Manager</asp:label></td>
					<td style="WIDTH: 193px; HEIGHT: 24px">
					    <cc1:dropdownlistsearch id="ddlFieldManager" runat="server" parametername="sFMID" width="229px" contenttype="string"></cc1:dropdownlistsearch>
					</td>
					<td style="WIDTH: 204px; HEIGHT: 24px">
						<asp:label id="Label3" runat="server" width="184px" font-size="X-Small" font-names="Verdana"
							font-bold="True">Fiscal Year</asp:label></td>
					<td style="HEIGHT: 24px">
	                    <uc1:FiscalYearSelectControl id="ctrlFiscalYearSelect" runat="server" ParameterName="FiscalYear"></uc1:FiscalYearSelectControl>
                    </td>
				</tr>
				<tr>
				    <td></td>
				    <td></td>
					<td style="WIDTH: 204px; HEIGHT: 24px">
						<asp:label id="Label4" runat="server" width="184px" font-size="X-Small" font-names="Verdana"
							font-bold="True">Statement Run</asp:label></td>
					<td style="HEIGHT: 24px">
	                    <uc2:StatementRunSelectControl id="ctrlStatementRunSelect" runat="server" ParameterName="StatementRun"></uc2:StatementRunSelectControl>
                    </td>

				</tr>
				<tr>
					<td style="WIDTH: 142px"></td>
					<td style="WIDTH: 193px"></td>
					<td style="WIDTH: 204px">
						<asp:button id="PrintButton" runat="server" width="128px" text="Print"></asp:button></td>
					<td></td>
				</tr>
			</table>
			<table id="Table2" style="Z-INDEX: 103; LEFT: 49px; WIDTH: 816px; POSITION: absolute; TOP: 216px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" border="0">
				<tr>
					<td style="WIDTH: 313px"></td>
					<td>
						<asp:label id="lblErrorMessage" runat="server" font-names="Verdana" font-size="XX-Small" width="344px"
							font-bold="True" forecolor="Red"></asp:label></td>
				</tr>
			</table>
			<cc2:rsgeneration id="rsGenerationStatementPrintRequestReport" runat="server" reportname="StatementPrintRequest" mode="PopUp"></cc2:rsgeneration>
		</form>
	</body>
</html>

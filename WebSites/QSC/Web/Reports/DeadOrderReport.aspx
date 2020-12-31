<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Page language="c#" Codebehind="DeadOrderReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Reports.DeadOrderReport" %>
<%@ Register  TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>DeadOrderReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body ms_positioning="GridLayout" topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<table id="Table1" style="Z-INDEX: 100; LEFT: 48px; WIDTH: 866px; POSITION: absolute; TOP: 104px; HEIGHT: 144px"
				cellspacing="1" cellpadding="1" width="866" align="center" border="0">
				<tr>
					<td style="WIDTH: 152px; HEIGHT: 34px"><asp:label id="lblStartDate" runat="server" width="136px" font-size="X-Small" font-names="Verdana"
							font-bold="True">Order Date From</asp:label></td>
					<td style="WIDTH: 180px; HEIGHT: 34px"><uc1:dateentry id="ucOrderDateFrom" runat="server"></uc1:dateentry></td>
					<td style="WIDTH: 129px; HEIGHT: 34px"><asp:label id="lblOrderDateTo" runat="server" font-size="X-Small" font-names="Verdana" font-bold="True">Order Date To</asp:label></td>
					<td style="HEIGHT: 34px"><uc1:dateentry id="ucOrderDateTo" runat="server"></uc1:dateentry></td>
				</tr>
				<tr>
					<td style="WIDTH: 152px; HEIGHT: 25px"><asp:label id="lblOrderId" runat="server" width="128px" font-size="X-Small" font-names="Verdana"
							font-bold="True">Order Id</asp:label></td>
					<td style="WIDTH: 181px; HEIGHT: 25px"><asp:textbox id="tbOrderId" runat="server"></asp:textbox></td>
					<td style="WIDTH: 129px; HEIGHT: 25px"><asp:label id="lblAccountId" runat="server" font-size="X-Small" font-names="Verdana" font-bold="True">Account Id</asp:label></td>
					<td style="HEIGHT: 25px"><asp:textbox id="tbAccountId" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 152px; HEIGHT: 24px"><asp:label id="lblCampaignId" runat="server" width="128px" font-size="X-Small" font-names="Verdana"
							font-bold="True">Campaign Id</asp:label></td>
					<td style="WIDTH: 181px; HEIGHT: 24px"><asp:textbox id="tbCampaignId" runat="server"></asp:textbox></td>
					<td style="WIDTH: 129px; HEIGHT: 24px"><asp:label id="lblErrorType" runat="server" font-size="X-Small" font-names="Verdana" font-bold="True">Error Type</asp:label></td>
					<td style="HEIGHT: 24px"><asp:dropdownlist id="ddlErrorType" runat="server" width="152px">
							<asp:listitem value="ALL">ALL</asp:listitem>
							<asp:listitem value="ADDRESS">ADDRESS</asp:listitem>
							<asp:listitem value="CREDIT CARD">CREDIT CARD</asp:listitem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="Campaign Id" style="WIDTH: 152px"></td>
					<td style="WIDTH: 181px"></td>
					<td style="WIDTH: 129px"><asp:button id="PrintButton" runat="server" width="136px" text="Print / Preview"></asp:button></td>
					<td></td>
				</tr>
			</table>
			<table id="Table3" style="Z-INDEX: 105; LEFT: 48px; WIDTH: 864px; POSITION: absolute; TOP: 80px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="864" bgcolor="gainsboro" border="0">
				<tr>
					<td>
						<asp:label id="Label2" runat="server" font-bold="True" font-names="Verdana" font-size="X-Small"
							width="232px" height="8px">Report Detail</asp:label></td>
				</tr>
			</table>
			<asp:label id="lblDeadOrderReport" style="Z-INDEX: 101; LEFT: 48px; POSITION: absolute; TOP: 32px"
				runat="server" width="235px" font-size="Large" font-names="Verdana">Dead Order Report</asp:label>
			<table id="Table2" style="Z-INDEX: 103; LEFT: 48px; WIDTH: 864px; POSITION: absolute; TOP: 248px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="864" border="0">
				<tr>
					<td style="WIDTH: 324px" valign="top" align="center"></td>
					<td valign="top" align="left">
						<asp:label id="lblErrorMessage" runat="server" width="287px" font-size="XX-Small" font-names="Verdana"
							font-bold="True" forecolor="Red"></asp:label></td>
				</tr>
			</table>
			<cc2:rsgeneration id="rsGenerationDeadOrderReport" runat="server" reportname="DeadOrderReport"></cc2:rsgeneration>
		</form>
	</body>
</html>

<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CodeDetailDropDown" Src="../Common/CodeDetailDropDown.ascx" %>
<%@ Page language="c#" Codebehind="AdjustmentDetailReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.AdjustmentDetailReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>AdjustmentDetailReport</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body ms_positioning="GridLayout" topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<asp:label id="lblAdjustmentDetailReport" style="Z-INDEX: 101; LEFT: 48px; POSITION: absolute; TOP: 32px"
				runat="server" font-names="Verdana" font-size="Large" width="304px">Adjustment Detail Report</asp:label>
			<table id="Table3" style="Z-INDEX: 104; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 232px; HEIGHT: 27px"
				cellspacing="1" cellpadding="1" width="784" border="0">
				<tr>
					<td style="WIDTH: 361px"></td>
					<td>
						<asp:label id="lblErrorMessage" runat="server" font-names="Verdana" font-size="XX-Small" width="296px"
							font-bold="True" forecolor="Red"></asp:label></td>
				</tr>
			</table>
			<table id="Table1" style="Z-INDEX: 102; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 104px; HEIGHT: 128px"
				cellspacing="1" cellpadding="1" width="784" border="0">
				<tr>
					<td style="WIDTH: 180px; HEIGHT: 28px">
						<asp:label id="lblEffectiveDateFrom" runat="server" font-names="Verdana" font-size="X-Small"
							width="152px" font-bold="True">Effective Date From</asp:label></td>
					<td style="WIDTH: 195px; HEIGHT: 28px">
						<uc1:dateentry id="ucAdjustDateFrom" runat="server"></uc1:dateentry></td>
					<td style="WIDTH: 137px; HEIGHT: 28px">
						<asp:label id="lblEffectiveDateTo" runat="server" font-names="Verdana" font-size="X-Small"
							width="128px" font-bold="True">Effective Date To</asp:label></td>
					<td style="HEIGHT: 28px">
						<uc1:dateentry id="ucAdjustDateTo" runat="server"></uc1:dateentry></td>
				</tr>
				<tr>
					<td style="WIDTH: 180px; HEIGHT: 25px">
						<asp:label id="lblAccountId" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Account Id</asp:label></td>
					<td style="WIDTH: 195px; HEIGHT: 25px">
						<asp:textbox id="tbAccountId" runat="server"></asp:textbox></td>
					<td style="WIDTH: 137px; HEIGHT: 25px">
						<asp:label id="lblAccountType" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Account Type</asp:label></td>
					<td style="HEIGHT: 25px">
						<asp:dropdownlist id="ddlAccountType" runat="server" width="168px" datatextfield="description" datavaluefield="instance"
							autopostback="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="WIDTH: 180px; HEIGHT: 17px">
						<asp:label id="lblOrderId" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Order Id</asp:label></td>
					<td style="WIDTH: 195px; HEIGHT: 17px">
						<asp:textbox id="tbOrderId" runat="server"></asp:textbox></td>
					<td style="WIDTH: 137px; HEIGHT: 17px">
						<asp:label id="lblAdjustmentType" runat="server" font-names="Verdana" font-size="X-Small" font-bold="True">Adjustment Type</asp:label></td>
					<td style="HEIGHT: 17px">
						<asp:dropdownlist id="ddlAdjustmentType" runat="server" width="168px" datatextfield="description"
							datavaluefield="instance" autopostback="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="WIDTH: 180px"></td>
					<td style="WIDTH: 195px" align="right"></td>
					<td style="WIDTH: 137px">
						<asp:button id="PrintButton" runat="server" width="118px" text="Print / Preview"></asp:button></td>
					<td></td>
				</tr>
			</table>
			<table id="Table2" style="Z-INDEX: 103; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 80px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="784" bgcolor="gainsboro" border="0">
				<tr>
					<td>
						<asp:label id="Label1" runat="server" font-names="Verdana" font-size="X-Small" width="232px"
							font-bold="True" height="8px">Report Detail</asp:label></td>
				</tr>
			</table>
			<cc2:rsgeneration id="rsGenerationAdjustmentDetailReport" runat="server" reportname="AdjustmentDetailReport"></cc2:rsgeneration>
		</form>
	</body>
</html>

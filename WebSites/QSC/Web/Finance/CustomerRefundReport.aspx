<%@ Page language="c#" Codebehind="CustomerRefundReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.CustomerRefundReport" %>
<%@ Register TagPrefix="uc1" TagName="StateProvince" Src="../Common/StateProvince.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>CustomerRefundReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body leftmargin="0" topmargin="0" rightmargin="0" ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" --><asp:label id="lblCustomerRefundReport" style="Z-INDEX: 101; LEFT: 48px; POSITION: absolute; TOP: 32px"
				runat="server" font-names="Verdana" font-size="Large" width="304px">Customer Refund  Report</asp:label>
			<table id="Table3" style="Z-INDEX: 103; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 240px; HEIGHT: 27px"
				cellspacing="1" cellpadding="1" width="784" border="0">
				<tr>
					<td style="WIDTH: 313px"></td>
					<td><asp:label id="lblErrorMessage" runat="server" font-names="Verdana" font-size="XX-Small" width="264px"
							forecolor="Red" font-bold="True"></asp:label></td>
				</tr>
			</table>
			<table id="Table2" style="Z-INDEX: 102; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 80px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="784" bgcolor="gainsboro" border="0">
				<tr>
					<td><asp:label id="lblReportDetail" runat="server" font-names="Verdana" font-size="X-Small" width="232px"
							font-bold="True" height="8px">Report Detail</asp:label></td>
				</tr>
			</table>
			<table id="Table1" style="Z-INDEX: 104; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 104px; HEIGHT: 136px"
				cellspacing="1" cellpadding="1" width="784" border="0">
				<tr>
					<td style="WIDTH: 159px; HEIGHT: 25px">
						<asp:label id="Label1" runat="server" width="184px" font-size="X-Small" font-names="Verdana"
							font-bold="True" style="margin-bottom: 0px">Create Date (From)</asp:label></td>
					<td style="WIDTH: 190px; HEIGHT: 25px">
						<uc1:dateentry id="ucCreateDateFrom" runat="server"></uc1:dateentry></td>
					<td style="WIDTH: 169px; HEIGHT: 25px">
						<asp:label id="Label2" runat="server" width="184px" font-size="X-Small" font-names="Verdana"
							font-bold="True">Create Date (To)</asp:label></td>
					<td style="HEIGHT: 25px">
						<uc1:dateentry id="ucCreateDateTo" runat="server"></uc1:dateentry></td>
				</tr>
				<tr>
					<td style="WIDTH: 159px; HEIGHT: 20px"><asp:label id="lblAmountFrom" runat="server" font-names="Verdana" font-size="X-Small" width="112px"
							font-bold="True">Amount From</asp:label></td>
					<td style="WIDTH: 190px; HEIGHT: 20px"><asp:textbox id="tbAmountFrom" runat="server" width="152px"></asp:textbox></td>
					<td style="WIDTH: 169px; HEIGHT: 20px"><asp:label id="lblAmountTo" runat="server" font-names="Verdana" font-size="X-Small" width="112px"
							font-bold="True">Amount To</asp:label></td>
					<td style="HEIGHT: 20px"><asp:textbox id="tbAmountTo" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 159px; HEIGHT: 15px"><asp:label id="lblProvince" runat="server" font-names="Verdana" font-size="X-Small" width="144px"
							font-bold="True">Refund To Province</asp:label></td>
					<td style="WIDTH: 190px; HEIGHT: 15px"><uc1:stateprovince id="ucProvinceddl" runat="server"></uc1:stateprovince></td>
					<td style="WIDTH: 169px; HEIGHT: 15px">&nbsp;</td>
					<td style="HEIGHT: 15px">&nbsp;</td>
				</tr>
				<tr>
					<td style="WIDTH: 159px; HEIGHT: 22px"><asp:label id="lblSortBy" runat="server" font-names="Verdana" font-size="X-Small" width="112px"
							font-bold="True">Sort By</asp:label></td>
					<td style="WIDTH: 190px; HEIGHT: 22px"><asp:dropdownlist id="ddlSortBy" runat="server" width="112px" autopostback="True">
							<asp:listitem value="NAME">Name</asp:listitem>
							<asp:listitem value="AMOUNT">Amount</asp:listitem>
							<asp:listitem value="Date">Date</asp:listitem>
						</asp:dropdownlist></td>
					<td style="WIDTH: 169px; HEIGHT: 22px"></td>
					<td style="HEIGHT: 22px"></td>
				</tr>
				<tr>
					<td style="WIDTH: 159px"></td>
					<td style="WIDTH: 190px" align="right"></td>
					<td style="WIDTH: 169px"><asp:button id="PrintButton" runat="server" text="Print / Preview"></asp:button></td>
					<td></td>
				</tr>
			</table>
			<cc2:rsgeneration id="rsGenerationCustomerRefundReport" runat="server" reportname="CustomerRefundReport"></cc2:rsgeneration>
		</form>
	</body>
</html>

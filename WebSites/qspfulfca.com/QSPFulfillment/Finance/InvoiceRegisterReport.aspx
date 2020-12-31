<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Page language="c#" Codebehind="InvoiceRegisterReport.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.InvoiceRegisterReport" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FieldManagerDDL" Src="../Common/FieldManagerDDL.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>InvoiceRegisterReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body ms_positioning="GridLayout" topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<asp:label id="lblInvoiceRegisterReport" style="Z-INDEX: 101; LEFT: 48px; POSITION: absolute; TOP: 32px"
				runat="server" font-names="Verdana" font-size="Large" width="304px">Invoice Register Report</asp:label>
			<table id="Table3" style="Z-INDEX: 103; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 208px; HEIGHT: 27px"
				cellspacing="1" cellpadding="1" width="784" border="0">
				<tr>
					<td style="WIDTH: 341px"></td>
					<td><asp:label id="lblErrorMessage" runat="server" font-names="Verdana" font-size="XX-Small" width="264px"
							font-bold="True" forecolor="Red"></asp:label></td>
				</tr>
			</table>
			<table id="Table2" style="Z-INDEX: 102; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 80px; HEIGHT: 24px"
				cellspacing="1" cellpadding="1" width="784" bgcolor="gainsboro" border="0">
				<tr>
					<td><asp:label id="lblReportDetail" runat="server" font-names="Verdana" font-size="X-Small" width="232px"
							height="8px" font-bold="True">Report Detail</asp:label></td>
				</tr>
			</table>
			<table id="Table1" style="Z-INDEX: 104; LEFT: 48px; WIDTH: 784px; POSITION: absolute; TOP: 104px; HEIGHT: 104px"
				cellspacing="1" cellpadding="1" width="784" border="0">
				<tr>
					<td style="WIDTH: 144px; HEIGHT: 24px"><asp:label id="lblInvDateFrom" runat="server" font-names="Verdana" font-size="X-Small" width="144px"
							font-bold="True">Invoice Date From</asp:label></td>
					<td style="WIDTH: 206px; HEIGHT: 24px"><uc1:dateentry id="ucInvDateFrom" runat="server"></uc1:dateentry></td>
					<td style="WIDTH: 174px; HEIGHT: 24px"><asp:label id="lblInvDateTo" runat="server" font-names="Verdana" font-size="X-Small" width="146px"
							font-bold="True">Invoice Date To</asp:label></td>
					<td style="HEIGHT: 24px"><uc1:dateentry id="ucInvDateTo" runat="server"></uc1:dateentry></td>
				</tr>
				<tr>
					<td style="WIDTH: 144px"><asp:label id="lblFM" runat="server" font-names="Verdana" font-size="X-Small" width="144px"
							font-bold="True">Field Manager</asp:label></td>
					<td style="WIDTH: 206px"><uc1:fieldmanagerddl id="ucFMddl2" runat="server"></uc1:fieldmanagerddl></td>
					<td style="WIDTH: 174px"><asp:label id="lblInvType" runat="server" font-names="Verdana" font-size="X-Small" width="146px"
							font-bold="True">Invoice Type</asp:label></td>
					<td><asp:dropdownlist id="ddlInvoiceType" runat="server" width="144px" datatextfield="description" datavaluefield="instance"
							autopostback="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="WIDTH: 144px"><asp:label id="lblSortBy" runat="server" font-names="Verdana" font-size="X-Small" width="144px"
							font-bold="True">Sort By</asp:label></td>
					<td style="WIDTH: 206px"><asp:dropdownlist id="ddlSortBy" runat="server" width="136px" autopostback="True">
							<asp:listitem value="INVOICE">Invoice Number</asp:listitem>
							<asp:listitem value="ACCOUNT">Account Id</asp:listitem>
						</asp:dropdownlist></td>
					<td style="WIDTH: 174px"></td>
					<td></td>
				</tr>
				<tr>
					<td style="WIDTH: 144px"></td>
					<td style="WIDTH: 206px"></td>
					<td style="WIDTH: 174px"><asp:button id="PrintButton" runat="server" text="Print / Preview"></asp:button></td>
					<td></td>
				</tr>
			</table>
			<cc2:rsgeneration id="rsGenerationInvoiceRegisterReport" runat="server" reportname="InvoiceRegisterReport"></cc2:rsgeneration>
		</form>
	</body>
</html>

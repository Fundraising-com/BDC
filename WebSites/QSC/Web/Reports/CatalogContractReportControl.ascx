<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CatalogContractReportControl.ascx.cs" Inherits="QSPFulfillment.Reports.CatalogContractReportControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc3" TagName="FiscalYearSelectControl" Src="../Common/FiscalYearSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<div style="TEXT-ALIGN: center">
	<table id="Table1" cellspacing="0" cellpadding="0" width="283" border="0" style="WIDTH: 283px; HEIGHT: 19px">
		<tr>
			<td><asp:label id="lblTitle" runat="server" cssclass="CSPageTitle" Font-Bold="True">Catalog Contract Report</asp:label></td>
		</tr>
	</table>
	<br>
	<table cellspacing="0" cellpadding="1" width="600" bgcolor="#000000" border="0" style="WIDTH: 600px; HEIGHT: 40px">
		<tr>
			<td style="WIDTH: 638px">
				<table id="Table1ss" cellspacing="0" cellpadding="2" width="100%" bgcolor="#ffffff" border="0">
					<tr>
						<td style="WIDTH: 201px; HEIGHT: 13px">
							<asp:label id="Label2" cssclass="csPlainText" runat="server" Font-Bold="True">Catalog - Current Season</asp:label></td>
						<td style="WIDTH: 395px; HEIGHT: 13px">
							<asp:dropdownlist id="ddlCatalogID" runat="server" Width="400px" onload="ddlCatalogID_Load"></asp:dropdownlist></td>
					</tr>
					<TR>
						<TD width="201" style="WIDTH: 201px; HEIGHT: 20px">
							<asp:label id="Label1" Font-Bold="True" cssclass="csPlainText" runat="server"> Catalog - Last Season</asp:label></TD>
						<TD style="WIDTH: 395px; HEIGHT: 20px">
							<asp:dropdownlist id="ddlCatalogIDLastSeason" runat="server" width="400px" onload="ddlCatalogIDLastSeason_Load"></asp:dropdownlist></TD>
					</TR>
					<tr>
						<td width="201" style="WIDTH: 201px">
							<asp:label id="Label3" cssclass="csPlainText" runat="server" Font-Bold="True">Report Type</asp:label></td>
						<td style="WIDTH: 395px"><asp:dropdownlist id="ddlReportType" runat="server" width="229px" contenttype="string">
								<asp:ListItem Value="Marketing" Selected="True">Marketing</asp:ListItem>
								<asp:ListItem Value="Printer">Printer</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br>
	<table cellspacing="0" cellpadding="2" width="596" bgcolor="#ffffff" border="0" style="WIDTH: 596px; HEIGHT: 28px">
		<tr>
			<td align="center" style="WIDTH: 672px"><asp:button id="btnPreview" runat="server" text="Preview" onclick="btnPreview_Click"></asp:button></td>
		</tr>
	</table>
</div>

<%@ Register TagPrefix="uc3" TagName="FiscalYearSelectControl" Src="../Common/FiscalYearSelectControl.ascx" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="ShipToBillToSearch" Src="ShipToBillToSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="InfoSearchGroup.ascx.cs" Inherits="QSPFulfillment.CustomerService.InfoSearchGroup" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td style="HEIGHT: 19px">
			<asp:label id="Label3" runat="server" cssclass="csPlainText" Font-Bold="True">Fiscal&nbsp;Year</asp:label>			
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 19px">
			<uc3:FiscalYearSelectControl id="ctrlFiscalYearSelect" runat="server" ParameterName="FiscalYear"></uc3:FiscalYearSelectControl>
		</td>
	</tr>
	<TR>
		<TD>
			<asp:Label id="Label1" runat="server" Font-Bold="True" CssClass="csPlainText">Shipping Address</asp:Label></TD>
	</TR>
	<TR>
		<TD><uc1:shiptobilltosearch id="ctrlShipToSearch" runat="server"></uc1:shiptobilltosearch></TD>
	</TR>
	<TR>
		<TD><BR>
			<asp:Label id="Label2" runat="server" Font-Bold="True" CssClass="csPlainText">Billing Address</asp:Label></TD>
	</TR>
	<TR>
		<TD><uc1:shiptobilltosearch id="ctrlBillToSearch" runat="server"></uc1:shiptobilltosearch></TD>
	</TR>
</TABLE>

<%@ Register TagPrefix="uc3" TagName="FiscalYearSelectControl" Src="../Common/FiscalYearSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="InfoSearchShippement.ascx.cs" Inherits="QSPFulfillment.CustomerService.InfoSearchShippement" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td style="HEIGHT: 19px">
			<asp:label id="Label12" runat="server" cssclass="csPlainText" Font-Bold="True">Fiscal&nbsp;Year</asp:label>			
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 19px">
			<uc3:FiscalYearSelectControl id="ctrlFiscalYearSelect" runat="server" ParameterName="FiscalYear"></uc3:FiscalYearSelectControl>
		</td>
	</tr>
	<TR>
		<TD><asp:label id="Label1" runat="server" CssClass="csPlainText">Shipment Date From</asp:label></TD>
	</TR>
	<TR>
		<TD><uc1:dateentry id="ctrlDateEntryShipFrom" runat="server" ParameterName="ShipmentDateFrom"></uc1:dateentry></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label2" runat="server" CssClass="csPlainText">Shipment Date To</asp:label></TD>
	</TR>
	<TR>
		<TD><uc1:dateentry id="ctrlDateEntryShipTo" runat="server" ParameterName="ShipmentDateTo"></uc1:dateentry></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label3" runat="server" CssClass="csPlainText">Order Date From</asp:label></TD>
	</TR>
	<TR>
		<TD><uc1:dateentry id="ctrlDateEntryCreatedFrom" runat="server" ParameterName="OrderDateFrom"></uc1:dateentry></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label4" runat="server" CssClass="csPlainText">Order Date To</asp:label></TD>
	</TR>
	<TR>
		<TD><uc1:dateentry id="ctrlDateEntryCreatedTo" runat="server" ParameterName="OrderDateTo"></uc1:dateentry></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label5" runat="server" CssClass="csPlainText">Ship To Account ID</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxShipToGroupID" runat="server" ParameterName="AccountID"></cc1:textboxsearch>
			<asp:RangeValidator id="RangeValidator3" runat="server" ErrorMessage="Ship To Account ID must be between 1 and 2147483647."
				Type="Integer" ControlToValidate="tbxShipToGroupID" MinimumValue="1" MaximumValue="2147483647">*</asp:RangeValidator></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label6" runat="server" CssClass="csPlainText">Order ID</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxOrderID" runat="server" ParameterName="OrderID"></cc1:textboxsearch>
			<asp:RangeValidator id="RangeValidator1" runat="server" ErrorMessage="Order ID must be between 1 and 2147483647."
				Type="Integer" ControlToValidate="tbxOrderID" MinimumValue="1" MaximumValue="2147483647">*</asp:RangeValidator></TD>
	</TR>
	<TR>
		<TD>
			<asp:label id="Label8" CssClass="csPlainText" runat="server">Shipment ID</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<cc1:textboxsearch id="tbxShippementID" runat="server" ParameterName="ShipmentID"></cc1:textboxsearch>
			<asp:RangeValidator id="RangeValidator2" runat="server" ErrorMessage="Shipment ID must be between 1 and 2147483647."
				Type="Integer" ControlToValidate="tbxShippementID" MinimumValue="1" MaximumValue="2147483647">*</asp:RangeValidator></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 21px">
			<asp:label id="Label7" CssClass="csPlainText" runat="server">FMID</asp:label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 3px">
			<cc1:textboxsearch id="tbxFMID" runat="server" ParameterName="FMID" Width="64px" MaxLength="4"></cc1:textboxsearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label9" runat="server" CssClass="csPlainText">Last Name</asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch id="TextBoxSearch1" runat="server" ParameterName="FMLastName"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label10" runat="server" CssClass="csPlainText">First Name</asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch id="TextBoxSearch2" runat="server" ParameterName="FMFirstName"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label11" runat="server" CssClass="csPlainText">Campaign ID</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxCampaignID" runat="server" ParameterName="CampaignID"></cc1:textboxsearch>&nbsp;
			<asp:RangeValidator id="RangeValidator5" runat="server" ErrorMessage="Campaign ID must be between 1 and 2147483647."
				Type="Integer" ControlToValidate="tbxCampaignID" MinimumValue="1" MaximumValue="2147483647">*</asp:RangeValidator></TD>
	</TR>
</TABLE>

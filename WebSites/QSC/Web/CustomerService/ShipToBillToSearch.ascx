<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="StateProvince" Src="../Common/StateProvince.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ShipToBillToSearch.ascx.cs" Inherits="QSPFulfillment.CustomerService.ShipToBillToSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<asp:Label id="lblGrouID" runat="server" CssClass="csPlainText">Account ID</asp:Label>:</TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch id="tbxGroupID" runat="server" ParameterName="GroupID"></cc1:TextBoxSearch>
			<asp:RangeValidator id="RangeValidator5" runat="server" ErrorMessage="Account ID must be between 1 and 2147483647."
				Type="Integer" ControlToValidate="tbxGroupID" MinimumValue="1" MaximumValue="2147483647">*</asp:RangeValidator>
		</TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblName" runat="server" CssClass="csPlainText">Name</asp:Label>:</TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch ParameterName="GroupName" id="tbxName" runat="server"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lbl" runat="server" CssClass="csPlainText">Address</asp:Label>:</TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch ParameterName="Address" id="tbxAddress" runat="server"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblCity" runat="server" CssClass="csPlainText">City</asp:Label>:</TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch ParameterName="City" id="tbxCity" runat="server"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblProvince" runat="server" CssClass="csPlainText">Province</asp:Label>:</TD>
	</TR>
	<TR>
		<TD>
			<cc1:DropDownListProvince id="ddlProvince" runat="server" ParameterName="Province" Code="CA"></cc1:DropDownListProvince></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 19px">
			<asp:Label id="lblPostalCode" runat="server" CssClass="csPlainText">PostalCode</asp:Label>:</TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch ParameterName="PostalCode" id="tbxPostalCode" runat="server"></cc1:TextBoxSearch></TD>
	</TR>
</TABLE>

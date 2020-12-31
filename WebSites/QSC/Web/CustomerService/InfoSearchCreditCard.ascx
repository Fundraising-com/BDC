<%@ Register TagPrefix="uc3" TagName="FiscalYearSelectControl" Src="../Common/FiscalYearSelectControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="InfoSearchCreditCard.ascx.cs" Inherits="QSPFulfillment.CustomerService.InfoSearchCreditCard" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
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
		<TD style="HEIGHT: 19px"><asp:label id="lblFirstName" runat="server" CssClass="csPlainText">Order ID</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxOrderID" runat="server" ParameterName="iOrderID"></cc1:textboxsearch>
			<asp:RangeValidator id="RangeValidator5" runat="server" ErrorMessage="Order ID must be between 1 and 2147483647."
				Type="Integer" ControlToValidate="tbxOrderID" MaximumValue="2147483647" MinimumValue="1">*</asp:RangeValidator></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblLastName" runat="server" CssClass="csPlainText">COHInstance</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxCOHInstance" runat="server" ParameterName="iCustomerOrderHeaderInstance"></cc1:textboxsearch>
			<asp:RangeValidator id="RangeValidator1" runat="server" ErrorMessage="COHInsatnce must be between 1 and 2147483647."
				Type="Integer" ControlToValidate="tbxCOHInstance" MaximumValue="2147483647" MinimumValue="1">*</asp:RangeValidator></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblCity" runat="server" CssClass="csPlainText">First Name</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxCity" runat="server" ParameterName="sFirstName"></cc1:textboxsearch></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblProvince" runat="server" CssClass="csPlainText">Last Name</asp:label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 21px">
			<cc1:TextBoxSearch id="TextBoxSearch3" runat="server" ParameterName="sLastName"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 18px"><asp:label id="lblPostalCode" runat="server" CssClass="csPlainText">Credit Card Number</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxCreditCardNumber" runat="server" ParameterName="iCreditCardNumber" MaxLength="16"></cc1:textboxsearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:label id="Label3" CssClass="csPlainText" runat="server">Authorization Code</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch id="TextBoxSearch1" runat="server" ParameterName="sAuthorizationCode"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD>
			<asp:label id="Label4" CssClass="csPlainText" runat="server">Return Code</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch id="TextBoxSearch2" runat="server" ParameterName="sReturnCode"></cc1:TextBoxSearch></TD>
	</TR>
</TABLE>

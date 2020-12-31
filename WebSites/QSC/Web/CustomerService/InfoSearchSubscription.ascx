<%@ Register TagPrefix="uc1" TagName="ControlerSearchCategory" Src="ControlerSearchCategory.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StateProvince" Src="../Common/StateProvince.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="InfoSearchSubscription.ascx.cs" Inherits="QSPFulfillment.CustomerService.InfoSearchSubscription" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="InfoSearchMagazine" Src="InfoSearchMagazine.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc3" TagName="FiscalYearSelectControl" Src="../Common/FiscalYearSelectControl.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td>
			<uc1:ControlerSearchCategory id="ctrlControlerSearchCategory" runat="server"></uc1:ControlerSearchCategory>
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 19px">
			<asp:label id="Label1" runat="server" cssclass="csPlainText" Font-Bold="True">Fiscal&nbsp;Year</asp:label>
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 19px">
			<uc3:FiscalYearSelectControl id="ctrlFiscalYearSelect" runat="server" ParameterName="FiscalYear"></uc3:FiscalYearSelectControl>
		</td>
	</tr>
	<TR>
		<TD style="HEIGHT: 18px">
			<asp:label id="Label15" CssClass="csPlainText" runat="server" Font-Bold="True">Recipient</asp:label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 19px"><asp:label id="lblLastName" runat="server" CssClass="csPlainText">Last Name</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch id="tbxRecipientLastName" runat="server" ParameterName="RecipientLastName" Validation="False"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblFirstName" runat="server" CssClass="csPlainText">First Name</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<cc1:TextBoxSearch id="tbxRecipientFirstName" runat="server" ParameterName="RecipientFirstName" Validation="False"></cc1:TextBoxSearch></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblCity" runat="server" CssClass="csPlainText">City</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxCity" runat="server" ParameterName="City"></cc1:textboxsearch></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblProvince" runat="server" CssClass="csPlainText">Province</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:dropdownlistprovince id="ddlProvince" runat="server" ParameterName="Province" Code="CA"></cc1:dropdownlistprovince></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 18px"><asp:label id="lblPostalCode" runat="server" CssClass="csPlainText">Postal Code</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxPostalCode" runat="server" ParameterName="PostalCode"></cc1:textboxsearch></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label11" Font-Bold="True" runat="server" CssClass="csPlainText">Product</asp:label></TD>
	</TR>
</TABLE>
<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
	<TBODY>
		<TR>
			<TD></TD>
		</TR>
		<TR>
			<TD><uc1:infosearchmagazine id="ctrlInfoSearchMagazine" runat="server" JavaSrciptFunction="SetTitleCodeSub"></uc1:infosearchmagazine></TD>
		</TR>
		<TR>
			<TD><asp:label id="Label8" Font-Bold="True" runat="server" CssClass="csPlainText">Subscription</asp:label></TD>
		</TR>
		<TR>
			<TD><asp:label id="Label5" runat="server" CssClass="csPlainText">COH Instance</asp:label></TD>
		</TR>
		<TR>
			<TD><cc1:textboxsearch id="tbxCOHInstance" runat="server" ParameterName="CustomerOrderHeaderInstance"></cc1:textboxsearch>
				<asp:RangeValidator id="RangeValidator1" runat="server" ControlToValidate="tbxCOHInstance" Type="Integer"
					ErrorMessage="Customer Order Header Instance must be between 1 and 2147483647." MaximumValue="2147483647"
					MinimumValue="1">*</asp:RangeValidator></TD>
		</TR>
		<TR>
			<TD><asp:label id="Label6" runat="server" CssClass="csPlainText">TransID</asp:label></TD>
		</TR>
		<TR>
			<TD><cc1:textboxsearch id="tbxTransID" runat="server" ParameterName="TransID"></cc1:textboxsearch>
				<asp:RangeValidator id="RangeValidator2" runat="server" ControlToValidate="tbxTransID" Type="Integer"
					ErrorMessage="Trans ID must be between 1 and 2147483647." MaximumValue="2147483647" MinimumValue="1">*</asp:RangeValidator></TD>
		</TR>
		<TR>
			<TD><asp:label id="Label2" runat="server" CssClass="csPlainText">Internet Order ID</asp:label></TD>
		</TR>
		<TR>
			<TD><cc1:textboxsearch id="tbxInternetOrderID" runat="server" ParameterName="InternetOrderID"></cc1:textboxsearch>
				<asp:RangeValidator id="Rangevalidator4" runat="server" ControlToValidate="tbxInternetOrderID" Type="Integer"
					ErrorMessage="Internet Order ID must be between 1 and 2147483647." MaximumValue="2147483647" MinimumValue="1">*</asp:RangeValidator></TD>
		</TR>
		<TR>
			<TD><asp:label id="Label3" runat="server" CssClass="csPlainText">From Date</asp:label></TD>
		</TR>
		<TR>
			<TD><uc1:dateentry id="ctrlDateEntryFrom" runat="server" ParameterName="FromDateSub"></uc1:dateentry></TD>
		</TR>
		<TR>
			<TD><asp:label id="Label4" runat="server" CssClass="csPlainText">To Date</asp:label></TD>
		</TR>
		<TR>
			<TD><uc1:dateentry id="ctrlDateEntryTo" runat="server" ParameterName="ToDateSub"></uc1:dateentry></TD>
		</TR>
		<TR>
			<TD><asp:label id="Label9" runat="server" CssClass="csPlainText">Remit ID</asp:label></TD>
		</TR>
		<TR>
			<TD><cc1:textboxsearch id="tbxRemitID" runat="server" ParameterName="RemitID"></cc1:textboxsearch>
				<asp:RangeValidator id="RangeValidator3" runat="server" ControlToValidate="tbxTransID" Type="Integer"
					ErrorMessage="Remit ID must be between 1 and 2147483647." MaximumValue="2147483647" MinimumValue="1">*</asp:RangeValidator></TD>
		</TR>
		<TR>
			<TD><asp:label id="Label10" runat="server" CssClass="csPlainText">Remit Date</asp:label></TD>
		</TR>
		<TR>
			<TD><uc1:dateentry id="DateEntry1" runat="server" ParameterName="RemitDate"></uc1:dateentry></TD>
		</TR>
		<TR>
			<TD><asp:label id="Label14" Font-Bold="True" runat="server" CssClass="csPlainText">Participant</asp:label></TD>
		</TR>
		<TR>
			<TD><asp:label id="Label12" runat="server" CssClass="csPlainText"> Last Name</asp:label></TD>
		</TR>
		<TR>
			<TD><cc1:textboxsearch id="tbxParticipantLastName" runat="server" ParameterName="ParticipantLastName"></cc1:textboxsearch></TD>
		</TR>
		<TR>
			<TD style="HEIGHT: 19px"><asp:label id="Label13" runat="server" CssClass="csPlainText"> First Name</asp:label></TD>
		</TR>
		<TR>
			<TD><cc1:textboxsearch id="tbxParticipantFirstName" runat="server" ParameterName="ParticipantFirstName"></cc1:textboxsearch></TD>
		</TR>
	</TBODY>
</TABLE>

<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="../../Common/AddressHygiene.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AddressMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.AddressMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td vAlign="top" width="50%"><asp:label id="Label5" runat="server" cssclass="CSPlainText">Type</asp:label></td>
		<td vAlign="middle" width="50%"><cc1:dropdownlistreq id="ddlType" runat="server" initialtext="Please select..." initialvalue="0" required="True"
				visible="False"></cc1:dropdownlistreq><asp:label id="lblType" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
		<TD vAlign="middle" width="50%"></TD>
		<TD vAlign="middle" width="50%"></TD>
	</tr>
	<tr>
		<td vAlign="middle" width="50%"><asp:label id="Label2" runat="server" cssclass="CSPlainText">Address Line 1</asp:label></td>
		<td vAlign="bottom" width="50%"><cc1:textboxreq id="tbxAddress1" runat="server" required="True" errormsgrequired="The field Address Line 1 is mandatory."
				columns="25" maxlength="50"></cc1:textboxreq></td>
		<TD vAlign="bottom" width="50%"></TD>
		<TD vAlign="bottom" width="50%"></TD>
	</tr>
	<tr>
		<td vAlign="middle" width="50%"><asp:label id="Label4" runat="server" cssclass="CSPlainText"> Address Line 2</asp:label></td>
		<td vAlign="bottom" width="50%"><asp:textbox id="tbxAddress2" runat="server" columns="25"></asp:textbox></td>
		<TD vAlign="bottom" width="50%"></TD>
		<TD vAlign="bottom" width="50%"></TD>
	</tr>
	<tr>
		<td vAlign="middle" width="50%"><asp:label id="Label1s" runat="server" cssclass="CSPlainText">City</asp:label></td>
		<td vAlign="bottom" width="50%"><cc1:textboxreq id="tbxCity" runat="server" required="True" errormsgrequired="The field City is mandatory."
				columns="25" maxlength="50"></cc1:textboxreq></td>
		<TD vAlign="bottom" width="50%"></TD>
		<TD vAlign="bottom" width="50%"></TD>
	<tr>
		<td vAlign="middle" width="50%"><asp:label id="StateProvinceLabel" runat="server" cssclass="CSPlainText"> Province</asp:label></td>
		<td vAlign="bottom" width="50%"><cc1:dropdownlistprovince id="ddlProvince" runat="server" initialtext="Please select..." errormsgrequired="The field province is mandatory."
				textfirstrow="Please select..." contenttype="int" AutoPostBack="True"></cc1:dropdownlistprovince></td>
		<TD vAlign="bottom" width="50%"></TD>
		<TD vAlign="bottom" width="50%"></TD>
	<tr>
		<td style="HEIGHT: 18px" vAlign="middle" width="50%"><asp:label id="PostalCodeLabel" runat="server" cssclass="CSPlainText">Postal Code</asp:label></td>
		<td style="HEIGHT: 18px" vAlign="bottom" width="50%"><cc1:postalcode id="tbxPostalCode" runat="server" required="True" errormsgrequired="The field Postal Code is mandatory."
				columns="7" AutoPostBack="True" contenttype="int" typedate="All" errormsgregexp="The ZIP / Postal code is invalid. Ex: 11111 or H1H1H1 or 11111-1111"
				parametername="Zip" MaxLength="10"></cc1:postalcode></td>
		<TD style="HEIGHT: 18px" vAlign="bottom" width="50%"></TD>
		<TD style="HEIGHT: 18px" vAlign="bottom" width="50%"></TD>
	</tr>
	<TR>
		<TD style="HEIGHT: 27px" width="50"><asp:label id="Label8" runat="server" cssclass="CSPlainText">Country</asp:label></TD>
		<TD style="HEIGHT: 27px" width="50" colSpan="2"><cc1:dropdownlistreq id="ddlCountry" runat="server" Required="True" InitialText="Please select..." Width="120px"
				AutoPostBack="True" onselectedindexchanged="ddlCountry_SelectedIndexChanged"></cc1:dropdownlistreq></TD>
		<TD style="HEIGHT: 27px"></TD>
		<TD style="HEIGHT: 27px"></TD>
	</TR>
	<tr>
		<td style="HEIGHT: 45px" colSpan="2"><asp:button id="btnValidateAddress" runat="server" CausesValidation="False" Text="Validate Address" onclick="btnValidateAddress_Click"></asp:button></td>
		<TD style="HEIGHT: 45px"></TD>
		<TD style="HEIGHT: 45px"></TD>
	</tr>
	<tr>
		<td style="HEIGHT: 22px" colSpan="2"><uc2:addresshygiene id="ctrlAddressHygiene" runat="server" visible="false"></uc2:addresshygiene><asp:label id="AddressHygieneStatusLabel" runat="server" ForeColor="Blue"></asp:label></td>
		<TD style="HEIGHT: 22px"></TD>
		<TD style="HEIGHT: 22px"></TD>
	</tr>
</table>

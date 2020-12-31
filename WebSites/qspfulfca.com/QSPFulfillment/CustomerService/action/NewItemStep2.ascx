<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NewItemStep2.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.NewItemStep2" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="../../Common/AddressHygiene.ascx" %>
<br>
<table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
	<tr>
		<td>
			<asp:label id="Label12" cssclass="csPlainText" runat="server">First Name</asp:label></td>
		<td>
			<asp:textbox id="tbxFirstName" runat="server"></asp:textbox></td>
		<td></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label13" cssclass="csPlainText" runat="server">Last Name</asp:label></td>
		<td>
			<asp:textbox id="tbxLastName" runat="server"></asp:textbox></td>
		<td></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label14" cssclass="csPlainText" runat="server"> Address Line 1</asp:label></td>
		<td>
			<asp:textbox id="tbxStreet1" runat="server"></asp:textbox></td>
		<td></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label15" cssclass="csPlainText" runat="server">Address Line 2</asp:label></td>
		<td>
			<asp:textbox id="tbxStreet2" runat="server"></asp:textbox></td>
		<td></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label33" cssclass="csPlainText" runat="server">City</asp:label></td>
		<td>
			<asp:textbox id="tbxCity" runat="server"></asp:textbox></td>
		<td></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label16" cssclass="csPlainText" runat="server">Postal Code</asp:label></td>
		<td>
			<cc1:postalcode id="tbxPostalCode" runat="server" maxlength="6"></cc1:postalcode></td>
		<td></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label17" cssclass="csPlainText" runat="server">Province</asp:label></td>
		<td>
			<cc1:dropdownlistprovince id="ddlProvince" runat="server" code="CA"></cc1:dropdownlistprovince></td>
		<td></td>
	</tr>
	<tr>
		<td colSpan="2" style="HEIGHT: 45px">
			<asp:button id="btnValidateAddress" runat="server" Text="Validate Address" CausesValidation="False" onclick="btnValidateAddress_Click"></asp:button></td>
	</tr>
	<tr>
		<td colspan="2" style="HEIGHT: 22px">
			<uc2:addressHygiene id="ctrlAddressHygiene" runat="server" visible="false"></uc2:addressHygiene>
			<asp:Label id="AddressHygieneStatusLabel" runat="server" ForeColor="Blue"></asp:Label></td>
	</tr>
	<tr>
		<td colspan="3">&nbsp;</td>
	</tr>
	<tr>
		<td><asp:label id="Label1" cssclass="CSPlainText" runat="server">Product Code</asp:label></td>
		<td><asp:label id="lblProductCode" cssclass="CSPlainText" runat="server"></asp:label></td>
		<td></A></td>
	<tr>
		<td><asp:label id="Label2" cssclass="CSPlainText" runat="server">Product Name</asp:label></td>
		<td><asp:label id="lblProductName" cssclass="CSPlainText" runat="server"></asp:label></td>
		<td></td>
	</tr>
	<tr>
		<td><asp:label id="Label5" cssclass="CSPlainText" runat="server">Price</asp:label></td>
		<td><cc1:currency id="tbxPrice" runat="server" required="True"></cc1:currency></td>
		<td></td>
	</tr>
	<tr>
		<td><asp:label id="Label6" cssclass="CSPlainText" runat="server">Catalog Price</asp:label></td>
		<td><asp:label id="lblCatalogPrice" cssclass="CSPlainText" runat="server"></asp:label></td>
		<td></td>
	</tr>
	<tr>
		<td><asp:label id="Label7" cssclass="CSPlainText" runat="server">Price override reason</asp:label></td>
		<td><asp:dropdownlist id="ddlPriceOverrideReason" runat="server"></asp:dropdownlist></td>
		<td><asp:button id="btnBack" runat="server" causesvalidation="false" text="Change Selection" onclick="btnBack_Click"></asp:button></td>
	</tr>
</table>

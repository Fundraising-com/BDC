<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CreditCard.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.CreditCard" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="../../Common/AddressHygiene.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="ControlerSubscriptionForCOHI" Src="../ControlerSubscriptionForCOHI.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerPaymentInfo" Src="../ControlerPaymentInfo.ascx" %>
<br>
<uc1:controlerpaymentinfo id="ctrlControlerPaymentInfo" runat="server"></uc1:controlerpaymentinfo><br>
<br>
<table cellspacing="0" cellpadding="0" border="0">
	<tr style="PADDING-BOTTOM: 10px">
		<td><asp:label id="lblAddressInfo" cssclass="csPlainText" runat="server" font-bold="True">Cardholder's Address Information</asp:label></td>
		<td></td>
	</tr>
	<tr>
		<td><asp:label id="Label31" runat="server" cssclass="csPlainText"> Address Line 1</asp:label></td>
		<td><cc1:textboxreq id="tbxStreet1" runat="server" required="True" errormsgrequired="The Address Line 1 is mandatory."></cc1:textboxreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label41" runat="server" cssclass="csPlainText">Address Line 2</asp:label></td>
		<td>
			<asp:textbox id="tbxStreet2" runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label333" runat="server" cssclass="csPlainText">City</asp:label>
		</td>
		<td>
			<cc1:textboxreq id="tbxCity" runat="server" required="True" errormsgrequired="The City is mandatory."></cc1:textboxreq></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label51" runat="server" cssclass="csPlainText">Province</asp:label></td>
		<td>
			<cc1:dropdownlistprovince id="ddlProvince" textfirstrow="Select" runat="server" code="CA"></cc1:dropdownlistprovince></td>
	</tr>
    <tr>
		<td>
			<asp:label id="Label71" runat="server" cssclass="csPlainText">Postal Code</asp:label></td>
		<td>
			<cc1:postalcode maxlength="6" id="tbxPostalCode" runat="server" required="True" errormsgrequired="The Postal Code is mandatory."
				errormsgregexp="The Postal Code is invalid. Ex: H1H1H1"></cc1:postalcode></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label61" runat="server" cssclass="csPlainText"> Country</asp:label></td>
		<td>
			<asp:dropdownlist id="ddlCountry" runat="server">
				<asp:listitem value="CA">Canada</asp:listitem>
			</asp:dropdownlist></td>
	</tr>
    <tr>
		<td colSpan="2" style="HEIGHT: 45px">
			<asp:button id="btnValidateAddress" runat="server" Text="Validate Address" CausesValidation="False" onclick="btnValidateAddress_Click"></asp:button></td>
	<tr>
		<td colspan="2" style="HEIGHT: 22px">
			<uc2:addressHygiene id="ctrlAddressHygiene" runat="server" visible="false"></uc2:addressHygiene>
			<asp:Label id="AddressHygieneStatusLabel" runat="server" ForeColor="Blue"></asp:Label></td>
	</tr>
	<tr style="PADDING-TOP: 40px">
		<td style="PADDING-RIGHT: 30px"><asp:label id="lblAmountDesc" runat="server" cssclass="csPlainText">The amount charged to the credit card will be:</asp:label></td>
		<td>
			<cc1:textboxfloat id="tbxAmount" runat="server" readonly="True" required="True" errormsgrequired="The Amount Charged is mandatory."></cc1:textboxfloat>
			<asp:rangevalidator id="ravAmountRange" runat="server" display="Dynamic" errormessage="The Amount Charged cannot be 0." CultureInvariantValues="true"
				controltovalidate="tbxAmount" minimumvalue=".01" type="Double" maximumvalue="1000.00">*</asp:rangevalidator>
		</td>
	</tr>
</table>
<br>
<uc1:controlersubscriptionforcohi id="ctrlControlerSubscriptionForCOHI" runat="server" showcheckboxesforchadd="true"
	showcancelledsubs="true" showprices="true" creditcardbounced="true"></uc1:controlersubscriptionforcohi>

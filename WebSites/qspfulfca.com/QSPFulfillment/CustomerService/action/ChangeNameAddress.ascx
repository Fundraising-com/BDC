<%@ Register TagPrefix="uc1" TagName="ControlerSubscriptionForCOHI" Src="../ControlerSubscriptionForCOHI.ascx" %>
<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="../../Common/AddressHygiene.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ChangeNameAddress.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.ChangeNameAddress" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<br>
<table width="100%">
	<TBODY>
		<tr>
			<td><asp:label id="Label1" cssclass="csPlainText" runat="server">First Name</asp:label></td>
			<td><cc1:textboxreq id="tbxFirstName" runat="server" errormsgrequired="The field First Name is mandatory."
					required="True"></cc1:textboxreq></td>
		</tr>
		<tr>
			<td><asp:label id="Label2" cssclass="csPlainText" runat="server">Last Name</asp:label></td>
			<td><cc1:textboxreq id="tbxLastName" runat="server" errormsgrequired="The field Last Name is mandatory."></cc1:textboxreq></td>
		</tr>
		<tr>
			<td><asp:label id="Label3" cssclass="csPlainText" runat="server"> Address Line 1</asp:label></td>
			<td><cc1:textboxreq id="tbxStreet1" runat="server" errormsgrequired="The field Address Line 1 is mandatory."></cc1:textboxreq></td>
		</tr>
		<tr>
			<td><asp:label id="Label4" cssclass="csPlainText" runat="server">Address Line 2</asp:label></td>
			<td><asp:textbox id="tbxStreet2" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td><asp:label id="Label33" cssclass="csPlainText" runat="server">City</asp:label></td>
			<td><cc1:textboxreq id="tbxCity" runat="server" errormsgrequired="The field City is mandatory."></cc1:textboxreq></td>
		</tr>
		<tr>
			<td><asp:label id="Label5" cssclass="csPlainText" runat="server">Province</asp:label></td>
			<td><cc1:dropdownlistprovince id="ddlProvince" runat="server" code="CA"></cc1:dropdownlistprovince></td>
		</tr>
		<tr>
			<td><asp:label id="Label7" cssclass="csPlainText" runat="server">Postal Code</asp:label></td>
			<td><cc1:postalcode id="tbxPostalCode" runat="server" errormsgrequired="The field Postal Code is mandatory."
					required="True" maxlength="6"></cc1:postalcode></td>
		</tr>
		<tr>
			<td><asp:label id="Label6" cssclass="csPlainText" runat="server"> Country</asp:label></td>
			<td><asp:dropdownlist id="ddlCountry" runat="server">
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
		<tr>
			<td colSpan="2"><br>
				<asp:label id="lblSubscriptions" cssclass="csPlainText" runat="server" font-bold="True">Change of address will also affect these subscriptions:</asp:label><br>
				<uc1:controlersubscriptionforcohi id="ctrlControlerSubscriptionForCOHI" runat="server" showcurrentsubscription="false"
					showcheckboxesforchadd="true"></uc1:controlersubscriptionforcohi></td>
		</tr>
	</TBODY>
</table>
</TD></TR></TBODY></TABLE>

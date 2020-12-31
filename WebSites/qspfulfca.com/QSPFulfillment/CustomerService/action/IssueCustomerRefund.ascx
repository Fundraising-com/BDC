<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="../../Common/AddressHygiene.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="IssueCustomerRefund.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.IssueCustomerRefund" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<br>
<table id="Table2" cellspacing="0" cellpadding="0" border="0">
	<tr>
		<td colspan="2">
			<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tbody>
					<tr>
						<td>
							<asp:label id="Label4" runat="server" cssclass="CSPlainText">Max Refund Amount:</asp:label></td>
		</td>
		<td>
			<asp:label id="lblRegularPrice" runat="server" cssclass="CSPlainText" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr>
		<td>
			<asp:label id="lblRefundAmount" runat="server" cssclass="CSPlainText">Refund Amount:</asp:label></td>
		<td>
			<cc1:currency id="tbxRefundAmount" runat="server"></cc1:currency></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label2" runat="server" cssclass="CSPlainText">Reason for refund:</asp:label></td>
		<td>
			<asp:textbox id="tbxRefundReason" runat="server" textmode="MultiLine" width="300" style="FONT-SIZE:8pt; COLOR:#000000; FONT-FAMILY:verdana,arial"></asp:textbox></td>
	</tr>
	<tr>
		<td>
			<br>
			<asp:label id="lblHeaderRefundTo" runat="server" cssclass="CSPlainText" font-bold="true">Refund To:</asp:label><br>
		</td>
		<td></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label1" runat="server" cssclass="CSPlainText">First Name</asp:label></td>
		<td>
			<asp:textbox id="tbxFirstName" runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label3" runat="server" cssclass="CSPlainText">Last Name</asp:label></td>
		<td>
			<asp:textbox id="tbxLastName" runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label31" runat="server" cssclass="csPlainText"> Address Line 1</asp:label>
		</td>
		<td>
			<asp:textbox id="tbxStreet1" runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label41" runat="server" cssclass="csPlainText">Address Line 2</asp:label></td>
		<td>
			<asp:textbox id="tbxStreet2" runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label333" runat="server" cssclass="csPlainText">City</asp:label>
		</td>
		<td>
			<asp:textbox id="tbxCity" runat="server"></asp:textbox></td>
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
			<cc1:postalcode maxlength="6" id="tbxPostalCode" runat="server"></cc1:postalcode></td>
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
	</tr>
	<tr>
		<td colspan="2" style="HEIGHT: 22px">
			<uc2:addressHygiene id="ctrlAddressHygiene" runat="server" visible="false"></uc2:addressHygiene>
			<asp:Label id="AddressHygieneStatusLabel" runat="server" ForeColor="Blue"></asp:Label></td>
	</tr>
</table>
</TD></TR></TBODY></TABLE>

<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="ControlerConfirmationPage.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="../Common/AddressHygiene.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="couponstep1.ascx.cs" Inherits="QSPFulfillment.CustomerService.couponstep1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<br>
<table class="CSTable" id="Table1" cellspacing="0" cellpadding="2" width="100%">
	<tr>
		<td class="CSTableHeader" colspan="2">Customer Information</td>
	</tr>
	<tr>
		<td>
			<table width="100%">
				<tr>
					<td width="250">
						<asp:label id="Label1" cssclass="csPlainText" runat="server">First Name</asp:label></td>
					<td>
						<asp:textbox id="tbxFirstName" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label2" cssclass="csPlainText" runat="server">Last Name</asp:label></td>
					<td>
						<asp:textbox id="tbxLastName" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label3" cssclass="csPlainText" runat="server"> Address Line 1</asp:label></td>
					<td>
						<asp:textbox id="tbxStreet1" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label4" cssclass="csPlainText" runat="server">Address Line 2</asp:label></td>
					<td>
						<asp:textbox id="tbxStreet2" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label33" cssclass="csPlainText" runat="server">City</asp:label></td>
					<td>
						<asp:textbox id="tbxCity" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label7" cssclass="csPlainText" runat="server">Postal Code</asp:label></td>
					<td>
						<cc1:postalcode id="tbxPostalCode" runat="server" maxlength="6"></cc1:postalcode></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label5" cssclass="csPlainText" runat="server">Province</asp:label></td>
					<td>
						<cc1:dropdownlistprovince id="ddlProvince" runat="server" code="CA"></cc1:dropdownlistprovince></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label6" cssclass="csPlainText" runat="server"> Country</asp:label></td>
					<td>
						<asp:dropdownlist id="ddlCountry" runat="server">
							<asp:listitem value="CA">Canada</asp:listitem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label8" runat="server" cssclass="csPlainText">Email</asp:label></td>
					<td>
						<cc1:email id="tbxEmail" runat="server"></cc1:email></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label9" runat="server" cssclass="csPlainText">Phone Number</asp:label></td>
					<td>
						<cc1:phone id="tbxPhoneNumber" runat="server" maxlength="12"></cc1:phone></td>
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
					<td></td>
					<td></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="CSTableHeader" colspan="2">Campaign&nbsp;Information</td>
	</tr>
	<tr>
		<td colspan="2">
			<table id="Table3" width="100%">
				<tr>
					<td width="250">
						<asp:label id="Label10" cssclass="csPlainText" runat="server">Campaign ID</asp:label></td>
					<td>
					
                        <cc1:TextBoxInteger ID="tbxCampaignID" runat="server" required="True" ErrorMsgRegExp="Number Required"   maxlength="12" errormsgrequired="CampaignID"></cc1:TextBoxInteger>
                    <asp:HyperLink id=hypFindCampaignID Runat="server" ImageUrl="images/find.gif" >
										</asp:hyperlink>
                    </td>
                    
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="CSTableHeader" colspan="2">Certificate&nbsp;Information</td>
	</tr>
	<tr>
		<td colspan="2">
			<table id="Table2" width="100%">
				<tr>
					<td width="250">
						<asp:label id="Label15" cssclass="csPlainText" runat="server">Certificate Number</asp:label></td>
					<td>
						<asp:textbox id="tbxCoupon" runat="server" maxlength="12"></asp:textbox></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="CSTableHeader" colspan="2">Order&nbsp;Information</td>
	</tr>
	<tr>
		<td colspan="2">
			<table id="Table4" width="100%">
				<tr>
					<td width="250">
						<asp:label id="Label11" cssclass="csPlainText" runat="server">Invoice Order</asp:label></td>
					<td>
                        <asp:checkbox id="InvoiceOrderCheckBox" runat="server" Checked="false"></asp:checkbox>
                    </td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<uc1:controlerconfirmationpage id="ctrlControlerConfirmationPage" runat="server"></uc1:controlerconfirmationpage>

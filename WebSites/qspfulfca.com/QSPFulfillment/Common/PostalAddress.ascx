<%@ Register TagPrefix="UC" TagName="StateProvince" Src="../Common/StateProvince.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PostalAddress.ascx.cs" Inherits="QSPFulfillment.CommonWeb.UC.PostalAddress" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table>
	<tr>
		<td  class="label"><asp:label id="Label1" runat="server" CssClass="csPlainText">Street 1</asp:label></td>
		<td><asp:textbox id="street1" MaxLength="50" Columns="55" Runat="server" /><asp:requiredfieldvalidator id="rq_street1" ErrorMessage="Please enter a street1" Text="*" display="Dynamic"
				ControlToValidate="street1" runat="server" /><asp:label id="lblStreet1" runat="server" CssClass="csPlainText" /></td>
	</tr>
	<tr>
		<td  class="label"><asp:label id="Label2" runat="server" CssClass="csPlainText">Street 2</asp:label></td>
		<td class="field"><asp:textbox id="street2" MaxLength="50" Columns="55" Runat="server" /><asp:label id="lblStreet2" runat="server" CssClass="csPlainText" /></td>
	</tr>
	<tr>
		<td  class="label"><asp:label id="Label3" runat="server" CssClass="csPlainText">City</asp:label></td>
		<td class="field"><asp:textbox id="city" MaxLength="50" Columns="35" Runat="server" /><asp:requiredfieldvalidator id="rq_city" ErrorMessage="Please enter a City" Text="*" display="Dynamic" ControlToValidate="city"
				runat="server" /><asp:label id="lblCity" runat="server" CssClass="csPlainText" /></td>
	</tr>
	<tr>
		<td ><asp:label id="lbStateProvince" Runat="server" CssClass="csPlainText" /></td>
		<td><UC:STATEPROVINCE id="StateProvince" Runat="server" /><asp:label id="lblStateProvince" runat="server" CssClass="csPlainText" /></td>
	</tr>
	<tr>
		<td  class="label"><asp:label  id="Label4" runat="server" CssClass="csPlainText">Postal Code</asp:label></td>
		<td class="field">
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:textbox id="PostalCode" MaxLength="5" Columns="5" Runat="server" /><asp:requiredfieldvalidator id="rq_PostalCode" ErrorMessage="Please enter a postal code." Text="*" ControlToValidate="PostalCode"
							runat="server" Display="Dynamic" /><asp:regularexpressionvalidator id="reg_PostalCode" ErrorMessage="Please enter a valid postal code." Text="*" ControlToValidate="PostalCode"
							runat="server" Display="Dynamic" ValidationExpression="\d{5}"></asp:regularexpressionvalidator></td>
					<td id="tablecell_PostalCode4" runat="server"><asp:Label Runat=server CssClass="csPlainText">&nbsp;&nbsp;Zip+4&nbsp;</asp:Label><asp:textbox id="PostalCode4" MaxLength="4" Columns="5" Runat="server" />
					</td>
				</tr>
			</table>
			<asp:label id="lblPostalCode" runat="server" CssClass="csPlainText" /></td>
	</tr>
	<tr>
		<td class="label"><asp:label  id="Label5" runat="server" CssClass="csPlainText">Country</asp:label></td>
		<td class="field">
			<asp:dropdownlist id="ddlCountry" Runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
				<asp:ListItem Value="CA" Text="Canada" Selected="True" />
			</asp:dropdownlist><asp:label id="lblCountry" runat="server" CssClass="csPlainText" />
		</td>
	</tr>
</table>

<%@ Register TagPrefix="uc1" TagName="PostalAddressDisabled" Src="../../Common/PostalAddressDisabled.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerAddress.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.ControlerAddress" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<table id="Table3">
	<tr>
		<td>
			<asp:label id="lbType" runat="server" cssclass="csPlainText"> Type</asp:label>
		</td>
		<td>
			<!--ID="tbType" CssClass="noBorder"-->
			<asp:dropdownlist id="ddlAddressType" runat="server">
				<asp:listitem value="54000" text="Select one" selected="True" />
				<asp:listitem value="54001" text="Ship To" />
				<asp:listitem value="54002" text="Bill To" />
				<asp:listitem value="54003" text="Secondary" />
				<asp:listitem value="54004" text="Home" />
			</asp:dropdownlist>
		</td>
	</tr>
	<tr>
		<td></td>
		<td></td>
	</tr>
	<tr>
		<td>
			<asp:label id="lbStreet1" runat="server" cssclass="csPlainText"> Address Line 1</asp:label>
		</td>
		<td>
			<asp:textbox id="tbxStreet1" runat="server" />
		</td>
	</tr>
	<tr>
		<td>
			<asp:label id="lbStreet2" runat="server" cssclass="csPlainText">Address Line 2</asp:label>
		</td>
		<td>
			<asp:textbox id="tbxStreet2" runat="server" />
		</td>
	</tr>
	<tr>
		<td>
			<asp:label id="lbCity" runat="server" cssclass="csPlainText">City</asp:label>
		</td>
		<td>
			<asp:textbox id="tbxCity" runat="server" />
		</td>
	</tr>
	<tr>
		<td>
			<asp:label id="lbPostalCode" runat="server" cssclass="csPlainText">Postal Code</asp:label>
		</td>
		<td>
			<cc1:postalcode id="tbxPostalCode" runat="server" maxlength="6" />
		</td>
	</tr>
	<tr>
		<td>
			<asp:label id="lbStateProvince" runat="server" cssclass="csPlainText">Province</asp:label>
		</td>
		<td>
			<cc1:dropdownlistprovince id="ddlProvince" runat="server" code="CA" />
		</td>
	</tr>
	<tr>
		<td>
			<asp:label id="lbCountry" runat="server" cssclass="csPlainText"> Country</asp:label>
		</td>
		<td>
			<asp:dropdownlist id="ddlCountry" runat="server" style="COLOR: black; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
				<asp:listitem value="CA">Canada</asp:listitem>
			</asp:dropdownlist>
		</td>
	</tr>
	<tr>
		<td colspan="2" align="center">
			&nbsp;<br>
			<asp:button id="btnSave" text="Save" runat="server" cssclass="fields2" onclick="btnSave_Click" /><br>
		</td>
	</tr>
</table>

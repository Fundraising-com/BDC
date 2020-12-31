<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="AddressHygiene.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Address.ascx.cs" Inherits="QSPFulfillment.CommonWeb.UC.Address" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td vAlign="middle" width="25%" colSpan="1" rowSpan="1" ><asp:label id="Address1Label" runat="server" cssclass="CSPlainText">Address Line 1</asp:label></td>
		<td vAlign="bottom" width="25%" colSpan="1" rowSpan="1" ><cc1:textboxreq id="Address1Tbx" runat="server" errormsgrequired="The field Address Line 1 is mandatory."
				required="True" columns="25" maxlength="50"></cc1:textboxreq></td>
		<TD vAlign="middle" width="25%" colSpan="1" rowSpan="1"><asp:label id="Address2Label" runat="server" cssclass="CSPlainText"> Address Line 2</asp:label></TD>
		<TD vAlign="bottom" width="25%"><asp:textbox id="Address2Tbx" runat="server" columns="25"></asp:textbox></TD>
	</tr>
	<tr>
		<td vAlign="middle" width="25%" colSpan="1" rowSpan="1" ><asp:label id="CityLabel" runat="server" cssclass="CSPlainText">City</asp:label></td>
		<td vAlign="middle" width="25%" colSpan="1" rowSpan="1" ><cc1:textboxreq id="CityTbx" runat="server" errormsgrequired="The field City is mandatory." required="True"
				columns="25" maxlength="50"></cc1:textboxreq></td>
		<td vAlign="middle" width="25%" colSpan="1" rowSpan="1"><asp:label id="PostalCodeLabel" runat="server" cssclass="CSPlainText">Postal Code / Zip Code</asp:label></td>
		<td vAlign="middle" width="25%" colSpan="1" rowSpan="1"><cc1:postalcode id="PostalCodeTbx" runat="server" maxlength="10" required="True" errormsgrequired="The field Zip / Postal Code is mandatory."
				contenttype="int" parametername="Zip" columns="7" errormsgregexp="The ZIP / Postal code is invalid. Ex: 11111 or H1H1H1 or 11111-1111"
				typedate="All"></cc1:postalcode></td>
	</tr>
	<tr>
		<td style="HEIGHT: 10px" vAlign="middle" width="25%" colSpan="1" rowSpan="1"><asp:label id="StateProvinceLabel" runat="server" cssclass="CSPlainText">State/Province</asp:label></td>
		<td style="HEIGHT: 10px" vAlign="middle" width="25%" colSpan="1" rowSpan="1"><cc1:dropdownlistprovince id="StateProvinceDDL" runat="server" errormsgrequired="The field Province is mandatory."
				required="True" code="CA" textfirstrow="Please select..." initialtext="Please select..."></cc1:dropdownlistprovince></td>
		<td style="HEIGHT: 10px" vAlign="middle" width="25%" colSpan="1" rowSpan="1"><asp:label id="CountyLabel" runat="server" cssclass="CSPlainText">County</asp:label></td>
		<td style="HEIGHT: 10px" vAlign="middle" width="25%" colSpan="1" rowSpan="1"><asp:textbox id="CountyTbx" runat="server" columns="25"></asp:textbox></td>
	</tr>
	<tr>
		<td vAlign="middle" width="25%" colSpan="1" rowSpan="1" style="HEIGHT: 2px"><asp:label id="CountryLabel" runat="server" cssclass="csPlainText"> Country</asp:label></td>
		<td vAlign="middle" width="25%" colSpan="1" rowSpan="1" style="HEIGHT: 2px"><cc1:dropdownlistreq id="CountryDDL" runat="server" errormsgrequired="Please select a Country." required="True"
				parametername="Country" contenttype="string" AutoPostBack="True"></cc1:dropdownlistreq></td>
		<td vAlign="middle" width="25%" colSpan="1" rowSpan="1" style="HEIGHT: 2px"></td>
		<td vAlign="middle" width="25%" colSpan="1" rowSpan="1" style="HEIGHT: 2px"></td>
	</tr>
	<TR>
		<TD style="HEIGHT: 33px" colSpan="2" vAlign=bottom width="25%">
			<asp:button id="ValidateAddressButton" runat="server" Text="Validate Address" CausesValidation="False" onclick="ValidateAddressButton_Click"></asp:button></TD>
		<TD style="HEIGHT: 33px" vAlign=bottom width="25%"></TD>
		<TD style="HEIGHT: 33px" vAlign=bottom width="25%"></TD>
	</TR>
	<tr>
		<td style="HEIGHT: 22px" colSpan="2" width="25%"><uc2:addresshygiene id="AddressHygieneControl" runat="server" visible="false"></uc2:addresshygiene><asp:label id="AddressHygieneStatusLabel" runat="server" ForeColor="Blue"></asp:label></td>
		<TD style="HEIGHT: 22px" width="25%"></TD>
		<TD style="HEIGHT: 22px" width="25%"></TD>
	</tr>
</table>
<br>

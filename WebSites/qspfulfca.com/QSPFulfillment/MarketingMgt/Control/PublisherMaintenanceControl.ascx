<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PublisherMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.PublisherMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="PublisherContactSearchControl" Src="PublisherContactSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PublisherContactMaintenanceControl" Src="PublisherContactMaintenanceControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="../../Common/AddressHygiene.ascx" %>

<table id="tblMaintenance" style="BORDER-RIGHT: silver 1px solid; BORDER-TOP: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-BOTTOM: silver 1px solid"
	cellpadding="3" runat="server">
	<tr>
		<td><br>
			<asp:label id="Label1" runat="server" cssclass="csPlainText">Publisher Name *</asp:label></td>
		<td><br>
			<cc1:textboxreq id="tbxName" runat="server" columns="64" maxlength="80" required="True" errormsgrequired="The field Publisher Name is mandatory."></cc1:textboxreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label2" runat="server" cssclass="csPlainText">Status *</asp:label></td>
		<td><cc1:dropdownlistreq id="ddlStatus" runat="server" parametername="Status" contenttype="int" errormsgrequired="The field Status is mandatory."
				required="True"></cc1:dropdownlistreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label3" runat="server" cssclass="csPlainText">Address Line 1 *</asp:label></td>
		<td><cc1:textboxreq id="tbxAddress1" runat="server" columns="50" maxlength="50" required="True" errormsgrequired="The field Address Line 1 is mandatory."></cc1:textboxreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label4" runat="server" cssclass="csPlainText">Address Line 2</asp:label></td>
		<td><cc1:textboxsearch id="tbxAddress2" runat="server" columns="50" maxlength="50" parametername="Address2"
				contenttype="string"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td><asp:label id="Label8" runat="server" cssclass="csPlainText">City *</asp:label></td>
		<td><cc1:textboxreq id="tbxCity" runat="server" columns="50" maxlength="50" required="True" errormsgrequired="The field City is mandatory."></cc1:textboxreq></td>
	</tr>
	<tr>
		<td style="HEIGHT: 19px"><asp:label id="Label10" runat="server" cssclass="csPlainText">State / Province *</asp:label></td>
		<td style="HEIGHT: 19px"><cc1:dropdownlistprovince id="ddlStateProvince" runat="server" parametername="StateProvince" contenttype="int"
				astextfirstrow="False"></cc1:dropdownlistprovince>
			<asp:requiredfieldvalidator id="rfvStateProvince" runat="server" errormessage="Please select a State / Province."
				controltovalidate="ddlStateProvince">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td><asp:label id="Label33" runat="server" cssclass="csPlainText">Zip / Postal Code *</asp:label></td>
		<td><cc1:postalcode id="tbxZip" runat="server" columns="7" maxlength="10" parametername="Zip" contenttype="int"
				typedate="All" required="True" errormsgrequired="The field Zip / Postal Code is mandatory."
				errormsgregexp="The ZIP / Postal code is invalid. Ex: 11111 or 11111-1111 or H1H1H1" clientscript="True"></cc1:postalcode></td>
	</tr>
	<tr>
		<td style="HEIGHT: 16px"><asp:label id="Label7" runat="server" cssclass="csPlainText">Country *</asp:label></td>
		<td style="HEIGHT: 16px"><cc1:dropdownlistreq id="ddlCountry" runat="server" parametername="Country" contenttype="string" errormsgrequired="Please select a Country."
				required="True"></cc1:dropdownlistreq></td>
	</tr>
	<tr>
		<td colSpan="2" style="HEIGHT: 45px">
			<asp:button id="btnValidateAddress" runat="server" Text="Validate Address" CausesValidation="False"></asp:button></td>
	</tr>
	<tr>
		<td colspan="2" style="HEIGHT: 22px">
			<uc2:addressHygiene id="ctrlAddressHygiene" runat="server" visible="false"></uc2:addressHygiene>
			<asp:Label id="AddressHygieneStatusLabel" runat="server" ForeColor="Blue"></asp:Label></td>
	</tr>
	<tr>
		<td style="PADDING-RIGHT: 50px; PADDING-LEFT: 50px; PADDING-BOTTOM: 50px; PADDING-TOP: 50px"
			colspan="2">
			<asp:label id="lblInstructions" runat="server" cssclass="csPlainText" font-bold="True">Publisher contacts:</asp:label>
			<br>
			<br>
			<uc1:publishercontactsearchcontrol id="ctrlPublisherContactSearchControl" runat="server"></uc1:publishercontactsearchcontrol><br>
			<asp:button id="btnCreateNew" runat="server" text="Create New Publisher Contact" cssclass="boxlook" onclick="btnCreateNew_Click"></asp:button></td>
	</tr>
	<tr>
		<td align="right" colspan="2"><asp:button id="btnSubmit" runat="server" text="Save" cssclass="boxlook" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" runat="server" text="Cancel" causesvalidation="False" cssclass="boxlook" onclick="btnCancel_Click"></asp:button></td>
	</tr>
</table>
<uc1:publishercontactmaintenancecontrol id="ctrlPublisherContactMaintenanceControl" runat="server" visible="false"></uc1:publishercontactmaintenancecontrol>

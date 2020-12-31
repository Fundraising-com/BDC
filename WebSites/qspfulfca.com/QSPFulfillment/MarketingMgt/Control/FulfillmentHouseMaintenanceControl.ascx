<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="../../Common/AddressHygiene.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FulfillmentHouseContactMaintenanceControl" Src="FulfillmentHouseContactMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FulfillmentHouseContactSearchControl" Src="FulfillmentHouseContactSearchControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FulfillmentHouseMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.FulfillmentHouseMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table id="tblMaintenance" style="BORDER-RIGHT: silver 1px solid; BORDER-TOP: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-BOTTOM: silver 1px solid"
	cellPadding="3" runat="server">
	<tr>
		<td><br>
			<asp:label id="Label1" runat="server" cssclass="csPlainText"> Fulfillment House Name *</asp:label></td>
		<td><br>
			<cc1:textboxreq id="tbxName" runat="server" errormsgrequired="The field Fulfillment House Name is mandatory."
				required="True" maxlength="80"></cc1:textboxreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label2" runat="server" cssclass="csPlainText">Status *</asp:label></td>
		<td><cc1:dropdownlistreq id="ddlStatus" runat="server" errormsgrequired="Please select a Status." required="True"
				parametername="Status" contenttype="int"></cc1:dropdownlistreq></td>
	</tr>
	<tr>
		<td style="HEIGHT: 16px"><asp:label id="Label7" runat="server" cssclass="csPlainText">Interface Media *</asp:label></td>
		<td style="HEIGHT: 16px"><cc1:dropdownlistreq id="ddlInterfaceMedia" runat="server" errormsgrequired="Please select an Interface Media"
				required="True" parametername="InterfaceMediaID" contenttype="int" initialvalue="0" initialtext="Please select..."></cc1:dropdownlistreq></td>
	</tr>
	<tr>
		<td style="HEIGHT: 10px"><asp:label id="Label5" runat="server" cssclass="csPlainText">Interface Layout *</asp:label></td>
		<td style="HEIGHT: 10px"><cc1:dropdownlistreq id="ddlInterfaceLayout" runat="server" errormsgrequired="Please select an Interface Layout"
				parametername="InterfaceLayoutID" contenttype="int" enabled="False"></cc1:dropdownlistreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label11" runat="server" cssclass="csPlainText"> Transmission Method</asp:label></td>
		<td><cc1:dropdownlistreq id="ddlTransmissionMethod" runat="server" errormsgrequired="Please select a Transmission Method."
				required="True" parametername="TransmissionMethodID" contenttype="int" initialvalue="0" initialtext="Please select..."></cc1:dropdownlistreq><A id="A1" onclick="javasrcipt:Open('Magazine.aspx?IsNewWindow=true&amp;ID=true&amp;IsOnlyMagazine=true')"
				href="javascript:;" runat="server"></A></td>
	</tr>
	<tr>
		<td><asp:label id="Label9" runat="server" cssclass="csPlainText">Hard Copy</asp:label></td>
		<td><asp:radiobuttonlist id="rblHardCopy" runat="server" cssclass="csPlainText" repeatdirection="Horizontal"
				Width="144px">
				<asp:ListItem Value="true">True</asp:ListItem>
				<asp:ListItem Value="false" Selected="True">False</asp:ListItem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td><asp:label id="Label6" runat="server" cssclass="csPlainText">QSP Agency Code</asp:label></td>
		<td><cc1:textboxsearch id="tbxQSPAgencyCode" runat="server" maxlength="20" parametername="QSPAgencyCode"
				contenttype="string" columns="20"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td><asp:label id="Label3" runat="server" cssclass="csPlainText">Address Line 1 *</asp:label></td>
		<td><cc1:textboxreq id="tbxAddress1" runat="server" errormsgrequired="The field Address Line 1 is mandatory."
				required="True" maxlength="35"></cc1:textboxreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label4" runat="server" cssclass="csPlainText">Address Line 2</asp:label></td>
		<td><cc1:textboxsearch id="tbxAddress2" runat="server" maxlength="35" parametername="Address2" contenttype="string"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td><asp:label id="Label8" runat="server" cssclass="csPlainText">City *</asp:label></td>
		<td><cc1:textboxreq id="tbxCity" runat="server" errormsgrequired="The field City is mandatory." required="True"
				maxlength="25"></cc1:textboxreq></td>
	</tr>
	<tr>
		<td style="HEIGHT: 3px"><asp:label id="Label10" runat="server" cssclass="csPlainText">State / Province *</asp:label></td>
		<td style="HEIGHT: 3px"><cc1:dropdownlistprovince id="ddlStateProvince" runat="server" parametername="StateProvince" contenttype="int"
				astextfirstrow="False"></cc1:dropdownlistprovince><asp:requiredfieldvalidator id="rfvStateProvince" runat="server" errormessage="Please select a State / Province."
				controltovalidate="ddlStateProvince">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td><asp:label id="Label33" runat="server" cssclass="csPlainText">Zip / Postal Code *</asp:label></td>
		<td><cc1:postalcode id="tbxZip" runat="server" errormsgrequired="The field Zip / Postal Code is mandatory."
				required="True" maxlength="10" parametername="Zip" contenttype="int" columns="7" typedate="All"
				errormsgregexp="The ZIP / Postal code is invalid. Ex: 11111 or H1H1H1 or 11111-1111"></cc1:postalcode></td>
	</tr>
	<tr>
		<td><asp:label id="Label12" runat="server" cssclass="csPlainText">Country *</asp:label></td>
		<td><cc1:dropdownlistreq id="ddlCountry" runat="server" errormsgrequired="Please select a Country." required="True"
				parametername="Country" contenttype="string"></cc1:dropdownlistreq></td>
	</tr>
	<tr>
		<td style="HEIGHT: 45px" colSpan="2"><asp:button id="btnValidateAddress" runat="server" CausesValidation="False" Text="Validate Address"></asp:button></td>
	</tr>
	<tr>
		<td style="HEIGHT: 22px" colSpan="2"><uc2:addresshygiene id="ctrlAddressHygiene" runat="server" visible="false"></uc2:addresshygiene><asp:label id="AddressHygieneStatusLabel" runat="server" ForeColor="Blue"></asp:label></td>
	</tr>
	<tr>
		<td style="PADDING-RIGHT: 50px; PADDING-LEFT: 50px; PADDING-BOTTOM: 50px; PADDING-TOP: 50px"
			colSpan="2"><asp:label id="lblInstructions" runat="server" cssclass="csPlainText" font-bold="True">Fulfillment House contacts:</asp:label><br>
			<br>
			<uc1:fulfillmenthousecontactsearchcontrol id="ctrlFulfillmentHouseContactSearchControl" runat="server"></uc1:fulfillmenthousecontactsearchcontrol><br>
			<asp:button id="btnCreateNew" runat="server" cssclass="boxlook" text="Create New Fulfillment House Contact" onclick="btnCreateNew_Click"></asp:button></td>
	</tr>
	<tr>
		<td align="right" colSpan="2"><asp:button id="btnSubmit" runat="server" cssclass="boxlook" text="Submit" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" runat="server" cssclass="boxlook" text="Cancel" causesvalidation="False" onclick="btnCancel_Click"></asp:button></td>
	</tr>
</table>
<uc1:fulfillmenthousecontactmaintenancecontrol id="ctrlFulfillmentHouseContactMaintenanceControl" runat="server" visible="false"></uc1:fulfillmenthousecontactmaintenancecontrol>

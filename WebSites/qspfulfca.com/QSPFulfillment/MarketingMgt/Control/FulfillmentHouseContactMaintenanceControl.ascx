<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FulfillmentHouseContactMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.FulfillmentHouseContactMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="FulfillmentHouseContactProductListControl" Src="FulfillmentHouseContactProductListControl.ascx" %>
<table id="tblMaintenance" style="BORDER-RIGHT: silver 1px solid; BORDER-TOP: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-BOTTOM: silver 1px solid"
	cellpadding="3" runat="server">
	<tr>
		<td><asp:label id="Label1" runat="server" cssclass="csPlainText">Fulfillment House Name</asp:label></td>
		<td><asp:label id="lblFulfillmentHouseName" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label13" runat="server" cssclass="csPlainText">First Name *</asp:label></td>
		<td><asp:textbox id="tbxFirstName" runat="server" columns="30" maxlength="50"></asp:textbox></td>
	</tr>
	<tr>
		<td><asp:label id="Label14" runat="server" cssclass="csPlainText">Last Name *</asp:label></td>
		<td><asp:textbox id="tbxLastName" runat="server" columns="30" maxlength="50"></asp:textbox></td>
	</tr>
	<tr>
		<td><asp:label id="Label15" runat="server" cssclass="csPlainText">Position Title</asp:label></td>
		<td><asp:textbox id="tbxPositionTitle" runat="server" columns="30" maxlength="50"></asp:textbox></td>
	</tr>
	<tr>
		<td><asp:label id="Label16" runat="server" cssclass="csPlainText">Email</asp:label></td>
		<td><cc1:email id="tbxEmail" runat="server" columns="30" maxlength="100"></cc1:email></td>
	</tr>
	<tr>
		<td><asp:label id="Label17" runat="server" cssclass="csPlainText">Work Phone Number</asp:label></td>
		<td><cc1:textboxreq id="tbxWorkPhone" runat="server" columns="20" maxlength="20"></cc1:textboxreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label18" runat="server" cssclass="csPlainText">Fax Number</asp:label></td>
		<td><cc1:textboxreq id="tbxFax" runat="server" columns="20" maxlength="20"></cc1:textboxreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label68" runat="server" cssclass="csPlainText"> Customer Service Contact First Name</asp:label></td>
		<td><asp:textbox id="tbxQSPContactFirstName" runat="server" columns="30" maxlength="50"></asp:textbox></td>
	</tr>
	<tr>
		<td><asp:label id="Label19" runat="server" cssclass="csPlainText"> Customer Service Contact Last Name</asp:label></td>
		<td><asp:textbox id="tbxQSPContactLastName" runat="server" columns="30" maxlength="50"></asp:textbox></td>
	</tr>
	<tr>
		<td><asp:label id="Label20" runat="server" cssclass="csPlainText">Customer Service Contact Email</asp:label></td>
		<td><cc1:email id="tbxQSPContactEmail" runat="server" columns="30" maxlength="100"></cc1:email></td>
	</tr>
	<tr>
		<td><asp:label id="Label21" runat="server" cssclass="csPlainText">Customer Service Contact Work Phone Number</asp:label></td>
		<td><cc1:textboxreq id="tbxQSPContactPhone" runat="server" columns="20" maxlength="50"></cc1:textboxreq></td>
	</tr>
	<tr id="tableRowMainContact" runat="server">
		<td><asp:label id="Label5" runat="server" cssclass="csPlainText">Main Contact</asp:label></td>
		<td><asp:radiobuttonlist id="rblMainContact" runat="server" cssclass="csPlainText" repeatdirection="Horizontal"
				autopostback="True" onselectedindexchanged="rblMainContact_SelectedIndexChanged">
				<asp:listitem value="True" selected="True">Yes</asp:listitem>
				<asp:listitem value="False">No</asp:listitem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr id="tableRowProductCode" runat="server">
		<td colspan="2"><asp:label id="Label6" runat="server" cssclass="csPlainText"> Products *</asp:label>
			<br>
			<uc1:fulfillmenthousecontactproductlistcontrol id="ctrlFulfillmentHouseContactProductListControl" runat="server"></uc1:fulfillmenthousecontactproductlistcontrol>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<div style="TEXT-ALIGN: right"><asp:button id="btnSubmit" runat="server" text="Submit" cssclass="boxlook" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" runat="server" text="Cancel" causesvalidation="False" cssclass="boxlook" onclick="btnCancel_Click"></asp:button></div>
		</td>
	</tr>
</table>

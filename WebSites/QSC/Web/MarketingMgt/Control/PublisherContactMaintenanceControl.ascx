<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PublisherContactMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.PublisherContactMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="PhoneListMaintenanceControl" Src="PhoneListMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PublisherContactProductListControl" Src="PublisherContactProductListControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<table id="tblMaintenance" style="BORDER-RIGHT: silver 1px solid; BORDER-TOP: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-BOTTOM: silver 1px solid"
	cellpadding="3" runat="server">
	<tr>
		<td><br>
			<asp:label id="Label1" runat="server" cssclass="csPlainText"> Publisher Name</asp:label></td>
		<td><br>
			<asp:label id="lblPublisherName" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label3" runat="server" cssclass="csPlainText">First Name *</asp:label></td>
		<td><cc1:textboxreq id="tbxFirstName" runat="server" columns="30" maxlength="30" required="True" errormsgrequired="The field First Name is mandatory."></cc1:textboxreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label4" runat="server" cssclass="csPlainText">Last Name *</asp:label></td>
		<td><cc1:textboxreq id="tbxLastName" runat="server" columns="30" maxlength="30" required="True" errormsgrequired="The field Last Name is mandatory."></cc1:textboxreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label2" runat="server" cssclass="csPlainText">Position Title</asp:label></td>
		<td><cc1:textboxsearch id="tbxPositionTitle" runat="server" columns="30" maxlength="50" contenttype="string"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td><asp:label id="Label8" runat="server" cssclass="csPlainText">Email Address</asp:label></td>
		<td><cc1:email id="tbxEmail" runat="server" columns="50" maxlength="50"></cc1:email></td>
	</tr>
	<tr>
		<td style="PADDING-RIGHT: 50px; PADDING-LEFT: 50px; PADDING-BOTTOM: 50px; PADDING-TOP: 50px"
			colspan="2"><asp:label id="lblInstructions" runat="server" cssclass="csPlainText" font-bold="True">Publisher contact phones</asp:label><br>
			<br>
			<uc1:phonelistmaintenancecontrol id="ctrlPhoneListMaintenanceControl" runat="server"></uc1:phonelistmaintenancecontrol></td>
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
			<uc1:publishercontactproductlistcontrol id="ctrlPublisherContactProductListControl" runat="server"></uc1:publishercontactproductlistcontrol>
		</td>
	</tr>
	<tr>
		<td align="right" colspan="2"><asp:button id="btnSubmit" runat="server" cssclass="boxlook" text="Submit" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" runat="server" cssclass="boxlook" text="Cancel" causesvalidation="False" onclick="btnCancel_Click"></asp:button></td>
	</tr>
</table>

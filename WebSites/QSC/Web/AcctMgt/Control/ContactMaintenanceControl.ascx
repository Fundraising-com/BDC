<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ContactMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.ContactMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="AddressMaintenanceControl" Src="AddressMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PhoneListMaintenanceControl" Src="PhoneListMaintenanceControl.ascx" %>
<div id="divContact" style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; PADDING-TOP: 4px"
	runat="server">
	<table cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td width="50%"><asp:label id="lblType" style="MARGIN-BOTTOM: 10px" runat="server" font-bold="True" cssclass="csPlainText"></asp:label>
				<table cellspacing="0" cellpadding="0" width="100%" border="0">
					<tr id="trPrimary" runat="server" visible="False">
						<td width="50%"><asp:label id="Label7" runat="server" cssclass="csPlainText">Is Primary?</asp:label></td>
						<td valign="middle" width="50%"><asp:checkbox id="chkPrimary" runat="server"></asp:checkbox></td>
					</tr>
					<tr id="trTitle" runat="server" visible="False">
						<td width="50%"><asp:label id="Label1" runat="server" cssclass="csPlainText">Title</asp:label></td>
						<td valign="middle" width="50%"><cc1:textboxreq id="tbxTitle" runat="server" columns="10" maxlength="10"></cc1:textboxreq></td>
					</tr>
					<tr>
						<td width="50%"><asp:label id="Label2" runat="server" cssclass="csPlainText">First Name</asp:label></td>
						<td valign="bottom" width="50%"><cc1:textboxreq id="tbxFirstName" runat="server" maxlength="20" errormsgrequired="The field First Name is mandatory."
								required="True"></cc1:textboxreq></td>
					</tr>
					<tr id="trMiddleInitial" runat="server" visible="False">
						<td width="50%"><asp:label id="Label6" runat="server" cssclass="csPlainText"> Middle Initial</asp:label></td>
						<td valign="bottom" width="50%"><cc1:textboxreq id="tbxMiddleInitial" runat="server" columns="10" maxlength="10"></cc1:textboxreq></td>
					</tr>
					<tr>
						<td width="50%"><asp:label id="Label3" runat="server" cssclass="csPlainText">Last Name</asp:label></td>
						<td valign="bottom" width="50%"><cc1:textboxreq id="tbxLastName" runat="server" maxlength="30" errormsgrequired="The field Last Name is mandatory."
								required="True"></cc1:textboxreq></td>
					</tr>
					<tr>
						<td width="50%"><asp:label id="Label4" runat="server" cssclass="csPlainText">Email</asp:label></td>
						<td valign="bottom" width="50%"><cc1:email id="tbxEmail" runat="server" maxlength="60"></cc1:email></td>
					</tr>
					<tr>
						<td width="50%"><asp:label id="Label5" runat="server" cssclass="csPlainText">Function</asp:label></td>
						<td valign="bottom" width="50%"><cc1:textboxreq id="tbxFunction" runat="server" maxlength="50"></cc1:textboxreq></td>
					</tr>
				</table>
			</td>
			<asp:placeholder id="plhLayout" runat="server"></asp:placeholder>
			<td width="50%">
				<div id="divAddressMaintenance" runat="server"><uc1:addressmaintenancecontrol id="ctrlAddressMaintenanceControl" runat="server" required="False"></uc1:addressmaintenancecontrol></div>
			</td>
		</tr>
	</table>
	<br>
	<asp:label id="lblPhoneTitle" runat="server" font-bold="True" cssclass="csPlainText">Phone Information</asp:label><uc1:phonelistmaintenancecontrol id="ctrlPhoneListMaintenanceControl" runat="server" required="True" showmainphonereadonly="False"></uc1:phonelistmaintenancecontrol>
	<div id="divRemove" runat="server"><br>
		<asp:button id="btnRemove" runat="server" cssclass="boxlook" text="Remove" causesvalidation="False" onclick="btnRemove_Click"></asp:button></div>
</div>

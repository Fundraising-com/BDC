<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PhoneMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.PhoneMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<tr>
	<td style="WIDTH: 25%" valign="middle"><cc1:dropdownlistreq id="ddlType" runat="server" initialvalue="0" initialtext="Please Select..." required="True"
			errormsgrequired="The field Phone Type is mandatory."></cc1:dropdownlistreq></td>
	<td style="WIDTH: 25%" valign="bottom"><cc1:textboxreq id="tbxPhoneNumber" runat="server" required="True" errormsgrequired="The field Phone Number is mandatory."
			maxlength="50"></cc1:textboxreq></td>
	<td style="WIDTH: 25%" valign="bottom"><asp:textbox id="tbxBestTimeToCall" runat="server"></asp:textbox></td>
	<td style="WIDTH: 25%" valign="bottom"><asp:button id="btnRemove" runat="server" cssclass="boxlook" text="Remove" causesvalidation="False" onclick="btnRemove_Click"></asp:button></td>
</tr>

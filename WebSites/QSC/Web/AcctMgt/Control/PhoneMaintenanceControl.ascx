<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PhoneMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.PhoneMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<tr>
	<td valign="middle"><cc1:dropdownlistreq id="ddlType" runat="server" initialvalue="0" initialtext="Please Select..." required="True"
			errormsgrequired="The field Phone Type is mandatory."></cc1:dropdownlistreq><asp:label id="lblType" runat="server" cssclass="csPlainText" visible="False"></asp:label></td>
	<td valign="bottom"><cc1:phone id="tbxPhoneNumber" runat="server" required="True" errormsgrequired="The field Phone Number is mandatory."
			maxlength="50" columns="12" errormsgregexp="The Phone Number is invalid. Ex: 123-456-7890"></cc1:phone></td>
	<td valign="bottom"><asp:textbox id="tbxBestTimeToCall" runat="server" columns="15" maxlength="2000"></asp:textbox></td>
</tr>
<tr id="trRemove" runat="server">
	<td colspan="3" valign="bottom" style="TEXT-ALIGN: right">
		<asp:button id="btnRemove" runat="server" cssclass="boxlook" text="Delete" causesvalidation="False" onclick="btnRemove_Click"></asp:button>
	</td>
</tr>

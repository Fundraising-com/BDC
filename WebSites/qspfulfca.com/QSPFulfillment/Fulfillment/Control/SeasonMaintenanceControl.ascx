<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SeasonMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.Fulfillment.Control.SeasonMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<div id="divSeason" style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; PADDING-TOP: 4px"
	runat="server">
	<table cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td width="50%">
				<asp:label id="lblSeasonName" runat="server" cssclass="csPlainText">Season Name</asp:label>
			</td>
			<td valign="bottom" width="50%">
				<cc1:textboxreq id="tbxSeasonName" runat="server" maxlength="50" errormsgrequired="The field Season Name is mandatory."
					required="True"></cc1:textboxreq>
			</td>
		</tr>
		<tr>
			<td width="50%">
				<asp:label id="lblFiscalYear" runat="server" cssclass="csPlainText">Fiscal Year</asp:label>
			</td>
			<td valign="bottom" width="50%">
				<cc1:textboxinteger id="tbxFiscalYear" runat="server" maxlength="4"></cc1:textboxinteger>
			</td>
		</tr>
		<tr>
			<td>
				<asp:label id="lblSeason" cssclass="csPlainText" runat="server">Season</asp:label>
			</td>
			<td>
				<cc1:dropdownlistreq id="ddlSeason" runat="server" errormsgrequired="Please select a season." required="True"></cc1:dropdownlistreq>
			</td>
		</tr>
		<tr>
			<td width="50%">
				<asp:label id="lblDefaultConversionRate" runat="server" cssclass="csPlainText">USD Conversion Rate</asp:label>
			</td>
			<td valign="bottom" width="50%">
				<cc1:textboxfloat id="tbxDefaultConversionRate" runat="server" maxlength="50"></cc1:textboxfloat>
			</td>
		</tr>
		<tr>
			<td colspan=2>
				<div style="TEXT-ALIGN: center"><br><asp:button id="btnSubmitBottom" runat="server" cssclass="boxlook" text="Save" onclick="btnSubmit_Click"></asp:button>&nbsp;<asp:button id="btnCloseBottom" runat="server" cssclass="boxlook" text="Cancel" causesvalidation="False" onclick="btnClose_Click"></asp:button></div>
			</td>
		</tr>
	</table>
</div>

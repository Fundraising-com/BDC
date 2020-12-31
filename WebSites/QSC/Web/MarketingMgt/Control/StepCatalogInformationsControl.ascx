<%@ Control Language="c#" AutoEventWireup="True" Codebehind="StepCatalogInformationsControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.StepCatalogInformationsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<div class="MaintenanceContainer" style="WIDTH: 600px">
	<table id="Table3" class="Maintenance" cellpadding="3">
		<tr>
			<td><br>
				<asp:label id="Label1" runat="server" cssclass="csPlainText">Catalog Code *</asp:label></td>
			<td><br>
				<cc1:textboxreq id="tbxCatalogCode" runat="server" errormsgrequired="The field Catalog Code is mandatory."
					required="True" maxlength="50" columns="50"></cc1:textboxreq>
			</td>
		</tr>
		<tr>
			<td><asp:label id="Label2" runat="server" cssclass="csPlainText">Catalog Name *</asp:label></td>
			<td>
				<cc1:textboxreq id="tbxCatalogName" runat="server" errormsgrequired="The field Catalog Name is mandatory."
					required="True" maxlength="50" columns="50"></cc1:textboxreq>
			</td>
		</tr>
		<tr>
			<td>
				<asp:label id="Label8" runat="server" cssclass="csPlainText">Type *</asp:label></td>
			<td>
				<cc1:dropdownlistreq id="ddlType" runat="server" errormsgrequired="Please select a Type." initialvalue="0"
					required="True"></cc1:dropdownlistreq>
			</td>
		</tr>
		<tr>
			<td><asp:label id="Label3" runat="server" cssclass="csPlainText">Language *</asp:label></td>
			<td>
				<cc1:dropdownlistreq id="ddlLanguage" runat="server" errormsgrequired="Please select a Language." required="True"></cc1:dropdownlistreq>
			</td>
		</tr>
		<tr>
			<td>
				<asp:label id="Label14" runat="server" cssclass="csPlainText">Year *</asp:label></td>
			<td>
				<cc1:dropdownlistreq id="ddlYear" runat="server" errormsgrequired="Please select a Year." initialvalue="0"
					required="True"></cc1:dropdownlistreq>
			</td>
		</tr>
		<tr>
			<td><asp:label id="Label9" runat="server" cssclass="csPlainText">Season *</asp:label></td>
			<td>
				<cc1:dropdownlistreq id="ddlSeason" runat="server" errormsgrequired="Please select a Season." required="True"></cc1:dropdownlistreq>
			</td>
		</tr>
		<tr>
			<td><asp:label id="Label10" runat="server" cssclass="csPlainText">Status *</asp:label></td>
			<td><cc1:dropdownlistreq id="ddlStatus" runat="server" contenttype="int" parametername="CatalogStatus" errormsgrequired="Please select a Status."
					initialvalue="0" required="True"></cc1:dropdownlistreq></td>
		</tr>
		<tr>
			<td><asp:label id="Label33" runat="server" cssclass="csPlainText">Is Replacement</asp:label></td>
			<td>
				<cc1:dropdownlistsearch id="ddlIsReplacement" runat="server"></cc1:dropdownlistsearch>
			</td>
		</tr>
		<tr>
			<td colspan="2"></td>
		</tr>
		<tr>
			<td align="center" colspan="2" style="TEXT-ALIGN: right"><asp:button id="btnSubmit" runat="server" text="Save" cssclass="boxlook" onclick="btnSubmit_Click"></asp:button>
				<asp:button id="btnSkip" runat="server" text="Skip" cssclass="boxlook" causesvalidation="False" onclick="btnSkip_Click"></asp:button></td>
		</tr>
	</table>
</div>

<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CatalogSectionMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.CatalogSectionMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<table id="Table3" style="BORDER-RIGHT: silver 1px solid; BORDER-TOP: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-BOTTOM: silver 1px solid"
	cellpadding="3">
	<tr>
		<td colspan="2"></td>
	</tr>
	<tr>
		<td style="WIDTH: 187px"><asp:label id="Label2" runat="server" cssclass="csPlainText">Section Name *</asp:label></td>
		<td style="WIDTH: 187px"><cc1:textboxreq id="tbxSectionName" runat="server" columns="40" maxlength="50" required="True" errormsgrequired="The field Section Name is mandatory."></cc1:textboxreq></td>
	</tr>
	<tr>
		<td style="WIDTH: 187px"><asp:label id="Label1" runat="server" cssclass="csPlainText">Section Type *</asp:label></td>
		<td style="WIDTH: 187px">
			<cc1:dropdownlistinteger id="ddlSectionType" runat="server" contenttype="int" parametername="SectionType"
				errormsgrequired="Please select a Section Type." initialvalue="0" required="True" autopostback="True"></cc1:dropdownlistinteger>
		</td>
	</tr>
	<tr id="trFSProgram" runat="server" visible="False">
		<td style="WIDTH: 187px">
			<asp:label id="Label3" cssclass="csPlainText" runat="server">Program *</asp:label></td>
		<td style="WIDTH: 187px">
			<cc1:dropdownlistinteger id="ddlFSProgram" runat="server" initialtext="Please select..." initialvalue="0"
				required="True" errormsgrequired="The field Program is required."></cc1:dropdownlistinteger>
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 34px" colspan="2"></td>
	</tr>
	<tr>
		<td style="TEXT-ALIGN: right" align="center" colspan="2"><asp:button id="btnSubmit" runat="server" text="Save" cssclass="boxlook" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" runat="server" text="Cancel" causesvalidation="False" cssclass="boxlook" onclick="btnCancel_Click"></asp:button></td>
	</tr>
</table>

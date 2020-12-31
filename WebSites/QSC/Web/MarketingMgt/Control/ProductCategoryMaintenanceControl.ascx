<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ProductCategoryMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.ProductCategoryMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<br>
<table id="Table3" style="BORDER-RIGHT: silver 1px solid; BORDER-TOP: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-BOTTOM: silver 1px solid"
	cellpadding="3">
	<tr>
		<td><asp:label id="Label3" runat="server" cssclass="csPlainText">Description *</asp:label></td>
		<td>
			<cc1:textboxreq id="tbxDescription" runat="server" columns="64" errormsgrequired="The field Description is mandatory."
				required="True" maxlength="64"></cc1:textboxreq>
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 34px" colspan="2"></td>
	</tr>
	<tr>
		<td align="right" colspan="2"><asp:button id="btnSubmit" runat="server" text="Submit" cssclass="boxlook" onclick="btnSubmit_Click"></asp:button>
			<asp:button id="btnCancel" runat="server" text="Cancel" causesvalidation="False" cssclass="boxlook" onclick="btnCancel_Click"></asp:button></td>
	</tr>
</table>

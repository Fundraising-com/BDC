<%@ Control Language="c#" AutoEventWireup="True" Codebehind="StepIncludeProductsControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.StepIncludeProductsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<asp:label id="Label1" runat="server" cssclass="csPlainText" font-bold="True">Products included in the catalog:</asp:label>
<br>
<asp:placeholder id="plhProductContractSearchControl" runat="server"></asp:placeholder>
<br>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr>
		<td>
			<input id="btnCreateProduct" runat="server" type="button" value="Create Product" class="boxlook">
			<input id="btnImport" runat="server" type="button" value="Import Products" class="boxlook">
		</td>
		<td align="right">
			<asp:button id="btnSubmit" runat="server" text="Done" cssclass="boxlook" onclick="btnSubmit_Click"></asp:button>
		</td>
	</tr>
</table>
<input type="hidden" id="hidDataBind" runat="server" name="hidDataBind" value="false">
<input type="hidden" id="hidProductInstance" runat="server" name="hidProductInstance" value="0">
<input type="hidden" id="hidProductType" runat="server" name="hidProductType" value="0">

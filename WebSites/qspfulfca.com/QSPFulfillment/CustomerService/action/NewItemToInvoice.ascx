<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NewItemToInvoice.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.NewItemToInvoice" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ControlerMagazineTerm" Src="../ControlerMagazineTerm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerAddress" Src="../ControlerAddress.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<uc1:controlermagazineterm id="ctrlControlerMagazineTerm" showsearch="False" runat="server"></uc1:controlermagazineterm><br>
<div id="divStep2" runat="server"><uc1:controleraddress id="ctrlControlerAddress" runat="server"></uc1:controleraddress><br>
	<br>
	<table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
		<tr>
			<td style="WIDTH: 120px"><asp:label id="Label1" runat="server" cssclass="CSPlainText">Product Code</asp:label></td>
			<td><asp:label id="lblProductCode" runat="server" cssclass="CSPlainText"></asp:label></td>
		<tr>
			<td style="WIDTH: 120px"><asp:label id="Label2" runat="server" cssclass="CSPlainText">Product Name</asp:label></td>
			<td><asp:label id="lblProductName" runat="server" cssclass="CSPlainText"></asp:label></td>
		</tr>
		<tr>
			<td style="WIDTH: 120px"><asp:label id="Label3" runat="server" cssclass="CSPlainText">Quantity</asp:label></td>
			<td><asp:label id="lblQuantity" runat="server" cssclass="CSPlainText"></asp:label></td>
		</tr>
		<tr>
			<td style="WIDTH: 120px"></td>
			<td></td>
		</tr>
		<tr>
			<td style="WIDTH: 120px"><asp:label id="Label5" runat="server" cssclass="CSPlainText">Price</asp:label></td>
			<td>
				<asp:textbox id="tbxPrice" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td style="WIDTH: 120px"><asp:label id="Label6" runat="server" cssclass="CSPlainText">Catalog Price</asp:label></td>
			<td><asp:label id="lblCatalogPrice" runat="server" cssclass="CSPlainText"></asp:label><asp:customvalidator id="Validation" runat="server"></asp:customvalidator></td>
		</tr>
	</table>
</div>
<br>
<br>
<div style="TEXT-ALIGN: right"><asp:button id="btnBack" runat="server" text="Back" onclick="btnBack_Click"></asp:button></div>

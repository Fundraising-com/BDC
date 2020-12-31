<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerInvoice.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerInvoice" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<cc2:DataGridObject id="dtgMain" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None"
	GridLines="None">
	<ItemStyle CssClass="CSTableItems"></ItemStyle>
	<HeaderStyle Font-Bold="True" CssClass="CSTableItems"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="ID">
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INVOICE_ID") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="INVOICE_DATE" HeaderText="Date"></asp:BoundColumn>
	</Columns>
</cc2:DataGridObject>
<asp:Label id="lblMessage" runat="server"></asp:Label>

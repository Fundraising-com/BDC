<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ResultProductCreditCard.ascx.cs" Inherits="QSPFulfillment.CustomerService.ResultProductCreditCard" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<cc2:DataGridObject id="dtgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True" BorderColor="#CCCCCC"
	BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" width="100%">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	<ItemStyle ForeColor="#000066" CssClass="CSSearchResult"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CSSearchResult" BackColor="#006699"></HeaderStyle>
	<FooterStyle ForeColor="#000066" CssClass="CSSearchResult" BackColor="White"></FooterStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="COH Instance">
			<ItemTemplate>
				<asp:Label id="lblCustomerOrderHeaderInstance" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.customerorderheaderinstance") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Trans ID">
			<ItemTemplate>
				<asp:Label id="lblTransID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TransID") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="ProductName" HeaderText="ProductName"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="ProductCode" HeaderText="TitleCode"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="ProductType" HeaderText="ProductType"></asp:BoundColumn>
		<asp:BoundColumn DataField="Quantity" HeaderText="Quantity"></asp:BoundColumn>
		<asp:BoundColumn DataField="Price" HeaderText="Price" DataFormatString="{0:c}"></asp:BoundColumn>
		<asp:BoundColumn DataField="RecipientName" HeaderText="Recipient Name"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="Status" HeaderText="Status"></asp:BoundColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" CssClass="CSPager"
		Mode="NumericPages"></PagerStyle>
</cc2:DataGridObject>
<asp:Label runat="server" id="lblMessage" visible="false"></asp:Label>

<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ResultProduct.ascx.cs" Inherits="QSPFulfillment.CustomerService.ResultProduct" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<cc2:DataGridObject id="dtgMain" runat="server" AutoGenerateColumns="False"
										AllowPaging="True" 
										BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
		BackColor="White" CellPadding="3">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<ItemStyle ForeColor="#000066" cssClass="CSSearchResult"></ItemStyle>
		<HeaderStyle Font-Bold="True"  cssClass="CSSearchResult" ForeColor="White" BackColor="#006699"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"  cssClass="CSSearchResult"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White"  cssClass="CSPager" Mode="NumericPages"></PagerStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="Order ID">
			<ItemTemplate>
				<asp:Label id="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderId") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn HeaderText="Catalog Code" DataField="CatalogCode" ></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Description" DataField="ItemDescription"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Quantity" DataField="QuantityOrdered"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Price Entered" DataField="PriceEntered"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Catalog Price" DataField="CatalogPrice"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Override Code" DataField="OverrideCode"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Status" DataField="OrderItemStatus"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Ship Date" DataField="ShipDate" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Purchassed By" DataField="PurchasedBy"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Student First Name" DataField="StudentFirstName"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Student Last Name" DataField="StudentLastName"></asp:BoundColumn>
		<asp:ButtonColumn visible=false Text="Select" CommandName="Select"></asp:ButtonColumn>
	</Columns>
	
</cc2:DataGridObject>


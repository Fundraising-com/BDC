<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ResultShipment.ascx.cs" Inherits="QSPFulfillment.CustomerService.ResultShipment" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<P>
	<cc2:DataGridObject id="dtgMain" runat="server" AutoGenerateColumns="False"
										AllowPaging="True" 
										BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" width="100%" 
		BackColor="White" CellPadding="3">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<ItemStyle ForeColor="#000066" cssClass="CSSearchResult"></ItemStyle>
		<HeaderStyle Font-Bold="True"  cssClass="CSSearchResult" ForeColor="White" BackColor="#006699"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"  cssClass="CSSearchResult"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" BackColor="White"  cssClass="CSPager" Mode="NumericPages"></PagerStyle>
		<Columns>
		<asp:TemplateColumn	visible=false HeaderText="Order ID">
			<ItemTemplate>
				<asp:Label id="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderID") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn	visible=true HeaderText="Shipment ID">
			<ItemTemplate>
				<asp:Label id="lblShipmentID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ShipmentID") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
			<asp:BoundColumn HeaderText="Order Date" DataField="orderdate" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
			<asp:BoundColumn HeaderText="Order Status" DataField="orderstatus"></asp:BoundColumn>
			<asp:BoundColumn HeaderText="Order Type" DataField="ordertype"></asp:BoundColumn>
			<asp:BoundColumn HeaderText="Qualifier Name" DataField="orderqualifier"></asp:BoundColumn>
			<asp:BoundColumn HeaderText="Campaign ID" DataField="campaignid"></asp:BoundColumn>
			<asp:BoundColumn HeaderText="Group Name" DataField="groupname"></asp:BoundColumn>
			

			<asp:BoundColumn HeaderText="Shipment Date" DataField="shipmentdate" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
			<asp:BoundColumn HeaderText="Excepted Delivery Date" DataField="expecteddeliverydate" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
			<asp:ButtonColumn Text="Select" CommandName="Select"></asp:ButtonColumn>
		</Columns>
		
	</cc2:DataGridObject></P><asp:Label runat=server id="lblMessage" visible="false"></asp:Label>

<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ShippingOrderDetailList" Codebehind="ShippingOrderDetailList.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td>
			<asp:datagrid id=dtgOrderDetail runat="server" Font-Names="Verdana" Font-Size="Small" ShowFooter="True" AllowSorting="True" DataKeyField="order_detail_id" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#999999" DataSource="<%# dTblOrderDetail %>">
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<EditItemStyle Wrap="False"></EditItemStyle>
				<AlternatingItemStyle Font-Size="X-Small" Wrap="False" BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle Font-Size="X-Small" Wrap="False" ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Size="X-Small" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn SortExpression="" HeaderText="Item&nbsp;#">
						<HeaderStyle Width="10px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblItemNo" Width="10px" Text='<%# (Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex")) + 1) %>' runat="server" CssClass="StandardLabel">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="product_name" HeaderText="Product">
						<HeaderStyle Width="200px"></HeaderStyle>
						<ItemTemplate>
							<asp:DropDownList id=ddlProduct Enabled=False Width="200px" DataSource="<%# tblProduct %>" runat="server" SelectedIndex='<%# getSelectedIndex(tblProduct, Convert.ToString(DataBinder.Eval(Container, "DataItem.product_id"))) %>' DataValueField="product_id" DataTextField="product_name">
							</asp:DropDownList>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="quantity" HeaderText="Quantity">
						<HeaderStyle Width="60px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit/Case">
						<HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblUnit" runat="server" Text="12"></asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="shipment_group_id" HeaderText="Shipping Address">
						<HeaderStyle Width="200px"></HeaderStyle>
						<ItemTemplate>
							<asp:DropDownList id="ddlShipmentGroup" Width="200px" DataSource="<%# dTblShipmentGroup %>" runat="server" SelectedIndex='<%# getSelectedIndex(dTblShipmentGroup, Convert.ToString(DataBinder.Eval(Container, "DataItem.shipment_group_id"))) %>' DataValueField="shipment_group_id" DataTextField="TitleAddress">
							</asp:DropDownList>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White" BackColor="#999999"
					Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
	<tr>
		<td align="center"><br>
		</td>
	</tr>	
</table>

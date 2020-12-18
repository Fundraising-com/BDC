<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderSupplyListInfo" Codebehind="OrderSupplyListInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td>
			<table id="Table2" cellSpacing="0" cellPadding="3" width="100%" align="center" border="0">
				<tr>
					<td><asp:label id="lblFormTitle" CssClass="TitleLabel" runat="server"></asp:label>
					</td>
				</tr>
			</table>
			<asp:datagrid id=dtgOrderDetail runat="server" Font-Names="Verdana" Font-Size="8pt" ShowFooter="True" AllowSorting="True" DataKeyField="order_detail_id" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#999999" DataSource="<%# dvOrderDetail %>">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="No">
						<HeaderStyle Width="5px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblItemNo" Width="10px" Text='<%# (Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex")) + 1) %>' runat="server" CssClass="StandardLabel">
							</asp:Label>
						</ItemTemplate>
						<FooterTemplate>
							<asp:Label id="Label3" Text="Total:" CssClass="StandardLabel" runat="server"></asp:Label>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_code" HeaderText="Item&#160;#">
						<HeaderStyle Width="15px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label1" Width="70px" Text='<%# DataBinder.Eval(Container, "DataItem.catalog_item_code") %>' runat="server" >
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_name" HeaderText="Product">
						<HeaderStyle Width="400px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="CatalogItemDetailName"  Text='<%# DataBinder.Eval(Container, "DataItem.catalog_item_name") %>' runat="server" >
							</asp:Label>
						</ItemTemplate>
						
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="quantity" HeaderText="Quantity">
						<HeaderStyle Width="60px"></HeaderStyle>
						<ItemStyle HorizontalAlign=Right></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblQuantity" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>' runat="server" Width="60px">
							</asp:Label>&nbsp;
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
						<FooterTemplate>
							<asp:Label id="lblTotalQuantity" Text="0" CssClass="StandardLabel" runat="server"></asp:Label>&nbsp;
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
		</td>
	</tr>
	<tr>
		<td>
		</td>
	</tr>
</table>

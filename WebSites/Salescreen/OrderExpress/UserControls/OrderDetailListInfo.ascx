<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderDetailListInfo" Codebehind="OrderDetailListInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
    <tr>
		<td align="left">			
	    </td>
	</tr>
	<tr align="left">
        <td class=SectionPageTitleInfo>
			<asp:label id="lblSectionTitle" runat="server" Font-Size=14px CssClass="StandardSectionLabel">
			    Section 1:&nbsp;
		    </asp:label>
		</td>				
    </tr>
	<tr>
		<td><asp:datagrid id=dtgOrderDetail runat="server" ShowFooter="True" AllowSorting="True" DataKeyField="order_detail_id" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" CssClass=GridStyle BorderColor="#999999" DataSource="<%# dvOrderDetail %>">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn SortExpression="" HeaderText="No">
						<HeaderStyle Width="5px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblItemNo" Width="5px" Text='<%# (Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex")) + 1) %>' runat="server"  CssClass="StandardLabel">
							</asp:Label>
						</ItemTemplate>
						<FooterTemplate>
							<asp:Label id="Label3" Text="Total:" CssClass="StandardLabel" runat="server"></asp:Label>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_code" HeaderText="Item&nbsp;#">
						<HeaderStyle Width="15px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblProductCode" Width="50px" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.catalog_item_code") %>' runat="server" >
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_name" HeaderText="Product">
						<ItemTemplate>
							<asp:Label id="Label2" Width="250px" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.catalog_item_name") %>' runat="server" >
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit/<br>Case">
						<HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblUnit" runat="server" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.nb_units") %>'>
							</asp:Label>&nbsp;
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Pro<br>Code">
						<HeaderStyle></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblAdjustmentQuantity" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.adjustment_quantity").ToString() != "" ? (DataBinder.Eval(Container, "DataItem.adjustment_quantity").ToString() != "0" ? (Convert.ToInt32(DataBinder.Eval(Container, "DataItem.adjustment_quantity")) * -1).ToString() : "" ) : "" %>' runat="server">
							</asp:Label>&nbsp;
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="quantity" HeaderText="#<br>Cases">
						<HeaderStyle></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblQuantity" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.quantity").ToString() == "0" ? "" : DataBinder.Eval(Container, "DataItem.quantity").ToString() %>' runat="server" >
							</asp:Label>&nbsp;
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
						<FooterTemplate>
							<asp:Label id="lblTotalQuantity" Text="0" CssClass="StandardLabel" runat="server"></asp:Label>&nbsp;
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="price" HeaderText="Case<br>Price">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblPrice" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.price", "{0:C}") %>' runat="server" Width="60px">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total" >
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblAmount" runat="server" Font-Size="8pt" Text='<%# (DataBinder.Eval(Container, "DataItem.adjustment_quantity").ToString() == "0" && DataBinder.Eval(Container, "DataItem.quantity").ToString() == "0" ) ? "" : DataBinder.Eval(Container, "DataItem.amount", "{0:C}") %>'>
							</asp:Label>&nbsp;
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
						<FooterTemplate>
							<asp:Label id="lblTotalAmount" Width="100px" Text="0.00" CssClass="StandardLabel" runat="server"></asp:Label>&nbsp;
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
		</td>
	</tr>
</table>

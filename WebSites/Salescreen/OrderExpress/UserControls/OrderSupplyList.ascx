<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderSupplyList" Codebehind="OrderSupplyList.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr id="trValSum" runat="server" visible="false">
		<td>
			<asp:validationsummary id="ValSum" runat="server" CssClass="LabelError" HeaderText="Correct the following error to proceed."></asp:validationsummary>
		</td>
	</tr>
	<tr>
		<td>
			<table id="Table2" cellSpacing="0" cellPadding="3" width="100%" align="center" border="0">
				<tr>
					<td>
					</td>
				</tr>
			</table>
			<asp:datagrid id=dtgOrderDetail runat="server" ShowFooter="True" AllowSorting="True" DataKeyField="order_detail_id" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" CssClass=Gridstyle  BorderColor="#999999" DataSource="<%# dvOrderDetail %>">
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
							<asp:Label id="lblItemNo" Width="10px" Text='<%# (Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex")) + 1) %>' runat="server" CssClass="StandardLabel">
							</asp:Label>
						</ItemTemplate>
						
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_code" HeaderText="Item&nbsp;#">
						<HeaderStyle Width="70px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblCatalogItemCode"  Text='<%# DataBinder.Eval(Container, "DataItem.catalog_item_code").ToString().Trim().Replace(" ", "&nbsp;") %>' runat="server" >
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_name" HeaderText="Product">
						<HeaderStyle Width="400px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblCatalogItemDetail" Text='<%# DataBinder.Eval(Container, "DataItem.catalog_item_name") %>' runat="server" >
							</asp:Label>
						</ItemTemplate>
						<FooterTemplate>
							<asp:Label id="Label3" Text="Total:" CssClass="StandardLabel" runat="server"></asp:Label>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="quantity" HeaderText="Quantity">
						<HeaderStyle Width="60px"></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtQuantity" CssClass=StandardTextBox Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>' runat="server" Width="60px">
										</asp:TextBox>
									</td>
									<td>
										<asp:CompareValidator id="compVal_Quantity" CssClass="LabelError" runat="server" ErrorMessage="The Quantity is invalid"
											ControlToValidate="txtQuantity" Type="Integer" Operator="DataTypeCheck">*</asp:CompareValidator>
									</td>
								</tr>
							</table>
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
</table>

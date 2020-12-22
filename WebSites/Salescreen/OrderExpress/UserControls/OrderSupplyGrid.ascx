<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderSupplyGrid" Codebehind="OrderSupplyGrid.ascx.cs" %>

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
					<td><asp:imagebutton id="imgBtnAddNew" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/BtnAdd.gif"
							ToolTip="Click here to add a new item in your order!" AlternateText="Add New"></asp:imagebutton></td>
				</tr>
			</table>
			<asp:datagrid id=dtgOrderDetail runat="server" Font-Names="Verdana" Font-Size="10pt" ShowFooter="True" AllowSorting="True" DataKeyField="order_detail_id" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#999999" DataSource="<%# dTblOrderDetail %>">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:ImageButton id="imgBtnDelete" runat="server" ImageUrl="~/images/BtnDelete.gif" CommandName="Delete"
								CausesValidation="False"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="" HeaderText="No">
						<HeaderStyle Width="5px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblItemNo" Width="10px" Text='<%# (Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex")) + 1) %>' runat="server" CssClass="StandardLabel">
							</asp:Label>
						</ItemTemplate>
						<FooterTemplate>
							<asp:Label id="Label3" Text="Total:" CssClass="StandardLabel" runat="server"></asp:Label>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_code" HeaderText="Item&nbsp;#">
						<HeaderStyle Width="15px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblCatalogItemCode" Width="70px" Text='<%# DataBinder.Eval(Container, "DataItem.catalog_item_code") %>' runat="server" >
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_name" HeaderText="Product">
						<HeaderStyle Width="300px"></HeaderStyle>
						<ItemTemplate>
							<asp:DropDownList id=ddlCatalogItemDetail DataSource="<%# tblCatalogItemDetail %>" runat="server" SelectedIndex='<%# getSelectedIndex(tblCatalogItemDetail, Convert.ToString(DataBinder.Eval(Container, "DataItem.catalog_item_detail_id"))) %>' DataValueField="catalog_item_detail_id" DataTextField="catalog_item_name">
							</asp:DropDownList>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="quantity" HeaderText="Quantity">
						<HeaderStyle Width="60px"></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtQuantity" CssClass="GridRefresh" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>' runat="server" Width="60px">
										</asp:TextBox>
									</td>
									<td>
										<asp:RequiredFieldValidator id="ReqFldVal_Quantity" CssClass="LabelError" runat="server" ErrorMessage="The Quantity is required"
											ControlToValidate="txtQuantity">*</asp:RequiredFieldValidator>
									</td>
									<td>
										<asp:CompareValidator id="compVal_Quantity" CssClass="LabelError" runat="server" ErrorMessage="The Quantity is invalid"
											ControlToValidate="txtQuantity" Type="Integer" Operator="DataTypeCheck">*</asp:CompareValidator>
									</td>
									<td>
										<asp:CompareValidator id="compVal_QuantityZero" CssClass="LabelError" runat="server" ErrorMessage="The Quantity must be greather than zero"
											ControlToValidate="txtQuantity" Type="Integer" Operator="GreaterThanEqual" ValueToCompare="0">*</asp:CompareValidator>
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

<script>
function updateCode(ddl,codeID)
{
	document.getElementById(codeID).innerHTML = arrSupplyCode[ddl.selectedIndex];
}
</script>

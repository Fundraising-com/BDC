<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderDetailList" Codebehind="OrderDetailList.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
    <tr id="trBusinessMessage" runat="server" visible="true">
		<td align="left"> <!--Section Body -->
		    <div class="BizRuleLabel">
                <!-- Place business rule text here -->
            </div>
            <br />
			<table id="Table533" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="lblBusinessMessage" runat="server" CssClass="BizRuleLabel"></asp:label>
						
					</td>
					<td vAlign="top"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr id="trValSum" runat="server" visible="false">
		<td>
			<asp:validationsummary id="ValSum" runat="server" CssClass="LabelError" HeaderText="<br>Correct the following error to proceed."></asp:validationsummary>
			<asp:CustomValidator id="CustVal_MinQty" runat="server" CssClass="Label Error" ErrorMessage="The Minimum for an order is [MinTotalQuantity] cases.">*</asp:CustomValidator>
		</td>
	</tr>
	<tr>
        <td class=SectionPageTitleInfo>
			<asp:label id="lblSectionTitle" runat="server" Font-Size=14px CssClass="StandardSectionLabel">
			    Section 1:&nbsp;
		    </asp:label>
		</td>				
    </tr>    	
	<tr>
		<td>
			<asp:datagrid id=dtgOrderDetail runat="server" ShowFooter="True" AllowSorting="True" DataKeyField="order_detail_id" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" CssClass=GridStyle BorderColor="#999999" DataSource="<%# dvOrderDetail %>">
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
					<asp:TemplateColumn SortExpression="" HeaderText="profit_Rate"  Visible=false>
						<ItemTemplate>
							<asp:Label id="lblProfitRate" Text='<%#DataBinder.Eval(Container, "DataItem.catalog_item_detail_profit_rate")%>' runat="server"></asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_code" HeaderText="Item&nbsp;#">
						<HeaderStyle Width="15px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblCatalogItemCode" Width="50px" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.catalog_item_code").ToString().Trim().Replace(" ", "&nbsp;") %>' runat="server" >
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_name" HeaderText="Product">
						<HeaderStyle Width="200px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblCatalogItemDetailName" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.catalog_item_name") %>' runat="server" >
							</asp:Label>
						</ItemTemplate>
						<FooterTemplate>
							<asp:Label id="Label2" Text="Total:" CssClass="StandardLabel" runat="server"></asp:Label>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit/<br>Case">
						<HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblUnit" runat="server" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.nb_units") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Pro<br>Code">
						<HeaderStyle></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtAdjustmentQuantity"  CssClass=StandardTextBox Text='<%# DataBinder.Eval(Container, "DataItem.adjustment_quantity").ToString() != "" ? (DataBinder.Eval(Container, "DataItem.adjustment_quantity").ToString() != "0" ? (Convert.ToInt32(DataBinder.Eval(Container, "DataItem.adjustment_quantity")) * -1).ToString() : "" ) : "" %>' runat="server" Width="40px">
										</asp:TextBox>
										<asp:Label id="lblAdjustmentQuantity" Visible=false CssClass="DescLabel" Text="N/A" runat="server" >
							            </asp:Label>
									</td>
									<td>
										<asp:CompareValidator id="compVal_AdjQuantity" CssClass="LabelError" runat="server" ErrorMessage="The Pro Code is invalid"
											ControlToValidate="txtAdjustmentQuantity" Type="Integer" Operator="DataTypeCheck">*</asp:CompareValidator>
									</td>
									<td>
										<asp:CompareValidator id="compVal_AdjQuantityZero" CssClass="LabelError" runat="server" ErrorMessage="The Pro Code cannot be greater than number of cases"
											ControlToValidate="txtAdjustmentQuantity" Type="Integer" Operator="GreaterThanEqual" ValueToCompare="0">*</asp:CompareValidator>
									</td>
									<td>
										<asp:CompareValidator id="compVal_AdjQuantity_Quantity" CssClass="LabelError" runat="server" ErrorMessage="Pro Code must be less or equal to the number of cases"
											ControlToValidate="txtAdjustmentQuantity" Type="Integer" Enabled="false" Operator="LessThanEqual"
											ValueToCompare="0">*</asp:CompareValidator>
									</td>
								</tr>
							</table>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="quantity" HeaderText="Units*/<br>Cases*">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtQuantity" CssClass=StandardTextBox Text='<%# DataBinder.Eval(Container, "DataItem.quantity").ToString() == "0" ? "" : DataBinder.Eval(Container, "DataItem.quantity").ToString() %>' runat="server" Width="40px">
										</asp:TextBox>
									</td>
									<td>
										<asp:CompareValidator id="compVal_Quantity" CssClass="LabelError" runat="server" ErrorMessage="The Quantity is invalid"
											ControlToValidate="txtQuantity" Type="Integer" Operator="DataTypeCheck">*</asp:CompareValidator>
									</td>
									<td>
										<asp:CompareValidator id="compVal_QuantityZero" CssClass="LabelError" runat="server" ErrorMessage="The Quantity must be greather than equal than the quantity entered in Pro Code"
											ControlToValidate="txtQuantity" Type="Integer" Operator="GreaterThanEqual" ControlToCompare="txtAdjustmentQuantity">*</asp:CompareValidator>
									</td>
									<td>
										<asp:CompareValidator id="compVal_MinLineItemQuantity" CssClass="LabelError" runat="server" ErrorMessage="The Minimum Quantity for an Item is [value]."
											ControlToValidate="txtQuantity" Type="Integer" Operator="GreaterThanEqual" SetFocusOnError="true" ValueToCompare="0">*</asp:CompareValidator>
									</td>
								</tr>
							</table>
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
						<FooterTemplate>
							<asp:Label id="lblTotalQuantity" Text="0" CssClass="StandardLabel" runat="server"></asp:Label>&nbsp;
							<asp:HiddenField ID="hdnMinTotalQuantity" runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="price" HeaderText="Unit/Case<br>Price">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblPrice" runat="server" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.price", "{0:C}") %>'>
							</asp:Label>&nbsp;
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right" Width="100px" Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblAmount" runat="server" CssClass="DescLabel" Text='<%# (DataBinder.Eval(Container, "DataItem.adjustment_quantity").ToString() == "0" && DataBinder.Eval(Container, "DataItem.quantity").ToString() == "0" ) ? "" : DataBinder.Eval(Container, "DataItem.amount", "{0:C}") %>'>
							</asp:Label>&nbsp;
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
						<FooterTemplate>
							<asp:Label id="lblTotalAmount" Text="0.00" CssClass="StandardLabel" runat="server"></asp:Label>&nbsp;
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
		</td>
	</tr>
	<tr>
		<td>
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td vAlign="top">
						<asp:label id="Label26" runat="server" CssClass="RequiredSymbol">
							*&nbsp;
						</asp:label>
					</td>
					<td>
						<asp:label id="Label27" runat="server" CssClass="RequiredSymbolLabel">
							Required Field
						</asp:label>
					</td>
				</tr>
			</table><br>
		</td>
	</tr>
			
</table>
<script>

function formatCurrency(num)
{
	num = num.toString().replace(/\$|\,/g,'');
	if(isNaN(num))
		num = "0";
	sign = (num == (num = Math.abs(num)));
	num = Math.floor(num*100+0.50000000001);
	cents = num%100;
	num = Math.floor(num/100).toString();
	if(cents<10)
		cents = "0" + cents;
	for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
		num = num.substring(0,num.length-(4*i+3))+','+	num.substring(num.length-(4*i+3));
	return (((sign)?'':'-') + '$' + num + '.' + cents);
}

function ValidateFieldValue(fValue)
{
	var trimValue = fValue.replace(/ /g,'');
	if(trimValue == '')
		trimValue = '0';
	return trimValue;
}

function formatFieldValue(fValue)
{
	var trimValue;
	if( (fValue == '0.00') || (fValue=='0'))
		trimValue = '';
	return trimValue;
}
</script>

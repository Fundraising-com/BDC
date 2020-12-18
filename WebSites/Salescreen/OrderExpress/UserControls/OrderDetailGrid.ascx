<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderDetailGrid" Codebehind="OrderDetailGrid.ascx.cs" %>

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
						<asp:imagebutton id="imgBtnAddNew" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/BtnAdd.gif"
							ToolTip="Click here to add a new item in your order!" AlternateText="Add New"></asp:imagebutton>
						<asp:CustomValidator id="CustVal_MinQty" runat="server" CssClass="Label Error" ErrorMessage="The Minimum for an order is 8 cases.">*</asp:CustomValidator>
					</td>
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
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_code" HeaderText="Item&nbsp;#">
						<HeaderStyle Width="15px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblCatalogItemCode" Width="50px" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.catalog_item_code") %>' runat="server" >
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="catalog_item_name" HeaderText="Product*">
						<HeaderStyle Width="200px"></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:DropDownList id=ddlCatalogItemDetail AutoPostBack="False" DataSource="<%# tblCatalogItemDetail %>" runat="server" SelectedIndex='<%# getSelectedIndex(tblCatalogItemDetail, Convert.ToString(DataBinder.Eval(Container, "DataItem.catalog_item_detail_id"))) %>' DataValueField="catalog_item_detail_id" DataTextField="catalog_item_name">
										</asp:DropDownList>
									</td>
									<td>
										<asp:RequiredFieldValidator id="ReqFldVal_CatalogItemDetail" CssClass="LabelError" runat="server" ErrorMessage="The Product is required"
											ControlToValidate="ddlCatalogItemDetail">*</asp:RequiredFieldValidator>
									</td>
									<td>
										<asp:CompareValidator id="CompVal_CatalogItemDetail" CssClass="LabelError" runat="server" ErrorMessage="The Product is required"
											ValueToCompare="0" Operator="GreaterThan" ControlToValidate="ddlCatalogItemDetail">*</asp:CompareValidator>
									</td>
									<td>
										<asp:CustomValidator id="CustVal_DuplicateProduct" CssClass="LabelError" runat="server" ErrorMessage="The Product is duplicated"
											ControlToValidate="ddlCatalogItemDetail">*</asp:CustomValidator>
									</td>
								</tr>
							</table>
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
					<asp:TemplateColumn HeaderText="Pro Code">
						<HeaderStyle></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtAdjustmentQuantity" Text='<%# DataBinder.Eval(Container, "DataItem.adjustment_quantity").ToString() != "" ? (Convert.ToInt32(DataBinder.Eval(Container, "DataItem.adjustment_quantity")) * -1) : 0 %>' runat="server" Width="40px">
										</asp:TextBox>
									</td>
									<td>
										<asp:CompareValidator id="compVal_AdjQuantity" CssClass="LabelError" runat="server" ErrorMessage="The Adjustment Quantity is invalid"
											ControlToValidate="txtAdjustmentQuantity" Type="Integer" Operator="DataTypeCheck">*</asp:CompareValidator>
									</td>
									<td>
										<asp:CompareValidator id="compVal_AdjQuantityZero" CssClass="LabelError" runat="server" ErrorMessage="The Adjustment Quantity must be greather than zero"
											ControlToValidate="txtAdjustmentQuantity" Type="Integer" Operator="GreaterThanEqual" ValueToCompare="0">*</asp:CompareValidator>
									</td>
								</tr>
							</table>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="quantity" HeaderText="Nb<br>Case*">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtQuantity" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>' runat="server" Width="40px">
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
											ControlToValidate="txtQuantity" Type="Integer" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
									</td>
								</tr>
							</table>
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
						<FooterTemplate>
							<asp:Label id="lblTotalQuantity" Text="0" CssClass="StandardLabel" runat="server"></asp:Label>&nbsp;
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="price" Visible="False" HeaderText="Case<br>Price*">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtPrice" Text='<%# DataBinder.Eval(Container, "DataItem.price", "{0:F}") %>' runat="server" Width="50px">
										</asp:TextBox>
									</td>
									<td>
										<asp:RequiredFieldValidator id="RegFldVal_Price" CssClass="LabelError" runat="server" ErrorMessage="The Price is required"
											ControlToValidate="txtPrice">*</asp:RequiredFieldValidator>
									</td>
									<td>
										<asp:CompareValidator id="compVal_Price" CssClass="LabelError" runat="server" ErrorMessage="The Price is invalid"
											ControlToValidate="txtPrice" Type="Currency" Operator="DataTypeCheck">*</asp:CompareValidator>
									</td>
									<td>
										<asp:CompareValidator id="compVal_PriceZero" CssClass="LabelError" runat="server" ErrorMessage="The Price must be greather than zero"
											ControlToValidate="txtPrice" Type="Currency" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
									</td>
								</tr>
							</table>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="price" HeaderText="Case<br>Price">
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
							<asp:Label id="lblAdjAmount" runat="server" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.adjustment_amount", "{0:C}") %>'>
							</asp:Label>&nbsp;
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Right"></FooterStyle>
						<FooterTemplate>
							<asp:Label id="lblTotalAdjAmount" Text="0.00" CssClass="StandardLabel" runat="server"></asp:Label>&nbsp;
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total" Visible="False">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right" Width="100px" Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblAmount" runat="server" CssClass="DescLabel" Text='<%# DataBinder.Eval(Container, "DataItem.amount", "{0:C}") %>'>
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
		<td><br>
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td vAlign="top">
						<asp:label id="Label26" runat="server" CssClass="RequiredSymbolLabel">
							*&nbsp;
						</asp:label>
					</td>
					<td>
						<asp:label id="Label27" runat="server" Font-Size="x-small" CssClass="RequiredSymbolLabel">
							Required Field
						</asp:label>
					</td>
				</tr>
			</table>
			<br>
		</td>
	</tr>
	<tr id="trBusinessMessage" runat="server" visible="false">
		<td align="center"> <!--Section Body -->
			<table id="Table533" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="lblBusinessMessage" runat="server" CssClass="BizRuleLabel"></asp:label>&nbsp;&nbsp;&nbsp;
						<br>
					</td>
					<td vAlign="top"></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<script>
function updatePrice(type,ddl,adjID,qtyID,priceID,totalID,codeID,nbProductID)
{
	objPrice = document.getElementById(priceID);
	objCode = document.getElementById(codeID);
	objNbProd = document.getElementById(nbProductID);
	if(type == "text")
	{
		objPrice.value = formatCurrency(arrProductPrice[ddl.selectedIndex],"false");
	}	
	else
	{
		objPrice.innerHTML = formatCurrency(arrProductPrice[ddl.selectedIndex]);
	}
	
	objCode.innerHTML = arrProductCode[ddl.selectedIndex];
	objNbProd.innerHTML = arrProductNbUnit[ddl.selectedIndex];
	
	OrderDetailRefresh(adjID,qtyID,priceID,totalID);
}
function OrderDetailRefresh(adjID,qtyID,priceID,totalID)
{
	ODL_RefreshSubTotal(adjID,qtyID,priceID,totalID);
	ODL_RefreshQuantityTotal();
	ODL_RefreshGrandTotal();
}

function ODL_RefreshSubTotal(adjID,qtyID,priceID,totalID)
{
	var qty = ValidateFieldValue(document.getElementById(qtyID).value);
	var price = document.getElementById(priceID).innerHTML.replace('$','');
	var adj = ValidateFieldValue(document.getElementById(adjID).value);
	var total = (qty - adj)*price;
	if(total < 0 ){total = 0.00}
	
	document.getElementById(totalID).innerHTML = formatCurrency(total);
}

function ODL_RefreshGrandTotal()
{
	var cptList = 0.00;
	var sTotal;
	for(var x = 0; x < AmountList.length; x++)
	{
		sTotal = document.getElementById(AmountList[x]).innerHTML.replace('$','');
		sTotal = ValidateFieldValue(sTotal);
		cptList += parseFloat(sTotal.replace(/,/g,''));	
	}
	document.getElementById(lblAmountTotalID).innerHTML = formatCurrency(cptList);
}

function ODL_RefreshQuantityTotal()
{
	var cptList = 0;			
	for(var x = 0; x < QtyList.length; x++)
	{
		if(!isNaN(parseInt(document.getElementById(QtyList[x]).value)))
		cptList += parseInt(document.getElementById(QtyList[x]).value);
	}
	document.getElementById(lblQTYTotalID).innerHTML = cptList;
}

function formatCurrency(num,dollars)
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
	if(dollars == "false")
	{
		return (((sign)?'':'-') + num + '.' + cents);
	}
	else
	{
		return (((sign)?'':'-') + '$' + num + '.' + cents);		
	}
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

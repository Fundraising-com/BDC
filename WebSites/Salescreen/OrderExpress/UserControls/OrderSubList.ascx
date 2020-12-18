<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderSubList" Codebehind="OrderSubList.ascx.cs" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" border="0">
	<tr align="left">
		<td Class="SectionPageTitleInfo">
			<asp:label id="Label4" runat="server">
				Order List
			</asp:label>
		</td>
	</tr>
	<tr>
		<td height=5px>			
		</td>
	</tr>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgOrder runat="server" width=600px ShowFooter="True" DataSource="<%# DVOrders %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" CssClass=GridStyle CellPadding="3" AllowSorting="False" PageSize="30" SearchMode="0">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle HorizontalAlign="Center" Width="1%"></HeaderStyle>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" height=15px ImageUrl="~/images/BtnDetail.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Order_ID") %>' CommandName="Select" CausesValidation="False">
							</ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<HeaderStyle HorizontalAlign="Center" Width="10px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblStatusRead" runat="server" Width="5px" BackColor='<%# System.Drawing.Color.FromName(DataBinder.Eval(Container, "DataItem.Color_Code").ToString()) %>' Height="5px" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
								&nbsp;&nbsp;
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="order_status_short_description" SortExpression="order_status_short_description"
						ReadOnly="True" HeaderText="Status">
						<ItemStyle Width="100px" wrap="false"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="order_status_name" SortExpression="order_status_name"
						ReadOnly="True" HeaderText="Status">
						<ItemStyle Width="100px" wrap="false"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="order_id" SortExpression="order_id" ReadOnly="True" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="fulf_order_id" SortExpression="fulf_order_id" ReadOnly="True" HeaderText="EDS&nbsp;Order&nbsp;#"></asp:BoundColumn>
					<asp:TemplateColumn SortExpression="order_date" HeaderText="Order&#160;Date">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.order_date", "{0:MM/dd/yyyy}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="total_quantity" SortExpression="total_quantity" ReadOnly="True" HeaderText="Total&#160;Qty"></asp:BoundColumn>
					<asp:BoundColumn DataField="total_amount_adj" SortExpression="total_amount_adj" ReadOnly="True" HeaderText="Total Amt"
						DataFormatString="{0:C}"></asp:BoundColumn>
				</Columns>
			</cc2:sorteddatagrid>
		</TD>
	</TR>
	<tr>
		<td height=5px>			
		</td>
	</tr>	
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td colSpan="2">
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Order(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

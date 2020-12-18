<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FormOrderTypeList" Codebehind="FormOrderTypeList.ascx.cs" %>

<table id="tblFormDetailTitle" cellSpacing="0" cellPadding="0" width="550" border="0">
	<tr>
		<td><asp:label id="lblFormTitle" CssClass="StandardLabel" runat="server"></asp:label>
			<br>
		</td>
	</tr>
	<TR>
		<TD><!--DataGrid  -->
			<asp:DataGrid id=dtgFormOrderType runat="server" Font-Size="8pt" PageSize="30" Width="100%" AllowPaging="False" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#CCCCCC" AutoGenerateColumns="False" DataSource="<%# dTblOrderType %>" ShowFooter="True" SearchMode="0" DataKeyField="order_type_id">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:CheckBox id="chkSelected" runat="server" ></asp:CheckBox>
						</ItemTemplate>
                        <HeaderStyle Width="1%" />
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="order_type_name" SortExpression="order_type_name" ReadOnly="True" HeaderText="Order type">
					</asp:BoundColumn>
					
				</Columns>
			</asp:DataGrid>
		</TD>
	</TR>
</table>

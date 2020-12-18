<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.PhoneNumberListInfo" Codebehind="PhoneNumberListInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td>			
			<asp:datagrid id=dtgPhoneNumber runat="server" DataSource="<%# dtPhoneNumber %>" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" DataKeyField="phone_number_entity_id" AllowSorting="True" ShowFooter="True" Font-Size="Small" Font-Names="Verdana">
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<EditItemStyle Wrap="False"></EditItemStyle>
				<AlternatingItemStyle Font-Size="X-Small" Wrap="False" BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle Font-Size="X-Small" Wrap="False" ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Size="X-Small" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn SortExpression="phone_number_type_name" HeaderText="Type">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblTypeName" Text='<%# DataBinder.Eval(Container, "DataItem.phone_number_type_name") %>' runat="server">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="phone_number" HeaderText="Phone Number">
						<HeaderStyle Width="400px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblPhoneNumber" Text='<%# DataBinder.Eval(Container, "DataItem.phone_number") %>' runat="server">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White" BackColor="#999999"
					Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
	<tr>
		<td align="center"></td>
	</tr>
</table>

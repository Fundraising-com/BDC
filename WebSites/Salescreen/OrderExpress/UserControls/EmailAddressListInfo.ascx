<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.EmailAddressListInfo" Codebehind="EmailAddressListInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td>
			<asp:datagrid id=dtgEmailAddress runat="server" Font-Names="Verdana" Font-Size="Small" ShowFooter="True" AllowSorting="True" DataKeyField="email_entity_id" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#999999" DataSource="<%# dtEmailAddress %>">
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<EditItemStyle Wrap="False"></EditItemStyle>
				<AlternatingItemStyle Font-Size="X-Small" Wrap="False" BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle Font-Size="X-Small" Wrap="False" ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Size="X-Small" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn SortExpression="email_type_name" HeaderText="Type">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.email_type_name") %>' runat="server" Width="100px">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="email_address" HeaderText="Email&nbsp;Address">
						<HeaderStyle Width="200px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblEmailAddress" Text='<%# DataBinder.Eval(Container, "DataItem.email_address") %>' runat="server" Width="200px">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="recipient_name" HeaderText="Recipient&nbsp;Name">
						<HeaderStyle Width="200px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.recipient_name") %>' runat="server" Width="200px">
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

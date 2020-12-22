<%@ Control Language="c#" AutoEventWireup="True" CodeFile="ProductGrid.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.Package.ProductGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
	<tr vAlign="top">
		<td align="left"><asp:label id="messagelabel" Runat="server" CssClass="NormalTextBold"></asp:label><asp:datagrid id="ProductDataGrid" CssClass="NormalText" Width="100%" DataKeyField="ProductId"
				AutoGenerateColumns="False" CellPadding="3" BorderWidth="1px" BorderStyle="None" BorderColor="#0A246A" AllowPaging="True" AllowSorting="True" runat="server" PageSize="20">
				<FooterStyle BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<AlternatingItemStyle CssClass="AlternateItemBackGround"></AlternatingItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Left" CssClass="AlternateItemBackGround NormalTextBold Passive"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemTemplate>
							<asp:CheckBox id="SelectCheckBox" runat="server"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Name">
						<ItemTemplate>
							<asp:LinkButton ID="ProductLinkbutton" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>' OnClick="ProductLink_Click">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="ProductCode" HeaderText="Product Code">
						<HeaderStyle Width="25%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="ScratchBookId" HeaderText="ScratchBookId"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" PageButtonCount="15" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
</table>

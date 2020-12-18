<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.WarehouseInventory" Codebehind="WarehouseInventory.ascx.cs" %>

<table width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td>&nbsp;&nbsp;&nbsp;&nbsp;
		</td>
		<td><br>
			<asp:DataGrid id="dtgWarehouseInventory" runat="server" ShowFooter="True" AutoGenerateColumns="False"
				BorderColor="#CCCCCC" CssClass=GridStyle CellPadding="1"
				AllowSorting="True" AllowPaging="True" Width="100%" PageSize="30" Font-Size="8pt">
				<PAGERSTYLE Mode="NumericPages" CssClass="PagerItemStyle"></PAGERSTYLE>
				<ALTERNATINGITEMSTYLE CssClass="AlternatingItemStyle_off"></ALTERNATINGITEMSTYLE>
				<FOOTERSTYLE CssClass="FooterItemStyle"></FOOTERSTYLE>
				<SELECTEDITEMSTYLE CssClass="SelectedItemStyle"></SELECTEDITEMSTYLE>
				<ITEMSTYLE CssClass="ItemStyle_off"></ITEMSTYLE>
				<HEADERSTYLE CssClass="HeaderItemStyle" forecolor="White" Wrap="False"></HEADERSTYLE>
				<Columns>
					<asp:TemplateColumn HeaderText="Product&nbsp;Code">
						<ItemTemplate>
							<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FGITEM") + " " + DataBinder.Eval(Container, "DataItem.FGCOUP") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Description">
						<HeaderStyle Width="250px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label3" width="300px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IVDESC")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Available" ItemStyle-HorizontalAlign="Right">
						<ItemTemplate>
							<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FGAVAL")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
			<br>
		</td>
		<td>&nbsp;&nbsp;&nbsp;&nbsp;
		</td>
	</tr>
</table>

<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CatalogItemSubList" Codebehind="CatalogItemSubList.ascx.cs" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td Class="SectionPageTitleInfo">
			<asp:label id="Label4" runat="server">
				Catalog Item List
			</asp:label></td>
	</tr>
	<TR>
		<TD><!--DataGrid  --><br>
			<cc2:sorteddatagrid id=dtgCatalogItem runat="server" Font-Size="8pt" width=600px ShowFooter="True" DataSource="<%# DVCatalogItems %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="10" SearchMode="0">
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
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.catalog_item_id") %>' CommandName="Select" CausesValidation="False">
							</ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="catalog_item_id" SortExpression="catalog_item_id" ReadOnly="True" HeaderText="ID">
						<HeaderStyle Width="1%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="catalog_item_code" SortExpression="catalog_item_code" ReadOnly="True"
						HeaderText="Code">
						<HeaderStyle Width="1%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="catalog_item_name" SortExpression="catalog_item_name" ReadOnly="True"
						HeaderText="Catalog Item Name"></asp:BoundColumn>
				</Columns>
			</cc2:sorteddatagrid>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Catalog Item(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

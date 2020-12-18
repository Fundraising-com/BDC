<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CatalogGroupCatalogSubList" Codebehind="CatalogGroupCatalogSubList.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td Class="SectionPageTitleInfo">
			<asp:label id="Label4" runat="server">
				Catalog Group List
			</asp:label></td>
	</tr>
	<TR>
		<TD><!--DataGrid  --><br>
			<cc2:sorteddatagrid id=dtgCatalogGroup runat="server" Font-Size="8pt" width=600px ShowFooter="True" DataSource="<%# DVCatalogGroups %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" PageSize="30" SearchMode="0">
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
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.catalog_group_id") %>' CommandName="Select" CausesValidation="False">
							</ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="catalog_group_id" SortExpression="catalog_group_id" ReadOnly="True" HeaderText="ID">
						<HeaderStyle Width="1%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="catalog_group_code" SortExpression="catalog_group_code" ReadOnly="True"
						HeaderText="Code">
						<HeaderStyle Width="1%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="catalog_group_name" SortExpression="catalog_group_name" ReadOnly="True"
						HeaderText="Catalog Group Name"></asp:BoundColumn>
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
							Number of Catalog Group(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

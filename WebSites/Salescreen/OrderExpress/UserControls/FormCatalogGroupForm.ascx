<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FormCatalogGroupForm" Codebehind="FormCatalogGroupForm.ascx.cs" %>

<table id="tblFormDetailTitle" cellSpacing="0" cellPadding="0" width="550" border="0">
	<TR>
		<TD>
			<asp:ImageButton id="imgBtnAddNew" runat="server" ImageUrl="~/images/BtnAdd.gif" CommandName="Add"
				CausesValidation="False"></asp:ImageButton>
		</TD>
	</TR>
	<tr>
		<td>
			<asp:datalist id=dtLstFormCatalogGroup  runat="server" DataKeyField="form_catalog_group_id" width="500px" DataSource="<%# DVFormCatalogGroup %>">
				<SeparatorTemplate>
					<hr width="100%" size="1px" color="#003366">
				</SeparatorTemplate>
				<ItemTemplate>
					<TABLE cellSpacing="0" cellPadding="3" border="3" style="BORDER-RIGHT: 1px outset; BORDER-TOP: 1px outset; BORDER-LEFT: 1px outset; BORDER-BOTTOM: 1px outset">
						<TR>
							<TD>
								<asp:label id="lblCatalogGroup" CssClass="StandardLabel" runat="server">Catalog&nbsp;Group&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD>
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
										<td>
											<asp:DropDownList id="ddlCatalogGroup" runat="server" AutoPostBack=True Width="200px" DataSource="<%# DVCatalogGroup %>" DataTextField="catalog_group_name" DataValueField="catalog_group_id" SelectedIndex='<%# getSelectedIndex(DVCatalogGroup,Convert.ToString(DataBinder.Eval(Container, "DataItem.catalog_group_id"))) %>'>
											</asp:DropDownList>
										</td>
										<td>
											<asp:CompareValidator id="CompVal_CatalogGroup" CssClass="LabelError" runat="server" ErrorMessage="The Catalog is required sss"
												ControlToValidate="ddlCatalogGroup" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
										</td>
									</tr>
								</table>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="Label3" CssClass="StandardLabel" runat="server">Catalog&nbsp;Category&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD vAlign="top">
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<asp:label id="Label1" CssClass="StandardLabel" runat="server">For Product</asp:label>
										</td>
										<td>
											&nbsp;
										</td>
										<td>
											<asp:label id="Label2" CssClass="StandardLabel" runat="server">For Supply</asp:label>
										</td>
									</tr>
									<tr>
										<td>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td>
														<asp:DropDownList id="ddlProductCategory" runat="server" Width="200px" DataSource='<%# getCatalogCategoryDataView(Convert.ToString(DataBinder.Eval(Container, "DataItem.catalog_group_id"))) %>' DataTextField="catalog_item_category_name" DataValueField="catalog_item_category_id" SelectedIndex='<%# getSelectedIndex(DVCatalogItemCategory,Convert.ToString(DataBinder.Eval(Container, "DataItem.product_catalog_item_category_id"))) %>'>
														</asp:DropDownList>
													</td>
													<td>
														<asp:CompareValidator id="CompVal_ProductCategory" CssClass="LabelError" runat="server" ErrorMessage="The Catalog Category for Product is required"
															ControlToValidate="ddlProductCategory" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
													</td>
												</tr>
											</table>
										</td>
										<td>
											&nbsp;
										</td>
										<td>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td>
														<asp:DropDownList id="ddlSupplyCategory" runat="server" Width="200px" DataSource='<%# getCatalogCategoryDataView(Convert.ToString(DataBinder.Eval(Container, "DataItem.catalog_group_id"))) %>' DataTextField="catalog_item_category_name" DataValueField="catalog_item_category_id" SelectedIndex='<%# getSelectedIndex(DVCatalogItemCategory, Convert.ToString(DataBinder.Eval(Container, "DataItem.supply_catalog_item_category_id"))) %>'>
														</asp:DropDownList>
													</td>
													<td>
													</td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</TD>
						</TR>
						<tr>
							<td colspan="3">
								<asp:ImageButton id="imgBtnDelete" runat="server" ImageUrl="~/images/BtnDelete.gif" CommandName="Delete"
									CausesValidation="False"></asp:ImageButton>
							</td>
						</tr>
					</TABLE>
				</ItemTemplate>
			</asp:datalist>
		</td>
	</tr>
	<tr>
		<td align="center"></td>
	</tr>
</table>

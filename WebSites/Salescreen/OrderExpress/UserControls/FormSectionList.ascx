<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FormSectionList" Codebehind="FormSectionList.ascx.cs" %>

<table id="tblFormDetailTitle" cellSpacing="0" cellPadding="0" width="550" border="0">
	<TR>
		<TD>
			<asp:ImageButton id="imgBtnAddNew" runat="server" ImageUrl="~/images/BtnAdd.gif" CommandName="Add"
				CausesValidation="False"></asp:ImageButton>
		</TD>
	</TR>
	<tr>
		<td>
			<asp:datalist id=dtLstFormSection  runat="server" DataKeyField="form_section_id" width="500px" DataSource="<%# DVFormSection %>">
				<SeparatorTemplate>
					<hr width="100%" size="1px" color="#003366">
				</SeparatorTemplate>
				<ItemTemplate>
					<TABLE cellSpacing="0" cellPadding="3" border="3" style="BORDER-RIGHT: 1px outset; BORDER-TOP: 1px outset; BORDER-LEFT: 1px outset; BORDER-BOTTOM: 1px outset">
						
						<TR>
							<TD vAlign="top">
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<asp:label id="Label4" CssClass="StandardLabel" runat="server">Section&nbsp;Title:&nbsp;</asp:label>
										</td>
										<td>
											&nbsp;
										</td>
										<td>
											<asp:textbox id="txtSectionTitle" Columns="100" CssClass="StandardTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.form_section_title") %>' runat="server"></asp:textbox>
										</td>
									</tr>
									<tr>
										<td>
											<asp:label id="Label5" CssClass="StandardLabel" runat="server">Description:&nbsp;</asp:label>
										</td>
										<td>
											&nbsp;
										</td>
										<td>
											<asp:textbox id="txtDescription" MaxLength="250" Columns=100 TextMode=MultiLine Rows=4 CssClass="StandardTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.description") %>' runat="server"></asp:textbox>
										</td>
									</tr>
									<TR>
							            <TD>
								            <asp:label id="lblCatalog" CssClass="StandardLabel" runat="server">Catalog:&nbsp;</asp:label>
							            </TD>
							            <td>
											&nbsp;
										</td>
										<TD>
								            <table border="0" cellpadding="0" cellspacing="0">
									            <tr>
										            <td>
										            <td>
											            <asp:DropDownList id="ddlCatalog" runat="server" AutoPostBack=True Width="200px" OnSelectedIndexChanged="ddlCatalog_SelectedIndexChanged" DataSource="<%# DVCatalog %>" DataTextField="catalog_name" DataValueField="catalog_id" SelectedIndex='<%# getSelectedIndex(DVCatalog,Convert.ToString(DataBinder.Eval(Container, "DataItem.catalog_id"))) %>'>
											            </asp:DropDownList>
										            </td>
										            <td>
											            <asp:CompareValidator id="CompVal_Catalog" CssClass="LabelError" runat="server" ErrorMessage="The Catalog is required."
												            ControlToValidate="ddlCatalog" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
										            </td>
									            </tr>
								            </table>
							            </TD>
						            </TR>
									<tr>
										<td>
											<asp:label id="Label2" CssClass="StandardLabel" runat="server">Catalog&nbsp;Category:&nbsp;</asp:label>
										</td>
										<td>
											&nbsp;
										</td>
										<td>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td>
														<asp:DropDownList id="ddlCatalogCategory" runat="server" Width="200px" DataSource='<%# getCatalogCategoryDataView(Convert.ToString(DataBinder.Eval(Container, "DataItem.catalog_id"))) %>' DataTextField="catalog_item_category_name" DataValueField="catalog_item_category_id" SelectedIndex='<%# getCatalogItemCategorySelectedIndex(Convert.ToString(DataBinder.Eval(Container, "DataItem.catalog_item_category_id"))) %>'>
														</asp:DropDownList>
													</td>
													<td>
														<asp:CompareValidator id="CompVal_CatalogCategory" CssClass="LabelError" runat="server" ErrorMessage="The Catalog Category is required"
															ControlToValidate="ddlCatalogCategory" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td>
											<asp:label id="Label1" CssClass="StandardLabel" runat="server">Section&nbsp;Type:&nbsp;</asp:label>
										</td>
										<td>
											&nbsp;
										</td>
										<td>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td>
														<asp:DropDownList id="ddlFormSectionType" runat="server" Width="200px" DataSource='<%# DVFormSectionType %>' DataTextField="form_section_type_name" DataValueField="form_section_type_id" SelectedIndex='<%# getSelectedIndex(DVFormSectionType, Convert.ToString(DataBinder.Eval(Container, "DataItem.form_section_type_id"))) %>'>
														</asp:DropDownList>
													</td>
													<td>
														<asp:CompareValidator id="compVal_FormSectionType" CssClass="LabelError" runat="server" ErrorMessage="The Section Type is required"
															ControlToValidate="ddlFormSectionType" Type="Integer" ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
													</td>
													<td>
											            &nbsp;&nbsp;
										            </td>
										            <td>
											            <asp:label id="Label3" CssClass="StandardLabel" runat="server">Section&nbsp;Number:&nbsp;</asp:label>
										            </td>
										            <td>
											            &nbsp;
										            </td>
										            <td>
											            <asp:textbox id="txtSectionNumber" Columns="1" MaxLength=1 CssClass="StandardTextBox" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.form_section_number") %>'></asp:textbox>
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

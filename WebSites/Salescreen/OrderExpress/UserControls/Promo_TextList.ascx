<%@ Reference Control="Promo_TextDetailInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Promo_TextList" Codebehind="Promo_TextList.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="20%" border="0">
				<TR>
					<TD><uc1:searchmodule id="QSPFormSearchModule" runat="server"></uc1:searchmodule></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td>
									<hr width="100%" color="#666666" SIZE="1">
								</td>
							</tr>
							<TR>
								<TD>
									<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD colSpan="2"><asp:label id="Label6" CssClass="StandardLabel" Runat="server">
													Filter&nbsp;By:
												</asp:label></TD>
										</TR>
										<TR>
											<td>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td><asp:label id="Label5" runat="server" CssClass="ModuleSearchText">
																Subdivision&nbsp;:
															</asp:label></td>
														<TD noWrap><asp:dropdownlist id="ddlSubdivision" runat="server"></asp:dropdownlist></TD>
														<td>&nbsp;
														</td>
														<td><asp:label id="LblIsNational" runat="server" CssClass="ModuleSearchText">
																Promotion's text &nbsp;:
															</asp:label></td>
														<TD noWrap><asp:dropdownlist id="ddlNational" runat="server">
																<asp:ListItem Selected="True" Value="-1">--SELECT--</asp:ListItem>
																<asp:ListItem Value="0">Not National</asp:ListItem>
																<asp:ListItem Value="1">National</asp:ListItem>
															</asp:dropdownlist></TD>
													</tr>
													<tr>
														<td><asp:label id="Label10" runat="server" CssClass="ModuleSearchText">
																Display&nbsp;Status:
															</asp:label></td>
														<TD noWrap><asp:dropdownlist id="ddlDisplayStatus" runat="server">
																<asp:ListItem Selected="True" Value="-1">--SELECT--</asp:ListItem>
																<asp:ListItem Value="0">Disabled</asp:ListItem>
																<asp:ListItem Value="1">Enabled</asp:ListItem>
															</asp:dropdownlist></TD>
														<td>&nbsp;
														</td>
														<TD id="tdFilterFMReportedTo" align="right" colSpan="2" runat="server">
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td><asp:label id="Label3" runat="server" CssClass="ModuleSearchText">
																			&nbsp;All&nbsp;FM&nbsp;Reported&nbsp;To&nbsp;:&nbsp;
																		</asp:label></td>
																	<td><asp:checkbox id="chkReportedTo" runat="server" CssClass="boxes"></asp:checkbox></td>
																</tr>
															</table>
														</TD>
													</tr>
												</table>
											</td>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<tr>
								<td>
									<hr width="100%" color="#666666" SIZE="1">
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td><asp:label id="lblNote" runat="server" CssClass="FilterNoteTitle">Note&nbsp;:</asp:label></td>
								<td><asp:label id="lblNoteDesc" runat="server" CssClass="FilterNoteDesc">All criteria is considered when refreshing the list.</asp:label></td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD><br>
			<asp:hyperlink id="hypLnkAddNew" runat="server" ImageUrl="~/images/BtnAdd.gif" NavigateUrl="javascript:void(0);"></asp:hyperlink></TD>
	</TR>
	<tr>
		<td><BR>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click on Column Headings to Resort Data.</asp:label></td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<TR>
		<TD><!--DataGrid  --><cc2:sorteddatagrid id=dtgPromo_Text runat="server" SearchMode="0" width="700px" PageSize="10" AllowPaging="True" AllowSorting="True" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#CCCCCC" DataSource="<%# DVPromo_Text %>" AutoGenerateColumns="False" ShowFooter="True" Font-Size="10pt">
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle" Wrap="False"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle" Wrap="False"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off" Font-Size="12px" Wrap="False"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif">
							</ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Promo_Text_code" HeaderText="Code">
						<ItemTemplate>
							<asp:Label id="Code" runat="server" Text='<%# ((String)DataBinder.Eval(Container, "DataItem.Promo_Text_code")).Replace(" ","&nbsp;") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Promo_Text_name" HeaderText="Short">
						<ItemTemplate>
							<asp:Label id="Name" runat="server" Text='<%# ((String)DataBinder.Eval(Container, "DataItem.Promo_Text_name")).Replace(" ","&nbsp;") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="description" HeaderText="Long">
						<HeaderStyle Width="400px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Description" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.description") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="field_sales_manager_id" HeaderText="FM&#160;ID">
						<ItemTemplate>
							<center>
								<asp:Label id="lblFM_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.field_sales_manager_id") %>' />
							</center>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="National">
						<HeaderStyle Width="10px"></HeaderStyle>
						<ItemTemplate>
							<CENTER>
								<asp:CheckBox id=chkNational Runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsNational")) %>'>
								</asp:CheckBox></CENTER>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText="Deleted">
						<HeaderStyle Width="10px"></HeaderStyle>
						<ItemTemplate>
							<asp:CheckBox Runat="server" ID="chkArchived" Checked='<%# DataBinder.Eval(Container, "DataItem.deleted")%>'>
							</asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
			</cc2:sorteddatagrid></TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of text(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

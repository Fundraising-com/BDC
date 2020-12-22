<%@ Reference Control="WarehouseDetailInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.WarehouseList" Codebehind="WarehouseList.ascx.cs" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<table cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="80%" border="0">
							<TR>
								<TD><uc1:searchmodule id="QSPFormSearchModule" MaxLengthValidate="0" runat="server"></uc1:searchmodule></TD>
							</TR>
							<TR>
								<TD>
									<TABLE id="tblFilter" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
										<tr>
											<td>
												<hr width="100%" color="#666666" SIZE="1">
											</td>
										</tr>
										<TR>
											<TD>
												<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<TD colSpan="2"><asp:label id="lblHeaderFilter" Runat="server" CssClass="StandardLabel">
																Filter 
																By:
															</asp:label>
														</TD>
													</TR>
													<TR>
														<td colSpan="2">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																<tr>
																	<td>
																		<table cellSpacing="0" cellPadding="0" border="0">
																			<tr>
																				<td><asp:label id="lblState" runat="server" CssClass="ModuleSearchText">
																						State&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																					</asp:label></td>
																				<td><asp:dropdownlist id="ddlState" runat="server" CssClass="boxes"></asp:dropdownlist></td>
																			</tr>
																		</table>
																	</td>
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
										<tr>
											<td>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td><asp:label id="lblNote" runat="server" CssClass="FilterNoteTitle">Note&nbsp;:</asp:label></td>
														<td><asp:label id="lblNoteDesc" runat="server" CssClass="FilterNoteDesc">All criteria is considered when refreshing the list.</asp:label></td>
													</tr>
												</table>
											</td>
										</tr>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
					<td vAlign="top" align="right"></td>
				</TR>
			</table>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<tr >
					<td align="left" colspan="2">
						<asp:imagebutton id="imgbtnAddWarehouse" runat="server" AlternateText="Click here to Add new Warehouse !"
							ImageUrl="~/images/btnAdd.gif" Visible="false"></asp:imagebutton><br>
						<br>
					</td>
				</tr>
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click on Column Headings to Resort Data.</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD width="100%"><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgWarehouse runat="server" SearchMode="0" ShowFooter="True" 
			DataSource="<%# DVWarehouse %>" AutoGenerateColumns="False" 
			CellPadding="3" AllowSorting="True" AllowPaging="True" Width="100%" PageSize="30" Font-Size="10pt"
			
			BorderColor="#CCCCCC" 
			BorderStyle="None" 
			BorderWidth="1px" 
			BackColor="White" 
			>
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				 <FooterStyle CssClass="FooterItemStyle" Font-Size="10px"></FooterStyle>
                <ItemStyle CssClass="ItemStyle_off" Font-Size="12px"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" Font-Size="12px" ForeColor="White">
                </HeaderStyle>
				
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:imagebutton id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandName="Create"
								CausesValidation="False"></asp:imagebutton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="warehouse_id" HeaderText="WH&nbsp;ID">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label8" Width="50px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.warehouse_id")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fulf_warehouse_id" HeaderText="EDS&nbsp;WH&nbsp;#">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblWarehouseNumber" Width="50px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fulf_warehouse_id")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="warehouse_name" HeaderText="Warehouse&nbsp;Name">
						<ItemTemplate>
							<asp:HyperLink id="hypLnkName" Width="250px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.warehouse_name") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="city" SortExpression="city" ReadOnly="True" HeaderText="City">
						<ItemStyle Wrap="False" Width="130px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
						
						<ItemTemplate>
							<asp:Label id=Label2 Width="100px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="subdivision_name_1" SortExpression="subdivision_name_1" ReadOnly="True"
						HeaderText="State"></asp:BoundColumn>
				</Columns>
			</cc2:sorteddatagrid>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellpadding="0" border="0">
				<TR>
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Warehouse(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

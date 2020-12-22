<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.VendorSelector" Codebehind="VendorSelector.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModuleSelector" Src="SearchModuleSelector.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD><uc1:SearchModuleSelector id="QSPFormSearchModule" runat="server"></uc1:SearchModuleSelector></TD>
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
																	<td><asp:label id="lblState" runat="server" CssClass="ModuleSearchTextSelector">
																			State&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																		</asp:label></td>
																	<td><asp:dropdownlist id="ddlState" runat="server" CssClass="ModuleSearchSelector"></asp:dropdownlist></td>
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
			</TABLE>
			<BR>
			<asp:label id="lblCurrentIndex" runat="server" Font-Size="xx-small" CssClass="eRewardsInstr">Page 1 of 1</asp:label></TD>
	</TR>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgList runat="server" Font-Size="8pt" ShowFooter="True" DataSource="<%# DVList %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="10">
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="Black" BackColor="#CCCCCC"></SelectedItemStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle CssClass="HeaderItemStyle"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
						<ItemTemplate>
							<asp:ImageButton id="imgBtnSelect" runat="server" ImageUrl="~/images/BtnSelect.gif" CommandName="Select"
								CausesValidation="False"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Vendor_id" HeaderText="ID">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Vendor_id")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Vendor_name" HeaderText="Vendor&nbsp;Name">
						<HeaderStyle Wrap="False" Width="170px"></HeaderStyle>
						<ItemTemplate>
							<asp:HyperLink id="hypLnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Vendor_name") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="city" SortExpression="city" ReadOnly="True" HeaderText="City">
						<ItemStyle Wrap="False" Width="130px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="subdivision_name_1" SortExpression="subdivision_name_1" ReadOnly="True"
						HeaderText="State"></asp:BoundColumn>
				</Columns>
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
			</cc2:sorteddatagrid>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListSelectorLabel">
							Number of Vendor(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td>
						<asp:ImageButton id="imgBtnOK" Visible="False" runat="server" CausesValidation="False" ImageUrl="~/images/btnOK.gif"
							AlternateText="Click here to confirm your selection"></asp:ImageButton>
					</td>
					<td>
						<asp:HyperLink id="hypLnkCancel" Visible="False" runat="server" ImageUrl="~/images/btnCancel.gif"
							NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

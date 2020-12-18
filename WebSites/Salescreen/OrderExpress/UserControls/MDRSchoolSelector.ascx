<%@ Register TagPrefix="uc1" TagName="SearchModuleSelector" Src="SearchModuleSelector.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.MDRSchoolSelector" Codebehind="MDRSchoolSelector.ascx.cs" %>

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
								<TD colSpan="2"><asp:label id="Label3" CssClass="StandardLabel" Runat="server">
										Filter By:
									</asp:label></TD>
							</TR>
							<TR>
								<td>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<TD>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:label id="Label4" runat="server" CssClass="ModuleSearchTextSelector">
																Organization&nbsp;Type&nbsp;:&nbsp;
															</asp:label></td>
														<TD><asp:dropdownlist id="ddlOrganizationType" CssClass="ModuleSearchSelector" runat="server"></asp:dropdownlist></TD>
													</tr>
												</table>
											</TD>
											<td align="center">
												<table border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td>
															<asp:label id="Label5" runat="server" CssClass="ModuleSearchTextSelector">
																State&nbsp;:&nbsp;
															</asp:label>
														</td>
														<td>
															<asp:dropdownlist id="ddlState" CssClass="ModuleSearchSelector" runat="server"></asp:dropdownlist>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</TR>
							<tr>
								<td>
									<hr size="1" width="100%" color="#666666">
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
			<asp:label id="lblCurrentIndex" runat="server" Font-Size="xx-small" CssClass="CurrentPageIndexSelectorLabel">Page 1 of 1</asp:label></TD>
	</TR>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgList runat="server" Font-Size="8pt" ShowFooter="True" DataSource="<%# DVList %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="10" onselectedindexchanged="dtgList_SelectedIndexChanged">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
						<ItemTemplate>
							<asp:ImageButton id="imgBtnSelect" runat="server" ImageUrl="~/images/BtnSelect.gif" CommandName="Select"
								CausesValidation="False" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn visible="false">
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CausesValidation="False"></ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="PID" HeaderText="MDR&nbsp;PID">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PID") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Name" HeaderText="School Name">
						<HeaderStyle Wrap="False" width=350px></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:HyperLink id="hypLnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Address" Visible="False" HeaderText="Address">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbAddress" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="City" HeaderText="City">
						<HeaderStyle Wrap="False" width="100px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="State" HeaderText="State">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbState" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.State") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbZip" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn visible="false">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblOrganizationID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.organization_id") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</cc2:sorteddatagrid>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListSelectorLabel">
							Number of MDR School(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR id="trButton" runat="server">
		<TD align="center">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td>
						<asp:ImageButton id="imgBtnOK" runat="server" CausesValidation="False" ImageUrl="~/images/btnOK.gif"
							AlternateText="Click here to confirm your selection" Visible="False"></asp:ImageButton>
					</td>
					<td>
						<asp:HyperLink id="hypLnkCancel" runat="server" ImageUrl="~/images/btnCancel.gif" NavigateUrl="javascript:window.close();"
							Visible="False">Cancel</asp:HyperLink>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

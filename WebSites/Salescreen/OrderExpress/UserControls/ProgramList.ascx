<%@ Reference Control="ProgramDetailInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramList" Codebehind="ProgramList.ascx.cs" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="20%" border="0">
				<TR>
					<TD><uc1:searchmodule id="QSPFormSearchModule" runat="server"></uc1:searchmodule></TD>
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
								<TD colSpan="2">
									<asp:label id="Label3" CssClass="StandardLabel" Runat="server">
										Filter By:
									</asp:label>
								</TD>
							</TR>
							<TR>
								<td>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td>
												<table border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td>
															<asp:label id="Label5" runat="server" CssClass="ModuleSearchText">
																Program Type&nbsp;:&nbsp;
															</asp:label>
														</td>
														<td>
															<asp:dropdownlist id="ddlProgramType" runat="server"></asp:dropdownlist>
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
		</TD>
	</TR>
	<TR>
		<TD><br>
			<asp:HyperLink id="hypLnkAddNew" runat="server" NavigateUrl="javascript:void(0);" ImageUrl="~/images/BtnAdd.gif"></asp:HyperLink>
		</TD>
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
		<TD><!--DataGrid  -->
			<cc2:sorteddatagrid id="dtgProgramItems" runat="server" ShowFooter="True" AutoGenerateColumns="False" 
			DataSource="<%# DVProgram %>" BorderColor="#CCCCCC" CssClass=GridStyle
			BackColor="White" CellPadding="2" AllowSorting="True" AllowPaging="True" PageSize="30" width=700px>
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle></HeaderStyle>
						<ItemTemplate>
							<asp:ImageButton id="imgBtnDetail" height=15px runat="server" CausesValidation="False" ImageUrl="~/images/BtnDetail.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.program_id") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="program_type_name" HeaderText="Type">
						<ItemStyle Wrap="False" ></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblProgramTypeName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.program_type_name") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="program_name" HeaderText="Program Name">
						<ItemStyle Wrap="False" Width="100%"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblProgramName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.program_name") %>' />
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
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Program(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

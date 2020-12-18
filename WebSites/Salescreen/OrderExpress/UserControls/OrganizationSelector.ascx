<%@ Reference Control="OrganizationDetailInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchModuleSelector" Src="SearchModuleSelector.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrganizationSelector" Codebehind="OrganizationSelector.ascx.cs" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD><uc1:searchmoduleselector id="QSPFormSearchModule" runat="server"></uc1:searchmoduleselector></TD>
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
											<td align="right">
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
										<tr>
											<td colspan="2">
												<table id="tblFMFilterOption" cellSpacing="0" cellPadding="0" border="0" runat="server"
													width="100%">
													<tr>
														<TD>
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td><asp:label id="Label1" runat="server" CssClass="ModuleSearchTextSelector">
																			Display&nbsp;Options:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																		</asp:label></td>
																	<td>
																		<asp:dropdownlist id="ddlFSMDisplayMode" runat="server" CssClass="ModuleSearchSelector">
																			<asp:ListItem Value="0" Selected="True">My Accounts Only</asp:ListItem>
																			<asp:ListItem Value="2">My Direct Report(s) Only</asp:ListItem>
																			<asp:ListItem Value="1">My Accounts & My Direct Reports</asp:ListItem>																			
																		</asp:dropdownlist><!--<asp:ListItem Value="3">All QSP Accounts</asp:ListItem>-->
																	</td>
																</tr>
															</table>
														</TD>
													</tr>
												</table>
												<table id="tblFieldSupportFilterOption" cellSpacing="0" cellPadding="0" border="0" runat="server"
													width="100%">
													<tr>
														<TD>
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td><asp:label id="Label6" runat="server" CssClass="ModuleSearchTextSelector">
																			FSM&nbsp;ID:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																		</asp:label></td>
																	<td>
																		<asp:TextBox id="txtFSMID" runat="server" Width="60px" MaxLength="4" CssClass="ModuleSearchSelector"></asp:TextBox>
																	</td>
																</tr>
															</table>
														</TD>
														<TD align=right>
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td><asp:label id="Label8" runat="server" CssClass="ModuleSearchTextSelector">
																			&nbsp;&nbsp;FSM&nbsp;Name:&nbsp;
																		</asp:label></td>
																	<td>
																		<asp:TextBox id="txtFSMName" runat="server" Width="200px" MaxLength="100" CssClass="ModuleSearchSelector"></asp:TextBox>
																	</td>
																</tr>
															</table>
														</TD>
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
											<td><asp:label id="lblNote" runat="server" CssClass="FilterNoteTitle">Note:</asp:label></td>
											<td><asp:label id="lblNoteDesc" runat="server" CssClass="FilterNoteDesc">All&nbsp;criteria&nbsp;is&nbsp;considered&nbsp;when&nbsp;refreshing&nbsp;the&nbsp;list.</asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<BR>
		</TD>
	</TR>
	<TR>
		<TD>
			<asp:label id="lblCurrentIndex" runat="server" Font-Size="xx-small" CssClass="CurrentPageIndexSelectorLabel">Page&nbsp;1&nbsp;of&nbsp;1</asp:label>
		</TD>
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
								CausesValidation="False"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CausesValidation="False"></ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="organization_id" HeaderText="QSP&nbsp;Org&nbsp;ID">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.organization_id") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="organization_name" HeaderText="Organization&nbsp;Name">
						<HeaderStyle Wrap="False" Width="250px"></HeaderStyle>
						<ItemTemplate>
							<asp:HyperLink id="hypLnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.organization_name") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="organization_type_name" SortExpression="organization_type_name" ReadOnly="True"
						HeaderText="Type">
						<ItemStyle Wrap="False" Width="80px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn visible="false" SortExpression="fm_id" HeaderText="FMID">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbFMID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fm_id") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn visible="false" SortExpression="fm_name" HeaderText="FM Name">
						<HeaderStyle Wrap="False" Width="120px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbFMname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fm_name") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="Address1" SortExpression="Address1" ReadOnly="True" HeaderText="Address">
						<ItemStyle Wrap="False" Width="200px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="city" SortExpression="city" ReadOnly="True" HeaderText="City">
						<ItemStyle Wrap="False" Width="70px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="subdivision_code" HeaderText="ST">
						<ItemStyle Wrap="False" Width="40px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.subdivision_code").ToString().Replace("US-","") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>			
					<asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
						<ItemStyle Wrap="False" Width="80px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblZip runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>'>
							</asp:Label>
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
						<asp:label id="lblTotal" runat="server" CssClass="TotalListSelectorLabel">
							Number of Organization(s):
						</asp:label>
					</td>
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
							AlternateText="Click here to confirm your selection"></asp:ImageButton>
					</td>
					<td>
						<asp:HyperLink id="hypLnkCancel" runat="server" ImageUrl="~/images/btnCancel.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

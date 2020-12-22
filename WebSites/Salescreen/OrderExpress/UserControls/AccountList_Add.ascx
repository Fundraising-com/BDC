<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountList_Add" Codebehind="AccountList_Add.ascx.cs" %>

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
														<TD colSpan="2"><asp:label id="lblHeaderFilter" CssClass="StandardLabel" Runat="server">
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
																				<td><asp:label id="Label1" runat="server" CssClass="ModuleSearchText">
																						QSP&nbsp;Program:&nbsp;
																					</asp:label>
																				</td>
																				<TD>
																				    <asp:dropdownlist id="ddlProgramType" runat="server" CssClass="boxes"></asp:dropdownlist>
																				</TD>
																				<td>&nbsp;&nbsp;</td>
																				<td><asp:label id="lblState" runat="server" CssClass="ModuleSearchText">
																						State:&nbsp;
																					</asp:label></td>
																				<td><asp:dropdownlist id="ddlState" runat="server" CssClass="boxes"></asp:dropdownlist></td>
																			
																			</tr>
																		
													                        <tr id="trFieldSupportFilterOption"  runat="server">
												        
																                <td><asp:label id="Label6" runat="server" CssClass="ModuleSearchText">
																		                FSM&nbsp;ID:&nbsp;
																	                </asp:label></td>
																                <td>
																	                <asp:TextBox id="txtFSMID" runat="server" Width="100px" MaxLength="4" CssClass="StandardTextBox"></asp:TextBox>
																                </td>
																                <td>&nbsp;&nbsp;</td>
															                    <td><asp:label id="Label4" runat="server" CssClass="ModuleSearchText">
																                        FSM&nbsp;Name:&nbsp;
															                        </asp:label></td>
														                        <td>
															                        <asp:TextBox id="txtFSMName" runat="server" Width="200px" MaxLength="100" CssClass="StandardTextBox"></asp:TextBox>
														                        </td>
													                        </tr>
												                        </table>
											                        </TD>																	                
												                </tr>
											                </table>
												        </td>
												    </tr>
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
				</TR>
			</table>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click on Column Headings to resort list.</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD width="100%"><!--DataGrid  --><cc2:sorteddatagrid id=dtgAccount runat="server" SearchMode="0" ShowFooter="True" DataSource="<%# DVAccount %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" CssClass=GridStyle CellPadding="3" AllowSorting="True" AllowPaging="True" Width="100%" PageSize="30" onselectedindexchanged="dtgAccount_SelectedIndexChanged">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:imagebutton id="imgBtnSelect" runat="server" ImageUrl="<%# addButton_ImageURL %>" CommandName="Select"
								CausesValidation="False"></asp:imagebutton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False">
						<HeaderStyle Width="10px" HorizontalAlign="Center"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblStatusRead" runat="server" Width="5px" BackColor='<%# System.Drawing.Color.FromName(DataBinder.Eval(Container, "DataItem.Color_Code").ToString()) %>' Height="5px" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
								&nbsp;&nbsp;
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False">
						<HeaderStyle Width="10px" HorizontalAlign="Center"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblIsRenew" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.is_renew")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="account_id" HeaderText="QSP&nbsp;Acct&nbsp;ID&nbsp;#">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.account_id")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fulf_account_id" HeaderText="EDS&nbsp;Acct&nbsp;#">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblAccountNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fulf_account_id", "{0:D9}")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="account_name" HeaderText="Account&nbsp;Name">
						<HeaderStyle Wrap="False" Width="250px"></HeaderStyle>
						<ItemTemplate>
							<asp:HyperLink id="hypLnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.account_name") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fiscal_year" HeaderText="FY" visible="false">
						<ItemTemplate>
							<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fiscal_year") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fm_id" HeaderText="FSM&nbsp;ID&nbsp;#">
						<ItemStyle Wrap="False" Width="50px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblFMID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fm_id") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fm_name" HeaderText="FSM&nbsp;Name">
						<ItemStyle Wrap="False" Width="150px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblFMName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fm_name") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="program_type_name" SortExpression="program_type_name" ReadOnly="True"
						HeaderText="QSP&nbsp;Program">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="city" SortExpression="city" ReadOnly="True" HeaderText="City">
						<ItemStyle Wrap="False" Width="130px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="subdivision_code" HeaderText="ST">
						<ItemStyle Wrap="False" Width="40px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.subdivision_code").ToString().Replace("US-","") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblZip runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					
				</Columns>
			</cc2:sorteddatagrid></TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Account(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

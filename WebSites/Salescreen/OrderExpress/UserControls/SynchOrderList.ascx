<%@ Reference Control="OrderDetailInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.SynchOrderList" Codebehind="SynchOrderList.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>

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
														<td>
															<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																<tr>
																	<td align="center">
																		<table cellSpacing="0" cellPadding="0" border="0">
																			<tr>
																				<td><asp:label id="lblState" runat="server" CssClass="ModuleSearchText">
																						State&nbsp;:&nbsp;
																					</asp:label></td>
																				<td><asp:dropdownlist id="ddlState" runat="server" CssClass="boxes"></asp:dropdownlist></td>
																			</tr>
																		</table>
																	</td>
																	<td>
																		<table cellSpacing="0" cellPadding="0" border="0">
																			<tr>
																				<td>
																					<asp:label id="Label10" runat="server" CssClass="ModuleSearchText">
																						Status&nbsp;Category&nbsp;:&nbsp;
																					</asp:label>
																				</td>
																				<TD>
																					<asp:dropdownlist id="ddlStatusCategory" runat="server"></asp:dropdownlist>
																				</TD>
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
		</TD>
	</TR>
	<TR>
		<TD width="100%"><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgOrder runat="server" SearchMode="0" ShowFooter="True" DataSource="<%# DVOrder %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" Width="100%" PageSize="30" Font-Size="10pt">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:imagebutton id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandName="Create"
								CausesValidation="False"></asp:imagebutton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<HeaderStyle Width="10px" HorizontalAlign="Center"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblStatusRead" runat="server" Width="5px" BackColor='<%# System.Drawing.Color.FromName(DataBinder.Eval(Container, "DataItem.Color_Code").ToString()) %>' Height="5px" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
								&nbsp;&nbsp;
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="order_status_name" SortExpression="order_status_name" ReadOnly="True"
						HeaderText="Status">
						<ItemStyle Width="100px" wrap="false"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="OL#QID" HeaderText="Order&nbsp;ID">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OL#QID")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="OL#ORD" HeaderText="EDS&nbsp;Order&nbsp;#">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OL#ORD")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="OLCUST" HeaderText="EDS&nbsp;Acct&nbsp;#">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblOrderNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OLCUST", "{0:D9}")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="OLNMSH" HeaderText="Account&nbsp;Name">
						<HeaderStyle Wrap="False" Width="250px"></HeaderStyle>
						<ItemTemplate>
							<asp:HyperLink id="hypLnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OLNMSH") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="OL#FSM" HeaderText="FSM&nbsp;ID">
						<ItemStyle Wrap="False" Width="50px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblFMID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OL#FSM") %>'>
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
					<asp:BoundColumn DataField="OLCYSH" SortExpression="OLCYSH" ReadOnly="True" HeaderText="City">
						<ItemStyle Wrap="False" Width="130px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="OLZPSH" HeaderText="Zip">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OLZPSH") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="OLSTSH" SortExpression="OLSTSH" ReadOnly="True" HeaderText="State"></asp:BoundColumn>
					<asp:BoundColumn DataField="OLRCCD" SortExpression="OLRCCD" ReadOnly="True" HeaderText="Op.Type"></asp:BoundColumn>
				</Columns>
			</cc2:sorteddatagrid></TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellpadding="0" border="0">
				<TR>
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Order(s) :
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

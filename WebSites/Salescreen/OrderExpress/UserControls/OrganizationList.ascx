<%@ Reference Control="OrganizationDetailInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrganizationList" Codebehind="OrganizationList.ascx.cs" %>
<meta content="False" name="vs_showGrid">

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
											<TD colSpan="2"><asp:label id="Label3" Runat="server" CssClass="StandardLabel">
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
																	<td><asp:label id="Label4" runat="server" CssClass="ModuleSearchText">
																			Organization&nbsp;Type&nbsp;:&nbsp;
																		</asp:label></td>
																	<TD><asp:dropdownlist id="ddlOrganizationType" runat="server"></asp:dropdownlist></TD>
																</tr>
															</table>
														</TD>
														<td align="right">
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td><asp:label id="Label5" runat="server" CssClass="ModuleSearchText">
																			State&nbsp;:&nbsp;
																		</asp:label></td>
																	<td><asp:dropdownlist id="ddlState" runat="server"></asp:dropdownlist></td>
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
																				<td><asp:label id="Label1" runat="server" CssClass="ModuleSearchText">
																						Display&nbsp;Options:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																					</asp:label></td>
																				<td>
																					<asp:dropdownlist id="ddlFSMDisplayMode" runat="server">
																						<asp:ListItem Value="0" Selected="True">My Accounts Only</asp:ListItem>
																						<asp:ListItem Value="2">My Direct Report(s) Only</asp:ListItem>
																						<asp:ListItem Value="1">My Accounts & My Direct Report(s)</asp:ListItem>																						
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
																				<td><asp:label id="Label6" runat="server" CssClass="ModuleSearchText">
																						FSM&nbsp;ID:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																					</asp:label></td>
																				<td>
																					<asp:TextBox id="txtFSMID" runat="server" Width="100px" MaxLength="4" CssClass="StandardTextBox"></asp:TextBox>
																				</td>
																			</tr>
																		</table>
																	</TD>
																	<TD align=right>
																		<table cellSpacing="0" cellPadding="0" border="0">
																			<tr>
																				<td><asp:label id="Label8" runat="server" CssClass="ModuleSearchText">
																						&nbsp;&nbsp;FSM&nbsp;Name:&nbsp;
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
												</table>
											</td>
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
					</TD>
				</TR>
			</table>
		</TD>
	</TR>
	<tr>
		<td align="left"><br>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page&nbsp;1&nbsp;of&nbsp;1&nbsp;</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click&nbsp;on&nbsp;Column&nbsp;Headings&nbsp;to&nbsp;Resort&nbsp;Data.&nbsp;</asp:label></td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<TR>
		<TD width="100%"><!--DataGrid  -->
			<cc2:SortedDataGrid id=dtgOrganization runat="server" PageSize="30" Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="3" CssClass=GridStyle BorderColor="#CCCCCC" AutoGenerateColumns="False" ShowFooter="True" SearchMode="0" AllowCustomPaging="True">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" height=15px ImageUrl="~/images/BtnDetail.gif" CausesValidation="False"></ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="OrganizationId" SortExpression="OrganizationId" ReadOnly="True" HeaderText="QSP&nbsp;Org&nbsp;ID"></asp:BoundColumn>
					<asp:TemplateColumn SortExpression="OrganizationName" HeaderText="Organization&nbsp;Name">
						<ItemStyle Wrap="False" Width="300px"></ItemStyle>
						<ItemTemplate>
							<ASP:LINKBUTTON id=lnkBtnOrganization runat="server" CommandName="Select" CausesValidation="False" Text='<%# Eval("OrganizationName") %>'>
							</ASP:LINKBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="false" SortExpression="FmId" HeaderText="FM ID">
						<ItemStyle Wrap="False" Width="50px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblFMID runat="server" Text='<%# Eval("FmId") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="OrganizationTypeName" HeaderText="Type">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblOrgType runat="server" Text='<%# Eval("OrganizationTypeName") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="Address1" SortExpression="Address1" ReadOnly="True" HeaderText="Address">
						<ItemStyle Wrap="False" Width="200px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="City" SortExpression="City" ReadOnly="True" HeaderText="City">
						<ItemStyle Wrap="False" Width="120px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="SubdivisionCode" HeaderText="ST">
						<ItemStyle Wrap="False" Width="40px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label7" runat="server" Text='<%# Eval("DisplaySubdivisionCode") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>			
					<asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
						<ItemStyle Wrap="False" Width="90px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblZip runat="server" Text='<%# Eval("Zip") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
							
				</Columns>
			</cc2:SortedDataGrid>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Organization(s):
						</asp:label>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

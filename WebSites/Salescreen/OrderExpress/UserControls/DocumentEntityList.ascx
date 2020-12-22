<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.DocumentEntityList" Codebehind="DocumentEntityList.ascx.cs" %>
<meta name="vs_showGrid" content="False">

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<table cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="80%" border="0">
							<TR>
								<TD>
									<uc1:SearchModule id="QSPFormSearchModule" runat="server" MaxLengthValidate="0"></uc1:SearchModule>
								</TD>
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
														<TD>
															<table border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td>
																		<asp:label id="Label4" runat="server" CssClass="ModuleSearchText">
																			Document&nbsp;Type&nbsp;:&nbsp;
																		</asp:label>
																	</td>
																	<TD>
																		<asp:dropdownlist id="ddlDocumentType" runat="server"></asp:dropdownlist>
																	</TD>
																</tr>
															</table>
														</TD>
														<td align="center">
															<table border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td>
																		<asp:label id="Label5" runat="server" CssClass="ModuleSearchText">
																			Status&nbsp;:&nbsp;
																		</asp:label>
																	</td>
																	<td>
																		<asp:dropdownlist id="ddlStatus" runat="server">
																			<asp:ListItem Selected="True" Value="0">---SELECT---</asp:ListItem>
																			<asp:ListItem Value="1">Not Received</asp:ListItem>
																			<asp:ListItem Value="2">Received</asp:ListItem>
																		</asp:dropdownlist>
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
			</table>
		</TD>
	</TR>
	<tr>
		<td align="left">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click on Column Headings to Resort Data.</asp:label></td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<TR>
		<TD width="100%"><!--DataGrid  -->
			<cc2:SortedDataGrid id=dtgDocument runat="server" PageSize="30" Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="3" CssClass=GridStyle BorderColor="#CCCCCC" AutoGenerateColumns="False" DataSource="<%# DVDocument %>" ShowFooter="True" SearchMode="0">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn Visible="False">
						<ItemTemplate>
							<asp:label runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.entity_type_id")%>' ID="lblEntityTypeID"/>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/btnReceived.gif" CausesValidation="False"
								CommandName="RECEIVED"></ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="account_id" SortExpression="account_id" ReadOnly="True" HeaderText="QSP&nbsp;ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="fulf_account_id" SortExpression="fulf_account_id" ReadOnly="True" HeaderText="EDS&nbsp;Acct&nbsp;#"></asp:BoundColumn>
					<asp:TemplateColumn SortExpression="account_name" HeaderText="Account&nbsp;Name">
						<ItemStyle Wrap="False" Width="300px"></ItemStyle>
						<ItemTemplate>
							<ASP:LINKBUTTON CommandName="RECEIVED" id=lnkBtnDocument runat="server" CausesValidation="False" Text='<%# DataBinder.Eval(Container, "DataItem.account_name") %>'>
							</ASP:LINKBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="document_type_name" HeaderText="Type">
						<ItemStyle Wrap="False" Width="200px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblDocumentTypeName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.document_type_name") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="approved" HeaderText="Received">
						<HeaderStyle Width="30px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<ASP:CheckBox id="chkReceived" runat="server" Enabled=False CssClass="TrueCheckBox" Checked='<%#  DataBinder.Eval(Container, "DataItem.approved").ToString() != "" ? DataBinder.Eval(Container, "DataItem.approved") : false  %>'>
							</ASP:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="approve_user_name" HeaderText="Received&nbsp;by">
						<ItemTemplate>
							<ASP:LABEL id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.approved_user_name") %>'>
							</ASP:LABEL>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="received_date" HeaderText="Received&#160;Date">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.approved_date", "{0:MM/dd/yyyy}") %>'>
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
							Number of Document(s):
						</asp:label>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

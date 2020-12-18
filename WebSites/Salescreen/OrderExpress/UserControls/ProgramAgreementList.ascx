<%@ Reference Control="ProgramAgreementDetailInfo.ascx" %>
<%@ Reference Control="~/UserControls/SearchModule.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramAgreementList" Codebehind="ProgramAgreementList.ascx.cs" %>
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
														<TD colSpan="2">
														    <asp:label id="lblHeaderFilter" Runat="server" CssClass="StandardLabel">
																Filter By:
															</asp:label>
														</TD>
													</TR>
													<TR>
														<td>
															<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																<tr>
																	<TD>
																		<table cellSpacing="0" cellPadding="0" border="0">
																			<tr>
																				<td width=100px nowrap><asp:label id="Label1" runat="server" CssClass="ModuleSearchText">
																						QSP&nbsp;Program:&nbsp;
																					</asp:label>
																				</td>
																				<TD>
																				    <asp:dropdownlist id="ddlProgramType" runat="server" CssClass="boxes"></asp:dropdownlist></TD>
																			</tr>
																		</table>
																	</TD>
																	<td>
																		<table cellSpacing="0" cellPadding="0" border="0">
																			<tr>
																				<td width=100px nowrap><asp:label id="lblState" runat="server" CssClass="ModuleSearchText">
																						State:&nbsp;
																					</asp:label></td>
																				<td align=left><asp:dropdownlist id="ddlState" runat="server" CssClass="boxes"></asp:dropdownlist></td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td>
																		<table  id="tblFilterStatusCategory" runat="server" cellSpacing="0" cellPadding="0" border="0">
																			<tr>
																				<td width=100px nowrap>
																					<asp:label id="Label10" runat="server" CssClass="ModuleSearchText">
																						Status&nbsp;Category:&nbsp;
																					</asp:label>
																				</td>
																				<TD>
																					<asp:dropdownlist id="ddlStatusCategory" runat="server"></asp:dropdownlist>
																				</TD>
																			</tr>
																		</table>
																	</td>
																	<TD >
																		<table id="tblFMFilterOption" runat="server" cellSpacing="0" cellPadding="0" border="0">
																			<tr>
																				<td width=100px nowrap><asp:label id="Label3" runat="server" CssClass="ModuleSearchText">
																						Display&nbsp;Options:&nbsp;
																					</asp:label></td>
																				<td>
																					<asp:dropdownlist id="ddlFSMDisplayMode" runat="server">
																						<asp:ListItem Value="0" Selected="True">My Accounts Only</asp:ListItem>
																						<asp:ListItem Value="2">My Direct Report(s) Only</asp:ListItem>
																						<asp:ListItem Value="1">My Accounts & My Direct Reports</asp:ListItem>																						
																					</asp:dropdownlist>
																				</td>
																			</tr>
																		</table>
																	</TD>
																</tr>
																<tr id="trFieldSupportFilterOption"  runat="server">
																    <td>
																        <table cellSpacing="0" cellPadding="0" border="0" width="100%">
																            <tr>
																	            <TD>
																		            <table cellSpacing="0" cellPadding="0" border="0">
																			            <tr>
																				            <td width=100px nowrap><asp:label id="Label6" runat="server" CssClass="ModuleSearchText">
																						            FSM&nbsp;ID:&nbsp;
																					            </asp:label></td>
																				            <td>
																					            <asp:TextBox id="txtFSMID" runat="server" Width="100px" MaxLength="4" CssClass=StandardTextBox></asp:TextBox>
																				            </td>
																			            </tr>
																		            </table>
																	            </TD>
																	        </tr>
																	    </table>
																	</td>
																	<td>
														                <table cellSpacing="0" cellPadding="0" border="0">
															                <tr>    
															                    <TD align=left>
																                    <table cellSpacing="0" cellPadding="0" border="0">
																	                    <tr>
																		                    <td width=100px nowrap><asp:label id="Label4" runat="server" CssClass="ModuleSearchText">
																				                    FSM&nbsp;Name:&nbsp;
																			                    </asp:label></td>
																		                    <td>
																			                    <asp:TextBox id="txtFSMName" runat="server" Width="200px" MaxLength="100" CssClass=StandardTextBox></asp:TextBox>
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
					<td>
					</td>
				</TR>
				<tr>
				    <td height=5px>
					</td>
				</tr>
			</table>			
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<tr>
					<td align="left" colspan="2">
						<asp:imagebutton id="imgbtnAddProgramAgreement" runat="server" AlternateText="Click here to Add new Program Agreement !"
							ImageUrl="~/images/btnAddPA.gif"></asp:imagebutton><br>
						
					</td>
				</tr>
				<tr>
				    <td height=5px>
					</td>
				</tr>
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page&nbsp;1&nbsp;of&nbsp;1</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click&nbsp;on&nbsp;Column&nbsp;Headings&nbsp;to&nbsp;Resort&nbsp;Data.&nbsp;</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD width="100%"><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgProgramAgreement runat="server" SearchMode="0" ShowFooter="True" DataSource="<%# DVProgramAgreement %>" AutoGenerateColumns="False"  CssClass=GridStyle BorderColor="#CCCCCC" CellPadding="3" AllowSorting="True" AllowPaging="True" Width="100%" PageSize="30" >
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off" Wrap="False"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle" Font-Size="10px" Wrap="False"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle" Wrap="False"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off" Font-Size="11px" Wrap="False"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:imagebutton id="imgBtnDetail" runat="server" height="15px" ImageUrl="~/images/BtnDetail.gif" CommandName="Create"
								CausesValidation="False"></asp:imagebutton>
						
</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:Label id="lblStatusRead" runat="server" BackColor='<%# System.Drawing.Color.FromName(DataBinder.Eval(Container, "DataItem.Color_Code").ToString()) %>' BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="StatusLabel">
								&nbsp;&nbsp;
							</asp:Label>
						
</ItemTemplate>
						<HeaderStyle Width="10px" HorizontalAlign="Center"></HeaderStyle>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="program_agreement_status_short_description" SortExpression="program_agreement_status_short_description"
						ReadOnly="True" HeaderText="Status">
						<ItemStyle Width="100px" wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="program_agreement_id" HeaderText="QSP&nbsp;PA&nbsp;ID&nbsp;#">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.program_agreement_id")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fulf_program_agreement_id" HeaderText="EDS&nbsp;PA&nbsp;#">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblProgramAgreementNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fulf_program_agreement_id")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="account_id" HeaderText="QSP&nbsp;Acct&nbsp;ID&nbsp;#">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblAccountID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.account_id")%>'>
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
					<asp:TemplateColumn SortExpression="program_agreement_name" HeaderText="Account&nbsp;Name">
						<HeaderStyle Wrap="False" Width="250px"></HeaderStyle>
						<ItemTemplate>
							<asp:HyperLink id="hypLnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.program_agreement_name") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
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
						<ItemStyle Wrap="False" Width="90px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblZip runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn SortExpression="create_last_name, create_first_name" HeaderText="Create&#160;By">
						<ItemTemplate>
							<asp:Label id="lblCreatorName" runat="server" Text=''>
							</asp:Label>
						
</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="create_date" HeaderText="Create&#160;At">
						<ItemStyle Wrap="False" Width="120px" ></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblCreateDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.create_date", "{0:MM/dd/yyyy hh:mm}") %>'>
							</asp:Label>
						
</ItemTemplate>
					</asp:TemplateColumn>
					
				</Columns>
                <EditItemStyle Wrap="False" />
			</cc2:sorteddatagrid>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellpadding="0" border="0">
				<TR>
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Program Agreement(s):
						</asp:label>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

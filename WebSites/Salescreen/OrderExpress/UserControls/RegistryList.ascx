<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.RegistryList" Codebehind="RegistryList.ascx.cs" %>

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
																				<td><asp:label id="lblExcludeRole" runat="server" CssClass="ModuleSearchText">
																						Display Only Users who have Role below :&nbsp;&nbsp;&nbsp;
																					</asp:label></td>
																				<td>
																					<asp:dropdownlist id="ddlExcludeRole" runat="server" CssClass="boxes">
																						<asp:ListItem Selected=True Value=0>--SELECT--</asp:ListItem>
																						<asp:ListItem Value=4>Administrator</asp:ListItem>
																						<asp:ListItem Value=5>Super User</asp:ListItem>
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
													<TR>
														<td colSpan="2">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																<tr>
																	<TD>
																		<asp:label id="Label7" runat="server" CssClass="ModuleSearchText">
																			Start&nbsp;Date&nbsp;:
																		</asp:label>
																	</TD>
																	<td>
																		<table border="0" cellpadding="0" cellspacing="0">
																			<tr>
																				<TD>
																					<asp:textbox id="txtStartDate" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:textbox>
																					<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />																	</TD>
																				<td>
																					<asp:HyperLink id="hypLnkStartDate" runat="server" NavigateUrl="javascript:void(0);" ImageUrl="~/images/Calendar.gif"
																						ToolTip="Click here to select the start date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
																				</td>
																				<td>
																					<asp:RequiredFieldValidator id="reqFldVal_StartDate" CssClass="LabelError" runat="server" ErrorMessage="The Start Date is required."
																						ControlToValidate="txtStartDate">*</asp:RequiredFieldValidator>
																					<asp:CompareValidator id="compVal_StartDate" runat="server" CssClass="LabelError" ErrorMessage="The Start Date is invalid."
																						ControlToValidate="txtStartDate" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
																					<asp:CompareValidator id="Comparevalidator1" runat="server" CssClass="LabelError" ErrorMessage="The Start Date must be less or equal than the End Date."
																						ControlToValidate="txtStartDate" Operator="LessThanEqual" Type="Date" ControlToCompare="txtEndDate">*</asp:CompareValidator>
																				</td>
																			</tr>
																		</table>
																	</td>
																	<td>
																		&nbsp;
																	</td>
																	<TD>
																		<asp:label id="Label1" runat="server" CssClass="ModuleSearchText">
																			End&nbsp;Date&nbsp;:
																		</asp:label>
																	</TD>
																	<td>
																		<table border="0" cellpadding="0" cellspacing="0">
																			<tr>
																				<TD>
																					<asp:textbox id="txtEndDate" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:textbox>
																					<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEndDate"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  CultureName="en-US"/>
																				</TD>
																				<td>
																					<asp:HyperLink id="hypLnkEndDate" runat="server" NavigateUrl="javascript:void(0);" ImageUrl="~/images/Calendar.gif"
																						ToolTip="Click here to select the end date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
																				</td>
																				<td>
																					<asp:RequiredFieldValidator id="reqFldVal_EndDate" CssClass="LabelError" runat="server" ErrorMessage="The End Date is required."
																						ControlToValidate="txtEndDate">*</asp:RequiredFieldValidator>
																					<asp:CompareValidator id="compVal_EndDate" runat="server" CssClass="LabelError" ErrorMessage="The Start Date is invalid."
																						ControlToValidate="txtEndDate" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
																				</td>
																			</tr>
																		</table>
																	</td>
																	<td></td>
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
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click on Column Headings to Resort Data.</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD width="100%"><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgRegistry runat="server" SearchMode="0" ShowFooter="True" DataSource="<%# DVRegistry %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" Width="100%" PageSize="30" Font-Size="10pt">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle" Wrap="False"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle" Wrap="False"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off" Font-Size="12px" Wrap="False"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn >
						<ItemTemplate>
							<asp:imagebutton id="imgBtnDetail" runat="server" visible =false ImageUrl="~/images/BtnDetail.gif" CommandName="Detail"
								CausesValidation="False"></asp:imagebutton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="registry_id" HeaderText="ID">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblRegistryID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.registry_id")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="user_name" HeaderText="User&nbsp;Name">
						<HeaderStyle Width="150px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblUserName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.user_name")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="complete_user_name" HeaderText="Complete&nbsp;Name">
						<HeaderStyle Width="250px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.complete_user_name")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="fm_id" SortExpression="fm_id" ReadOnly="True" HeaderText="FSM&nbsp;ID">
						<ItemStyle Wrap="False" Width="130px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="login_datetime" HeaderText="Login&#160;Date&#160;Time">
						<ItemStyle Wrap="False" Width="200px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.login_datetime") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
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
							Number of Registry Line(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CampaignList" Codebehind="CampaignList.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD><uc1:searchmodule id="QSPFormSearchModule" CausesValidation="True" runat="server" MaxLengthValidate="0"></uc1:searchmodule></TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD><asp:label id="lblNote" runat="server" ForeColor="#053868" font-size="11px" Font-Names="Verdana"
							Font-Bold="True">Note : All Search Criterias, Filters are considered when refreshing</asp:label>
						<TABLE class="ModuleSearchTable" id="tblFilter" cellSpacing="1" cellPadding="0" width="100%"
							border="1" runat="server">
							<TR>
								<TD>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR>
											<TD bgColor="#003366" colSpan="2"><FONT color="#ffffff" size="1"><STRONG><asp:label id="lblHeaderFilter" Runat="server">Filter</asp:label></STRONG></FONT></TD>
										</TR>
										<TR>
											<td>
												<table border="0" cellpadding="0" cellspacing="0" width="100%">
													<tr>
														<TD>
															<asp:label id="Label6" runat="server" CssClass="ModuleSearchText">
																Fiscal&nbsp;Year&nbsp;:
															</asp:label>
														</TD>
														<TD noWrap>
															<asp:dropdownlist id="ddlFiscalYear" runat="server" CssClass="boxes"></asp:dropdownlist>
														</TD>
														<td>
															&nbsp;
														</td>
														<td>
															<asp:label id="Label5" runat="server" CssClass="ModuleSearchText">
																Program&nbsp;Type&nbsp;:
															</asp:label>
														</td>
														<TD noWrap>
															<asp:dropdownlist id="ddlProgramType" runat="server" CssClass="boxes"></asp:dropdownlist>
														</TD>
														<td>
															&nbsp;
														</td>
														<td>
															<asp:label id="LblState" runat="server" CssClass="ModuleSearchText">
																State :
															</asp:label>
														</td>
														<TD noWrap>
															<asp:dropdownlist id="ddlState" runat="server" CssClass="boxes"></asp:dropdownlist>
														</TD>
													</tr>
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
																		<asp:textbox id="txtStartDate" runat="server" Width="100px" CssClass="boxes"></asp:textbox>
																	</TD>
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
															<asp:label id="Label8" runat="server" CssClass="ModuleSearchText">
																End&nbsp;Date&nbsp;:
															</asp:label>
														</TD>
														<td>
															<table border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<TD>
																		<asp:textbox id="txtEndDate" runat="server" Width="100px" CssClass="boxes"></asp:textbox>
																	</TD>
																	<td>
																		<asp:RequiredFieldValidator id="reqFldVal_EndDate" CssClass="LabelError" runat="server" ErrorMessage="The End Date is required."
																			ControlToValidate="txtEndDate">*</asp:RequiredFieldValidator>
																		<asp:CompareValidator id="compVal_EndDate" runat="server" CssClass="LabelError" ErrorMessage="The Start Date is invalid."
																			ControlToValidate="txtEndDate" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
																	</td>
																</tr>
															</table>
														</td>
														<td>
														</td>
														<td>
														</td>
														<td>
														</td>
													</tr>
												</table>
											</td>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
			<BR>
			<asp:label id="lblCurrentIndex" runat="server" CssClass="eRewardsInstr">Page 1 of 1</asp:label></TD>
	</TR>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgCamp runat="server" Font-Size="10pt" ShowFooter="True" DataSource="<%# DVCampaigns %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="30">
				<PagerStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="#003366"
					Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle ForeColor="Black" BackColor="#F5F5F5"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="Black" BackColor="#CCCCCC"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#003366"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Campaign_ID") %>' CommandName="Select" CausesValidation="False">
							</ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>					
					<asp:BoundColumn DataField="campaign_id" SortExpression="campaign_id" ReadOnly="True" HeaderText="ID"></asp:BoundColumn>
					<asp:TemplateColumn SortExpression="Campaign_Name" HeaderText="Campaign Name">
						<ItemStyle Wrap="False" Width="250px"></ItemStyle>
						<ItemTemplate>
							<ASP:LINKBUTTON id="lnkBtnCampaign" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Campaign_Name") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Campaign_ID") %>' CommandName="Select">
							</ASP:LINKBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="account_id" SortExpression="account_id" ReadOnly="True" HeaderText="Account&nbsp;ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="fm_id" SortExpression="fm_id" ReadOnly="True" HeaderText="FM&#160;No"></asp:BoundColumn>
					<asp:TemplateColumn SortExpression="FM_Name" HeaderText="FM Name">
						<ItemStyle Wrap="False" Width="150px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FM_Name") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="start_date" HeaderText="Start&#160;Date">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.start_date", "{0:MM/dd/yyyy}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="end_date" HeaderText="End&#160;Date">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.end_date", "{0:MM/dd/yyyy}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="fiscal_year" SortExpression="fiscal_year" ReadOnly="True" HeaderText="FY">
						<ItemStyle Wrap="False" Width="40px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="program_type_name" HeaderText="Program&#160;Type">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.program_type_name").ToString().Replace(" ", "&nbsp;") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>					
					<asp:BoundColumn DataField="city" SortExpression="city" ReadOnly="True" HeaderText="City">
						<ItemStyle Wrap="False" Width="130px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Zip" SortExpression="Zip" ReadOnly="True" HeaderText="Zip"></asp:BoundColumn>
					<asp:TemplateColumn SortExpression="subdivision_name_1" HeaderText="State">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.subdivision_name_1") %>'>
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
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" ForeColor="#993300" Font-Bold="true" Font-Size="X-Small"
							Font-Name="Verdana">
							Number of Campaign(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

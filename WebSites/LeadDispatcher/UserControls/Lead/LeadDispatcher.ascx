<%@ Register TagPrefix="uc2" TagName="PaymentAdjusment_Header" Src="../PaymentAdjusment_Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MyCalendar" Src="../MyCalendar.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LeadDispatcher.ascx.cs" Inherits="CRMWeb.UserControls.Lead.LeadDispatcher" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<P>
	<TABLE id="Table1" style="WIDTH: 786px; HEIGHT: 662px" cellSpacing="0" cellPadding="0"
		width="786" border="0">
		<TR>
			<TD style="HEIGHT: 19px"></TD>
			<TD style="HEIGHT: 19px"></TD>
			<TD style="WIDTH: 12px; HEIGHT: 19px"></TD>
			<TD style="WIDTH: 12px; HEIGHT: 19px"></TD>
		</TR>
		<TR>
			<TD style="HEIGHT: 12px">&nbsp;&nbsp;
			</TD>
			<TD style="HEIGHT: 12px"><asp:label id="Label6" runat="server" Width="152px" ForeColor="#6695C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
					Font-Size="8pt" Height="13px">Select a Promotion Group:</asp:label><asp:dropdownlist id="cboPromoGroup" runat="server" Width="128px" AutoPostBack="True" onselectedindexchanged="cboPromoGroup_SelectedIndexChanged"></asp:dropdownlist></TD>
			<TD style="WIDTH: 12px; HEIGHT: 12px"></TD>
			<TD style="WIDTH: 12px; HEIGHT: 12px"></TD>
		</TR>
		<TR>
			<TD style="HEIGHT: 332px" vAlign="top" align="left">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
			<TD style="HEIGHT: 332px" vAlign="top" align="left">
				<DIV>
					<DIV>
						<DIV>&nbsp;</DIV>
					</DIV>
				</DIV>
				<DIV>
					<TABLE id="Table2" style="WIDTH: 696px; HEIGHT: 301px" cellSpacing="0" cellPadding="0"
						width="696" border="0">
						<TR>
							<TD style="HEIGHT: 228px">
								<DIV>
									<DIV><asp:label id="lblNbUnassigned" Font-Size="11pt" Font-Names="Microsoft Sans Serif" Font-Bold="True"
											ForeColor="#294585" Width="656px" runat="server" BackColor="#F7F7F7"> Unassigned Leads</asp:label>
										<TABLE id="Table7" style="WIDTH: 744px; HEIGHT: 190px" cellSpacing="0" cellPadding="0"
											width="744" border="0">
											<TR>
												<TD style="WIDTH: 530px; HEIGHT: 189px">
													<DIV id="divUnassigned" style="OVERFLOW: auto; WIDTH: 664px; HEIGHT: 176px" align="left"
														runat="server">
														<DIV>
															<DIV>
																<DIV><asp:datagrid id="dgUnassigned" Font-Size="11pt" Width="1175px" runat="server" BackColor="#F7F7F7"
																		PageSize="50" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#6695C3"
																		BorderWidth="2px" HorizontalAlign="Left">
																		<AlternatingItemStyle BackColor="#DEDEE7"></AlternatingItemStyle>
																		<HeaderStyle Font-Size="8.5pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" HorizontalAlign="Left"
																			ForeColor="ControlLightLight" BackColor="#6795C3"></HeaderStyle>
																		<Columns>
																			<asp:BoundColumn Visible="False" DataField="country_code"></asp:BoundColumn>
																			<asp:BoundColumn Visible="False" DataField="fk_kit_type_id"></asp:BoundColumn>
																			<asp:TemplateColumn>
																				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="2%"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Center"></ItemStyle>
																				<ItemTemplate>
																					<center>
																						<asp:CheckBox id="Select" runat="server" />
																					</center>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:BoundColumn Visible="False" DataField="lead_id"></asp:BoundColumn>
																			<asp:ButtonColumn Text="Lead ID" DataTextField="lead_id" SortExpression="lead_id asc" HeaderText="Lead_ID">
																				<HeaderStyle Width="6%"></HeaderStyle>
																				<ItemStyle ForeColor="Black"></ItemStyle>
																			</asp:ButtonColumn>
																			<asp:TemplateColumn Visible="False" HeaderText="Kit To Send">
																				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="10%"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Center"></ItemStyle>
																				<ItemTemplate>
																					<center>
																						<asp:DropDownList id="cboKit" runat="server" Width="115px" Font-Size="9pt" BackColor="#F7F7F7" />
																					</center>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:BoundColumn Visible="False" DataField="ext_consultant" SortExpression="ext_consultant" HeaderText="FM">
																				<HeaderStyle Width="11%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="comment" SortExpression="Comment" HeaderText="Comment">
																				<HeaderStyle Width="3%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="channel" SortExpression="channel" HeaderText="Channel">
																				<HeaderStyle Width="5%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="state_code" SortExpression="state_code" HeaderText="State">
																				<HeaderStyle Width="7%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="part" SortExpression="part" HeaderText="Part">
																				<HeaderStyle Width="1%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn Visible="False" DataField="country_code" SortExpression="country_code" HeaderText="Country"></asp:BoundColumn>
																			<asp:BoundColumn DataField="promotion" SortExpression="promotion" HeaderText="Promotion">
																				<HeaderStyle Width="12%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="promotion_type_code" SortExpression="promo_type" HeaderText="Type">
																				<HeaderStyle Width="3%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="day_phone" HeaderText="Day_Phone">
																				<HeaderStyle Width="9%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="evening_phone" HeaderText="Eve_Phone">
																				<HeaderStyle Width="9%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="group_type" SortExpression="group_type" HeaderText="Group_Type">
																				<HeaderStyle Width="12%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="organization_type_desc" SortExpression="organization_type_desc" HeaderText="Organization">
																				<HeaderStyle Width="17%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="status" SortExpression="status" HeaderText="Status">
																				<HeaderStyle Width="5%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="lead_entry_date" SortExpression="lead_entry_date" HeaderText="Entry Date"
																				DataFormatString="{0:MM/dd/yyyy}">
																				<HeaderStyle Width="7%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn Visible="False" DataField="promo_group_id"></asp:BoundColumn>
																		</Columns>
																		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
																	</asp:datagrid></DIV>
															</DIV>
														</DIV>
														<asp:label id="lblNoLeadsUnassigned" Font-Bold="True" ForeColor="IndianRed" Width="120px" runat="server"
															Visible="False">No Leads Found</asp:label></DIV>
													<asp:label id="Label9" runat="server" Width="80px" ForeColor="DimGray" Font-Bold="True" Font-Names="Microsoft Sans Serif"
														Font-Size="7pt" Height="12px">Limit Per Page:</asp:label>
													<asp:dropdownlist id="cboLimitPerPage" runat="server" Width="64px" Font-Size="7pt" AutoPostBack="True"
														BackColor="#F7F7F7" onselectedindexchanged="cboLimitPerPage_SelectedIndexChanged">
														<asp:ListItem Value="10">10</asp:ListItem>
														<asp:ListItem Value="25">25</asp:ListItem>
														<asp:ListItem Value="50" Selected="True">50</asp:ListItem>
														<asp:ListItem Value="0">No Limit</asp:ListItem>
													</asp:dropdownlist>&nbsp;
													<asp:label id="Label2" runat="server" Width="56px" ForeColor="DimGray" Font-Bold="True" Font-Names="Microsoft Sans Serif"
														Font-Size="7pt" Height="4px">kit For All:</asp:label>&nbsp;
													<asp:dropdownlist id="cboKitForAll" runat="server" Width="104px" Font-Size="7pt" AutoPostBack="True"
														BackColor="#F7F7F7" onselectedindexchanged="cboKitForAll_SelectedIndexChanged">
														<asp:ListItem Value="10">10</asp:ListItem>
														<asp:ListItem Value="25">25</asp:ListItem>
														<asp:ListItem Value="50" Selected="True">50</asp:ListItem>
														<asp:ListItem Value="0">No Limit</asp:ListItem>
													</asp:dropdownlist>&nbsp; &nbsp;
													<asp:linkbutton id="lnkRefresh" Font-Size="7pt" Font-Names="Microsoft Sans Serif" Font-Bold="True"
														ForeColor="#6695C3" runat="server" onclick="LinkButton1_Click">Refresh Data</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
												<TD style="WIDTH: 7px; HEIGHT: 189px"></TD>
												<TD vAlign="top" style="HEIGHT: 189px">
													<DIV><asp:linkbutton id="lnkExpand" Font-Size="9pt" Font-Names="Lucida Console" Font-Bold="True" ForeColor="#6695C3"
															Width="40px" runat="server" onclick="lnkExpand_Click">--></asp:linkbutton></DIV>
													<DIV><asp:linkbutton id="lnkUnExpand" Font-Size="9pt" Font-Names="Lucida Console" Font-Bold="True" ForeColor="#6695C3"
															Width="40px" runat="server" Visible="False" onclick="lnkUnExpand_Click"><--</asp:linkbutton>&nbsp;</DIV>
													<DIV>&nbsp;</DIV>
													<asp:textbox id="txtStartDateUnassigned" Width="78px" runat="server" BorderColor="#6695C3" BorderWidth="1px"
														Visible="False" BorderStyle="Solid"></asp:textbox><asp:imagebutton id="cmdCalEnd" runat="server" Visible="False" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton><uc1:mycalendar id="MyCalendar" runat="server" Visible="False"></uc1:mycalendar><asp:textbox id="txtEndDateUnassigned" Width="78px" runat="server" BorderColor="#6695C3" BorderWidth="1px"
														Visible="False" BorderStyle="Solid"></asp:textbox><asp:imagebutton id="cmdCalStart" runat="server" Visible="False" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton><asp:imagebutton id="cmdFilterUnassigned" runat="server" Visible="False" ImageUrl="../../images/Search.gif"></asp:imagebutton></TD>
											</TR>
										</TABLE>
									</DIV>
								</DIV>
							</TD>
							<TD style="HEIGHT: 228px"></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 179px">
								<TABLE id="Table4" style="WIDTH: 688px; HEIGHT: 244px" cellSpacing="1" cellPadding="1"
									width="688" border="0">
									<TR>
										<TD style="WIDTH: 543px; HEIGHT: 73px" vAlign="middle">
											<TABLE id="Table3" style="WIDTH: 520px; HEIGHT: 48px" cellSpacing="0" cellPadding="0" width="520"
												border="0">
												<TR>
													<TD style="WIDTH: 222px; HEIGHT: 43px"></TD>
													<TD style="WIDTH: 39px; HEIGHT: 43px" vAlign="top"><asp:imagebutton id="cmdUnassign" runat="server" ImageUrl="../../images/arrow_up.gif"></asp:imagebutton></TD>
													<TD style="HEIGHT: 43px" vAlign="top"><asp:imagebutton id="cmdAssign" runat="server" ImageUrl="../../images/arrow_down2.gif"></asp:imagebutton>&nbsp;&nbsp;</TD>
												</TR>
											</TABLE>
											<asp:label id="lblError" ForeColor="Red" runat="server" Visible="False">**You must select a consultant</asp:label></TD>
										<TD style="HEIGHT: 73px"></TD>
										<TD style="HEIGHT: 73px"></TD>
										<TD style="HEIGHT: 73px"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 543px; HEIGHT: 17px" vAlign="bottom" bgColor="#f7f7f7"><asp:label id="Label5" Font-Size="11pt" Font-Names="Microsoft Sans Serif" Font-Bold="True"
												ForeColor="#294585" Width="86px" runat="server" BackColor="WhiteSmoke">Leads For </asp:label><asp:dropdownlist id="cboConsultants" Font-Size="10pt" Font-Names="Microsoft Sans Serif" Font-Bold="True"
												ForeColor="#294585" Width="176px" runat="server" AutoPostBack="True" BackColor="WhiteSmoke" onselectedindexchanged="cboConsultants_SelectedIndexChanged"></asp:dropdownlist>&nbsp;
											<asp:label id="lblNbLeads" Font-Size="11pt" Font-Names="Microsoft Sans Serif" Font-Bold="True"
												ForeColor="#294585" Width="142px" runat="server" BackColor="WhiteSmoke"></asp:label></TD>
										<TD style="HEIGHT: 17px"></TD>
										<TD style="HEIGHT: 17px"></TD>
										<TD style="HEIGHT: 17px"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 543px" vAlign="top">
											<DIV style="OVERFLOW: auto; WIDTH: 536px; HEIGHT: 172px" align="left">
												<DIV>
													<DIV><asp:datagrid id="dgAssigned" Font-Size="11pt" Width="864px" runat="server" BackColor="#F7F7F7"
															AllowSorting="True" AutoGenerateColumns="False" BorderColor="#6695C3" BorderWidth="2px">
															<AlternatingItemStyle BackColor="#DEDEE7"></AlternatingItemStyle>
															<HeaderStyle Font-Size="8.5pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="ControlLightLight"
																BackColor="#6795C3"></HeaderStyle>
															<Columns>
																<asp:TemplateColumn>
																	<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	<ItemTemplate>
																		<center>
																			<asp:CheckBox id="Select2" runat="server" />
																		</center>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:BoundColumn Visible="False" DataField="lead_id"></asp:BoundColumn>
																<asp:ButtonColumn Text="Lead ID" DataTextField="lead_id" SortExpression="lead_id asc" HeaderText="Lead_ID">
																	<HeaderStyle Width="6%"></HeaderStyle>
																	<ItemStyle ForeColor="Black"></ItemStyle>
																</asp:ButtonColumn>
																<asp:BoundColumn DataField="called" SortExpression="called" HeaderText="Called?">
																	<HeaderStyle Width="5%"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="lead_entry_date" SortExpression="lead_entry_date" HeaderText="Entry Date"
																	DataFormatString="{0:MM/dd/yyyy}">
																	<HeaderStyle Width="10%"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="assignment_date" SortExpression="assignment_date" HeaderText="Assign. Date"
																	DataFormatString="{0:MM/dd/yyyy}">
																	<HeaderStyle Width="12%"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="organization" SortExpression="organization" HeaderText="Organization">
																	<HeaderStyle Width="19%"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="promo" SortExpression="promo" HeaderText="Promotion">
																	<HeaderStyle Width="20%"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="promo_type" SortExpression="promo_type" HeaderText="Promo_Type">
																	<HeaderStyle Width="20%"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="group_type" SortExpression="group_type" HeaderText="Group_Type">
																	<HeaderStyle Width="15%"></HeaderStyle>
																</asp:BoundColumn>
															</Columns>
														</asp:datagrid></DIV>
												</DIV>
												<asp:label id="lblNoLeads" Font-Bold="True" ForeColor="IndianRed" Width="120px" runat="server"
													Visible="False">No Leads Found</asp:label></DIV>
											<asp:label id="lblUncalled" Font-Size="8pt" Font-Names="Microsoft Sans Serif" Font-Bold="True"
												ForeColor="#6695C3" Width="96px" runat="server">Uncalled Leads:</asp:label><asp:textbox id="txtUncalled" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#6695C3"
												Width="32px" runat="server" BorderColor="#6695C3" BorderWidth="1px" BorderStyle="Solid" Enabled="False">0</asp:textbox></TD>
										<TD></TD>
										<TD>
											<TABLE id="Table5" style="WIDTH: 95px; HEIGHT: 194px" cellSpacing="0" cellPadding="0" width="95"
												border="0">
												<TR>
													<TD style="HEIGHT: 13px"><asp:label id="Label16" Font-Size="8pt" Font-Names="Microsoft Sans Serif" Font-Bold="True"
															ForeColor="#6695C3" Width="104px" runat="server">Start Entry Date:</asp:label></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 6px" vAlign="top" align="left"><asp:textbox id="txtStartDateAssigned" Width="72px" runat="server" BorderColor="#6695C3" BorderWidth="1px"
															BorderStyle="Solid"></asp:textbox><asp:imagebutton id="calStartAssigned" runat="server" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 5px" vAlign="top"><asp:label id="Label15" Font-Size="8pt" Font-Names="Microsoft Sans Serif" Font-Bold="True"
															ForeColor="#6695C3" Width="104px" runat="server">End Entry Date:</asp:label></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 7px" vAlign="top" align="left"><asp:textbox id="txtEndDateAssigned" Width="72px" runat="server" BorderColor="#6695C3" BorderWidth="1px"
															BorderStyle="Solid"></asp:textbox><asp:imagebutton id="calEndAssigned" runat="server" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 8px" vAlign="top"><asp:label id="Label1" Font-Size="8pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#6695C3"
															Width="104px" runat="server">Start Assign Date:</asp:label></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 8px" vAlign="top" align="left"><asp:textbox id="txtStartAssDate" Width="72px" runat="server" BorderColor="#6695C3" BorderWidth="1px"
															BorderStyle="Solid"></asp:textbox><asp:imagebutton id="CalStartAssDate" runat="server" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 8px" vAlign="top"><asp:label id="Label4" Font-Size="8pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#6695C3"
															Width="104px" runat="server">End Assign Date:</asp:label></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 8px" vAlign="top" align="left"><asp:textbox id="txtEndAssDate" Width="72px" runat="server" BorderColor="#6695C3" BorderWidth="1px"
															BorderStyle="Solid"></asp:textbox><asp:imagebutton id="calEndAssDate" runat="server" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton></TD>
												</TR>
												<TR>
													<TD vAlign="top">
														<TABLE id="Table6" style="WIDTH: 111px; HEIGHT: 2px" cellSpacing="1" cellPadding="0" width="111"
															border="0">
															<TR>
																<TD style="WIDTH: 36px; HEIGHT: 5px" vAlign="top" align="center"><asp:imagebutton id="cmdFilterAssigned" runat="server" ImageUrl="../../images/Search.gif"></asp:imagebutton></TD>
																<TD style="HEIGHT: 5px" vAlign="middle"><asp:label id="Label11" Font-Size="9pt" Font-Names="Microsoft Sans Serif" Width="72px" runat="server">Apply Filter</asp:label></TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
											</TABLE>
										</TD>
										<TD vAlign="top"><uc1:mycalendar id="MyCalendarAssign" runat="server" Visible="False"></uc1:mycalendar></TD>
									</TR>
								</TABLE>
							</TD>
							<TD style="HEIGHT: 179px"></TD>
						</TR>
						<TR>
							<TD background="WhiteSmoke"></TD>
							<TD></TD>
						</TR>
					</TABLE>
				</DIV>
			</TD>
			<TD style="WIDTH: 12px; HEIGHT: 332px" vAlign="top"></TD>
			<TD style="WIDTH: 12px; HEIGHT: 332px" vAlign="top"></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD></TD>
			<TD></TD>
		</TR>
	</TABLE>
</P>

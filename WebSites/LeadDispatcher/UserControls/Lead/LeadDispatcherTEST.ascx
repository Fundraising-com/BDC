<%@ Register TagPrefix="uc2" TagName="PaymentAdjusment_Header" Src="../PaymentAdjusment_Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MyCalendar" Src="../MyCalendar.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LeadDispatcherTEST.ascx.cs" Inherits="CRMWeb.UserControls.Lead.LeadDispatcherTEST" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<P>
	<TABLE id="Table1" style="WIDTH: 760px; HEIGHT: 686px" cellSpacing="0" cellPadding="0"
		width="760" border="0">
		<TR>
			<TD style="HEIGHT: 12px">&nbsp;&nbsp;
			</TD>
			<TD style="HEIGHT: 12px"></TD>
			<TD style="WIDTH: 12px; HEIGHT: 12px"></TD>
			<TD style="WIDTH: 12px; HEIGHT: 12px"></TD>
		</TR>
		<TR>
			<TD style="HEIGHT: 2px"></TD>
			<TD style="HEIGHT: 2px" vAlign="middle" align="left"><asp:label id="Label6" runat="server" Width="152px" ForeColor="#6695C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
					Font-Size="8pt" Height="16px">Select a Promotion Group:</asp:label><asp:dropdownlist id="cboPromoGroup" runat="server" Width="104px" AutoPostBack="True" onselectedindexchanged="cboPromoGroup_SelectedIndexChanged"></asp:dropdownlist></TD>
			<TD style="WIDTH: 12px; HEIGHT: 2px"></TD>
			<TD style="WIDTH: 12px; HEIGHT: 2px"></TD>
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
							<TD style="HEIGHT: 205px">
								<DIV>
									<DIV><asp:label id="lblNbUnassigned" runat="server" Width="656px" ForeColor="#294585" Font-Bold="True"
											Font-Names="Microsoft Sans Serif" Font-Size="11pt" BackColor="#F7F7F7"> Unassigned Leads</asp:label>
										<TABLE id="Table7" style="WIDTH: 744px; HEIGHT: 190px" cellSpacing="0" cellPadding="0"
											width="744" border="0">
											<TR>
												<TD style="WIDTH: 530px">
													<DIV style="OVERFLOW: auto; WIDTH: 656px; HEIGHT: 176px" align="left">
														<DIV>
															<DIV>
																<DIV><asp:datagrid id="dgUnassigned" runat="server" Width="999px" Font-Size="11pt" BackColor="#F7F7F7"
																		EnableViewState="False" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#6695C3"
																		BorderWidth="2px" HorizontalAlign="Left">
																		<AlternatingItemStyle BackColor="#DEDEE7"></AlternatingItemStyle>
																		<HeaderStyle Font-Size="8.5pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" HorizontalAlign="Left"
																			ForeColor="ActiveCaptionText" BackColor="#6795C3"></HeaderStyle>
																		<Columns>
																			<asp:BoundColumn Visible="False" DataField="country_code"></asp:BoundColumn>
																			<asp:BoundColumn Visible="False" DataField="fk_kit_type_id"></asp:BoundColumn>
																			<asp:BoundColumn Visible="False" DataField="lead_id"></asp:BoundColumn>
																			<asp:ButtonColumn Text="Lead ID" DataTextField="lead_id" SortExpression="lead_id asc" HeaderText="Lead_ID">
																				<HeaderStyle Width="6%"></HeaderStyle>
																				<ItemStyle ForeColor="Black"></ItemStyle>
																			</asp:ButtonColumn>
																			<asp:BoundColumn DataField="channel" SortExpression="channel" HeaderText="Channel">
																				<HeaderStyle Width="5%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="status" SortExpression="status" HeaderText="Status">
																				<HeaderStyle Width="5%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="state_code" SortExpression="state_code" HeaderText="State">
																				<HeaderStyle Width="9%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="part" SortExpression="part" HeaderText="Part">
																				<HeaderStyle Width="1%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn Visible="False" DataField="country_code" SortExpression="country_code" HeaderText="Country"></asp:BoundColumn>
																			<asp:BoundColumn DataField="promotion_type_code" SortExpression="promo_type" HeaderText="Promo_Type">
																				<HeaderStyle Width="8%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="promotion" SortExpression="promotion" HeaderText="Promotion">
																				<HeaderStyle Width="15%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="day_phone" HeaderText="Day_Phone">
																				<HeaderStyle Width="12%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="group_type" SortExpression="group_type" HeaderText="Group_Type">
																				<HeaderStyle Width="12%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="organization_type_desc" SortExpression="organization_type_desc" HeaderText="Organization">
																				<HeaderStyle Width="17%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn DataField="lead_entry_date" SortExpression="lead_entry_date" HeaderText="Entry Date"
																				DataFormatString="{0:MM/dd/yyyy}">
																				<HeaderStyle Width="7%"></HeaderStyle>
																			</asp:BoundColumn>
																			<asp:BoundColumn Visible="False" DataField="promo_group_id"></asp:BoundColumn>
																		</Columns>
																	</asp:datagrid></DIV>
															</DIV>
														</DIV>
														<asp:label id="lblNoLeadsUnassigned" runat="server" Width="120px" ForeColor="IndianRed" Font-Bold="True"
															Visible="False">No Leads Found</asp:label></DIV>
													<asp:label id="Label3" runat="server" Width="384px" Font-Size="8pt">*The columns can be sorted by clicking the header</asp:label></TD>
												<TD style="WIDTH: 7px"></TD>
												<TD><asp:imagebutton id="cmdRefresh" runat="server" Visible="False" ImageUrl="../../images/refresh2.gif"></asp:imagebutton>
													<DIV><asp:label id="Label8" runat="server" Width="94px" ForeColor="#6695C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
															Font-Size="8pt" Visible="False">Refresh</asp:label></DIV>
													<DIV>&nbsp;</DIV>
													<asp:textbox id="txtStartDateUnassigned" runat="server" Width="78px" BorderColor="#6695C3" BorderWidth="1px"
														Visible="False" BorderStyle="Solid"></asp:textbox><asp:imagebutton id="cmdCalEnd" runat="server" Visible="False" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton><uc1:mycalendar id="MyCalendar" runat="server" Visible="False"></uc1:mycalendar><asp:textbox id="txtEndDateUnassigned" runat="server" Width="78px" BorderColor="#6695C3" BorderWidth="1px"
														Visible="False" BorderStyle="Solid"></asp:textbox><asp:imagebutton id="cmdCalStart" runat="server" Visible="False" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton><asp:imagebutton id="cmdFilterUnassigned" runat="server" Visible="False" ImageUrl="../../images/Search.gif"></asp:imagebutton></TD>
											</TR>
										</TABLE>
									</DIV>
								</DIV>
							</TD>
							<TD style="HEIGHT: 205px"></TD>
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
													<TD style="WIDTH: 222px"></TD>
													<TD style="WIDTH: 39px" vAlign="top"><asp:imagebutton id="cmdUnassign" runat="server" ImageUrl="../../images/arrow_up.gif"></asp:imagebutton></TD>
													<TD vAlign="top"><asp:imagebutton id="cmdAssign" runat="server" ImageUrl="../../images/arrow_down2.gif"></asp:imagebutton>&nbsp;</TD>
												</TR>
											</TABLE>
											<asp:label id="lblError" runat="server" ForeColor="Red" Visible="False">**You must select a consultant</asp:label></TD>
										<TD style="HEIGHT: 73px"></TD>
										<TD style="HEIGHT: 73px"></TD>
										<TD style="HEIGHT: 73px"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 543px; HEIGHT: 14px" vAlign="bottom" bgColor="#f7f7f7"><asp:label id="Label5" runat="server" Width="86px" ForeColor="#294585" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="11pt" BackColor="WhiteSmoke">Leads For </asp:label><asp:dropdownlist id="cboConsultants" runat="server" Width="176px" ForeColor="#294585" Font-Bold="True"
												Font-Names="Microsoft Sans Serif" Font-Size="10pt" AutoPostBack="True" BackColor="WhiteSmoke" onselectedindexchanged="cboConsultants_SelectedIndexChanged"></asp:dropdownlist>&nbsp;
											<asp:label id="lblNbLeads" runat="server" Width="142px" ForeColor="#294585" Font-Bold="True"
												Font-Names="Microsoft Sans Serif" Font-Size="11pt" BackColor="WhiteSmoke"></asp:label></TD>
										<TD style="HEIGHT: 14px"></TD>
										<TD style="HEIGHT: 14px"></TD>
										<TD style="HEIGHT: 14px"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 543px" vAlign="top">
											<DIV style="OVERFLOW: auto; WIDTH: 536px; HEIGHT: 176px" align="left">
												<DIV>
													<DIV><asp:datagrid id="dgAssigned" runat="server" Width="864px" Font-Size="11pt" BackColor="#F7F7F7"
															AllowSorting="True" AutoGenerateColumns="False" BorderColor="#6695C3" BorderWidth="2px">
															<AlternatingItemStyle BackColor="#DEDEE7"></AlternatingItemStyle>
															<HeaderStyle Font-Size="8.5pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="ActiveCaptionText"
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
																	<HeaderStyle Width="10%"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="organization" SortExpression="organization" HeaderText="Organization">
																	<HeaderStyle Width="19%"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="promo_type" SortExpression="promo_type" HeaderText="Promo_Type">
																	<HeaderStyle Width="20%"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="promo" SortExpression="promo" HeaderText="Promotion">
																	<HeaderStyle Width="20%"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="group_type" SortExpression="group_type" HeaderText="Group Type">
																	<HeaderStyle Width="15%"></HeaderStyle>
																</asp:BoundColumn>
															</Columns>
														</asp:datagrid></DIV>
												</DIV>
												<asp:label id="lblNoLeads" runat="server" Width="120px" ForeColor="IndianRed" Font-Bold="True"
													Visible="False">No Leads Found</asp:label></DIV>
											<asp:label id="lblUncalled" runat="server" Width="96px" ForeColor="#6695C3" Font-Bold="True"
												Font-Names="Microsoft Sans Serif" Font-Size="8pt">Uncalled Leads:</asp:label><asp:textbox id="txtUncalled" runat="server" Width="32px" ForeColor="#6695C3" Font-Bold="True"
												Font-Names="Microsoft Sans Serif" BorderColor="#6695C3" BorderWidth="1px" BorderStyle="Solid" Enabled="False">0</asp:textbox></TD>
										<TD></TD>
										<TD>
											<TABLE id="Table5" style="WIDTH: 95px; HEIGHT: 194px" cellSpacing="0" cellPadding="0" width="95"
												border="0">
												<TR>
													<TD style="HEIGHT: 13px"><asp:label id="Label16" runat="server" Width="104px" ForeColor="#6695C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
															Font-Size="8pt">Start Entry Date:</asp:label></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 6px" vAlign="top" align="left"><asp:textbox id="txtStartDateAssigned" runat="server" Width="72px" BorderColor="#6695C3" BorderWidth="1px"
															BorderStyle="Solid"></asp:textbox><asp:imagebutton id="calStartAssigned" runat="server" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 5px" vAlign="top"><asp:label id="Label15" runat="server" Width="104px" ForeColor="#6695C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
															Font-Size="8pt">End Entry Date:</asp:label></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 7px" vAlign="top" align="left"><asp:textbox id="txtEndDateAssigned" runat="server" Width="72px" BorderColor="#6695C3" BorderWidth="1px"
															BorderStyle="Solid"></asp:textbox><asp:imagebutton id="calEndAssigned" runat="server" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 8px" vAlign="top"><asp:label id="Label1" runat="server" Width="104px" ForeColor="#6695C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
															Font-Size="8pt">Start Assign Date:</asp:label></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 8px" vAlign="top" align="left"><asp:textbox id="txtStartAssDate" runat="server" Width="72px" BorderColor="#6695C3" BorderWidth="1px"
															BorderStyle="Solid"></asp:textbox><asp:imagebutton id="CalStartAssDate" runat="server" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 8px" vAlign="top"><asp:label id="Label4" runat="server" Width="104px" ForeColor="#6695C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
															Font-Size="8pt">End Assign Date:</asp:label></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 8px" vAlign="top" align="left"><asp:textbox id="txtEndAssDate" runat="server" Width="72px" BorderColor="#6695C3" BorderWidth="1px"
															BorderStyle="Solid"></asp:textbox><asp:imagebutton id="calEndAssDate" runat="server" ImageUrl="../../images/calCalendar.gif"></asp:imagebutton></TD>
												</TR>
												<TR>
													<TD vAlign="top">
														<TABLE id="Table6" style="WIDTH: 111px; HEIGHT: 2px" cellSpacing="1" cellPadding="0" width="111"
															border="0">
															<TR>
																<TD style="WIDTH: 36px; HEIGHT: 5px" vAlign="top" align="center"><asp:imagebutton id="cmdFilterAssigned" runat="server" ImageUrl="../../images/Search.gif"></asp:imagebutton></TD>
																<TD style="HEIGHT: 5px" vAlign="middle"><asp:label id="Label11" runat="server" Width="72px" Font-Names="Microsoft Sans Serif" Font-Size="9pt">Apply Filter</asp:label></TD>
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
			<TD style="HEIGHT: 181px" vAlign="top" align="left"></TD>
			<TD style="HEIGHT: 181px" vAlign="top" align="left"></TD>
			<TD style="WIDTH: 12px; HEIGHT: 181px" vAlign="top" align="left"></TD>
			<TD style="WIDTH: 12px; HEIGHT: 181px" vAlign="top" align="left"></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD></TD>
			<TD></TD>
		</TR>
	</TABLE>
</P>

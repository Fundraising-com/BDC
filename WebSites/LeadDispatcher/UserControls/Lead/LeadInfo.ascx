<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LeadInfo.ascx.cs" Inherits="CRMWeb.UserControls.Lead.LeadInfo1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" style="WIDTH: 656px; HEIGHT: 554px" cellSpacing="0" cellPadding="0"
	width="656" border="0">
	<TR>
		<TD style="WIDTH: 25px; HEIGHT: 18px">&nbsp;&nbsp;&nbsp;
		</TD>
		<TD style="HEIGHT: 18px">&nbsp;</TD>
		<TD style="HEIGHT: 18px"></TD>
	</TR>
	<TR>
		<TD vAlign="bottom" style="WIDTH: 25px"></TD>
		<TD vAlign="bottom"><asp:label id="Label20" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="10pt" Font-Bold="True"
				ForeColor="#294585" Width="208px">Lead/Group</asp:label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 25px; HEIGHT: 160px" vAlign="top"></TD>
		<TD style="HEIGHT: 160px" vAlign="top">
			<TABLE id="Table6" style="WIDTH: 640px; HEIGHT: 168px" borderColor="#6695c3" cellSpacing="0"
				cellPadding="0" width="640" bgColor="#f7f7f7" border="1">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table7" style="WIDTH: 636px; HEIGHT: 176px" borderColor="#6695c3" cellSpacing="0"
							cellPadding="0" width="636" border="0">
							<TR>
								<TD style="WIDTH: 509px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label33" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3">First Name:</asp:label></TD>
								<TD style="WIDTH: 233px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label8" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt" Font-Bold="True"
										ForeColor="#6795C3" Width="64px">Last Name:</asp:label></TD>
								<TD style="WIDTH: 229px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label5" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt" Font-Bold="True"
										ForeColor="#6795C3" Width="64px">Title:</asp:label></TD>
								<TD style="WIDTH: 141px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label4" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt" Font-Bold="True"
										ForeColor="#6795C3" Width="86px">Salutation:</asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 509px; HEIGHT: 23px" vAlign="top"><asp:label id="lblFirstName" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 233px; HEIGHT: 23px" vAlign="top"><asp:label id="lblLastName" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 229px; HEIGHT: 23px" vAlign="top"><asp:label id="lblTitle" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 141px; HEIGHT: 23px" vAlign="top"><asp:label id="lblSalutation" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 509px; HEIGHT: 15px" vAlign="bottom"><asp:label id="Label49" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3" Width="86px">Group Type:</asp:label></TD>
								<TD style="WIDTH: 233px; HEIGHT: 15px" vAlign="bottom"><asp:label id="Label48" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3" Width="65px">Org Type:</asp:label></TD>
								<TD style="WIDTH: 229px; HEIGHT: 15px" vAlign="bottom">
									<asp:label id="Label2" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										runat="server">Lead ID:</asp:label></TD>
								<TD style="WIDTH: 141px; HEIGHT: 15px" vAlign="bottom">
									<asp:label id="Label25" Width="96px" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt"
										Font-Names="Microsoft Sans Serif" runat="server">Group Web Site:</asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 509px; HEIGHT: 15px" vAlign="top">
									<asp:label id="lblGroupType" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:label></TD>
								<TD style="WIDTH: 233px; HEIGHT: 15px" vAlign="top">
									<asp:label id="lblOrgType" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:label></TD>
								<TD style="WIDTH: 229px; HEIGHT: 15px" vAlign="top">
									<asp:label id="lblLeadID" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:label></TD>
								<TD style="WIDTH: 304px; HEIGHT: 15px" vAlign="top">
									<asp:HyperLink id="lblGroupWebSite" Font-Size="10pt" runat="server"></asp:HyperLink></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 509px; HEIGHT: 21px" vAlign="bottom">
									<asp:label id="Label10" Width="112px" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt"
										Font-Names="Microsoft Sans Serif" runat="server">Organization Name:</asp:label></TD>
								<TD style="WIDTH: 233px; HEIGHT: 21px" vAlign="bottom">
									<asp:label id="Label14" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										runat="server">Comments:</asp:label></TD>
								<TD style="WIDTH: 229px; HEIGHT: 21px" vAlign="bottom"></TD>
								<TD style="WIDTH: 141px; HEIGHT: 21px" vAlign="top"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 509px; HEIGHT: 60px" vAlign="top">
									<asp:label id="lblOrg2" Width="184px" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:label></TD>
								<TD style="WIDTH: 166px; HEIGHT: 60px" vAlign="top" colSpan="3">
									<DIV>
										<DIV>
											<DIV>
												<DIV style="OVERFLOW: auto; WIDTH: 444px; HEIGHT: 80px" align="left">
													<DIV>
														<DIV>
															<DIV>
																<DIV>
																	<DIV>
																		<DIV>
																			<asp:datagrid id="dgComments" Width="416px" Font-Size="11pt" runat="server" HorizontalAlign="Left"
																				BorderWidth="0px" BorderColor="#6695C3" AutoGenerateColumns="False" AllowSorting="True" BackColor="#F7F7F7"
																				ShowHeader="False" PageSize="2" Height="8px">
																				<AlternatingItemStyle BackColor="#DEDEE7"></AlternatingItemStyle>
																				<HeaderStyle Font-Size="8.5pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" HorizontalAlign="Left"
																					ForeColor="ActiveCaptionText" BackColor="#6795C3"></HeaderStyle>
																				<Columns>
																					<asp:BoundColumn Visible="False" DataField="entry_date" SortExpression="lead_entry_date" HeaderText="Comment Date"
																						DataFormatString="{0:MM/dd/yyyy}">
																						<HeaderStyle Width="10%"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="comments" HeaderText="Comment">
																						<HeaderStyle Width="90%"></HeaderStyle>
																					</asp:BoundColumn>
																				</Columns>
																			</asp:datagrid></DIV>
																	</DIV>
																</DIV>
															</DIV>
														</DIV>
													</DIV>
												</DIV>
											</DIV>
										</DIV>
									</DIV>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<DIV>&nbsp;</DIV>
		</TD>
		<TD style="HEIGHT: 140px" vAlign="top"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 25px; HEIGHT: 180px" vAlign="top"></TD>
		<TD style="HEIGHT: 180px" vAlign="top">
			<TABLE id="Table14" cellSpacing="0" cellPadding="0" width="300" border="0">
				<TR>
					<TD style="WIDTH: 362px"><asp:label id="Label38" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="10pt" Font-Bold="True"
							ForeColor="#294585" Width="208px">Address</asp:label></TD>
					<TD></TD>
					<TD><asp:label id="Label12" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="10pt" Font-Bold="True"
							ForeColor="#294585" Width="256px">Phone/E-Mail</asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 362px" vAlign="top">
						<TABLE id="Table12" style="WIDTH: 304px; HEIGHT: 143px" borderColor="#6695c3" cellSpacing="0"
							cellPadding="0" width="304" bgColor="#f7f7f7" border="1">
							<TR>
								<TD vAlign="top">
									<TABLE id="Table13" style="WIDTH: 360px; HEIGHT: 120px" borderColor="#6695c3" cellSpacing="0"
										cellPadding="0" width="360" border="0">
										<TR>
											<TD style="WIDTH: 195px; HEIGHT: 18px" vAlign="bottom"><asp:label id="Label24" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3">Street:</asp:label></TD>
											<TD style="WIDTH: 164px; HEIGHT: 18px" vAlign="bottom"></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 346px; HEIGHT: 2px" vAlign="top" colSpan="2"><asp:label id="lblAddress" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"
													Width="304px"></asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 195px; HEIGHT: 1px" vAlign="bottom"><asp:label id="Label23" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3" Width="111px">City:</asp:label></TD>
											<TD style="WIDTH: 164px; HEIGHT: 1px" vAlign="bottom"><asp:label id="Label22" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3">State:</asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 195px; HEIGHT: 5px" vAlign="top"><asp:label id="lblCity" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
											<TD style="WIDTH: 164px; HEIGHT: 5px" vAlign="top"><asp:label id="lblState" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 195px; HEIGHT: 5px" vAlign="bottom"><asp:label id="Label21" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3" Width="67px" Height="3px">Zip:</asp:label></TD>
											<TD style="WIDTH: 164px; HEIGHT: 5px" vAlign="bottom"><asp:label id="Label19" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3">Country:</asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 195px; HEIGHT: 23px" vAlign="top"><asp:label id="lblZip" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
											<TD style="WIDTH: 164px; HEIGHT: 23px" vAlign="top"><asp:label id="lblCountry" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD vAlign="top">&nbsp;
					</TD>
					<TD vAlign="top">
						<TABLE id="Table8" style="WIDTH: 264px; HEIGHT: 143px" borderColor="#6695c3" cellSpacing="0"
							cellPadding="0" width="264" bgColor="#f7f7f7" border="1">
							<TR>
								<TD vAlign="top">
									<TABLE id="Table9" style="WIDTH: 208px; HEIGHT: 120px" borderColor="#6695c3" cellSpacing="0"
										cellPadding="0" width="208" border="0">
										<TR>
											<TD style="WIDTH: 121px; HEIGHT: 18px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 18px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 18px" vAlign="middle"><asp:label id="Label56" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3" Width="71px">Day Phone:</asp:label></TD>
											<TD style="WIDTH: 86px; HEIGHT: 18px" vAlign="middle"><asp:label id="lblDayPhone" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 121px; HEIGHT: 8px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 8px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 8px" vAlign="middle"><asp:label id="Label55" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3" Width="98px">Eve. Phone:</asp:label></TD>
											<TD style="WIDTH: 86px; HEIGHT: 8px" vAlign="middle"><asp:label id="lblEvePhone" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 121px; HEIGHT: 9px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 9px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 9px" vAlign="middle"><asp:label id="Label53" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3" Width="104px">Best Time To Call:</asp:label></TD>
											<TD style="WIDTH: 86px; HEIGHT: 9px" vAlign="middle"><asp:label id="lblBestTime" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"
													Width="120px"></asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 121px; HEIGHT: 1px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 1px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 1px" vAlign="middle"><asp:label id="Label51" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3" Width="111px">Email:</asp:label></TD>
											<TD style="WIDTH: 86px; HEIGHT: 1px" vAlign="middle"><asp:label id="lblEmail" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 121px; HEIGHT: 18px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 18px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 18px" vAlign="middle"><asp:label id="Label52" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3">Fax:</asp:label></TD>
											<TD style="WIDTH: 86px; HEIGHT: 18px" vAlign="middle"><asp:label id="lblFax" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 121px; HEIGHT: 4px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 4px" vAlign="middle"></TD>
											<TD style="WIDTH: 97px; HEIGHT: 4px" vAlign="middle"><asp:label id="Label54" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
													Font-Bold="True" ForeColor="#6795C3" Width="84px">Other Phone:</asp:label></TD>
											<TD style="WIDTH: 86px; HEIGHT: 4px" vAlign="middle"><asp:label id="lblOtherPhone" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
						&nbsp;
					</TD>
				</TR>
			</TABLE>
		</TD>
		<TD style="HEIGHT: 180px" vAlign="top"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 25px; HEIGHT: 11px" vAlign="top"></TD>
		<TD style="HEIGHT: 11px" vAlign="top"><asp:label id="lblNbUnassigned" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="10pt"
				Font-Bold="True" ForeColor="#294585" Width="176px">Tracking Information</asp:label></TD>
		<TD style="HEIGHT: 11px" vAlign="top"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 25px; HEIGHT: 54px" vAlign="top"></TD>
		<TD style="HEIGHT: 54px" vAlign="top">
			<TABLE id="Table4" style="WIDTH: 637px; HEIGHT: 50px" borderColor="#6695c3" cellSpacing="0"
				cellPadding="0" width="637" bgColor="#f7f7f7" border="1">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table5" style="WIDTH: 632px; HEIGHT: 82px" borderColor="#6695c3" cellSpacing="0"
							cellPadding="0" width="632" border="0">
							<TR>
								<TD style="WIDTH: 133px; HEIGHT: 5px" vAlign="bottom"><asp:label id="Label1" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt" Font-Bold="True"
										ForeColor="#6795C3">Promo Type:</asp:label></TD>
								<TD style="WIDTH: 89px; HEIGHT: 5px" vAlign="bottom"><asp:label id="Label3" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt" Font-Bold="True"
										ForeColor="#6795C3">Promotion:</asp:label></TD>
								<TD style="HEIGHT: 5px" vAlign="bottom"><asp:label id="Label15" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3">Channel:</asp:label></TD>
								<TD style="WIDTH: 116px; HEIGHT: 5px" vAlign="bottom"><asp:label id="Label16" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3" Width="111px">Lead Entry Date:</asp:label></TD>
								<TD style="HEIGHT: 5px" vAlign="bottom"><asp:label id="Label18" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3" Width="111px">Status:</asp:label></TD>
								<TD style="HEIGHT: 5px" vAlign="bottom"></TD>
								<TD style="HEIGHT: 5px" vAlign="bottom"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 133px; HEIGHT: 8px" vAlign="top"><asp:label id="lblPromoType" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 89px; HEIGHT: 8px" vAlign="top"><asp:label id="lblPromotion" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="HEIGHT: 8px" vAlign="top"><asp:label id="lblChannel" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 116px; HEIGHT: 8px" vAlign="top"><asp:label id="lblEntryDate" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="HEIGHT: 8px" vAlign="top"><asp:label id="lblStatus" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="HEIGHT: 8px" vAlign="bottom"></TD>
								<TD style="HEIGHT: 8px" vAlign="bottom"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 133px; HEIGHT: 6px" vAlign="bottom"><asp:label id="Label13" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3">Consultant:</asp:label></TD>
								<TD style="WIDTH: 89px; HEIGHT: 6px" vAlign="bottom"><asp:label id="Label11" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3">FM:</asp:label></TD>
								<TD style="HEIGHT: 6px" vAlign="bottom"><asp:label id="Label9" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt" Font-Bold="True"
										ForeColor="#6795C3">Assignment Date:</asp:label></TD>
								<TD style="WIDTH: 116px; HEIGHT: 6px" vAlign="bottom"><asp:label id="Label6" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt" Font-Bold="True"
										ForeColor="#6795C3">Assigner:</asp:label></TD>
								<TD style="HEIGHT: 6px" vAlign="bottom"><asp:label id="Label7" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt" Font-Bold="True"
										ForeColor="#6795C3">Web Site:</asp:label></TD>
								<TD style="HEIGHT: 6px" vAlign="bottom"></TD>
								<TD style="HEIGHT: 6px" vAlign="bottom"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 133px; HEIGHT: 25px" vAlign="top"><asp:label id="lblConsultant" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 89px; HEIGHT: 25px" vAlign="top"><asp:label id="lblFM" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="HEIGHT: 25px" vAlign="top"><asp:label id="lblAssignmentDate" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 116px; HEIGHT: 25px" vAlign="top"><asp:label id="lblAssigner" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="HEIGHT: 25px" vAlign="top">
									<asp:HyperLink id="lblFromWebSite" Font-Size="10pt" runat="server"></asp:HyperLink></TD>
								<TD style="HEIGHT: 25px" vAlign="top"></TD>
								<TD style="HEIGHT: 25px" vAlign="top"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</TD>
		<TD vAlign="top"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 25px; HEIGHT: 20px" vAlign="top"></TD>
		<TD style="HEIGHT: 20px" vAlign="top"></TD>
		<TD style="HEIGHT: 20px" vAlign="top"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 25px; HEIGHT: 16px"></TD>
		<TD style="HEIGHT: 16px"><asp:label id="Label40" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="10pt" Font-Bold="True"
				ForeColor="#294585" Width="256px">Campaign</asp:label></TD>
		<TD style="HEIGHT: 16px"></TD>
	</TR>
	<TR>
		<TD vAlign="top" style="WIDTH: 25px"></TD>
		<TD vAlign="top">
			<TABLE id="Table3" style="WIDTH: 637px; HEIGHT: 60px" borderColor="#6695c3" cellSpacing="0"
				cellPadding="0" width="637" bgColor="#f7f7f7" border="1">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table15" style="WIDTH: 632px; HEIGHT: 64px" borderColor="#6695c3" cellSpacing="0"
							cellPadding="0" width="632" border="0">
							<TR>
								<TD style="WIDTH: 121px; HEIGHT: 10px" vAlign="bottom"><asp:label id="Label39" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3" Width="96px">Nb Participants:</asp:label></TD>
								<TD style="WIDTH: 86px; HEIGHT: 10px" vAlign="bottom"><asp:label id="Label35" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3" Width="112px">Campaign Reason:</asp:label></TD>
								<TD style="WIDTH: 86px; HEIGHT: 10px" vAlign="bottom"><asp:label id="Label30" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3" Width="104px">Heard About Us:</asp:label></TD>
								<TD style="WIDTH: 86px; HEIGHT: 10px" vAlign="bottom"><asp:label id="Label28" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3" Width="111px">Fundraising Goal:</asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 121px; HEIGHT: 2px" vAlign="top"><asp:label id="lblNbPart" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 86px; HEIGHT: 2px" vAlign="top"><asp:label id="lblReason" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 86px; HEIGHT: 2px" vAlign="top"><asp:label id="lblHeard" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt" Width="160px"></asp:label></TD>
								<TD style="WIDTH: 86px; HEIGHT: 2px" vAlign="top"><asp:label id="lblGoal" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 121px; HEIGHT: 20px" vAlign="bottom"><asp:label id="Label26" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3" Width="128px">Campaign Start Date:</asp:label></TD>
								<TD style="WIDTH: 86px; HEIGHT: 20px" vAlign="bottom"><asp:label id="Label17" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="8.5pt"
										Font-Bold="True" ForeColor="#6795C3" Width="84px">kit Type:</asp:label></TD>
								<TD style="WIDTH: 86px; HEIGHT: 20px" vAlign="middle"></TD>
								<TD style="WIDTH: 86px; HEIGHT: 20px" vAlign="middle"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 121px; HEIGHT: 29px" vAlign="top"><asp:label id="lblStartDate" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 86px; HEIGHT: 29px" vAlign="top"><asp:label id="lblkit" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 86px; HEIGHT: 29px" vAlign="middle"></TD>
								<TD style="WIDTH: 86px; HEIGHT: 29px" vAlign="middle"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</TD>
		<TD vAlign="top"></TD>
	</TR>
</TABLE>

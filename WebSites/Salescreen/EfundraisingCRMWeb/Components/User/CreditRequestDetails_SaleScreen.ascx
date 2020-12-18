<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CreditRequestDetails_SaleScreen.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.CreditRequestDetails_SaleScreen" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<LINK href="../../Resources/Css/style.css" type="text/css" rel="stylesheet">
<LINK href="../../Resources/Css/TallySale.css" type="text/css" rel="stylesheet">
<TABLE id="Table1" style="WIDTH: 656px; HEIGHT: 232px" cellSpacing="0" cellPadding="0"
	width="656" border="0">
	<TR>
		<TD class="NormalTextBold">
			<TABLE id="Table4" height="30" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR class="AlternateItemBackGround">
					<TD>
						<asp:Label id="Label1" runat="server" Font-Bold="True">Request Details</asp:Label></TD>
					<TD align="left"></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD><!--HERE -->
			<TABLE id="Table3" style="WIDTH: 100%; HEIGHT: 187px" borderColor="#6695c3" cellSpacing="0"
				cellPadding="0" width="688" border="0">
				<TR>
					<TD class="NormalTextBold Passive" style="WIDTH: 69px; HEIGHT: 12px" vAlign="bottom"
						height="12">Lead ID</TD>
					<TD class="NormalTextBold Passive" style="WIDTH: 121px; HEIGHT: 12px" vAlign="bottom"
						height="12">Amt Requested</TD>
					<TD class="NormalTextBold Passive" style="WIDTH: 28.9%; HEIGHT: 12px" vAlign="bottom"
						height="12">First Name</TD>
					<TD class="NormalTextBold Passive" style="WIDTH: 30%; HEIGHT: 12px" vAlign="bottom"
						height="12">Last Name</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 69px; HEIGHT: 16px" vAlign="top">
						<asp:label id="LeadIDLabel" CssClass="NormalText" runat="server"></asp:label></TD>
					<TD style="WIDTH: 121px; HEIGHT: 16px" vAlign="top">
						<asp:label id="AmtREquestedLabel" CssClass="NormalText" runat="server"></asp:label></TD>
					<TD style="WIDTH: 24%; HEIGHT: 16px" vAlign="top">
						<asp:label id="FirstNameLabel" CssClass="NormalText" runat="server"></asp:label></TD>
					<TD style="WIDTH: 25%; HEIGHT: 16px" vAlign="top">
						<asp:label id="LastNameLabel" CssClass="NormalText" runat="server" Width="53px"></asp:label></TD>
				</TR>
				<TR>
					<TD class="NormalTextBold Passive" style="WIDTH: 69px; HEIGHT: 15px" vAlign="bottom">Address</TD>
					<TD style="WIDTH: 121px; HEIGHT: 15px" vAlign="bottom"></TD>
					<TD style="WIDTH: 192px; HEIGHT: 15px" vAlign="bottom"></TD>
					<TD style="WIDTH: 25%; HEIGHT: 15px" vAlign="bottom"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 22px" vAlign="top" colSpan="4">
						<asp:label id="AddressLabel" CssClass="NormalText" runat="server" Width="528px"></asp:label></TD>
				</TR>
				<TR>
					<TD class="NormalTextBold Passive" style="WIDTH: 69px; HEIGHT: 19px" vAlign="bottom">History:</TD>
					<TD style="WIDTH: 121px; HEIGHT: 19px" vAlign="top"></TD>
					<TD style="WIDTH: 192px; HEIGHT: 19px" vAlign="top"></TD>
					<TD style="WIDTH: 59px; HEIGHT: 19px" vAlign="top"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 97px; HEIGHT: 15px" vAlign="top" colSpan="4">
						<DIV>
							<asp:datagrid id="dgHistory" CssClass="NormalText" runat="server" Width="608px" HorizontalAlign="Left"
								BorderWidth="2px" BorderColor="Gray" AutoGenerateColumns="False" AllowSorting="True" PageSize="5"
								BackColor="#F7F7F7" AllowPaging="True">
								<AlternatingItemStyle CssClass="AlternateItemBackGround"></AlternatingItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Left" CssClass="AlternateItemBackGround NormalTextBold Passive"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="credit_Check_id"></asp:BoundColumn>
									<asp:BoundColumn DataField="fc" HeaderText="FC">
										<HeaderStyle Width="20%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="request_date" HeaderText="Request Date">
										<HeaderStyle Width="15%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="amount_requested" HeaderText="$ Requested">
										<HeaderStyle Width="20%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="credit_check_status" HeaderText="Credit Status">
										<HeaderStyle Width="15%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="amount_confirmed" HeaderText="$ Approved">
										<HeaderStyle Width="20%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="2%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<CENTER>
												<asp:ImageButton id="InfoImageButton" runat="server" OnClick="InfoImageButton_Click" ImageUrl="../../Ressources/Images/report2.gif"></asp:ImageButton></CENTER>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE> <!--END HERE -->
			<DIV>&nbsp;</DIV>
		</TD>
	</TR>
</TABLE>

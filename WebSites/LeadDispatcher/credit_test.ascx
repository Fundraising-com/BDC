<%@ Control Language="c#" AutoEventWireup="True" Codebehind="credit_test.ascx.cs" Inherits="CRMWeb.UserControls.credit_test" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<HTML>
	<body>
		<TABLE id="Table3" style="WIDTH: 576px; HEIGHT: 330px" cellSpacing="0" cellPadding="0"
			width="576" border="0">
			<TR>
				<TD style="WIDTH: 30px; HEIGHT: 30px"></TD>
				<TD style="HEIGHT: 30px">
					<TABLE id="Table1" style="WIDTH: 824px; HEIGHT: 26px" cellSpacing="0" cellPadding="0" width="824"
						border="0">
						<TR>
							<TD style="HEIGHT: 25px"></TD>
							<TD style="HEIGHT: 25px" vAlign="top" bgColor="#006699"><ie:tabstrip id="TabStripAR" AutoPostBack="True" runat="server" Height="26px" Width="329px" TabSelectedStyle="background-color:#EDEDE1;font-family:verdana;font-weight:bold;font-size:8pt;color:#000000;width:79;height:21;text-align:center;"
									TabHoverStyle="background-color:#0678B1;font-family:verdana;font-weight:bold;font-size:9pt;color:#ffffff;width:79;height:21;text-align:center;"
									TabDefaultStyle="background-color:#006699;font-family:verdana;font-weight:bold;font-size:8pt;color:#ffffff;width:79;height:21;text-align:center;"
									SelectedIndex="1">
									<ie:Tab Text="Credit Requests"></ie:Tab>
									<ie:TabSeparator></ie:TabSeparator>
									<ie:Tab Text="Credit Reports"></ie:Tab>
									<ie:TabSeparator></ie:TabSeparator>
									<ie:Tab Text="Experian_Access / Options"></ie:Tab>
									<ie:TabSeparator></ie:TabSeparator>
								</ie:tabstrip></TD>
							<TD style="HEIGHT: 25px"></TD>
						</TR>
					</TABLE>
				</TD>
				<TD style="WIDTH: 12px; HEIGHT: 30px"></TD>
				<TD style="WIDTH: 1px; HEIGHT: 30px"></TD>
			</TR>
			<TR>
				<TD style="WIDTH: 30px; HEIGHT: 3px"></TD>
				<TD style="HEIGHT: 3px"><asp:label id="Label1" runat="server" Width="224px" Font-Names="Microsoft Sans Serif" Font-Bold="True"
						ForeColor="#294585" Font-Size="11pt"> Experian Results</asp:label></TD>
				<TD style="WIDTH: 12px; HEIGHT: 3px"></TD>
				<TD style="WIDTH: 1px; HEIGHT: 3px"></TD>
			</TR>
			<TR>
				<TD style="WIDTH: 30px; HEIGHT: 201px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
				<TD style="HEIGHT: 201px" vAlign="top">
					<TABLE id="Table2" style="WIDTH: 552px; HEIGHT: 178px" cellSpacing="0" cellPadding="0"
						width="552" border="0">
						<TR>
							<TD style="HEIGHT: 162px"></TD>
							<TD style="WIDTH: 541px; HEIGHT: 162px" vAlign="top" align="left">
								<DIV style="OVERFLOW: auto; WIDTH: 544px; HEIGHT: 160px" align="left">&nbsp;&nbsp;
									<asp:linkbutton id="lnkRefresh" runat="server" Font-Names="Microsoft Sans Serif" Font-Bold="True"
										ForeColor="#6695C3" Font-Size="7pt">Refresh Data</asp:linkbutton><asp:datagrid id="dgPayments" runat="server" Height="88px" Width="504px" BorderWidth="2px" BorderColor="#6695C3"
										BackColor="#F7F7F7" AutoGenerateColumns="False" AllowSorting="True">
										<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
										<HeaderStyle Font-Size="8.5pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="ActiveCaptionText"
											BackColor="#6795C3"></HeaderStyle>
										<Columns>
											<asp:ButtonColumn Text="Button" HeaderText="Lead ID"></asp:ButtonColumn>
											<asp:BoundColumn DataField="payment_entry_date" HeaderText="Lead Name" DataFormatString="{0:dd/MM/yyyy}">
												<HeaderStyle Width="20%"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="collection_status" HeaderText="FC">
												<HeaderStyle Width="15%"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="payment_amount" HeaderText="Amount" DataFormatString="{0:$#,##0.00;($#,##0.00);0.00}">
												<HeaderStyle Width="15%"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Payment_method" HeaderText="Time">
												<HeaderStyle Width="15%"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid></DIV>
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
							<TD style="HEIGHT: 162px" vAlign="top" align="left"></TD>
						</TR>
					</TABLE>
					<DIV></DIV>
					<asp:label id="lblAdd" runat="server" Width="168px" Font-Names="Microsoft Sans Serif" Font-Bold="True"
						ForeColor="#294585" Visible="False" Font-Size="11pt">Status</asp:label></TD>
				<TD style="WIDTH: 12px; HEIGHT: 201px" vAlign="top"></TD>
				<TD style="WIDTH: 1px; HEIGHT: 201px" vAlign="top"></TD>
			</TR>
			<TR>
				<TD style="WIDTH: 30px"></TD>
				<TD style="HEIGHT: 181px" vAlign="top" align="left">
					<TABLE id="Table6" style="WIDTH: 568px; HEIGHT: 225px" borderColor="#6695c3" cellSpacing="0"
						cellPadding="0" width="568" bgColor="#f7f7f7" border="1">
						<TR>
							<TD vAlign="top">
								<TABLE id="Table7" style="WIDTH: 560px; HEIGHT: 50px" borderColor="#6695c3" cellSpacing="0"
									cellPadding="0" width="560" border="0">
									<TR>
										<TD style="WIDTH: 127px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label33" runat="server" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#6795C3"
												Font-Size="8.5pt">Lead ID:</asp:label></TD>
										<TD style="WIDTH: 233px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label8" runat="server" Width="80px" Font-Names="Microsoft Sans Serif" Font-Bold="True"
												ForeColor="#6795C3" Font-Size="8.5pt">Credit Status:</asp:label></TD>
										<TD style="WIDTH: 229px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label5" runat="server" Width="112px" Font-Names="Microsoft Sans Serif" Font-Bold="True"
												ForeColor="#6795C3" Font-Size="8.5pt">Permanent Status:</asp:label></TD>
										<TD style="WIDTH: 59px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label16" runat="server" Width="112px" Font-Names="Microsoft Sans Serif" Font-Bold="True"
												ForeColor="#6795C3" Font-Size="8.5pt">Valid Until:</asp:label></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 127px; HEIGHT: 16px" vAlign="top"><asp:label id="lblFirstName" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
										<TD style="WIDTH: 233px; HEIGHT: 16px" vAlign="top"><asp:dropdownlist id="DropDownList1" runat="server" Width="120px" BackColor="#00C000">
												<asp:ListItem Value="Accepted" Selected="True">Accepted</asp:ListItem>
											</asp:dropdownlist></TD>
										<TD style="WIDTH: 229px; HEIGHT: 16px" vAlign="top"><asp:dropdownlist id="DropDownList2" runat="server" Width="120px" BackColor="#00C000">
												<asp:ListItem Value="Accepted" Selected="True">Very Good</asp:ListItem>
											</asp:dropdownlist></TD>
										<TD style="WIDTH: 59px; HEIGHT: 16px" vAlign="top"><asp:textbox id="TextBox1" runat="server" Width="112px">12/25/2006</asp:textbox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 127px; HEIGHT: 14px" vAlign="bottom"><asp:label id="Label49" runat="server" Width="104px" Font-Names="Microsoft Sans Serif" Font-Bold="True"
												ForeColor="#6795C3" Font-Size="8.5pt"> Amount Reqested:</asp:label></TD>
										<TD style="WIDTH: 233px; HEIGHT: 14px" vAlign="bottom"><asp:label id="Label48" runat="server" Width="120px" Font-Names="Microsoft Sans Serif" Font-Bold="True"
												ForeColor="#6795C3" Font-Size="8.5pt">Amount Confirmed:</asp:label></TD>
										<TD style="WIDTH: 229px; HEIGHT: 14px" vAlign="bottom"><asp:label id="Label2" runat="server" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#6795C3"
												Font-Size="8.5pt"> Max amount:</asp:label></TD>
										<TD style="WIDTH: 59px; HEIGHT: 14px" vAlign="bottom"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 127px; HEIGHT: 15px" vAlign="top"><asp:label id="lblGroupType" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
										<TD style="WIDTH: 233px; HEIGHT: 15px" vAlign="top"><asp:textbox id="TextBox2" runat="server" Width="112px">800$</asp:textbox></TD>
										<TD style="WIDTH: 229px; HEIGHT: 15px" vAlign="top"><asp:textbox id="TextBox3" runat="server" Width="112px">2000$</asp:textbox></TD>
										<TD style="WIDTH: 59px; HEIGHT: 15px" vAlign="top"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 127px; HEIGHT: 15px" vAlign="top"></TD>
										<TD style="WIDTH: 233px; HEIGHT: 15px" vAlign="top"></TD>
										<TD style="WIDTH: 229px; HEIGHT: 15px" vAlign="top"></TD>
										<TD style="WIDTH: 59px; HEIGHT: 15px" vAlign="top"></TD>
									</TR>
								</TABLE>
								<DIV>
									<DIV><asp:button id="Button2" runat="server" Width="81px" Text="Show Report"></asp:button><asp:button id="Button3" runat="server" Width="141px" Text="Save and Send To FC"></asp:button><asp:panel id="Panel1" runat="server" Height="106px" BackColor="White" BorderStyle="Inset">Panel</asp:panel></DIV>
								</DIV>
							</TD>
						</TR>
					</TABLE>
					<DIV>&nbsp;</DIV>
				</TD>
				<TD style="WIDTH: 12px; HEIGHT: 181px" vAlign="top" align="left"></TD>
				<TD style="WIDTH: 1px; HEIGHT: 181px" vAlign="top" align="left"></TD>
			</TR>
			<TR>
				<TD style="WIDTH: 30px"></TD>
				<TD>
					<DIV>
						<DIV>
							<DIV>&nbsp;</DIV>
						</DIV>
					</DIV>
				</TD>
				<TD></TD>
			</TR>
		</TABLE>
	</body>
</HTML>

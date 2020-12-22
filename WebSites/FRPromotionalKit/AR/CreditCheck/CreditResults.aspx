<%@ Page language="c#" Codebehind="CreditResults.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.AR.CreditCheck.CreditResponse" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML xmlns:o>
	<HEAD>
		<title>CreditResponse</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../../Resources/Css/style.css" type="text/css" rel="stylesheet">
		<LINK href="../../Resources/Css/TallySale.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table3" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 576px; POSITION: absolute; TOP: 8px; HEIGHT: 330px"
				cellSpacing="0" cellPadding="0" width="576" border="0">
				<TR>
					<TD style="HEIGHT: 3px"></TD>
					<TD style="WIDTH: 12px; HEIGHT: 3px"></TD>
					<TD style="WIDTH: 1px; HEIGHT: 3px"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 3px" bgColor="#f7f7f7"><asp:label id="Label1" runat="server" Width="224px" Font-Size="11pt" ForeColor="#294585" Font-Bold="True"
							Font-Names="Microsoft Sans Serif"> Experian Results</asp:label></TD>
					<TD style="WIDTH: 12px; HEIGHT: 3px"></TD>
					<TD style="WIDTH: 1px; HEIGHT: 3px"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 201px" vAlign="top">
						<TABLE id="Table2" style="WIDTH: 600px; HEIGHT: 178px" cellSpacing="0" cellPadding="0"
							width="600" border="0">
							<TR>
								<TD style="HEIGHT: 162px"></TD>
								<TD style="WIDTH: 548px; HEIGHT: 162px" vAlign="top" align="left">
									<DIV style="OVERFLOW: auto; WIDTH: 592px; HEIGHT: 160px" align="left"><asp:linkbutton id="lnkRefresh" runat="server" Font-Size="7pt" ForeColor="#6695C3" Font-Bold="True"
											Font-Names="Microsoft Sans Serif">Refresh Data</asp:linkbutton><asp:datagrid id="dgResults" runat="server" Width="584px" Font-Size="11pt" AllowSorting="True"
											AutoGenerateColumns="False" BackColor="#F7F7F7" BorderColor="#6695C3" BorderWidth="2px">
											<AlternatingItemStyle BackColor="#DEDEE7"></AlternatingItemStyle>
											<HeaderStyle Font-Size="8.5pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="ActiveCaptionText"
												BackColor="#6795C3"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="credit_check_id"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="lead_id"></asp:BoundColumn>
												<asp:ButtonColumn Text="lead_id" DataTextField="lead_id" HeaderText="Lead ID">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:ButtonColumn>
												<asp:BoundColumn DataField="lead_name" HeaderText="Lead Name">
													<HeaderStyle Width="20%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="FC" HeaderText="FC">
													<HeaderStyle Width="22%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="amount" HeaderText="Amount" DataFormatString="{0:$#,##0.00;($#,##0.00);0.00}">
													<HeaderStyle Width="12%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="time" HeaderText="Time">
													<HeaderStyle Width="25%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="score" HeaderText="Score">
													<HeaderStyle Width="7%"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid>
										<asp:button id="Button1" runat="server" Width="208px" Text="Auto Process All Bad Accounts"></asp:button></DIV>
								</TD>
								<TD style="HEIGHT: 162px" vAlign="top" align="left"></TD>
							</TR>
						</TABLE>
						<DIV></DIV>
					</TD>
					<TD style="WIDTH: 12px; HEIGHT: 201px" vAlign="top"></TD>
					<TD style="WIDTH: 1px; HEIGHT: 201px" vAlign="top"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 2px" vAlign="top" align="left" bgColor="#f7f7f7"><asp:label id="lblAdd" runat="server" Width="168px" Font-Size="11pt" ForeColor="#294585" Font-Bold="True"
							Font-Names="Microsoft Sans Serif" Visible="False">Update Status</asp:label></TD>
					<TD style="WIDTH: 12px; HEIGHT: 2px" vAlign="top" align="left"></TD>
					<TD style="WIDTH: 1px; HEIGHT: 2px" vAlign="top" align="left"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 181px" vAlign="top" align="left">
						<TABLE id="Table6" style="WIDTH: 568px; HEIGHT: 225px" borderColor="#6695c3" cellSpacing="0"
							cellPadding="0" width="568" bgColor="#f7f7f7" border="1">
							<TR>
								<TD vAlign="top">
									<TABLE id="Table7" style="WIDTH: 528px; HEIGHT: 99px" borderColor="#6695c3" cellSpacing="0"
										cellPadding="0" width="528" border="0">
										<TR>
											<TD style="WIDTH: 127px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label33" runat="server" Font-Size="8.5pt" ForeColor="#6795C3" Font-Bold="True"
													Font-Names="Microsoft Sans Serif">Lead ID:</asp:label></TD>
											<TD style="WIDTH: 143px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label8" runat="server" Width="80px" Font-Size="8.5pt" ForeColor="#6795C3" Font-Bold="True"
													Font-Names="Microsoft Sans Serif">Credit Status:</asp:label></TD>
											<TD style="WIDTH: 138px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label5" runat="server" Width="112px" Font-Size="8.5pt" ForeColor="#6795C3" Font-Bold="True"
													Font-Names="Microsoft Sans Serif" Visible="False">Credit Rating:</asp:label></TD>
											<TD style="WIDTH: 59px; HEIGHT: 19px" vAlign="bottom"><asp:label id="Label16" runat="server" Width="112px" Font-Size="8.5pt" ForeColor="#6795C3"
													Font-Bold="True" Font-Names="Microsoft Sans Serif" Visible="False">Rating Valid Until:</asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 127px; HEIGHT: 14px" vAlign="top">
												<asp:textbox id="LeadIDTextBox" runat="server" Width="105px" ReadOnly="True"></asp:textbox></TD>
											<TD style="WIDTH: 143px; HEIGHT: 14px" vAlign="top"><asp:dropdownlist id="CreditStatusDropDown" runat="server" Width="120px"></asp:dropdownlist></TD>
											<TD style="WIDTH: 138px; HEIGHT: 14px" vAlign="top"><asp:dropdownlist id="CreditRatingDropDown" runat="server" Width="120px" Visible="False"></asp:dropdownlist></TD>
											<TD style="WIDTH: 59px; HEIGHT: 14px" vAlign="top"><asp:textbox id="ValidUntilTextBox" runat="server" Width="112px" Visible="False"></asp:textbox></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 127px; HEIGHT: 17px" vAlign="bottom"><asp:label id="Label49" runat="server" Width="104px" Font-Size="8.5pt" ForeColor="#6795C3"
													Font-Bold="True" Font-Names="Microsoft Sans Serif"> Amount Reqested:</asp:label></TD>
											<TD style="WIDTH: 143px; HEIGHT: 17px" vAlign="bottom"><asp:label id="Label48" runat="server" Width="120px" Font-Size="8.5pt" ForeColor="#6795C3"
													Font-Bold="True" Font-Names="Microsoft Sans Serif">Amount Approved:</asp:label></TD>
											<TD style="WIDTH: 138px; HEIGHT: 17px" vAlign="bottom">
												<asp:label id="Label3" runat="server" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#6795C3"
													Font-Size="8.5pt" Width="128px" Visible="False">Approval Method:</asp:label></TD>
											<TD style="WIDTH: 59px; HEIGHT: 17px" vAlign="bottom"><asp:label id="Label2" runat="server" Font-Size="8.5pt" ForeColor="#6795C3" Font-Bold="True"
													Font-Names="Microsoft Sans Serif" Width="88px" Visible="False"> Max amount:</asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 127px; HEIGHT: 15px" vAlign="top">
												<asp:textbox id="AmtRequestedTextBox" runat="server" Width="105px" ReadOnly="True"></asp:textbox></TD>
											<TD style="WIDTH: 143px; HEIGHT: 15px" vAlign="top">
												<asp:textbox id="AmountConfirmedTextBox" runat="server" Width="112px"></asp:textbox></TD>
											<TD style="WIDTH: 138px; HEIGHT: 15px" vAlign="top">
												<asp:dropdownlist id="Dropdownlist1" runat="server" Width="120px" Visible="False"></asp:dropdownlist></TD>
											<TD style="WIDTH: 59px; HEIGHT: 15px" vAlign="top">
												<asp:textbox id="MaxAmountTextBox" runat="server" Width="112px" Visible="False"></asp:textbox></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 127px; HEIGHT: 15px" vAlign="top"></TD>
											<TD style="WIDTH: 143px; HEIGHT: 15px" vAlign="top"></TD>
											<TD style="WIDTH: 138px; HEIGHT: 15px" vAlign="top"></TD>
											<TD style="WIDTH: 59px; HEIGHT: 15px" vAlign="top"></TD>
										</TR>
									</TABLE>
									<DIV>
										<DIV>
											<asp:button id="Button2" runat="server" Width="81px" Text="Show Report"></asp:button>
											<asp:button id="Button3" runat="server" Width="141px" Text="Save and Send To FC"></asp:button>
											<asp:panel id="Panel1" runat="server" Width="563px" BackColor="White" BorderStyle="Inset" Height="152px">Panel</asp:panel></DIV>
									</DIV>
								</TD>
							</TR>
						</TABLE>
						<DIV>
							<P class="MsoNormal"><SPAN style="FONT-FAMILY: Arial">
									<TABLE id="Table1" style="WIDTH: 392px; cellSpacing: " cellPadding="1" width="392" border="0">
										<TR>
											<TD align="right"><SPAN style="FONT-FAMILY: Arial">$2,000</SPAN></TD>
											<TD><SPAN style="FONT-SIZE: 10pt; FONT-FAMILY: Arial; mso-bidi-font-size: 12.0pt">(fundraising 
													goal)</SPAN></TD>
										</TR>
										<TR>
											<TD align="right">/ 100<U><SPAN style="FONT-FAMILY: Arial"></SPAN></U></TD>
											<TD><SPAN style="FONT-SIZE: 10pt; FONT-FAMILY: Arial; mso-bidi-font-size: 12.0pt">(# of 
													registrants)</SPAN></TD>
										</TR>
										<TR>
											<TD align="right">$20</TD>
											<TD><SPAN style="FONT-SIZE: 10pt; FONT-FAMILY: Arial; mso-bidi-font-size: 12.0pt">(amount 
													you want to raise per child)</SPAN></TD>
										</TR>
										<TR>
											<TD align="right">+ $30</TD>
											<TD>(cost per case of fundraising chocolate)</TD>
										</TR>
										<TR>
											<TD align="right">$50</TD>
											<TD>(your fundraising fee)</TD>
										</TR>
									</TABLE>
									<o:p></o:p></SPAN></P>
							<SPAN style="FONT-SIZE: 10pt; FONT-FAMILY: Arial; mso-bidi-font-size: 12.0pt; mso-fareast-font-family: 'Times New Roman'; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
							</SPAN>
						</DIV>
					</TD>
					<TD style="WIDTH: 12px; HEIGHT: 181px" vAlign="top" align="left"></TD>
					<TD style="WIDTH: 1px; HEIGHT: 181px" vAlign="top" align="left"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

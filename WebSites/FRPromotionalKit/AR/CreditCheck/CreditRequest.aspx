<%@ Register TagPrefix="uc1" TagName="CreditRequestDetails" Src="../../Components/User/CreditRequestDetails.ascx" %>
<%@ Page language="c#" Codebehind="CreditRequest.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.AR.CreditCheck.CreditRequest" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CreditRequest</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Resources/Css/style.css" type="text/css" rel="stylesheet">
		<LINK href="../../Resources/Css/TallySale.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<script language="javascript">

function showPleaseWait()
{
document.getElementById('PleaseWait').style.display = 'block';
}


		</script>
		<form id="Form1" method="post" runat="server">
			<DIV align="left">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 40px; WIDTH: 712px; TOP: 16px; HEIGHT: 488px"
					cellSpacing="0" cellPadding="0" width="712" align="left" border="0">
					<TBODY>
						<TR>
							<TD></TD>
							<TD></TD>
							<TD style="WIDTH: 21px"></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 42px" vAlign="top">
								<TABLE id="Table7" style="WIDTH: 728px; HEIGHT: 30px" height="30" cellSpacing="0" cellPadding="0"
									width="728" border="0">
									<TR class="AlternateItemBackGround">
										<TD></TD>
										<TD class="BigTextBold" align="left">New Credit Requests</TD>
										<TD></TD>
									</TR>
								</TABLE>
							</TD>
							<TD style="HEIGHT: 42px"></TD>
							<TD style="WIDTH: 21px; HEIGHT: 42px"></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 158px" vAlign="top">
								<DIV id="divRequests" style="OVERFLOW: auto; WIDTH: 728px; HEIGHT: 178px" align="left"
									runat="server">
									<DIV><asp:linkbutton id="lnkRefresh" runat="server" CssClass="NormalTextBold Passive TextUnderline" Height="8px"
											Font-Bold="True" Width="88px" onclick="lnkRefresh_Click">Refresh Data</asp:linkbutton></DIV>
									<asp:datagrid id="dgRequests" runat="server" CssClass="NormalText" Width="704px" BackColor="#F7F7F7"
										PageSize="50" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="2px">
										<AlternatingItemStyle CssClass="AlternateItemBackGround"></AlternatingItemStyle>
										<HeaderStyle Font-Size="8.5pt" Font-Bold="True" HorizontalAlign="Left" CssClass="AlternateItemBackGround NormalTextBold Passive"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="credit_check_id"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="contract">
												<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="1%"></HeaderStyle>
												<HeaderTemplate>
													<asp:CheckBox id="chkAll" Checked="False" OnCheckedChanged="ChkAll_CheckedChanged" runat="server"
														AutoPostBack="true" ToolTip="Select/Deselect All"></asp:CheckBox>
												</HeaderTemplate>
												<ItemTemplate>
													<input type="checkbox" runat="server" id="RequestCheckBox" />
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="lead_id"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Lead Name">
												<HeaderStyle Width="20%"></HeaderStyle>
												<ItemTemplate>
													<asp:LinkButton id=LeadNameLink runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.lead_name") %>' OnClick="LeadNameLink_Click">
													</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="FC" HeaderText="FC">
												<HeaderStyle Width="23%"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="amount" HeaderText="Amount">
												<HeaderStyle Width="12%"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="time" HeaderText="Time">
												<HeaderStyle Width="22%"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn Visible="False" SortExpression="part" HeaderText="Request #"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Status">
												<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="10%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<center>
														<asp:DropDownList id="CreditStatusDropDown" onselectedindexChanged="CreditStatusDropDown_SelectedIndexChanged"
															AutoPostBack="true" runat="server" Width="115px" Font-Size="9pt" BackColor="#F7F7F7" />
														<asp:TextBox id="AmtTextBox" BackColor="#ccffcc" runat="server" Visible="False" Width="115px"
															Font-Size="9pt" />
													</center>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="2%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<CENTER>
														<asp:ImageButton id="ReportImageButton" onmouseup="showPleaseWait()" runat="server" OnClick="ReportImageButton_Click"
															ImageUrl="../../Resources/Images/report2.gif"></asp:ImageButton></CENTER>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Score">
												<HeaderStyle Width="5%"></HeaderStyle>
												<ItemTemplate>
													<asp:LinkButton id="ScoreLinkButton" Text='<%# DataBinder.Eval(Container, "DataItem.Credit_Score") %>' OnClick="ScoreLinkButton_Click" runat="server">LinkButton</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="credit_score" HeaderText="score"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><asp:label id="NoRequestsLabel" runat="server" Font-Bold="True" Width="184px" Visible="False"
										Font-Size="12pt" ForeColor="IndianRed">No Requests Found</asp:label></DIV>
							</TD>
							<TD style="HEIGHT: 158px"></TD>
							<TD style="WIDTH: 21px; HEIGHT: 158px"></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 78px" vAlign="top" align="left">
								<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
									<TR>
										<TD style="HEIGHT: 22px" height="22"></TD>
										<TD style="WIDTH: 45px; HEIGHT: 22px" height="22"></TD>
										<TD style="WIDTH: 668px; HEIGHT: 22px" align="right" height="22"><asp:label id="ErrorLabel" runat="server" CssClass="NormalText ImportantMessage" Font-Bold="True"
												Visible="False" Font-Size="8pt">Error: Missing Maximum Amount!  </asp:label><asp:button onmouseup="showPleaseWait()" id="BulkOrderButton" runat="server" CssClass="buttonFlat"
												Text="Bulk Order" onclick="BulkOrderButton_Click"></asp:button><asp:button id="ProcessButton" runat="server" CssClass="buttonFlat" Width="88px" Text="Process " onclick="ProcessButton_Click"></asp:button></TD>
										<TD style="HEIGHT: 22px" align="right" height="22"></TD>
									</TR>
								</TABLE>
								<DIV class="helptext" id="PleaseWait" style="DISPLAY: none; TEXT-ALIGN: center">
									<table id="MyTable" style="WIDTH: 247px; HEIGHT: 8px">
										<tr>
											<td bgColor="#ffffff">
												<h2>
													<asp:label id="ContactingLabel" runat="server" CssClass="NormalTextBold">Contacting Experian...</asp:label><asp:image id="ContactingImage" runat="server" ImageUrl="../../Resources/Images/work.gif"></asp:image></h2>
											</td>
										</tr>
									</table>
								</DIV>
							<TD style="HEIGHT: 78px"></TD>
							<TD style="WIDTH: 21px; HEIGHT: 78px"></TD>
						</TR>
						<TR>
							<TD vAlign="top" align="left" height="1">
								<!-- Start frame -->
								<TABLE height="100" cellSpacing="0" cellPadding="0" width="90%" border="0">
									<TR>
										<TD class="BannerBorderBackground" width="1" height="1"></TD>
										<TD class="BannerBorderBackground" height="1"></TD>
										<TD class="BannerBorderBackground" width="1" height="1"></TD>
									</TR>
									<TR>
										<TD class="BannerBorderBackground" width="1"></TD>
										<TD class="FrameBodyBackground" style="PADDING-RIGHT: 10px; PADDING-LEFT: 10px; PADDING-BOTTOM: 10px; PADDING-TOP: 10px"
											vAlign="top" align="left"><uc1:creditrequestdetails id="CreditRequestDetails1" runat="server"></uc1:creditrequestdetails></TD>
										<TD class="BannerBorderBackground" width="1"></TD>
									</TR>
									<TR>
										<TD class="BannerBorderBackground" width="1" height="1"></TD>
										<TD class="BannerBorderBackground" height="1"></TD>
										<TD class="BannerBorderBackground" width="1" height="1"></TD>
									</TR>
								</TABLE>
								<!--END frame -->
								<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="300" border="0">
									<tr>
										<td style="HEIGHT: 29px" height="29"></td>
									</tr>
									<TR>
										<TD>
											<TABLE id="Table6" height="30" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR class="AlternateItemBackGround">
													<TD></TD>
													<TD class="BigTextBold" align="left">Experian Report</TD>
													<TD></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
									<TR>
										<TD><asp:textbox id="ReportTextBox" runat="server" Height="640px" Width="760px" TextMode="MultiLine"
												Wrap="False" Rows="35"></asp:textbox></TD>
									</TR>
								</TABLE>
							</TD>
							<TD height="1"></TD>
							<TD style="WIDTH: 21px" height="1"></TD>
						</TR>
					</TBODY>
				</TABLE>
			</DIV>
		</form>
	</body>
</HTML>

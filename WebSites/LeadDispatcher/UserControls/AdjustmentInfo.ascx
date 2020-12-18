<%@ Register TagPrefix="uc1" TagName="PaymentAdjusment_Header" Src="PaymentAdjusment_Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MyCalendar" Src="MyCalendar.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AdjustmentInfo.ascx.cs" Inherits="CRMWeb.UserControls.AdjustmentInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table5" style="WIDTH: 576px; HEIGHT: 330px" cellSpacing="0" cellPadding="0"
	width="576" border="0">
	<TR>
		<TD style="WIDTH: 30px; HEIGHT: 30px"></TD>
		<TD style="HEIGHT: 30px"></TD>
		<TD style="WIDTH: 12px; HEIGHT: 30px"></TD>
		<TD style="WIDTH: 12px; HEIGHT: 30px"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 30px">&nbsp;</TD>
		<TD><asp:label id="Label2" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#294585"
				Width="168px" runat="server"> Summary</asp:label></TD>
		<TD style="WIDTH: 12px"></TD>
		<TD style="WIDTH: 12px"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 30px"></TD>
		<TD>
			<TABLE id="Table3" style="WIDTH: 552px; HEIGHT: 24px" cellSpacing="1" cellPadding="1" width="552"
				border="0">
				<TR>
					<TD style="WIDTH: 449px"><uc1:paymentadjusment_header id="PaymentAdjusment_Header1" runat="server"></uc1:paymentadjusment_header></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</TD>
		<TD style="WIDTH: 12px"></TD>
		<TD style="WIDTH: 12px"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 30px; HEIGHT: 18px"></TD>
		<TD style="HEIGHT: 18px"></TD>
		<TD style="WIDTH: 12px; HEIGHT: 18px"></TD>
		<TD style="WIDTH: 12px; HEIGHT: 18px"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 30px; HEIGHT: 3px"></TD>
		<TD style="HEIGHT: 3px"><asp:label id="Label1" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#294585"
				Width="168px" runat="server">Adjustment Details</asp:label></TD>
		<TD style="WIDTH: 12px; HEIGHT: 3px"></TD>
		<TD style="WIDTH: 12px; HEIGHT: 3px"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 30px; HEIGHT: 181px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
		<TD style="HEIGHT: 181px" vAlign="top">
			<TABLE id="Table2" style="WIDTH: 553px; HEIGHT: 128px" cellSpacing="0" cellPadding="0"
				width="553" border="0">
				<TR>
					<TD style="HEIGHT: 146px"></TD>
					<TD style="WIDTH: 541px; HEIGHT: 146px" vAlign="top" align="right">
						<DIV style="OVERFLOW: auto; WIDTH: 550px; HEIGHT: 144px" align="left"><asp:datagrid id="dgAdjustment" Width="520px" runat="server" AllowSorting="True" AutoGenerateColumns="False"
								BackColor="#F7F7F7" BorderColor="#6695C3" BorderWidth="2px">
								<AlternatingItemStyle BackColor="#DEDEE7"></AlternatingItemStyle>
								<HeaderStyle Font-Size="8.5pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="ActiveCaptionText"
									BackColor="#6795C3"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="Adjustment_No" HeaderText="No">
										<HeaderStyle Width="5%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="adjustment_date" HeaderText="Adjustment Date" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="20%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="reason" HeaderText="Reason">
										<HeaderStyle Width="40%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="adjustment_amount" HeaderText="Sale Amount" DataFormatString="{0:$#,##0.00;($#,##0.00);0.00}">
										<HeaderStyle Width="10%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="adjustment_on_shipping" HeaderText="Shipping Amount">
										<HeaderStyle Width="15%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%"></HeaderStyle>
										<ItemTemplate>
											<center>
												<asp:ImageButton id="Edit" runat="server" ImageUrl="../images/Edit2.gif" CommandName="Edit" />
											</center>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>&nbsp;&nbsp;
							<TABLE id="Table1" style="WIDTH: 520px; HEIGHT: 24px" cellSpacing="0" cellPadding="0" width="520"
								border="0">
								<TR>
									<TD><asp:label id="Label10" Font-Bold="True" ForeColor="Green" runat="server">No Adjustments</asp:label></TD>
									<TD></TD>
									<TD align="right"><asp:imagebutton id="ImageButton2" runat="server" ImageUrl="../images/new.gif"></asp:imagebutton></TD>
								</TR>
							</TABLE>
						</DIV>
					</TD>
					<TD style="HEIGHT: 146px" vAlign="top" align="left"></TD>
				</TR>
			</TABLE>
			<DIV></DIV>
			<asp:label id="lblAdd" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#294585"
				Width="168px" runat="server" Visible="False">Add/Edit</asp:label></TD>
		<TD style="WIDTH: 12px; HEIGHT: 181px" vAlign="top"></TD>
		<TD style="WIDTH: 12px; HEIGHT: 181px" vAlign="top"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 30px; HEIGHT: 181px"></TD>
		<TD style="HEIGHT: 181px" vAlign="top" align="left"><asp:panel id="Panel1" Width="536px" runat="server" Visible="False">
				<TABLE id="Table6" style="WIDTH: 549px; HEIGHT: 112px" borderColor="#6695c3" cellSpacing="0"
					cellPadding="0" width="549" bgColor="#f7f7f7" border="1">
					<TR>
						<TD borderColor="#6695c3">
							<TABLE id="Table4" style="WIDTH: 544px; HEIGHT: 64px" cellSpacing="0" cellPadding="1" width="544"
								border="0">
								<TR>
									<TD style="WIDTH: 130px; HEIGHT: 19px">
										<asp:Label id="Label3" runat="server" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
											Font-Size="8.5pt">Adjustment Date:</asp:Label></TD>
									<TD style="WIDTH: 148px; HEIGHT: 19px" vAlign="bottom">
										<asp:TextBox id="txtAdjDate" runat="server" Width="120px" BorderWidth="1px" BorderColor="#6695C3"
											BorderStyle="Solid"></asp:TextBox>
										<asp:ImageButton id="calAdjustment" runat="server" ImageUrl="../images/calCalendar.gif"></asp:ImageButton></TD>
									<TD style="HEIGHT: 19px">
										<asp:Label id="Label6" runat="server" Width="128px" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
											Font-Size="8.5pt" Height="16px">Adj. Shipping Amount:</asp:Label></TD>
									<TD style="HEIGHT: 19px">
										<asp:TextBox id="txtShippingAmt" runat="server" Width="120px" BorderWidth="1px" BorderColor="#6695C3"
											BorderStyle="Solid"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 130px">
										<asp:Label id="Label5" runat="server" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
											Font-Size="8.5pt">Adjustment Reason:</asp:Label></TD>
									<TD style="WIDTH: 148px">
										<asp:DropDownList id="cboReason" runat="server" Width="136px"></asp:DropDownList></TD>
									<TD>
										<asp:Label id="Label8" runat="server" Width="104px" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
											Font-Size="8.5pt">Adj. Tax Amount:</asp:Label></TD>
									<TD>
										<asp:TextBox id="txtTaxAmount" runat="server" Width="120px" BorderWidth="1px" BorderColor="#6695C3"
											BackColor="#E0E0E0" BorderStyle="Solid" ReadOnly="True"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 130px; HEIGHT: 5px">
										<asp:Label id="Label4" runat="server" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
											Font-Size="8.5pt">Adj. Sale Amount:</asp:Label></TD>
									<TD style="WIDTH: 148px; HEIGHT: 5px">
										<asp:TextBox id="txtAmount" runat="server" Width="120px" BorderWidth="1px" BorderColor="#6695C3"
											BorderStyle="Solid"></asp:TextBox></TD>
									<TD style="HEIGHT: 5px">
										<asp:Label id="Label9" runat="server" Width="112px" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
											Font-Size="8.5pt">Adj. Total Amount:</asp:Label></TD>
									<TD style="HEIGHT: 5px">
										<asp:TextBox id="txtNameOnCard" runat="server" Width="120px" BorderWidth="1px" BorderColor="#6695C3"
											BackColor="#E0E0E0" BorderStyle="Solid" ReadOnly="True"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 130px; HEIGHT: 11px" colSpan="1">
										<asp:Label id="Label7" runat="server" Width="104px" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
											Font-Size="8.5pt">Comments:</asp:Label></TD>
									<TD style="HEIGHT: 11px" colSpan="3">
										<asp:TextBox id="txtComments" runat="server" Width="136px" BorderWidth="1px" BorderColor="#6695C3"
											BorderStyle="Solid" Height="32px" TextMode="MultiLine"></asp:TextBox>
										<asp:TextBox id="txtAdjNo" runat="server" Width="24px" Height="16px"></asp:TextBox></TD>
								</TR>
							</TABLE>
							<asp:ImageButton id="cmdUpdate" runat="server" ImageUrl="../images/update2.gif"></asp:ImageButton>
							<asp:ImageButton id="cmdAdd" runat="server" ImageUrl="../images/add.gif"></asp:ImageButton>
							<asp:ImageButton id="cmdCancel" runat="server" ImageUrl="../images/cancel2.gif"></asp:ImageButton></TD>
					</TR>
				</TABLE>
			</asp:panel></TD>
		<TD style="WIDTH: 12px; HEIGHT: 181px" vAlign="top" align="left"></TD>
		<TD style="WIDTH: 12px; HEIGHT: 181px" vAlign="top" align="left"><uc1:mycalendar id="MyCalendar1" runat="server" Visible="False"></uc1:mycalendar></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 30px"></TD>
		<TD></TD>
		<TD></TD>
	</TR>
</TABLE>

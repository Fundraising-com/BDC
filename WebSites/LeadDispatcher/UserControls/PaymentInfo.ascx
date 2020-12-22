<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PaymentInfo.ascx.cs" Inherits="CRMWeb.UserControls.PaymentInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="MyCalendar" Src="MyCalendar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PaymentAdjusment_Header" Src="PaymentAdjusment_Header.ascx" %>
<P>
	<TABLE id="Table1" style="WIDTH: 576px; HEIGHT: 330px" cellSpacing="0" cellPadding="0"
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
					Width="168px" runat="server">Payment Details</asp:label></TD>
			<TD style="WIDTH: 12px; HEIGHT: 3px"></TD>
			<TD style="WIDTH: 12px; HEIGHT: 3px"></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 30px; HEIGHT: 181px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
			<TD style="HEIGHT: 181px" vAlign="top">
				<TABLE id="Table2" style="WIDTH: 552px; HEIGHT: 181px" cellSpacing="0" cellPadding="0"
					width="552" border="0">
					<TR>
						<TD style="HEIGHT: 162px"></TD>
						<TD style="WIDTH: 541px; HEIGHT: 162px" vAlign="top" align="right">
							<div style="OVERFLOW: auto; WIDTH: 544px; HEIGHT: 136px" align="left">&nbsp;&nbsp;
								<asp:datagrid id="dgPayments" Width="520px" runat="server" Height="64px" BorderWidth="2px" BorderColor="#6695C3"
									BackColor="#F7F7F7" AutoGenerateColumns="False" AllowSorting="True">
									<AlternatingItemStyle BackColor="#DEDEE7"></AlternatingItemStyle>
									<HeaderStyle Font-Size="8.5pt" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="ActiveCaptionText"
										BackColor="#6795C3"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="Payment_No" HeaderText="No">
											<HeaderStyle Width="5%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="payment_entry_date" HeaderText="Payment Date" DataFormatString="{0:dd/MM/yyyy}">
											<HeaderStyle Width="20%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="payment_amount" HeaderText="Amount" DataFormatString="{0:$#,##0.00;($#,##0.00);0.00}">
											<HeaderStyle Width="15%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="collection_status" HeaderText="Collection Status">
											<HeaderStyle Width="15%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Payment_method" HeaderText="Payment Method">
											<HeaderStyle Width="15%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="cashable_date" HeaderText="Cashable Date" DataFormatString="{0:dd/MM/yyyy}">
											<HeaderStyle Width="20%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="Credit_card_no" HeaderText="Credit Card #"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="name_on_card" HeaderText="Name On Card"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="expiry_date" HeaderText="Exp. Date"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="authorization_number" HeaderText="Authorization No"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="commission_paid" HeaderText="Commission Paid"></asp:BoundColumn>
										<asp:TemplateColumn>
											<ItemTemplate>
												<center>
													<asp:ImageButton id="Edit" runat="server" ImageUrl="../images/Edit2.gif" CommandName="Edit" />
												</center>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
								</asp:datagrid></div>
							<asp:imagebutton id="ImageButton2" runat="server" ImageUrl="../images/new.gif"></asp:imagebutton>&nbsp; 
							&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;</TD>
						<TD style="HEIGHT: 162px" vAlign="top" align="left"></TD>
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
					<TABLE id="Table6" borderColor="#6695c3" cellSpacing="0" cellPadding="0" width="300" bgColor="#f7f7f7"
						border="1">
						<TR>
							<TD borderColor="#6695c3">
								<TABLE id="Table4" style="WIDTH: 544px; HEIGHT: 96px" cellSpacing="0" cellPadding="1" width="544"
									border="0">
									<TR>
										<TD style="WIDTH: 130px">
											<asp:Label id="Label3" runat="server" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="8.5pt">Payment Date:</asp:Label></TD>
										<TD style="WIDTH: 148px" vAlign="bottom">
											<asp:TextBox id="txtPaymentDate" runat="server" Width="120px" BorderColor="#6695C3" BorderWidth="1px"
												BorderStyle="Solid"></asp:TextBox>
											<asp:ImageButton id="calPayment" runat="server" ImageUrl="../images/calCalendar.gif"></asp:ImageButton></TD>
										<TD>
											<asp:Label id="Label8" runat="server" Width="88px" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="8.5pt">Credit Card:</asp:Label></TD>
										<TD>
											<asp:TextBox id="txtCreditCard" runat="server" Width="120px" BorderColor="#6695C3" BorderWidth="1px"
												BorderStyle="Solid"></asp:TextBox>
											<asp:TextBox id="txtPaymentNo" runat="server" Width="24px" Height="16px"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 130px">
											<asp:Label id="Label4" runat="server" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="8.5pt">Sale Amount:</asp:Label></TD>
										<TD style="WIDTH: 148px">
											<asp:TextBox id="txtAmount" runat="server" Width="120px" BorderColor="#6695C3" BorderWidth="1px"
												BorderStyle="Solid"></asp:TextBox></TD>
										<TD>
											<asp:Label id="Label9" runat="server" Width="88px" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="8.5pt">Name On Card:</asp:Label></TD>
										<TD>
											<asp:TextBox id="txtNameOnCard" runat="server" Width="120px" BorderColor="#6695C3" BorderWidth="1px"
												BorderStyle="Solid"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 130px; HEIGHT: 2px">
											<asp:Label id="Label5" runat="server" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="8.5pt">Collection Status:</asp:Label></TD>
										<TD style="WIDTH: 148px; HEIGHT: 2px">
											<asp:DropDownList id="cboCollectionStatus" runat="server" Width="100px"></asp:DropDownList></TD>
										<TD style="HEIGHT: 2px">
											<asp:Label id="Label10" runat="server" Width="88px" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="8.5pt">Exp. Date:</asp:Label></TD>
										<TD style="HEIGHT: 2px">
											<asp:TextBox id="txtExpDate" runat="server" Width="120px" BorderColor="#6695C3" BorderWidth="1px"
												BorderStyle="Solid"></asp:TextBox>
											<asp:ImageButton id="calExp" runat="server" ImageUrl="../images/calCalendar.gif"></asp:ImageButton></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 130px; HEIGHT: 18px">
											<asp:Label id="Label6" runat="server" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="8.5pt">Payment Method:</asp:Label></TD>
										<TD style="WIDTH: 148px; HEIGHT: 18px">
											<asp:DropDownList id="cboPaymentMethod" runat="server" Width="100px"></asp:DropDownList></TD>
										<TD style="HEIGHT: 18px">
											<asp:Label id="Label11" runat="server" Width="104px" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="8.5pt">Authorization No:</asp:Label></TD>
										<TD style="HEIGHT: 18px">
											<asp:TextBox id="txtAuthNo" runat="server" Width="120px" BorderColor="#6695C3" BorderWidth="1px"
												BorderStyle="Solid"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 130px">
											<asp:Label id="Label7" runat="server" Width="88px" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="8.5pt">Cashable Date:</asp:Label></TD>
										<TD style="WIDTH: 148px">
											<asp:TextBox id="txtCashableDate" runat="server" Width="120px" BorderColor="#6695C3" BorderWidth="1px"
												BorderStyle="Solid"></asp:TextBox>
											<asp:ImageButton id="calCashable" runat="server" ImageUrl="../images/calCalendar.gif"></asp:ImageButton></TD>
										<TD>
											<asp:Label id="Label12" runat="server" Width="96px" ForeColor="#6795C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
												Font-Size="8.5pt">Commision Paid:</asp:Label></TD>
										<TD>
											<asp:CheckBox id="chkCommission" runat="server" Width="136px" Height="24px"></asp:CheckBox></TD>
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
</P>

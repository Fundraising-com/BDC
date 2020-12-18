<%@ Page language="c#" Codebehind="Refund.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.Sales.CreditCard.Refund" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Refund</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Resources/Css/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			Sale ID :
			<asp:TextBox id="SaleIdTextBox" runat="server" Width="65px"></asp:TextBox><br>
			Amount :
<asp:TextBox id="TextBoxAmount" runat="server" Width="64px" 
                ondatabinding="AmountTextBox_DataBinding"></asp:TextBox>
			<asp:Button id="ButtonGetAmount" runat="server" Text="Last Payment" 
                CausesValidation="False" onclick="ButtonGetAmount_Click" Font-Size="8pt" 
                Height="17px" Width="75px" Visible="False"></asp:Button>
            <br />
			<asp:panel id="Panel2" runat="server">
				<P>Credit Card info :</P>
				<TABLE class="NormalText" border="0">
					<TR>
						<TD>Card Type :</TD>
						<TD>
							<asp:dropdownlist id="DropDownListCCType" runat="server">
								<asp:ListItem Value="2" Selected="True">Visa</asp:ListItem>
								<asp:ListItem Value="3">MasterCard</asp:ListItem>
								<asp:ListItem Value="8">Amex</asp:ListItem>
								<asp:ListItem Value="9">Discover</asp:ListItem>
							</asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD>Credit Card Number :</TD>
						<TD>
							<asp:textbox id="TextBoxCCNumber" runat="server" MaxLength="16"></asp:textbox>
							<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="* Credit Card number is required."
								ControlToValidate="TextBoxCCNumber"></asp:RequiredFieldValidator></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 3px">Expiry Date :</TD>
						<TD style="HEIGHT: 3px">
							<asp:dropdownlist id="DropDownListMonth" runat="server">
								<asp:ListItem Value="01" Selected="True">01</asp:ListItem>
								<asp:ListItem Value="02">02</asp:ListItem>
								<asp:ListItem Value="03">03</asp:ListItem>
								<asp:ListItem Value="04">04</asp:ListItem>
								<asp:ListItem Value="05">05</asp:ListItem>
								<asp:ListItem Value="06">06</asp:ListItem>
								<asp:ListItem Value="07">07</asp:ListItem>
								<asp:ListItem Value="08">08</asp:ListItem>
								<asp:ListItem Value="09">09</asp:ListItem>
								<asp:ListItem Value="10">10</asp:ListItem>
								<asp:ListItem Value="11">11</asp:ListItem>
								<asp:ListItem Value="12">12</asp:ListItem>
							</asp:dropdownlist>&nbsp;/&nbsp;
							<asp:dropdownlist id="DropDownListYear" runat="server">
								<asp:ListItem Value="09">2009</asp:ListItem>
								<asp:ListItem Value="10">2010</asp:ListItem>
								<asp:ListItem Value="11">2011</asp:ListItem>
								<asp:ListItem Value="12">2012</asp:ListItem>
								<asp:ListItem Value="13">2013</asp:ListItem>
								<asp:ListItem Value="14">2014</asp:ListItem>
								<asp:ListItem Value="15">2015</asp:ListItem>
							</asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD>Name on CC :</TD>
						<TD>
							<asp:textbox id="TextBoxName" runat="server" Width="184px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD vAlign="bottom">&nbsp;</TD>
						<TD>&nbsp;<asp:RadioButtonList id="SecurityRadioButtonList0" runat="server" Width="96px" 
                                Height="24px" RepeatDirection="Horizontal"
								CssClass="NormalText" Visible="False">
								<asp:ListItem Value="true">Yes</asp:ListItem>
								<asp:ListItem Value="false" Selected="True">No</asp:ListItem>
							</asp:RadioButtonList>
							<asp:textbox id="TextBoxCVV3" runat="server" Width="64px" Visible="False"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>Street Address :</TD>
						<TD>
							<asp:textbox id="TextBoxStreetAddress" runat="server"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>City :</TD>
						<TD>
							<asp:textbox id="TextBoxCity" runat="server" Width="144px" 
                                ontextchanged="TextBoxCity0_TextChanged"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>State :</TD>
						<TD>
							<asp:textbox id="TextBoxState" runat="server" Width="32px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>Zip Code :</TD>
						<TD>
							<asp:textbox id="TextBoxZipCode" runat="server" Width="56px"></asp:textbox></TD>
					</TR>
				</TABLE>
				<P>
					<asp:Button ID="ButtonSendRefund" runat="server" CausesValidation="False" 
                        onclick="ButtonSendRefund_Click" Text="Send Refund Request" Width="137px" />
            </asp:panel>
            <table style="width:100%;">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                    <asp:DataGrid ID="SalesDataGrid" runat="server" AutoGenerateColumns="False" 
                        CellPadding="2" Font-Names="Arial" Font-Size="11pt" ShowFooter="True" 
                        Width="650px">
                        <AlternatingItemStyle BackColor="#DEDEE7" />
                        <HeaderStyle BackColor="#6795C3" Font-Bold="True" Font-Size="10pt" />
                        <FooterStyle BackColor="#6795C3" Font-Bold="True" Font-Size="10pt" />
                        <Columns>
                            <asp:BoundColumn DataField="sale_id" HeaderText="Sale ID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="refund_amount" HeaderText="Refund Amount">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="status_code" HeaderText="Refund Status">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="request_date" HeaderText="Request Date">
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="ButtonNext1" runat="server" CausesValidation="False" 
                            onclick="ButtonNext_Click" Text="Update Payments" />
                        <asp:Button ID="ButtonRefresh" runat="server" CausesValidation="False" 
                            onclick="ButtonRefresh_Click" Text="Refresh" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <br>
			<br>
			<br>
			<asp:Panel id="PanelMain" runat="server">
                <asp:Panel ID="PanelCredit" runat="server" CssClass="NormalText" 
                    Enabled="False">
                    Recent Refunds :
                    <p>
                        &nbsp;</p>
                </asp:Panel>
                <p>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                </p>
            </asp:Panel>
		</form>
	</body>
</HTML>

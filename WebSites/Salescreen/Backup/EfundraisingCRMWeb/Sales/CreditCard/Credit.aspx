<%@ Page language="c#" Codebehind="Credit.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.Sales.CreditCard.Credit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Refund</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Resources/Css/TallySale.css" type="text/css" rel="stylesheet">
		<LINK href="../../Resources/Css/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:Label id="LabelStatus" runat="server" Visible="False"></asp:Label>
			<br>
			<asp:radiobutton id="RadioButtonDataGrid" runat="server" Checked="True" AutoPostBack="True" Text="Select the payment(s) you want cancel :" oncheckedchanged="RadioButtonDataGrid_CheckedChanged"></asp:radiobutton><br>
			<asp:panel id="PanelDataGrid" runat="server">
				<asp:datagrid id="PaymentsDataGrid" runat="server" Font-Size="11pt" Font-Names="Arial" AutoGenerateColumns="False"
					CellPadding="2" ShowFooter="True" Width="704px">
					<AlternatingItemStyle BackColor="#DEDEE7"></AlternatingItemStyle>
					<HeaderStyle Font-Size="10pt" Font-Bold="True" BackColor="#6795C3"></HeaderStyle>
					<FooterStyle Font-Size="10pt" Font-Bold="True" BackColor="#6795C3"></FooterStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
							<HeaderTemplate>
								<asp:Label id="Label2" runat="server">Select :</asp:Label><BR>
								<asp:LinkButton id="LinkButtonAll" onclick="LinkButtonAll_Click" runat="server" CausesValidation="False">All</asp:LinkButton>
								<asp:Label id="Label3" runat="server">/</asp:Label>
								<asp:LinkButton id="LinkButtonNone" onclick="LinkButtonNone_Click" runat="server" CausesValidation="False">None</asp:LinkButton>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="CheckBoxInclude" runat="server" OnCheckedChanged="CheckBoxInclude_CheckeckChanged"
									AutoPostBack="True"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="sale_id" HeaderText="Sale Id">
							<ItemStyle Font-Bold="True"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="payment_no" HeaderText="Payment No"></asp:BoundColumn>
						<asp:BoundColumn DataField="payment_method" HeaderText="Payment Method"></asp:BoundColumn>
						<asp:BoundColumn DataField="payment_entry_date" HeaderText="Entry Date"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="amount"></asp:BoundColumn>
						<asp:BoundColumn DataField="amount_display" HeaderText="Amount"></asp:BoundColumn>
						<asp:BoundColumn DataField="foreign_orderid" HeaderText="Paymentech OrderID"></asp:BoundColumn>
					</Columns>
				</asp:datagrid>
			</asp:panel><br>
			<asp:radiobutton id="RadioButtonAmountInput" runat="server" Checked="False" AutoPostBack="True" Text="OR input the amount manually :" oncheckedchanged="RadioButtonAmountInput_CheckedChanged"></asp:radiobutton><br>
			<asp:panel id="PanelAmountInput" runat="server" Enabled="False">
				<asp:textbox id="TextBoxAmount" runat="server" Width="64px" Enabled="False">0</asp:textbox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="TextBoxAmount"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="This is not a valid amount."
					ControlToValidate="TextBoxAmount" ValidationExpression="[\d.]+"></asp:RegularExpressionValidator>
				<BR>
				<I>When using manual entry, the amount will be split equally between each sales.</I>
			</asp:panel><br>
			<br>
			<asp:panel id="Panel1" runat="server">
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
							<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="* Credit Card number is required."
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
								<asp:ListItem Value="10" Selected="True">2010</asp:ListItem>
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
						<TD vAlign="bottom">Security Number :</TD>
						<TD>Verify Security Code ? (Does NOT work with AMEX)&nbsp;:
							<asp:RadioButtonList id="SecurityRadioButtonList" runat="server" Width="96px" Height="24px" RepeatDirection="Horizontal"
								CssClass="NormalText">
								<asp:ListItem Value="true">Yes</asp:ListItem>
								<asp:ListItem Value="false" Selected="True">No</asp:ListItem>
							</asp:RadioButtonList>
							<asp:textbox id="TextBoxCVV2" runat="server" Width="64px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>Street Address :</TD>
						<TD>
							<asp:textbox id="TextBoxStreetAddress" runat="server"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>City :</TD>
						<TD>
							<asp:textbox id="TextBoxCity" runat="server" Width="144px"></asp:textbox></TD>
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
					<asp:Button id="ButtonCredit0" runat="server" Text="Credit" 
                        onclick="ButtonCredit_Click"></asp:Button>
			        <asp:Button ID="ButtonCredit1" runat="server" onclick="ButtonCreditNew_Click" 
                        Text="CreditNEW" />
            </asp:panel>
		</P>
	    </form>
		</body>
</HTML>

<%@ Page language="c#" Codebehind="PayDeposit.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Finance.payDeposit" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Account Receivable</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE><LINK href="Includes/QSPFulfillment.css" type=text/css rel=stylesheet >
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
  </HEAD>
<body leftMargin=0 topMargin=0 marginheight="0" 
marginwidth="0">
<form id=Form1 method=post runat="server">
			<!-- #include file="../Includes/Menu.inc" --><asp:label id=LblBankDeposit runat="server" Font-Size="Large" Font-Names="Verdana" ForeColor="#2F4F88" align="center">Pay Deposit</asp:label>
			<!-- Messages -->
<table>
  <tr>
    <td><asp:label id=lblReportTotalDeposit runat="server" Enabled="False" Visible="False"></asp:label></td></tr>
  <tr>
    <td><asp:label id=lblReportTotalPayment runat="server" Enabled="False" Visible="False"></asp:label></td></tr>
  <tr>
    <td><asp:label id=lblBDID runat="server" Enabled="False" Visible="False"></asp:label></td></tr>
  <tr>
    <td><asp:label id=lblPgCnt runat="server" Enabled="False" Visible="False">PgCnt</asp:label></td></tr></table>
			
			<!-- Deposits Data -->
<table>
  <tr>
    <td colSpan=2><asp:button id=pbAddNewDeposit runat="server" Text="Add New Record" CssClass="button2" onclick="pbAddNewDeposit_Click"></asp:button></td></tr>
  <tr>
    <td align=left><asp:label id=LblStatus runat="server" Font-Size="Larger" Font-Names="Verdana" ForeColor="#2F4F88" CssClass="font7boldvblue">Status</asp:label><asp:dropdownlist id=ddlDepositStatus runat="server" CssClass="boxlookw" AutoPostBack="True" DataValueField="instance" DataTextField="description" onselectedindexchanged="ddlDepositStatus_SelectedIndexChanged"></asp:dropdownlist><asp:label id=LblSearch runat="server" Font-Size="Larger" Font-Names="Verdana" CssClass="font7boldvblue">Search</asp:label><asp:label id=LblSearchFrom runat="server" Font-Size="Larger" Font-Names="Verdana" ForeColor="#2F4F88" Visible="False" CssClass="font7boldvblue">Search From</asp:label><asp:textbox id=Searchbox runat="server" Font-Size="XX-Small" CssClass="textbox2" Width="100px"></asp:textbox><asp:label id=LblSearchDate2 runat="server" Font-Size="Larger" Font-Names="Verdana" Visible="False" CssClass="font7boldvblue">To</asp:label><asp:textbox id=SearchboxDate runat="server" Font-Size="XX-Small" Visible="False" CssClass="textbox2" Width="100px">
						</asp:textbox><asp:label id=lblSearchBy runat="server" Font-Size="Larger" Font-Names="Verdana" ForeColor="#2F4F88" CssClass="font7boldvblue" Font-Bold="True">Search By</asp:label><asp:dropdownlist id=ddlSearchBy runat="server" CssClass="boxlookw" AutoPostBack="True" onselectedindexchanged="ddlSearchBy_SelectedIndexChanged">
							<asp:ListItem Value="BANK_ACCOUNT_NUMBER">Account #</asp:ListItem>
							<asp:ListItem Value="DEPOSIT_AMOUNT">Amount</asp:ListItem>
							<asp:ListItem Value="DEPOSIT_DATE">Deposit Dt</asp:ListItem>
							<asp:ListItem Value="BANK_DEPOSIT_ID">Deposit Id</asp:ListItem>
							<asp:ListItem Value="ITEM_COUNT"># Of Item </asp:ListItem>
						</asp:dropdownlist><asp:button id=pbSearch runat="server" Font-Names="Verdana" ForeColor="#2F4F88" Text="Go" CssClass="boxlook" onclick="pbSearch_Click"></asp:button></td>
    <td align=right><asp:label id=Label3 runat="server" Font-Size="Larger" Font-Names="Verdana" ForeColor="#2F4F88" CssClass="font7boldvblue">View</asp:label><asp:dropdownlist id=ddlPageSize runat="server" CssClass="boxlookw" AutoPostBack="True" onselectedindexchanged="ddlPageSize_SelectedIndexChanged">
							<asp:ListItem Value="10">10</asp:ListItem>
							<asp:ListItem Value="25">25</asp:ListItem>
							<asp:ListItem Value="50">50</asp:ListItem>
							<asp:ListItem Value="All">All</asp:ListItem>
						</asp:dropdownlist></td></tr>
  <tr>
    <td colSpan=2><asp:datagrid id=dgDeposits runat="server" ShowFooter="True" DataKeyField="Bank_Deposit_Id" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" CellPadding="0">
<SelectedItemStyle Font-Bold="True" ForeColor="#2F4F88">
</SelectedItemStyle>

<ItemStyle BackColor="DarkGray">
</ItemStyle>

<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="0.5cm" VerticalAlign="Middle" BackColor="Navy">
</HeaderStyle>

<FooterStyle HorizontalAlign="Right" Height="0.5cm" BorderWidth="1px" ForeColor="Black" BorderStyle="None" BorderColor="#2F4F88" BackColor="DarkGray">
</FooterStyle>

<Columns>
<asp:TemplateColumn>
<ItemTemplate>
										<asp:LinkButton id="LinkButton1" runat="server" Text="<img border=0 alt='Deposit Detail' align=center src=../Images/Details.gif> " CommandName="ShowPayments"></asp:LinkButton>
									
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="bank_Deposit_Id" HeaderText="Deposit Id">
<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
										<asp:Label id=bank_deposit_id runat="server" Width="70px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "bank_deposit_id") %>'>
										</asp:Label>
									
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="bank_Deposit_Status_id" HeaderText="Deposit Status">
<ItemStyle HorizontalAlign="Left">
</ItemStyle>

<ItemTemplate>
<asp:Label id=Bank_Deposit_status runat="server" CssClass="font8v" Text='<%# getDepositStatusDesc((int)(DataBinder.Eval(Container.DataItem, "bank_deposit_status_id"))) %>' Width="160px">
										</asp:Label>
</ItemTemplate>

<FooterTemplate>
<asp:Label id=Label1 runat="server" CssClass="font8boldv">Total Deposits:</asp:Label>
</FooterTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="Bank_Account_Id" HeaderText="Acct Id">
<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
										<asp:Label id=Bank_Account_id runat="server" Width="48px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "bank_account_id") %>'>
										</asp:Label>
									
</ItemTemplate>

<FooterTemplate>
										<asp:Label id=lblDepositRecCount runat="server" Width="56px" CssClass="font8boldv" Text="<%# lblPgCnt.Text %>">
										</asp:Label>
									
</FooterTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="BANK_ACCOUNT_NUMBER" HeaderText="Acct No.">
<ItemTemplate>
										<asp:Label id=Bank_Account_number runat="server" Width="150px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "bank_account_number") %>'>
										</asp:Label>
									
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="Bank_account_Name" HeaderText="Bank Name">
<ItemTemplate>
										<asp:Label id=Bank_account_Name runat="server" Width="160px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "bank_account_Name") %>'>
										</asp:Label>
									
</ItemTemplate>

<FooterTemplate>
										<P>&nbsp;
											<asp:Label id="lblReportTotalAmount" runat="server" Width="160px" CssClass="font8boldv">Report Deposit Total:</asp:Label></P>
									
</FooterTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="deposit_Amount" HeaderText="Amount">
<ItemStyle HorizontalAlign="Right">
</ItemStyle>

<ItemTemplate>
										<asp:Label id=Deposit_Amount runat="server" Width="80px" CssClass="Font8v" Text='<%# DataBinder.Eval(Container.DataItem, "deposit_amount") %>'>
										</asp:Label>
									
</ItemTemplate>

<FooterTemplate>
										<P>
											<asp:Label id=lblDepositAmountReport runat="server" Width="136px" CssClass="Font8boldv" Text="<%# lblReportTotalDeposit.Text %>">
											</asp:Label></P>
									
</FooterTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="Deposit_Date" HeaderText="Date">
<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
										<asp:Label id=Label4 runat="server" Width="80px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "deposit_date") %>'>
										</asp:Label>
									
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="Item_count" HeaderText="# Deposited">
<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
										<asp:Label id=item_count runat="server" Width="85px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "item_count") %>'>
										</asp:Label>
									
</ItemTemplate>
</asp:TemplateColumn>
</Columns>

<PagerStyle VerticalAlign="Bottom" NextPageText="Page" Height="0.5cm" Font-Size="Small" Font-Names="Verdana" Font-Bold="True" PrevPageText="Page" HorizontalAlign="Right" ForeColor="White" BackColor="Navy" Wrap="False" Mode="NumericPages">
</PagerStyle>
						</asp:datagrid></td></tr></table>
			
			<!-- Payments Data -->
<table <tr>
  <TR>
    <td colSpan=2><asp:label id=lblMessage runat="server" CssClass="Font8BoldVRed"></asp:label></td></TR>
  <tr>
    <td colSpan=2><asp:label id=lblPayments runat="server" Font-Size="Medium" Font-Names="Verdana" ForeColor="#2F4F88" Visible="False">Payments</asp:label></td></tr>
  <tr>
    <td align=left><asp:label id=lblSearchPayment runat="server" Font-Size="Larger" Font-Names="Verdana" ForeColor="#2F4F88" Visible="False" CssClass="font7boldvblue">Search</asp:label><asp:label id=lblSearchPFrom runat="server" Font-Size="Larger" Font-Names="Verdana" Visible="False" CssClass="font7boldvblue">Search From</asp:label><asp:textbox id=SearchPaymentBox runat="server" Font-Size="XX-Small" Visible="False" CssClass="textbox2"></asp:textbox><asp:label id=lblSearchPDate2 runat="server" Font-Size="Larger" Font-Names="Verdana" Visible="False" CssClass="font7boldvblue">To</asp:label><asp:textbox id=SearchboxPDate runat="server" Font-Size="XX-Small" Visible="False" CssClass="textbox2"></asp:textbox><asp:label id=lblSearchPaymentBy runat="server" Font-Size="Larger" Font-Names="Verdana" ForeColor="#2F4F88" Visible="False" CssClass="font7boldvblue">Search By</asp:label><asp:dropdownlist id=ddlSearchPaymentBy runat="server" Visible="False" CssClass="boxlookw" AutoPostBack="True" onselectedindexchanged="ddlSearchPaymentBy_SelectedIndexChanged">
							<asp:ListItem Value="PAYMENT_ID">Payment Id</asp:ListItem>
							<asp:ListItem Value="CHEQUE_NUMBER">Cheque #</asp:ListItem>
							<asp:ListItem Value="PAYMENT_AMOUNT">Payment Amount</asp:ListItem>
							<asp:ListItem Value="ORDER_ID">Order Id</asp:ListItem>
							<asp:ListItem Value="CAMPAIGN_ID">Campaign Id</asp:ListItem>
							<asp:ListItem Value="CHEQUE_DATE">Cheque Date</asp:ListItem>
						</asp:dropdownlist><asp:button id=pbSearchPayment runat="server" Visible="False" Text="Go" CssClass="button2" onclick="pbSearchPayment_Click"></asp:button></td>
    <td align=right><asp:button id=pbHidePayments runat="server" Visible="False" Text="Hide Payments" CssClass="boxlook" onclick="pbHidePayments_Click">
						</asp:button></td></tr>
  <tr>
    <td colSpan=2><asp:datagrid id=dgPayments runat="server" ForeColor="Black" ShowFooter="True" DataKeyField="Payment_Id" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" CellPadding="0" BackColor="White" PageSize="6" HorizontalAlign="Left">
							<ItemStyle BorderWidth="1px" ForeColor="Black" BackColor="DarkGray"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="0.5cm" ForeColor="White" VerticalAlign="Middle"
								BackColor="Navy"></HeaderStyle>
							<FooterStyle HorizontalAlign="Right" Height="0.5cm" BorderWidth="1px" ForeColor="Black" BorderColor="#2F4F88"
								BackColor="DarkGray"></FooterStyle>
							<Columns>
								<asp:TemplateColumn SortExpression="payment_Id" HeaderText="Pay Id">
									<ItemStyle HorizontalAlign="Center" ForeColor="Gray"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=PaymentId runat="server" Width="64px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "payment_id") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="cheque_number" HeaderText="Cheque #">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=ChequeNumber runat="server" Width="115px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "cheque_number") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="Label2" runat="server" Width="120px" CssClass="font8boldv">Payment Count:</asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="cheque_date" HeaderText="Cheque Dt">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=ChequeDate runat="server" Width="71px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "cheque_date") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id=lblPaymentRecCount runat="server" CssClass="font8boldv" Text="<%# lblPaymentCount.Text %>">
										</asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Cheque_Payer" HeaderText="Payer">
									<ItemTemplate>
										<asp:Label id=ChequePayer runat="server" Width="330px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "cheque_payer") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="lblReportPaymentAmount" runat="server" Width="176px" CssClass="font8boldv">Payment Total:</asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Payment_Amount" HeaderText="Amount">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=PaymentAmount runat="server" Width="136px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "Payment_amount") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id=lblPaymentAmountReport runat="server" Width="120px" CssClass="Font8boldv" Text="<%# lblReportTotalPayment.Text %>">
										</asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Payment_Effective_Date" HeaderText="Effect Dt">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=EffectiveDate runat="server" Width="71px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "Payment_Effective_Date") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Order_Id" HeaderText="Order Id">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=OrderId runat="server" Width="64px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "Order_Id") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Campaign_Id" HeaderText="CA Id">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=CampaignId runat="server" Width="64px" CssClass="font8v" Text='<%# DataBinder.Eval(Container.DataItem, "Campaign_Id") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Height="0.5cm" Font-Size="Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Right"
								ForeColor="White" BackColor="Navy" Wrap="False" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td></tr>
  <tr>
    <td colSpan=2><asp:label id=lblPaymentCount runat="server" Enabled="False" Visible="False"></asp:label></td></tr></table></form>
	</body>
</HTML>

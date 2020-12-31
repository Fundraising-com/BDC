<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="UC" TagName="Date" Src="~/Common/DateEntry.ascx" %>
<%@Page   language="c#" Codebehind="BankDepositAdd.aspx.cs"   AutoEventWireup="True" Inherits="QSPFulfillment.Finance.BankDepositAdd" aspCompat="True"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BankDepositAdd</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<LINK href="Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<!--#include file="../Includes/Menu.inc"-->
			<table>
				<tr>
					<td colSpan="6"><asp:label id="lblAddBankDeposit" runat="server" CssClass="font10boldv" Font-Bold="True" ForeColor="#2F4F88">Add Bank Deposit</asp:label></td>
				</tr>
				<tr>
					<td><asp:label id="lblBankDepositStatus" runat="server" Font-Names="Verdana" Font-Size="X-Small">Deposit Status</asp:label></td>
					<td colSpan="5"><asp:textbox id="BankDepositStatus" runat="server" CssClass="textbox" Font-Names="Verdana" ReadOnly="True"
							AutoPostBack="True"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="lblBankAccount" runat="server" Font-Names="Verdana" Font-Size="X-Small">Bank Account</asp:label></td>
					<td><asp:dropdownlist id="ddlBankAccountId" runat="server" CssClass="boxlookw" AutoPostBack="True" DataValueField="bank_account_id"
							DataTextField="bank_account_id" onselectedindexchanged="ddlBankAccountId_SelectedIndexChanged"></asp:dropdownlist></td>
					<td><asp:label id="lblAccountNumber" runat="server" Font-Names="Verdana" Font-Size="X-Small">Account Number</asp:label></td>
					<td><asp:dropdownlist id="ddlBankAccountNumber" runat="server" CssClass="boxlookw" AutoPostBack="True"
							DataValueField="bank_account_number" DataTextField="bank_account_number" onselectedindexchanged="ddlBankAccountNumber_SelectedIndexChanged"></asp:dropdownlist></td>
					<td><asp:label id="LblBankName" runat="server" Font-Names="Verdana" Font-Size="X-Small">Bank Name</asp:label></td>
					<td><asp:dropdownlist id="ddlBankAccountName" runat="server" CssClass="boxlookw" AutoPostBack="True" DataValueField="bank_account_name"
							DataTextField="bank_account_name" Enabled="False"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td><asp:label id="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small">Deposit Date</asp:label></td>
					<td rowSpan="2"><asp:panel id="Panel2" runat="server" Height="24px">
							<UC:DATE id="DepositDate" runat="server" Height=".1cm" Required="True"></UC:DATE>
						</asp:panel></td>
					<td><asp:label id="lblDepositAmount" runat="server" Font-Names="Verdana" Font-Size="X-Small">Deposit Amount</asp:label></td>
					<td><asp:textbox id="DepositAmount" runat="server" CssClass="textbox" Font-Bold="True" Font-Names="Verdana"
							Enabled="False"></asp:textbox></td>
					<td><asp:label id="lblItemCount" runat="server" Font-Names="Verdana" Font-Size="X-Small">Item Count</asp:label></td>
					<td><asp:textbox id="ItemCount" runat="server" CssClass="textbox" Font-Bold="True" Font-Names="Verdana"
							ReadOnly="True" Enabled="False"></asp:textbox></td>
				</tr>
				<tr>
				</tr>
				<tr>
					<td><asp:label id="lblPayments" runat="server" CssClass="font10boldv" Font-Bold="True" ForeColor="#2F4F88">Payments</asp:label></td>
					<td colSpan="4"></td>
					<td align="right"><asp:hyperlink id="LinktoDepositList" runat="server" CssClass="font7boldvblue" Font-Names="Verdana"
							Font-Size="XX-Small" NavigateUrl="PayDeposit.aspx">Back to Bank Deposit List</asp:hyperlink></td>
				</tr>
				<tr>
					<td colSpan="6">
						<DIV style="OVERFLOW: scroll"><asp:datagrid id="dgPayments" runat="server" CssClass="font8v" Height="48px" BorderColor="Black"
								CellPadding="0" BorderWidth="1px" BorderStyle="Solid" AutoGenerateColumns="False" PageSize="2">
								<AlternatingItemStyle BorderWidth="1px" BorderStyle="Solid"></AlternatingItemStyle>
								<ItemStyle Width="5px" CssClass="font8v"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="font8vb" BackColor="Navy" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle Height="0.5cm" ForeColor="White" BackColor="Navy"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Include?">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle BackColor="#BFBFBF"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="ckbSelectPayment" runat="server" AutoPostBack="True" OnCheckedChanged="AddPaymentandCount"
												EnableViewState="True"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="payment_id" HeaderText="Pay Id">
										<HeaderStyle Width="74px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" BackColor="#BFBFBF"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="cheque_number" HeaderText="Cheque #">
										<HeaderStyle Width="104px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" BackColor="#BFBFBF"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="cheque_date" HeaderText="Date">
										<HeaderStyle Width="80px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" BackColor="#BFBFBF"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="cheque_payer" HeaderText="Payer">
										<HeaderStyle Width="400px"></HeaderStyle>
										<ItemStyle BackColor="#BFBFBF"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="payment_amount" HeaderText="Amount">
										<HeaderStyle Width="96px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" BackColor="#BFBFBF"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle VerticalAlign="Bottom" Height="0.5cm" BorderWidth="0.5cm" Font-Size="Small" Font-Names="Verdana"
									Font-Bold="True" BorderStyle="None" HorizontalAlign="Right" ForeColor="White" BackColor="Navy"
									Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</td>
				</tr>
				<tr>
					<td><asp:button id="pbSelectPayments" runat="server" CssClass="button2" Text="Select All" onclick="pbselectPayments_Click"></asp:button></td>
					<td><asp:button id="pbUnSelectPayments" runat="server" CssClass="button2" Text="UnSelect All" Width="80px" onclick="pbUnSelectPayments_Click"></asp:button></td>
					<td colSpan="3"></td>
					<td><asp:button id="pbAddBankDeposit" runat="server" CssClass="button2" Text="Submit" Width="64px" onclick="pbAddBankDeposit_Click"></asp:button></td>
				</tr>
				<tr>
					<td colSpan="6"><asp:label id="lblMessage" runat="server" CssClass="Font8BoldVRed" Width="360px" Visible="False">
							Error please try again</asp:label></td>
				</tr>
			</table>
			<asp:validationsummary id="ValidationSummary1" runat="server" CssClass="Font8BoldVRed" Height="24px" Width="400px"
				DisplayMode="List"></asp:validationsummary></form>
		</FORM>
	</body>
</HTML>

<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PaymentOptions.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.PaymentInformation.PaymentOptions" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellSpacing="0" cellPadding="1" border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="1" border="0" id="test">
				<TR vAlign="top" height="20">
					<TD class="NormalText" style="WIDTH: 115px" width="115">Payment Method</TD>
					<TD vAlign="top"><asp:dropdownlist id="paymentMethodDropDownList" Width="120px" 
                            Runat="server" CssClass="NormalText" 
                            onselectedindexchanged="paymentMethodDropDownList_SelectedIndexChanged" 
                            AutoPostBack="True"></asp:dropdownlist></TD>
				</TR>
				<TR vAlign="top" height="20">
					<TD class="NormalText" style="WIDTH: 115px; HEIGHT: 6px">Payment Term</TD>
					<TD vAlign="top" style="HEIGHT: 6px"><asp:dropdownlist id="PaymentTermDropdownList" Width="120px" Runat="server" CssClass="NormalText"></asp:dropdownlist></TD>
				</TR>
				<TR vAlign="top" height="20">
					<TD class="NormalText" style="WIDTH: 115px; HEIGHT: 14px">Deposit Method</TD>
					<TD vAlign="top" style="HEIGHT: 14px"><asp:dropdownlist id="SaleDepositMethodDropdownList" Width="120px" Runat="server" CssClass="NormalText"></asp:dropdownlist></TD>
				</TR>
				<TR vAlign="top" height="20">
					<TD class="NormalText" style="WIDTH: 115px">Deposit Payment</TD>
					<TD vAlign="top"><asp:textbox id="SaleDepositPaymentTextBox" Width="120px" Runat="server" CssClass="NormalText normalTextBox"
							BorderStyle="Solid"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2" vAlign="bottom">
						<asp:Button id="ProcessButton" runat="server" Text="Process Credit Card" tabIndex="9" onclick="ProcessButton_Click"></asp:Button></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>

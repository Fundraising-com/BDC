<%@ Page Language="c#" CodeBehind="Refund.aspx.cs" Trace="false" AutoEventWireup="True" MaintainScrollPositionOnPostback="true"
    Inherits="efundraising.EFundraisingCRMWeb.Sales.CreditCard.Refund" Culture="en-US" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html><head runat="server"><title>Refund</title><meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"><meta content="C#" name="CODE_LANGUAGE"><meta content="JavaScript" name="vs_defaultClientScript"><meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"></head><body style="font-family: Arial"><form id="Form1" method="post" runat="server">
    
    <asp:ValidationSummary runat="server" ID="vsSendRequest" ValidationGroup="SendRequest"
        Font-Size="Smaller" />
    <asp:ValidationSummary runat="server" ID="vsRefreshGrid" ValidationGroup="RefreshGrid"
        Font-Size="Smaller" />
    <table style="border-collapse: collapse; font-size: smaller" width="854px">
        <tr>
            <td>&nbsp;Sale ID </td>
            <td colspan="2">
                <asp:TextBox ID="txtSaleId" runat="server" Width="65px" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;Amount </td>
            <td colspan="2">
                <asp:TextBox ID="txtAmount" runat="server" Width="64px" OnDataBinding="AmountTextBox_DataBinding" />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 35px; vertical-align: bottom; font-weight: bold">&nbsp;Credit Card information </td>
        </tr>
        <tr>
            <td>&nbsp;Card Type </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlCCType" runat="server">
                    <asp:ListItem Value="2" Selected="True">Visa</asp:ListItem>
                    <asp:ListItem Value="3">MasterCard</asp:ListItem>
                    <asp:ListItem Value="8">Amex</asp:ListItem>
                    <asp:ListItem Value="9">Discover</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;Credit Card Number </td>
            <td colspan="2">
                <asp:TextBox ID="txtCCNumber" runat="server" MaxLength="16"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;Expiry Date </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlMonth" runat="server">
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
                </asp:DropDownList>&nbsp;&nbsp;/&nbsp; <asp:DropDownList ID="ddlYear" runat="server" />
            </td>
        </tr>
        <tr>
            <td>&nbsp; Currency </td>
            <td colspan="2">
                <asp:dropdownlist id="DropDownListCountry" runat="server">
							<asp:ListItem Value="US" Selected="True">US</asp:ListItem>
							<asp:ListItem Value="CA">CA</asp:ListItem>
							
						</asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 35px; vertical-align: bottom; font-weight: bold">&nbsp;Cardholder information </td>
        </tr>
        <tr>
            <td>&nbsp;First Name </td>
            <td colspan="2">
                <asp:TextBox ID="txtFirstName" runat="server" Width="184px" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;Last Name </td>
            <td colspan="2">
                <asp:TextBox ID="txtLastName" runat="server" Width="184px" />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 50px; vertical-align: top">
                <asp:Button ID="btnSendRefund" runat="server" CausesValidation="True" OnClick="ButtonSendRefund_Click"
                    Text="Send Refund Request" Width="137px" ValidationGroup="SendRequest" />
            </td>
        </tr>
        <tr>
            <td style="width: 28%">
                <asp:CheckBox ToolTip="All cancelled refund request will be displayed regardless of the request date"
                    runat="server" ID="chkDisplayCancelled" Text="Display Cancelled Refund Requests"
                    Checked="false" AutoPostBack="true" OnCheckedChanged="chkDisplayCancelled_CheckedChanged" />
            </td>
            <td style="text-align: right; width: 50%">&nbsp;From Date <span style="cursor: hand" onclick="javascript:OpenDatePickerWindow();">
                    <asp:TextBox runat="server" ID="txtFromDate" Width="100px" 
                    ontextchanged="txtFromDate_TextChanged" /><img style="border: none"
                        width="16" alt="Open calendar" height="16" src="../../Ressources/Images/UserControls/icon_calendar.gif" /></span></td>
            <td style="text-align: right; width: 22%; padding-right: 5px">&nbsp;To Date <span style="cursor: hand" onclick="javascript:OpenDatePickerWindow();">
                    <asp:TextBox runat="server" ID="txtTodate" Width="100px" /><img style="border: none"
                        width="16" height="16" alt="Open calendar" src="../../Ressources/Images/UserControls/icon_calendar.gif" /></span></td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="pSalesDataGridHeader" runat="server">
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="font-size: 11pt;
                        width: 850px; font-family: Arial; border-collapse: collapse">
                        <tr style="font-weight: bold; font-size: 10pt; background-color: #6795c3">
                            <td style="width: 50px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sale ID
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td style="width: 150px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Refund Amount
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td style="width: 150px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Refund Status
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td style="width: 200px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Request Date
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td style="width: 150px">
                                &nbsp;</td>
                            <td style="width: 150px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:DataGrid ID="dgSales" runat="server" AutoGenerateColumns="False" 
                    Font-Names="Arial" Width="850px" CellPadding="0" OnItemDataBound="SalesDataGrid_ItemDataBound"
                    OnItemCommand="SalesDataGrid_ItemCommand">
                    <ItemStyle Font-Size="Smaller" />
                    <AlternatingItemStyle BackColor="#DEDEE7" />
                    <HeaderStyle BackColor="#6795C3" Font-Bold="True" Font-Size="10pt" />
                    <Columns>
                        <asp:BoundColumn DataField="CreditCardRefundRequestID" ReadOnly="true" 
                            Visible="false" />
                        <asp:BoundColumn DataField="SaleID" ReadOnly="true" HeaderText="Sale ID" 
                            HeaderStyle-Width="50px" >
<HeaderStyle Width="50px"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="StatusCode" ReadOnly="true" Visible="false" />
                        <asp:BoundColumn DataField="RefundAmount" ReadOnly="true" HeaderText="Refund Amount"
                            DataFormatString="{0:$0.00}" HeaderStyle-Width="150px" >
<HeaderStyle Width="150px"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="StatusDescription" ReadOnly="true" HeaderText="Refund Status"
                            HeaderStyle-Width="150px" >
<HeaderStyle Width="150px"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RequestDate" ReadOnly="true" HeaderText="Request Date"
                             HeaderStyle-Width="200px" >
<HeaderStyle Width="200px"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Cancelled" ReadOnly="true" Visible="false" />
                        <asp:BoundColumn DataField="Processed" ReadOnly="true" Visible="false" />
                        <asp:ButtonColumn ItemStyle-HorizontalAlign="Center" ButtonType="PushButton" Text="Cancel Payment"
                            CausesValidation="true" ValidationGroup="RefreshGrid" 
                            HeaderStyle-Width="150px" >
<HeaderStyle Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:ButtonColumn>
                        <asp:ButtonColumn ItemStyle-HorizontalAlign="Center" ButtonType="PushButton" Text="Cancel Request"
                            CausesValidation="true" ValidationGroup="RefreshGrid" 
                            HeaderStyle-Width="150px" >
<HeaderStyle Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:ButtonColumn>
                         <asp:BoundColumn DataField="bppsID" ReadOnly="true" HeaderText="Trans.ID" 
                            HeaderStyle-Width="50px" ><HeaderStyle Width="150px"></HeaderStyle>
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnRefresh" runat="server" CausesValidation="True" ValidationGroup="RefreshGrid"
                    OnClick="ButtonRefresh_Click" Text="Refresh" />
                <%--<asp:Button ID="btnUpdatePayments" runat="server" CausesValidation="False" OnClick="ButtonUpdatePayments_Click"
					Text="Update Payments" />--%></td>
        </tr>
    </table>
    <asp:Label runat="server" ID="lblError" ForeColor="red" Font-Size="Smaller" />
    <asp:RequiredFieldValidator ID="rfvSaleId" runat="server" ErrorMessage="The Sale ID is required."
        ControlToValidate="txtSaleId" Display="None" ValidationGroup="SendRequest" />
    <asp:CompareValidator ID="cvSaleId" runat="server" ErrorMessage="The sale ID must be numeric."
        ControlToValidate="txtSaleId" Type="Integer" Operator="DataTypeCheck" Display="None"
        ValidationGroup="SendRequest" />
    <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="The Sale amount is required."
        ControlToValidate="txtAmount" Display="None" ValidationGroup="SendRequest" />
    <asp:CompareValidator ID="cvAmount" runat="server" ErrorMessage="The amount must be numeric."
        ControlToValidate="txtAmount" Type="Currency" Operator="DataTypeCheck" Display="None"
        ValidationGroup="SendRequest" />
    <asp:RequiredFieldValidator ID="rfvCCNumber" runat="server" ErrorMessage="The Credit Card Number is required."
        ControlToValidate="txtCCNumber" Display="None" ValidationGroup="SendRequest" />
    <%--	<asp:CompareValidator ID="cvCCNumber" runat="server" ErrorMessage="The Credit Card Number must be numeric."
		ControlToValidate="txtCCNumber" Type="Integer" Operator="DataTypeCheck" Display="None"
		ValidationGroup="SendRequest" />--%><asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="The Cardholder First Name is required."
        ControlToValidate="txtFirstName" Display="None" ValidationGroup="SendRequest" /><asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="The Cardholder Last Name is required."
        ControlToValidate="txtLastName" Display="None" ValidationGroup="SendRequest" /></form>
</body>
</html>






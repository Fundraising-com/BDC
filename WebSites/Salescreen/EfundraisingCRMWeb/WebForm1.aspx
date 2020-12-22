<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EfundraisingCRM.WebForm1" %>

<%@ Register src="Components/User/AddressHygiene/AddressHygiene.ascx" tagname="AddressHygiene" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 86px;
        }
    </style>
</head>
<body>
<body>


    <form id="form1" runat="server">
    <div>
    &nbsp;
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
</body>
    </div>
    
<body>
<a href="mailto:xx@xy.com?subject=Hello&amp;body=BabeDear [First Name] [Last Name],Thank for your choosing eFundraising for your upcoming fundraising campaign.
%0A%0A
Your order has been received by the manufacturer and is expected to deliver on the date below.%0A
Once it has shipped from the manufacturer you will receive a notification via e-mail, %0A
containing tracking information. Additionally, should an unforeseen delay occur, we will e-mail to advise you.
%0A%0A
ORDER INFORMATION%0A
Order Number: [ext_order_id]%0A
Billing Address : [B_address] [B_city],[B_state] [B_country] [B_zip]%0A
Delivery Address: [S_address] [S_city],S_state] [S_country] [S_zip]%0A
Delivery Date: [actual_delivery_date]%0A%0A

[detail]%0A
Shipping Fees: [shipping_fees]%0A
Surcharge: [surcharge]%0A
[Taxes]%0A
%0A
If you require any additional assistance, please do not hesitate to contact us at 1-888-875-1245 or via e-mail at%0A
EFRcustomerservice@qsp.com. We are open daily, Monday to Friday, from 8AM to 8PM EST.%0A%0A

Sincerely,%0A%0A

The eFundraising Customer Service Team


Dear [First Name] [Last Name],Thank for your choosing eFundraising for your upcoming fundraising campaign.
%0A%0A
Your order has been received by the manufacturer and is expected to deliver on the date below.%0A
Once it has shipped from the manufacturer you will receive a notification via e-mail, %0A
containing tracking information. Additionally, should an unforeseen delay occur, we will e-mail to advise you.
%0A%0A
ORDER INFORMATION%0A
Order Number: [ext_order_id]%0A
Billing Address : [B_address] [B_city],[B_state] [B_country] [B_zip]%0A
Delivery Address: [S_address] [S_city],S_state] [S_country] [S_zip]%0A
Delivery Date: [actual_delivery_date]%0A%0A
Once it has shipped from the manufacturer you will receive a notification via e-mail, %0A
containing tracking information. Additionally, should an unforeseen delay occur, we will e-mail to advise you.
%0A%0A
ORDER INFORMATION%0A

">click to send mail</a>
    <asp:Label ID="AddressHygieneStatusLabel" runat="server" Text="Label"></asp:Label>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <table style="width:100%;">
        <tr>
            <td class="style1">
                ZIP:</td>
            <td>
                <asp:Label ID="ZIPlbl" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                Address</td>
            <td>
                <asp:Label ID="AddressLabel" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </form>
</body>
</html>

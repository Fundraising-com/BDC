<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddressPopUp.aspx.cs" Inherits="EfundraisingCRM.Sales.SalesScreen.AddressPopUp" %>

<%@ Register src="../../Components/User/Address.ascx" tagname="Address" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                    Text="Enter New Shipping Address"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:Address ID="Address" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                                                                    <asp:Button ID="sendEmailButton" runat="server" Text="Send Email" 
                                                                        
                    onclick="sendEmailButton_Click" />
                                                                </td>
        </tr>
    </table>
    </form>
</body>
</html>

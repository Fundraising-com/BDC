<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XML.aspx.cs" Inherits="EfundraisingCRMWeb.Sales.SalesScreen.XML" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#333333" 
            Text="Select File:    "></asp:Label>
        <asp:DropDownList ID="FilesDropDownList" runat="server" AutoPostBack="True" 
            Font-Bold="True" ForeColor="#333333" Height="33px" 
            onselectedindexchanged="FilesDropDownList_SelectedIndexChanged" Width="477px">
        </asp:DropDownList>
    </div>
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
    <asp:GridView ID="SaleUpdatesGridView" runat="server" 
        AutoGenerateColumns="False" Width="692px" AllowPaging="True" 
        AllowSorting="True" onpageindexchanging="SaleUpdatesGridView_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="saleid" HeaderText="Sale Id" ReadOnly="True" />
            <asp:BoundField DataField="ShipTracking" HeaderText="Updates" ReadOnly="True" />
        </Columns>
        <HeaderStyle BackColor="#669999" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
        
    </asp:GridView>
            </td>
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
            <td valign="top">
                <asp:Button ID="PaymentButton" runat="server" onclick="PaymentButton_Click" 
                    Text="Payments Updated" />
                <asp:Button ID="PaymentInsertedButton1" runat="server" 
                    onclick="PaymentInsertedButton_Click" Text="Payments Inserted" />
                <asp:Button ID="AdjustmentUpdatedButton" runat="server" 
                    onclick="AdjustmentUpdatedButton_Click" Text="Adjustment Updated" />
                <asp:Button ID="AdjustmentInsertedButton0" runat="server" 
                    onclick="AdjustmentInsertedButton0_Click" Text="Adjustments Inserted" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
    <asp:GridView ID="PaymentsGridView" runat="server" 
        AutoGenerateColumns="False" Width="692px" AllowPaging="True" 
        AllowSorting="True" onpageindexchanging="PaymentsGridView_PageIndexChanging" >
        <Columns>
            <asp:BoundField DataField="saleid" HeaderText="Sale Id" ReadOnly="True" />
            <asp:BoundField DataField="status" HeaderText="Status" ReadOnly="True" />
        </Columns>
        <HeaderStyle BackColor="Yellow" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
        
    </asp:GridView>
            </td>
            <td class="style1">
            </td>
            <td class="style1">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

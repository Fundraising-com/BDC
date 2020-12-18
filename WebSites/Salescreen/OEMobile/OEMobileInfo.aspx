<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OEMobileInfo.aspx.cs" Inherits="OEMobileInfo" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
    <mobile:Form id="Form1" runat="server">
        <mobile:Label ID="lblError" Runat="server" Font-Bold="True" Font-Name="Arial" Font-Size="Small"
            ForeColor="Red">
        </mobile:Label>
        <mobile:Label ID="lblGreetings" Runat="server" Font-Name="Arial" Font-Size="Small"></mobile:Label><br />
        <mobile:Label ID="lblAction" Runat="server" Font-Name="Arial" Font-Size="Small">Please select an account</mobile:Label>
        <mobile:SelectionList ID="ddlFM" Runat="server" Visible="False"></mobile:SelectionList>
        <mobile:Command ID="cmdShowAccount" Runat="server" OnClick="cmdShowAccount_Click">Show Accounts</mobile:Command>
        <mobile:SelectionList ID="ddlAccount" Runat="server"></mobile:SelectionList>
        <mobile:Command ID="cmdShowOrder" Runat="server" OnClick="cmdShowOrder_Click" Visible="False">Show Orders</mobile:Command>
        <mobile:Label ID="lblOrderTitle" Runat="server" Font-Name="Arial" Font-Size="Small" Visible="False">CampaignID, QSPOrderID, Status</mobile:Label>
        <mobile:List ID="lstOrder" Runat="server" Visible="False">
        </mobile:List>
        <br />
    </mobile:Form>
</body>
</html>

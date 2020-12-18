<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderInformationView.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.OrderInformationView" %>
<table width="100%">
    <tr>
        <td width="25%">
            <span class="Text_Title">QSP Id:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblQspId" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">EDS Id:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblEdsId" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Status:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblStatusBox" runat="server" BorderWidth="1px" BorderStyle="Solid"
                BorderColor="Black">&nbsp;&nbsp;</asp:Label>
            &nbsp;&nbsp;
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Field sales manager:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblFSM" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Order date:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblOrderDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Order form:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblOrderForm" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Order type:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblOrderType" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Delivery method:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblDeliveryMethod" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Requested delivery date:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblRequestedDeliveryDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr id="trRequestedLeadTime" runat="server">
        <td width="25%">
            <span class="Text_Title">Requested lead time:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblRequestedLeadTime" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr id="trRequestedDeliveryWarehouse" runat="server">
        <td width="25%">
            <span class="Text_Title">Requested delivery warehouse:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblRequestedDeliveryWarehouse" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr id="trShippingDate" runat="server">
        <td width="25%">
            <span class="Text_Title">Shipping date:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblShippingDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr id="trCustomerPO" runat="server">
        <td width="25%">
            <span class="Text_Title">Customer PO#:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblCustomerPO" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr id="trComments" runat="server">
        <td width="25%">
            <span class="Text_Title">Comments:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblComments" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
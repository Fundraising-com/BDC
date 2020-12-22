<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderSummaryView.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.OrderSummaryView" %>
<table width="100%">
    <tr>
        <td width="25%">
            <span class="Text_Title">Sub total:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblSubTotal" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">&nbsp;</span>
        </td>
        <td width="25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Tax rate:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblTaxRate" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">&nbsp;</span>
        </td>
        <td width="25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Tax amount:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblTaxAmount" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">&nbsp;</span>
        </td>
        <td width="25%">
            &nbsp;
        </td>
    </tr>
    <tr id="trShippingCharges" runat="server">
        <td width="25%">
            <span class="Text_Title">Shipping charges:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblShippingCharges" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">&nbsp;</span>
        </td>
        <td width="25%">
            &nbsp;
        </td>
    </tr>
    <tr id="trSurcharge" runat="server">
        <td width="25%">
            <span class="Text_Title">Surcharges:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblSurcharges" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">&nbsp;</span>
        </td>
        <td width="25%">
            &nbsp;
        </td>
    </tr>
    <tr id="trCredits" runat="server">
        <td width="25%">
            <span class="Text_Title">Credits:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblCredits" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">&nbsp;</span>
        </td>
        <td width="25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Grand total:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">&nbsp;</span>
        </td>
        <td width="25%">
            &nbsp;
        </td>
    </tr>

</table>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderTermsView.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.OrderTermsView" %>
<table width="100%">
    <tr>
        <td width="25%">
            <span class="Text_Title">Start date:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">End date:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">GOAL- Estimated Gross:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblGoalEstimatedGross" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Enrollment:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblEnrollment" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">QSP fiscal year:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblFiscalYear" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr id="trAccountProfit" runat="server">
        <td width="25%">
            <span class="Text_Title">Account profit:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblAccountProfit" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
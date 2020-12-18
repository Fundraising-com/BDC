<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgramAgreementTerms.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.ProgramAgreementTerms" %>
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
            <span class="Text_Title">Holiday start date:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblHolidayStartDate" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Holiday end date:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblHolidayEndDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Goal - estimated gross:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblGoal" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Number of participants:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblParticipants" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Account profit:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblAccountProfit" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>

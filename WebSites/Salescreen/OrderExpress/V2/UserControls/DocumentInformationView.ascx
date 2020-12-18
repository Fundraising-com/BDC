<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentInformationView.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.DocumentInformationView" %>
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
            <span class="Text_Title">Type:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Name:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Approved:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:CheckBox ID="cbApproved" runat="server" Enabled="false" />
        </td>
    </tr>
    <tr id="trApprovedBy" runat="server">
        <td width="25%">
            <span class="Text_Title">Approved by:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblApprovedBy" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr id="trApprovedAt" runat="server">
        <td width="25%">
            <span class="Text_Title">Approved at:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblApprovedAt" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr id="trExeptionNumber" runat="server">
        <td width="25%">
            <span class="Text_Title">Exeption number:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblExeptionNumber" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr id="trExeptionDates" runat="server">
        <td width="25%">
            <span class="Text_Title">Exeption start date:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblExeptionStartDate" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Exeption end date:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblExeptionEndDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
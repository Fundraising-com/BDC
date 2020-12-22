<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationInformationView.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.OrganizationInformationView" %>
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
            <span class="Text_Title">Name:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Type:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Level:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblLevel" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr id="trTaxExemption" runat="server" visible="false">
        <td width="25%">
            <span class="Text_Title">Tax exemption:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblTaxExemption" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Expires:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblTaxExeptionExpiry" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">MDR PID:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblMDRPID" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Comments:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblComments" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>

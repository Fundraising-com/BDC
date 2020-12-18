<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuditInformationView.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.AuditInformationView" %>
<table width="100%">
    <tr>
        <td width="25%">
            <span class="Text_Title">Create date:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblCreateDate" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Created by:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Update date:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblUpdateDate" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Updated by:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblUpdatedBy" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgramInformationView.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.ProgramInformationView" %>
<table width="100%">
    <tr>
        <td width="25%">
            <span class="Text_Title">Catalog selection:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblCatalogSelection" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Catalog type:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblCatalogType" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>



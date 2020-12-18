<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="QSP.OrderExpress.Web.UserControls.HeaderControl" Codebehind="HeaderControl.ascx.cs" %>
<table border="0" cellpadding="0" cellspacing="0" >
    <tr id="trHeaderDisplay" runat="server" visible="true">
        <td colspan="2">
            <asp:Image ID="imgIcon" runat="server" Height="40px" Visible="true"></asp:Image>&nbsp;&nbsp;
            <asp:Label ID="lblSectionTitle" runat="server" CssClass="SectionTitleLabel">Section Title :</asp:Label>&nbsp;&nbsp;
            <asp:Label ID="lblPageTitle" runat="server" CssClass="PageTitleLabel">Page Title </asp:Label>
        </td>
    </tr>
    <tr id="trDirectionsTitle" runat="server" visible="true" style="height: 2px;">
        <td colspan="2" style="height: 2px;">
            <asp:Label ID="lblDirection" runat="server" CssClass="DirectionTitleLabel">Directions :</asp:Label>
        </td>
    </tr>
    <tr id="trDirectionDesc" runat="server" visible="true" colspan="2" style="height: 30px;">
        <td>
            <asp:Label ID="lblInstruction" runat="server" CssClass="DirectionLabel" Width="800px"></asp:Label>
        </td>
    </tr>
</table>

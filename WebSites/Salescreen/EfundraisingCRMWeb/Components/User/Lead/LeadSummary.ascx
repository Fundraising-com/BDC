<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadSummary.ascx.cs"
    Inherits="EFundraisingCRMWeb.Components.User.Lead.LeadSummary" %>
<asp:Panel ID="Panel1" runat="server" CssClass="Frame" BorderStyle="Solid" BorderWidth="2px"
    BorderColor="Gainsboro" Width="266px" Height="216px">
    <table class="NormalText" id="Table1" border="0" style="height: 213px">
        <tr valign="top">
            <td style="width: 107px; height: 20px" valign="top" align="left">
                <asp:Label ID="Label3" Width="104px" CssClass="NormalText" runat="server">Lead ID:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="LeadIDLabel" CssClass="NormalText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 107px; height: 20px" valign="top" align="left">
                <asp:Label ID="Label1" Width="104px" CssClass="NormalText" runat="server">Lead Entry Date:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="LeadEntryDateLabel" CssClass="NormalText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 107px; height: 20px" align="left">
                <asp:Label ID="Label2" Width="104px" CssClass="NormalText" runat="server">Promotion:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="PromotionLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 107px; height: 20px" align="left">
                <asp:Label ID="Label4" Width="104px" CssClass="NormalText" runat="server">Channel:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="ChannelLabel" CssClass="NormalText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 107px; height: 20px" align="left">
                <asp:Label ID="Label5" Width="104px" CssClass="NormalText" runat="server">Client Status:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="ClientStatusLabel" CssClass="NormalText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 107px; height: 20px" align="left">
                <asp:Label ID="Label6" Width="104px" CssClass="NormalText" runat="server">Group Type:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="GroupTypeLabel" CssClass="NormalText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 107px; height: 20px" align="left">
                <asp:Label ID="Label7" Width="104px" CssClass="NormalText" runat="server">VIF:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="VIFLabel" CssClass="NormalText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 107px; height: 20px" align="left">
                <asp:Label ID="Label8" Width="104px" CssClass="NormalText" runat="server">Kit Sent:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="KitSentLabel" CssClass="NormalText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 107px; height: 21px" align="left">
                <asp:Label ID="Label9" Width="113px" CssClass="NormalText" runat="server">Participant Count:</asp:Label>
            </td>
            <td style="height: 21px" align="left">
                <asp:Label ID="ParticipantCountLabel" CssClass="NormalText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 107px; height: 20px" align="left">
                <asp:Label ID="Label10" Width="96px" CssClass="NormalText" runat="server">Partner :</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="PartnerLabel" CssClass="NormalText" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>

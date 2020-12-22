<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddressView.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.AddressView" %>
<table width="100%" class="Box">
    <tr class="GridItemStyle">
        <td width="25%">
            <span class="Text_Important">
                Address name:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblAddressName" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
    </tr>
    <tr class="GridItemStyle_Alternative">
        <td width="25%">
            <span class="Text_Important">
                First name:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblFirstName" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
    </tr>
    <tr class="GridItemStyle">
        <td width="25%">
            <span class="Text_Important">
                Last name:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblLastName" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
    </tr>
    <tr class="GridItemStyle_Alternative">
        <td width="25%">
            <span class="Text_Important">
                Address line 1:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblAddressLine1" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
    </tr>
    <tr class="GridItemStyle">
        <td width="25%">
            <span class="Text_Important">
                Address line 2:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblAddressLine2" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
    </tr>
    <tr class="GridItemStyle_Alternative">
        <td width="25%">
            <span class="Text_Important">
                City:
            </span>
        </td>
        <td width="25%">
            <asp:Label ID="lblCity" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Important">
                County:
            </span>
        </td>
        <td width="25%">
            <asp:Label ID="lblCounty" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
    </tr>
    <tr class="GridItemStyle">
        <td width="25%">
            <span class="Text_Important">
                State:
            </span>
        </td>
        <td width="25%">
            <asp:Label ID="lblState" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Important">
                Zip code:
            </span>
        </td>
        <td width="25%">
            <asp:Label ID="lblZipCode" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
    </tr>
    <tr class="GridItemStyle_Alternative">
        <td width="25%">
            <span class="Text_Important">
                Phone:
            </span>
        </td>
        <td width="25%">
            <asp:Label ID="lblPhone" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Important">
                Fax:
            </span>
        </td>
        <td width="25%">
            <asp:Label ID="lblFax" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
    </tr>
    <tr class="GridItemStyle">
        <td width="25%">
            <span class="Text_Important">
                Email:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblEmail" runat="server" CssClass="Text_Small"></asp:Label>
        </td>
    </tr>
    <tr class="GridItemStyle_Alternative">
        <td width="25%">
            <span class="Text_Important">
                Residential area:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:CheckBox ID="cbResidential" runat="server" Enabled="false" />
        </td>
    </tr>
</table>

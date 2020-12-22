<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddressEdit.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.AddressEdit" %>
<table width="100%" class="Box">
    <tr class="GridItemStyle">
        <td width="25%">
            <span class="Text_Important">
                Address name:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="tbAddressName" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr class="GridItemStyle_Alternative">
        <td width="25%">
            <span class="Text_Important">
                First name:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="tbFirstName" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr class="GridItemStyle">
        <td width="25%">
            <span class="Text_Important">
                Last name:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="tbLastName" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr class="GridItemStyle_Alternative">
        <td width="25%">
            <span class="Text_Important">
                Address line 1:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="tbAddressLine1" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr class="GridItemStyle">
        <td width="25%">
            <span class="Text_Important">
                Address line 2:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="tbAddressLine2" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr class="GridItemStyle_Alternative">
        <td width="25%">
            <span class="Text_Important">
                City:
            </span>
        </td>
        <td width="25%">
            <asp:TextBox ID="tbCity" runat="server" Width="200px"></asp:TextBox>
        </td>
        <td width="25%">
            <span class="Text_Important">
                County:
            </span>
        </td>
        <td width="25%">
            <asp:TextBox ID="tbCounty" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr class="GridItemStyle">
        <td width="25%">
            <span class="Text_Important">
                State:
            </span>
        </td>
        <td width="25%">
            <asp:DropDownList ID="ddlState" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td width="25%">
            <span class="Text_Important">
                Zip code:
            </span>
        </td>
        <td width="25%">
            <asp:TextBox ID="tbZipCode" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr class="GridItemStyle_Alternative">
        <td width="25%">
            <span class="Text_Important">
                Phone:
            </span>
        </td>
        <td width="25%">
            <asp:TextBox ID="tbPhone" runat="server" Width="200px"></asp:TextBox>
        </td>
        <td width="25%">
            <span class="Text_Important">
                Fax:
            </span>
        </td>
        <td width="25%">
            <asp:TextBox ID="tbFax" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr class="GridItemStyle">
        <td width="25%">
            <span class="Text_Important">
                Email:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="tbEmail" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr class="GridItemStyle_Alternative">
        <td width="25%">
            <span class="Text_Important">
                Residential area:
            </span>
        </td>
        <td width="75%" colspan="3">
            <asp:CheckBox ID="cbResidential" runat="server" />
        </td>
    </tr>
</table>
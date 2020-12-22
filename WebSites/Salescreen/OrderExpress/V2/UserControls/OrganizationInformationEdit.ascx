<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationInformationEdit.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.OrganizationInformationEdit" %>
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
            <asp:TextBox ID="tbName" runat="server" Width="500px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Type:</span>
        </td>
        <td width="25%">
            <asp:DropDownList ID="ddlType" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td width="25%">
            <span class="Text_Title">Level:</span>
        </td>
        <td width="25%">
            <asp:DropDownList ID="ddlLevel" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trTaxExemption" runat="server" visible="false">
        <td width="25%">
            <span class="Text_Title">Tax exemption:</span>
        </td>
        <td width="25%">
            <asp:TextBox ID="tbTaxExemption" runat="server" Width="200px"></asp:TextBox>
        </td>
        <td width="25%">
            <span class="Text_Title">Expires:</span>
        </td>
        <td width="25%">
            <asp:TextBox ID="tbTaxExeptionExpiry" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>    
    <tr>
        <td width="25%">
            <span class="Text_Title">MDR PID:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="tbMDRPID" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Comments:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="tbComments" runat="server" Rows="5" TextMode="MultiLine" Width="500px"></asp:TextBox>
        </td>
    </tr>
</table>
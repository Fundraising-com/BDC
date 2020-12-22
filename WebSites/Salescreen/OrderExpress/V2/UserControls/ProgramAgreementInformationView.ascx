<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgramAgreementInformationView.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.ProgramAgreementInformationView" %>
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
            <span class="Text_Title">EDS Id:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblEdsId" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Status:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblStatusBox" runat="server" BorderWidth="1px" BorderStyle="Solid"
                BorderColor="Black">&nbsp;&nbsp;</asp:Label>
            &nbsp;&nbsp;
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Program agreement date:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblProgramAgreementDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>

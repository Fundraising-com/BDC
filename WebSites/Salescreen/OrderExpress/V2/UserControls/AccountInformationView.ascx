<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountInformationView.ascx.cs"
    Inherits="QSP.OrderExpress.Web.V2.UserControls.AccountInformationView" %>
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
            <span class="Text_Title">Name:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
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
            <span class="Text_Title">Field sales manager:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblFSM" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Organization name:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblOrganizationName" runat="server" Text=""></asp:Label>
            &nbsp;&nbsp;
            <asp:HyperLink ID="hlOrganizationView" runat="server" Target="_blank">
                <asp:Image ID="imgOrganizationView" runat="server" ImageUrl="~/images/BtnDetail.gif" />
            </asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Organization type:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblOrganizationType" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Organization level:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblOrganizationLevel" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">QSP Program:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblQSPProgram" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Trade class:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblTradeClass" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Last fiscal year:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblLastFiscalYear" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Inactive months:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblInactiveMonths" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Last order:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblLastOrder" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Default warehouse:</span>
        </td>
        <td width="75%" colspan="3">
            <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label>
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
            <span class="Text_Title">Tax exemption expiration:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblTaxExpirationExpire" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="25%">
            <span class="Text_Title">Collection date:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblCollectionDate" runat="server" Text=""></asp:Label>
        </td>
        <td width="25%">
            <span class="Text_Title">Collection amount:</span>
        </td>
        <td width="25%">
            <asp:Label ID="lblCollectionAmount" runat="server" Text=""></asp:Label>
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

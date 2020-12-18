<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="OrganizationView.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.OrganizationView" %>

<%@ Register TagPrefix="uc" TagName="OrganizationInformationView" Src="~/V2/UserControls/OrganizationInformationView.ascx" %>
<%@ Register TagPrefix="uc" TagName="AddressView" Src="~/V2/UserControls/AddressView.ascx" %>
<%@ Register TagPrefix="uc" TagName="AccountSearchResults" Src="~/V2/UserControls/AccountSearchResults.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Organization:</span>&nbsp;<span class="PageHeader2">Detail</span>
                <asp:Label ID="lblDirections" runat="server">
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    Please verify the Organization Information below and click on Edit button to modify
                    data. </span>
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="SectionHeader">
                            Organization information
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:OrganizationInformationView ID="ucOrganizationInformationView" runat="server">
                            </uc:OrganizationInformationView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td colspan="2" class="SectionHeader">
                            Addresses
                        </td>
                    </tr>
                    <tr>
                        <td class="GridHeaderItemStyle">
                            Biil to:
                        </td>
                        <td class="GridHeaderItemStyle">
                            Ship to:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:AddressView ID="ucBillingAddressView" runat="server"></uc:AddressView>
                        </td>
                        <td>
                            <uc:AddressView ID="ucShippingAddressView" runat="server"></uc:AddressView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="SectionHeader">
                            Accounts
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:AccountSearchResults ID="ucAccountSearchResults" runat="server"></uc:AccountSearchResults>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trButtonsSpacer" runat="server">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trButtons" runat="server">
            <td align="center">
                <asp:HyperLink ID="hlPrint" runat="server" Target="_blank" CssClass="LinkButton"
                    Width="100px">Printer friendly</asp:HyperLink>
                &nbsp;
                <asp:HyperLink ID="hlEdit" runat="server" CssClass="LinkButton" Width="100px">Edit</asp:HyperLink>
                &nbsp;
                <asp:HyperLink ID="hlClose" runat="server" CssClass="LinkButton" Width="100px">Close</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

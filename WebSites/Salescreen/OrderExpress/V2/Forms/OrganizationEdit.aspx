<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="OrganizationEdit.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.OrganizationEdit" %>

<%@ Register TagPrefix="uc" TagName="OrganizationInformationEdit" Src="~/V2/UserControls/OrganizationInformationEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="AddressEdit" Src="~/V2/UserControls/AddressEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="MethodNotificationList" Src="~/V2/UserControls/MethodNotificationList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Organization:</span>&nbsp;<span class="PageHeader2">Edit</span>
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    Edit Organization Information, Postal Address, Phone Numbers and/or Email Addresses.
                    'Bill To' Information can easily be copied over to 'Ship To' Information. </span>
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
                            <uc:OrganizationInformationEdit ID="ucOrganizationInformationEdit" runat="server">
                            </uc:OrganizationInformationEdit>
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
                            <uc:AddressEdit ID="ucBillingAddressEdit" runat="server"></uc:AddressEdit>
                        </td>
                        <td>
                            <uc:AddressEdit ID="ucShippingAddressEdit" runat="server"></uc:AddressEdit>
                        </td>
                    </tr>
                    <tr id="trCopyAddress" runat="server" visible="false">
                        <td>
                            <asp:HyperLink ID="HyperLink2" runat="server">Copy from shipping address</asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink ID="HyperLink1" runat="server">Copy from billing address</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trMethodNotification" runat="server">
            <td>
                <table width="100%">
                    <tr>
                        <td class="SectionHeader">
                            Validation errors
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:MethodNotificationList ID="ucMethodNotificationList" runat="server"></uc:MethodNotificationList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="SectionHeader">
                            &nbsp;
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
            <td align="center">
                <asp:LinkButton ID="lbSave" runat="server" OnClick="lbSave_Click" CssClass="LinkButton"
                    Width="100px">Save changes</asp:LinkButton>
                &nbsp;
                <asp:HyperLink ID="hlCancel" runat="server" CssClass="LinkButton" Width="100px">Cancel</asp:HyperLink>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="OrderView.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.OrderView" %>

<%@ Register TagPrefix="uc" TagName="AccountInformationView" Src="~/V2/UserControls/AccountInformationView.ascx" %>
<%@ Register TagPrefix="uc" TagName="AddressView" Src="~/V2/UserControls/AddressView.ascx" %>
<%@ Register TagPrefix="uc" TagName="BusinessExceptionList" Src="~/V2/UserControls/BusinessExceptionList.ascx" %>
<%@ Register TagPrefix="uc" TagName="DocumentSearchResults" Src="~/V2/UserControls/DocumentSearchResults.ascx" %>
<%@ Register TagPrefix="uc" TagName="OrderInformationView" Src="~/V2/UserControls/OrderInformationView.ascx" %>
<%@ Register TagPrefix="uc" TagName="OrderTermsView" Src="~/V2/UserControls/OrderTermsView.ascx" %>
<%@ Register TagPrefix="uc" TagName="OrderSummaryView" Src="~/V2/UserControls/OrderSummaryView.ascx" %>
<%@ Register TagPrefix="uc" TagName="OrderDetailList" Src="~/V2/UserControls/OrderDetailList.ascx" %>
<%@ Register TagPrefix="uc" TagName="ChargeList" Src="~/V2/UserControls/ChargeList.ascx" %>
<%@ Register TagPrefix="uc" TagName="AuditInformationView" Src="~/V2/UserControls/AuditInformationView.ascx" %>
<%@ Register TagPrefix="uc" TagName="StatusHistoryList" Src="~/V2/UserControls/StatusHistoryList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Order:</span>&nbsp;<span class="PageHeader2">Detail</span>
                <asp:Label ID="lblDirections" runat="server">
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    Please verify Account and Order information below and click on Edit button to access
                    edit fields and modify data. </span>
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
                            Account information
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:AccountInformationView ID="ucAccountInformationView" runat="server"></uc:AccountInformationView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAccountNotesSpacer" runat="server">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trAccountNotes" runat="server">
            <td>
                <table width="100%">
                    <tr>
                        <td class="SectionHeader">
                            Account notes
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:BusinessExceptionList ID="ucBusinessExceptionList" runat="server"></uc:BusinessExceptionList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAccountDocumentsSpacer" runat="server">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trAccountDocuments" runat="server">
            <td>
                <table width="100%">
                    <tr>
                        <td class="SectionHeader">
                            Account documents
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:DocumentSearchResults ID="ucDocumentList" runat="server"></uc:DocumentSearchResults>
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
                            Account bill to:
                        </td>
                        <td class="GridHeaderItemStyle">
                            Order ship to:
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
                            Order information
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:OrderInformationView ID="ucOrderInformationView" runat="server"></uc:OrderInformationView>
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
                            Order terms
                        </td>
                    </tr>
                    <tr>
                        <td class="Text_Note_Text">
                            You are in agreement that QSP will be working with your organization in connection
                            with a fundraising program as follows:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:OrderTermsView ID="ucOrderTermsView" runat="server"></uc:OrderTermsView>
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
                            Order details
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:OrderDetailList ID="ucOrderDetailList" runat="server"></uc:OrderDetailList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trOrderChargesSpacer" runat="server">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trOrderCharges" runat="server">
            <td>
                <table width="100%">
                    <tr>
                        <td class="SectionHeader">
                            Order charges
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:ChargeList ID="ucChargeList" runat="server"></uc:ChargeList>
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
                            Order summary
                        </td>
                    </tr>
                    <tr>
                        <td class="Text_Note_Text">
                            Invoices will include applicable taxes unless the Organization is exempt. Tax exempt
                            forms or resale certificates are required with order. Most forms are available on
                            state websites. Fax forms to QSP Field Support to avoid taxes on invoices.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:OrderSummaryView ID="ucOrderSummaryView" runat="server"></uc:OrderSummaryView>
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
                            Audit information
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:AuditInformationView ID="ucAuditInformationView" runat="server"></uc:AuditInformationView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trStatusHistorySpacer" runat="server">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trStatusHistory" runat="server">
            <td>
                <table width="100%">
                    <tr>
                        <td class="SectionHeader">
                            Status history
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:StatusHistoryList ID="ucStatusHistoryList" runat="server"></uc:StatusHistoryList>
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
                &nbsp;
                <asp:LinkButton ID="hlResetStatus" runat="server" CssClass="LinkButton" 
                    Width="100px" onclick="hlResetStatus_Click">Reset Status</asp:LinkButton>
            </td>
        </tr>
        <tr id="trConfirmationSpacer" runat="server">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblConfirmation" runat="server" CssClass="Text_Title" Text="The status is reset successfully" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

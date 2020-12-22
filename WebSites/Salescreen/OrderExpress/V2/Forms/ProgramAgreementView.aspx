<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="ProgramAgreementView.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.ProgramAgreementView" %>

<%@ Register TagPrefix="uc" TagName="AccountInformationView" Src="~/V2/UserControls/AccountInformationView.ascx" %>
<%@ Register TagPrefix="uc" TagName="AddressView" Src="~/V2/UserControls/AddressView.ascx" %>
<%@ Register TagPrefix="uc" TagName="BusinessExceptionList" Src="~/V2/UserControls/BusinessExceptionList.ascx" %>
<%@ Register TagPrefix="uc" TagName="DocumentSearchResults" Src="~/V2/UserControls/DocumentSearchResults.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProgramAgreementInformationView" Src="~/V2/UserControls/ProgramAgreementInformationView.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProgramAgreementTermsView" Src="~/V2/UserControls/ProgramAgreementTerms.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProgramInformationView" Src="~/V2/UserControls/ProgramInformationView.ascx" %>
<%@ Register TagPrefix="uc" TagName="AuditInformationView" Src="~/V2/UserControls/AuditInformationView.ascx" %>
<%@ Register TagPrefix="uc" TagName="StatusHistoryList" Src="~/V2/UserControls/StatusHistoryList.ascx" %>
<%@ Register TagPrefix="uc" TagName="OrderSearchResults" Src="~/V2/UserControls/OrderSearchResults.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Program agreement:</span>&nbsp;<span class="PageHeader2">Detail</span>
                <asp:Label ID="lblDirections" runat="server">
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    Please verify the program agreement information below and click on Edit button to
                    access edit fields and modify data. Here’s a tip! By clicking on the forward arrow
                    button below, you can access the Account Detail for this Program Agreement.
                </span>
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
                            Program agreement ship to:
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
                            Program agreement information
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:ProgramAgreementInformationView ID="ucProgramAgreementInformationView" runat="server">
                            </uc:ProgramAgreementInformationView>
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
                            Program agreement terms
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
                            <uc:ProgramAgreementTermsView ID="ucProgramAgreementTermsView" runat="server"></uc:ProgramAgreementTermsView>
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
                            Program information
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:ProgramInformationView ID="ucProgramInformationView" runat="server"></uc:ProgramInformationView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trOrdersSpacer" runat="server">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trOrders" runat="server">
            <td>
                <table width="100%">
                    <tr>
                        <td class="SectionHeader">
                            Orders
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:OrderSearchResults ID="ucOrderSearchResults" runat="server"></uc:OrderSearchResults>
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
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

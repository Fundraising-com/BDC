<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="DocumentView.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.DocumentView" %>

<%@ Register TagPrefix="uc" TagName="AccountInformationView" Src="~/V2/UserControls/AccountInformationView.ascx" %>
<%@ Register TagPrefix="uc" TagName="DocumentInformationView" Src="~/V2/UserControls/DocumentInformationView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Document:</span>&nbsp;<span class="PageHeader2">Detail</span>
                <asp:Label ID="lblDirections" runat="server">
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    Please verify the document information below and click on Edit button to access edit
                    fields and modify data. </span>
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
                            Document information
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc:DocumentInformationView ID="ucDocumentInformationView" runat="server"></uc:DocumentInformationView>
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

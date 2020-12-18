<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="DocumentEdit.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.DocumentEdit" %>

<%@ Register TagPrefix="uc" TagName="DocumentInformationEdit" Src="~/V2/UserControls/DocumentInformationEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="MethodNotificationList" Src="~/V2/UserControls/MethodNotificationList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Document:</span>&nbsp;<span class="PageHeader2">Edit</span>
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    Edit Document Information.</span>
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
                            <uc:DocumentInformationEdit ID="ucDocumentInformationEdit" runat="server"></uc:DocumentInformationEdit>
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

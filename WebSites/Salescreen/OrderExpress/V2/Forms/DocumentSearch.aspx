<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="DocumentSearch.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.DocumentSearch" %>

<%@ Register TagPrefix="uc" TagName="DocumentSearch" Src="~/V2/UserControls/DocumentSearch.ascx" %>
<%@ Register TagPrefix="uc" TagName="DocumentSearchResults" Src="~/V2/UserControls/DocumentSearchResults.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Document:&nbsp;</span><span class="PageHeader2">Search</span>
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    To acknowledge the receipt of a document for an account, click on the text box in
                    the Received column. Use the Search features and click on Refresh button to filter
                    data. </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <uc:DocumentSearch ID="ucDocumentSearch" runat="server"></uc:DocumentSearch>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <uc:DocumentSearchResults ID="ucDocumentSearchResults" runat="server"></uc:DocumentSearchResults>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="OrderSearch.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.OrderSearch" %>

<%@ Register TagPrefix="uc" TagName="OrderSearch" Src="~/V2/UserControls/OrderSearch.ascx" %>
<%@ Register TagPrefix="uc" TagName="OrderSearchResults" Src="~/V2/UserControls/OrderSearchResults.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Order:&nbsp;</span><span class="PageHeader2">Search</span>
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    To locate an Order, use the Search and Filter features and click on "Search orders"
                    button. </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <uc:OrderSearch ID="ucOrderSearch" runat="server"></uc:OrderSearch>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <uc:OrderSearchResults ID="ucOrderSearchResults" runat="server"></uc:OrderSearchResults>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

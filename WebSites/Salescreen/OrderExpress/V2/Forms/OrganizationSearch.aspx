<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="OrganizationSearch.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.OrganizationSearch" %>

<%@ Register TagPrefix="uc" TagName="OrganizationSearch" Src="~/V2/UserControls/OrganizationSearch.ascx" %>
<%@ Register TagPrefix="uc" TagName="OrganizationSearchResults" Src="~/V2/UserControls/OrganizationSearchResults.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Organization:&nbsp;</span><span class="PageHeader2">Search</span>
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    To locate an Organization, use the Search and Filter features and click on "Search
                    organizations" button.
                    <br />
                    <br />
                    An Organization represents the main entity or fund-raising organization. It can
                    be a school, sorority, etc. While an Organization may have one or more Accounts,
                    there can only be one Organization for an account[s]. Every Organization is assigned
                    a unique QSP ID Number that is tied to the EDS Account Numbers associated with the
                    Organization. </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <uc:OrganizationSearch ID="ucOrganizationSearch" runat="server"></uc:OrganizationSearch>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <uc:OrganizationSearchResults ID="ucOrganizationSearchResults" runat="server"></uc:OrganizationSearchResults>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

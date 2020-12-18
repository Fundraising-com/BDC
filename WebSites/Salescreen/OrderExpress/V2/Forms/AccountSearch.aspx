<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="AccountSearch.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.AccountSearch" %>

<%@ Register TagPrefix="uc" TagName="AccountSearch" Src="~/V2/UserControls/AccountSearch.ascx" %>
<%@ Register TagPrefix="uc" TagName="AccountSearchResults" Src="~/V2/UserControls/AccountSearchResults.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Account:&nbsp;</span><span class="PageHeader2">Search</span>
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    To locate an Account, use the Search and Filter features and click on "Search accounts"
                    button.
                    <br />
                    <br />
                    An Account can be the entire Organization, i.e. ABC Middle School [when there's
                    only one QSP Program], or a group within an Organization, i.e. ABC Middle School
                    7th Grade, ABC Middle School Music Club, etc [when there are multiple QSP Programs].
                    Every Account is assigned a unique EDS account number based on the type of program,
                    i.e. DM, Food, Gift, MMB, etc. </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <uc:AccountSearch ID="ucAccountSearch" runat="server"></uc:AccountSearch>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <uc:AccountSearchResults ID="ucAccountSearchResults" runat="server"></uc:AccountSearchResults>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

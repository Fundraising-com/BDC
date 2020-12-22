<%@ Page Title="" Language="C#" MasterPageFile="~/V2/Site.Master" AutoEventWireup="true"
    CodeBehind="ProgramAgreementSearch.aspx.cs" Inherits="QSP.OrderExpress.Web.V2.Forms.ProgramAgreementSearch" %>

<%@ Register TagPrefix="uc" TagName="ProgramAgreementSearch" Src="~/V2/UserControls/ProgramAgreementSearch.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProgramAgreementSearchResults" Src="~/V2/UserControls/ProgramAgreementSearchResults.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <span class="PageHeader1">Program agreement:&nbsp;</span><span class="PageHeader2">Search</span>
                <br />
                <br />
                <span class="Text_Directions">Directions:</span> <span class="Text_Small">
                    <br />
                    To locate a Program Agreement, use the Search and Filter features and click on "Search
                    program agreements" button. </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <uc:ProgramAgreementSearch ID="ucProgramAgreementSearch" runat="server"></uc:ProgramAgreementSearch>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <uc:ProgramAgreementSearchResults ID="ucProgramAgreementSearchResults" runat="server">
                </uc:ProgramAgreementSearchResults>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

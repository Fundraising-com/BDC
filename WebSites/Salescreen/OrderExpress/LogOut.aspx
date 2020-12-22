<%@ Page Language="C#" MasterPageFile="~/SecondaryMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.LogOut" Codebehind="LogOut.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <table id="Table1" height="200px" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="center" colspan="2" height="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
    </table>
    <table id="TblLogOut" height="100%" cellspacing="0" cellpadding="0" width="100%"
        border="0">
        <tr>
            <td align="center" colspan="2" height="100%">
                <p>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <font face="Verdana" size="6">Thank you for Using Order Express!</font></p>
                <p>
                    <asp:Label ID="lblSignOut" runat="server" Font-Names="Verdana" Font-Size="Large">Label</asp:Label></p>
                <p>
                    <font size="6">
                        <asp:HyperLink ID="HypLnkSignIn" runat="server" Font-Names="Verdana" NavigateUrl="Login.aspx"
                            Font-Size="16">Sign In</asp:HyperLink></font></p>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMessage" runat="server" CssClass="eRewardsError" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <table id="Table2" height="150px" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="center" colspan="2" height="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/SecondaryMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.SessionOffPage" Codebehind="SessionOffPage.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ MasterType VirtualPath="~/SecondaryMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
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
    <table id="tblErrorPage" height="100%" cellspacing="0" cellpadding="0" width="100%"
        align="center" border="0" >
        <tr>
            <td valign="top" align="center" width="100%" height="100%">
                <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
                    border="0">
                    <tr>
                        <td valign="middle" align="center" width="100%" height="100%">
                            <table class="Login" id="tblWelcome" cellspacing="0" cellpadding="0" width="550"
                                align="middle" border="0" runat="server">
                                <tr>
                                    <td style="font-size: 20px" rowspan="6">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br>
                                        <asp:Label ID="lblTitle" runat="server" CssClass="Login" Font-Size="16"> Your session information has expired.
                                        </asp:Label><br>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br>
                                        <asp:Label ID="lblInstruction" runat="server" Font-Size="12pt" Font-Bold="True">
												For security purpose please <b>Sign In Again</b>, by clicking on the link bellow.
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <br>
                                        <asp:HyperLink ID="HypLnkSignIn" runat="server" Font-Names="Verdana" NavigateUrl="Login.aspx"
                                            Font-Size="Large">Sign In</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <br>
                                        <asp:Label ID="lblMessage" runat="server" CssClass="eRewardsError"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="center" width="100%" height="100%">
                        </td>
                    </tr>
                    <tr>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table3" height="200px" cellspacing="0" cellpadding="0" width="100%" border="0">
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

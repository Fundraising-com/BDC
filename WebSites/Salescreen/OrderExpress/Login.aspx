<%@ Page Language="C#" MasterPageFile="~/SecondaryMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.Login" Codebehind="Login.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ MasterType VirtualPath="~/SecondaryMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <table id="TblLoginPage" height="100%" cellspacing="0" cellpadding="0" width="100%"
        align="center" border="0">
        <tr>
            <td valign="top" align="center" width="100%" height="100%">
                <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
                    border="0">
                    <tr>
                        <td valign="top" align="left" width="100%" height="100%">
                            <br>
                            <br>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="top" width="20">
                                        <br>
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0" width="120">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSectionTitle" runat="server" CssClass="SectionTitleLabel">Login:</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPageTitle" CssClass="PageTitleLabel" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="300" border="0">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDirectionTitle" CssClass="DirectionTitleLabel" runat="server">
																Directions
                                                    </asp:Label>
                                                    <br>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblInstruction" CssClass="DirectionLabel" runat="server">
																<br>To login, click on enter your User Name and Password, and click on Login button.
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br>
                                                    <table cellspacing="0" cellpadding="0" border="0" width="250">
                                                        <tr>
                                                            <td align="right">
                                                                <table cellspacing="0" cellpadding="2" border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblLogin" runat="server" CssClass="LoginLabel">
																						Username:&nbsp;
                                                                            </asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtLogin" runat="server" CssClass="LoginTextBox" Width="98px" ToolTip="Please enter you user name"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:RequiredFieldValidator ID="ReqFieldVal_UserName" runat="server" CssClass="LabelError"
                                                                                ErrorMessage="The User Name is required" ControlToValidate="txtLogin">*</asp:RequiredFieldValidator></td>
                                                                        <td>
                                                                            <asp:RegularExpressionValidator ID="RegExpVal_UserName" runat="server" CssClass="LabelError"
                                                                                ErrorMessage="The '<' or '>' characters are not accepted." ValidationExpression="[^<>]*"
                                                                                ControlToValidate="txtLogin">*</asp:RegularExpressionValidator></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblPwd" runat="server" CssClass="LoginLabel">Password:&nbsp;</asp:Label></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPwd" runat="server" CssClass="LoginTextBox" Width="98px" TextMode="Password"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:RequiredFieldValidator ID="ReqFieldVal_pwd" runat="server" CssClass="LabelError"
                                                                                ErrorMessage="The Password is required" ControlToValidate="txtPwd">*</asp:RequiredFieldValidator></td>
                                                                        <td>
                                                                            <asp:RegularExpressionValidator ID="RegExpVal_pwd" runat="server" CssClass="LabelError"
                                                                                ErrorMessage="The '<' or '>' characters are not accepted." ValidationExpression="[^<>]*"
                                                                                ControlToValidate="txtPwd">*</asp:RegularExpressionValidator></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:ImageButton ID="imgbtnLogin" runat="server" CssClass="imgRollover" ImageUrl="images/btnLogin.gif"
                                                                                AlternateText="Click here to enter QSP Order Express !"></asp:ImageButton></td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="5" height="31">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblMessage" runat="server" CssClass="LabelError" ForeColor="Red"></asp:Label></td>
                                                            <td>
                                                                <asp:ValidationSummary ID="ValSum" runat="server" CssClass="LabelError"></asp:ValidationSummary>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br>
                <br>
                <br>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblModeLogin" runat="server" Visible="False">0</asp:Label></td>
        </tr>
    </table>
    <table id="Table2" height="220px" cellspacing="0" cellpadding="0" width="100%" border="0">
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

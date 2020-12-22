<%@ Reference Control="~/UserControls/AccountList.ascx" %>
<%@ Reference Control="CreditApplicationDetail.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountStep_Confirmation"
    CodeBehind="AccountStep_Confirmation.ascx.cs" %>
<style type="text/css">
    .LinkButton
    {
        border-style: solid;
        border-width: 1px;
        border-color: #336699;
        text-decoration: none;
        padding: 4px;
        color: #333333;
        background-color: #DDDDDD;
    }
</style>
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr>
        <td align="left">
            <!--Section Title -->
        </td>
    </tr>
    <tr id="trTitleConfirmation" runat="server" visible="False">
        <td align="left">
            <!--Section Body -->
            <table id="Table5" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="StandardLabel" Visible="False"> 4 - The Account Creation process is now terminated. You may continue and add Order to this new account :
                        </asp:Label><br>
                        <br>
                    </td>
                    <td valign="top">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <!--Section Body -->
        </td>
    </tr>
    <tr id="trConfirmation" runat="server">
        <td>
            <br>
            <table id="Table6" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <table id="Table12" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMessageConfirmation" runat="server" CssClass="StandardLabel">
										The Account have been saved sucessfully with the status proceed.<br>													
                                    </asp:Label><br>
                                    <br>
                                </td>
                            </tr>
                        </table>
                        <table id="tblCreditApp" cellspacing="0" cellpadding="0" border="0" runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCreditAppMsg" runat="server" CssClass="DescLabel">
										Please Note that <b>All New Account must fill a credit application form</b>.  
										You can enter new order on this account, but no order will be proceed until
										a valid credit application will be approved.
                                    </asp:Label><br>
                                    <br>
                                    <asp:Label ID="lblCreditAppMsgBtn" runat="server" CssClass="DescLabel">									
										You can fill right now this form by clicking the button above or 
										later by going to the detail of the account.												
                                    </asp:Label><br>
                                    <br>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="500" border="0">
                                        <tr>
                                            <td align="center">
                                                <asp:ImageButton ID="imgBtnCreditApplication" runat="server" ToolTip="Click here to fill the Credit Application Form"
                                                    AlternateText="Click here to fill the Credit Application Form" ImageUrl="~/images/btnCreditApplication.gif"
                                                    CausesValidation="False"></asp:ImageButton>
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
    <tr>
        <td>
            <br>
            <br>
        </td>
    </tr>
    <tr>
        <td align="left">
            <br>
            <asp:HyperLink ID="hlAccountPrint" runat="server" CssClass="LinkButton" Width="100px"
                Target="_blank">Print account</asp:HyperLink>
            &nbsp;
            <asp:HyperLink ID="hlAccountSearch" runat="server" CssClass="LinkButton" Width="120px">Account search</asp:HyperLink>
            &nbsp;
            <asp:HyperLink ID="hlAccountCreate" runat="server" CssClass="LinkButton" Width="120px">Create account</asp:HyperLink>
            &nbsp;
            <asp:HyperLink ID="hlOrderCreate" runat="server" CssClass="LinkButton" Width="100px">Create order</asp:HyperLink>
        </td>
    </tr>
</table>

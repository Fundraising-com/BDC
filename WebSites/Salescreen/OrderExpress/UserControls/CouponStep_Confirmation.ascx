<%@ Control Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.UserControls.CouponStep_Confirmation" Codebehind="CouponStep_Confirmation.ascx.cs" %>
<link href="Styles.css" rel="stylesheet" type="text/css" />
<table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td align="left">
            <!--Section Title -->
        </td>
    </tr>
    <tr>
        <td align="left">
            <!--Section Body -->
            <table id="Table5" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="StandardLabel" Visible="False">The Promotion Creation process is now terminated :
						</asp:Label><br />
                        <br />
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
    <tr>
        <td>
            <br />
            <table id="Table6" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        &nbsp; &nbsp;&nbsp;
                    </td>
                    <td>
                        <table id="Table12" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMessageConfirmation" runat="server" CssClass="DescInfoLabel" Font-Name="Times New Roman" Font-Names="Times New Roman">The Promotion has been saved sucessfully.<br>													
									</asp:Label><br />
                                    <br />
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
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td align="center">
            <br />
            <table border="0" cellpadding="0" cellspacing="0" width="500">
                <tr>
                    <td align="center">
                        <asp:ImageButton ID="btnAgreementList" runat="server" ImageUrl="~/images/btnAgreementList.gif" OnClick="ImageButton1_Click"/>
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="btnAddnew" runat="server" ImageUrl="~/images/BtnAddNew.gif" OnClick="ImageButton2_Click"/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

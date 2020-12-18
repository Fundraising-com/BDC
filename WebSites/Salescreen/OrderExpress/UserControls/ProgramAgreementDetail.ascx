<%@ Reference Control="ToolBar.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramAgreementDetail"
    CodeBehind="ProgramAgreementDetail.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ProgramAgreementHeaderDetailForm" Src="ProgramAgreementHeaderDetailForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProgramAgreementInfo" Src="ProgramAgreementInfo.ascx" %>
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr id="EditionRow" runat="server">
        <td>
            <uc1:ProgramAgreementHeaderDetailForm ID="HeaderDetail" runat="server"></uc1:ProgramAgreementHeaderDetailForm>
            <br />
        </td>
    </tr>
    <tr id="ValidationRow" runat="server">
        <td align="left">
            <uc1:ProgramAgreementInfo ID="ProgramAgreementInfoControl" runat="server"></uc1:ProgramAgreementInfo>
            <br />
        </td>
    </tr>
    <tr id="EditionButtonRow" runat="server">
        <td align="center">
            <br>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td align="center" style="width: 601px">
                        <table id="Table2" cellspacing="0" cellpadding="0" width="600" border="0">
                            <tr valign="middle">
                                <td align="center" style="height: 27px">
                                    <asp:ImageButton ID="DeleteButton" runat="server" CausesValidation="False" AlternateText="Cancel the PA"
                                        ImageUrl="~/images/btnDelete.gif" OnClientClick="if(!confirm('Are you sure that you want to delete this PA?'))return false;">
                                    </asp:ImageButton>
                                </td>
                                <td align="center" style="height: 27px">
                                    <asp:ImageButton ID="ValidateButton" runat="server" CausesValidation="False" AlternateText="Confirm Changes"
                                        ImageUrl="~/images/btnConfirm.gif" Style="margin-top: 3px;"></asp:ImageButton>
                                </td>
                                <td align="center" style="height: 27px">
                                    <asp:HyperLink ID="CancelHyperLinkEdition" runat="server" ImageUrl="~/images/btnCloseWithoutChange.gif"
                                        NavigateUrl="javascript:window.close();">Close and do not save</asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="ValidationButtonRow" runat="server">
        <td align="center" style="height: 116px">
            <br>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td align="center">
                        <table id="Table2sss" cellspacing="0" cellpadding="0" width="400" border="0">
                            <tr>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="BackButton" runat="server" CausesValidation="False" AlternateText="Back"
                                        ImageUrl="~/images/btnBack.gif"></asp:ImageButton>
                                </td>
                                <td align="center">
                                    <table id="Table2ssss" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td align="center">
                                                <asp:ImageButton ID="ProceedButton" runat="server" CausesValidation="False" ImageUrl="~/images/btnSubmitPA.gif"
                                                    AlternateText="Click here to confirm your PA" ToolTip="Click here to submit and process your PA">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <br>
                                                <asp:ImageButton ID="SaveForLaterButton" runat="server" ImageUrl="~/images/btnSavePA.gif"
                                                    AlternateText="Save" CausesValidation="False"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="center" valign="top">
                                    <asp:HyperLink ID="CancelHyperLinkValidation" runat="server" ImageUrl="~/images/btnClose.gif"
                                        NavigateUrl="javascript:window.close();">Close and do not save.</asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

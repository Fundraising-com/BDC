<%@ Control Language="C#" AutoEventWireup="false" Inherits="QSP.OrderExpress.Web.UserControls.DualAddressForm" Codebehind="DualAddressForm.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AddressControlForm" Src="AddressControlForm.ascx" %>
<table id="Table3ss" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="center">
            <table id="Table3" cellspacing="0" cellpadding="0" border="0">
                <tr id="trValSumAddressInfo" runat="server" visible="false">
                    <td colspan="2" align="left">
                        <asp:ValidationSummary ID="ValSumAddressInfo" runat="server" HeaderText="Correct the following error to proceed."
                            CssClass="LabelError"></asp:ValidationSummary>
                    </td>
                </tr>
                <tr id="trBillToAdressLabel" runat="server" visible="true">
                <td colspan="2">
                <asp:Label ID="BillToAddressNoteLabel" runat="server" CssClass="NoteLabel">Note: To edit Account 'Bill To' Information, go to Account List, select the Account and click on Edit button. 
</asp:Label>
                </td>
                </tr>
                <tr class="HeaderItemStyle">
                    <td width="350" align="left">
                        <asp:Label ID="lblTitleAddressInfo" runat="server">
						&nbsp;Bill To
                        </asp:Label></td>
                    <td width="350" align="left">
                        <asp:Label ID="Label10" runat="server">
						&nbsp;Ship To
                        </asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <uc1:AddressControlForm ID="BillingAddressControlForm" runat="server" AllowPostalBox="true" OrganizationNameRequired="false"></uc1:AddressControlForm>
                    </td>
                    <td>
                        <uc1:AddressControlForm ID="ShippingAddressControlForm" runat="server" AllowPostalBox="false" OrganizationNameRequired="true"></uc1:AddressControlForm>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label26" runat="server" CssClass="RequiredSymbol">
										*&nbsp;
                                    </asp:Label></td>
                                <td>
                                    <asp:Label ID="Label27" runat="server" CssClass="RequiredSymbolLabel">
										Required Field
                                    </asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="imgBtnCopyAddress" runat="server" CausesValidation="False" ImageUrl="~/images/btnCopyAddress.gif">
                        </asp:ImageButton></td>
                </tr>
            </table>
        </td>
    </tr>
</table>

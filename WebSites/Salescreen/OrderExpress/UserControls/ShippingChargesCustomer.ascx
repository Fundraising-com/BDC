<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ShippingChargesCustomer" Codebehind="ShippingChargesCustomer.ascx.cs" %>
<table cellspacing="0" cellpadding="0" width="550" border="0">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblShippingChargesPayBy" runat="server" CssClass="StandardLabel">Expedited&nbsp;Freight&nbsp;Charges&nbsp;Paid&nbsp;By:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:Label>
                    </td>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr id="trEdit" runat="server">
                                <td>
                                    <asp:DropDownList ID="ddlPaymentAssignmentType" runat="server" Width="200px">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblPaymentAssignmentType" CssClass="DescInfoLabel" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompVal_PaymentAssignmentType" runat="server" CssClass="LabelError"
                                        ControlToValidate="ddlPaymentAssignmentType" ErrorMessage="Freight Payment Responsibility Required"
                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                </td>
                            </tr>
                            <tr id="trReadOnly" runat="server">
                                <td colspan="2">
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
            &nbsp;
        </td>
    </tr>
</table>

<%@ Reference Control="OrganizationDetail.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountHeaderForm"
    CodeBehind="AccountHeaderForm.ascx.cs" %>
<table id="Table3" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td>
            <br>
        </td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="2" border="0">
                <tr>
                    <td nowrap width="160">
                        <asp:Label ID="lblLabelAccID" runat="server" CssClass="StandardLabel">Account ID :</asp:Label>
                    </td>
                    <td nowrap width="440">
                        <asp:TextBox ID="txtAccID" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLabelTypeID" runat="server" CssClass="StandardLabel">Type :</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" DataValueField="account_type_id" DataTextField="account_type_name">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label16" runat="server" CssClass="StandardLabel">EDS Account # :</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEDSAccount" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" CssClass="StandardLabel">Organization :</asp:Label>
                    </td>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="400" border="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtOrganizationID" ReadOnly="True" runat="server" Width="50px" Enabled="True"></asp:TextBox>
                                    <td>
                                        <asp:CompareValidator ID="CompValOrgID" runat="server" CssClass="LabelError" ErrorMessage="The organization ID is invalid (must be a number)."
                                            ControlToValidate="txtOrganizationID" Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:TextBox ID="txtOrganizationName" runat="server" ReadOnly="True" Width="230px"
                                            Enabled="True"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="imgBtnDetailOrg" runat="server" ImageUrl="~/images/BtnDetail.gif"
                                            CausesValidation="False"></asp:ImageButton>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="imgBtnSelectOrg" runat="server" ImageUrl="~/images/BtnSelect.gif"
                                            CausesValidation="False"></asp:ImageButton>
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLabelFM" runat="server" CssClass="StandardLabel">Field Sales Manager :</asp:Label>
                    </td>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="400" border="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtFMID" ReadOnly="True" runat="server" Width="50px" Enabled="True"></asp:TextBox>
                                    <td>
                                        <asp:CompareValidator ID="CompValFMID" runat="server" CssClass="LabelError" ErrorMessage="The FM ID is invalid (must be a number)."
                                            ControlToValidate="txtFMID" Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:TextBox ID="txtFMName" runat="server" ReadOnly="True" Width="230px" Enabled="True"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif"
                                            CausesValidation="False"></asp:ImageButton>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="imgBtnSelect" runat="server" ImageUrl="~/images/BtnSelect.gif"
                                            CausesValidation="False"></asp:ImageButton>
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trTaxExemption" runat="server" visible="false">
                    <td>
                        <asp:Label ID="lblLabelTaxExemptionNumber" runat="server" CssClass="StandardLabel">Tax Exemption Number :</asp:Label>
                    </td>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="400" border="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtTaxExemptionNumber" runat="server" MaxLength="25" Width="200px"></asp:TextBox>
                                </td>
                                <td width="100%">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label23" runat="server" CssClass="StandardLabel">Expire&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTaxExemptionExpirationDate" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="compVal_TaxExemptionExpirationDate" runat="server" CssClass="LabelError"
                                        ErrorMessage="The Tax Exemption Expiration Date is invalid." ControlToValidate="txtTaxExemptionExpirationDate"
                                        Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLabelCreditLimit" runat="server" CssClass="StandardLabel">Credit Limit :</asp:Label>
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCreditLimit" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="compVal_CreditLimit" runat="server" CssClass="LabelError"
                                        ErrorMessage="The Credit Limit is invalid." ControlToValidate="txtCreditLimit"
                                        Operator="DataTypeCheck" Type="Currency">*</asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Label ID="lblLabelComments" runat="server" CssClass="StandardLabel">Comments :</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtComments" runat="server" MaxLength="4000" TextMode="MultiLine"
                            Rows="4" Width="400px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderHeaderDetailForm"
    CodeBehind="OrderHeaderDetailForm.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="DualAddressForm" Src="DualAddressForm.ascx" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr id="tblRowSectionAccount" runat="server">
        <td align="center">
            <table id="Table1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="lblTitleAccountInfo" runat="server">
							Account Information
                        </asp:Label>
                    </td>
                </tr>
                <tr id="trValSumAccountInfo" runat="server" visible="false">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <!--Section Body -->
                        <table id="tblAccountInfo" cellspacing="0" cellpadding="0" width="100%" border="0"
                            runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" CssClass="StandardLabel">
										QSP&nbsp;Account&nbsp;ID&nbsp;#:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblAccID" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" CssClass="StandardLabel">
											EDS&nbsp;Account&nbsp;#:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblEDSAccID" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" CssClass="StandardLabel">
										Account&nbsp;Name:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblAccountName" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trAccountStatus" runat="server" visible="False">
                                <td valign="top">
                                    <asp:Label ID="Label17" runat="server" CssClass="StandardLabel">
										Account&nbsp;Status:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAccountStatusColor" runat="server" Width="5px" BackColor="Orange"
                                                    Height="3px" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
													&nbsp;&nbsp;
                                                </asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAccountStatus_ShortDescription" runat="server" CssClass="DescInfoLabel">
													New Account
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAccountStatus" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" CssClass="StandardLabel">
										FSM&nbsp;Info:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblAccountFMInfo" runat="server" CssClass="DescInfoLabel">
										0000 - John Smith
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 38px">
                                    <asp:Label ID="Label19" runat="server" CssClass="StandardLabel">
										Organization&nbsp;Type&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td style="height: 38px">
                                </td>
                                <td valign="top" width="100%" style="height: 38px">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblOrgType" runat="server" CssClass="DescInfoLabel">
													Public
                                                </asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" CssClass="StandardLabel">
													Organization&nbsp;Level:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td width="100%">
                                                <asp:Label ID="lblOrgLevel" runat="server" CssClass="DescInfoLabel">
													High School
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label30" runat="server" CssClass="StandardLabel">
										Trade&nbsp;Class:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td valign="top" width="100%">
                                    <asp:Label ID="lblTradeClass" runat="server" CssClass="DescInfoLabel">
										None
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" CssClass="StandardLabel">
										QSP&nbsp;Program:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="lblProgramTypeName" runat="server" CssClass="DescInfoLabel">
										Chocolate</asp:Label>
                                </td>
                            </tr>
                            <tr id="trTaxExemption" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="Label6" runat="server" CssClass="StandardLabel">
											Tax&nbsp;Exemption:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td colspan="4">
                                    <table cellspacing="0" cellpadding="0" width="400" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTaxExemptionNumber" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                            <td width="100">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td width="1">
                                                <asp:Label ID="Label7" runat="server" CssClass="StandardLabel">Expire:&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTaxExemptionExpirationDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label12" runat="server" CssClass="StandardLabel">
											Comment:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%" colspan="4">
                                    <asp:Label ID="lblAccountComment" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br>
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label21" runat="server" CssClass="NoteLabel">
										Note:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" CssClass="NoteLabel">
									In some states, exemption or resale certificate forms are required to exempt an account from taxes.  The appropriate form must be submitted with the order; otherwise, invoices will include applicable taxes.
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br>
                    </td>
                </tr>
            </table>
            <uc1:DualAddressForm ID="DualAddressFormControl" runat="server" BillingEnabled="false"
                HygieneAddress="true" EntityName="Account&nbsp;Name" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td align="center">
        </td>
    </tr>
    <tr id="tblRowSectionOrder" runat="server">
        <td align="center">
            <table id="Tabless1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="lblTitleOrderInfo" runat="server">
							Order Information
                        </asp:Label>
                    </td>
                </tr>
                <tr id="trValSumOrderInfo" runat="server" visible="false">
                    <td align="left">
                        <asp:ValidationSummary ID="ValSumOrderInfo" runat="server" CssClass="LabelError"
                            HeaderText="Correct the following error to proceed."></asp:ValidationSummary>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <!--Section Body -->
                        <table id="tblOrderInfo" cellspacing="0" cellpadding="0" border="0" runat="server">
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label22" runat="server" CssClass="StandardLabel">
										Order&nbsp;ID:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblOrderID" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" CssClass="StandardLabel">
										EDS&nbsp;Order&nbsp;#&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblEDSOrderID" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label25" runat="server" CssClass="StandardLabel">
										Order&nbsp;Status:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td width="50%">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblOrderStatusColor" runat="server" BackColor="Orange" BorderWidth="1px"
                                                    BorderStyle="Solid" BorderColor="Black" CssClass="StatusLabel">
													&nbsp;&nbsp;
                                                </asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOrderStatus_ShortDescription" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOrderStatus_Description" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trFmInfo" runat="server">
                                <td>
                                    <asp:Label ID="Label5" runat="server" CssClass="StandardLabel">
									FSM&nbsp;Info:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblFMInfo" runat="server" CssClass="DescInfoLabel">
									0000 - John Smith
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr id="trFmEdit" runat="server">
                                <td>
                                    <asp:Label ID="lblLabelFM" runat="server" CssClass="StandardLabel">
									Field&nbsp;Sales&nbsp;Manager:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="400" border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtFMID" runat="server" Width="50px" CssClass="StandardTextBox"
                                                    Enabled="True"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompValFMID" runat="server" CssClass="LabelError" ErrorMessage="The FM ID is invalid (must be a number)."
                                                    ControlToValidate="txtFMID" Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
                                            </td>
                                            <td>
                                                &nbsp;<asp:TextBox ID="txtFMName" runat="server" Width="230px" CssClass="StandardTextBox"
                                                    ReadOnly="True" Enabled="True"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ReqFldVal_FMID" runat="server" CssClass="LabelError"
                                                    ErrorMessage="The FSM is required." ControlToValidate="txtFMID">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="imgBtnSelectFM" runat="server" ImageUrl="~/images/BtnSelect.gif"
                                                    CausesValidation="False"></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Label ID="lblLabelFMNote" runat="server" CssClass="NoteSmallLabel">
													Click on the Select button to access an FSM list to complete these fields.
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label31" runat="server" CssClass="StandardLabel">
									Order&nbsp;Date:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblOrderDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trOrderTypeEdit" runat="server">
                                <td valign="top">
                                    <asp:Label ID="Label34" runat="server" CssClass="StandardLabel">
								Order&nbsp;Type:<span class="RequiredSymbolLabel">*</span>
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="DescLabel" DataValueField="order_type_id"
                                                    DataTextField="order_type_name">
                                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">Standard Order</asp:ListItem>
                                                    <asp:ListItem Value="2">Pre-Sales Estimate</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompVal_Type" runat="server" CssClass="LabelError" ErrorMessage="The Order Type is required"
                                                    ControlToValidate="ddlOrderType" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trOrderTypeInfo" runat="server">
                                <td>
                                    <asp:Label ID="Label42" runat="server" CssClass="StandardLabel">
										Order&nbsp;Type:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table id="Table4" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblOrderType" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trDeliveryMethodEdit" runat="server">
                                <td valign="top">
                                    <asp:Label ID="Label35" runat="server" CssClass="StandardLabel">
										Delivery&nbsp;Method:<span class="RequiredSymbolLabel">*</span>
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlDeliveryMethod" runat="server" CssClass="DescLabel" AutoPostBack="True">
                                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">Common Carrier</asp:ListItem>
                                                    <asp:ListItem Value="2">Pick Up at Warehouse</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompVal_DeliveryMethod" runat="server" CssClass="LabelError"
                                                    ErrorMessage="The Delivery Method is required" ControlToValidate="ddlDeliveryMethod"
                                                    Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trDeliveryMethodInfo" runat="server">
                                <td>
                                    <asp:Label ID="Label41" runat="server" CssClass="StandardLabel">
										Delivery&nbsp;Method:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table id="Table2" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDeliveryMethod" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trWarehouseInfo" runat="server">
                                <td>
                                    <asp:Label ID="Label40" runat="server" CssClass="StandardLabel">
										Delivery&nbsp;Warehouse:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table id="Table833" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblWarehouseName" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trWarehouseSelector" runat="server">
                                <td valign="top">
                                    <asp:Label ID="Label11" runat="server" CssClass="StandardLabel">
								Delivery&nbsp;Warehouse:<span class="RequiredSymbolLabel">*</span>
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="400" border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtFulfWarehouseID" runat="server" Width="50px" Enabled="True"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="compVal_FulfWarehouseID" runat="server" CssClass="LabelError"
                                                    ErrorMessage="The EDS Warehouse # is invalid (must be a number)." ControlToValidate="txtFulfWarehouseID"
                                                    Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
                                            </td>
                                            <td>
                                                &nbsp;<asp:TextBox ID="txtWarehouseName" runat="server" Width="230px" ReadOnly="False"
                                                    Enabled="True"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="ReqFldVal_FulfWarehouseID" runat="server" CssClass="LabelError"
                                                    ErrorMessage="The Warehouse is required." ControlToValidate="txtFulfWarehouseID">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="imgBtnSelectWarehouse" runat="server" ImageUrl="~/images/BtnSelect.gif"
                                                    CausesValidation="False"></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Label ID="Label38" runat="server" CssClass="NoteSmallLabel">
													Click on the Select button to access a Warehouse list to complete these fields.
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trRequestedDelivery" runat="server">
                                <td>
                                    <asp:Label ID="Label36" runat="server" CssClass="StandardLabel">Requested&nbsp;Delivery&nbsp;Date:<span class="RequiredSymbolLabel">*</span></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr id="trRequestedDeliveryDate" runat="server">
                                            <td>
                                                <asp:TextBox ID="txtDeliveryDate" runat="server" Font-Size="9px" Width="100px" AutoPostBack="False"
                                                    Height="14px" Font-Names="Verdana, Arial, Tahoma" onFocus="window.focus()"></asp:TextBox>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDeliveryDate"
                                                    Mask="99/99/9999" MessageValidatorTip="false" OnFocusCssClass="StandardTextBox"
                                                    OnInvalidCssClass="StandardTextBox" OnBlurCssNegative="StandardTextBox" OnFocusCssNegative="StandardTextBox"
                                                    MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hypLnkDeliveryDate" runat="server" ImageUrl="~/images/Calendar.gif"
                                                    ToolTip="Click here to select the date from a popup calendar !" NavigateUrl="javascript:void(0);">HyperLink</asp:HyperLink>&nbsp;
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="reqFldVal_DeliveryDate" runat="server" CssClass="LabelError"
                                                    ErrorMessage="The Delivery Date is required." ControlToValidate="txtDeliveryDate">*</asp:RequiredFieldValidator><asp:CompareValidator
                                                        ID="compVal_DeliveryDate" runat="server" CssClass="LabelError" ErrorMessage="The Delivery Date is invalid"
                                                        ControlToValidate="txtDeliveryDate" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
                                                <asp:CustomValidator ID="custDeliveryDateValidator" runat="server" CssClass="LabelError"
                                                    ErrorMessage="Date Error - A Valid Date Is Required To Proceed." OnServerValidate="ValidateDeliveryDate">*</asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr id="trRequestedDeliveryOptionList" runat="server">
                                            <td>
                                                <asp:DropDownList ID="ddlRequestedDeliveryDateDropDownList" runat="server" CssClass="DescLabel">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trRequestedDeliveryTime" runat="server">
                                <td>
                                    <asp:Label ID="Label13" runat="server" CssClass="StandardLabel">Requested&nbsp;Delivery&nbsp;Time:<span class="RequiredSymbolLabel">*</span></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDeliveryTime" runat="server" Font-Size="9px" Width="100px" AutoPostBack="False"
                                                    Height="14px" Font-Names="Verdana, Arial, Tahoma"></asp:TextBox>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDeliveryTime"
                                                    Mask="99:99" MaskType="Time" MessageValidatorTip="false" OnFocusCssClass="StandardTextBox"
                                                    OnInvalidCssClass="StandardTextBox" OnBlurCssNegative="StandardTextBox" OnFocusCssNegative="StandardTextBox"
                                                    DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="reqFldVal_DeliveryTime" runat="server" CssClass="LabelError"
                                                    ErrorMessage="The delivery time is required." ControlToValidate="txtDeliveryTime">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator CssClass="LabelError" ErrorMessage="The delivery time is not valid."
                                                    ControlToValidate="txtDeliveryTime" runat="server" ID="regExVal_DeliveryTime"
                                                    ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trRequestedLeadTime" runat="server">
                                <td>
                                    <asp:Label ID="Label37" runat="server" CssClass="StandardLabel" Width="150px">Requested&nbsp;Lead-Time:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDayLeadTime" runat="server" CssClass="DescInfoLabel"> 0
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label33" runat="server" CssClass="StandardLabel">&nbsp;&nbsp;Business&nbsp;Day(s)
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblLabelShippingDate" runat="server" CssClass="StandardLabel"> Shipping&nbsp;Date:
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblShippingDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label44" runat="server" CssClass="StandardLabel">
										Customer&nbsp;PO#:
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomerPO_Number" runat="server" CssClass="DescLabel" MaxLength="10"
                                        Columns="10"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label32" runat="server" CssClass="StandardLabel">
										Comment:
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComment" runat="server" CssClass="DescLabel" MaxLength="60" Columns="60"
                                        TextMode="MultiLine"></asp:TextBox>
                                    <br>
                                    <asp:Label ID="Label39" runat="server" CssClass="NoteSmallLabel">
										Comment field is limited to 60 characters/spaces.
                                    </asp:Label>
                                    <br />
                                    <asp:Label ID="lblComments" runat="server" CssClass="CommentBoxStandardLabel">
								    Note: To avoid delaying this order, only use an appropriate delivery-related comment in this field.
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblExpFreightComment" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="left">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="Label18" runat="server" CssClass="CommentBoxStandardLabel">
													*&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label24" runat="server" CssClass="CommentBoxStandardLabel">
													Required Field
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trBusinessMessage" runat="server" visible="false">
                    <td align="left" colspan="5">
                        <!--Section Body -->
                        <br>
                        <table id="Table533" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblBusinessMessage" runat="server" CssClass="BizRuleLabel"></asp:Label>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br>
            <table id="Tablesds1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="Label14" runat="server">
								Order Terms
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <!--Section Body -->
                        <table id="Table5" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td colspan="5">
                                    <asp:Label ID="Label15" runat="server" CssClass="StandardLabel" Font-Size="xx-small">
											You are in agreement that QSP will be working with your organization in connection with a fundraising program as follows:
                                    </asp:Label><br>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label16" runat="server" CssClass="StandardLabel">Start&nbsp;Date&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblStartDate" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label29" runat="server" CssClass="StandardLabel">End&nbsp;Date&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblEndDate" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="Table6" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label23" runat="server" CssClass="StandardLabel" Width="200px">GOAL-&nbsp;Estimated&nbsp;Gross&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEstimatedAmount" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="Table7" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label28" runat="server" CssClass="StandardLabel" Width="200px">Enrollment&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEnrollment" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trProfitRate" runat="server">
                                <td>
                                    <asp:Label ID="Label45" runat="server" CssClass="StandardLabel" Width="200px">Account&nbsp;Profit&nbsp;:</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblProfitRate" runat="server" Width="100px" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br>
        </td>
    </tr>
    <tr>
        <td align="center">
        </td>
    </tr>
</table>

<script language="javascript">
    function ShowHideWarehouseSelector() 
    {
        var trWarehouseSelector = document.getElementById('<%=this.trWarehouseSelector.ClientID%>');
        var trWarehouseInfo = document.getElementById('<%=this.trWarehouseInfo.ClientID%>');
        var ddlDeliveryMethod = document.getElementById('<%=this.ddlDeliveryMethod.ClientID%>');

        if (ddlDeliveryMethod != null) 
        {
            if (ddlDeliveryMethod.selectedIndex == 2) 
            {
                trWarehouseSelector.style.display = "block";

                // ResetWHSE();
                
            }
            else 
            {
                trWarehouseSelector.style.display = "none";
            }
        }
    }

    function ResetWHSE() 
    {
        //var wareID = document.getElementById('<%=this.txtFulfWarehouseID.ClientID%>');
        //var wareName = document.getElementById('<%=this.txtWarehouseName.ClientID%>');
        //wareID.value = "";
        //wareName.value = "";

        // alert("reset whse!");
    }
</script>


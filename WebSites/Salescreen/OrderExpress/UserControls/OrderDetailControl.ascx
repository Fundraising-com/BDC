<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderDetailControl"
    CodeBehind="OrderDetailControl.ascx.cs" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="OrderSupplyListInfo" Src="~/UserControls/OrderSupplyListInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DualAddressForm" Src="~/UserControls/DualAddressForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressControlInfo" Src="~/UserControls/AddressControlInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderDetailSectionList" Src="OrderDetailSectionList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EntityExceptionList" Src="~/UserControls/EntityExceptionList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderDetailSectionListInfo" Src="~/UserControls/OrderDetailSectionListInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderSupplyForm" Src="~/UserControls/OrderSupplyForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChargeList" Src="~/UserControls/OrderChargeList.ascx" %>
<%@ Register Namespace="QSP.WebControl.Reporting" TagPrefix="rs" Assembly="QSP.WebControl" %>
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr id="trStatusMessage" runat="server" visible="false">
        <td style="height: 19px">
            <asp:Label ID="Label72" runat="server" Font-Size="7pt" ForeColor="Red">
								Status Note 
            </asp:Label>
        </td>
    </tr>
    <tr id="trCampInfoTitle" runat="server">
        <td align="left">
            <!--Section Body -->
            <br>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Image Height="80px" ID="imgBusinessForm" runat="server" />
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <table id="tblCampInfoTitle" cellspacing="0" cellpadding="0" border="0">
                            <tr id="trAccountInfoTitle" runat="server">
                                <td style="height: 19px">
                                    <asp:Label ID="Label2" runat="server" CssClass="FormTitleLabel"> Account :
                                    </asp:Label>
                                </td>
                                <td style="height: 19px">
                                    <asp:Label ID="lblAccountNumber" runat="server" CssClass="FormTitleDescLabel">
										            00000
                                    </asp:Label>
                                </td>
                                <td style="height: 19px">
                                    &nbsp;-&nbsp;
                                </td>
                                <td style="height: 19px">
                                    <asp:Label ID="lblAccountName123" runat="server" CssClass="FormTitleDescLabel">
										            Account Name
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr id="trFormInfoTitle" runat="server">
                                <td style="height: 18px">
                                    <asp:Label ID="Label3" runat="server" CssClass="FormTitleLabel"> Order Form :
                                    </asp:Label>
                                </td>
                                <td align="right" style="height: 18px">
                                    <asp:Label ID="lblFormID" runat="server" CssClass="FormTitleDescLabel">
										            23
                                    </asp:Label>
                                </td>
                                <td style="height: 18px">
                                    &nbsp;-&nbsp;
                                </td>
                                <td style="height: 18px">
                                    <asp:Label ID="lblFormName" runat="server" CssClass="FormTitleDescLabel">
										            WFC WarehouseStock Order Form
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br>
        </td>
    </tr>
    <tr id="tblRowSectionAccount" runat="server">
        <td align="center">
            <table id="Table1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr align="left">
                    <td class="SectionPageTitleInfo">
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
                                    <asp:Label ID="Label1" runat="server" CssClass="StandardLabel">
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
                                    <asp:Label ID="Label4" runat="server" CssClass="StandardLabel">
											EDS&nbsp;Account&nbsp;#:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblEDSAccID" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trAccountNameEdit" runat="server">
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
                            <tr id="trAccountNameInfo" runat="server">
                                <td>
                                    <asp:Label ID="Label5" runat="server" CssClass="StandardLabel">
										Account&nbsp;Name:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table id="Table13" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAccountNameInfo" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnDetailAccount" runat="server" Height="15px" CausesValidation="False"
                                                    ToolTip="Click here to Access to Account Detail!" ImageUrl="~/images/BtnDetail.gif">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
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
                                                <asp:Label ID="lblAccountStatusColor" runat="server" BackColor="Orange" BorderWidth="1px"
                                                    BorderStyle="Solid" BorderColor="Black" CssClass="StatusLabel">
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
                                    <asp:Label ID="Label6" runat="server" CssClass="StandardLabel">
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
                                <td>
                                    <asp:Label ID="Label19" runat="server" CssClass="StandardLabel">
										Organization&nbsp;Type&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td valign="top" width="100%">
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
                                    <asp:Label ID="Label7" runat="server" CssClass="StandardLabel">
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
                            <tr>
                                <td>
                                    <asp:Label ID="Label41" CssClass="StandardLabel" runat="server">
										Last&nbsp;Fiscal&nbsp;Year:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td valign="top" width="100%">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblLastFiscalYear" CssClass="DescInfoLabel" Width="200px" runat="server"></asp:Label>
                                            </td>
                                            <td width="100%">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label42" CssClass="StandardLabel" runat="server">Last&nbsp;Order&nbsp;Date:&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLastOrderDate" CssClass="DescInfoLabel" runat="server" Width="200px"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trTaxExemption" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="Label8" runat="server" CssClass="StandardLabel">
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
                                                <asp:Label ID="Label10" runat="server" CssClass="StandardLabel">Expire:&nbsp;</asp:Label>
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
                                <td align="left">
                                    <asp:Label ID="Label11" runat="server" CssClass="NoteLabel">
									In some states, exemption or resale certificate forms are required to exempt an account from taxes.  The appropriate form must be submitted with the order; otherwise, invoices will include applicable taxes.
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;
        </td>
    </tr>
    <tr id="trAccountException" runat="server">
        <td align="center">
            <table id="tblAccountException" runat="server" border="0" cellpadding="0" cellspacing="0"
                width="700">
                <tr align="left">
                    <td class="SectionPageTitleInfo" style="height: 19px">
                        <asp:Label ID="Label59" runat="server">
								Account Note
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:EntityExceptionList ID="AccountExceptionList" IsReadOnly="True" runat="server">
                        </uc1:EntityExceptionList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <br>
        </td>
    </tr>
    <tr id="trShippingDetail" runat="server">
        <td align="center">
            <uc1:DualAddressForm ID="DualAddressFormControl" runat="server" BillingEnabled="false"
                EntityName="Account&nbsp;Name" HygieneAddress="true" />
        </td>
    </tr>
    <tr id="trShippingReadOnly" runat="server">
        <td align="center">
            <table id="Table2" cellspacing="0" cellpadding="0" border="0">
                <tr class="HeaderItemStyle">
                    <td width="350" style="height: 19px" align="left">
                        <asp:Label ID="Label60" runat="server">
							&nbsp;Bill To
                        </asp:Label>
                    </td>
                    <td width="350" style="height: 19px" align="left">
                        <asp:Label ID="Label61" runat="server">
							&nbsp;Ship To
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:AddressControlInfo ID="AddressInfo_Billing" LabelOrgNameText="Account&nbsp;Name"
                            runat="server"></uc1:AddressControlInfo>
                    </td>
                    <td>
                        <uc1:AddressControlInfo ID="AddressInfo_Shipping" LabelOrgNameText="Account&nbsp;Name"
                            runat="server"></uc1:AddressControlInfo>
                    </td>
                </tr>
            </table>
            <br>
        </td>
    </tr>
    <tr id="tblRowSectionOrder" runat="server">
        <td align="center">
            <table id="Tabless1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr align="left">
                    <td class="SectionPageTitleInfo">
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
                                    <asp:Label ID="Label14" runat="server" CssClass="StandardLabel">
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
                                    <asp:Label ID="Label15" runat="server" CssClass="StandardLabel">
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
                            <tr id="trFmDetail" runat="server">
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
                                            <td style="width: 10px">
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
                            <tr id="trOrderTypeReadOnly" runat="server">
                                <td valign="top">
                                    <asp:Label ID="Label73" runat="server" CssClass="StandardLabel">
											Order Type:
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblOrderTypeName" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trDeliveryMethodReadOnly" runat="server">
                                <td valign="top">
                                    <asp:Label ID="Label74" runat="server" CssClass="StandardLabel">
											Delivery Method:
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblDeliveryMethodName" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trWareHouseReadOnly" runat="server">
                                <td>
                                    <asp:Label ID="Label75" runat="server" CssClass="StandardLabel">
											Warehouse&nbsp;Name:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table id="Table12" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label76" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnWarehouse" runat="server" CausesValidation="False" ImageUrl="~/images/BtnDetail.gif">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
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
                                            <td style="height: 22px">
                                                <asp:DropDownList ID="ddlDeliveryMethod" runat="server" CssClass="DescLabel" OnSelectedIndexChanged="ddlDeliveryMethod_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">Common Carrier</asp:ListItem>
                                                    <asp:ListItem Value="2">Pick Up at Warehouse</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 22px">
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
                            <tr id="trDeliveryInfo" runat="Server">
                                <td>
                                    <asp:Label ID="lblLabelDeliveryDate" runat="server" CssClass="StandardLabel">Requested Delivery Date:
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblDeliveryDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trRequestedDeliveryTime" runat="Server">
                                <td>
                                    <asp:Label ID="Label26" runat="server" CssClass="StandardLabel">Requested Delivery Time:
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblDeliveryTime" runat="server" CssClass="DescInfoLabel"></asp:Label>
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
                                    <asp:Label ID="Label16" runat="server" CssClass="StandardLabel">
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
                            <tr id="trDeliveryDetail" runat="server">
                                <td>
                                    <asp:Label ID="Label36" runat="server" CssClass="StandardLabel">Requested&nbsp;Delivery&nbsp;Date:<span class="RequiredSymbolLabel">*</span>
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr id="trRequestedDeliveryDate" runat="server">
                                            <td>
                                                <asp:TextBox ID="txtDeliveryDate" runat="server" Font-Size="9px" Width="100px" AutoPostBack="False"
                                                    Height="14px" Font-Names="Verdana, Arial, Tahoma"></asp:TextBox>
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
                                                    ErrorMessage="Date Error - A Valid Date Is Required To Proceed." Display="dynamic"
                                                    OnServerValidate="ValidateDeliveryDate">*</asp:CustomValidator>
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
                            <tr id="trRequestedDeliveryTimeEdit" runat="server">
                                <td>
                                    <asp:Label ID="Label27" runat="server" CssClass="StandardLabel">Requested&nbsp;Delivery&nbsp;Time:<span class="RequiredSymbolLabel">*</span></asp:Label>
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
                            <tr id="trCustomerPOInfo" runat="server">
                                <td>
                                    <asp:Label ID="lblLabelCustomerPO_Number" runat="server" CssClass="StandardLabel"> Customer&nbsp;PO#:
                                    </asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblCustomerPO_Number" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trCommentsInfo" runat="Server">
                                <td>
                                    <asp:Label ID="Label48" runat="server" CssClass="StandardLabel">
											Comment:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblComment" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trCustomerPODetail" runat="server">
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
                            <tr id="trCommentsDetail" runat="server">
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
                            <tr id="trRequiredField" runat="server">
                                <td colspan="3">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="Label18" runat="server" CssClass="RequiredSymbol">
													*&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label24" runat="server" CssClass="RequiredSymbolLabel">
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
                    <td align="center" colspan="5">
                        <!--Section Body -->
                        <br>
                        <table id="Table533" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    wewewewewewewewewe
                                    <asp:Label ID="lblBusinessMessage" runat="server" CssClass="BizRuleLabel"></asp:Label>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br>
            <br>
            <br />
        </td>
    </tr>
    <tr id="trOrderTerms" runat="server">
        <td align="center">
            <table id="Tablesds1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr align="left">
                    <td class="SectionPageTitleInfo">
                        <asp:Label ID="Label23" runat="server">
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
                                    <asp:Label ID="Label28" runat="server" CssClass="StandardLabel" Font-Size="xx-small">
											You are in agreement that QSP will be working with your organization in connection with a fundraising program as follows:
                                    </asp:Label><br>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label29" runat="server" CssClass="StandardLabel">Start&nbsp;Date&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblStartDate" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label45" runat="server" CssClass="StandardLabel">End&nbsp;Date&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblEndDate" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="Table6" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label46" runat="server" CssClass="StandardLabel" Width="200px">GOAL-&nbsp;Estimated&nbsp;Gross&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEstimatedAmount" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="Table7" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label47" runat="server" CssClass="StandardLabel" Width="200px">Enrollment&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEnrollment" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trProfitRate" runat="server">
                                <td>
                                    <asp:Label ID="Label77" runat="server" CssClass="StandardLabel" Width="200px">Account&nbsp;Profit&nbsp;:</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblProfitRate" runat="server" Width="100px" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;
        </td>
    </tr>
    <tr id="trOrderInfo_Standard" runat="server">
        <td align="center">
            <table id="Tablesfds1e" cellspacing="0" cellpadding="0" border="0">
                <tr align="left">
                    <td class="SectionPageTitleInfo">
                        <asp:Label ID="Label62" runat="server">
								Order Detail - Standard Product
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br>
                        <uc1:OrderDetailSectionListInfo ID="OrderDetailSectionListInfoFinal" runat="server">
                        </uc1:OrderDetailSectionListInfo>
                        <br>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trOrderInfo_Optional" runat="server">
        <td align="center">
            <table id="Table4" cellspacing="0" cellpadding="0" border="0">
                <tr align="left">
                    <td class="SectionPageTitleInfo">
                        <asp:Label ID="Label63" runat="server">
								Order Detail - Optional Product
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br>
                        <uc1:OrderDetailSectionListInfo ID="OrderDetailSectionListInfo_Optional" runat="server">
                        </uc1:OrderDetailSectionListInfo>
                        <br>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;
        </td>
    </tr>
    <tr id="trOrderDetaillabel" runat="server">
        <td class="SectionPageTitleInfo" align="left">
            <asp:Label ID="Label71" runat="server">
							Order Detail
            </asp:Label>
        </td>
    </tr>
    <tr id="trOrderDetail_Standard" runat="server">
        <td>
            <table id="Table10" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="background-color: transparent">
                        <iewc:TabStrip ID="TbStrp_Form" runat="server" AutoPostBack="False" SepDefaultStyle="border-bottom:solid 1px #000000; background: transparent;"
                            TargetID="multPage_Form" TabDefaultStyle="border-bottom: #006699 2px solid; background-color: transparent;"
                            BackColor="LightGoldenrodYellow">
                            <iewc:Tab DefaultImageUrl="images/tabForm_StandardProduct_off.gif" SelectedImageUrl="images/tabForm_StandardProduct_on.gif">
                            </iewc:Tab>
                            <iewc:TabSeparator></iewc:TabSeparator>
                            <iewc:Tab DefaultImageUrl="images/tabForm_StockProduct_off.gif" SelectedImageUrl="images/tabForm_StockProduct_on.gif">
                            </iewc:Tab>
                            <iewc:TabSeparator DefaultStyle="width:100%;background-color:transparent;"></iewc:TabSeparator>
                        </iewc:TabStrip>
                        <iewc:MultiPage ID="multPage_Form" Style="border-right: #bfbfbf 2px outset; padding-right: 5px;
                            border-top: medium none; padding-left: 5px; padding-bottom: 5px; border-left: #bfbfbf 2px outset;
                            padding-top: 5px; border-bottom: #bfbfbf 2px outset" runat="server" Height="100%"
                            Width="750px">
                            <iewc:PageView>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="5px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl1" CssClass="NoteLabel">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <uc1:OrderDetailSectionList ID="OrderDetailSectionListStep" runat="server"></uc1:OrderDetailSectionList>
                                            <br>
                                        </td>
                                    </tr>
                                </table>
                            </iewc:PageView>
                            <iewc:PageView>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="5px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label runat="server" Height="300px" ID="lblNonAvailableOptionalSectionType"
                                                Font-Size="14px" CssClass="StandardLabel">
						                                            <br><br><br>No Products are Available in this Type of Section.
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <uc1:OrderDetailSectionList ID="OrderDetailSectionListStep_Optional" runat="server">
                                            </uc1:OrderDetailSectionList>
                                            <br>
                                        </td>
                                    </tr>
                                </table>
                            </iewc:PageView>
                        </iewc:MultiPage>
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <br>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ImageButton ID="imgBtnShowSupply" runat="server" ImageUrl="~/images/btnShowSupply.gif"
                AlternateText="Show Supply Section" CausesValidation="False" OnClick="imgBtnShowSupply_Click"
                Visible="false"></asp:ImageButton>
        </td>
    </tr>
    <tr id="trOrderSupplyOrderDetail" runat="server">
        <td align="center">
            <br>
            <table id="Table8" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="SectionPageTitleInfo">
                        <table id="Table621" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr align="left">
                                <td class="SectionPageTitleInfo">
                                    <asp:Label ID="Label70" runat="server">
													Supply Order Detail
                                    </asp:Label>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgBtnHideSupply" runat="server" ImageUrl="~/images/btnHideSupply.gif"
                                        AlternateText="Hide Supply Section" CausesValidation="False" OnClick="imgBtnHideSupply_Click">
                                    </asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <!--Section Body -->
                        <table id="Table9" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <uc1:OrderSupplyForm ID="SupplyForm" runat="server"></uc1:OrderSupplyForm>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <br>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trOrderSupply" runat="server">
        <td align="center">
            <table id="Tablessds1e" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <table id="Table62" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr align="left">
                                <td class="SectionPageTitleInfo">
                                    <asp:Label ID="Label64" runat="server">
											Supply Order Detail
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:OrderSupplyListInfo ID="OrderSupplyListInfoFinal" runat="server"></uc1:OrderSupplyListInfo>
                                </td>
                            </tr>
                            <tr>
                                <td height="5px">
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <table id="Table43dd" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top">
                                                <table id="Table4dd" cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td style="height: 19px">
                                                        </td>
                                                        <td style="height: 19px">
                                                            <asp:Label ID="Label65" runat="server" CssClass="StandardLabel">
																	Ship&nbsp;To&nbsp;:&nbsp;
                                                            </asp:Label>
                                                        </td>
                                                        <td style="height: 19px">
                                                        </td>
                                                        <td width="100%" style="height: 19px">
                                                            <asp:Label ID="lblShipTo" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label66" runat="server" CssClass="StandardLabel">
																	Supply&nbsp;Ship&nbsp;Date&nbsp;:&nbsp;
                                                            </asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td width="100%">
                                                            <asp:Label ID="lblShipSupplyDeliveryDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label67" runat="server" CssClass="StandardLabel" Width="160px">
																	Requested&nbsp;Lead-Time:&nbsp;
                                                            </asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td width="100%">
                                                            <asp:Label ID="lblShipSupplyNbDayLeadTime" runat="server" CssClass="DescInfoLabel">0</asp:Label>
                                                            <asp:Label ID="Label68" runat="server" CssClass="StandardLabel">&nbsp;Business Day(s)
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td valign="top">
                                                <table id="tblAddressSupply" cellspacing="0" cellpadding="0" border="0" runat="server">
                                                    <tr class="HeaderItemStyle">
                                                        <td>
                                                            <asp:Label ID="Label69" runat="server">
																	&nbsp;Supply Shipping Address
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <uc1:AddressControlInfo ID="AddressControlInfo_Supply" LabelOrgNameText="Account&nbsp;Name"
                                                                runat="server"></uc1:AddressControlInfo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
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
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;
        </td>
    </tr>
    <tr id="trOrderException" runat="server">
        <td align="center">
            <table id="tblOrderException" cellspacing="0" cellpadding="0" border="0" width="700"
                runat="server">
                <tr>
                    <td align="left">
                        <asp:ValidationSummary ID="ValSumExceptionInfo" runat="server" CssClass="LabelError"
                            HeaderText="Correct the following error to proceed."></asp:ValidationSummary>
                    </td>
                </tr>
                <tr align="left">
                    <td class="SectionPageTitleInfo" style="height: 20px">
                        <asp:Label ID="lblOrderExceptionTitle" runat="server">
								Important Information
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:EntityExceptionList ID="OrderExceptionList" IsReadOnly="True" runat="server">
                        </uc1:EntityExceptionList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
            <br>
        </td>
    </tr>
    <tr id="trChargeList" runat="server">
        <td align="Center">
            <table cellspacing="0" cellpadding="0" border="0" width="700">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr align="left">
                    <td class="SectionPageTitleInfo" style="height: 20px">
                        Surcharges generated for this order
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:ChargeList ID="ChargeList1" IsReadOnly="True" runat="server"></uc1:ChargeList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;
        </td>
    </tr>
    <tr id="trOrderSummary" runat="server">
        <td align="Center">
            <table id="OrderSummary" cellspacing="0" cellpadding="0" border="0" width="600">
                <tr align="left">
                    <td class="SectionPageTitleInfo" colspan="3">
                        <asp:Label ID="Label58" runat="server">
				Order Summary
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label52" runat="server" CssClass="StandardLabel">
							Sub Total :
                        </asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Label ID="lblSubTotal" runat="server" CssClass="DescInfoLabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label53" runat="server" CssClass="StandardLabel">
							Tax Rate :
                        </asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Label ID="lblTaxRate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label54" runat="server" CssClass="StandardLabel">
							Tax Amount :
                        </asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Label ID="lblTaxAmount" runat="server" CssClass="DescInfoLabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label55" runat="server" CssClass="StandardLabel">
							Shipping Charges :
                        </asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Label ID="lblShippingCharges" runat="server" CssClass="DescInfoLabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label13" runat="server" CssClass="StandardLabel">
							Surcharges :
                        </asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Label ID="lblSurcharges" runat="server" CssClass="DescInfoLabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 11px">
                        <hr width="100%" size="2">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label56" runat="server" CssClass="StandardLabel">
							Grand Total :
                        </asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Label ID="lblGrandTotal" runat="server" CssClass="DescInfoLabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="left">
                        <asp:Label ID="Label57" runat="server" CssClass="StandardLabel" Font-Size="xx-small">
								Invoices will include applicable taxes unless the Organization is exempt.  Tax exempt forms or resale certificates are required with order.  Most forms are available on state websites.  Fax forms to QSP Field Support to avoid taxes on invoices.
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;
        </td>
    </tr>
    <tr id="trAudit" runat="server">
        <td align="center">
            <table id="tblFormDetailTitle" cellspacing="3" cellpadding="0" width="650" border="0">
                <tr align="left">
                    <td class="SectionPageTitleInfo" colspan="2">
                        <asp:Label ID="Label49" runat="server">
				Audit Information
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0" width="325">
                            <tr>
                                <td>
                                    <asp:Label ID="lblLabelCreateName" CssClass="StandardLabel" runat="server">
						    Created&nbsp;By:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCreateName" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    <asp:Label ID="Label50" CssClass="StandardLabel" runat="server">Created&nbsp;At:&nbsp;</asp:Label>
                                </td>
                                <td style="height: 19px">
                                    <asp:Label ID="lblCreateDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0" width="325">
                            <tr>
                                <td>
                                    <asp:Label ID="lblLabelUpdateName" CssClass="StandardLabel" runat="server">
						    Updated&nbsp;By:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblUpdateName" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label51" CssClass="StandardLabel" runat="server">Updated&nbsp;At:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblUpdateDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        &nbsp;<asp:ImageButton ID="imgBtnViewHistory" runat="server" ImageUrl="~/images/btnViewHistory.gif" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;
        </td>
    </tr>
    <tr id="trExceptionButton" runat="server">
        <td align="center">
            <br>
            <table id="tblExceptionButton" runat="server" visible="false" cellspacing="0" cellpadding="0"
                border="0">
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="chkBoxShowOnlyException" Checked="False" runat="server" Text="Show Only Information on Notice and Exception"
                            CssClass="StandardLabel" AutoPostBack="True"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblShowOnlyExceptionNote" runat="server" CssClass="StandardLabel">Uncheck this check box, if you want to review the order.</asp:Label>
                    </td>
                </tr>
            </table>
            <br>
        </td>
    </tr>
    <tr>
        <td>
            <hr width="100%" size="2">
        </td>
    </tr>
    <tr id="trReadOnlyMode" runat="Server">
        <td align="center">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center">
                        <asp:ImageButton ID="imgEditOrder" ImageUrl="~/images/BtnEditOrder.gif" runat="server"
                            OnClick="imgEditOrder_Click" />
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="imgEditOrderPE" ImageUrl="~/images/btnEditPersonalizationOnly.gif"
                            runat="server" OnClick="imgEditOrderPE_Click" />
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td align="center">
                        <rs:RSGenerationImageButton ID="PrintFormReport" runat="server" ImageUrl="../images/btnprintorder.gif"
                            OnClick="PrintFormReport_Click"></rs:RSGenerationImageButton>
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td align="center">
                        <asp:HyperLink ID="hypLnkClose" runat="server" ImageUrl="~/images/btnClose.gif" NavigateUrl="javascript:window.close();">Close</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trButtonConfirm" runat="server" visible="false">
        <td align="center">
            <table id="Table2sss" cellspacing="0" cellpadding="0" width="400" border="0">
                <tr>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgBtnBack" runat="server" CausesValidation="False" AlternateText="Save"
                            ImageUrl="~/images/BtnBack.gif" OnClick="imgBtnBack_Click"></asp:ImageButton>
                    </td>
                    <td align="center">
                        <table id="Table2ssss" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="imgBtnProceed" runat="server" CausesValidation="False" ImageUrl="~/images/btnSubmitOrder.gif"
                                        AlternateText="Click here to confirm your order" ToolTip="Click here to submit and process your order"
                                        OnClick="imgBtnProceed_Click"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <br>
                                    <asp:ImageButton ID="imgBtnSaveForLater" runat="server" ImageUrl="~/images/btnSaveOrder.gif"
                                        AlternateText="Save" CausesValidation="False" OnClick="imgBtnSaveForLater_Click">
                                    </asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="center" valign="top">
                        <asp:HyperLink ID="hypLnkCancel1" runat="server" ImageUrl="~/images/btnClose.gif"
                            NavigateUrl="javascript:window.close();">Close and do not save.</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trButtonValidate" runat="server" visible="false">
        <td align="center">
            <table id="Table11" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr valign="middle">
                    <td align="center">
                        <asp:ImageButton ID="imgBtnCancelOrder" runat="server" CausesValidation="False" AlternateText="Cancel the Order"
                            ImageUrl="~/images/btnCancelOrder.gif" OnClick="imgBtnCancelOrder_Click"></asp:ImageButton>
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="imgBtnRollBack" runat="server" CausesValidation="False" AlternateText="Rollback Changes"
                            ImageUrl="~/images/btnRollBack.gif" OnClick="imgBtnRollBack_Click"></asp:ImageButton>
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="imgBtnValidate" runat="server" CausesValidation="False" AlternateText="Confirm Change"
                            ImageUrl="~/images/btnConfirmOrder.gif" Style="margin-top: 3px;" OnClick="imgBtnValidate_Click">
                        </asp:ImageButton>
                    </td>
                    <td align="center">
                        <asp:HyperLink ID="hypLnkCancel" runat="server" ImageUrl="~/images/btnCloseWithoutChange.gif"
                            NavigateUrl="javascript:window.close();">Close and do not save</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <hr width="100%" size="2">
        </td>
    </tr>
</table>
<input type="hidden" id="hidEditPersonalization" runat="server" value="0" />

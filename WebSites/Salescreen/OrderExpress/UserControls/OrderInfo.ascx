<%@ Reference Control="WarehouseDetailInfo.ascx" %>
<%@ Reference Control="AccountDetailInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderSupplyListInfo" Src="OrderSupplyListInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressControlInfo" Src="AddressControlInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShippingChargesCustomer" Src="ShippingChargesCustomer.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderInfo"
    CodeBehind="OrderInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="OrderSummaryInfo" Src="OrderSummaryInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderDetailSectionListInfo" Src="OrderDetailSectionListInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EntityExceptionList" Src="EntityExceptionList.ascx" %>
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr id="trAccountInfo" runat="server">
        <td align="center">
            <!--Section Title -->
            <table id="Table1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="lblTitleAccountInfo" runat="server">
							Account Information
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <!--Section Body -->
                        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label31" runat="server" CssClass="StandardLabel">
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
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
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
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" CssClass="StandardLabel">
										Account&nbsp;Name:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table id="Table8" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAccountName" runat="server" CssClass="DescInfoLabel"></asp:Label>
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
                            <tr id="trAccountStatus" runat="server" visible="false">
                                <td>
                                </td>
                                <td valign="top">
                                    <asp:Label ID="Label17" runat="server" CssClass="StandardLabel">
										Account&nbsp;Status:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" id="Table12">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAccountStatusColor" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="1px" Height="3px" BackColor="Orange" Width="5px">
													&nbsp;&nbsp;
                                                </asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAccountStatus_ShortDescription" runat="server" CssClass="DescInfoLabel">New Account</asp:Label>
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
                                </td>
                                <td>
                                    <asp:Label ID="Label42" runat="server" CssClass="StandardLabel">
										FSM&nbsp;Info:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblAccountFMInfo" runat="server" CssClass="DescInfoLabel">0000 - John Smith</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label19" CssClass="StandardLabel" runat="server">
										Organization&nbsp;Type:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" id="Table13">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblOrgType" runat="server" CssClass="DescInfoLabel">Public</asp:Label>
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
                                                <asp:Label ID="lblOrgLevel" runat="server" CssClass="DescInfoLabel">High School</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td valign="top">
                                    <asp:Label ID="Label44" runat="server" CssClass="StandardLabel">
										Trade&nbsp;Class:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td valign="top" width="100%">
                                    <asp:Label ID="lblTradeClass" runat="server" CssClass="DescInfoLabel">None</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" CssClass="StandardLabel">
										QSP&nbsp;Program:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="lblProgramTypeName" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label39" CssClass="StandardLabel" runat="server">
										Last&nbsp;Fiscal&nbsp;Year:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%" colspan="4">
                                    <table cellspacing="0" cellpadding="0" width="300" border="0" id="Table14">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblLastFiscalYear" CssClass="DescInfoLabel" Width="200px" runat="server"></asp:Label>
                                            </td>
                                            <td width="100%">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label40" CssClass="StandardLabel" runat="server">Last&nbsp;Order&nbsp;Date:&nbsp;</asp:Label>
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
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label18" runat="server" CssClass="StandardLabel">
										Tax&nbsp;Exemption:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td colspan="4">
                                    <table id="Table10" cellspacing="0" cellpadding="0" width="400" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTaxExemptionNumber" runat="server" CssClass="DescInfoLabel" Width="200px"></asp:Label>
                                            </td>
                                            <td width="100%">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label27" runat="server" CssClass="StandardLabel">Expire&nbsp;:&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTaxExemptionExpirationDate" runat="server" CssClass="DescInfoLabel"
                                                    Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label37" runat="server" CssClass="StandardLabel">
										Comment:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblAccountComment" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br>
                        <table id="Table244" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td align="center" colspan="5">
                                    <table cellspacing="0" cellpadding="0" border="0" id="Table15">
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="Label21" runat="server" CssClass="NoteLabel">
													Note:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="NoteLabel">
													Exemption or resale certificate forms required with order.  Based on state laws, Invoice will include taxes unless account is tax exempt.
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
            </table>
        </td>
    </tr>
    <tr id="trAccountException" runat="server">
        <td align="center">
            <table id="tblAccountException" runat="server" border="0" cellpadding="0" cellspacing="0"
                width="700">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="Label41" runat="server">
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
    <tr id="trOrderAddress" runat="server">
        <td align="center">
            <table id="Table3" cellspacing="0" cellpadding="0" border="0">
                <tr class="HeaderItemStyle">
                    <td width="350" align="left">
                        <asp:Label ID="Label8" runat="server">
								&nbsp;Bill To
                        </asp:Label>
                    </td>
                    <td width="350" align="left">
                        <asp:Label ID="Label7" runat="server">
								&nbsp;Ship To
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:AddressControlInfo ID="AddressControlInfo_Billing" LabelOrgNameText="Account&nbsp;Name"
                            runat="server"></uc1:AddressControlInfo>
                    </td>
                    <td>
                        <uc1:AddressControlInfo ID="AddressControlInfo_Shipping" LabelOrgNameText="Account&nbsp;Name"
                            runat="server"></uc1:AddressControlInfo>
                    </td>
                </tr>
            </table>
            <br>
        </td>
    </tr>
    <tr id="trOrderInfo" runat="server">
        <td align="center">
            <table id="Tabless1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="Label3" runat="server">
								Order Information
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <!--Section Body -->
                        <table id="Table9" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="Label28" runat="server" CssClass="StandardLabel">
											QSP Order ID:
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
                                        <asp:Label ID="Label22" runat="server" CssClass="StandardLabel">
											EDS&nbsp;Order&nbsp;#:&nbsp;
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
                                        <asp:Label ID="Label30" runat="server" CssClass="StandardLabel">
											Order Status:
                                        </asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td width="50%">
                                        <table border="0" cellpadding="0" cellspacing="0" id="Table16">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblOrderStatusColor" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                        BorderWidth="1px" BackColor="Orange" CssClass="StatusLabel">
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
                                        <asp:Label ID="Label43" runat="server" CssClass="StandardLabel">
											FSM&nbsp;Info:&nbsp;
                                        </asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td width="100%">
                                        <asp:Label ID="lblFMInfo" runat="server" CssClass="DescInfoLabel">0000 - John Smith</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="Label32" runat="server" CssClass="StandardLabel">
											Order Date:
                                        </asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOrderDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="Label9" runat="server" CssClass="StandardLabel">
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
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="Label10" runat="server" CssClass="StandardLabel">
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
                                <tr id="trWarehouseInfo" runat="server">
                                    <td>
                                        <asp:Label ID="Label33" runat="server" CssClass="StandardLabel">
											Warehouse&nbsp;Name:&nbsp;
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
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLabelDeliveryDate" runat="server" CssClass="StandardLabel"> Delivery Date:</asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDeliveryDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trRequestedDeliveryTime" runat="server">
                                    <td>
                                        <asp:Label ID="lblLabelDeliveryTime" runat="server" CssClass="StandardLabel"> Delivery Time:</asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDeliveryTime" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Width="150px" CssClass="StandardLabel"> Requested&nbsp;Lead-Time:&nbsp;
                                        </asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lblDayLeadTime" runat="server" CssClass="DescInfoLabel"> 0
                                        </asp:Label>
                                        <asp:Label ID="Label45" runat="server" CssClass="StandardLabel">&nbsp;Business Day(s)
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLabelShippingDate" runat="server" CssClass="StandardLabel"> Shipping Date:
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
                                <tr>
                                    <td>
                                        <asp:Label ID="Label38" runat="server" CssClass="StandardLabel">
											Comment:&nbsp;
                                        </asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td width="100%">
                                        <asp:Label ID="lblComment" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
            <br>
        </td>
    </tr>
    <tr id="trOrderTerms" runat="server">
        <td align="center">
            <table id="Tablesds1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="Label12" runat="server">
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
                                    <asp:Label ID="Label14" runat="server" CssClass="StandardLabel" Font-Size="xx-small">
											You are in agreement that QSP will be working with your organization in connection with a fundraising program as follows:
                                    </asp:Label><br>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label13" runat="server" CssClass="StandardLabel">Start&nbsp;Date&nbsp;:&nbsp;</asp:Label>
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
                                    <asp:Label ID="Label16" runat="server" CssClass="StandardLabel" Width="200px">GOAL-&nbsp;Estimated&nbsp;Gross&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEstimatedAmount" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="Table7" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label23" runat="server" CssClass="StandardLabel" Width="200px">Enrollment&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEnrollment" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trProfitRate" runat="server">
                                <td>
                                    <asp:Label ID="Label47" runat="server" CssClass="StandardLabel" Width="200px">Account&nbsp;Profit&nbsp;:</asp:Label>
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
    <tr id="trOrderDetail" runat="server">
        <td align="center">
            <table id="Tablesfds1e" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="Label4" runat="server">
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
    <tr id="trOrderDetail_Optional" runat="server">
        <td align="center">
            <table id="Table4" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="Label46" runat="server">
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
    <tr id="trOrderSupply" runat="server">
        <td align="center">
            <table id="Tablessds1e" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <table id="Table62" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="SectionPageTitleInfo">
                                    <asp:Label ID="Label24" runat="server">
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
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label26" runat="server" CssClass="StandardLabel">
																	Ship&nbsp;To&nbsp;:&nbsp;
                                                            </asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td width="100%">
                                                            <asp:Label ID="lblShipTo" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label34" runat="server" CssClass="StandardLabel">
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
                                                            <asp:Label ID="Label35" runat="server" CssClass="StandardLabel" Width="160px">
																	Requested&nbsp;Lead-Time:&nbsp;
                                                            </asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td width="100%">
                                                            <asp:Label ID="lblShipSupplyNbDayLeadTime" runat="server" CssClass="DescInfoLabel">0</asp:Label>
                                                            <asp:Label ID="Label36" runat="server" CssClass="StandardLabel">&nbsp;Business Day(s)
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
                                                            <asp:Label ID="Label15" runat="server">
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
    <tr id="trOrderException" runat="server">
        <td align="center">
            <table id="tblOrderException" cellspacing="0" cellpadding="0" border="0" width="700"
                runat="server">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="ValSumExceptionInfo" runat="server" CssClass="LabelError"
                            HeaderText="Correct the following error to proceed."></asp:ValidationSummary>
                    </td>
                </tr>
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
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
    <tr id="trOrderSummary" runat="server">
        <td align="center">
            <table cellspacing="0" cellpadding="0" width="600" border="0" id="Table17">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="Label25" runat="server">
								Order Summary
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:OrderSummaryInfo ID="OrderSummary" runat="server"></uc1:OrderSummaryInfo>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

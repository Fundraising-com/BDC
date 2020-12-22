<%@ Reference Control="OrderList.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderStep_Confirmation"
    CodeBehind="OrderStep_Confirmation.ascx.cs" %>
<%@ Register Namespace="QSP.WebControl.Reporting" TagPrefix="rs" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="ChargeList" Src="~/UserControls/OrderChargeList.ascx" %>
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
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr id="trCampInfoTitle" runat="server">
        <td align="left">
            <!--Section Body -->
            <br>
            <table id="tblCampInfoTitle" cellspacing="0" cellpadding="0" border="0">
                <tr id="trQSPID" runat="server">
                    <td>
                        <asp:Label ID="Label4" runat="server" CssClass="StandardLabel">
								QSP&nbsp;Account&nbsp;ID&nbsp;#&nbsp;:&nbsp;
                        </asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblAccountID" runat="server" CssClass="StandardLabel" ForeColor="#993300">
								12123234
                        </asp:Label>
                    </td>
                </tr>
                <tr id="trAccountInfoTitle" runat="server">
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="StandardLabel">
								Account&nbsp;:&nbsp;
                        </asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblAccountName" runat="server" CssClass="StandardLabel" ForeColor="#993300">
								Las Vegas Elementary School
                        </asp:Label>
                    </td>
                </tr>
                <tr id="trFormInfoTitle" runat="server">
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="StandardLabel"> Order Form :
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFormID" runat="server" CssClass="StandardLabel" ForeColor="#993300">
							0
                        </asp:Label>
                    </td>
                    <td>
                        &nbsp;-&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblFormName" runat="server" CssClass="StandardLabel" ForeColor="#993300">
							WFC WarehouseStock Order Form
                        </asp:Label>
                    </td>
                </tr>
            </table>
            <br>
        </td>
    </tr>
    <tr>
        <td>
            <table id="Table1222" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td align="left">
                        <!--Section Body -->
                        <table id="Table5" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" CssClass="StandardLabel" runat="server" Visible="False"> 8 - Confirmation of the order :
                                    </asp:Label>
                                    <br>
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
                    </td>
                </tr>
                <tr id="trConfirmation" runat="server">
                    <td>
                        <br>
                        <table id="Table6" cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <table id="Table12" cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="Label28" runat="server" CssClass="StandardLabel">
																	QSP&nbsp;Order&nbsp;#:&nbsp;
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
                                                        <td valign="top">
                                                            <asp:Label ID="Label30" runat="server" CssClass="StandardLabel">
																Order Status :
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
                                                                            BorderWidth="1px" Height="3px" BackColor="Orange" Width="5px">
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
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Label ID="lblMessageConfirmation" CssClass="StandardLabel" runat="server" Visible="False">The order have been saved successfully with the status submitted.<br></asp:Label>
                                                <br>
                                                <br>
                                            </td>
                                        </tr>
                                        <tr id="trChargeList" runat="server">
                                            <td>
                                                <asp:Label ID="Label5" CssClass="StandardLabel" runat="server" Visible="True">The following surcharges were generated for the order</asp:Label>
                                                <br>
                                                <br>
                                                <uc1:ChargeList ID="ChargeList1" IsReadOnly="True" runat="server"></uc1:ChargeList>
                                                <br>
                                                <br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMessageNotification" CssClass="DescLabel" runat="server">
													An order acknowledgement will be e-mailed to you today. To check the status of this order, go to Order [Menu Bar] and click on Order List<br>													
                                                </asp:Label>
                                                <br>
                                                <br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCreditAppMsg" CssClass="DescLabel" runat="server">
													Please Note:  <b>A Credit Application is required for New Accounts.</b>  Exceptions include Public and Catholic Schools and/or Accounts with NO Sales within the last 24 months.
													Therefore, an order for New Accounts cannot be processed without the receipt of a <u>valid</u> Credit Application form <u>and</u> QSP's approval.												
													<br><br>													
													To complete the form now, click 'Credit Application Form' Button.  The form is also available on the Account Detail page, by selecting the Account in the Account List [Menu Bar], if you prefer to complete it later.
                                                </asp:Label>
                                                <br>
                                                <br>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" cellpadding="0" border="0" width="500">
                                        <tr>
                                            <td align="center">
                                                <asp:ImageButton ID="imgBtnCreditApplication" runat="server" CausesValidation="False"
                                                    ImageUrl="~/images/btnCreditApplication.gif" AlternateText="Click here to fill the Credit Application Form"
                                                    ToolTip="Click here to fill the Credit Application Form"></asp:ImageButton>
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
                    <td align="center">
                        <br>
                        <asp:HyperLink ID="hlOrderPrint" runat="server" CssClass="LinkButton" Width="100px"
                            Target="_blank">Print order</asp:HyperLink>
                        &nbsp;
                        <asp:HyperLink ID="hlOrderSearch" runat="server" CssClass="LinkButton" Width="100px">Order search</asp:HyperLink>
                        &nbsp;
                        <asp:HyperLink ID="hlOrderCreate" runat="server" CssClass="LinkButton" Width="100px">Create order</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

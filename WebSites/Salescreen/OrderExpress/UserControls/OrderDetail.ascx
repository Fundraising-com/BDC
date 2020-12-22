<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Reference Control="OrderDetailInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderDetailSectionList" Src="OrderDetailSectionList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressControlForm" Src="AddressControlForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderSupplyForm" Src="OrderSupplyForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderSummaryInfo" Src="OrderSummaryInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderInfo" Src="OrderInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderDetail"
    CodeBehind="OrderDetail.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="EntityExceptionList" Src="EntityExceptionList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderHeaderDetailForm" Src="OrderHeaderDetailForm.ascx" %>
<link href="Styles.css" rel="stylesheet" type="text/css" />
<table id="TableMain" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr id="trOrderDetail" runat="server">
        <td>
            <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
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
                                            <td>
                                                <asp:Label ID="Label2" runat="server" CssClass="FormTitleLabel"> Account :
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAccountNumber" runat="server" CssClass="FormTitleDescLabel">
										            00000
                                                </asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;-&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAccountName" runat="server" CssClass="FormTitleDescLabel">
										            Account Name
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trFormInfoTitle" runat="server">
                                            <td style="height: 19px">
                                                <asp:Label ID="Label3" runat="server" CssClass="FormTitleLabel"> Order Form :
                                                </asp:Label>
                                            </td>
                                            <td align="right" style="height: 19px">
                                                <asp:Label ID="lblFormID" runat="server" CssClass="FormTitleDescLabel">
										            23
                                                </asp:Label>
                                            </td>
                                            <td style="height: 19px">
                                                &nbsp;-&nbsp;
                                            </td>
                                            <td style="height: 19px">
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
                <tr>
                    <td>
                        <uc1:OrderHeaderDetailForm ID="HeaderDetailForm" runat="server"></uc1:OrderHeaderDetailForm>
                    </td>
                </tr>
                <tr>
                    <td class="SectionPageTitleInfo">
                        <asp:Label ID="Label4" runat="server">
							Order Detail
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td style="background-color: transparent">
                                    <iewc:TabStrip ID="TbStrp_Form" runat="server" AutoPostBack="False" SepDefaultStyle="border-bottom:solid 1px #000000; background: transparent;"
                                        TargetID="multPage_Form" TabDefaultStyle="border-bottom: #006699 2px solid; background-color: transparent;"
                                        BackColor="LightGoldenrodYellow">
                                        <iewc:Tab DefaultImageUrl="../images/tabForm_StandardProduct_off.gif" SelectedImageUrl="../images/tabForm_StandardProduct_on.gif">
                                        </iewc:Tab>
                                        <iewc:TabSeparator></iewc:TabSeparator>
                                        <iewc:Tab DefaultImageUrl="../images/tabForm_StockProduct_off.gif" SelectedImageUrl="../images/tabForm_StockProduct_on.gif">
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
                                </td>
                            </tr>
                        </table>
                        <br>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgBtnShowSupply" runat="server" ImageUrl="~/images/btnShowSupply.gif"
                            AlternateText="Show Supply Section" CausesValidation="False"></asp:ImageButton>
                    </td>
                </tr>
                <tr id="trOrderSupply" runat="server">
                    <td>
                        <br>
                        <table id="Table62" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="SectionPageTitleInfo">
                                    <table id="Table621" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="SectionPageTitleInfo">
                                                <asp:Label ID="Label1" runat="server">
													Supply Order Detail
                                                </asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="imgBtnHideSupply" runat="server" ImageUrl="~/images/btnHideSupply.gif"
                                                    AlternateText="Hide Supply Section" CausesValidation="False"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <!--Section Body -->
                                    <table id="Table4" cellspacing="0" cellpadding="0" border="0">
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
            </table>
        </td>
    </tr>
    <tr id="trOrderInfo" runat="server">
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td align="left">
                        <uc1:OrderInfo ID="OrderInfo1" runat="server"></uc1:OrderInfo>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <br>
                        <table id="tblExceptionButton" runat="server" visible="false" cellspacing="0" cellpadding="0"
                            border="0">
                            <tr>
                                <td align="center">
                                    <asp:CheckBox ID="chkBoxShowOnlyException" Checked="False" runat="server" Text="Show Only Information on Notice and Exception"
                                        CssClass="StandardLabel" AutoPostBack="True" OnCheckedChanged="chkBoxShowOnlyException_CheckedChanged">
                                    </asp:CheckBox>
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
            </table>
            <br>
        </td>
    </tr>
    <tr id="trButtonValidate" runat="server">
        <td align="center">
            <br>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td align="center" style="width: 601px">
                        <table id="Table2" cellspacing="0" cellpadding="0" width="600" border="0">
                            <tr valign="middle">
                                <td align="center" style="height: 27px">
                                    <asp:ImageButton ID="imgBtnCancelOrder" runat="server" CausesValidation="False" AlternateText="Cancel the Order"
                                        ImageUrl="~/images/btnCancelOrder.gif"></asp:ImageButton>
                                </td>
                                <td align="center" style="height: 27px">
                                    <asp:ImageButton ID="imgBtnRollBack" runat="server" CausesValidation="False" AlternateText="Rollback Changes"
                                        ImageUrl="~/images/btnRollback.gif"></asp:ImageButton>
                                </td>
                                <td align="center" style="height: 27px">
                                    <asp:ImageButton ID="imgBtnValidate" runat="server" CausesValidation="False" AlternateText="Confirm Change"
                                        ImageUrl="~/images/btnConfirmOrder.gif" Style="margin-top: 3px;"></asp:ImageButton>
                                </td>
                                <td align="center" style="height: 27px">
                                    <asp:HyperLink ID="hypLnkCancel" runat="server" ImageUrl="~/images/btnCloseWithoutChange.gif"
                                        NavigateUrl="javascript:window.close();">Close and do not save</asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trButtonConfirm" runat="server">
        <td align="center" style="height: 116px">
            <br>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td align="center">
                        <table id="Table2sss" cellspacing="0" cellpadding="0" width="400" border="0">
                            <tr>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgBtnBack" runat="server" CausesValidation="False" AlternateText="Save"
                                        ImageUrl="~/images/btnBack.gif"></asp:ImageButton>
                                </td>
                                <td align="center">
                                    <table id="Table2ssss" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td align="center">
                                                <asp:ImageButton ID="imgBtnProceed" runat="server" CausesValidation="False" ImageUrl="~/images/btnSubmitOrder.gif"
                                                    AlternateText="Click here to confirm your order" ToolTip="Click here to submit and process your order">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <br>
                                                <asp:ImageButton ID="imgBtnSaveForLater" runat="server" ImageUrl="~/images/btnSaveOrder.gif"
                                                    AlternateText="Save" CausesValidation="False"></asp:ImageButton>
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
            </table>
        </td>
    </tr>
</table>
<input type="hidden" id="hidEditPersonalization" runat="server" value="0" />

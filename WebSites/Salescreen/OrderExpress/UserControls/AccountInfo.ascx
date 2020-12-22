<%@ Reference Control="CreditApplicationDetailInfo.ascx" %>
<%@ Reference Control="WarehouseDetailInfo.ascx" %>
<%@ Reference Control="OrganizationDetailInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressControlInfo" Src="AddressControlInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountInfo"
    CodeBehind="AccountInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="EntityExceptionList" Src="EntityExceptionList.ascx" %>
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr>
        <td align="left">
            <!--Section Title -->
        </td>
    </tr>
    <tr id="trAccountInfo" runat="server">
        <td align="center">
            <table id="Table1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="Label5" runat="server">
							Account Information
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="5px">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <!--Section Body -->
                        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" CssClass="StandardLabel" runat="server">
										QSP&nbsp;Account&nbsp;ID:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblAccID" CssClass="DescInfoLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" CssClass="StandardLabel" runat="server">
										EDS&nbsp;Account&nbsp;#&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblEDSAccID" CssClass="DescInfoLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label12" CssClass="StandardLabel" runat="server">
										Organization&nbsp;Name&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table cellspacing="0" cellpadding="0" border="0" id="Table8">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblOrganizationName" CssClass="DescInfoLabel" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnDetailOrg" runat="server" Height="15px" ImageUrl="~/images/BtnDetail.gif"
                                                    CausesValidation="False"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" CssClass="StandardLabel" runat="server">
										Account&nbsp;Name&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblAccountName" CssClass="DescInfoLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trAccountStatus" runat="server">
                                <td valign="top">
                                    <asp:Label ID="Label17" CssClass="StandardLabel" runat="server">
										Account&nbsp;Status&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAccountStatusColor" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="1px" BackColor="Orange" CssClass="StatusLabel">
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
                                    <asp:Label ID="Label11" CssClass="StandardLabel" runat="server">
										Field&nbsp;Sales&nbsp;Manager&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblFMInfo" CssClass="DescInfoLabel" runat="server">0000 - John Smith</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label19" CssClass="StandardLabel" runat="server">
										Organization&nbsp;Type&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblOrgType" runat="server" CssClass="DescInfoLabel">Public</asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label28" runat="server" CssClass="StandardLabel">
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
                                <td valign="top">
                                    <asp:Label ID="Label26" CssClass="StandardLabel" runat="server">
										Trade&nbsp;Class&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td valign="top" width="100%">
                                    <asp:Label ID="lblTradeClass" CssClass="DescInfoLabel" runat="server">AYSO</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label24" CssClass="StandardLabel" runat="server">
										Last&nbsp;Fiscal&nbsp;Year:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <table cellspacing="0" cellpadding="0" width="300" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblLastFiscalYear" CssClass="DescInfoLabel" Width="200px" runat="server"></asp:Label>
                                            </td>
                                            <td width="100%">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label27" CssClass="StandardLabel" runat="server">Last&nbsp;Order&nbsp;Date:&nbsp;</asp:Label>
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
                            <tr>
                                <td>
                                    <asp:Label ID="Label25" CssClass="StandardLabel" runat="server">
										#&nbsp;Inactive&nbsp;Months:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="lblNbOfInactiveMonth" CssClass="DescInfoLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" CssClass="StandardLabel" runat="server">
										QSP&nbsp;Program&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="lblProgramTypeName" CssClass="DescInfoLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trDefaultWarehouse" runat="server">
                                <td>
                                    <asp:Label ID="Label34" runat="server" CssClass="StandardLabel">
										Default Warehouse:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0" id="Table666">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDefaultWarehouse" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnWarehouse" runat="server" Height="15px" ImageUrl="~/images/BtnDetail.gif"
                                                    CausesValidation="False"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trTaxExemption" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="Label6" CssClass="StandardLabel" runat="server">
										Tax&nbsp;Exemption&nbsp;:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="300" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTaxExemptionNumber" CssClass="DescInfoLabel" runat="server" Width="200px"></asp:Label>
                                            </td>
                                            <td width="100%">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" CssClass="StandardLabel" runat="server">Expire&nbsp;:&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTaxExemptionExpirationDate" CssClass="DescInfoLabel" runat="server"
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
                                    <asp:Label ID="Label37" runat="server" CssClass="StandardLabel">
										Collection&nbsp;Date:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <table id="tblCollectionInfoReadOnly" cellspacing="0" cellpadding="0" width="400"
                                        border="0" runat="server">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAccountCollectionDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                            <td width="100">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td width="1">
                                                <asp:Label ID="Label40" runat="server" CssClass="StandardLabel">Total&nbsp;Amount:&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAccountCollectionAmount" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label13" CssClass="StandardLabel" runat="server">
										Comment:&nbsp;
                                    </asp:Label>
                                </td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblComment" CssClass="DescInfoLabel" runat="server"></asp:Label>
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
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="Label21" CssClass="NoteLabel" runat="server">
													Note:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" CssClass="NoteLabel" runat="server">
													Tax exempt forms or resale certificates are 
													required with order. Based on state laws, invoices  will include applicable taxes unless forms are provided.
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btnCreditApplication" runat="server" ImageUrl="~/images/btnCreditApplication.gif"
                                                    CausesValidation="False" AlternateText="Click here to see Credit Application of the current Account">
                                                </asp:ImageButton>
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
                width="600">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="Label22" runat="server">
								Account Note
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:EntityExceptionList ID="ExceptionList" IsReadOnly="True" runat="server"></uc1:EntityExceptionList>
                    </td>
                </tr>
            </table>
            <br>
        </td>
    </tr>
    <tr id="trAccountAddress" runat="server">
        <td align="center">
            <table id="Table3" cellspacing="0" cellpadding="0" border="0">
                <tr class="HeaderItemStyle">
                    <td width="350" align="left">
                        <asp:Label ID="Label8" runat="server">
							&nbsp;Bill To
                        </asp:Label>
                    </td>
                    <td width="350" align="left">
                        <asp:Label ID="Label18" runat="server">
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
    <tr id="trOrderTermsInfo" runat="server">
        <td align="center">
            <table cellspacing="0" cellpadding="2" width="600" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" colspan="2" align="left">
                        <asp:Label ID="Label10" runat="server">
							Order&nbsp;Terms
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label14" CssClass="NoteLabel" runat="server">
							You are in agreement that QSP will be working with your organization in connection with a fundraising program as follows:
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="5px">
                    </td>
                </tr>
                <tr align="left">
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td width="100">
                                    <asp:Label ID="Label15" CssClass="StandardLabel" runat="server">Start&nbsp;Date&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblStartDate" CssClass="DescInfoLabel" runat="server" Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label29" CssClass="StandardLabel" runat="server">End&nbsp;Date&nbsp;:&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEndDate" CssClass="DescInfoLabel" runat="server" Width="100px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label16" CssClass="StandardLabel" runat="server">GOAL-&nbsp;Estimated&nbsp;Gross&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEstimatedAmount" CssClass="DescInfoLabel" runat="server" Width="100px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label23" runat="server" CssClass="StandardLabel">Enrollment&nbsp;:&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEnrollment" runat="server" Width="100px" CssClass="DescInfoLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

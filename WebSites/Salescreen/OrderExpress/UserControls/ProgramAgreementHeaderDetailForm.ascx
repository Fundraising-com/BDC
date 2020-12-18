<%@ Control 
    Language="c#" 
    AutoEventWireup="false"
    Codebehind="ProgramAgreementHeaderDetailForm.ascx.cs"
    Inherits="QSP.OrderExpress.Web.UserControls.ProgramAgreementHeaderDetailForm"
     %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="DualAddressForm" Src="DualAddressForm.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc2" %>
<br />
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr id="tblRowSectionAccount" runat="server">
        <td align="center">
            <table id="Table1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td class="SectionPageTitleInfo" align="left">
                        <asp:Label ID="lblTitleAccountInfo" runat="server">
							Account Information
                        </asp:Label></td>
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
                                    </asp:Label></td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblAccID" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" CssClass="StandardLabel">
											EDS&nbsp;Account&nbsp;#:&nbsp;
                                    </asp:Label></td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblEDSAccID" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" CssClass="StandardLabel">
										Account&nbsp;Name:&nbsp;
                                    </asp:Label></td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblAccountName" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                            </tr>
                            <tr id="trAccountStatus" runat="server" visible="False">
                                <td valign="top">
                                    <asp:Label ID="Label17" runat="server" CssClass="StandardLabel">
										Account&nbsp;Status:&nbsp;
                                    </asp:Label></td>
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
                                                </asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAccountStatus" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" CssClass="StandardLabel">
										FSM&nbsp;Info:&nbsp;
                                    </asp:Label></td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblAccountFMInfo" runat="server" CssClass="DescInfoLabel">
										0000 - John Smith
                                    </asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label19" runat="server" CssClass="StandardLabel">
										Organization&nbsp;Type&nbsp;:&nbsp;
                                    </asp:Label></td>
                                <td>
                                </td>
                                <td valign="top" width="100%">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblOrgType" runat="server" CssClass="DescInfoLabel">
													Public
                                                </asp:Label></td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" CssClass="StandardLabel">
													Organization&nbsp;Level:&nbsp;
                                                </asp:Label></td>
                                            <td>
                                                &nbsp;</td>
                                            <td width="100%">
                                                <asp:Label ID="lblOrgLevel" runat="server" CssClass="DescInfoLabel">
													High School
                                                </asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label30" runat="server" CssClass="StandardLabel">
										Trade&nbsp;Class:&nbsp;
                                    </asp:Label></td>
                                <td>
                                </td>
                                <td valign="top" width="100%">
                                    <asp:Label ID="lblTradeClass" runat="server" CssClass="DescInfoLabel">
										None
                                    </asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" CssClass="StandardLabel">
										QSP&nbsp;Program:&nbsp;
                                    </asp:Label></td>
                                <td>
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="lblProgramTypeName" runat="server" CssClass="DescInfoLabel">
										Chocolate</asp:Label></td>
                            </tr>
                            <tr id="trTaxExemption" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="Label6" runat="server" CssClass="StandardLabel">
											Tax&nbsp;Exemption:&nbsp;
                                    </asp:Label></td>
                                <td>
                                </td>
                                <td colspan="4">
                                    <table cellspacing="0" cellpadding="0" width="400" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTaxExemptionNumber" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                                            <td width="100">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td width="1">
                                                <asp:Label ID="Label7" runat="server" CssClass="StandardLabel">Expire:&nbsp;</asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblTaxExemptionExpirationDate" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label12" runat="server" CssClass="StandardLabel">
											Comment:&nbsp;
                                    </asp:Label></td>
                                <td>
                                </td>
                                <td width="100%" colspan="4">
                                    <asp:Label ID="lblAccountComment" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
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
                                    </asp:Label></td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" CssClass="NoteLabel">
									In some states, exemption or resale certificate forms are required to exempt an account from taxes.  The appropriate form must be submitted with the order; otherwise, invoices will include applicable taxes.
                                    </asp:Label></td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
			<uc1:DualAddressForm ID="DualAddressFormControl" runat="server" BillingEnabled="false" HygieneAddress="True" EntityName="Account&nbsp;Name" />
        </td>
    </tr>
    <tr>
        <td>
            <br />
            <br />
        </td>
    </tr>
    <tr id="tblRowSectionProgramAgreement" runat="server">
        <td align="center">
            <table id="Tabless1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td class="SectionPageTitleInfo">
                        <asp:Label ID="lblTitleProgramAgreementInfo" runat="server">
							Program Agreement Information
                        </asp:Label></td>
                </tr>
                <tr id="trValSumProgramAgreementInfo" runat="server" visible="false">
                    <td>
                        <asp:ValidationSummary ID="ValSumProgramAgreementInfo" runat="server" CssClass="LabelError"
                            HeaderText="Correct the following error to proceed."></asp:ValidationSummary>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <!--Section Body -->
                        <table id="tblProgramAgreementInfo" cellspacing="0" cellpadding="0" border="0" runat="server">
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label22" runat="server" CssClass="StandardLabel">
										Program Agreement&nbsp;ID:&nbsp;
                                    </asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblProgramAgreementID" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" CssClass="StandardLabel">
										EDS&nbsp;Program&nbsp;Agreement&nbsp;#&nbsp;:&nbsp;
                                    </asp:Label></td>
                                <td>
                                </td>
                                <td width="100%">
                                    <asp:Label ID="lblEDSProgramAgreementID" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label25" runat="server" CssClass="StandardLabel">
										Program&nbsp;Agreement&nbsp;Status:&nbsp;
                                    </asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td width="50%">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblProgramAgreementStatusColor" runat="server" BackColor="Orange"
                                                    BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" CssClass="StatusLabel">
													&nbsp;&nbsp;
                                                </asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblProgramAgreementStatus_ShortDescription" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblProgramAgreementStatus_Description" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label31" runat="server" CssClass="StandardLabel">
									Program&nbsp;Agreement&nbsp;Date:&nbsp;
                                    </asp:Label></td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblProgramAgreementDate" runat="server" CssClass="DescInfoLabel"></asp:Label></td>
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
                                    <asp:Label ID="lblBusinessMessage" runat="server" CssClass="BizRuleLabel"></asp:Label>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table id="Tablesds1e" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td align="center">
                        <table cellspacing="0" cellpadding="2" width="600" border="0">
                            <tr>
                                <td class="SectionPageTitleInfo" colspan="2">
                                    <asp:Label ID="lblTitleProgramTermInfo" runat="server">
							        Program&nbsp;Agreement&nbsp;Terms
                                    </asp:Label></td>
                            </tr>
                            <tr id="trValSumProgramTermInfo" runat="server" visible="false" align="left">
                                <td>
                                    <asp:ValidationSummary ID="ValSumOrderTermInfo" runat="server" HeaderText="Correct the following error to proceed."
                                        CssClass="LabelError"></asp:ValidationSummary>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label14" runat="server" CssClass="NoteLabel">
							        You are in agreement that QSP will be working with your organization in connection with a fundraising program as follows:
                                    </asp:Label>
                                    <br>
                                </td>
                            </tr>
                            <tr>
                                <td height="5px">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblProgramTermInfo" cellspacing="0" cellpadding="0" width="100%" border="0"
                                        runat="server">
                                        <tr>
                                            <td>
                                                <table width="600" cellspacing="0" cellpadding="1" border="0">
                                                    <tr>
                                                        <td><img src="../images/spacer.gif" width="145" height="1" /></td>
                                                        <td><img src="../images/spacer.gif" width="145" height="1" /></td>
                                                        <td><img src="../images/spacer.gif" width="145" height="1" /></td>
                                                        <td><img src="../images/spacer.gif" width="145" height="1" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="middle"><asp:Label ID="Label15" runat="server" CssClass="StandardLabel">Start&nbsp;Date:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:Label></td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtStartDate" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma"
                                                                Width="75px"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtStartDate"
                                                                Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" CultureName="en-US" Enabled="True" 
                                                                ErrorTooltipEnabled="True" />
                                                        
                                                            <asp:HyperLink ID="hypLnkStartDate" runat="server" ImageUrl="~/images/Calendar.gif"
                                                                NavigateUrl="javascript:void(0);" ToolTip="Click here to select the start date from a popup calendar !">HyperLink</asp:HyperLink>
                                                            <asp:RequiredFieldValidator ID="ReqFldVal_StartDate" runat="server" CssClass="LabelError"
                                                                ControlToValidate="txtStartDate" 
                                                                ErrorMessage="&lt;br /&gt;Program Start Date is required." Display="Dynamic" 
                                                                ToolTip="Program Start Date is required.">Program Start Date is required.</asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="compVal_StartDate" runat="server" CssClass="LabelError"
                                                                ControlToValidate="txtStartDate" ErrorMessage="Program Start Date is invalid."
                                                                Type="Date" Operator="GreaterThanEqual" Display="Dynamic"><br />Program Start Date is invalid.</asp:CompareValidator>
                                                        </td>
                                                        
                                                        <td align="right" valign="middle"><asp:Label ID="Label29" runat="server" CssClass="StandardLabel">End&nbsp;Date:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:Label></td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtEndDate" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma"
                                                                Width="75px"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtEndDate"
                                                                Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" CultureName="en-US" />
                                                        
                                                            <asp:HyperLink ID="hypLnkEndDate" runat="server" ImageUrl="~/images/Calendar.gif"
                                                                NavigateUrl="javascript:void(0);" ToolTip="Click here to select the end date from a popup calendar !">HyperLink</asp:HyperLink>
                                                            <asp:RequiredFieldValidator ID="ReqFldVal_EndDate" runat="server" CssClass="LabelError"
                                                                ControlToValidate="txtEndDate" 
                                                                ErrorMessage="Program End Date is required." Display="Dynamic">Program End Date is required.</asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="compVal_EndDate" runat="server" CssClass="LabelError" ControlToValidate="txtEndDate"
                                                                ErrorMessage="Program End Date is invalid." Type="Date" 
                                                                Operator="LessThanEqual" Display="Dynamic"><br />Program End Date is invalid.</asp:CompareValidator>
                                                            <asp:CompareValidator ID="compValProgDate" runat="server" CssClass="LabelError" ControlToValidate="txtStartDate"
                                                                ErrorMessage="Program End Date must be greater than the Start Date." Type="Date"
                                                                Operator="LessThan" ControlToCompare="txtEndDate" Display="Dynamic"><br />Program End Date must be greater than the Start Date.</asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="middle"><asp:Label ID="Label33" runat="server" CssClass="StandardLabel">Closed&nbsp;Start&nbsp;Date:&nbsp;</asp:Label></td>
                                                        <td align="left" valign="middle">
                                                                        <asp:TextBox ID="txtHolidayStartDate" runat="server" Font-Size="9px" Height="14px"
                                                                            Font-Names="Verdana, Arial, Tahoma" Width="75px"></asp:TextBox>
                                                                    
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtHolidayStartDate"
                                                                            Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left"
                                                                            AcceptNegative="Left" CultureName="en-US" />
                                                                        <asp:HyperLink ID="hypLnkHolidayStartDate" runat="server" ImageUrl="~/images/Calendar.gif"
                                                                            NavigateUrl="javascript:void(0);" ToolTip="Click here to select the start date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
                                                                    <asp:CompareValidator ID="compVal_HolidayStartDate" runat="server" CssClass="LabelError"
                                                                            ControlToValidate="txtHolidayStartDate" ErrorMessage="Program Close Start Date is invalid."
                                                                            Type="Date" Operator="DataTypeCheck">*</asp:CompareValidator>
                                                        </td>
                                                        
                                                        <td align="right" valign="middle"><asp:Label ID="Label34" runat="server" CssClass="StandardLabel">Closed&nbsp;End&nbsp;Date:&nbsp;</asp:Label></td>
                                                        <td align="left" valign="middle">
                                                            
                                                                        <asp:TextBox ID="txtHolidayEndDate" runat="server" Font-Size="9px" Height="14px"
                                                                            Font-Names="Verdana, Arial, Tahoma" Width="75px"></asp:TextBox>
                                                                    
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtHolidayEndDate"
                                                                            Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left"
                                                                            AcceptNegative="Left" CultureName="en-US" />
                                                                        <asp:HyperLink ID="hypLnkHolidayEndDate" runat="server" ImageUrl="~/images/Calendar.gif"
                                                                            NavigateUrl="javascript:void(0);" ToolTip="Click here to select the holiday end date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
                                                                    <asp:CompareValidator ID="compVal_HolidayEndDate" runat="server" CssClass="LabelError"
                                                                            ControlToValidate="txtHolidayEndDate" ErrorMessage="<br />Program Close End Date is invalid."
                                                                            Type="Date" Operator="DataTypeCheck">*</asp:CompareValidator><asp:CompareValidator ID="Comparevalidator3" runat="server" CssClass="LabelError"
                                                                            ControlToValidate="txtHolidayStartDate" ErrorMessage="<br />Program Close End Date must be greater than the Start Date."
                                                                            Type="Date" Operator="LessThan" ControlToCompare="txtHolidayEndDate">*</asp:CompareValidator>
                                                                        <asp:Label ID="txtHolidayEndDateResult" runat="server" ForeColor="Red" 
                                                                            Text="A Closed Start Date MUST have an end date" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"><img src="../images/spacer.gif" width="580" height="5" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="middle"><asp:Label ID="Label16" runat="server" CssClass="StandardLabel">GOAL-&nbsp;Gross:&nbsp;($)&nbsp;*&nbsp;</asp:Label></td>
                                                        <td align="left" valign="middle"><asp:TextBox ID="txtEstimatedAmount" runat="server" CssClass="DescLabel" Width="75px"></asp:TextBox></td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="EstimatedAmountRequiredFieldValidator" runat="server" CssClass="LabelError"
                                                                ErrorMessage="The GOAL - Estimated Gross is required." ControlToValidate="txtEstimatedAmount">*</asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="compVal_EstimatedAmount" runat="server" CssClass="LabelError"
                                                                ControlToValidate="txtEstimatedAmount" ErrorMessage="The Estimated Amount is invalid."
                                                                Type="Currency" Operator="DataTypeCheck">*</asp:CompareValidator>
                                                            <asp:RangeValidator ID="RngVal_EstimatedAmount" runat="server" CssClass="LabelError"
                                                                ControlToValidate="txtEstimatedAmount" ErrorMessage="The Estimated Amount must be between 0 and 50,000,000."
                                                                Type="Currency" MaximumValue="50000000" MinimumValue="0">*</asp:RangeValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="middle"><asp:Label ID="Label23" runat="server" CssClass="StandardLabel">#&nbsp;Participants:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:Label></td>
                                                        <td align="left" valign="middle"><asp:TextBox ID="txtEnrollment" runat="server" CssClass="DescLabel" Width="75px"></asp:TextBox></td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="ReqFldVal_txtEnrollment" runat="server" CssClass="LabelError"
                                                                ErrorMessage="The # Participants is required." ControlToValidate="txtEnrollment">*</asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="compVal_Enrollment" runat="server" CssClass="LabelError"
                                                                ControlToValidate="txtEnrollment" ErrorMessage="The # Participants is required."
                                                                Type="Integer" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                            <asp:RangeValidator ID="RngVal_Enrollment" runat="server" CssClass="LabelError" ControlToValidate="txtEnrollment"
                                                                ErrorMessage="The # of Participants must be between 0 and 9999." Type="Integer"
                                                                MaximumValue="9999" MinimumValue="0">*</asp:RangeValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"><img src="../images/spacer.gif" width="580" height="5" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="middle"><asp:Label ID="Label35" runat="server" CssClass="StandardLabel">Account&nbsp;Profit:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:Label></td>
                                                        <td align="left" valign="middle">
                                                            <asp:RadioButtonList ID="radBtnLstProfitRate" runat="server" CssClass="DescInfoLabel"
                                                                RepeatDirection="Horizontal">
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="reqVal_radBtnLstPrfitRate" runat="server" CssClass="LabelError"
                                                                ErrorMessage="Account Profit is required." ControlToValidate="radBtnLstProfitRate">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
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
            <br />
            <table id="Tablesds1e1" cellspacing="0" cellpadding="0" width="600" border="0">
                <tr>
                    <td align="center">
                        <table cellspacing="0" cellpadding="2" width="600" border="0">
                            <tr>
                                <td class="SectionPageTitleInfo" colspan="2">
                                    <asp:Label ID="lblTitleProgramInformationInfo" runat="server">
							        Program&nbsp;Information&nbsp;
                                    </asp:Label></td>
                            </tr>
                            <tr id="trValSumProgramInformationInfo" runat="server" visible="false" align="left">
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Correct the following error[s] to proceed."
                                        CssClass="LabelError"></asp:ValidationSummary>
                                </td>
                            </tr>
                            <tr>
                                <td height="5px">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblProgramInformationInfo" cellspacing="0" cellpadding="0" width="100%"
                                        border="0" runat="server">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="1" border="0">
                                                    <tr>
                                                        <td><img src="../images/spacer.gif" width="145" height="1" /></td>
                                                        <td><img src="../images/spacer.gif" width="435" height="1" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="middle"><asp:Label ID="Label5" runat="server" CssClass="StandardLabel">Catalog&nbsp;Selection:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:Label></td>
                                                        <td align="left" valign="middle">
                                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" CssClass="DescInfoLabel"></asp:CheckBoxList>
                                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Catalog Selection is required" OnServerValidate="CustomValidator1_ServerValidate">*</asp:CustomValidator>
                                                            </td>
                                                    </tr>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="1" border="0">
                                                    <tr>
                                                        <td><img src="../images/spacer.gif" width="145" height="1" /></td>
                                                        <td><img src="../images/spacer.gif" width="435" height="1" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="middle"><asp:Label ID="Label11" runat="server" CssClass="StandardLabel">Catalog&nbsp;Type:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:Label></td>
                                                        <td align="left" valign="middle">
                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="DescInfoLabel" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="LabelError" ErrorMessage="Catalog Type is required." ControlToValidate="RadioButtonList1">*</asp:RequiredFieldValidator>
                                                            </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <asp:Label ID="Label32" runat="server" CssClass="RequiredSymbol">*</asp:Label>
                        &nbsp;
                        <asp:Label ID="Label36" runat="server" CssClass="RequiredSymbolLabel">Required Field</asp:Label>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td align="center">
        </td>
    </tr>
</table>

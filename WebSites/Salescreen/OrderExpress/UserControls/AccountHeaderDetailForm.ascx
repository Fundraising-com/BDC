<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountHeaderDetailForm"
    CodeBehind="AccountHeaderDetailForm.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="DualAddressForm" Src="DualAddressForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MatchingAccountList" Src="~/UserControls/MatchingAccountList.ascx" %>
<uc1:MatchingAccountList ID="MatchingAccountListControl" runat="server" />
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tbody>
        <tr>
            <td align="center">
                <table id="Table1e" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td class="SectionPageTitleInfo" align="left">
                                <asp:Label ID="lblTitleAccountInfo" runat="server">
							Account Information
                                </asp:Label>
                            </td>
                        </tr>
                        <tr id="trValSumAccountInfo" runat="server" visible="false">
                            <td align="left">
                                <asp:ValidationSummary ID="ValSumAccountInfo" runat="server" HeaderText="Please correct the following error[s] to proceed."
                                    CssClass="LabelError"></asp:ValidationSummary>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <!--Section Body -->
                                <table id="tblAccountInfo" cellspacing="0" cellpadding="0" width="100%" border="0"
                                    runat="server">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" CssClass="StandardLabel">QSP&nbsp;Account&nbsp;ID&nbsp;#:&nbsp;</asp:Label>
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
                                        <tr id="trOrgNameInfo" runat="server">
                                            <td style="height: 18px">
                                                <asp:Label ID="Label32" runat="server" CssClass="StandardLabel">
													Organization&nbsp;Name:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td style="height: 18px">
                                            </td>
                                            <td width="100%" style="height: 18px">
                                                <asp:Label ID="lblOrganizationName" runat="server" CssClass="DescInfoLabel">Organization Name</asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trOrgNameEdit" runat="server">
                                            <td>
                                                <asp:Label ID="Label31" runat="server" CssClass="StandardLabel">
													Organization&nbsp;Name:&nbsp;<span class="RequiredSymbolLabel">*</span>
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="100%">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtOrganizationName" runat="server" CssClass="DescLabel" MaxLength="30"></asp:TextBox>
                                                            <ajaxtoolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                                TargetControlID="txtOrganizationName" WatermarkText="Type Organization Here"
                                                                WatermarkCssClass="DescLabel" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="ReqFldVal_OrgName" runat="server" CssClass="LabelError"
                                                                ControlToValidate="txtOrganizationName" ErrorMessage="The Organization Name is required.">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label36" runat="server" CssClass="NoteSmallLabel">
																The Organization Name is limited to 30 characters maximum.
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" CssClass="StandardLabel">
													Account&nbsp;Name:&nbsp;<span class="RequiredSymbolLabel">*</span>
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="100%">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtAccountName" runat="server" CssClass="DescLabel" Columns="30"
                                                                MaxLength="30"></asp:TextBox>
                                                        </td>
                                                        <ajaxtoolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                                            TargetControlID="txtAccountName" WatermarkText="Enter Account Name Here" WatermarkCssClass="DescLabel" />
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="ReqFldVal_AccountName" runat="server" CssClass="LabelError"
                                                                ControlToValidate="txtAccountName" ErrorMessage="The Account Name is required.">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="ReqFldVal_AccountName1" runat="server" CssClass="LabelError"
                                                                InitialValue="New Account" ControlToValidate="txtAccountName" ErrorMessage="You must enter a different name than the default value :New Account.">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAccountName"
                                                                CssClass="LabelError" Display="Dynamic" ErrorMessage="Asterisks (*) is not allowed."
                                                                ValidationExpression="[^\*]*">*</asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;-&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAccountName1" runat="server" CssClass="DescLabel" Columns="30"
                                                                MaxLength="30"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAccountName1"
                                                                CssClass="LabelError" Display="Dynamic" ErrorMessage="Asterisks (*) is not allowed."
                                                                ValidationExpression="[^\*]*">*</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Label ID="Label12" runat="server" CssClass="NoteSmallLabel">
																Each Account Name field is limited to 30 characters
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trAccountStatus" runat="server" visible="False">
                                            <td valign="top" align="left">
                                                <asp:Label ID="Label17" runat="server" CssClass="StandardLabel">
													Account&nbsp;Status:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="100%">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAccountStatusColor" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                BorderWidth="1px" BackColor="Orange" Height="13px" Width="8px">
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
                                                            <asp:Label ID="lblAccountStatus_Description" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trAccountFY" runat="server" visible="False">
                                            <td>
                                                <asp:Label ID="Label24" runat="server" CssClass="StandardLabel">
													Fiscal&nbsp;Year:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="100%">
                                                <asp:Label ID="lblFiscalYear" runat="server" CssClass="DescInfoLabel">2005</asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trFmInfo" runat="server">
                                            <td>
                                                <asp:Label ID="Label11" runat="server" CssClass="StandardLabel">
													Field&nbsp;Sales&nbsp;Manager:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="100%">
                                                <asp:Label ID="lblFMInfo" runat="server" CssClass="DescInfoLabel">0000 - John Smith</asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trFmEdit" runat="server">
                                            <td valign="top">
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
                                                            <asp:CompareValidator ID="CompValFMID" runat="server" CssClass="LabelError" ControlToValidate="txtFMID"
                                                                ErrorMessage="The FM ID is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:CompareValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                            <asp:TextBox ID="txtFMName" runat="server" Width="230px" CssClass="StandardTextBox"
                                                                Enabled="True"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="ReqFldVal_FMID" runat="server" CssClass="LabelError"
                                                                ControlToValidate="txtFMID" ErrorMessage="The FSM is required.">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="imgBtnSelectFM" runat="server" CausesValidation="False" ImageUrl="~/images/BtnSelect.gif">
                                                            </asp:ImageButton>
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
                                        <tr id="trOrgTypeInfo" runat="server">
                                            <td valign="top">
                                                <asp:Label ID="Label8" runat="server" CssClass="StandardLabel">
													Organization&nbsp;Type:&nbsp;
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
                                                            <asp:Label ID="Label14" runat="server" CssClass="StandardLabel">
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
                                        <tr id="trOrgTypeEdit" runat="server">
                                            <td valign="top">
                                                <asp:Label ID="Label19" runat="server" CssClass="StandardLabel">
													Organization&nbsp;Type:&nbsp;<span class="RequiredSymbolLabel">*</span>
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="100%">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td valign="top">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlOrgType" runat="server" CssClass="DescLabel" DataTextField="organization_type_name"
                                                                            DataValueField="organization_type_id">
                                                                            <asp:ListItem Value="1" Selected="True">Public</asp:ListItem>
                                                                            <asp:ListItem Value="2">Catholic</asp:ListItem>
                                                                            <asp:ListItem Value="3">Christian</asp:ListItem>
                                                                            <asp:ListItem Value="4">Other</asp:ListItem>
                                                                            <asp:ListItem Value="5">Campus</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="LabelError"
                                                                            ErrorMessage="The Organization Type is required." ControlToValidate="ddlOrgType"
                                                                            Operator="GreaterThan" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="Label20" runat="server" CssClass="StandardLabel">
																Organization&nbsp;Level:&nbsp;<span class="RequiredSymbolLabel">*</span>
                                                            </asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td valign="top">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlOrgLevel" runat="server" CssClass="DescLabel" DataTextField="organization_level_name"
                                                                            DataValueField="organization_level_id">
                                                                            <asp:ListItem Value="1">Elementary</asp:ListItem>
                                                                            <asp:ListItem Value="2">Middle</asp:ListItem>
                                                                            <asp:ListItem Value="3">High</asp:ListItem>
                                                                            <asp:ListItem Value="4" Selected="True">Other</asp:ListItem>
                                                                            <asp:ListItem Value="5">Baseball</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="LabelError"
                                                                            ErrorMessage="The Organization Level is required." ControlToValidate="ddlOrgLevel"
                                                                            Operator="GreaterThan" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trTradeClassInfo" runat="server">
                                            <td>
                                                <asp:Label ID="Label33" runat="server" CssClass="StandardLabel">
													Trade&nbsp;Class:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="100%">
                                                <asp:Label ID="lblTradeClass" runat="server" CssClass="DescInfoLabel">None</asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trTradeClassEdit" runat="server">
                                            <td valign="top">
                                                <asp:Label ID="Label35" runat="server" CssClass="StandardLabel">
													Trade&nbsp;Class:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td valign="top" width="100%">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTradeClass" runat="server" CssClass="DescLabel" DataTextField="trade_class_name"
                                                                DataValueField="trade_class_id">
                                                                <asp:ListItem Value="0">None</asp:ListItem>
                                                                <asp:ListItem Value="1">AYSO</asp:ListItem>
                                                                <asp:ListItem Value="2">Camp Fire</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trProgramTypeInfo" runat="server">
                                            <td>
                                                <asp:Label ID="Label18" runat="server" CssClass="StandardLabel">
													QSP&nbsp;Program:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="100%">
                                                <asp:Label ID="lblProgramType" runat="server" CssClass="DescInfoLabel">Chocolate</asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trProgramTypeEdit" runat="server">
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="StandardLabel">
													QSP&nbsp;Program:&nbsp;<span class="RequiredSymbolLabel">*</span>
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlType" runat="server" DataTextField="program_type_name" DataValueField="program_type_id">
                                                </asp:DropDownList>
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
                                            <td width="100%">
                                                <asp:Label ID="lblDefaultWarehouse" runat="server" CssClass="DescInfoLabel"></asp:Label>
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
                                            <td>
                                                <table id="tblTaxInfoEdit" cellspacing="0" cellpadding="0" width="400" border="0"
                                                    runat="server">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtTaxExemptionNumber" runat="server" CssClass="DescLabel" Width="200px"
                                                                MaxLength="25"></asp:TextBox>
                                                        </td>
                                                        <td width="100%">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" CssClass="StandardLabel">Expire:&nbsp;</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTaxExemptionExpirationDate" runat="server" CssClass="DescLabel"
                                                                Width="100px" MaxLength="10" Height="14px" Font-Name="Arial" Font-Size="11px"></asp:TextBox>
                                                            <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtTaxExemptionExpirationDate"
                                                                Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" CultureName="en-US" />
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hypLnkTaxExemptionExpirationDate" runat="server" ImageUrl="~/images/Calendar.gif"
                                                                NavigateUrl="javascript:void(0);" ToolTip="Click here to select the date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:CompareValidator ID="compVal_TaxExemptionExpirationDate" runat="server" CssClass="LabelError"
                                                                ControlToValidate="txtTaxExemptionExpirationDate" ErrorMessage="The Tax Exemption Expiration Date is invalid."
                                                                Type="Date" Operator="DataTypeCheck">*</asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table id="tblTaxInfoReadOnly" cellspacing="0" cellpadding="0" width="400" border="0"
                                                    runat="server">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTaxExemptionNumber" runat="server" CssClass="DescInfoLabel"></asp:Label>
                                                        </td>
                                                        <td width="100">
                                                            &nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td width="1">
                                                            <asp:Label ID="Label5" runat="server" CssClass="StandardLabel">Expire:&nbsp;</asp:Label>
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
                                            <td valign="top">
                                                <asp:Label ID="Label12ee" runat="server" CssClass="StandardLabel">
													Comment:&nbsp;
                                                </asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtComment" runat="server" CssClass="DescLabel" Width="95%" Rows="4"
                                                    TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="Label21" runat="server" CssClass="NoteLabel">
												Note:&nbsp;
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" CssClass="NoteLabel">
												Exemption or resale certificate forms required with order.  Based on state laws, Invoice will include taxes unless account is tax exempt.												
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td valign="top">
                                            <asp:Label ID="Label22" runat="server" CssClass="RequiredSymbol">
												*&nbsp;
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label25" runat="server" CssClass="RequiredSymbolLabel">
												Required Field
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br>
            </td>
        </tr>
        <tr>
            <td align="center">
                <uc1:DualAddressForm ID="DualAddressFormControl" runat="server" EntityName="Account&nbsp;Name"
                    HygieneAddress="true"></uc1:DualAddressForm>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table cellspacing="0" cellpadding="2" width="600" border="0">
                    <tr>
                        <td class="SectionPageTitleInfo" colspan="2" align="left">
                            <asp:Label ID="lblTitleOrderTermInfo" runat="server">
							Order&nbsp;Terms
                            </asp:Label>
                        </td>
                    </tr>
                    <tr id="trValSumOrderTermInfo" runat="server" visible="false">
                        <td align="left">
                            <asp:ValidationSummary ID="ValSumOrderTermInfo" runat="server" HeaderText="Correct the following error to proceed."
                                CssClass="LabelError"></asp:ValidationSummary>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label13" runat="server" CssClass="NoteLabel">
							You are in agreement that QSP will be working with your organization in connection with a fundraising program as follows:
                            </asp:Label>
                            <br>
                        </td>
                    </tr>
                    <tr>
                        <td height="5px">
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <table id="tblSumOrderTermInfo" cellspacing="0" cellpadding="0" width="100%" border="0"
                                runat="server">
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" CssClass="StandardLabel">Start&nbsp;Date:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStartDate" runat="server" Width="100px" Font-Size="9px" CssClass="StandardTextBox"
                                                        ToolTip="Input a Date as mm/dd/yyyy" Height="14px" Font-Names="Verdana, Arial, Tahoma"
                                                        MaxLength="10"></asp:TextBox>
                                                    <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartDate"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" CultureName="en-US" />
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hypLnkStartDate" runat="server" ImageUrl="~/images/Calendar.gif"
                                                        NavigateUrl="javascript:void(0);" ToolTip="Click here to select the start date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="LabelError"
                                                        ControlToValidate="txtStartDate" ErrorMessage="The Program Start Date is required.">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:CompareValidator ID="compVal_StartDate" runat="server" CssClass="LabelError"
                                                        ControlToValidate="txtStartDate" ErrorMessage="The Program Start Date is invalid."
                                                        Type="Date" Operator="DataTypeCheck">*</asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:RangeValidator ID="rangVal_StartDate" runat="server" CssClass="LabelError" ControlToValidate="txtStartDate"
                                                        ErrorMessage="The Program Start Date must be included in the current Fiscal Year."
                                                        Type="Date">*</asp:RangeValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label29" runat="server" CssClass="StandardLabel">End&nbsp;Date:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEndDate" runat="server" Width="100px" Font-Size="9px" CssClass="StandardTextBox"
                                                        Height="14px" Font-Names="Verdana, Arial, Tahoma"></asp:TextBox>
                                                    <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtEndDate"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" CultureName="en-US" />
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hypLnkEndDate" runat="server" ImageUrl="~/images/Calendar.gif"
                                                        NavigateUrl="javascript:void(0);" ToolTip="Click here to select the end date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="ReqFldVal_EndDate" runat="server" CssClass="LabelError"
                                                        ControlToValidate="txtEndDate" ErrorMessage="The Program End Date is required.">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 9px">
                                                    <asp:CompareValidator ID="compVal_EndDate" runat="server" CssClass="LabelError" ControlToValidate="txtEndDate"
                                                        ErrorMessage="The Program End Date is invalid." Type="Date" Operator="DataTypeCheck">*</asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:CompareValidator ID="compValProgDate" runat="server" CssClass="LabelError" ControlToValidate="txtStartDate"
                                                        ErrorMessage="The End Date must be greater than the Start Date." Type="Date"
                                                        Operator="LessThan" ControlToCompare="txtEndDate">*</asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:RangeValidator ID="rangVal_EndDate" runat="server" CssClass="LabelError" ControlToValidate="txtEndDate"
                                                        ErrorMessage="The Program End Date must be included in the current Fiscal Year."
                                                        Type="Date">*</asp:RangeValidator>
                                                </td>
                                                <td>
                                                    <asp:CustomValidator ID="custProgramDateValidator" runat="server" CssClass="LabelError"
                                                        ErrorMessage="Start Date and End Date must be in the same fiscal year" Display="dynamic"
                                                        OnServerValidate="ValidateProgramDate">*</asp:CustomValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" CssClass="StandardLabel" Width="200px">GOAL-&nbsp;Estimated&nbsp;Gross:&nbsp;($)&nbsp;</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEstimatedAmount" runat="server" CssClass="DescLabel" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CompareValidator ID="compVal_EstimatedAmount" runat="server" CssClass="LabelError"
                                                        ControlToValidate="txtEstimatedAmount" ErrorMessage="The Estimated Amount is invalid."
                                                        Type="Currency" Operator="DataTypeCheck">*</asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:RangeValidator ID="RngVal_EstimatedAmount" runat="server" CssClass="LabelError"
                                                        ControlToValidate="txtEstimatedAmount" ErrorMessage="The Estimated Amount must be between 0 and 50,000,000"
                                                        Type="Currency" MaximumValue="50000000" MinimumValue="0">*</asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label23" runat="server" CssClass="StandardLabel" Width="200px">Enrollment:&nbsp;</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEnrollment" runat="server" CssClass="DescLabel" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CompareValidator ID="compVal_Enrollment" runat="server" CssClass="LabelError"
                                                        ControlToValidate="txtEnrollment" ErrorMessage="The Enrollment is invalid." Type="Integer"
                                                        Operator="DataTypeCheck">*</asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:RangeValidator ID="RngVal_Enrollment" runat="server" CssClass="LabelError" ControlToValidate="txtEnrollment"
                                                        ErrorMessage="The Enrollment must be between 0 and 500,000" Type="Integer" MaximumValue="500000"
                                                        MinimumValue="0">*</asp:RangeValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="Label28" runat="server" CssClass="RequiredSymbol">
											*&nbsp;
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label30" runat="server" CssClass="RequiredSymbolLabel">
											Required Field
                                        </asp:Label>
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
                                        <asp:Label ID="lblBusinessMessage" runat="server" CssClass="BizRuleLabel"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br>
                <br>
            </td>
        </tr>
    </tbody>
</table>

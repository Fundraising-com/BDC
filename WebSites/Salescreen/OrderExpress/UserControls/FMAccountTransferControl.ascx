<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="QSP.OrderExpress.Web.UserControls.FMAccountTransferControl" Codebehind="FMAccountTransferControl.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table id="Table5" cellspacing="0" cellpadding="0" align="left" border="0">
    <tr>
        <td>
            <asp:Label ID="ErrorMessage" runat="server" CssClass="LabelError"></asp:Label>
        </td>
    </tr>
    <tr id="dkfjks" runat="server">
        <td>
            <table id="Table6" cellspacing="0" cellpadding="0" align="left" border="0">
                <tr>
                    <td style="width: 155px">
                        <asp:Label ID="Label3" runat="server" CssClass="StandardLabel">
								Show Accounts For:<span class="RequiredSymbolLabel">*</span>
                        </asp:Label></td>
                    <td style="width: 323px">
                        <asp:TextBox ID="txtFMID" runat="server" CssClass="StandardTextBox" Enabled="True"
                            Width="50px"></asp:TextBox>
                        <asp:TextBox ID="txtFMName" runat="server" CssClass="StandardTextBox" Enabled="True"
                            Width="230px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFMID"
                            CssClass="LabelError" ErrorMessage="The FSM is required.">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFMID"
                            CssClass="LabelError" ErrorMessage="The FM ID is invalid (must be a number)."
                            Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator></td>
                    <td colspan="2">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgBtnSelectFM" runat="server" CausesValidation="False" ImageUrl="~/images/BtnSelect.gif" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/images/btnRefresh.gif"
                                        OnClick="ImageButton1_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr width="100%" color="#666666" size="1">
                    </td>
                    <td colspan="1">
                    </td>
                </tr>
                <tr>
                    <td style="width: 155px">
                        <asp:Label ID="Label1" runat="server" Text="To FM" CssClass="StandardLabel"></asp:Label></td>
                    <td style="width: 323px">
                        <asp:TextBox ID="txtFMID1" runat="server" CssClass="StandardTextBox" Width="50px"></asp:TextBox>
                        <asp:TextBox ID="txtFMName1" runat="server" CssClass="StandardTextBox" Width="230px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFMID1"
                            CssClass="LabelError" ErrorMessage="The FSM is required.">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtFMID1"
                            CssClass="LabelError" ErrorMessage="The FM ID is invalid (must be a number)."
                            Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
                    </td>
                    <td style="width: 78px">
                        <asp:ImageButton ID="imgBtnSelectFM1" runat="server" ImageUrl="~/images/BtnSelect.gif" /></td>
                    <td style="width: 202px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 155px">
                        <asp:Label ID="Label2" runat="server" Text="Sales To FM" CssClass="StandardLabel"></asp:Label></td>
                    <td style="width: 323px">
                        <asp:TextBox ID="txtFMID2" runat="server" CssClass="StandardTextBox" Width="50px"></asp:TextBox>
                        <asp:TextBox ID="txtFMName2" runat="server" CssClass="StandardTextBox" Width="230px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFMID2"
                            CssClass="LabelError" ErrorMessage="The FSM is required.">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtFMID2"
                            CssClass="LabelError" ErrorMessage="The FM ID is invalid (must be a number)."
                            Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
                    </td>
                    <td style="width: 78px">
                        <asp:ImageButton ID="imgBtnSelectFM2" runat="server" ImageUrl="~/images/BtnSelect.gif" /></td>
                    <td style="width: 202px">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr width="100%" color="#666666" size="1">
                    </td>
                    <td colspan="1">
                    </td>
                </tr>
                <tr>
                    <td style="width: 155px; height: 36px;">
                        <asp:Label ID="Label4" runat="server" Text="Effective Date" CssClass="StandardLabel"></asp:Label></td>
                    <td style="width: 323px; height: 36px;" colspan="2">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="StandardTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDate"
                            CssClass="LabelError" ErrorMessage="Please Enter the Date">*</asp:RequiredFieldValidator>
                             <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtDate"
                            CssClass="LabelError" ErrorMessage="Please Enter a Valid Date"
                            Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
                        <asp:ImageButton ID="Image1" runat="Server" AlternateText="Click to show calendar"
                            ImageUrl="~/images/Calendar.gif" /><br />
                        <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" PopupButtonID="Image1"
                            TargetControlID="txtDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td colspan="1" style="width: 323px; height: 36px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 155px">
                        <asp:Label ID="Label5" runat="server" Text="Reason" CssClass="StandardLabel"></asp:Label></td>
                    <td style="width: 323px" colspan="2">
                        <asp:TextBox ID="txtReason" runat="server" CssClass="StandardTextBox" TextMode="MultiLine"
                            Width="297px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtReason"
                            CssClass="LabelError" ErrorMessage="Please Enter the Reason">*</asp:RequiredFieldValidator><br />
                    </td>
                    <td colspan="1" style="width: 323px">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr width="100%" color="#666666" size="1">
                    </td>
                    <td colspan="1">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgSelect1" runat="server" ImageUrl="~/images/TransferAccounts.gif"
                                        OnClick="imgSelect1_Click" />
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgSelect2" runat="server" ImageUrl="~/images/TransferTerritory.gif"
                                        OnClick="imgSelect2_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr width="100%" color="#666666" size="1">
                    </td>
                    <td colspan="1">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" width="400" border="0">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page&nbsp;1&nbsp;of&nbsp;1</asp:Label></td>
                    <td align="right">
                        <asp:Label ID="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click&nbsp;on&nbsp;Column&nbsp;Headings&nbsp;to&nbsp;Resort&nbsp;Data.</asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%">
            <!--DataGrid  -->
            <asp:GridView ID="MasterGridView" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="fulf_account_id" OnPageIndexChanging="MasterGridView_PageIndexChanging"
                OnSorting="MasterGridView_Sorting" PageSize="30">
                <PagerStyle CssClass="PagerItemStyle" />
                <FooterStyle CssClass="FooterItemStyle" />
                <HeaderStyle CssClass="HeaderItemStyle" ForeColor="White" Wrap="False" />
                <Columns>
                    <asp:BoundField DataField="flagpoleinstance" HeaderText="FlagPole ID" SortExpression="flagpoleinstance asc" />
                    <asp:BoundField DataField="flagpole_name" HeaderText="FlagPole Name" SortExpression="flagpole_name asc">
                        <ItemStyle Wrap="False" Width="275px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="account_name" HeaderText="Account Name" SortExpression="account_name asc">
                        <ItemStyle Width="250px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fulf_account_id" HeaderText="EDS Account #" SortExpression="fulf_account_id asc" />
                    <asp:BoundField DataField="city" HeaderText="City" SortExpression="city asc">
                        <ItemStyle Wrap="False" Width="130px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="zip" HeaderText="Zip" SortExpression="zip asc">
                        <ItemStyle Wrap="False" Width="90px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="state" HeaderText="State" SortExpression="state asc">
                        <ItemStyle Wrap="False" Width="40px"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Select FlagPole">
                        <ItemTemplate>
                            <asp:CheckBox ID="FlagPoleID" runat="server" AutoPostBack="True" OnCheckedChanged="FlagPoleID_CheckedChanged"
                                value='<%# Eval("flagpoleinstance") %>' />&nbsp;
                        </ItemTemplate>
                        <ControlStyle ForeColor="White" />
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle ForeColor="White" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select Account">
                        <ItemTemplate>
                            <asp:CheckBox ID="AccountCheckBox" runat="server" OnCheckedChanged="AccountID_CheckedChanged" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle ForeColor="White" />
                        <ControlStyle ForeColor="White" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="ItemStyle_off" Font-Size="12px" />
                <SelectedRowStyle CssClass="SelectedItemStyle" />
                <AlternatingRowStyle CssClass="AlternatingItemStyle_off" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        <br>
                        <asp:Label ID="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Account(s):
                        </asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>

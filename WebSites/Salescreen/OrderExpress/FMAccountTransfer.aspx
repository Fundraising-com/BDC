<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.FMAccountTransfer" Codebehind="FMAccountTransfer.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %>
<%@ MasterType VirtualPath="~/MainMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContentHolder" runat="Server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="left">
                <uc1:Header ID="Header" runat="server"></uc1:Header>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <div>
        <asp:Label ID="Label3" runat="server" CssClass="StandardLabel">
								Show Accounts For:<span class="RequiredSymbolLabel">*</span>
        </asp:Label>
        <asp:ImageButton ID="imgBtnSelectFM" runat="server" ImageUrl="~/images/BtnSelect.gif"
            CausesValidation="False"></asp:ImageButton>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/GET-ACCOUNT.gif"
            CausesValidation="False" OnClick="ImageButton1_Click"></asp:ImageButton>
       </div>
    <div>
        <asp:TextBox ID="txtFMID" runat="server" Width="50px" CssClass="StandardTextBox"
            Enabled="True"></asp:TextBox>
        <asp:TextBox ID="txtFMName" runat="server" Width="230px" CssClass="StandardTextBox"
            ReadOnly="True" Enabled="True"></asp:TextBox>
        <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="LabelError"
            ErrorMessage="The FM ID is invalid (must be a number)." ControlToValidate="txtFMID"
            Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="LabelError"
            ErrorMessage="The FSM is required." ControlToValidate="txtFMID">*</asp:RequiredFieldValidator>
    </div>
    <div>
        &nbsp;<br />
        <hr />
        &nbsp;</div>
    <asp:Label ID="Label1" runat="server" Text="To FM"></asp:Label>
    <asp:TextBox ID="txtFMID1" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtFMName1" runat="server"></asp:TextBox>&nbsp;<asp:ImageButton
        ID="imgBtnSelectFM1" runat="server" ImageUrl="~/images/BtnSelect.gif" />
    <asp:Label ID="Label2" runat="server" Text="Sales To FM"></asp:Label>
    <asp:TextBox ID="txtFMID2" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtFMName2" runat="server"></asp:TextBox>
    <asp:ImageButton ID="imgBtnSelectFM2" runat="server" ImageUrl="~/images/BtnSelect.gif" /><br />
    <asp:Label ID="Label4" runat="server" Text="Effective Date"></asp:Label>
    <asp:TextBox runat="server" ID="txtDate" />
    <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/images/Calendar.gif" AlternateText="Click to show calendar" /><br />
    <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txtDate"
        PopupButtonID="Image1"  />
    <br />
    <asp:Label ID="Label5" runat="server" Text="Reason"></asp:Label>
    <asp:TextBox ID="txtReason" runat="server"></asp:TextBox><br />
    <asp:ImageButton ID="imgSelect1" runat="server" ImageUrl="~/images/TransferAccounts.gif" OnClick="imgSelect1_Click" />
    <asp:ImageButton ID="imgSelect2" runat="server" ImageUrl="~/images/TransferTerritory.gif" OnClick="imgSelect2_Click" /><br />
    <hr />
    <asp:Label ID="sample" runat="server"></asp:Label>
    <hr />
    <asp:GridView ID="MasterGridView" runat="server" DataKeyNames="fulf_account_id"
        AutoGenerateColumns="False" AllowPaging="True" AllowSorting="true" OnPageIndexChanging="MasterGridView_PageIndexChanging" OnSorting="MasterGridView_Sorting">
        <PagerStyle CssClass="PagerItemStyle"></PagerStyle>
        <FooterStyle CssClass="FooterItemStyle"></FooterStyle>
        <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" ForeColor="White"></HeaderStyle>
        <Columns>
            <asp:BoundField DataField="flagpoleinstance" HeaderText="FlagPole ID" SortExpression="flagpoleinstance asc" />
            <asp:BoundField DataField="flagpole_name" HeaderText="FlagPole Name" SortExpression="flagpole_name asc" />
            <asp:BoundField DataField="account_name" HeaderText="Account Name" SortExpression="account_name asc" />
            <asp:BoundField DataField="fulf_account_id" HeaderText="EDS Account #" SortExpression="fulf_account_id asc" />
            <asp:BoundField DataField="city" HeaderText="City" SortExpression="city asc" />
            <asp:BoundField DataField="zip" HeaderText="Zip" SortExpression="zip asc" />
            <asp:BoundField DataField="state" HeaderText="State" SortExpression="state asc" />
            <asp:TemplateField HeaderText="Select FlagPole" ControlStyle-ForeColor="White">
                <ItemTemplate>
                    <asp:CheckBox ID="FlagPoleID" runat="server" OnCheckedChanged="FlagPoleID_CheckedChanged"
                        value='<%# Eval("flagpoleinstance") %>' AutoPostBack="True" />&nbsp;
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select Account">
                <ItemTemplate>
                    <asp:CheckBox ID="AccountCheckBox" runat="server" OnCheckedChanged="AccountID_CheckedChanged" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle CssClass="ItemStyle_off" Font-Size="12px" />
        <SelectedRowStyle CssClass="SelectedItemStyle" />
        <AlternatingRowStyle CssClass="AlternatingItemStyle_off" />
    </asp:GridView>
    <br />
    <br />
</asp:Content>

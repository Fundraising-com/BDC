<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.AccountTransfer" Codebehind="AccountTransfer.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %>
<%@ Register Src="~/UserControls/AccountTransferControl.ascx" TagName="AccountTransfer" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MainMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContentHolder" Runat="Server">
 <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr> 
            <td align="left">
                <uc1:Header ID="Header" runat="server"></uc1:Header>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentHolder" Runat="Server">
 <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; margin-left: 5px;">
                    <tr>
                        <td align="left">
                            <uc1:AccountTransfer ID="ctrlAccountTransfer" runat="server"></uc1:AccountTransfer>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
        </tr>
    </table>
    <asp:Label id="abcNote" Text="Account Name" runat="server" Visible="false"></asp:Label>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/SecondaryMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.NewOrderDetailDisplay" Codebehind="NewOrderDetailDisplay.aspx.cs" %>

<%@ MasterType VirtualPath="~/SecondaryMaster.master" %>
<%@ Register TagPrefix="uc1" TagName="OrderDetail" Src="~/UserControls/OrderDetailControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContentHolder" runat="Server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="left">
                <uc1:Header ID="Header" runat="server"></uc1:Header>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="left">
                <uc1:OrderDetail ID="OrderDetailControl" runat="server"></uc1:OrderDetail>
                <br>
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
        </tr>
        <tr>
        <td style="height: 20px;">
            &nbsp;</td>
    </tr>
    </table>
</asp:Content>

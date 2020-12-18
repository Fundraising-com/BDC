<%@ Page Language="C#" MasterPageFile="~/SecondaryMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.Logo" Codebehind="Logo.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %>
<%@ Register Src="UserControls/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc2" %>
<%@ Register Src="UserControls/Logo.ascx" TagName="Logo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="left">
                <uc1:Header ID="Header" runat="server"></uc1:Header>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                     margin-left: 5px;">
                    <tr>
                        <td align="left">
                            <uc1:Logo ID="ctrlLogo" runat="server"></uc1:Logo>
                            <br>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <uc2:ToolBar ID="ctrlToolBar" runat="server" />
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
</asp:Content>

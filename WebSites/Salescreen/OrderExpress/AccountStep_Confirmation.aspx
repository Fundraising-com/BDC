<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    Inherits="QSP.OrderExpress.Web.AccountStep_Confirmation" CodeBehind="AccountStep_Confirmation.aspx.cs" %>

<%@ Register Src="UserControls/AccountStep_Confirmation.ascx" TagName="AccountStep_Confirmation"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" runat="Server">
    <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="left">
                <uc1:Header ID="Header" runat="server"></uc1:Header>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; margin-left: 5px;">
                    <tr>
                        <td align="left">
                            <uc1:AccountStep_Confirmation ID="ctrlAccountStep_Confirmation" runat="server"></uc1:AccountStep_Confirmation>
                            <br>
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
    <table id="Table2" height="135px" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="center" colspan="2" height="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

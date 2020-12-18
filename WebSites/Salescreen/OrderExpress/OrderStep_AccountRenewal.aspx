<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.OrderStep_AccountRenewal"  Codebehind="OrderStep_AccountRenewal.aspx.cs" %> 
<%@ Register Src="UserControls/OrderStep_AccountRenewal.ascx" TagName="OrderStep_AccountRenewal" TagPrefix="uc1" %> 
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %>
<%@ MasterType VirtualPath="~/MainMaster.master" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentHolder" Runat="Server"> 
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="left">
                <uc1:Header ID="Header" runat="server"></uc1:Header>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 98%;
                     margin-left: 5px;">
                    <tr>
                        <td align="left">
                            <uc1:OrderStep_AccountRenewal id="ctrlOrderStep_AccountRenewal" runat="server"></uc1:OrderStep_AccountRenewal> 
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
    
</asp:Content> 

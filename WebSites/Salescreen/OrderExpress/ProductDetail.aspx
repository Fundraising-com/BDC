<%@ Page Language="C#" MasterPageFile="~/SecondaryMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.ProductDetail" Codebehind="ProductDetail.aspx.cs" %>

<%@ Register Src="UserControls/ProductDetail.ascx" TagName="ProductDetail" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/HeaderControl.ascx" %>
<%@ MasterType VirtualPath="~/SecondaryMaster.master" %>
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
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; margin-left: 105px;">
                    <tr>
                        <td align="left">
                            <uc1:ProductDetail ID="ctrlProductDetail" runat="server"></uc1:ProductDetail>
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
        <tr>
            <td style="height: 60px;">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

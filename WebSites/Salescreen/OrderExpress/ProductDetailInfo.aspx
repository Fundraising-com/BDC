<%@ Page Language="C#" MasterPageFile="~/SecondaryMaster.master" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.ProductDetailInfo" Codebehind="ProductDetailInfo.aspx.cs" %>

<%@ Register Src="UserControls/ProductDetailInfo.ascx" TagName="ProductDetailInfo"
    TagPrefix="uc1" %>
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
                <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 45px;">
                    <tr>
                        <td align="left">
                            <uc1:ProductDetailInfo ID="ctrlProductDetailInfo" runat="server"></uc1:ProductDetailInfo>
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
    <tr>
        <td style="height: 60px;">
            &nbsp;</td>
    </tr>
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.UserControls.CouponStep_Selection" Codebehind="CouponStep_Selection.ascx.cs" %>
<%@ Register Src="VendorSelector.ascx" TagName="VendorSelector" TagPrefix="uc1" %>
<link href="Styles.css" rel="stylesheet" type="text/css" />
<table cellpadding=0 cellspacing=0 border=0 width=100%>
    <tr>
        <td>
            <uc1:VendorSelector ID="ctrlVendorSelector" runat="server" />
        </td>
    </tr>
</table>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.UserControls.CouponStep_Validation" Codebehind="CouponStep_Validation.ascx.cs" %>
<%@ Register Src="VendorInfo.ascx" TagName="VendorInfo" TagPrefix="uc1" %>
<link href="Styles.css" rel="stylesheet" type="text/css" />
<table cellpadding=0 cellspacing=0>
    <tr>
        <td colspan=2 style="border-bottom:solid 1px black">
            <asp:Label ID="Label2" runat="server" Text="Vendor Information" CssClass="DescLabel" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan=2>
            <uc1:VendorInfo ID="ctrlVendorInfo" runat="server" />
            <br />
        </td>
    </tr>
     <tr>
        <td colspan=2 style="border-bottom:solid 1px black">
            <asp:Label ID="Label3" runat="server" Text="Agreement Terms" CssClass="DescLabel" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr id="trFMID" runat=Server>
        <td style="width: 139px" valign="top">
            <span class="StandardLabel">FM ID:</span></td>
        <td valign="top">
                        <asp:Label ID="lblFMID" runat="server" CssClass="DescLabel"></asp:Label></td>
    </tr>
	<tr>
		<td style="width: 148px; height: 20px;"><asp:label id="Label11" CssClass="StandardLabel" runat="server">Image&nbsp;:</asp:label></td>
		<td style="height: 20px">
            <asp:Label ID="lblImage" runat="server" CssClass="DescLabel"></asp:Label></td>
	</tr>
	<tr>
		<td style="width: 148px"><asp:label id="Label1" CssClass="StandardLabel" runat="server">Offer&nbsp;:</asp:label></td>
		<td>
            <asp:Label ID="lblOffer" runat="server" CssClass="DescLabel"></asp:Label></td>
	</tr>
	<tr>
		<td vAlign="middle" align="right" style="width: 148px; height: 102px;"><asp:image id="imgPromo" Width="100px" ImageUrl="" Runat="server" Height="100px"></asp:image></td>
		<td style="height: 102px"><asp:label id="lblPromotion" CssClass="DescLabelWhiteBackground" Width="325px" Runat="server"
				Height="100px" Font-Size="X-Small"></asp:label></td>
	</tr>
    <tr>
        <td style="width: 139px; height: 19px;" valign="top">
            <span class="StandardLabel">Labeling Start Date :</span></td>
        <td valign="top" style="height: 19px">
            <asp:Label ID="lblStartDate" runat="server" CssClass="DescLabel"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 139px; height: 18px;" valign="top">
            <span class="StandardLabel">Labeling End Date :</span></td>
        <td valign="top" style="height: 18px">
                        <asp:Label ID="lblEndDate" runat="server" CssClass="DescLabel"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 139px" valign="top">
            <span class="StandardLabel">Expiration Date :</span></td>
        <td valign="top">
                        <asp:Label ID="lblExpirationDate" runat="server" CssClass="DescLabel"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 139px" valign="top">
            <span class="StandardLabel">Description :</span>
        </td>
        <td valign="top">
            <asp:Label ID="lblDescription" runat="server" CssClass="DescLabelWhiteBackground"
                Font-Size="X-Small" Height="100px" Width="325px"></asp:Label><br />
            &nbsp;</td>
    </tr>
    <tr id="trNavigationButton" runat=server visible=false>
	    <td align=left style="height: 22px"><asp:ImageButton id="btnBack" runat="server" ImageUrl="~/images/btnBack.gif" AlternateText="Back to vendor selection" CausesValidation="False" OnClick="btnBack_Click"></asp:ImageButton></td>
	    <td align=right style="height: 22px"><asp:ImageButton id="btnConfirm" runat="server" ImageUrl="~/images/btnConfirm.gif" AlternateText="Go to validation" OnClick="btnConfirm_Click"></asp:ImageButton></td>
	</tr>
</table>

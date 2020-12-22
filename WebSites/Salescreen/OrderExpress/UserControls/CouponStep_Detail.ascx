<%@ Control Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.UserControls.CouponStep_Detail" Codebehind="CouponStep_Detail.ascx.cs" %>
<%@ Register Src="CouponHeaderForm.ascx" TagName="CouponHeaderForm" TagPrefix="uc1" %>
<%@ Register Src="CouponStep_Validation.ascx" TagName="CouponStep_Validation" TagPrefix="uc2" %>

<link href="Styles.css" rel="stylesheet" type="text/css" />
<table>
    <tr runat="server" id="tr_step2">
        <td colspan=2>
            <uc1:CouponHeaderForm ID="ctrl_couponHeaderForm" runat="server" />
        </td>
    </tr>
    <tr runat="server" id="tr_step3">
        <td colspan=2>
            <uc2:CouponStep_Validation ID="ctrlCouponStep_Validation" runat="server" />
        </td>
    </tr>

   <tr id="trNavigationButton" runat=server>
	    <td align=left style="height: 22px"><asp:ImageButton id="btnBack" runat="server" ImageUrl="~/images/btnBack.gif" AlternateText="Back to vendor selection" CausesValidation="False" OnClick="btnBack_Click"></asp:ImageButton></td>
	    <td align=right style="height: 22px"><asp:ImageButton id="btnNext" runat="server" ImageUrl="~/images/BtnNext.gif" AlternateText="Go to validation" OnClick="btnNext_Click"></asp:ImageButton></td>
	</tr>
</table>

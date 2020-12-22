<%@ Register TagPrefix="uc1" TagName="Promo_CouponHeaderForm" Src="Promo_CouponHeaderForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Promo_CouponDetail" Codebehind="Promo_CouponDetail.ascx.cs" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD colspan=2>
			<uc1:Promo_CouponHeaderForm id="ctrlPromo_CouponHeaderForm" runat="server"></uc1:Promo_CouponHeaderForm><br>
		</TD>
	</TR>
	<tr id="trEditButton" runat=server>
		<td align="center" colspan=2>
			<table border="0" cellpadding="0" cellspacing="0" width="400" id="Table1">
				<tr>
					<td align="center" style="width: 144px">
						<asp:ImageButton id="imgBtnDelete" runat="server" CausesValidation="False" ImageUrl="~/images/btnDelete.gif"
							AlternateText="Delete"></asp:ImageButton>
					</td>
					<td align="center">
						<asp:ImageButton id="imgBtnSave" runat="server" ImageUrl="~/images/btnSave.gif" AlternateText="Save"></asp:ImageButton>
					</td>
					<td align="center">
						<asp:HyperLink id="hypLnkCancel" runat="server" ImageUrl="~/images/btnCancel.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr id="trNavigationButton" runat=server visible=false>
	    <td align=left style="height: 22px"><asp:ImageButton id="btnBack" runat="server" ImageUrl="~/images/btnBack.gif" AlternateText="Back to vendor selection" CausesValidation="False"></asp:ImageButton></td>
	    <td align=right style="height: 22px"><asp:ImageButton id="btnNext" runat="server" ImageUrl="~/images/btnNext.gif" AlternateText="Go to validation"></asp:ImageButton></td>
	</tr>
</TABLE>

<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Promo_CouponDetailInfo" Codebehind="Promo_CouponDetailInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Promo_CouponInfo" Src="Promo_CouponInfo.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD><asp:label id="lblError" Font-Bold="True" ForeColor="Red" Runat="server"></asp:label><br>
			<uc1:Promo_CouponInfo id="ctrlPromo_CouponInfo" runat="server"></uc1:Promo_CouponInfo><br>
		</TD>
	</TR>
	<TR>
		<td align="center">
			<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td align="center"><asp:imagebutton id="imgBtnEdit" runat="server" CausesValidation="False" ImageUrl="~/images/btnEdit.gif"
							AlternateText="Edit"></asp:imagebutton></td>
					<td>&nbsp;&nbsp;
					</td>
					<td align="center"><asp:hyperlink id="hypLnkClose" runat="server" ImageUrl="~/images/btnClose.gif" NavigateUrl="javascript:window.close();">Cancel</asp:hyperlink></td>
				</tr>
			</table>
		</td>
	</TR>
</TABLE>

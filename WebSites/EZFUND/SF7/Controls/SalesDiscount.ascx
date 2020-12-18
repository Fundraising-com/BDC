<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SalesDiscount.ascx.vb" Inherits="StoreFront.StoreFront.SalesDiscount" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" width="100%" runat="server">
	<TR>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="ContentTableHeader">&nbsp;</TD>
		<TD class="ContentTableHeader" noWrap>Sales and Discounts</TD>
		<TD class="ContentTableHeader">&nbsp;</TD>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="Content" colspan="3">&nbsp;</TD>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<TD class="Content">
			<asp:Label id="lblSpecialOffer" runat="server"></asp:Label></TD>
		<TD class="Content">&nbsp;</TD>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<TD class="Content" noWrap>
			Coupon&nbsp;Code:
			<asp:TextBox id="txtPromotionCode" runat="server" MaxLength="100"></asp:TextBox></TD>
		<TD class="Content">&nbsp;</TD>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<TD class="Content button" align="right">
			<asp:LinkButton ID="btnApply" Runat="server">
				<asp:Image BorderWidth="0" ID="imgApply" Runat="server" AlternateText="Apply"></asp:Image>
			</asp:LinkButton>
		</TD>
		<TD class="Content">&nbsp;</TD>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR id="Spacer1">
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<TD class="Content" align="right">&nbsp;</TD>
		<TD class="Content">&nbsp;</TD>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR id="BarSpacer">
		<td class="ContentTableHorizontal" colspan="5" height="1"><img src="images/clear.gif" height="1"></td>
	</TR>
	<TR id="Spacer3">
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<TD class="Content" align="right">&nbsp;</TD>
		<TD class="Content">&nbsp;</TD>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR id="Label">
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<TD class="Headings" align="left">Applied Coupons</TD>
		<TD class="Content">&nbsp;</TD>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR id="Spacer2">
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<TD class="Content" align="right">&nbsp;</TD>
		<TD class="Content">&nbsp;</TD>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR id="CouponList">
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<TD class="Content" align="left">
			<asp:DataList id="DataList1" runat="server" Width="100%">
				<ItemTemplate>
					<table runat="server" id="CouponTable" border="0" cellpadding="2" cellspacing="0" width="100%">
						<tr>
							<td width="100%" class="Content" align="left">
								<asp:Label ID="DiscountName" Runat="server" CssClass="Content">
									<%# DataBinder.Eval(Container.DataItem,"Description") %>
								</asp:Label>
							</td>
<%-- begin: JDB - Advanced Coupon Functionality --%>
							<td class="Content" align="right"><%#Format(Me.m_objXMLCart.GetDiscountTotalByDiscount(DataBinder.Eval(Container.DataItem,"ID")), "c")%></td>
						</tr>
						<tr>
							<td class="Content">&nbsp;</td>
<%-- end: JDB - Advanced Coupon Functionality --%>
							<td class="Content" align="right">
								<asp:LinkButton ID="btnCouponRemove" Runat="server" OnClick="RemoveCoupon" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'>
									<asp:Image BorderWidth="0" ID="imgCouponRemove" Runat="server" AlternateText="Remove"></asp:Image>
								</asp:LinkButton>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:DataList></TD>
		<TD class="Content">&nbsp;</TD>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="Content" colspan="3">&nbsp;</TD>
		<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" colspan="5" height="1"><img src="images/clear.gif" height="1"></td>
	</TR>
</TABLE>

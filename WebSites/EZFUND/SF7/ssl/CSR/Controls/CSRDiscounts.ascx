<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRDiscounts.ascx.vb" Inherits="StoreFront.StoreFront.CSRDiscounts" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<br>
<br>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="MainTableHeader">&nbsp;</TD>
		<TD class="MainTableHeader" noWrap>Order Summary and Discounts</TD>
		<TD class="MainTableHeader">&nbsp;</TD>
		<TD class="MainTableHeader">&nbsp;</TD>
		<TD class="MainTableHeader">&nbsp;</TD>
		<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content" colSpan="5">&nbsp;</TD>
		<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content" vAlign="top" colSpan="5">
			<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
				runat="server">
				<tr>
					<td class="Content" width="5">&nbsp;</td>
					<td class="Content" width="1"><IMG src="images/clear.gif" width="1"></td>
					<td class="content" width="50%"><cc1:totaldisplay id="TotalDisplay1" runat="server" SubTotalLabel="Subtotal:" ShippingTotalLabel="Shipping:"
							HandlingTotalLabel="Handling:" HorizontalBorderStyle="ContentTableHorizontal" TableBorderStyle="ContentTable" GrandTotalStyle="Headings"
							HeadingClass="ContentTableHeader" DisplayPaymentMethod="False" DisplayTaxShipNotIncluded="False" DisplayShipmentTotal="False"
							DisplayOrderTotal="false" DisplaySubHandlingTotal="False"></cc1:totaldisplay></td>
					<td class="content" width="1">&nbsp;</td>
					<td class="content" vAlign="top" width="50%">
						<asp:datalist id="Coupons" Width="100%" CellPadding="0" CellSpacing="0" Runat="server">
							<HeaderTemplate>
								<TR>
									<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
									<TD class="ContentTableHeader" noWrap width="70%">Coupons Applied</TD>
									<TD class="ContentTableHeader" noWrap width="20%"></TD>
									<TD class="ContentTableHeader" width="10%">&nbsp;</TD>
									<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
								</TR>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
									<td width="70%" class="content">
										<asp:Label ID="DiscountName" Runat="server" CssClass="Content">
											<%# DataBinder.Eval(Container.DataItem,"Description") %>
										</asp:Label>
									</td>
									<td width="20%" class="content">
									</td>
									<td width="10%" align="right">
										<asp:LinkButton ID="btnCouponRemove" Runat="server" OnClick="RemoveCoupon" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'>
											<asp:Image BorderWidth="0" ImageUrl="../images/remove.jpg" ID="imgCouponRemove" Runat="server" 
 AlternateText="Remove"></asp:Image>
										</asp:LinkButton>
									<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
					</td>
				</tr>
				<TR>
					<td class="ContentTable" colspan="5" width="1"></td>
				</TR>
				</ItemTemplate> </asp:datalist><asp:datalist CellPadding="0" CellSpacing="0" id="GiftCertificates" runat="server" Width="100%">
					<HeaderTemplate>
						<TR>
							<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
							<TD class="ContentTableHeader" noWrap width="70%">Gift Certificates Applied</TD>
							<TD class="ContentTableHeader" noWrap width="20%">Amount</TD>
							<TD class="ContentTableHeader" width="10%">&nbsp;</TD>
							<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
						</TR>
					</HeaderTemplate>
					<ItemTemplate>
						<tr>
							<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
							<td width="70%" class="Content">
								<%# DataBinder.Eval(Container.DataItem,"Code") %>
							</td>
							<td width="20%" class="Content">
								<%# PriceDisplay2(DataBinder.Eval(Container.DataItem,"DollarOff")) %>
							</td>
							<td width="50%" align="right">
								<asp:LinkButton ID="GiftCertificateRemove" Runat="server" OnClick="RemoveGiftCertificate" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'>
									<asp:Image BorderWidth="0" ImageUrl="../images/remove.jpg" ID="imgGiftCertificateRemove" Runat="server" 
 AlternateText="Remove"></asp:Image>
								</asp:LinkButton>
							</td>
							<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
						</tr>
						<tr>
							<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
							<td colspan="3" class="Content">
								<%# PriceDisplay2(DataBinder.Eval(Container.DataItem,"AmountUsed"))%>
								Applied To Order,
								<%# PriceDisplay2(DataBinder.Eval(Container.DataItem,"DollarOff") - DataBinder.Eval(Container.DataItem,"AmountUsed")) %>
								Remaining
							</td>
							<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
						</tr>
						<TR>
							<td class="ContentTable" width="1" colspan="5"><img src="images/clear.gif" width="1"></td>
						</TR>
					</ItemTemplate>
				</asp:datalist>
		</td>
		<td class="content" width="1">&nbsp;</td>
	</tr>
</TABLE>
</TD>
<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
</TR>
<TR>
	<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	<td class="Content" width="1" colSpan="5">&nbsp;</td>
	<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
</TR>
<TR>
	<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	<td class="Content" width="1">&nbsp;</td>
	<td class="Content" width="100%" colSpan="3">
		<TABLE id="Table4" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
			runat="server">
			<TR>
				<td class="ContentTable"></td>
				<TD class="ContentTableHeader" colSpan="4">Add Discounts to Order</TD>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</TR>
			<TR>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<TD class="Content">&nbsp;</TD>
				<TD class="Content" noWrap>Coupon&nbsp;Code:&nbsp;
				</TD>
				<TD class="Content" noWrap><asp:textbox id="txtPromotionCode" runat="server" MaxLength="100"></asp:textbox></TD>
				<TD class="Content"><asp:linkbutton id="ApplyCoupon" Runat="server">
						<asp:Image BorderWidth="0" ImageUrl="../images/apply.jpg" ID="imgApply" Runat="server" AlternateText="Apply"></asp:Image>
					</asp:linkbutton></TD>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</TR>
			<TR>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<TD class="Content" colSpan="4">&nbsp;</TD>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</TR>
			<TR>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<TD class="Content">&nbsp;</TD>
				<TD class="Content" noWrap>Gift&nbsp;Certificate&nbsp;Code:&nbsp;
				</TD>
				<TD class="Content" noWrap><asp:textbox id="GiftCertificateCode" runat="server" MaxLength="100"></asp:textbox></TD>
				<TD class="Content"><asp:linkbutton id="ApplyGiftCertificate" Runat="server">
						<asp:Image BorderWidth="0" ImageUrl="../images/apply.jpg" ID="Image1" Runat="server" AlternateText="Apply"></asp:Image>
					</asp:linkbutton></TD>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</TR>
			<TR>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<TD class="Content" colSpan="4">&nbsp;</TD>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</TR>
			<TR id="OtherDiscount" runat="server">
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<TD class="Content">&nbsp;</TD>
				<TD class="Content" noWrap>Other&nbsp;Discount&nbsp;Name:&nbsp;
				</TD>
				<TD class="Content" noWrap><asp:textbox id="OtherDiscountName" runat="server" MaxLength="100"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Amount:&nbsp;<asp:textbox id="OtherDiscountAmount" runat="server" MaxLength="100"></asp:textbox></TD>
				<TD class="Content"><asp:linkbutton id="ApplyOtherDiscount" Runat="server">
						<asp:Image BorderWidth="0" ImageUrl="../images/apply.jpg" ID="Image2" Runat="server" AlternateText="Apply"></asp:Image>
					</asp:linkbutton></TD>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</TR>
			<TR>
				<td class="ContentTable" width="1" colSpan="6" height="1"></td>
			</TR>
		</TABLE>
	</td>
	<td class="Content" width="1"><IMG src="images/clear.gif" width="1"></td>
	<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
</TR>
<TR>
	<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	<TD class="Content" colSpan="5">&nbsp;</TD>
	<td class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
</TR>
<TR>
	<td class="MainTableHeader" width="1" colSpan="7" height="1"></td>
</TR>
</TABLE>
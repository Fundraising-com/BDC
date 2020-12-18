<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="AddressLabel.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="COrderDetailControl.ascx.vb" Inherits="StoreFront.StoreFront.COrderDetailControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="ContentTableHeader">&nbsp;</TD>
		<TD class="ContentTableHeader" colSpan="3">Billing Information</TD>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content" colSpan="4">&nbsp;</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<TD class="Content" vAlign="top" width="50%"><uc1:addresslabel id="BillingAddress" runat="server"></uc1:addresslabel></TD>
		<TD><IMG height="1" src="images/clear.gif" width="10"></TD>
		<TD class="Content" vAlign="top" width="50%">
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td class="Content">
						<asp:Label id="lblPaymentMethod" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td class="Content">&nbsp;</td>
				</tr>
				<tr>
					<td class="Content">Payment Information</td>
				</tr>
			</table>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content" colSpan="4">&nbsp;</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
		<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
		<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
	</TR>
</TABLE>
<br>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3">
			<asp:datalist id="Datalist2" runat="server" Width="100%">
				<FooterTemplate>
					<table border="0" cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td class="ContentTable" height="1"><IMG src="images/clear.gif" width="1" height="1"></td>
						</tr>
					</table>
				</FooterTemplate>
				<ItemTemplate>
					<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR>
							<TD vAlign="top" width="50%">
								<TABLE id="Table10" cellSpacing="0" cellPadding="0" width="100%" border="0">
									<TR>
										<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
										<TD class="ContentTableHeader">&nbsp;</TD>
										<TD class="ContentTableHeader">Ship To:
											<asp:Label id="Label2" runat="server" CssClass="ContentTableHeader">
												<%# DataBinder.Eval(Container.DataItem.Address,"NickName") %>
											</asp:Label></TD>
										<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
									</TR>
									<TR>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
										<TD class="Content">&nbsp;</TD>
										<TD class="Content">&nbsp;</TD>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
									</TR>
									<TR>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
										<TD class="Content">&nbsp;</TD>
										<TD class="Content">
											<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD class="Content">
														<asp:TextBox id=txtShipID Runat="server" Text='<%# DataBinder.Eval(Container.DataItem.Address,"ID") %>' Visible="False">
														</asp:TextBox>
														<uc1:AddressLabel id=Addresslabel1 runat="server" AddressSource='<%# DataBinder.Eval(Container.DataItem,"Address") %>'>
														</uc1:AddressLabel></TD>
												</TR>
												<TR>
													<TD class="Content">&nbsp;</TD>
												</TR>
												<TR>
													<TD class="Content" noWrap>
														<asp:Label id="ShipMethod" Runat="server"></asp:Label></TD>
												</TR>
											</TABLE>
										</TD>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
									</TR>
									<TR>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
										<TD class="Content">&nbsp;</TD>
										<TD class="Content">&nbsp;</TD>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
									</TR>
									<TR>
										<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
										<TD class="ContentTable" style="HEIGHT: 1px" colSpan="2"><IMG height="1" src="images/clear.gif" width="1"></TD>
										<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
									</TR>
								</TABLE>
								<BR>
								<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
									<TR>
										<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
										<TD class="ContentTableHeader">&nbsp;</TD>
										<TD class="ContentTableHeader">Special Instructions</TD>
										<TD class="ContentTableHeader">&nbsp;</TD>
										<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
									</TR>
									<TR>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
										<TD class="Content" colSpan="3">&nbsp;</TD>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
									</TR>
									<TR>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
										<TD class="Content">&nbsp;</TD>
										<TD class="Content" align="left">
											<asp:Label id="lblSpecialInstruction" Runat="server">
												<%# DataBinder.Eval(Container.DataItem.Address,"Instructions") %>
											</asp:Label></TD>
										<TD class="Content">&nbsp;</TD>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
									</TR>
									<TR>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
										<TD class="Content" colSpan="3">&nbsp;</TD>
										<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
									</TR>
									<TR>
										<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
										<TD class="ContentTable" style="HEIGHT: 1px" colSpan="2"><IMG height="1" src="images/clear.gif" width="1"></TD>
										<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
									</TR>
								</TABLE>
								<BR>
							</TD>
							<TD><IMG height="1" src="images/clear.gif" width="10"></TD>
							<TD vAlign="top" width="50%">
								<cc1:DynamicCartDisplay id=Dynamiccartdisplay2 runat="server" Width="100%" HeadingClass="ContentTableHeader" DataSource='<%# DataBinder.Eval(Container.DataItem,"OrderItems") %>' GiftWrapDetail="True" DisplayGiftWrapRow="True" TotalColumnDisplay="False" OptionsColumnDisplay="False" ReOrderBtnDisplay="True" StatusColumnDisplay="False" DesignCount="2" BorderClass="ContentTable" HorizontalClass="ContentTableHorizontal" BuyNowImg="buynow.gif" GiftWrapImg="giftwrap.gif" OptionsLabel="Options" PriceLabel="Price" ProductLabel="Product" QuantityLabel="Qty" RemoveImg="remove.gif" ReOrderImg="reorder.gif" SavedCartImg="wishlist.gif" StatusLabel="Status" TotalLabel="Total">
								</cc1:DynamicCartDisplay><BR>
								<BR>
								<cc1:TotalDisplay id="ShipmentTotalDisplay1" runat="server" HandlingTotalLabel="Handling:" ShippingTotalLabel="Shipping:"
									SubTotalLabel="Subtotal:" HorizontalBorderStyle="ContentTableHorizontal" DisplayPaymentMethod="False"
									TableBorderStyle="ContentTable" HeadingClass="ContentTableHeader" DisplayOrderTotal="False" DisplayTaxShipNotIncluded="False"
									HeadingString="Shipment Total" ShipmentTotalLabel="Shipment Total:" ShipmentTotalStyle="Headings"
									DisplayGrandTotal="False" DisplayGiftCertificateTotal="False"></cc1:TotalDisplay></TD>
						</TR>
					</TABLE>
				</ItemTemplate>
				<SeparatorTemplate>
					<table border="0" cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td class="ContentTable" height="1"><IMG src="images/clear.gif" width="1" height="1"></td>
						</tr>
					</table>
				</SeparatorTemplate>
			</asp:datalist>
		</td>
	</tr>
	<tr>
		<td class="Content" colSpan="3">&nbsp;</td>
	</tr>
	<tr>
		<td class="Content" width="50%">&nbsp;</td>
		<td class="Content"><IMG height="1" src="images/clear.gif" width="10"></td>
		<td class="Content" align="right" width="50%"><cc1:totaldisplay id="TotalDisplay1" runat="server" DisplayTaxShipNotIncluded="False" DisplayOrderTotal="False"
				DisplayCountryTaxTotal="False" DisplayDiscountTotal="False" DisplayHandlingTotal="False" DisplayLocalTaxTotal="False" DisplayMerchandiseTotal="False"
				DisplayShippingTotal="False" DisplayStateTaxTotal="False" DisplaySubTotal="False" GrandTotalStyle="Headings" HeadingClass="ContentTableHeader"
				TableBorderStyle="ContentTable" DisplayPaymentMethod="False" HorizontalBorderStyle="ContentTableHorizontal" SubTotalLabel="Subtotal:" ShippingTotalLabel="Shipping:"
				HandlingTotalLabel="Handling:"></cc1:totaldisplay></td>
	</tr>
</table>

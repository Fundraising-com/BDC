<%@ Register TagPrefix="uc1" TagName="Swatch" Src="Swatch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="VolumePricing" Src="VolumePricing.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductDetail2.ascx.vb" Inherits="StoreFront.StoreFront.ProductDetail2" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="CAttributeControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CInventoryControl" Src="CInventoryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProductBundleDetail" src="ProductBundleDetail.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RelatedProductControl" Src="RelatedProductControl.ascx" %>

<TABLE id="Table1" runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0" class="Content detail2">
	<TR>
		<td id="ImageCell"><uc1:swatch id="Swatches" runat="server"></uc1:swatch></td>
		<TD width="100%"><TABLE id="Table2" runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0" class="Content options">
				<TR id="trProductName">
					<TD colspan="10"><h1 class="Headings"><asp:label id="lblProductName" Runat="server">Product
							Name:&nbsp;</asp:label>
						<%# DataBinder.Eval(Product,"Name") %></h1></TD>
				</TR>
				<TR id="trProductCode">
					<TD colspan="10"><asp:label id="lblProductCode" Runat="server">Product
							ID:&nbsp;</asp:label>
						<%# DataBinder.Eval(Product,"ProductCode") %></TD>
				</TR>
				<TR id="trCategorySpacer" class="hide">
					<TD colspan="10"></TD>
				</TR>
				<TR id="trCategory">
					<TD colspan="10"><asp:label id="lblCategory" Runat="server">Category(s):&nbsp;<BR>
						</asp:label>
						<%# DataBinder.Eval(Product, "CategoryNames") %></TD>
				</TR>
				<TR id="trVendorManuSpacer" class="hide">
					<TD colspan="10"></TD>
				</TR>
				<TR id="trVendor">
					<TD colspan="10"><asp:label id="lblVendor" Runat="server">Vendor:&nbsp;</asp:label>
						<%# DataBinder.Eval(Product,"Vendor") %></TD>
				</TR>
				<TR id="trManufacturer">
					<TD colspan="10"><asp:label id="lblManufacturer" Runat="server">Manufacturer:&nbsp;</asp:label>
						<%# DataBinder.Eval(Product,"Manufacturer") %></TD>
				</TR>
				<TR id="trPriceSpacer" class="hide">
					<TD colspan="10"></TD>
				</TR>
				<TR id="trPrice">
					<TD colspan="10"><asp:label id="lblPrice" Runat="server">Price:&nbsp;</asp:label>
						<%# PriceDisplay(DataBinder.Eval(Product, "Price")) %></TD>
				</TR>
				<TR id="trSalePrice">
					<TD colspan="10"><asp:label id="lblSalePrice" Runat="server">Sale
							Price:&nbsp;</asp:label>
						<%# Format(DataBinder.Eval(Product,"SalePrice"), "c") %></TD>
				</TR>
				<tr id="trCustomPrice">
					<td id="lblYourPrice" nowrap>Your
						Price: </td>
					<td id="lblSalePriceDisplay" class="Content"></td>
					<td id="CustomPriceCell"></td>
				</tr>
				<!-- Verisign Recurring Billing -->
				<tr id="trSubscriptionPrice">
					<td colspan="10"><asp:label id="lblSubscriptionPrice" Runat="server"></asp:label>
						<%# PriceDisplay(DataBinder.Eval(Product, "Price"))%></td>
				</tr>
				<tr id="trRecurringPrice">
					<td colspan="10"><asp:label id="lblRecurringPrice" Runat="server"></asp:label>
						<%# PriceDisplay(DataBinder.Eval(Product, "RecurringSubscriptionPrice"))%>&nbsp;<%# GetPaymentPeriodName(DataBinder.Eval(Product, "PaymentPeriod")) %></td>
				</tr>
				<!-- Verisign Recurring Billing -->
				<tr id="trSavings">
					<td id="lblSavings" nowrap></td>
				</tr>
				<TR id="trVolumePricing">
					<TD colspan="10"><asp:LinkButton id="btnVolumePricing"  CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'  onclick="LinkButton_Click" Runat="server"> </asp:LinkButton>
					</TD>
				</TR>
				<TR id="trStockVolumeSpacer" class="hide">
					<TD colspan="10"></TD>
				</TR>
				<TR id="trVolumePricing3">
					<TD id="VolumePriceGrid" colspan="10"><uc1:volumepricing id="Volumepricing1" runat="server" ProdID='<%#DataBinder.Eval(Product,"ProductID") %>'> </uc1:volumepricing> </TD>
				</TR>
				<TR id="trStockInfo">
					<TD colspan="10"><asp:linkbutton id="StockInfo" onclick="StockButton_Click" Runat="server" CommandName='<%#DataBinder.Eval(Product,"ProductID") %>' Text="Stock Status"> </asp:linkbutton></TD>
				</TR>
				<TR id="trStockStatus">
					<TD colspan="10"><uc1:cinventorycontrol id="CInventoryControl1" runat="server"></uc1:cinventorycontrol></TD>
				</TR>
				<TR id="trAttributesSpacer" class="hide">
					<TD colspan="10"></TD>
				</TR>
				<TR id="trAttributes">
					<TD colspan="10"><uc1:cattributecontrol id="CAttributeControl1" runat="server"></uc1:cattributecontrol></TD>
				</TR>
				<TR id="trQty">
					<TD colspan="10">Qty:
						<asp:textbox id="txtQty" MaxLength="10" runat="server" Columns="2"></asp:textbox></TD>
				</TR>
				<%-- BEGIN: GJV - 8/23/2007 - OSP merge --%>
				<TR id="trAddToCart">
					<%-- END: GJV - 8/23/2007 - OSP merge --%>
					<TD colspan="10" class="button"><asp:LinkButton ID="btnAddToCart" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'>
							<asp:Image BorderWidth="0" ID="imgAddToCart" runat="server" AlternateText="Add To Cart"></asp:Image>
						</asp:LinkButton>
					</TD>
				</TR>
				<TR id="trSavedCart">
					<TD colspan="10" class="button"><asp:LinkButton ID="btnAddToSavedCart" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'>
							<asp:Image BorderWidth="0" ID="imgAddToSavedCart" runat="server" AlternateText="Add To Saved Cart"></asp:Image>
						</asp:LinkButton>
					</TD>
				</TR>
				<TR id="trEMailFriend">
					<TD colspan="10" class="button"><asp:LinkButton ID="btnEMailFriend" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'>
							<asp:Image BorderWidth="0" ID="imgEMailFriend" runat="server" AlternateText="Email Friend"></asp:Image>
						</asp:LinkButton>
					</TD>
				</TR>
				<TR id="trVolumePricingSpacer" class="hide">
					<TD colspan="10"></TD>
				</TR>
			</TABLE></TD>
	</TR>
	<TR id="trDescriptionSpacer" class="hide">
		<TD colspan="10"></TD>
	</TR>
	<tr>
	    <TD colspan="10">
	        <uc1:ProductBundleDetail ID="ProductBundleDetail1" runat="Server" Visible="False"></uc1:ProductBundleDetail>	  
	    </TD>
	</tr>
	<TR id="trDescription">
        <TD colspan="10">
			<h2><asp:label id="lblDescription" Runat="server">Description:&nbsp;</asp:label></h2>
			<%# DataBinder.Eval(Me, "Description") %>
		</TD>
	</TR>
</TABLE>

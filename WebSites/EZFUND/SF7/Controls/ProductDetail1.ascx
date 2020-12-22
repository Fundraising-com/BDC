<%@ Register TagPrefix="uc1" TagName="Swatch" Src="Swatch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="CAttributeControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductDetail1.ascx.vb" Inherits="StoreFront.StoreFront.ProductDetail1" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="VolumePricing" Src="VolumePricing.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CInventoryControl" Src="CInventoryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProductBundleDetail" src="ProductBundleDetail.ascx" %>

<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR id="trProductName">
		<TD colSpan="10"><h1>
				<asp:label id="lblProductName" Runat="server">Product
					Name:&nbsp;</asp:label>
				<%# DataBinder.Eval(Product,"Name") %></h1></TD>
	</TR>
	<TR>
		<TD class="Content col-1"><uc1:Swatch id="Swatches" runat="server"></uc1:Swatch> </TD>
		<TD class="Content col-2" vAlign="top" width="100%"><TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<TR id="trProductCode">
					<TD class="Content" colSpan="10"><asp:label id="lblProductCode" Runat="server">Product
							ID:&nbsp;</asp:label>
						<%# DataBinder.Eval(Product,"ProductCode") %></TD>
				</TR>
				<TR id="trVendorManuSpacer" class="hide">
					<TD class="Content" colSpan="10"></TD>
				</TR>
				<TR id="trVendor">
					<TD class="Content" colSpan="10"><asp:label id="lblVendor" Runat="server">Vendor:&nbsp;</asp:label>
						<%# DataBinder.Eval(Product,"Vendor") %></TD>
				</TR>
				<TR id="trManufacturer">
					<TD class="Content" colSpan="10"><asp:label id="lblManufacturer" Runat="server">Manufacturer:&nbsp;</asp:label>
						<%# DataBinder.Eval(Product,"Manufacturer") %></TD>
				</TR>
				<TR id="trCategory">
					<TD class="Content" colSpan="3" height="22"><asp:label id="lblCategory" Runat="server">Category(s):&nbsp;<BR>
						</asp:label>
						<%# DataBinder.Eval(Product, "CategoryNames") %></TD>
				</TR>
				<TR id="trPriceSpacer" class="hide">
					<TD class="Content" colSpan="10"></TD>
				</TR>
				<TR id="trPrice">
					<TD class="Content" colSpan="10"><asp:label id="lblPrice" Runat="server">Price:&nbsp;</asp:label>
						<%# PriceDisplay(DataBinder.Eval(Product, "Price")) %></TD>
				</TR>
				<TR id="trSalePrice">
					<TD class="Content" colSpan="10"><asp:label id="lblSalePrice" Runat="server">Sale
							Price:&nbsp;</asp:label>
						<%# Format(DataBinder.Eval(Product,"SalePrice"), "c") %></TD>
				</TR>
				<tr id="trCustomPrice">
					<td class="Content" id="lblYourPrice" noWrap>Your
						Price: </td>
					<td class="Content" id="lblSalePriceDisplay" noWrap></td>
					<td class="Content" id="CustomPriceCell" noWrap align="left"></td>
				</tr>
				<!-- Verisign Recurring Billing -->
				<tr id="trSubscriptionPrice">
					<td class="Content" colSpan="10"><asp:label id="lblSubscriptionPrice" Runat="server"></asp:label>
						<%# PriceDisplay(DataBinder.Eval(Product, "Price"))%></td>
				</tr>
				<tr id="trRecurringPrice">
					<td class="Content" colSpan="10"><asp:label id="lblRecurringPrice" Runat="server"></asp:label>
						<%# PriceDisplay(DataBinder.Eval(Product, "RecurringSubscriptionPrice"))%>&nbsp;
						<%GetPaymentPeriodName(DataBinder.Eval(Product, "PaymentPeriod")) %></td>
				</tr>
				<!-- Verisign Recurring Billing -->
				<tr id="trSavings">
					<td class="Content" id="lblSavings" colSpan="10"></td>
				</tr>
				<TR id="trVolumePricing">
					<TD class="Content" vAlign="top" colSpan="10"><asp:linkbutton id=btnVolumePricing onclick=LinkButton_Click Runat="server" CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'> </asp:linkbutton></TD>
				</TR>
				<TR id="trStockVolumeSpacer" class="hide">
					<TD class="Content" colSpan="10"></TD>
				</TR>
				<TR class="Content" id="trVolumePricing3">
					<TD class="Content" id="VolumePriceGrid" colSpan="10"><uc1:volumepricing id=Volumepricing1 runat="server" ProdID='<%#DataBinder.Eval(Product,"ProductID") %>'> </uc1:volumepricing></TD>
				</TR>
				<TR>
					<TD class="Content" colSpan="10"></TD>
				</TR>
				<TR id="trStockInfo">
					<TD class="Content" vAlign="top" colSpan="10"><asp:linkbutton id=StockInfo onclick=StockButton_Click Runat="server" CommandName='<%#DataBinder.Eval(Product,"ProductID") %>' Text="Stock Status"></asp:linkbutton></TD>
				</TR>
				<TR id="trStockStatus">
					<TD class="Content" colSpan="10"><uc1:cinventorycontrol id="CInventoryControl1" runat="server"></uc1:cinventorycontrol></TD>
				</TR>
				<TR id="trEMailFriendSpacer" class="hide">
					<TD class="Content" colSpan="10"></TD>
				</TR>
				<TR id="trEMailFriend">
					<TD class="Content button" colSpan="10"><asp:linkbutton id=btnEMailFriend onclick=AddCart Runat="server" CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'>
							<asp:Image BorderWidth="0" ID="imgEMailFriend" runat="server" AlternateText="Email Friend"></asp:Image>
						</asp:linkbutton></TD>
				</TR>
			</TABLE></TD>
		<TD class="Content col-3">
<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server" class="Content optionsbox">
				<%-- BEGIN: GJV - 8/23/2007 - OSP merge --%>
				<TR id="trOptions">
					<%-- END: GJV - 8/23/2007 - OSP merge --%>
					<TD class="ContentTableHeader" noWrap>Order Options:</TD>
				</TR>
				<TR id="trAttributesSpacer" class="hide">
					<TD></TD>
				</TR>
				<TR id="trAttributes">
					<TD><uc1:cattributecontrol id="CAttributeControl1" runat="server"></uc1:cattributecontrol></TD>
				</TR>
				<TR id="trQty">
					<TD><asp:label id="lblQty" Runat="server">Qty:</asp:label>
						<asp:textbox id="txtQty" runat="server" Columns="2" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR id="trAddToCart">
					<TD><asp:linkbutton id=btnAddToCart onclick=AddCart Runat="server" CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'>
							<asp:Image BorderWidth="0" ID="imgAddToCart" runat="server" AlternateText="Add To Cart"></asp:Image>
						</asp:linkbutton></TD>
				</TR>
				<TR id="trSavedCart">
					<TD><asp:linkbutton id=btnAddToSavedCart onclick=AddCart Runat="server" CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'>
							<asp:Image BorderWidth="0" ID="imgAddToSavedCart" runat="server" AlternateText="Add To Saved Cart"></asp:Image>
						</asp:linkbutton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
	  <TD class="Content" colSpan="3">
		<uc1:ProductBundleDetail ID="ProductBundleDetail1" runat="Server" Visible="False"></uc1:ProductBundleDetail>	  	
	  </TD>
  </TR>
	<TR id="trDescription">
		<TD class="Content" colSpan="3"><h2>
				<asp:label id="lblDescription" Runat="server">Description:&nbsp;</asp:label>
			</h2>
			<%# DataBinder.Eval(Me, "Description") %></TD>
	</TR>
	<%-- Tee 10/22/2007 related product --%>
	<%-- end Tee --%>
</TABLE>

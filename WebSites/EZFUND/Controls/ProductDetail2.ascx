<%@ Register TagPrefix="uc1" TagName="VolumePricing" Src="VolumePricing.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductDetail2.ascx.vb" Inherits="StoreFront.StoreFront.ProductDetail2" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="CAttributeControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CInventoryControl" Src="CInventoryControl.ascx" %>
<TABLE id="Table1" runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<td class="Content" id="ImageCell" vAlign="top"><asp:panel id="ProductImage" Runat="server"><IMG 
      src='<%# DataBinder.Eval(Me,"ImageString") %>'></asp:panel>
		</td>
		<TD class="Content">&nbsp;</TD>
		<TD class="Content" vAlign="top" width="100%">
			<TABLE id="Table2" runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR id="trProductName">
					<TD class="Headings" noWrap><asp:label id="lblProductName" Runat="server">Product 
            Name:&nbsp;</asp:label>
						<%# DataBinder.Eval(Product,"Name") %>
					</TD>
				</TR>
				<TR id="trProductCode">
					<TD class="Content" noWrap><asp:label id="lblProductCode" Runat="server">Product 
            ID:&nbsp;</asp:label><%# DataBinder.Eval(Product,"ProductCode") %></TD>
				</TR>
				<TR id="trCategorySpacer">
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR id="trCategory">
					<TD class="Content"><asp:label id="lblCategory" Runat="server">Category(s):&nbsp;<BR></asp:label><%# DataBinder.Eval(Product, "CategoryNames") %></TD>
				</TR>
				<TR id="trVendorManuSpacer">
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR id="trVendor">
					<TD class="Content" noWrap><asp:label id="lblVendor" Runat="server">Vendor:&nbsp;</asp:label><%# DataBinder.Eval(Product,"Vendor") %></TD>
				</TR>
				<TR id="trManufacturer">
					<TD class="Content" noWrap><asp:label id="lblManufacturer" Runat="server">Manufacturer:&nbsp;</asp:label><%# DataBinder.Eval(Product,"Manufacturer") %></TD>
				</TR>
				<TR id="trDescriptionSpacer">
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR id="trDescription">
					<TD class="Content"><asp:label id="lblDescription" Runat="server">Description:&nbsp;<BR></asp:label><%# DataBinder.Eval(Me, "Description") %></TD>
				</TR>
				<TR id="trPriceSpacer">
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR id="trPrice">
					<TD class="Content" noWrap><asp:label id="lblPrice" Runat="server">Price:&nbsp;</asp:label><% PriceDisplay(DataBinder.Eval(Product, "Price")) %></TD>
				</TR>
				<TR id="trSalePrice">
					<TD class="Content" noWrap><asp:label id="lblSalePrice" Runat="server">Sale 
            Price:&nbsp;</asp:label><%# Format(DataBinder.Eval(Product,"SalePrice"), "c") %></TD>
				</TR>
				<tr id="trCustomPrice">
					<td class="Content" id="lblYourPrice" nowrap>Your Price:
					</td>
					<td id="lblSalePriceDisplay" nowrap class="Content">
					</td>
					<td class="Content" nowrap id="CustomPriceCell"></td>
				</tr>
				<tr id="trSavings">
					<td class="Content" id="lblSavings" nowrap></td>
				</tr>
				<TR id="trVolumePricing">
					<TD class="Content" vAlign="top">
						<asp:LinkButton id="btnVolumePricing"  CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'  onclick="LinkButton_Click" Runat="server">
						</asp:LinkButton>
					</TD>
				</TR>
				<TR id="trStockVolumeSpacer">
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR class="Content" id="trVolumePricing3">
					<TD class="Content" id="VolumePriceGrid" colspan="10">
						<uc1:volumepricing id="Volumepricing1" runat="server" ProdID='<%#DataBinder.Eval(Product,"ProductID") %>'>
						</uc1:volumepricing>
					</TD>
				</TR>
				<TR>
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR id="trStockInfo">
					<TD class="Content" vAlign="top">
						<asp:linkbutton id="StockInfo" onclick="StockButton_Click" Runat="server" CommandName='<%#DataBinder.Eval(Product,"ProductID") %>' Text="Stock Status">
						</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD class="Content">
						<uc1:cinventorycontrol id="CInventoryControl1" runat="server"></uc1:cinventorycontrol></TD>
				</TR>
				<TR id="trAttributesSpacer">
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR id="trAttributes">
					<TD class="Content"><uc1:cattributecontrol id="CAttributeControl1" runat="server"></uc1:cattributecontrol></TD>
				</TR>
				<TR>
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR id="trQty">
					<TD class="Content">Qty:
						<asp:textbox id="txtQty" MaxLength="10" runat="server" Columns="2"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap>
						<asp:LinkButton ID="btnAddToCart" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'>
							<asp:Image BorderWidth="0" ID="imgAddToCart" runat="server" AlternateText="Add To Cart"></asp:Image>
						</asp:LinkButton>
					</TD>
				</TR>
				<TR>
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR id="trSavedCart">
					<TD class="Content" noWrap>
						<asp:LinkButton ID="btnAddToSavedCart" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'>
							<asp:Image BorderWidth="0" ID="imgAddToSavedCart" runat="server" AlternateText="Add To Saved Cart"></asp:Image>
						</asp:LinkButton>
					</TD>
				</TR>
				<TR>
					<TD class="Content">&nbsp;</TD>
				</TR>
				<TR id="trEMailFriend">
					<TD class="Content" noWrap>
						<asp:LinkButton ID="btnEMailFriend" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Product,"ProductID") %>'>
							<asp:Image BorderWidth="0" ID="imgEMailFriend" runat="server" AlternateText="Email Friend"></asp:Image>
						</asp:LinkButton>
					</TD>
				</TR>
				<TR id="trVolumePricingSpacer">
					<TD class="Content">&nbsp;</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<br>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="SeperatorLine" runat="server">
	<tr>
		<td class="ContentTable"><IMG height="2" src="images/clear.gif" width="1"></td>
	</tr>
</table>
<br>
<asp:repeater id="Repeater1" runat="server" EnableViewState="False">
	<ItemTemplate>
		<TR>
			<TD class="Content" colSpan="5">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="Content">&nbsp;</TD>
			<TD class="Content" width="1%" valign="top">
				<table id="tblImgCell" runat="server" cellspacing="0" cellpadding="0" border="0">
					<tr>
						<TD class="Content" width="1%" valign="top" id="tdRImageCell">
							<asp:HyperLink Runat=server ID="lnkImage" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DetailLink") %>'>
								<img border=0 src='<%#DataBinder.Eval(Container.DataItem,"SmallImage") %>'></asp:HyperLink>
							<asp:Panel Runat="server" ID="SmallImage">
								<img src='<%#DataBinder.Eval(Container.DataItem,"SmallImage") %>'></asp:Panel>
						</TD>
					</tr>
				</table>
			</TD>
			<TD class="Content" width="1%">&nbsp;</TD>
			<TD valign="top">
				<table id="Table6" runat="server" cellSpacing="0" cellPadding="0" border="0">
					<tr id="trRProductName">
						<td class="Content">
							<asp:Label ID="lblProductName2" Runat="server">Product Name:&nbsp;</asp:Label>
							<asp:HyperLink id="lnkProductName" Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DetailLink") %>'>
								<%# DataBinder.Eval(Container.DataItem,"Name") %>
							</asp:HyperLink>
							<asp:Label id="lblRProductName" runat="server">
								<%# DataBinder.Eval(Container.DataItem,"Name") %>
							</asp:Label>
						</td>
					</tr>
					<tr id="trRProductCode">
						<td class="Content">
							<asp:Label ID="lblProductCode2" Runat="server">Product ID:&nbsp;</asp:Label>
							<asp:HyperLink id=lnkProductCode Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DetailLink") %>'>
								<%# DataBinder.Eval(Container.DataItem,"ProductCode") %>
							</asp:HyperLink>
							<asp:Label id="lblRProductCode" runat="server">
								<%# DataBinder.Eval(Container.DataItem,"ProductCode") %>
							</asp:Label>
						</td>
					</tr>
					<tr id="trRShortDescriptonSpacer">
						<td class="Content">&nbsp;</td>
					</tr>
					<tr id="trRShortDescription">
						<td class="Content">
							<asp:Label ID="lblDescription2" Runat="server">Short Description:&nbsp;</asp:Label><%# DataBinder.Eval(Container.DataItem, "ShortDescription") %></td>
					</tr>
					<tr id="trRPriceSpacer">
						<td class="Content">&nbsp;</td>
					</tr>
					<tr id="trRPrice">
						<td class="Content">
							<asp:Label ID="lblPrice2" Runat="server">Price:&nbsp;</asp:Label><%# PriceDisplay2(DataBinder.Eval(Container.DataItem, "Price")) %></td>
					</tr>
					<tr id="trRSalePrice">
						<td class="Content" id="RecommendedYourPriceCell" nowrap>Your Price:
							<asp:Label ID="lblRCustomPrice" Runat="server"></asp:Label>
						</td>
						<td id="RecommendedSalePriceCell" nowrap class="Content">Sale Price:
							<asp:Label ID="lblRSalePrice" Runat="server"></asp:Label>
						</td>
						<td class="Content" id="RecommendedCustomPriceCell" nowrap>
							<asp:Label ID="lblRCustomPriceOnly" Runat="server"></asp:Label>
						</td>
					</tr>
				</table>
			</TD>
			<TD class="Content">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="Content" colSpan="5">&nbsp;</TD>
		</TR>
	</ItemTemplate>
	<HeaderTemplate>
		<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD class="Content">&nbsp;</TD>
				<TD class="Headings" noWrap colSpan="3"><%# DataBinder.Eval(me, "RecommendedTitle") %></TD>
				<TD class="Content">&nbsp;</TD>
			</TR>
	</HeaderTemplate>
	<FooterTemplate>
		</Table>
	</FooterTemplate>
</asp:repeater>

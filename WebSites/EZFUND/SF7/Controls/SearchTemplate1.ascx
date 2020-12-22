<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SearchTemplate1.ascx.vb" Inherits="StoreFront.StoreFront.SearchTemplate1" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="CAttributeControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CInventoryControl" Src="CInventoryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchFiltersControl" Src="SearchFiltersControl.ascx" %>
<asp:Panel ID="ResultInfo" Runat="server" CssClass="Content"><B>Search Results</B><BR>Search for: 
<asp:Label id="lblKeyword" runat="server"></asp:Label>&nbsp;in 
<asp:Label id="lblCategory" runat="server">Category:</asp:Label>
<asp:Label id="lblCategoryName" CssClass="Content" runat="server"></asp:Label>&nbsp;returned 
<asp:label id="lblCount" CssClass="Content" runat="server"></asp:label>&nbsp; 
<asp:Label id="lblProducts" CssClass="Content" Runat="server">Products</asp:Label><BR><A href="Search.aspx">
		New Search</A> | <A href="Search.aspx?Advanced=1">Advanced Search</A><BR><BR>
</asp:Panel>
<uc1:SearchFiltersControl id="SearchFiltersControl1" runat="server"></uc1:SearchFiltersControl>
<asp:datagrid id="DataGrid1" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
	ShowHeader="False" BorderWidth="0px">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<asp:DataList id="DataList1" Runat="server" Width="100%" ItemStyle-VerticalAlign="Top" RepeatDirection="Horizontal">
					<ItemStyle Wrap="False"></ItemStyle>
					<ItemTemplate>
						<table runat="Server" id="ContentTable" border="0" cellpadding="0" cellspacing="0" width="100%" class="search-grid-tbl">
							<tr id="ImageCell">
								<td class="Content" align="center">
									<asp:HyperLink Runat=server ID="lnkImage" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
										<img border=0 src='<%#IIf(Eval("SmallImage").ToLower.StartsWith("http://") OrElse Eval("SmallImage").ToLower.StartsWith("https://"), Eval("SmallImage"), ResolveURL("~/" & DataBinder.Eval(Container.DataItem,"SmallImage")))%>'></asp:HyperLink>
									<asp:Panel Runat="server" ID="SmallImage">
										<img src='<%#IIf(Eval("SmallImage").ToLower.StartsWith("http://") OrElse Eval("SmallImage").ToLower.StartsWith("https://"), Eval("SmallImage"), ResolveURL("~/" & DataBinder.Eval(Container.DataItem,"SmallImage")))%>'></asp:Panel>
								</td>
							</tr>
							<tr id="ImageSpacer" class="hide">
								<td class="Content"></td>
							</tr>
							<tr id="ProductCodeCell">
								<td class="Content" align="center">
									<asp:Label runat="server" ID="lblProductCode">Product Code: &nbsp;</asp:Label>
									<asp:HyperLink Runat=server ID="lnkProductCode" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
										<%# DataBinder.Eval(Container.DataItem,"ProductCode") %>
									</asp:HyperLink>
									<asp:Label runat="server" ID="lblProdCode">
										<%# DataBinder.Eval(Container.DataItem,"ProductCode") %>
									</asp:Label>
								</td>
							</tr>
							<tr id="ProductNameCell">
								<td class="Content" align="center">
									<asp:Label runat="server" ID="lblProductName">Product Name: &nbsp;</asp:Label>
									<asp:HyperLink Runat=server ID="lnkProductName" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
										<%# DataBinder.Eval(Container.DataItem,"Name") %>
									</asp:HyperLink>
									<asp:Label runat="server" ID="lblProdName">
										<%# DataBinder.Eval(Container.DataItem,"Name") %>
									</asp:Label>
								</td>
							</tr>
							<tr id="ShortDescriptionCell">
								<td class="Content" align="center">
									<asp:Label runat="server" ID="lblDescription">Description: &nbsp;</asp:Label><%# DataBinder.Eval(Container.DataItem,"ShortDescription") %></td>
							</tr>
							<tr id="VendorCell">
								<td class="Content" align="center">
									<asp:Label runat="server" ID="lblVendor">Vendor: &nbsp;</asp:Label><%# DataBinder.Eval(Container.DataItem,"Vendor") %></td>
							</tr>
							<tr id="ManufacturerCell">
								<td class="Content" align="center">
									<asp:Label runat="server" ID="lblManufacturer">Manufacturer: &nbsp;</asp:Label><%# DataBinder.Eval(Container.DataItem,"Manufacturer") %>
								</td>
							</tr>
							<tr id="AttributesCell">
								<td class="Content" align="center">
									<uc1:CAttributeControl id="CAttributeControl1" runat="server"></uc1:CAttributeControl></td>
							</tr>
							<TR id="DisplayStockInfo">
								<TD class="Content" vAlign="top">
									<asp:LinkButton id="StockInfo" CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'  onclick="StockButton_Click" Runat="server">
									</asp:LinkButton>
								</TD>
							</TR>
							<TR id="trStockStatus">
								<TD class="Content" vAlign="top">
									<uc1:CInventoryControl id="CInventoryControl1" ProductID='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>' runat="server" >
									</uc1:CInventoryControl>
								</TD>
							</TR>
							<tr id="trRegularPrice" class="Content">
								<td class="Content" align="left">
									<asp:Label ID="lblPrice" Runat="server">Price:&nbsp;</asp:Label>
									<asp:Label ID="lblRegularPriceDisplay" Runat="server" Visible="True"></asp:Label>
								</td>
							</tr>
							<!-- Verisign Recurring Billing -->
							<tr id="trSubscriptionPrice" class="Content" visible="False">
								<td class="Content" align="left">
									<asp:Label ID="lblSubscriptionPrice" Runat="server">Subscription Price:&nbsp;</asp:Label>
									<asp:Label ID="lblSubscriptionPriceDisplay" Runat="server" Visible="True"></asp:Label>
								</td>
							</tr>
							<tr id="trRecurringPrice" class="Content" visible="false">
								<td class="Content" align="left">
									<asp:Label ID="lblRecurringPrice" Runat="server">Recurring Price:&nbsp;</asp:Label>
									<asp:Label ID="lblRecurringPriceDisplay" Runat="server" Visible="True"></asp:Label>
								</td>
							</tr>
							<!-- Verisign Recurring Billing -->
							<tr id="trSalePrice" class="Content">
								<td class="Content" align="left">
									<asp:Label ID="lblSalePrice" Runat="server">Sale&nbsp;Price:&nbsp;</asp:Label>
									<asp:Label ID="lblSalePriceDisplay" Runat="server" Visible="True"></asp:Label>
								</td>
							</tr>
							<tr id="trCustomPrice" class="Content">
								<td class="Content">
									<asp:Label ID="lblCustomPrice" Runat="server">Your&nbsp;Price:&nbsp;</asp:Label>
									<asp:Label ID="lblCustomPriceDisplay" Runat="server" Visible="True"></asp:Label>
								</td>
							</tr>
							<tr id="MoreInfoCell">
								<td class="Content" align="center">
									<asp:HyperLink Runat=server ID="lnkMoreInfo" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
										<asp:label ID="lblMoreInfo" Runat="server">More Info</asp:label>
									</asp:HyperLink>
								</td>
							</tr>
							<tr id="BuyNowCell">
								<td class="Content" align="center">
									<asp:LinkButton ID="btnAddToCart" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
										<asp:Image BorderWidth="0" ID="imgAddToCart" runat="server" AlternateText="Add To Cart"></asp:Image>
									</asp:LinkButton>
								</td>
							</tr>
							<tr id="SavedCartCell" style='<%# IIf(CInt(DataBinder.Eval(Container.DataItem, "ProductType")) <> 0 AndAlso CInt(DataBinder.Eval(Container.DataItem, "ProductType")) = 1, "DISPLAY: none", "DISPLAY: block") %>'>
								<td class="Content" align="center">
									<asp:LinkButton ID="btnAddToSavedCart" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
										<asp:Image BorderWidth="0" ID="imgAddToSavedCart" runat="server" AlternateText="Add To Saved Cart"></asp:Image>
									</asp:LinkButton>
								</td>
							</tr>
							<tr id="EMailFriendCell">
								<td class="Content" align="center">
									<asp:LinkButton ID="btnEMailFriend" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
										<asp:Image BorderWidth="0" ID="imgEMailFriend" runat="server" AlternateText="Email Friend"></asp:Image>
									</asp:LinkButton>
								</td>
							</tr>
						</table>
					</ItemTemplate>
				</asp:DataList>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Right" Position="TopAndBottom" CssClass="Content" Mode="NumericPages"></PagerStyle>
</asp:datagrid>

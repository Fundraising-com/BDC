<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="CAttributeControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SearchTemplate3.ascx.vb" Inherits="StoreFront.StoreFront.SearchTemplate3" targetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="CInventoryControl" Src="CInventoryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="VolumePricing" Src="VolumePricing.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchFiltersControl" Src="SearchFiltersControl.ascx" %>
<asp:panel id="ResultInfo" Runat="server"><B>Search Results</B><BR>Search for: 
<asp:Label id="lblKeyword" runat="server"></asp:Label>&nbsp;in 
<asp:Label id="lblCategory" runat="server">Category:</asp:Label>
<asp:Label id="lblCategoryName" runat="server"></asp:Label>&nbsp;returned 
<asp:label id="lblCount" runat="server"></asp:label>
<asp:Label id="lblProducts" Runat="server" CssClass="Content">Products</asp:Label><BR><A href="Search.aspx">
		New Search</A> | <A href="Search.aspx?Advanced=1">Advanced Search</A><BR><BR></asp:panel>
<uc1:SearchFiltersControl id="SearchFiltersControl1" runat="server"></uc1:SearchFiltersControl>
<TABLE class="ContentTableHeader">
	<TR id="HeaderRow">
		<TD class="ContentTableHeader" width="100%">
			<asp:Label ID="lblHeaderRowCategoryname" CssClass="ContentTableHeader" text="" Runat="server" /></TD>
		<td class="ContentTableHeader">
			<asp:panel id="DrillDownPanel" Runat="server">
				<asp:DropDownList id="NextLevel" Runat="server" CssClass="content" AutoPostBack="False" DataTextField="Name"
					DataValueField="id"></asp:DropDownList>
			</asp:panel></td>
	</TR>
</TABLE>
<asp:datagrid id="DataGrid1" runat="server" BorderWidth="0px" Width="100%"
	CellPadding="0" ShowHeader="False" AllowPaging="True" AutoGenerateColumns="False" PageSize="5">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<TABLE id="ResultTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server" >
				    <TR>
						<TD class="ContentTableHorizontal" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR class="Content" align="left">
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="Content">&nbsp;</TD>
						<TD align="left" width="100%" colspan="3">
							<TABLE id="ResultContent" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0"
								runat="server">
								<TR>
									<TD vAlign="middle" colSpan="2">
										<TABLE id="ImageDisplay" runat="server">
											<TR id="DisplayImage">
												<TD class="Content" vAlign="middle" align="left" rowSpan="8">
													<asp:Panel id="leftImage" Runat="server" Visible="True">
														<asp:HyperLink id=lnkImage Runat="server" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
															<img border=0 align=left  src='<%#IIf(Eval("SmallImage").ToLower.StartsWith("http://") OrElse Eval("SmallImage").ToLower.StartsWith("https://"), Eval("SmallImage"), ResolveURL("~/" & DataBinder.Eval(Container.DataItem,"SmallImage")))%>'></asp:HyperLink>
														<asp:Panel Runat="server" id="SmallImage">
															<IMG src='<%#IIf(Eval("SmallImage").ToLower.StartsWith("http://") OrElse Eval("SmallImage").ToLower.StartsWith("https://"), Eval("SmallImage"), ResolveURL("~/" & DataBinder.Eval(Container.DataItem,"SmallImage")))%>' align=left>
														</asp:Panel>
													</asp:Panel></TD>
											</TR>
										</TABLE>
									</TD>
									<TD>&nbsp;</TD>
									<TD class="Content" vAlign="top" noWrap width="100%">
										<TABLE id="ProductInfoTable" width="100%" runat="server" >
											<TR>
												<TD class="Content">
													<asp:Label id="lblProductCode" runat="server">Product Id:&nbsp;</asp:Label>
													<asp:HyperLink id=lnkProductCode Runat="server" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
														<%# DataBinder.Eval(Container.DataItem,"ProductCode") %>
													</asp:HyperLink>
													<asp:Label id="lblProdCode" runat="server">
														<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>
													</asp:Label></TD>
												<td rowspan="2">
													<TABLE runat="server" align="right" id="RightDisplay" >
														<tr id="RDisplayVendor">
															<TD class="Content" vAlign="top" noWrap>
																<asp:Label id="lblVendor" runat="server" CssClass="Content">Vendor:&nbsp;</asp:Label>
																<%#DataBinder.Eval(Container.DataItem,"Vendor")%>
															</TD>
														</tr>
														<tr id="RDisplayManufacturer">
															<TD class="Content" vAlign="top" noWrap>
																<asp:Label id="lblManufacturer" runat="server" CssClass="Content">Manufacturer:&nbsp;</asp:Label>
																<%#DataBinder.Eval(Container.DataItem,"Manufacturer")%>
															</TD>
														</tr>
													</TABLE>
												</td>
											</TR>
											<TR id="ProductNameRow">
												<TD class="Content" vAlign="top">
													<asp:Label id="lblProductName" runat="server">Product Name:&nbsp;</asp:Label>
													<asp:HyperLink id=lnkProductName Runat="server" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
														<%#DataBinder.Eval(Container.DataItem,"Name")%>
													</asp:HyperLink>
													<asp:Label id="lblProdName" runat="server">
														<%#DataBinder.Eval(Container.DataItem,"Name")%>
													</asp:Label></TD>
											</TR>
											<TR id="DisplayShortDescription">
												<TD class="Content" vAlign="top">
													<asp:Label id="lblDescription" runat="server">Description:&nbsp;</asp:Label><%#DataBinder.Eval(Container.DataItem,"ShortDescription")%></TD>
											</TR>
											<TR id="DisplayMoreInfo">
												<td class="Content">
													<asp:HyperLink Runat=server ID="lnkMoreInfo" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
														<asp:label ID="lblMoreInfo" Runat="server">More Info</asp:label>
													</asp:HyperLink>
												</td>
											</TR>
											<tr id="trRegularPrice" class="Content">
												<td class="Content" align="left">
													<asp:Label ID="lblPrice" Runat="server">Price:&nbsp;</asp:Label>
													<asp:Label ID="lblRegularPriceDisplay" Runat="server" Visible="True"></asp:Label>
												</td>
											</tr>
											<!-- Verisign Recurring Billing -->
											<tr id=trSubscriptionPrice class="Content" Visible="False">
												<td class="Content" align="left">
													<asp:Label ID="lblSubscriptionPrice" Runat="server">&nbsp;</asp:Label>
													<asp:Label ID="lblSubscriptionPriceDisplay" Runat="server" Visible="True"></asp:Label>
												</td>
											</tr>
											<tr id="trRecurringPrice" class="Content" Visible="False">
												<td class="Content" align="left">
													<asp:Label ID="lblRecurringPrice" Runat="server">&nbsp;</asp:Label>
													<asp:Label ID="lblRecurringPriceDisplay" Runat="server" Visible="True"></asp:Label>
												</td>
											</tr>
											<!-- Veridign Recurring Billing -->
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
											<%-- Tee 8/30/2007 product configurator --%>
											<TR id="ProdBundle">
												<TD class="Content"><%# BundleInfo(DataBinder.Eval(Container.DataItem, "BundledProducts")) %></TD>
											</TR>
											<%-- end Tee --%>
											<TR id="AttributeRow">
												<TD class="Content" vAlign="top">
													<uc1:CAttributeControl id="CAttributeControl1" runat="server"></uc1:CAttributeControl></TD>
											</TR>
											<TR id="DisplayStockInfo">
												<TD class="Content" vAlign="top">
													<asp:LinkButton id="StockInfo" CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>' OnDataBinding="StockInfo_DataBinding" onclick="StockButton_Click" Runat="server">
													</asp:LinkButton>
												</TD>
											</TR>
											<TR id="trStockStatus">
												<TD class="Content" vAlign="top">
													<uc1:CInventoryControl id="CInventoryControl1" ProductID='<%#DataBinder.Eval(Container.DataItem,"ProductID")%>' runat="server" >
													</uc1:CInventoryControl>
												</TD>
											</TR>
											<TR id="DisplayQuantity">
												<TD class="Content" vAlign="top">
													<asp:TextBox id="txtQuantity" Runat="server" Width="30px" Columns="2" MaxLength="10">1</asp:TextBox></TD>
											</TR>
											<TR id="AddToCart">
												<TD class="Content" vAlign="top">
													<asp:LinkButton ID="btnAddToCart" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
														<asp:Image BorderWidth="0" ID="imgAddToCart" runat="server" AlternateText="Add To Cart"></asp:Image>
													</asp:LinkButton></TD>
											</TR>
											<TR id="AddToSavedCart" style='<%# IIf(CInt(DataBinder.Eval(Container.DataItem, "ProductType")) <> 0 AndAlso CInt(DataBinder.Eval(Container.DataItem, "ProductType")) <> 1, "DISPLAY: none", "DISPLAY: block") %>'>
												<TD class="Content" vAlign="top">
													<asp:LinkButton ID="btnAddToSavedCart" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
														<asp:Image BorderWidth="0" ID="imgAddToSavedCart" runat="server" AlternateText="Add To Saved Cart"></asp:Image>
													</asp:LinkButton></TD>
											</TR>
											
											<TR id="EMailFriend">
												<TD class="Content" vAlign="top">
													<asp:LinkButton ID="btnEMailFriend" Runat="server" onclick=AddCart CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
														<asp:Image BorderWidth="0" ID="imgEMailFriend" runat="server" AlternateText="Email Friend"></asp:Image>
													</asp:LinkButton>
												</TD>
											</TR>
											
											<TR id="DisplayVolumePricing">
												<TD class="Content" vAlign="top">
													<asp:LinkButton id="btnVolumePricing" CommandName='<%#DataBinder.Eval(Container.DataItem,"ProductID")%>' onclick="LinkButton_Click" Runat="server">
													</asp:LinkButton>
												</TD>
											</TR>
											<TR id="VolumePriceGrid">
												<TD class="Content" colspan="10">
													<uc1:volumepricing id="Volumepricing1" runat="server">
													</uc1:volumepricing>
												</TD>
											</TR>
										</TABLE>
									</TD>
									<TD>&nbsp;</TD>
									<TD vAlign="middle">
										<TABLE id="RightImageDisplay" runat="server" align="right" visible="false">
											<TR valign="top">
												<TD class="Content" id="rDisplayImage" vAlign="middle">
													<asp:Panel id="RightImage" Runat="server" Visible="False">
														<asp:Panel Runat="server" id="SmallImage2">
															<IMG src='<%#IIf(Eval("SmallImage").ToLower.StartsWith("http://") OrElse Eval("SmallImage").ToLower.StartsWith("https://"), Eval("SmallImage"), ResolveURL("~/" & DataBinder.Eval(Container.DataItem,"SmallImage")))%>'>
														</asp:Panel>
														<asp:HyperLink id=lnkImage2 Runat="server" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
															<img border=0 src='<%#IIf(Eval("SmallImage").ToLower.StartsWith("http://") OrElse Eval("SmallImage").ToLower.StartsWith("https://"), Eval("SmallImage"), ResolveURL("~/" & DataBinder.Eval(Container.DataItem,"SmallImage")))%>'></asp:HyperLink>
													</asp:Panel></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR id="Spacer">
									<TD class="Content" colSpan="5">&nbsp;</TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR id="SeperatorRow">
						<TD class="ContentTableHorizontal" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR id="SpacerRow">
						<TD class="Content" colSpan="7">&nbsp;</TD>
					</TR>
					<TR id="FooterRow">
						<TD class="ContentTableHorizontal" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR>
						<TD class="Content" colSpan="7" height="3"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Right" Position="TopAndBottom" CssClass="Content" Mode="NumericPages"></PagerStyle>
</asp:datagrid>

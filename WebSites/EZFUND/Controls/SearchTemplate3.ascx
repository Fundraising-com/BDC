<%@ Register TagPrefix="uc1" TagName="VolumePricing" Src="VolumePricing.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CInventoryControl" Src="CInventoryControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SearchTemplate3.ascx.vb" Inherits="StoreFront.StoreFront.SearchTemplate3" targetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="CAttributeControl.ascx" %>
<asp:panel id="ResultInfo" Runat="server"><B>Search Results</B><BR>Search for: 
<asp:Label id="lblKeyword" runat="server"></asp:Label>&nbsp;in 
<asp:Label id="lblCategory" runat="server">Category:</asp:Label>
<asp:Label id="lblCategoryName" runat="server"></asp:Label>&nbsp;returned 
<asp:label id="lblCount" runat="server"></asp:label>
<asp:Label id="lblProducts" Runat="server" CssClass="Content">Products</asp:Label><BR><A href="Search.aspx">
		New Search</A> | <A href="Search.aspx?Advanced=1">Advanced Search</A><BR><BR></asp:panel><asp:datagrid id="DataGrid1" runat="server" PageSize="5" AutoGenerateColumns="False" AllowPaging="True"
	ShowHeader="False" CellPadding="0" Width="100%" BorderWidth="0px" enableviewstate="False">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<TABLE id="ResultTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server"
					enableviewstate="false">
					<TR id="HeaderRow">
						<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="ContentTableHeader">&nbsp;</TD>
						<TD class="ContentTableHeader" nowrap><%# DataBinder.Eval(Container.DataItem,"CategoryName") %></TD>
						<td class="ContentTableHeader" width="100%">&nbsp;</td>
						<TD class="ContentTableHeader" align="right">
							<asp:DropDownList id="NextLevel" CssClass="content" Runat="server" DataValueField="id" DataTextField="Name"
								OnSelectedIndexChanged="DrillDown" AutoPostBack="True"></asp:DropDownList></TD>
						<TD class="ContentTableHeader">&nbsp;</TD>
						<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR class="Content" align="left">
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="Content">&nbsp;</TD>
						<TD align="left" width="100%" colspan="3">
							<TABLE id="ResultContent" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0"
								runat="server">
								<TR>
									<TD vAlign="top" colSpan="2">
										<TABLE id="ImageDisplay" runat="server">
											<TR id="DisplayImage">
												<TD class="Content" vAlign="middle" align="left" rowSpan="8">
													<asp:Panel id="leftImage" Runat="server" Visible="True">
														<asp:HyperLink id=lnkImage Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DetailLink") %>'>
															<img border=0 align=left  src='<%#DataBinder.Eval(Container.DataItem,"SmallImage") %>'></asp:HyperLink>
														<asp:Panel Runat="server" id="SmallImage">
															<IMG src='<%# DataBinder.Eval(Container.DataItem,"SmallImage") %>' align=left>
														</asp:Panel>
													</asp:Panel></TD>
											</TR>
										</TABLE>
									</TD>
									<TD>&nbsp;</TD>
									<TD class="Content" vAlign="top" noWrap width="100%">
										<TABLE id="ProductInfoTable" width="100%" runat="server">
											<TR>
												<TD class="Content">
													<asp:Label id="lblProductCode" runat="server">Product Id:&nbsp;</asp:Label>
													<asp:HyperLink id=lnkProductCode Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DetailLink") %>'>
														<%# DataBinder.Eval(Container.DataItem,"ProductCode") %>
													</asp:HyperLink>
													<asp:Label id="lblProdCode" runat="server">
														<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>
													</asp:Label></TD>
												<td rowspan="2">
													<TABLE runat="server" align="right" id="RightDisplay">
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
													<asp:HyperLink id=lnkProductName Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DetailLink") %>'>
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
													<asp:HyperLink Runat=server ID="lnkMoreInfo" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DetailLink") %>'>
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
											<TR id="AttributeRow">
												<TD class="Content" vAlign="top">
													<uc1:CAttributeControl id="CAttributeControl1" runat="server"></uc1:CAttributeControl></TD>
											</TR>
											<TR id="DisplayStockInfo">
												<TD class="Content" vAlign="top">
													<asp:LinkButton id="StockInfo" CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>' OnDataBinding =StockInfo_DataBinding onclick="StockButton_Click" Runat="server">
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
											<TR id="AddToSavedCart">
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
											<TR class="Content" id="VolumePriceGrid">
												<TD class="Content" colspan="10">
													<uc1:volumepricing id="Volumepricing1" runat="server" ProdID='<%#DataBinder.Eval(Container.DataItem,"ProductID")%>'>
													</uc1:volumepricing>
												</TD>
											</TR>
										</TABLE>
									</TD>
									<TD>&nbsp;</TD>
									<TD vAlign="top">
										<TABLE id="RightImageDisplay" runat="server" align="right" visible="false">
											<TR valign="top">
												<TD class="Content" id="rDisplayImage" vAlign="middle">
													<asp:Panel id="RightImage" Runat="server" Visible="False">
														<asp:Panel Runat="server" id="SmallImage2">
															<IMG src='<%#DataBinder.Eval(Container.DataItem,"SmallImage") %>'>
														</asp:Panel>
														<asp:HyperLink id=lnkImage2 Runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DetailLink") %>'>
															<img border=0 src='<%#DataBinder.Eval(Container.DataItem,"SmallImage") %>'></asp:HyperLink>
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
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Right" Position="TopAndBottom" CssClass="Content" Mode="NumericPages"></PagerStyle>
</asp:datagrid>

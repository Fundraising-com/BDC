<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SearchTemplate2.ascx.vb" Inherits="StoreFront.StoreFront.SearchTemplate2" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
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
<TABLE class="ContentTableHeader">
	<TR id="HeaderRow">
		<TD class="ContentTableHeader" width="100%">
			<asp:Label ID="lblHeaderRowCategoryname" CssClass="ContentTableHeader" text="" Runat="server" /></TD>
		<td class="ContentTableHeader">
			<asp:panel id="DrillDownPanel" Runat="server" EnableViewState="True">
				<asp:DropDownList id="NextLevel" CssClass="content" Runat="server" AutoPostBack="False" DataTextField="Name"
					DataValueField="id"></asp:DropDownList>
			</asp:panel></td>
	</TR>
</TABLE>
<asp:datagrid id="DataGrid1" runat="server" BorderWidth="0px" Width="100%" CellPadding="0" ShowHeader="False"
	AutoGenerateColumns="False" AllowPaging="True" EnableViewState="true">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<TABLE id="ResultTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server"
					EnableViewState="true">
					<TR>
						<TD class="ContentTableHorizontal" colSpan="12" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR>
					    <TD class="ContentTable"></TD>
						<TD class="Content" colSpan="10">&nbsp;</TD>
						<TD class="ContentTable"></TD>
					</TR>
					<TR id="ContentRow">
						<TD class="ContentTable" style="WIDTH: 1px" width="1"><IMG height="1" src="images/clear.gif"></TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="Content" vAlign="top" align="left" width="1%">
							<asp:HyperLink id=lnkProductCode Runat="server" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
								<%# DataBinder.Eval(Container.DataItem,"ProductCode") %>
							</asp:HyperLink>
							<asp:Label id="lblProdCode" runat="server">
								<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>
							</asp:Label></TD>
						<TD class="Content">&nbsp;&nbsp;</TD>
						<TD class="Content" vAlign="top" width="100%">
							<TABLE id="ProductInfoTable" cellSpacing="0" cellPadding="0" border="0" runat="Server"
								width="100%" EnableViewState="true">
								<TR id="DisplayProductName">
									<TD class="Content" align="left">
										<asp:HyperLink id=lnkProductName Runat="server" NavigateUrl='<%#ResolveURL(DataBinder.Eval(Container.DataItem,"DetailLink"))%>'>
											<%# DataBinder.Eval(Container.DataItem,"Name") %>
										</asp:HyperLink>
										<asp:Label id="lblProdName" runat="server">
											<%#DataBinder.Eval(Container.DataItem,"Name")%>
										</asp:Label></TD>
								</TR>
								<TR id="ProdSpacerRow">
									<TD class="Content">&nbsp;</TD>
								</TR>
								<%-- Tee 8/30/2007 product configurator --%>
								<TR id="ProdBundle">
									<TD class="Content"><%# BundleInfo(DataBinder.Eval(Container.DataItem, "BundledProducts")) %></TD>
								</TR>
								<%-- end Tee --%>
								<TR id="AttributeRow">
									<TD class="Content">
										<uc1:CAttributeControl id="CAttributeControl1" runat="server"></uc1:CAttributeControl></TD>
								</TR>
								<TR id="DisplayStockInfo">
									<TD class="Content" vAlign="top">
										<asp:LinkButton id="StockInfo" onclick="StockButton_Click" Runat="server" OnDataBinding="StockInfo_DataBinding" CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
										</asp:LinkButton></TD>
								</TR>
								<TR id="trStockStatus">
									<TD class="Content" vAlign="top">
										<uc1:CInventoryControl id=CInventoryControl1 runat="server" ProductID='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
										</uc1:CInventoryControl></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="Content">&nbsp;</TD>
						<td valign="top">
							<table id="tblPricing" cellSpacing="0" cellPadding="0" width="100%" align="right" border="0"
								runat="server" EnableViewState="true">
								<tr>
									<TD class="Content" id="tdRegularPrice" vAlign="top" align="left" width="35%" nowrap>
										<asp:Label id="lblRegularPriceDisplay" Runat="server" Visible="True"></asp:Label></TD>
									<TD class="Content" id="tdSalePrice" vAlign="top" align="left" width="35%" nowrap>
										<asp:Label id="lblSalePriceDisplay" Runat="server" Visible="True"></asp:Label></TD>
									<TD class="Content" id="tdCustomPrice" vAlign="top" align="left" width="35%" nowrap>
										<asp:Label id="lblCustomPriceDisplay" Runat="server" Visible="True"></asp:Label>
									</TD>
									<td class="Content" id="tdSubscriptionPrice" valign="top" align="right" width="35%"
										nowrap visible="False">
										<asp:Label ID="lblSubscriptionPriceDisplay" Runat="server" Visible="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="Content" id="tdRecurringPrice" valign="top" align="right" width="35%" nowrap
										visible="False">
										<asp:Label ID="lblRecurringPriceDisplay" Runat="server" Visible="True"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
						<TD>&nbsp;&nbsp;</TD>
						<TD class="Content" vAlign="top" align="right" width="20%">
							<TABLE id="ActionTable" cellSpacing="0" cellPadding="0" width="100%" align="right" border="0"
								runat="server" EnableViewState="true">
								<TR id="AddToCart">
									<TD class="Content" align="right">
										<asp:TextBox id="txtQuantity" runat="server" CssClass="Content" Width="30px" MaxLength="10" Columns="2">1</asp:TextBox></TD>
									<TD class="Content">&nbsp;</TD>
									<TD class="Content" noWrap align="right" width="100%">
										<asp:LinkButton id="btnAddToCart" onclick="AddCart" Runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
											<asp:Image BorderWidth="0" ID="imgAddToCart" runat="server" AlternateText="Add To Cart"></asp:Image>
										</asp:LinkButton></TD>
								</TR>
								<TR id="AddToSavedCart" style='<%# IIf(CInt(DataBinder.Eval(Container.DataItem, "ProductType")) <> 0 AndAlso CInt(DataBinder.Eval(Container.DataItem, "ProductType")) <> 1, "DISPLAY: none", "DISPLAY: block") %>'>
									<TD class="Content">&nbsp;</TD>
									<TD class="Content">&nbsp;</TD>
									<TD class="Content" noWrap align="right" width="100%">
										<asp:LinkButton id=btnAddToSavedCart onclick=AddCart Runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
											<asp:Image BorderWidth="0" ID="imgAddToSavedCart" runat="server" AlternateText="Add To Saved Cart"></asp:Image>
										</asp:LinkButton></TD>
								</TR>
								<TR id="EMailFriend">
									<TD class="Content">&nbsp;</TD>
									<TD class="Content">&nbsp;</TD>
									<TD class="Content" noWrap align="right" width="100%">
										<asp:LinkButton id=btnEMailFriend onclick=AddCart Runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'>
											<asp:Image BorderWidth="0" ID="imgEMailFriend" runat="server" AlternateText="Email Friend"></asp:Image>
										</asp:LinkButton></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="ContentTable" style="WIDTH: 1px" width="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" style="WIDTH: 1px" width="1"><IMG height="1" src="images/clear.gif"></TD>
						<TD class="Content" colSpan="10">&nbsp;</TD>
						<TD class="ContentTable" style="WIDTH: 1px" width="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR id="SeperatorRow">
						<TD class="ContentTableHorizontal" colSpan="12" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR id="SpacerRow">
						<TD class="Content" colSpan="12">&nbsp;</TD>
					</TR>
					<TR id="FooterRow">
						<TD class="ContentTableHorizontal" colSpan="12" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR>
						<TD class="Content" colSpan="12" height="3"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Right" Position="TopAndBottom"
		CssClass="Content" Mode="NumericPages"></PagerStyle>
</asp:datagrid>

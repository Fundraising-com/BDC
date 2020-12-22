<%@ Control Language="vb"  AutoEventWireup="false" Codebehind="SpeedSearch.ascx.vb" Inherits="StoreFront.StoreFront.SpeedSearch" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="CInventoryControl" Src="CInventoryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="CAttributeControl.ascx" %>
<asp:panel id="ResultInfo" Runat="server"><B>Search Results</B><BR>Search for: 
<asp:Label id="lblKeyword" runat="server"></asp:Label>&nbsp;in 
<asp:Label id="lblCategory" runat="server">Category:</asp:Label>
<asp:Label id="lblCategoryName" runat="server"></asp:Label>&nbsp;returned 
<asp:label id="lblCount" runat="server"></asp:label>
<asp:Label id="lblProducts" Runat="server" CssClass="Content">Products</asp:Label><BR><A href="Search.aspx">
		New Search</A> | <A href="Search.aspx?Advanced=1">Advanced Search</A><BR><BR></asp:panel><br>
<asp:datagrid id="DataGrid1" runat="server" BorderWidth="0px" Width="100%" CellPadding="0" ShowHeader="False" AutoGenerateColumns="False" AllowPaging="True" AllowCustomPaging="True">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<TABLE id="ResultTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
					<TR id="ContentRow">
						<TD class="Content">&nbsp;</TD>
						<TD class="Content" vAlign="top" width="100%">
							<TABLE id="ProductInfoTable" cellSpacing="0" cellPadding="0" border="0" runat="Server">
								<TR>
									<TD class="Content" colspan="6">&nbsp;
									</TD>
								</TR>
								<TR>
									<TD class="Content" id="DisplayProductName" align="left" valign="top" nowrap width="25%">
										<asp:HyperLink id="lnkProductName" Runat="server" NavigateUrl='<%# me.DetailLink%>'>
											<%# DataBinder.Eval(Container.DataItem,"Name") %>
										</asp:HyperLink>&nbsp; &nbsp;&nbsp; &nbsp;
									</TD>
									<TD class="Content" vAlign="top">
										&nbsp;</TD>
									<td class="Content" id="tdRegularPrice" align="left" valign="top" nowrap width="5%">
										<asp:Label ID="lblRegularPriceDisplay" Runat="server" Visible="True">
											<%# me.ProductPrice%>
										</asp:Label>&nbsp; &nbsp;&nbsp; &nbsp;
									</td>
									<TD class="Content" id="ShortDescription" vAlign="top" width="40%">
										<asp:Label id="lblDescription" runat="server">Description:&nbsp;</asp:Label><%#DataBinder.Eval(Container.DataItem,"ShortDescription")%>&nbsp; 
										&nbsp;</TD>
									<TD class="Content" vAlign="top">
										&nbsp;</TD>
									<TD class="Content" id="DisplayImage" align="right" valign="top" width="100%">
										<asp:Panel id="pImage" Runat="server">
											<asp:HyperLink id="lnkImage2" Runat="server" NavigateUrl='<%# me.DetailLink%>'>
												<%# me.DetailLinkDisplay%>
											</asp:HyperLink>
										</asp:Panel>
									</TD>
								</TR>
								<TR>
									<TD class="Content" colspan="6"><hr width="100%">
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Right" Position="TopAndBottom" CssClass="Content" Mode="NumericPages"></PagerStyle>
</asp:datagrid>

<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="CAttributeControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="BuildYourOwnControl.ascx.vb" Inherits="StoreFront.StoreFront.BuildYourOwnControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table id="productTable" Runat="server" cellpadding="0" cellspacing="0" border="0">
	<tr>
		<td>
			<asp:DataList ID="productList" Runat="server" CellPadding="10" RepeatDirection="Horizontal" ItemStyle-VerticalAlign="Top"
				ItemStyle-Width="30%" RepeatColumns="3">
				<ItemTemplate>
					<a href="BYODetail.aspx?id=<%#DataBinder.Eval(Container.Dataitem, "ProductID")%>">					
					<asp:Image ID="productImage" Runat="server" CssClass="Content" ImageAlign="AbsMiddle" ImageUrl='<%# "../" & DataBinder.Eval(Container.DataItem, "Product.SmallImage")%>'>
					</asp:Image></a>
					<br>
					<table width="100%" border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td class="Content" valign="top">
								<asp:CheckBox ID="checkProduct" Runat="server" CssClass="Content" >
								</asp:CheckBox>
							</td>
							<td style="PADDING-TOP: 4px" class="Content" width="100%"  valign="top" style="TEXT-ALIGN: left">
								<a href="BYODetail.aspx?id=<%#DataBinder.Eval(Container.Dataitem, "ProductID")%>" border="0"><%#DataBinder.Eval(Container.Dataitem, "Product.Name")%></a>
							</td>
						</tr>
						<%-- Tee 8/29/2007 product configurator --%>
						<tr>
							<td class="Content" colspan="2">
								<%#DataBinder.Eval(Container.Dataitem, "Product.ShortDescription")%>
							</td>
						</tr>
						<%-- end Tee --%>
					</table>
					<br>
					<%-- Tee 8/21/2007 product configurator --%>
					<uc1:CAttributeControl id="CAttributeControl1" runat="server"></uc1:CAttributeControl>
					<%-- end Tee --%>
					<asp:Label ID="StockMessage" CssClass="Content" Runat="server" Visible="False">Temporarily out of stock</asp:Label>
					<asp:TextBox ID="ProdID" Runat="server" CssClass="Content" Text='<%#DataBinder.Eval(Container.Dataitem, "ProductID")%>' Visible="False">
					</asp:TextBox>
				</ItemTemplate>
			</asp:DataList>
		</td>
	</tr>
</table>

<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductBundleControl.ascx.vb" Inherits="StoreFront.StoreFront.ProductBundleControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1" height="19"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4" height="19">&nbsp;&nbsp;Products 
				Selected for Bundle&nbsp; <input id="hidProdId" type="hidden" runat="server"> <input id="hidPrice" type="hidden" runat="server">
				<input id="hidTotalPrice" type="hidden" runat="server"> <input id="hidTempPrice" type="hidden" runat="server" value="0">
				<input id="hidBundleCount" type="hidden" runat="server"> <input id="hidTempCount" type="hidden" runat="server" value="0">
			</TD>
			<TD class="ContentTableHeader" width="1" height="19"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" colSpan="4"><asp:datalist id="bundleProductList" Runat="server" CssClass="Content" Width="100%">
					<HeaderTemplate>
						<TABLE CellPadding="0" Width="100%">
							<tr>
								<td class="Content" align="left" width="20%">
									Product Code
								</td>
								<td class="Content" align="left" width="30%">
									Product Name
								</td>
								<td class="Content" align="Left" width="15%">
									Price
								</td>
								<td class="Content" align="left" width="15%">
									Bundle Quantity
								</td>
								<td class="Content" align="left" width="20%">
									Bundle Display Order
								</td>
							</tr>
						</TABLE>
					</HeaderTemplate>
					<ItemTemplate>
						<TABLE ID="productTable1" CellPadding="0" Width="100%" border="0">
							<tr>
								<td class="Content" align="left" width="20%">&nbsp;&nbsp;
									<%# DataBinder.Eval(Container.DataItem, "Product.Code") %>
									<input type=hidden runat=server id=hidBundleId value='<%# DataBinder.Eval(Container.DataItem, "Product.UID") %>'>
								</td>
								<td class="Content" align="left" width="30%">
									<asp:Label ID="ProductName1" CssClass="Content">
										<%#DataBinder.Eval(Container.DataItem, "Product.Name")%>
									</asp:Label>
								</td>
								<td class="Content" align="left" width="15%">
									<%# SingleBundlePrice(DataBinder.Eval(Container.DataItem, "Product.Price"), DataBinder.Eval(Container.DataItem, "Quantity")) %>
								</td>
								<td class="Content" align="left" width="15%">
									<asp:TextBox ID="quantity1" MaxLength="2" Columns="2" CssClass="content" Runat="server" Text='<%# DataBinder.Eval(Container.dataItem, "Quantity") %>'>
									</asp:TextBox>
								</td>
								<td class="Content" align="Left" width="20%">
									<asp:TextBox CssClass="Content" ID="txtDisplayOrder" Runat="server" Width="50" Text='<%# dataBinder.Eval(Container.dataItem, "DisplayOrder") %>'>
									</asp:TextBox>
								</td>
							</tr>
						</TABLE>
					</ItemTemplate>
				</asp:datalist></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr>
			<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colSpan="3"><asp:linkbutton id="selectProducts" Runat="server">
					<asp:Image ImageAlign="AbsMiddle" ID="imgSelectProducts" ImageUrl="../images/select.jpg" Runat="server"
						BorderWidth="0"></asp:Image>
				</asp:linkbutton></td>
			<td class="Content" align="center" width="15%"><asp:linkbutton id="btnSaveBundle" Runat="server">
					<img src="images/save.jpg" border="0">
				</asp:linkbutton></td>
			<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
	</TBODY></TABLE>

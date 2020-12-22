<%@ Control Language="vb" AutoEventWireup="false" Codebehind="BundleTemplateControl.ascx.vb" Inherits="StoreFront.StoreFront.BundleTemplateControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input id="hdnID" type="hidden" name="hdnID" runat="server"> <input id="hidModified" type="hidden" runat="server">
<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
	<TR>
		<TD class="Content" colSpan="6" height="10"></TD>
	</TR>
	<TR>
		<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
	</TR>
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="Content" align="left" colSpan="3" height="10"><asp:datalist id="stepList" Runat="server" CssClass="Content" Width="100%">
				<ItemTemplate>
					<TABLE ID="stepListTable" CellPadding="0" Width="100%" border="0">
						<tr>
							<td class="Content" align="left" width="50%" nowrap>
								Step Name:
								<asp:TextBox ID="txtStep" CssClass="Content" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "StepName")%>'>
								</asp:TextBox><input type="hidden" id="hidProdCount" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ProductDetails.Count") %>'>
							</td>
							<td class="Content" align="left" width="30%" nowrap>
								Total Selectable:
								<asp:TextBox ID="txtSelectable" CssClass="Content" Runat="server" Columns="8" Text='<%#DataBinder.Eval(Container.DataItem, "SelectableQuantity")%>'>
								</asp:TextBox>
							</td>
							<td class="Content" align="left" width="20%" nowrap>
								Display Order:
								<asp:TextBox ID="txtDisplayOrder" CssClass="Content" Runat="server" Columns="6" Text='<%#DataBinder.Eval(Container.DataItem, "DisplayOrder")%>'>
								</asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="Content" height="20" colspan="3">&nbsp;</td>
						</tr>
						<tr>
							<td class="content" colspan="3" align="left" width="100%">
								<asp:DataList ID="ProductList" Runat="server" CssClass="Content" Width="100%" DataSource='<%# DataBinder.Eval(Container.DataItem, "ProductDetails") %>'>
									<HeaderTemplate>
										<TABLE ID="productTable2" CellPadding="0" Width="100%">
											<tr>
												<td class="Content" align="left" width="20%">
													<b>Product Code</b>
												</td>
												<td class="Content" align="left" width="30%">
													<b>Product Name</b>
												</td>
												<td class="content" align="left" width="15%">
													<b>Price</b>
												</td>
												<td class="Content" align="left" width="15%">
													<b>Bundle Quantity</b>
												</td>
												<td class="Content" align="left" width="20%">
													<b>Display Order</b>
												</td>
											</tr>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE ID="productTable1" CellPadding="0" Width="100%">
											<tr>
												<td class="Content" align="left" width="20%">
													&nbsp;<%# DataBinder.Eval(Container.DataItem, "Product.ProductCode") %>
												</td>
												<td class="Content" align="left" width="30%">
													<%#DataBinder.Eval(Container.DataItem, "Product.Name")%>
												</td>
												<td class="Content" align="left" width="15%">
													<%# GetPrice(DataBinder.Eval(Container.DataItem, "ProductID"), DataBinder.Eval(Container.DataItem, "Product.ProductType"), DataBinder.Eval(Container.DataItem, "Product.Price")) %>
												</td>
												<td class="Content" align="left" width="15%">
													<input type="hidden" id="hidProdId" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>'>
													<asp:TextBox ID="quantity" MaxLength="2" Columns="2" CssClass="content" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>'>
													</asp:TextBox>
												</td>
												<td class="Content" align="left" width="20%">
													<asp:TextBox ID="tbDisplayOrder" Runat="server" CssClass="Content" Width="35" Text='<%# DataBinder.Eval(Container.DataItem, "DisplayOrder") %>'>
													</asp:TextBox>
												</td>
											</tr>
										</TABLE>
									</ItemTemplate>
								</asp:DataList>
							</td>
						</tr>
						<tr>
							<td class="Content" height="20" colspan="3">&nbsp;</td>
						</tr>
						<tr>
							<td class="content" align="right">&nbsp;
							</td>
							<td class="Content" align="right" colspan="2">
								<asp:linkbutton id="selectProducts" Runat="server" CommandName='<%#DataBinder.Eval(Container.DataItem, "StepID")%>' OnClick="SelectProductsClick">
									<asp:Image ID="imgSelectProducts" ImageUrl="../images/select.jpg" Runat="server" BorderWidth="0"></asp:Image>
								</asp:linkbutton>
								<asp:LinkButton ID="btnDeleteStep" Runat="server" CssClass="Content" CommandName='<%#DataBinder.Eval(Container.DataItem, "StepID")%>' OnClick="DeleteStep">
									<asp:Image ID="imgDeleteStep" CssClass="Content" Runat="server" ImageUrl="../images/delete.jpg"></asp:Image>
								</asp:LinkButton>
								<asp:LinkButton ID="btnSaveStep" Runat="server" CssClass="Content" CommandName='<%#DataBinder.Eval(Container.DataItem, "StepID")%>'>
									<asp:Image ID="Image1" CssClass="Content" Runat="server" ImageUrl="../images/save.jpg"></asp:Image>
								</asp:LinkButton>&nbsp;
							</td>
						</tr>
					</TABLE>
					<hr>
				</ItemTemplate>
				<FooterTemplate>
					<table cellpadding="0" width="100%" border="0">
						<tr>
							<td class="Content" width="50%">
								Step Name:
								<asp:TextBox ID="txtStepName" Runat="server" CssClass="Content"></asp:TextBox><FONT color="#ff0000">*</FONT>
							</td>
							<td class="Content" width="30%">
								Display Order:
								<asp:TextBox ID="txtDisplayOrd" Runat="server" CssClass="Content" Columns="8"></asp:TextBox><FONT color="#ff0000">*</FONT></td>
							<td class="Content" width="20%">
								<asp:LinkButton ID="btnAddNewStep" Runat="server" CssClass="Content" OnClick="AddNewStep">
									<asp:Image ID="imgAddNew" CssClass="Content" Runat="server" ImageUrl="../images/add_new.jpg"
										ImageAlign="AbsMiddle"></asp:Image>
								</asp:LinkButton>
							</td>
						</tr>
					</table>
				</FooterTemplate>
			</asp:datalist></td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
	</TR>
	<TR>
		<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="4" height="10"></TD>
		<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="4" align="left">
			<asp:linkbutton id="btnSaveAll" Runat="server">
				<img border="0" src="images/save.jpg">
			</asp:linkbutton>
		</TD>
		<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
</table>

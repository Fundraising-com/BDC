<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRProducts.ascx.vb" Inherits="StoreFront.StoreFront.CSRProducts" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="../controls/CSRAttributes.ascx" %>
<asp:datalist id="packages" runat="server" RepeatLayout="Table" Width="100%" CellSpacing="0">
	<ItemTemplate>
		<TR>
			<TD class="Headings" align="left" width="1" colspan="2">Package&nbsp;<%# Container.itemindex + 1 %><input type=hidden runat=server id="hdnOrderAddressIndex" value='<%# Container.ItemIndex %>' NAME="hdnOrderAddressIndex"></TD>
		</TR>
		<TR>
			<TD class="Content" align="left" valign="top">
				<asp:datalist class="ContentTableHeader" Border="0" RepeatLayout="Table" Width="100%" CellSpacing="0"
					CellPadding="0" id="Cart" runat="server">
					<HeaderTemplate>
						<TR>
							<TD class="ContentTableHeader" align="left" width="1"><IMG src="images/clear.gif" width="1"></TD>
							<TD class="ContentTableHeader" align="left" width="40%" style="padding-left:3px;">
								Style/Product&nbsp;
							</TD>
							<TD class="ContentTableHeader" align="left" width="15%">Qty&nbsp;</TD>
							<TD class="ContentTableHeader" align="left" width="15%">Price&nbsp;</TD>
							<TD class="ContentTableHeader" align="left" width="15%">Amount&nbsp;</TD>
							<TD class="ContentTableHeader" align="left" width="15%">
								Options&nbsp;
							</TD>
							<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1">
							</TD>
						</TR>
					</HeaderTemplate>
					<ItemTemplate>
						<TR>
							<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
							<TD class="Content" noWrap align="left">
								<TABLE id="tblProductName" width="100%" cellSpacing="0" cellPadding="0" border="0">
									<TR>
										<TD class="Content" noWrap style="padding:3px;">
											<%#  DataBinder.Eval(Container.DataItem,"ProductCode") %>
											-
											<%#  DataBinder.Eval(Container.DataItem,"Name") %>
										</TD>
									</TR>
									<TR>
										<TD class="Content" align="left" style="padding-left:3px;"><%#  DataBinder.Eval(Container.DataItem,"AttributeString") %></TD>
									</TR>
								</TABLE>
							</TD>
							<TD class="Content" align="left" noWrap><%#  DataBinder.Eval(Container.DataItem,"Quantity") %>
								<asp:Label ID="BackOrder" visible='<%# IIf(DataBinder.Eval(Container.DataItem,"BOQuantity")=0, false, true) %>' Runat="server">&nbsp;(Total)<br>(<%#  DataBinder.Eval(Container.DataItem,"BOQuantity") %> backordered -
<asp:Linkbutton ID="RemoveBackorder" Runat=server OnClick="RemoveBackorder" CommandArgument='<%# Container.ItemIndex %>' commandname='<%#  DataBinder.Eval(Container.DataItem,"ParentIndex") %>'>
											Remove?</asp:Linkbutton>)</asp:Label></TD>
							<TD class="Content" align="left" noWrap><%#  PriceDisplay2(DataBinder.Eval(Container.DataItem,"ItemPrice")) %></TD>
							<TD class="Content" align="left" noWrap><%#  PriceDisplay2(DataBinder.Eval(Container.DataItem,"ItemTotal")) %></TD>
							<TD class="Content" noWrap align="left" style="padding:3px;">
								<asp:LinkButton ID="cmdDelete" Runat="server" OnClick="deleteRow" CommandArgument='<%# Container.ItemIndex %>' commandname='<%#  DataBinder.Eval(Container.DataItem,"ParentIndex") %>'>
									<asp:Image BorderWidth="0" ID="imgDelete" runat="server" ImageUrl="../images/remove.jpg" AlternateText="Delete"></asp:Image>
								</asp:LinkButton><br>
								<asp:HyperLink Visible='<%#  DataBinder.Eval(Container.DataItem,"IsGiftWrapable") %>' NavigateUrl='<%# "../CSRGiftWraps.aspx?OrderAddressIndex=" & DataBinder.Eval(Container.DataItem,"ParentIndex") & "&ItemIndex=" & Container.ItemIndex %>' Target=_blank ID="cmdGiftWrap" Runat="server">
									<asp:Image BorderWidth="0" ID="Image1" runat="server" ImageUrl="../images/gift_wrap.jpg" AlternateText="Gift Wrap"></asp:Image>
								</asp:HyperLink>
							</TD>
							<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1">
							</TD>
						</TR>
						<tr>
							<td class="ContentTableHeader" colSpan="7"><IMG height="1" src="images/clear.gif"></td>
						</tr>
					</ItemTemplate>
				</asp:datalist>
			</TD>
		</TR>
	</ItemTemplate>
</asp:datalist>
<TABLE class="contentTable" id="tblAddProduct" width="100%" cellSpacing="0" cellPadding="0"
	border="0">
	<TR>
		<TD class="content" noWrap colSpan="8" width="100%">&nbsp;
		</TD>
	</TR>
	<TR>
		<td class="ContentTableHeader" noWrap></td>
		<TD class="ContentTableHeader" align="left" noWrap width="20%"><asp:label id="Label2" Runat="server" Width="20">&nbsp;Add Products To Order</asp:label></TD>
		<td class="ContentTableHeader" align="left" width="30%">&nbsp;</td>
		<td class="ContentTableHeader" align="left" noWrap width="10%"><asp:label Visible="False" id="lblNewQty" Runat="server" Width="20">Qty&nbsp;</asp:label></td>
		<td class="ContentTableHeader" align="left" noWrap width="10%"><asp:label Visible="False" id="lblNewPrice" Runat="server" Width="20">Price&nbsp;</asp:label></td>
		<td class="ContentTableHeader" align="left" noWrap width="10%">&nbsp;</td>
		<td class="ContentTableHeader" align="left" noWrap width="20%"><asp:label Visible="False" id="lblNewOptions" Runat="server" Width="20">Options</asp:label></td>
		<td class="ContentTableHeader" noWrap></td>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap width="20%" style="padding-left:3px;">Input Code or SKU:<br>
			<asp:textbox id="NewSKU" Runat="server"></asp:textbox>&nbsp;<asp:linkbutton id="DisplayProduct" Runat="server">
				<asp:Image ImageUrl="../images/icon_go.gif" Runat="server" ImageAlign="AbsBottom"></asp:Image>
			</asp:linkbutton><br>
			<a href="javascript:Search()">Search Product Inventory</a>
		</TD>
		<td class="Content" nowrap align="left" width="30%">&nbsp;&nbsp;
			<asp:Label ID="NoProdMessage" Runat="server">No matches were found.  Proceed to <a href="javascript:Search()">
					search results</a>?</asp:Label>
			<asp:Label ID="NewProdName" Runat="server" Visible="False"></asp:Label>
		</td>
		<td class="Content" nowrap align="left" width="10%">
			<asp:TextBox ID="NewQuantity" Width="30" Runat="server" Visible="False"></asp:TextBox>
		</td>
		<td class="Content" nowrap align="left" width="10%">
			<asp:TextBox ID="NewPrice" Width="60" Runat="server" Visible="False"></asp:TextBox>
			<input type="hidden" id="OldPrice" runat="server" size="1"> <input type="hidden" id="hdnProdID" runat="server" size="1">
		</td>
		<td class="Content" nowrap align="left" width="20%">
			<uc1:cattributecontrol id="CAttributeControl1" runat="server" Visible="False"></uc1:cattributecontrol>
		</td>
		<td class="Content" nowrap align="left" width="10%">
			<asp:LinkButton id="AddProduct" Runat="server" Visible="False">
				<asp:Image ID="imgAddProduct" Runat="server" ImageUrl="../images/add.jpg"></asp:Image>
			</asp:LinkButton></td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<tr>
		<td class="ContentTableHeader" colSpan="8"><IMG height="1" src="images/clear.gif"></td>
	</tr>
</TABLE>
<br>

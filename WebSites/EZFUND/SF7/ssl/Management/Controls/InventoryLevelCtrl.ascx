<%@ Control Language="vb" AutoEventWireup="false" Codebehind="InventoryLevelCtrl.ascx.vb" Inherits="StoreFront.StoreFront.InventoryLevelCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%" align="center">
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="10%">&nbsp;Valid</TD>
		<TD class="ContentTableHeader" width="30%">&nbsp;Inventory Levels</TD>
		<TD class="ContentTableHeader" width="20%">&nbsp;Qty In Stock</TD>
		<TD class="ContentTableHeader" width="20%">&nbsp;Low Qty Flag</TD>
		<TD class="ContentTableHeader" width="20%">&nbsp;SKU</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="content" width="100%" colSpan="5"><asp:datalist id="dlInventory" CellPadding="0" Width="100%" Runat="server" CellSpacing="0">
				<ItemTemplate>
					<TABLE cellSpacing="0" id="tblItems" runat="server" cellPadding="0" width="100%" align="center">
						<TR>
							<TD class="content" align="left">
								<asp:textbox id="txtuid" Runat="server" Visible=False CssClass="content" Text='<%#DataBinder.Eval(Container.DataItem,"uid")%>'>
								</asp:textbox></TD>
							<TD class="content" noWrap align="left" width="10%"><asp:checkbox id="chkValid" Runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"Valid")%>'>
								</asp:checkbox></TD>
							<TD class="content" noWrap align="left" width="30%">&nbsp;<%#DataBinder.Eval(Container.DataItem,"Name")%></TD>
							<TD class="content" align="left" width="20%">&nbsp;
								<asp:textbox id="txtQty" Runat="server" CssClass="content" Width="72px" Text='<%#DataBinder.Eval(Container.DataItem,"Quantity")%>'>
								</asp:textbox></TD>
							<TD class="content" align="left" width="20%">&nbsp;
								<asp:textbox id=txtQtyLowFlag Runat="server" Width="72px" Text='<%#DataBinder.Eval(Container.DataItem,"Quantity_Low_Flag")%>' CssClass="content">
								</asp:textbox></TD>
							<TD class="content" align="left" width="20%">&nbsp;
								<asp:textbox id="txtSku" Runat="server" Width="72px" Text='<%#DataBinder.Eval(Container.DataItem,"Sku")%>' CssClass="content">
								</asp:textbox>
							</TD>
						</TR>
						<TR>
							<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
						</TR>
					</TABLE>
				</ItemTemplate>
			</asp:datalist></td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="Content" colSpan="7"><IMG height="5" src="images/clear.gif"></TD>
	</TR>
	<TR>
		<TD class="Content" align="right" colSpan="7">
			<asp:LinkButton ID="cmdSave" Runat="server">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</TD>
	</TR>
</table>

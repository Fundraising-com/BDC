<%@ Control Language="vb" AutoEventWireup="false" Codebehind="InventoryLevelCtrl.ascx.vb" Inherits="StoreFront.StoreFront.InventoryLevelCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%" align="center">
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="33%">&nbsp;Inventory Levels</TD>
		<TD class="ContentTableHeader" width="33%">Qty In Stock</TD>
		<TD class="ContentTableHeader" width="34%">Low Qty Flag</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="content" width="100%" colSpan="3"><asp:datalist id="dlInventory" CellPadding="0" Width="100%" Runat="server" CellSpacing="0">
				<ItemTemplate>
					<TABLE cellSpacing="0" id="tblItems" runat="server" cellPadding="0" width="100%" align="center">
						<TR>
							<TD class="content" align="left">
								<asp:textbox id="txtuid" Runat="server" Visible=False CssClass="content" Text='<%#DataBinder.Eval(Container.DataItem,"uid")%>'>
								</asp:textbox></TD>
							<TD class="content" noWrap align="left" width="33%">&nbsp;<%#DataBinder.Eval(Container.DataItem,"Name")%></TD>
							<TD class="content" align="left" width="33%">&nbsp;
								<asp:textbox id="txtQty" Runat="server" CssClass="content" Width="72px" Text='<%#DataBinder.Eval(Container.DataItem,"Quantity")%>'>
								</asp:textbox></TD>
							<TD class="content" align="left" width="34%">&nbsp;
								<asp:textbox id="txtQtyLowFlag" Runat="server" CssClass="content" Width="72px" Text='<%#DataBinder.Eval(Container.DataItem,"Quantity_Low_Flag")%>'>
								</asp:textbox></TD>
						</TR>
						<TR>
							<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
						</TR>
					</TABLE>
				</ItemTemplate>
			</asp:datalist></td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="Content" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
	</TR>
	<TR>
		<TD class="Content" align="right" colSpan="5">
			<asp:LinkButton ID="cmdSave" Runat="server">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</TD>
	</TR>
</table>

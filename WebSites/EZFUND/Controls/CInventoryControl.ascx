<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CInventoryControl.ascx.vb" Inherits="StoreFront.StoreFront.CInventoryControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE class="ContentTableHeader" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR class="ContentTableHeader" vAlign="top">
		<TD class="ContentTableHeader">&nbsp;&nbsp;<asp:Label id="lblStockInfo" runat="server">Stock Status</asp:Label>
		</TD>
	</TR>
	<TR class="Content" vAlign="top">
		<TD class="Content">
			<asp:datalist id="dlInventory" runat="server" HorizontalAlign="Center" width="100%" CellPadding="0" BorderWidth="0px">
				<ItemStyle Wrap="False" CssClass="Content" VerticalAlign="Top"></ItemStyle>
				<ItemTemplate>
					<TABLE class="Content" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR class="Content">
							<TD class="Content" noWrap align="left" width="50%"><%#DataBinder.Eval(Container.DataItem,"Name")%></TD>
							<TD class="Content" noWrap align="left" width="50%">&nbsp;<%#DataBinder.Eval(Container.DataItem,"Display_Message")%></TD>
						</TR>
					</TABLE>
				</ItemTemplate>
			</asp:datalist></TD>
	</TR>
</TABLE>

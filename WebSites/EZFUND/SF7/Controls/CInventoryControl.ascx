<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CInventoryControl.ascx.vb" Inherits="StoreFront.StoreFront.CInventoryControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE cellSpacing="0" cellPadding="0" width="50%" border="0">
	<TR>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="ContentTableHeader">&nbsp;</TD>
		<TD class="ContentTableHeader">&nbsp;<asp:Label id="lblStockInfo" runat="server">Stock Status</asp:Label></TD>
		<TD class="ContentTableHeader">&nbsp;</TD>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR class="Content" vAlign="top">
		<TD colspan="5" class="Content">
			<asp:datalist id="dlInventory" runat="server" HorizontalAlign="Center" width="100%" CellPadding="0" BorderWidth="0px">
				<ItemStyle Wrap="False" CssClass="Content" VerticalAlign="Top"></ItemStyle>
				<ItemTemplate>
					<TABLE class="Content" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR class="Content">
						    <td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
						    <TD class="Content">&nbsp;</TD>
							<TD class="Content" noWrap align="center" width="50%"><%#DataBinder.Eval(Container.DataItem,"Name")%></TD>
							<TD class="Content" noWrap width="50%" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem,"Display_Message")%></TD>
							<TD class="Content">&nbsp;</TD>
							<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
						</TR>
					</TABLE>
				</ItemTemplate>
			</asp:datalist>
		</TD>
	</TR>
	<TR>
		<td colspan="5" class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
</TABLE>

<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductSalesStats.ascx.vb" Inherits="StoreFront.StoreFront.ProductSalesStats" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<TD width="50%" class="headings">Best Selling Products</TD>
		<td width="1" class="contenttable"><IMG width="1" src="images/clear.gif"></td>
		<td width="50%" class="headings">Worst Selling Products</td>
	</tr>
	<tr>
		<td class="content" width="50%">
			<asp:DataList id="DlBest" runat="server" CellPadding="0" Width="100%" ShowHeader="False" ShowFooter="False">
				<ItemStyle Wrap="False"></ItemStyle>
				<ItemTemplate>
					<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR>
							<TD class="content" width="1"><IMG height="5" src="images/clear.gif"></TD>
						</TR>
						<TR>
							<TD class="content" noWrap align="left" width="70%">
								<asp:Label id="BestName" Runat="server">
									&nbsp;<%#DataBinder.Eval(Container.DataItem,"Name")%>
								</asp:Label></TD>
							<TD class="content">&nbsp;
							</TD>
							<TD class="content" align="center" width="30%">
								<asp:Label id="BestQty" Runat="server">
									<%#DataBinder.Eval(Container.DataItem,"QuantitySold")%>
								</asp:Label>
							</TD>
						</TR>
					</TABLE>
				</ItemTemplate>
				<HeaderStyle Wrap="False"></HeaderStyle>
			</asp:DataList>
		</td>
		<td width="1" class="contenttable"><IMG width="1" src="images/clear.gif"></td>
		<td class="content" width="50%">
			<asp:DataList id="dlWorst" runat="server" CellPadding="0" Width="100%">
				<SelectedItemStyle Wrap="False"></SelectedItemStyle>
				<EditItemStyle Wrap="False"></EditItemStyle>
				<AlternatingItemStyle Wrap="False"></AlternatingItemStyle>
				<ItemStyle Wrap="False"></ItemStyle>
				<ItemTemplate>
					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
						<tr>
							<td class="content" width="1"><IMG height="5" src="images/clear.gif"></td>
						</tr>
						<tr>
							<td align="left" width="70%" class="content" nowrap>
								<asp:Label ID="WorstName" Runat="server">
									&nbsp;<%#DataBinder.Eval(Container.DataItem,"Name")%>
								</asp:Label>
							</td>
							<td class="content">&nbsp;
							</td>
							<td align="center" width="30%" class="content">
								<asp:Label ID="WorstQty" Runat="server">
									<%#DataBinder.Eval(Container.DataItem,"QuantitySold")%>
								</asp:Label>
							</td>
							<td class="content">&nbsp;
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:DataList>
		</td>
	</tr>
	<tr>
		<td class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
	</tr>
</table>

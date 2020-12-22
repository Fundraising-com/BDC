<%@ Control Language="vb" AutoEventWireup="false" Codebehind="OrderResultsControl.ascx.vb" Inherits="StoreFront.StoreFront.OrderResultsControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:datagrid id="DataGrid1" runat="server" CellPadding="0" BorderWidth="0px" Width="100%" AllowPaging="True" ShowHeader="False" AutoGenerateColumns="False" PageSize="10">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<TABLE id="HistoryTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
					<TR>
						<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="ContentTableHeader">&nbsp;</TD>
						<TD class="ContentTableHeader" vAlign="center" noWrap align="middle" width="20%">Order 
							ID&nbsp;</TD>
						<TD class="ContentTableHeader" vAlign="center" noWrap align="middle" width="20%">Date&nbsp;</TD>
						<TD class="ContentTableHeader" vAlign="center" noWrap align="middle" width="20%">Customer 
							Name&nbsp;
						</TD>
						<TD class="ContentTableHeader" vAlign="center" noWrap align="middle" width="20%">Status&nbsp;
						</TD>
						<TD class="ContentTableHeader" vAlign="center" noWrap align="middle" width="20%">Action</TD>
						<TD class="ContentTableHeader">&nbsp;</TD>
						<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="Content" colSpan="7">&nbsp;</TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="Content" vAlign="top" noWrap align="middle" width="20%"><%# DataBinder.Eval(Container.DataItem,"OrderNumber") %></TD>
						<TD class="Content" vAlign="top" noWrap align="middle" width="20%"><%# DataBinder.Eval(Container.DataItem,"OrderDate") %></TD>
						<TD class="Content" vAlign="top" noWrap align="middle" width="20%"><%# DataBinder.Eval(Container.DataItem,"CustomerName") %></TD>
						<TD class="Content" vAlign="top" noWrap align="right" width="20%"><%# DataBinder.Eval(Container.DataItem,"Status") %></TD>
						<TD class="Content" vAlign="center" align="right" width="20%">
							<asp:LinkButton ID="cmdDetails" Runat="server" OnClick=GetStatus CommandArgument='<%# DataBinder.Eval(Container.DataItem,"OrderNumber") %>'>
								<asp:Image BorderWidth="0" ID="imgDetails" runat="server" ImageUrl="../images/view.jpg" AlternateText="Details"></asp:Image>
							</asp:LinkButton>
							<BR>
						</TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="Content" colSpan="7"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="Content" vAlign="top" noWrap align="middle" width="20%">&nbsp;</TD>
						<TD class="Content" vAlign="top" noWrap align="middle" width="20%">&nbsp;</TD>
						<TD class="Content" vAlign="top" noWrap align="middle" width="20%">&nbsp;</TD>
						<TD class="Content" vAlign="top" noWrap align="right" width="20%">&nbsp;</TD>
						<TD class="Content" vAlign="center" align="right" width="20%">
							<asp:LinkButton ID="cmdCancelOrder" Runat="server" onclick=Cancel CommandArgument='<%# DataBinder.Eval(Container.DataItem,"OrderNumber") %>'>
								<asp:Image BorderWidth="0" ID="imgCancelOrder" runat="server" ImageUrl="../images/cancel.jpg" AlternateText="Cancel"></asp:Image>
							</asp:LinkButton>
						</TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="Content" colSpan="7"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" colSpan="8" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Right" Position="TopAndBottom" CssClass="Content" Wrap="False" Mode="NumericPages"></PagerStyle>
</asp:datagrid>

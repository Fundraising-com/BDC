<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SalesStatsControl.ascx.vb" Inherits="StoreFront.StoreFront.SalesStatsControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table height="205" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Total Days:
		</td>
		<td class="content" noWrap align="center" width="50%"><asp:label id="lblTotalDays" runat="server"></asp:label>&nbsp;</td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Total Number of Orders:
		</td>
		<td class="content" noWrap align="center" width="50%"><asp:label id="lblTotalOrders" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Average Daily Orders:
		</td>
		<td class="content" noWrap align="center" width="50%"><asp:label id="lblAveDailyOrders" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Average Daily Units Sold:
		</td>
		<td class="content" noWrap align="center" width="50%"><asp:label id="lblAveDailyUnits" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Average Daily Sales:
		</td>
		<td class="content" noWrap align="center" width="50%"><asp:label id="lblAveDailySales" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Average Amount Per Order:
		</td>
		<td class="content" noWrap align="right" width="50%"><asp:label id="lblAvePerOrder" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%"><asp:label id="ee" CssClass="Headings" Runat="server">&nbsp;</asp:label>Average 
			Price Per Unit Sold:
		</td>
		<td class="content" noWrap align="right" width="50%"><asp:label id="lblAvePriceperUnit" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></td>
	</tr>
</table>

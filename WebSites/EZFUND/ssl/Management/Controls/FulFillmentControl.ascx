<%@ Control Language="vb" AutoEventWireup="false" Codebehind="FulFillmentControl.ascx.vb" Inherits="StoreFront.StoreFront.FulFillmentControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="Content" colSpan="6">
			<P id="ErrorAlignment" runat="server">
				<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
			<P id="MessageAlignment" runat="server">
				<asp:Label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:Label>
			</P>
		</td>
	</tr>
	<tr>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="ContentTableHeader" colSpan="4">&nbsp;View Orders
		</td>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="Content" colSpan="4">&nbsp;</td>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="left">&nbsp;Date Range:
		</td>
		<td class="content" align="left"><asp:dropdownlist id="ddDateRange" runat="server" CssClass="content" AutoPostBack="True">
				<asp:ListItem Selected="True" Value="1">Today</asp:ListItem>
				<asp:ListItem Value="2">Week To Date</asp:ListItem>
				<asp:ListItem Value="3">Month To Date</asp:ListItem>
				<asp:ListItem Value="4">Year To Date</asp:ListItem>
				<asp:ListItem Value="5">Enter a Date Range</asp:ListItem>
			</asp:dropdownlist></td>
		<TD class="content" noWrap align="left">&nbsp;From:&nbsp;<asp:textbox id="txtFrom" runat="server" CssClass="content" Width="97px" Enabled="False" MaxLength="12"></asp:textbox>&nbsp;
		</TD>
		<TD class="content" noWrap align="left">&nbsp;To:&nbsp;<asp:textbox id="txtTo" runat="server" CssClass="content" Width="97px" Enabled="False" MaxLength="12"></asp:textbox>&nbsp;
		</TD>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="left" colSpan="4">&nbsp;</td>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1">
		</td>
	</tr>
	<tr>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap colspan="2" align="left">&nbsp;Payment Status:&nbsp;
			<asp:dropdownlist id="ddPayments" runat="server" CssClass="content">
				<asp:ListItem Selected="True" Value="0">Select Payment Status</asp:ListItem>
				<asp:ListItem Value="1">Payment(s) Pending</asp:ListItem>
				<asp:ListItem Value="2">Payment(s) Collected</asp:ListItem>
			</asp:dropdownlist></td>
		<td class="content" colspan="2" noWrap align="right">&nbsp;Ship Status:&nbsp;
			<asp:dropdownlist id="ddShipments" runat="server" CssClass="content">
				<asp:ListItem Selected="True" Value="0">Select Shipping Status</asp:ListItem>
				<asp:ListItem Value="1">Shipments(s) Pending</asp:ListItem>
				<asp:ListItem Value="2">Shipments(s) Shipped</asp:ListItem>
			</asp:dropdownlist>&nbsp;&nbsp;&nbsp;
		</td>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	<tr>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="left" colSpan="4">&nbsp;</td>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" align="right" colSpan="4">
			<asp:LinkButton ID="cmdGetOrders" Runat="server">
				<asp:Image BorderWidth="0" ID="imgGetOrders" runat="server" ImageUrl="../images/submit.jpg" AlternateText="Submit"></asp:Image>
			</asp:LinkButton>
			&nbsp;</td>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="left" colSpan="4">&nbsp;</td>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
	</tr>
</table>
</TD></TR>
<tr>
	<td class="Content">
		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="ContentTableHeader" colSpan="4">&nbsp;Locate an Order
				</td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="Content" colSpan="4">&nbsp;</td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="Headings" colspan="3" noWrap align="left">&nbsp;Search By Order ID or 
					Customer Name:
				</td>
				<td class="content" align="left" width="100%"><asp:TextBox id="txtFree" CssClass="content" Width="75%" Runat="server"></asp:TextBox></td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" noWrap align="left" colSpan="4">&nbsp;</td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" align="middle" colSpan="4">
					<asp:LinkButton ID="cmdLocateOrder" Runat="server">
						<asp:Image BorderWidth="0" ID="imgLocateOrder" runat="server" ImageUrl="../images/submit.jpg" AlternateText="Submit"></asp:Image>
					</asp:LinkButton>
					&nbsp;</td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" noWrap align="left" colSpan="4">&nbsp;</td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
			</tr>
		</table>

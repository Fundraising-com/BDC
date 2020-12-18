<%@ Register TagPrefix="uc1" TagName="ShipStatusControl" Src="ShipStatusControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="OrderStatusControl.ascx.vb" Inherits="StoreFront.StoreFront.OrderStatusControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="HistoryTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" noWrap align="left" width="50%">&nbsp;&nbsp;Order 
			ID:&nbsp;<asp:Label CssClass="ContentTableHeader" Runat="server" ID="lblOrderID"></asp:Label></TD>
		<td class="contenttable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="ContentTableHeader" noWrap align="right" width="50%">Order Date:&nbsp;<asp:Label CssClass="ContentTableHeader" Runat="server" ID="lblOrderDate"></asp:Label>&nbsp;</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" vAlign="top">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="Headings" colSpan="5">&nbsp;Payments</td>
				</tr>
				<tr>
					<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				</tr>
				<tr>
					<td class="content" align="right">Primary:</td>
					<td class="content" align="right"><asp:label id="lblTotalPaid" Runat="server" CssClass="content"></asp:label></td>
					<td class="content" align="right"><asp:dropdownlist id="ddPrimaryPayments" runat="server" CssClass="content" AutoPostBack="True">
							<asp:ListItem Value="2">Paid</asp:ListItem>
							<asp:ListItem Value="1">Pending</asp:ListItem>
						</asp:dropdownlist></td>
					<td class="content" align="right">
						<asp:LinkButton ID="cmdSetPrimary" Runat="server" OnClick="Process" CommandName="0">
							<asp:Image BorderWidth="0" ID="imgSetPrimary" runat="server" ImageUrl="../images/process.jpg" AlternateText="Process"></asp:Image>
						</asp:LinkButton>
					</td>
				</tr>
				<tr>
					<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				</tr>
				<tr id="BOPayment" runat="server">
					<td class="content" align="right">BackOrder:</td>
					<td class="content" align="right"><asp:label id="lblRemaining" Runat="server" CssClass="content"></asp:label></td>
					<td class="content" align="right"><asp:dropdownlist id="ddBackOrderPayments" runat="server" CssClass="content" AutoPostBack="True">
							<asp:ListItem Value="2">Paid</asp:ListItem>
							<asp:ListItem Value="1">Pending</asp:ListItem>
						</asp:dropdownlist></td>
					<td class="content" align="right">
						<asp:LinkButton ID="cmdsetBackOrder" Runat="server" OnClick="Process" CommandName="1">
							<asp:Image BorderWidth="0" ID="imgsetBackOrder" runat="server" ImageUrl="../images/process.jpg" AlternateText="Process"></asp:Image>
						</asp:LinkButton>
					</td>
					<td class="content">&nbsp;</td>
				</tr>
			</table>
		</TD>
		<td class="contenttable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content" valign="top" align="middle">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<TD class="content" align="middle" valign="top">
						<uc1:ShipStatusControl id="ShipStatusControl1" runat="server"></uc1:ShipStatusControl>
					</TD>
				</tr>
			</table>
		</TD>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" colSpan="8" height="1"><IMG height="1" src="images/clear.gif"></TD>
	</TR>
</TABLE>

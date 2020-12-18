<%@ Control Language="vb" AutoEventWireup="false" Codebehind="FulFillmentControl.ascx.vb" Inherits="StoreFront.StoreFront.FulFillmentControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="Content" colSpan="6">
			<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></P>
			<P id="MessageAlignment" runat="server"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
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
		<TD class="content" noWrap align="left">&nbsp;From:&nbsp;<asp:textbox id="txtFrom" runat="server" CssClass="content" MaxLength="12" Enabled="False" Width="97px"></asp:textbox>&nbsp;
		</TD>
		<TD class="content" noWrap align="left">&nbsp;To:&nbsp;<asp:textbox id="txtTo" runat="server" CssClass="content" MaxLength="12" Enabled="False" Width="97px"></asp:textbox>&nbsp;
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
		<td class="content" noWrap align="left" colSpan="2">&nbsp;Payment Status:&nbsp;
			<asp:dropdownlist id="ddPayments" runat="server" CssClass="content">
				<asp:ListItem Selected="True" Value="0">Select Payment Status</asp:ListItem>
				<asp:ListItem Value="1">Payment(s) Pending</asp:ListItem>
				<asp:ListItem Value="2">Payment(s) Collected</asp:ListItem>
			</asp:dropdownlist></td>
		<td class="content" noWrap align="right" colSpan="2">&nbsp;Ship Status:&nbsp;
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
		<td class="content" noWrap align="left" colSpan="2">
			<asp:Panel ID="pnlUploadKey" Runat="server">
      <P>If you wish to see full credit card numbers,<BR>
					you will need to provide your private key.</P>Encryption Key:<INPUT id="fileUpload" type="file" name="fileUpload" runat="server">
			</asp:Panel>
		</td>
		<td class="content" vAlign="bottom" align="right" colSpan="2"><asp:linkbutton id="cmdGetOrders" Runat="server">
				<asp:Image BorderWidth="0" ID="imgGetOrders" runat="server" ImageUrl="../images/submit.jpg"
					AlternateText="Submit"></asp:Image>
			</asp:linkbutton>&nbsp;</td>
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
</TD></TR><tr>
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
				<td class="Headings" noWrap align="left" colSpan="3">&nbsp;Search By Order ID or 
					Customer Name:
				</td>
				<td class="content" align="left" width="100%"><asp:textbox id="txtFree" CssClass="content" Width="75%" Runat="server"></asp:textbox></td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" noWrap align="left" colSpan="4">&nbsp;</td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" align="center" colSpan="4"><asp:linkbutton id="cmdLocateOrder" Runat="server">
						<asp:Image BorderWidth="0" ID="imgLocateOrder" runat="server" ImageUrl="../images/submit.jpg"
							AlternateText="Submit"></asp:Image>
					</asp:linkbutton>&nbsp;</td>
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

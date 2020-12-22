<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ShipStatusControl.ascx.vb" Inherits="StoreFront.StoreFront.ShipStatusControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<td class="Headings" colSpan="5">&nbsp;Shipments
		</td>
	</tr>
</table>
<asp:datalist id="dlShipStatus" Runat="server">
	<ItemTemplate>
		<table cellSpacing="0" cellPadding="0" width="100%">
			<tr>
				<TD class="content" colspan="5" width="1"><IMG height="5" src="images/clear.gif"></TD>
			</tr>
			<tr>
				<td class="content">&nbsp;
					<asp:TextBox ID="AddID" Runat =server Visible =False Text='<%#DataBinder.Eval(Container.DataItem,"AddressID")%>'>
					</asp:TextBox>
					<asp:TextBox ID="OrderID" Runat =server Visible =False Text='<%#DataBinder.Eval(Container.DataItem,"OrderID")%>'>
					</asp:TextBox></td>
				<td class="content" align="right" nowrap width="100%">
					<%#DataBinder.Eval(Container.DataItem,"NickName")%>
				</td>
				<td class="content" align="right" valign="top">&nbsp;
					<asp:dropdownlist id="ddShipped" runat="server" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="SetShipping" AutoPostBack="True" CssClass="content">
						<asp:ListItem Value="2">Shipped</asp:ListItem>
						<asp:ListItem Value="1">Pending</asp:ListItem>
					</asp:dropdownlist>
				</td>
				<td class="content" align="right" valign="top">
					<asp:LinkButton ID="cmdSetHomeShipped" Runat="server" OnClick="ship" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"AddressID")%>' CommandName=0>
						<asp:Image BorderWidth="0" ID="imgSetHomeShipped" runat="server" ImageUrl="../images/icon_process_shipment.gif" AlternateText="Set Home Shipped"></asp:Image>
					</asp:LinkButton>
					&nbsp;</td>
				<td class="content" align="right" valign="top">
					<asp:LinkButton ID="cmdTrack" Runat="server" OnClick=TrackClick CommandName=0 CommandArgument='<%#DataBinder.Eval(Container.DataItem,"AddressID")%>'>
						<asp:Image BorderWidth="0" ID="imgTrack" runat="server" ImageUrl="../images/icon_track_shipment.gif" AlternateText="Track"></asp:Image>
					</asp:LinkButton>
					<asp:TextBox ID="txtTrackVisible" Runat="server" Visible="False" Text='<%#DataBinder.Eval(Container.DataItem,"TrackingNumber")%>'>
					</asp:TextBox></td>
				<td class="content">&nbsp;</td>
			</tr>
			<tr>
				<TD class="content" colspan="5" width="1"><IMG height="5" src="images/clear.gif"></TD>
			</tr>
			<tr id="BOShipRow" runat="server">
				<td class="content">&nbsp;
					<asp:TextBox ID=BOCount Runat =server Visible =False Text='<%#DataBinder.Eval(Container.DataItem,"BOQuantity")%>'>
					</asp:TextBox></td>
				<td class="content" align="right" nowrap width="100%">
					<%#DataBinder.Eval(Container.DataItem,"NickName")%>
					(Back Order)</td>
				<td class="content" align="right">&nbsp;
					<asp:dropdownlist id="ddBackOrderShipped" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="SetBOShipping" runat="server" AutoPostBack="True" CssClass="content">
						<asp:ListItem Value="2">Shipped</asp:ListItem>
						<asp:ListItem Value="1">Pending</asp:ListItem>
					</asp:dropdownlist>
				</td>
				<td class="content" align="right" valign="top">
					<asp:LinkButton ID="cmdSetBackOrderShipped" Runat="server" OnClick="ship" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"AddressID")%>' CommandName=1>
						<asp:Image BorderWidth="0" ID="imgSetBackOrderShipped" runat="server" ImageUrl="../images/icon_process_shipment.gif" AlternateText="Set BackOrder Shipped"></asp:Image>
					</asp:LinkButton>
					&nbsp;</td>
				<td class="content" align="right" valign="top">
					<asp:LinkButton ID="cmdBoTrack" Runat="server" OnClick =TrackClick CommandName=1 CommandArgument='<%#DataBinder.Eval(Container.DataItem,"AddressID")%>'>
						<asp:Image BorderWidth="0" ID="imgBoTrack" runat="server" ImageUrl="../images/icon_track_shipment.gif" AlternateText="Track"></asp:Image>
					</asp:LinkButton>
					<asp:TextBox ID="txtBOTrackVisible" Runat="server" Visible="False" Text='<%#DataBinder.Eval(Container.DataItem,"BOTrackingNumber")%>'>
					</asp:TextBox></td>
				<td class="content">&nbsp;</td>
			</tr>
			<tr id="rowSep" runat="server">
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</tr>
		</table>
	</ItemTemplate>
</asp:datalist>

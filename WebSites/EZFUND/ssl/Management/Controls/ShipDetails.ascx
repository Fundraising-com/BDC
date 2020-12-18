<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="../../Controls/AddressLabel.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ShipDetails.ascx.vb" Inherits="StoreFront.StoreFront.ShipDetails" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></P>
<P id="MessageAlignment" runat="server"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" noWrap align="left" width="50%">&nbsp;Order ID:<asp:label id="lblOrderID" CssClass="ContentTableHeader" Runat="server"></asp:label></td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" noWrap align="right" width="50%">Order Date:<asp:label id="lblDate" CssClass="ContentTableHeader" Runat="server"></asp:label>&nbsp;
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="Content" align="left" width="50%">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td width="100%"><IMG height="5" src="images/clear.gif"></td>
				</tr>
				<tr>
					<td class="Headings">&nbsp;Shipment Details
					</td>
				</tr>
				<tr>
					<td width="100%"><IMG height="5" src="images/clear.gif"></td>
				</tr>
				<tr>
					<td class="content" align="middle"><uc1:addresslabel id="AddressLabel1" runat="server"></uc1:addresslabel>
						<br>
						<asp:Label Runat="server" ID="lblEmail" CssClass="content"></asp:Label>
					</td>
				</tr>
				<tr>
					<td class="content" align="middle">&nbsp;</td>
				</tr>
				<tr>
					<td class="content" align="middle">
						<asp:LinkButton ID="cmdPackingSlip" Runat="server">
							<asp:Image BorderWidth="0" ID="imgPackingSlip" runat="server" ImageUrl="../images/PackingSlip.jpg" AlternateText="Packing Slip"></asp:Image>
						</asp:LinkButton>
					</td>
				</tr>
				<tr>
					<td class="content" align="middle">&nbsp;</td>
				</tr>
			</table>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="Content" align="right" width="50%">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td width="100%"><IMG height="5" src="images/clear.gif"></td>
				</tr>
				<tr>
					<td class="content" noWrap align="left">&nbsp;&nbsp;No of Items in Shipment:&nbsp;<asp:label id="lblItems" CssClass="content" Runat="server"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="content" noWrap align="left">&nbsp;&nbsp;Shipment Weight:<asp:label id="lblWeight" CssClass="content" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="content" noWrap align="left">&nbsp;&nbsp;Shipment Carrier:<asp:label id="lblCarrier" CssClass="content" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="content" noWrap align="left">&nbsp;&nbsp;Shipment Method:<asp:label id="lblMethod" CssClass="content" Runat="server"></asp:label>&nbsp;&nbsp;</td>
				</tr>
			</table>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
	</tr>
</table>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="Content" align="left" width="100%">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td colSpan="2"><IMG height="5" src="images/clear.gif"></td>
				</tr>
				<tr>
					<td class="Headings" colSpan="2">&nbsp;Tracking Details
					</td>
				</tr>
				<tr>
					<td colSpan="2"><IMG height="5" src="images/clear.gif"></td>
				</tr>
				<tr>
					<td class="content" noWrap align="right" width="159">&nbsp;Tracking Number:&nbsp;</td>
					<td class="content" noWrap>&nbsp;<asp:textbox id="txtTrackingNumber" CssClass="content" Runat="server" MaxLength="50"></asp:textbox></td>
				</tr>
				<tr>
					<td class="content" noWrap align="right" width="159">&nbsp;Date Sent:&nbsp;</td>
					<td class="content" noWrap>&nbsp;<asp:textbox id="txtSent" CssClass="content" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="content" noWrap align="right" width="159">&nbsp;Tracking Message:&nbsp;</td>
					<td class="content" noWrap>&nbsp;<asp:textbox id="txtMsg" CssClass="content" Runat="server" Height="54px" Width="326px" MaxLength="255"></asp:textbox>&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td class="content" noWrap align="right" colSpan="2">&nbsp;
					</td>
				</tr>
				<tr>
					<td class="content" noWrap align="right" colSpan="2">
						<asp:LinkButton ID="cmdShip" Runat="server">
							<asp:Image BorderWidth="0" ID="imgShip" runat="server" ImageUrl="../images/icon_process_shipment.gif" AlternateText="Process Shipment"></asp:Image>
						</asp:LinkButton>
						&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td colSpan="2"><IMG height="5" src="images/clear.gif"></td>
				</tr>
			</table>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
	</tr>
</table>

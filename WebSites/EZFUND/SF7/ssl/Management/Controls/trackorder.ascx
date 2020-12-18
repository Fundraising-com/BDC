<%@ Register TagPrefix="uc1" TagName="TrackingForOrder" Src="TrackingForOrder.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="trackorder.ascx.vb" Inherits="StoreFront.StoreFront.trackorder" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="../../Controls/AddressLabel.ascx" %>
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
					<td class="content" align="middle"><uc1:addresslabel id="AddressLabel1" runat="server"></uc1:addresslabel></td>
				</tr>
				<tr>
					<td class="content" align="middle">&nbsp;</td>
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
				<TBODY>
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
						<td colspan="2">
							<uc1:TrackingForOrder id="TrackingForOrder1" runat="server" Order='<%# databinder.eval(me,"Order")%>' AddressID='<%# databinder.eval(me,"AddressID")%>' >
							</uc1:TrackingForOrder></td>
		</td>
	</tr>
	<tr>
		<td colSpan="2"><IMG height="5" src="images/clear.gif"></td>
	</tr>
</table></TD>
<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD></TR>
<tr>
	<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
</tr></TBODY></TABLE>

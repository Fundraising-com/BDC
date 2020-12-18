<%@ Control Language="vb" AutoEventWireup="false" Codebehind="WSConfCtrl.ascx.vb" Inherits="StoreFront.StoreFront.WSConfCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TBODY>
		<TR>
			<TD class="ErrorMessages" noWrap align="left" colSpan="4"><asp:label id="lblMessage" Runat="server" CssClass="ErrorMessages"></asp:label></TD>
		</TR>
		<!-- Start Inventory Check -->
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="2">&nbsp;&nbsp;Inventory 
				Check Web Services&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" width="35%" colSpan="1">Check inventory 
				when item added to cart:</td>
			<td class="content" align="left" width="65%" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkAddedCart" Runat="server" Text=""></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" width="35%" colSpan="1">Check inventory 
				before placing order:</td>
			<td class="content" align="left" width="65%" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkPlaceOrder" Runat="server" Text=""></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" width="35%" colSpan="1">Web Service Url:</td>
			<td class="content" align="left" width="65%" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtInvWS" Width="80%" Runat="server"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<!-- End Inventory Check -->
		<!-- Start Submit Order -->
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="2">&nbsp;&nbsp;Submit 
				Order Web Service&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" width="35%" colSpan="1">Submit Order to 
				Web Service:</td>
			<td class="content" align="left" width="65%" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkSubmitOrder" Runat="server" Text=""></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" width="35%" colSpan="1">Web Service Url:</td>
			<td class="content" align="left" width="65%" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtOrderWS" Width="80%" Runat="server"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<!-- End Submit Order -->
		<!-- Start WS Error Notification -->
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="2">&nbsp;&nbsp;Web 
				Service Error Notifications&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" width="35%" colSpan="1">E-mail:</td>
			<td class="content" align="left" width="65%" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtEmail" Width="80%" Runat="server"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<!-- End WS Error Notification -->
		<TR>
			<td class="content" align="right" width="75%" colSpan="4"><asp:linkbutton id="cmdCancel" Runat="server">
					<asp:Image BorderWidth="0" ID="imgCancel" runat="server" ImageUrl="../images/cancel.jpg" AlternateText="Cancel"></asp:Image>
				</asp:linkbutton><asp:linkbutton id="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:linkbutton></td>
		</TR>
	</TBODY></TABLE>

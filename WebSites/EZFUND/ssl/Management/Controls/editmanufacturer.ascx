<%@ Control Language="vb" AutoEventWireup="false" Codebehind="editmanufacturer.ascx.vb" Inherits="StoreFront.StoreFront.editmanufacturer" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input id="txtIDHidden" type="hidden" runat="server">
<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></P>
<P id="MessageAlignment" runat="server"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" align="left" colSpan="2">&nbsp;Edit Manufacturer<asp:label id="lblCustomerHeader" CssClass="ContentTableHeader" Runat="server"></asp:label></td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right" width="1%">&nbsp;Manufacturer Name:&nbsp;</TD>
		<TD><asp:textbox id="txtName" runat="server" Width="310px" MaxLength="75"></asp:textbox></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
	</TR>
	<TR>
		<TD class="Content" colSpan="3" height="10"></TD>
	</TR>
	<TR>
		<td></td>
		<td align="right" colSpan="3"><asp:linkbutton id="cmdCancel" Runat="server">
				<asp:Image BorderWidth="0" ID="imgCancel" runat="server" ImageUrl="../images/cancel.jpg" AlternateText="Cancel"></asp:Image>
			</asp:linkbutton>&nbsp;
			<asp:linkbutton id="cmdSave" Runat="server">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:linkbutton></td>
		<td></td>
	</TR>
</table>

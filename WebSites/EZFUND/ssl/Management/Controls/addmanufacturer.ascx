<%@ Control Language="vb" AutoEventWireup="false" Codebehind="addmanufacturer.ascx.vb" Inherits="StoreFront.StoreFront.addmanufacturer" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input type="hidden" id="txtIDHidden" runat="server" NAME="txtIDHidden">
<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></P>
<P id="MessageAlignment" runat="server"><asp:label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:label></P>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" align="left" colspan="2">&nbsp;Add Manufacturer<asp:Label CssClass="ContentTableHeader" Runat="server" ID="lblCustomerHeader"></asp:Label></td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colspan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right" width="1%">&nbsp;Manufacturer Name:&nbsp;</TD>
		<TD><asp:textbox id="txtName" runat="server" Width="310px" MaxLength="75"></asp:textbox></TD>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" height="10" colspan="3"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
	</TR>
	<TR>
		<TD class="Content" height="10" colspan="3"></TD>
	</TR>
	<TR>
		<td></td>
		<td align="right" colspan="3">
			<asp:LinkButton ID="cmdCancel" Runat="server">
				<asp:Image BorderWidth="0" ID="imgCancel" runat="server" ImageUrl="../images/cancel.jpg" AlternateText="Cancel"></asp:Image>
			</asp:LinkButton>
			&nbsp;
			<asp:LinkButton ID="cmdAdd" Runat="server">
				<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</td>
		<td></td>
	</TR>
</table>

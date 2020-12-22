<%@ Control Language="vb" AutoEventWireup="false" Codebehind="addcustomer.ascx.vb" Inherits="StoreFront.StoreFront.addcustomer" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" align="left" colspan="2">&nbsp;Add Customer<asp:Label CssClass="ContentTableHeader" Runat="server" ID="lblCustomerHeader"></asp:Label></td>
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
		<TD class="Content" noWrap align="right" width="1%">&nbsp;First Name:&nbsp;</TD>
		<TD><asp:textbox id="txtCAFirstName" MaxLength="100" runat="server"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;Last Name:&nbsp;</TD>
		<TD><asp:textbox id="txtCALastName" MaxLength="100" runat="server"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colspan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;E-Mail Address:&nbsp;</TD>
		<TD><asp:textbox id="txtCAEMail" MaxLength="255" runat="server"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;Password:&nbsp;</TD>
		<TD><asp:textbox id="txtCAPassword" MaxLength="255" runat="server" TextMode="Password"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;Confirm Password:&nbsp;</TD>
		<TD><asp:textbox id="txtCAConfirmPassword" MaxLength="255" runat="server" TextMode="Password"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" height="10" colspan="3"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;Subscribe To Mail List:&nbsp;</TD>
		<TD><asp:checkbox id="chkSubscribe" runat="server"></asp:checkbox></TD>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" height="10" colspan="3"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<tr id="trCustomerGroups" class="content" runat="server">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;<asp:Label ID="lblPriceGroups" Runat="server">Price Group:</asp:Label>&nbsp;</TD>
		<td>
			<cc1:selectvalcontrol id="txtCustomerGroups" runat="server" DisplaySelect="CustomerGroups" ReadFromDB="True">
				<asp:ListItem Value="CustomerGroups">[CustomerGroups]</asp:ListItem>
				<asp:ListItem Value="CustomerGroups">[CustomerGroups]</asp:ListItem>
			</cc1:selectvalcontrol></td>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" colspan="2" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
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
		<TD align="right" colspan="9">
			<asp:LinkButton ID="cmdAdd" Runat="server">
				<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</TD>
	</TR>
</table>

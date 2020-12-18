<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="editcustomercontrol.ascx.vb" Inherits="StoreFront.StoreFront.editcustomercontrol" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input type="hidden" id="hdnID" runat="server">
<table cellSpacing="0" cellPadding="0" width="100%" runat="server" id="Table1">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" align="left" colspan="2" width="319">&nbsp;Edit 
			Customer<asp:Label CssClass="ContentTableHeader" Runat="server" ID="lblCustomerHeader"></asp:Label></td>
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
		<TD width="169"><asp:textbox MaxLength="100" id="txtCAFirstName" runat="server"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;Last Name:&nbsp;</TD>
		<TD width="169"><asp:textbox MaxLength="100" id="txtCALastName" runat="server"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
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
		<TD width="169"><asp:textbox MaxLength="255" id="txtCAEMail" runat="server"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" height="10" width="100%">&nbsp;</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">
			&nbsp;Password:&nbsp;</TD>
		<TD width="169"><asp:textbox MaxLength="255" id="txtCAPassword" runat="server"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">
			&nbsp;Confirm Password:&nbsp;</TD>
		<TD width="169"><asp:textbox MaxLength="255" id="txtCAConfirmPassword" runat="server"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" height="10" width="100%">&nbsp;</TD>
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
		<TD width="169"><asp:checkbox id="chkSubscribe" runat="server"></asp:checkbox></TD>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" height="10" colspan="3"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<tr id="Row1">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;Price Group:&nbsp;</TD>
		<td width="169">
			<cc1:selectvalcontrol id="txtCustomerGroups" runat="server" DisplaySelect="CustomerGroups" ReadFromDB="True">
				<asp:ListItem Value="CustomerGroups">[CustomerGroups]</asp:ListItem>
				<asp:ListItem Value="CustomerGroups">[CustomerGroups]</asp:ListItem>
			</cc1:selectvalcontrol></td>
		<TD class="Content" height="10" width="100%"></TD>
		<TD class="ContentTableHeader" colspan="2" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR id="Row2">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" height="10" colspan="3"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
	</TR>
	<TR>
		<TD class="Content" height="10" colspan="3" width="321"></TD>
	</TR>
	<TR>
		<TD align="right" colspan="9">
			<asp:LinkButton ID="cmdSave" Runat="server" OnClick="cmdSave_Click">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</TD>
	</TR>
</table>

<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AddEditAdmin.ascx.vb" Inherits="StoreFront.StoreFront.AddEditAdmin" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="contenttableheader" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
		<td class="contenttableheader" width="100%" colSpan="2">&nbsp;<asp:label id="lblTitle" Runat="server"></asp:label></td>
		<td class="contenttableheader" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
		<td class="content" width="100%" colSpan="2" height="5"><IMG src="../images/clear.gif"></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
		<td class="content" width="20%">&nbsp;User Name:&nbsp;&nbsp;</td>
		<td class="content" width="80%"><asp:textbox id="txtUsername" Runat="server" MaxLength="100"></asp:textbox>&nbsp;<FONT color="#ff0000">*</FONT></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
		<td class="content" width="100%" colSpan="2" height="5"><IMG src="../images/clear.gif"></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
		<td class="content">&nbsp;Password:&nbsp;&nbsp;</td>
		<td class="content">
			<P class="content"><asp:textbox id="txtPassword" Runat="server" TextMode="password" MaxLength="10"></asp:textbox>&nbsp;<FONT color="#ff0000">*</FONT>&nbsp;Max 
				10 Characters</P>
		</td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
		<td class="content" width="100%" colSpan="2" height="5"><IMG src="../images/clear.gif"></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
		<td class="content">&nbsp;First Name:&nbsp;&nbsp;</td>
		<td class="content"><asp:textbox id="txtFName" Runat="server"></asp:textbox>&nbsp;<FONT color="#ff0000">*</FONT></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
		<td class="content" width="100%" colSpan="2" height="5"><IMG src="../images/clear.gif"></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
		<td class="content">&nbsp;Last Name:&nbsp;&nbsp;</td>
		<td class="content"><asp:textbox id="txtLName" Runat="server"></asp:textbox>&nbsp;<FONT color="#ff0000">*</FONT></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
		<td class="content" width="100%" colSpan="2" height="5"><IMG src="../images/clear.gif"></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
		<td class="content">&nbsp;Role:&nbsp;&nbsp;</td>
		<td class="content"><asp:dropdownlist id="ddlRoles" Runat="server" Width="160px"></asp:dropdownlist></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1">
		</td>
	</tr>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
		<td class="content" width="100%" colSpan="2" height="5"><IMG src="../images/clear.gif"></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
	</tr>
	<TR>
		<TD class="contenttable" width="1"></TD>
		<TD class="content">&nbsp;Locked:</TD>
		<TD class="content">
			<asp:CheckBox id="chkLocked" runat="server"></asp:CheckBox></TD>
		<TD class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></TD>
	</TR>
	<tr>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
		<td class="content" width="100%" colSpan="2" height="5"><IMG src="../images/clear.gif"></td>
		<td class="contenttable" width="1"><IMG src="../images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="contenttable" width="100%" colSpan="4" height="1"><IMG src="../images/clear.gif"></td>
	</tr>
</table>
<p align="right"><asp:linkbutton id="cmdCancel" Runat="server">
		<asp:image id="Img2" Runat="server" ImageUrl="../images/cancel.jpg"></asp:image>
	</asp:linkbutton>&nbsp;&nbsp;
	<asp:linkbutton id="cmdSave" Runat="server">
		<asp:image id="img1" Runat="server" ImageUrl="../images/save.jpg"></asp:image>
	</asp:linkbutton><BR>
	<INPUT id="hdnTitle" type="hidden" name="hdnTitle" runat="server"> <INPUT id="hdnAdminId" type="hidden" value="0" name="hdnAdminId" runat="server"></p>

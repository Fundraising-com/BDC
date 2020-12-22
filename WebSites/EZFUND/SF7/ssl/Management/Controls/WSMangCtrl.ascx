<%@ Control Language="vb" AutoEventWireup="false" Codebehind="WSMangCtrl.ascx.vb" Inherits="StoreFront.StoreFront.WSMangCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server" ID="Table1">
	<TBODY>
		<TR>
			<TD class="ErrorMessages" noWrap align="left" colSpan="4">
				<asp:Label CssClass="ErrorMessages" ID="lblMessage" Runat="server"></asp:Label>
			</TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="2">&nbsp;&nbsp;Outbound 
				Web Services Credentials&nbsp;
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
			<td class="content" align="right" nowrap colspan="1">User Name:</td>
			<td class="content" align="left" colspan="1">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox id="txtUserName" runat="server" MaxLength="50"></asp:TextBox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" nowrap colspan="1">Password:</td>
			<td class="content" align="left" colspan="1">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox TextMode="Password" id="txtPassword" runat="server" MaxLength="50"></asp:TextBox></td>
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
		<TR>
			<td class="content" align="right" width="75%" colSpan="4">
				<asp:LinkButton ID="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY></TABLE>

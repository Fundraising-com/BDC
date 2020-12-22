<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AddEditcustompages.ascx.vb" Inherits="StoreFront.StoreFront.Addcustompages" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="cc1" Namespace="Karamasoft.WebControls.UltimateEditor" Assembly="UltimateEditor" %>
<%@ Register TagPrefix="uc1" TagName="UploadControl" Src="UploadControl.ascx" %>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" align="left" colSpan="2">&nbsp;Custom Page</td>
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
		<TD class="Content" noWrap align="right" width="99">Page Name:&nbsp;</TD>
		<TD class="Content">
			<table cellSpacing="0" cellPadding="0">
				<tr>
					<td class="Content"><%=Me.VirtualDirectory%></td>
					<td class="Content"><asp:textbox id=txtName Text="<%# GetPageName(DataSource.Pagename) %>" runat="server" maxlength="100" Width="250"></asp:textbox></td>
					<td class="Content">.aspx</td>
				</tr>
			</table>
			<asp:textbox id="txthiddenname" Text="<%# DataSource.Pagename %>" runat="server" Visible="False" maxlength="100">
			</asp:textbox>
		</TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right" height="10" width="99">Page Title:&nbsp;</TD>
		<TD align="left"><asp:textbox id="txttitle" Text="<%# DataSource.PageTitle %>" Runat="server" MaxLength="100" Width="250"></asp:textbox>
			<asp:textbox id="txthiddentitle" Text="<%# DataSource.PageTitle %>" visible="False" Runat="server">
			</asp:textbox>
		</TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right" height="10" width="99" valign="top">Content:&nbsp;</TD>
		<TD align="left">
			<cc1:UltimateEditor id="UltimateEditor1" runat="server"></cc1:UltimateEditor></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD align="left" colspan="3"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<td class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
	</TR>
	<TR>
		<td class="Content" colSpan="5" height="10"><IMG height="1" src="images/clear.gif"></td>
	</TR>
	<TR>
		<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" height="10" width="99"></TD>
		<TD align="right" colspan="2">
			<asp:HyperLink ID="hlnkCancel" Runat="server" NavigateUrl="../CustomPages.aspx" ImageUrl="../images/cancel.jpg"></asp:HyperLink>
			<asp:LinkButton ID="lnkSave" Runat="server" OnClick="lnkSave_OnClick">
				<img src="images/save.jpg" border="0" />
			</asp:LinkButton>
		</TD>
		<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
</table>

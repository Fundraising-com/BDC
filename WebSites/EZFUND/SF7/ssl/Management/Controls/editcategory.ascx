<%@ Register TagPrefix="uc1" TagName="UploadControl" Src="UploadControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="editcategory.ascx.vb" Inherits="StoreFront.StoreFront.editcategory" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input id="txtIDHidden" type="hidden" runat="server">
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" align="left" colSpan="2">&nbsp;Edit Category<asp:label id="lblCustomerHeader" Runat="server" CssClass="ContentTableHeader"></asp:label></td>
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
		<TD class="Content" noWrap align="right" width="1%">&nbsp;Category Name:&nbsp;</TD>
		<TD><asp:textbox id="txtName" Width="310px" runat="server" MaxLength="50"></asp:textbox></TD>
		<TD class="Content" width="100%" align="left" height="10">&nbsp;&nbsp;
			<asp:CheckBox ID="chkFeatured" Runat="server" Text="Featured" TextAlign="Left"></asp:CheckBox></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right" width="1%">&nbsp;Category 
			Description:&nbsp;</TD>
		<TD><asp:textbox id="txtCatDesc" Width="310px" TextMode="MultiLine" runat="server" MaxLength="255"></asp:textbox></TD>
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
		<TD class="Content" noWrap align="right" width="1%">&nbsp;Category Image:&nbsp;</TD>
		<TD><uc1:UploadControl id="catEditUpload" runat="server"></uc1:UploadControl></TD>
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
		<td align="right" colSpan="3">
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

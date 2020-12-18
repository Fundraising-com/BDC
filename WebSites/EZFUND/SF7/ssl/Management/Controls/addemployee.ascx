<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="addemployee.ascx.vb" Inherits="StoreFront.StoreFront.addemployee" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%" style="MIN-WIDTH: 600px">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" align="left" colSpan="3">&nbsp;<asp:label id="lblEmployeeHeader" CssClass="ContentTableHeader" Runat="server">Add Customer Service Representative</asp:label></td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="24"></TD>
		<td class="Content" nowrap style="padding-right: 24px;"><asp:Label ID="lblOverrideCapabilities" Runat="server" CssClass="content"><b>Override Capabilities for Representative</b></asp:Label></td>		
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right" width="127">&nbsp;User Name:&nbsp;</TD>
		<TD><asp:textbox id="txtUserName" MaxLength="100" runat="server" TabIndex="1"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<td class="Content"><asp:CheckBox ID="chkOverridePricing" Runat="server" TabIndex="4" CssClass="content" Text="Pricing:"></asp:CheckBox></td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right" width="127">&nbsp;Password:&nbsp;</TD>
		<TD><asp:textbox id="txtPassword" MaxLength="100" runat="server" TabIndex="2" TextMode="Password"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<td class="Content"><asp:CheckBox ID="chkOverrideShippingCharges" Runat="server" TabIndex="5" CssClass="content" Text="Shiping Charges:"></asp:CheckBox></td>		
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1" height="24"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right" width="127" height="24">&nbsp;Confirm 
			Password:&nbsp;</TD>
		<TD height="24"><asp:textbox id="txtConfirmPass" MaxLength="100" runat="server" TabIndex="3" TextMode="Password"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
		<TD class="Content" width="100%" height="24"></TD>
		<td class="Content"><asp:CheckBox ID="chkOverrideTaxes" Runat="server" TabIndex="6" CssClass="content" Text="Taxes:"></asp:CheckBox></td>
		<TD class="ContentTableHeader" width="1" height="24"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="4" height="10">
			<P>&nbsp;</P>
		</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="4" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
	</TR>
	<TR>
		<TD class="Content" colSpan="3" height="10"></TD>
	</TR>
	<TR>
		<TD align="right" colSpan="9"><asp:linkbutton id="cmdAdd" Runat="server">
				<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:linkbutton></TD>
	</TR>
</table>
<P>
	<asp:textbox id="Textbox_hide" MaxLength="100" runat="server" Visible="False"></asp:textbox></P>
<P>
	<asp:textbox id="Textbox_hide1" MaxLength="100" runat="server" Visible="False"></asp:textbox></P>

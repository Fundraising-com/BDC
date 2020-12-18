<%@ Control Language="vb" AutoEventWireup="false" Codebehind="editvendorcontrol.ascx.vb" Inherits="StoreFront.StoreFront.editvendorcontrol" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<input type="hidden" id="txtAddressIDHidden" runat="server">
<table cellSpacing="0" cellPadding="0" width="100%" runat="server">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td colspan="4">
			<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></P>
			<P id="MessageAlignment" runat="server"><asp:label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:label></P>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" align="left" colSpan="2">&nbsp;Edit Vendor<asp:label id="lblCustomerHeader" Runat="server" CssClass="ContentTableHeader"></asp:label></td>
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
		<TD class="Content" noWrap align="right" width="1%">&nbsp;Vendor Name:&nbsp;</TD>
		<TD><asp:textbox id="txtVendorName" runat="server" Width="202px" MaxLength="75"></asp:textbox></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR id="Row1">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" align="left" colSpan="2" width="449">&nbsp;Ship From 
			Vendor Information</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR id="Row2">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;City:&nbsp;</TD>
		<TD><asp:textbox id="txtCity" runat="server" TextMode="SingleLine" MaxLength="50"></asp:textbox></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR id="Row3">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;State/Province:&nbsp;</TD>
		<TD><cc1:selectvalcontrol id="selState" runat="server" Width="206px" DisplayValue="State" ReadFromDB="True">
				<asp:ListItem Value="States">[States]</asp:ListItem>
			</cc1:selectvalcontrol></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<tr id="Row4">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;Postal Code:&nbsp;</TD>
		<td><asp:TextBox ID="txtZip" Runat="server" MaxLength="50"></asp:TextBox></td>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR id="Row5">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;Country:&nbsp;</TD>
		<TD><cc1:selectvalcontrol id="selCountry" runat="server" Width="203px" DisplaySelect="Country" ReadFromDB="True">
				<asp:ListItem Value="Country">[Countries]</asp:ListItem>
				<asp:ListItem Value="Country">[Countries]</asp:ListItem>
			</cc1:selectvalcontrol></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR id="Row6">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;E-Mail Address:&nbsp;</TD>
		<TD><asp:textbox id="txtEMail" runat="server" MaxLength="255"></asp:textbox></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR id="Row7">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" colSpan="3" height="10"></TD>
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
		<TD align="right" colSpan="9">
			<asp:LinkButton ID="cmdSave" Runat="server">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</TD>
	</TR>
</table>

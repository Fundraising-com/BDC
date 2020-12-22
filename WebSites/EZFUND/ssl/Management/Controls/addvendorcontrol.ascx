<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="addvendorcontrol.ascx.vb" Inherits="StoreFront.StoreFront.addvendorcontrol" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%" runat="server" id="Tablevender1">
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
		<td class="ContentTableHeader" align="left" colSpan="2" width="449">&nbsp;Add 
			Vendor<asp:label id="lblCustomerHeader" Runat="server" CssClass="ContentTableHeader"></asp:label></td>
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
		<TD width="36%"><asp:textbox id="txtVendorName" runat="server" MaxLength="75"></asp:textbox><FONT color="#ff0000">*</FONT></TD>
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
		<TD width="340"><asp:textbox id="txtCity" runat="server" TextMode="SingleLine" MaxLength="50"></asp:textbox><FONT color="#ff0000"></FONT></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR id="Row3">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;State/Province:&nbsp;</TD>
		<TD width="340"><cc1:selectvalcontrol id="selState" runat="server" Width="206px" DisplayValue="State" ReadFromDB="True"></cc1:selectvalcontrol><FONT color="#ff0000"></FONT></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR id="Row4">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;Country:&nbsp;</TD>
		<TD width="340"><cc1:selectvalcontrol id="selCountry" runat="server" Width="205px" DisplaySelect="Country" ReadFromDB="True" Item_Selected="0"></cc1:selectvalcontrol><FONT color="#ff0000"></FONT></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR id="Row5">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right">&nbsp;Postal Code:&nbsp;</TD>
		<TD width="340"><asp:TextBox ID="txtZip" Runat="server" Width="64px" MaxLength="50"></asp:TextBox><FONT color="#ff0000"></FONT></TD>
		<TD class="Content" width="100%" height="10"></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR id="Row6">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" noWrap align="right" valign="baseline">&nbsp;E-Mail 
			Address:&nbsp;</TD>
		<TD width="340"><asp:textbox id="txtEMail" runat="server" MaxLength="255"></asp:textbox><FONT color="#ff0000"></FONT></TD>
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
		<TD class="Content" colSpan="3" height="10" width="451"></TD>
	</TR>
	<TR>
		<TD align="right" colSpan="9">
			<asp:LinkButton ID="cmdAdd" Runat="server">
				<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</TD>
	</TR>
</table>

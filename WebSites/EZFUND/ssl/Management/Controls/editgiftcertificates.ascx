<%@ Control Language="vb" AutoEventWireup="false" Codebehind="editgiftcertificates.ascx.vb" Inherits="StoreFront.StoreFront.editgiftcertificates" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:Label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:Label>
<br>
<br>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader">&nbsp;</td>
		<td class="ContentTableHeader" align="left">Edit Gift Certificates</td>
		<td class="ContentTableHeader">&nbsp;</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="Content" colspan="3">&nbsp;</td>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="Content">&nbsp;</td>
		<td class="Content">
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td nowrap align="right" class="Content">Gift Certificate Code:&nbsp;</td>
					<td nowrap align="left" class="Content"><asp:TextBox Runat="server" ID="txtGiftCode" Columns="20" MaxLength="100"></asp:TextBox></td>
					<td nowrap class="Content" width="100%">&nbsp;</td>
				</tr>
				<tr>
					<td nowrap align="right" class="Content">Amount:&nbsp;</td>
					<td nowrap align="left" class="Content"><asp:TextBox Runat="server" ID="txtAmount" Columns="10"></asp:TextBox></td>
					<td nowrap class="Content" width="100%">&nbsp;</td>
				</tr>
				<TR>
					<TD class="Content" noWrap align="right">Gift Certificate Expires:&nbsp;</TD>
					<TD class="Content" noWrap align="left">
						<asp:DropDownList id="DropDownList1" runat="server">
							<asp:ListItem Value="Never">Never</asp:ListItem>
							<asp:ListItem Value="On Specified Date">On Specified Date</asp:ListItem>
						</asp:DropDownList></TD>
					<TD class="Content" noWrap width="100%"></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right"></TD>
					<TD class="Content" noWrap align="left">
						Expiration Date:&nbsp;<asp:TextBox id="txtDate" Columns="10" runat="server"></asp:TextBox></TD>
					<TD class="Content" noWrap width="100%"></TD>
				</TR>
			</table>
		</td>
		<td class="Content">&nbsp;</td>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="Content" colspan="3">&nbsp;</td>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<TD class="ContentTable" HEIGHT="1" colspan="5"><IMG src="images/clear.gif" HEIGHT="1"></TD>
	</tr>
	<tr>
		<TD class="Content" HEIGHT="1" colspan="5">&nbsp;</TD>
	</tr>
	<tr>
		<TD class="Content" HEIGHT="1" colspan="5" align="right">
			<asp:LinkButton ID="btnSave" Runat="server" OnClick="SaveClick">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</TD>
	</tr>
</table>

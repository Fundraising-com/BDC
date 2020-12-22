<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AffiliateAddressCtrl.ascx.vb" Inherits="StoreFront.StoreFront.AffiliateAddressCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></P>
<P id="MessageAlignment" runat="server"><asp:label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:label></P>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader">&nbsp;General Information
			<table id="tblGeneral" cellSpacing="0" cellPadding="0" align="center" runat="server">
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">First Name:&nbsp;</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox maxlength="24" id="FirstName" runat="server" CssClass="content"></asp:textbox><FONT color="#ff0000"><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Middle Initial:&nbsp;
					</TD>
					<TD class="Content" width="100%"><asp:textbox maxlength="2" id="MI" runat="server" CssClass="content" Columns="3"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Last Name:&nbsp;
					</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox maxlength="25" id="LastName" runat="server" CssClass="content"></asp:textbox><FONT color="#ff0000"><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Company:&nbsp;</TD>
					<TD class="Content" width="100%"><asp:textbox id="Company" maxlength="75" runat="server" CssClass="content"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Address:&nbsp;</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox id="Address1" maxlength="255" runat="server" CssClass="content"></asp:textbox><FONT color="#ff0000"><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Address2:&nbsp;</TD>
					<TD class="Content" width="100%"><asp:textbox id="Address2" runat="server" maxlength="255" CssClass="content"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">City:&nbsp;</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox id="City" runat="server" maxlength="50" CssClass="content"></asp:textbox><FONT color="#ff0000"><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">State/Province:&nbsp;</TD>
					<TD class="Content" noWrap width="100%"><cc1:selectvalcontrol id="State" runat="server" CssClass="content" Width="206px" DisplaySelect="States" ReadFromDB="True">
							<asp:ListItem Value="States">[States]</asp:ListItem>
							<asp:ListItem Value="States">[States]</asp:ListItem>
							<asp:ListItem Value="States">[States]</asp:ListItem>
						</cc1:selectvalcontrol><FONT color="#ff0000"><asp:label id="Label6" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Postal Code:&nbsp;</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox id="Zip" runat="server" maxlength="50" CssClass="content"></asp:textbox><asp:label id="Label7" CssClass="Content" Runat="server"><FONT color="#ff0000">*</FONT>&nbsp;United States and Canada</asp:label></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Country:&nbsp;</TD>
					<TD class="Content" noWrap width="100%"><cc1:selectvalcontrol id="Country" runat="server" CssClass="content" Width="206px" DisplaySelect="Country" ReadFromDB="True">
							<asp:ListItem Value="Country">[Countries]</asp:ListItem>
							<asp:ListItem Value="Country">[Countries]</asp:ListItem>
							<asp:ListItem Value="States">[States]</asp:ListItem>
						</cc1:selectvalcontrol><FONT color="#ff0000"><asp:label id="Label8" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Phone:&nbsp;</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox id="Phone" runat="server" CssClass="content"></asp:textbox><FONT color="#ff0000"><asp:label id="Label9" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Fax:&nbsp;</TD>
					<TD class="Content" width="100%"><asp:textbox id="Fax" runat="server" CssClass="content"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Web Site Address:&nbsp;
					</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox id="WebSite" runat="server" maxlength="50" CssClass="content" Width="303px"></asp:textbox><FONT color="#ff0000"><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
			</table>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
	</TR>
</table>
<br>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader">&nbsp;Login
			<table id="tblLogin" cellSpacing="0" cellPadding="0" align="center" runat="server">
				<TR>
					<TD class="Content" noWrap align="right" colSpan="2">&nbsp;
					</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">E-mail Address:&nbsp;
					</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox id="EMail" maxlength="50" runat="server" CssClass="content"></asp:textbox><FONT color="#ff0000"><asp:label id="Label10" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Password:&nbsp;
					</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox id="password" maxlength="50" runat="server" CssClass="content" TextMode="Password"></asp:textbox><FONT color="#ff0000"><asp:label id="Label11" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Confirm Password:&nbsp;
					</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox id="Confirmpassword" maxlength="50" runat="server" CssClass="content" TextMode="Password"></asp:textbox><FONT color="#ff0000"><asp:label id="Label12" CssClass="ErrorMessages" Runat="server">*</asp:label></FONT></TD>
				</TR>
			</table>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
	</TR>
</table>
<br>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader">&nbsp;Commissions
			<table id="tblCommissions" cellSpacing="0" cellPadding="0" align="center" runat="server">
				<TR>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right"></TD>
					<TD class="Content" width="100%">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Commission Percent:&nbsp;
					</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox id="txtPercent" runat="server" CssClass="content" Width="94px"></asp:textbox>(ex: 
						.05 = 5%)<b></b></TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">Commission&nbsp; Flat Fee:&nbsp;
					</TD>
					<TD class="Content" noWrap width="100%"><asp:textbox id="txtFlatFee" runat="server" CssClass="content" Width="94px"></asp:textbox><FONT color="#ff0000"></FONT></TD>
				</TR>
			</table>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTable" colSpan="2" height="1"><IMG height="1" src="images/clear.gif"></TD>
	</TR>
	<tr>
		<td><IMG src="images/clear.gif" width="1" height="15"></td>
	</tr>
	<TR>
		<TD class="Content" colSpan="3" align="right">
			<asp:LinkButton ID="cmdSave" Runat="server">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</TD>
	</TR>
</table>

<%@ Control Language="vb" AutoEventWireup="false" Codebehind="editpricegroup.ascx.vb" Inherits="StoreFront.StoreFront.editpricegroup" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<td>&nbsp;</td>
		<td>
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="ContentTableHeader" align="left" colSpan="2">&nbsp;General</td>
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
					<TD class="Content" noWrap align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Group 
						Name:&nbsp;</TD>
					<TD><asp:textbox id="txtGroupName" MaxLength=50 runat="server"></asp:textbox></TD>
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
					<TD class="Content" height="10"></TD>
					<TD align="left">
						<asp:LinkButton ID="btnSelect" Runat="server" onclick="btnSelect_Click">
							<asp:Image BorderWidth="0" ID="imgSelect" runat="server" ImageUrl="../images/select.jpg" AlternateText="Select Members"></asp:Image>
						</asp:LinkButton>
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
					<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
				</TR>
			</table>
		</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td height="10"></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td>
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="ContentTableHeader" align="left" colSpan="3">&nbsp;Pricing</td>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" colSpan="4" height="10"></TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<tr>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" noWrap align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Price 
						Options:&nbsp;</TD>
					<td><asp:dropdownlist id="cboPriceOptions" Runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<TD class="Content" width="100%" colSpan="2" height="10"></TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" colSpan="4" height="10"></TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR id="trPercent" runat="server">
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" noWrap align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Percent 
						Change:&nbsp;</TD>
					<TD><asp:textbox id="txtPercentChange" runat="server" Wrap="false" Width="26px"></asp:textbox>&nbsp;%</TD>
					<TD class="Content" width="100%" colSpan="2" height="10"></TD>
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
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" colSpan="4" height="10"></TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<tr>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" noWrap align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Apply 
						Pricing To:&nbsp;</TD>
					<td><asp:dropdownlist id="cboPriceChoices" Runat="server" AutoPostBack="true"></asp:dropdownlist></td>
					<TD class="Content" width="100%" colSpan="2" height="10"></TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" colSpan="4" height="10"></TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" height="10"></TD>
					<TD align="left" colSpan="2">
						<asp:LinkButton ID="btnPriceChoice" Runat="server" onclick="PriceChoice_Click">
							<asp:Image BorderWidth="0" ID="imgPriceChoice" runat="server" ImageUrl="../images/select.jpg" AlternateText="Select"></asp:Image>
						</asp:LinkButton>
					</TD>
					<TD class="Content" width="100%" height="10"></TD>
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
					<TD class="Content" colSpan="6" height="10"></TD>
				</TR>
				<tr>
					<td align="right" colSpan="6">
						<asp:LinkButton ID="btnSave" Runat="server" OnClick="btnSave_Click">
							<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
						</asp:LinkButton>
					</td>
				</tr>
			</table>
		</td>
		<td>&nbsp;</td>
	</tr>
</table>

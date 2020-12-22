<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AffliateSettingCtrl.ascx.vb" Inherits="StoreFront.StoreFront.AffliateSettingCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></P>
<P id="MessageAlignment" runat="server"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="ContentTableHeader">&nbsp;Commissions
			<table id="tblLogin" cellSpacing="0" cellPadding="0" align="center" runat="server">
				<tr>
					<td class="content" colSpan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="Content" noWrap align="right">Default Percent:&nbsp;
					</td>
					<td class="Content" noWrap width="100%"><asp:textbox id="txtPercent" runat="server" CssClass="content" Width="94px"></asp:textbox><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label>&nbsp;(ex: 
						.05 = 5%)</td>
				</tr>
				<tr>
					<td class="Content" noWrap align="right">Default Flat Fee:&nbsp;
					</td>
					<td class="Content" noWrap width="100%"><asp:textbox id="txtFlatFee" runat="server" CssClass="content" Width="94px"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></td>
				</tr>
				<tr>
					<td class="content" colSpan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="Content" noWrap align="right">Pay Commission
					</td>
					<td class="content">&nbsp;</td>
				</tr>
				<tr>
					<td class="Content" noWrap align="right">On Sales:&nbsp;
					</td>
					<td class="Content" noWrap width="100%"><asp:dropdownlist id="ddPayOutType" Runat="server">
							<asp:ListItem Value="0">Originating From A Direct Link</asp:ListItem>
							<asp:ListItem Value="1">For 30 Days After Direct Link</asp:ListItem>
							<asp:ListItem Value="2">For 60 Days After Direct Link</asp:ListItem>
							<asp:ListItem Value="3">For 90 Days After Direct Link</asp:ListItem>
							<asp:ListItem Value="4">For 1 Year After Direct Link</asp:ListItem>
							<asp:ListItem Value="5">Indefinitely</asp:ListItem>
						</asp:dropdownlist><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></td>
				</tr>
				<tr>
					<td class="content" colSpan="2">&nbsp;</td>
				</tr>
			</table>
		</td>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
	</tr>
</table>
<p><br>
</p>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="ContentTableHeader">&nbsp;Payments
			<table id="tblCommissions" cellSpacing="0" cellPadding="0" align="center" runat="server">
				<tr>
					<td class="content" colSpan="3">&nbsp;</td>
				</tr>
				<tr>
					<td class="Content" noWrap align="right">Minimum Payment:&nbsp;
					</td>
					<td class="Content" noWrap width="100%"><asp:textbox id="txtMinimum" runat="server" CssClass="content" Width="94px"></asp:textbox><asp:label id="Label14" CssClass="ErrorMessages" Runat="server">*</asp:label></td>
					<td class="content">&nbsp;</td>
				</tr>
				<tr>
					<td class="Content" vAlign="top" noWrap align="right">Payment Terms:&nbsp;
					</td>
					<td class="Content" noWrap width="100%" colSpan="2" height="100%"><asp:textbox id="txtTerms" runat="server" CssClass="content" Width="390px" Height="149px" TextMode="MultiLine" MaxLength="255"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label>&nbsp;</td>
				</tr>
				<tr>
					<td class="content" colSpan="3">&nbsp;</td>
				</tr>
			</table>
		</td>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" colSpan="2" height="1"><IMG height="1" src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Content" colSpan="3">&nbsp;
		</td>
	</tr>
	<tr>
		<td class="Content" align="right" colSpan="3"><asp:linkbutton id="cmdSave" Runat="server">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:linkbutton></td>
	</tr>
</table>

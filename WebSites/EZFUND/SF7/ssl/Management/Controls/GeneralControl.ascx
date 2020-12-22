<%@ Control Language="vb" AutoEventWireup="false" Codebehind="GeneralControl.ascx.vb" Inherits="StoreFront.StoreFront.GeneralControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%-- begin: GJV - 7.0.2 issue 915 - validate site and ssl path url's --%>
<center>
	<asp:Label id="lblErrorMessage" Runat="server" CssClass="ErrorMessages" Visible="False" EnableViewState="False"></asp:Label>
</center>
<%-- end: GJV - 7.0.2 issue 915 - validate site and ssl path url's --%>
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server" ID="Table1">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="5">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;&nbsp;General&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" colSpan="1">Store Name:</td>
			<td class="content" align="left" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="StoreName" runat="server" MaxLength="50"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" colSpan="1">&nbsp;</td>
			<td class="content" align="left" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;<a href="http://support.storefront.net/licensing/Login.aspx" target="_blank">Store Activation</a></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<%--
		<tr>
			<TD class="Content" width="1" colSpan="5">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="2">&nbsp;&nbsp;StoreFront 
				Affiliate Program&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="left" colSpan="3">&nbsp;Earn commission on StoreFront sales 
				referred by your site. <a href="" target="_blank">Sign Up!</a></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colSpan="1">StoreFront Affiliate ID:</td>
			<td class="content" align="left" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="AffiliateID" MaxLength="255" Runat="server"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
--%>
		<TR>
			<TD class="Content" width="1" colSpan="5">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;&nbsp;Anonymous 
				Checkout Options
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" align="left">&nbsp;&nbsp;&nbsp;
				<asp:CheckBox id="chkAllowAnonymous" runat="server" Text="Allow Anonymous Checkout"></asp:CheckBox></TD>
			<td class="content" align="left" colSpan="2">
				<asp:CheckBox id="chkCreateAccount" runat="server" Text="Allow Account Creation on Order Confirmation Page"></asp:CheckBox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<%-- BEGIN: GJV - 9/7/2007 - Component License --%>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;&nbsp;Component 
				License</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD noWrap colSpan="3" align="center">&nbsp;<asp:textbox id="txtComponentLicense" Runat="server" Height="100px" Width="400" Wrap="true" TextMode="MultiLine"></asp:textbox>&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<%-- END: GJV - 9/7/2007 - Component License --%>
		<%-- begin: JDB - Google Analytics --%>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;&nbsp;Google 
				Analytics</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="left" colSpan="3">
				&nbsp;Google Analytics gives you the information you need to improve your 
				online results.&nbsp; It’s free, easy to use, scalable and secure. <a href="http://www.google.com/analytics/" target="_blank">
					Sign Up!</a></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colSpan="1">Google Analytics ID:</td>
			<td class="content" align="left" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtGoogleAnalyticsID" MaxLength="20" Runat="server"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<%-- end: JDB - Google Analytics --%>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<td class="content" align="right" width="75%" colSpan="5"><asp:linkbutton id="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:linkbutton></td>
		</TR>
	</TBODY></TABLE>

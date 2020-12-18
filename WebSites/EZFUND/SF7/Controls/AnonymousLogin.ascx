<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AnonymousLogin.ascx.vb" Inherits="StoreFront.StoreFront.AnonymousLogin" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<hr class="content">
<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<td class="content" colspan="4" height="1"><img src="../images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="content" width="1"></td>
		<TD class="CenterHeadings" noWrap colSpan="2" align="center">Check Out Without an 
			Account</TD>
		<TD height="10" class="content" width="1"></TD>
	</TR>
	<TR>
		<td class="content" width="1"></td>
		<TD height="10"></TD>
		<TD vAlign="top" height="10"></TD>
		<TD height="10" class="content" width="1"></TD>
	</TR>
	<TR>
		<td class="content" width="1"></td>
		<TD class="Content" align="center" colSpan="2">To check out without signing in or 
			creating an account, enter your e-mail address and click the Continue button 
			below.</TD>
		<TD class="content" width="1"></TD>
	</TR>
	<TR>
		<td class="content" width="1"></td>
		<TD height="10"></TD>
		<TD height="10"></TD>
		<TD class="content" width="1"></TD>
	</TR>
	<TR>
		<td class="content" width="1"></td>
		<TD class="Content" noWrap align="right">E-Mail Address:&nbsp;&nbsp; &nbsp;</TD>
		<TD valign="top"><asp:textbox id="txtAnonEMail" tabIndex="12" cssclass="Content" MaxLength="255" runat="server"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnContinue" tabIndex="13" Runat="server">
				<asp:Image runat="server" AlternateText="Continue" BorderWidth="0px" ID="imgContinue"></asp:Image>
			</asp:linkbutton></TD>
		<TD class="content" width="1"></TD>
	</TR>
	<TR>
		<td class="content" colspan="4" height="1"><img src="../images/clear.gif" width="1"></td>
	</TR>
</TABLE>
<script>

</script>

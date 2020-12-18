<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AnonymousLogin.ascx.vb" Inherits="StoreFront.StoreFront.AnonymousLogin" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<hr class="content">
<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="Content">
			<h2 class="subHeadings">Check Out Without an Account</h2>
			<p>To check out without signing in or creating an account, enter your e-mail address and click the Continue button below.</p>
			<table class="Content">
				<tr>
					<td>E-Mail Address:&nbsp;</td>
					<td><asp:textbox id="txtAnonEMail" tabIndex="12" cssclass="Content" MaxLength="255" runat="server"></asp:textbox>&nbsp;</td>
					<td>
<asp:linkbutton id="btnContinue" tabIndex="13" Runat="server">
				<asp:Image runat="server" AlternateText="Continue" BorderWidth="0px" ID="imgContinue"></asp:Image>
			</asp:linkbutton>					
					</td>
				</tr>
			</table>
		</TD>
	</TR>
</TABLE>
<script>

</script>

<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Affiliatesignin.ascx.vb" Inherits="StoreFront.StoreFront.Affiliatesignin" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label><asp:textbox id="ReturnPage" runat="server" Visible="False"></asp:textbox></P>
<P id="MessageAlignment" runat="server"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
<P>
	<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
		<TR>
			<TD vAlign="top">
				<TABLE id="table42" cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD class="Headings" colSpan="2" noWrap>&nbsp;Sign In To Your Affiliate Account</TD>
						<TD height="10">&nbsp;</TD>
					</TR>
					<TR>
						<TD height="10"></TD>
						<TD vAlign="top" height="10"></TD>
						<TD height="10">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="Content" colSpan="2">To access your affiliate account, enter your e-mail 
							address and password below.</TD>
						<TD height="10">&nbsp;</TD>
					</TR>
					<TR>
						<TD height="10"></TD>
						<TD height="10"></TD>
						<TD height="10">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="Content" align="right" noWrap>E-Mail Address:&nbsp;</TD>
						<td><asp:textbox id="txtSIEMail" tabIndex="1" runat="server" CssClass="Content" MaxLength=50></asp:textbox></td>
						<TD height="10">&nbsp;</TD>
					</TR>
					<tr>
						<td class="content" noWrap align="right">Password:&nbsp;</td>
						<td class="content"><asp:textbox id="txtSIPassword" tabIndex="2" runat="server" TextMode="Password" cssclass="Content" MaxLength=50></asp:textbox></td>
						<TD height="10">&nbsp;</TD>
					</tr>
					<tr>
						<td class="content" align="right" colSpan="2"><asp:hyperlink id="ForgotLink" tabIndex="3" runat="server" class="Content" NavigateUrl="../CustForgotPassword.aspx"><u>Forgot 
									Your Password?</u></asp:hyperlink></td>
						<TD height="10">&nbsp;</TD>
					</tr>
					<tr>
						<td class="content" colSpan="3">&nbsp;</td>
					</tr>
					<tr>
						<td class="content" align="right" colSpan="2">
							<asp:LinkButton ID="btnSignIn" Runat="server" tabIndex="4">
								<asp:Image BorderWidth="0" ID="imgSignIn" Runat="server" AlternateText="Sign In"></asp:Image>
							</asp:LinkButton>
						</td>
						<TD height="10">&nbsp;</TD>
					</tr>
				</TABLE>
			</TD>
			<TD width="1" bgColor="#000000"><img src="images/black.gif" width="1" height="1"></TD>
			<TD vAlign="top">
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD height="10">&nbsp;</TD>
						<TD class="Headings" noWrap colSpan="2">&nbsp;Create a New Affiliate Account</TD>
					</TR>
					<TR>
						<TD height="10">&nbsp;</TD>
						<TD height="10"></TD>
						<TD height="10"></TD>
					</TR>
					<TR>
						<TD height="10">&nbsp;</TD>
						<TD class="Content" colSpan="2">Sign up for an account today and start generating 
							revenue from sales referred from your site.</TD>
					</TR>
					<TR>
						<TD height="10">&nbsp;</TD>
						<TD height="10"></TD>
						<TD height="10"></TD>
					</TR>
					<TR>
						<TD height="10">&nbsp;</TD>
						<TD class="Content" noWrap align="right">E-Mail Address:&nbsp;</TD>
						<TD><asp:textbox id="txtCAEMail" tabIndex="7" runat="server" cssclass="Content" MaxLength=50></asp:textbox></TD>
					</TR>
					<tr>
						<td class="content">&nbsp;</td>
						<td class="Content" noWrap align="right">Password:&nbsp;</td>
						<td class="Content"><asp:textbox id="txtCAPassword" tabIndex="8" runat="server" TextMode="Password" cssclass="Content" MaxLength=50></asp:textbox></td>
					</tr>
					<tr>
						<td class="content">&nbsp;</td>
						<td class="Content" noWrap align="right">Confirm Password:&nbsp;</td>
						<td class="Content"><asp:textbox id="txtCAConfirmPassword" tabIndex="9" runat="server" TextMode="Password" cssclass="Content" MaxLength=50></asp:textbox></td>
					</tr>
					<tr>
						<td class="content" colSpan="3">&nbsp;</td>
					</tr>
					<tr>
						<td class="content">&nbsp;</td>
						<td class="Content" align="right" colSpan="2">
							<asp:LinkButton ID="btnCreate" Runat="server" TabIndex="11">
								<asp:Image BorderWidth="0" ID="imgCreate" Runat="server" AlternateText="Create Account"></asp:Image>
							</asp:LinkButton>
						</td>
					</tr>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
</P>

<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="false" Inherits="eSubsReportingCenter.Login" %>
<%@ Register TagPrefix="uc1" TagName="HeaderA" Src="HeaderTemplate/HeaderA.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Login</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Login" method="post" runat="server">
			<uc1:headera id="HeaderA1" runat="server"></uc1:headera><asp:label id="Label3" style="Z-INDEX: 101; LEFT: 26px; POSITION: absolute; TOP: 138px" runat="server" Font-Size="X-Large" Font-Names="Arial">Reporting Center</asp:label>
			<TABLE id="Table1" style="Z-INDEX: 102; LEFT: 60px; POSITION: absolute; TOP: 182px; HEIGHT: 85px" cellSpacing="0" cellPadding="0" width="500" border="0">
				<TR>
					<TD style="WIDTH: 112px" noWrap align="right"><asp:label id="Label1" runat="server" Font-Names="Arial">User Name:</asp:label>&nbsp;</TD>
					<TD style="WIDTH: 297px"><asp:textbox id="tbUserName" runat="server" Width="100%"></asp:textbox></TD>
					<TD><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="tbUserName" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 112px" noWrap align="right"><asp:label id="Label2" runat="server" Font-Names="Arial">Password:</asp:label>&nbsp;</TD>
					<TD style="WIDTH: 297px"><asp:textbox id="tbPassword" runat="server" Width="100%" TextMode="Password"></asp:textbox></TD>
					<TD><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="tbPassword" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 112px" noWrap></TD>
					<TD style="WIDTH: 297px" vAlign="center" align="right">
						<!--
					<asp:hyperlink id="HyperLink1" runat="server" Font-Size="Smaller" Font-Names="Arial" Font-Underline="True">I forgot my password</asp:hyperlink>
					-->
						&nbsp;
						<asp:button id="btnLogin" runat="server" Text="Login"></asp:button></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD noWrap style="WIDTH: 112px">&nbsp;</TD>
					<TD align="left" style="WIDTH: 297px" vAlign="center">&nbsp;</TD>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD noWrap style="WIDTH: 112px"></TD>
					<TD align="left" style="WIDTH: 297px" vAlign="center">
						<asp:Label id="lbErrorMessage" runat="server" Font-Names="Arial" ForeColor="Red" Visible="False">Your login is invalid. Try again or contact<br> the webmaster</asp:Label></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

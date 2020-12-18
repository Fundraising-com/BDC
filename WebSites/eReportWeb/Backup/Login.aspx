<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="True" Inherits="efundraising.eReportWeb.Login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Report Login</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name=vs_defaultClientScript content="JavaScript">
		<meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Resources/Css/style.css" type="text/css" rel="stylesheet">
		<script language=javascript>
		function SetSelected(tbox)
		{
			if (tbox != null)
				tbox.focus();
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="SetSelected(document.getElementById('TextBoxUsername'))">
		<form id="Form1" method="post" runat="server">
			<h1>eFundraising <% if(efundraising.eReportWeb.Components.Server.Config.IsExternal) { %> Partner<% } %> Reporting</h1>
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td vAlign=middle height=40 colspan="2" class="BigTextBold Active">Login</td>
				</tr>
				<tr>
					<td height="10"></td>
				</tr>
			</table>
			<asp:Panel ID="PanelNotAuthenticated" Runat="server">
				<table class=Login cellSpacing=0 cellPadding=0 border=0>
					<tr>
						<td>
							<table cellSpacing=0 cellPadding=0>
								<tr>
									<td class=NormalTextBold width=100>Username</td>
									<td><asp:TextBox id=TextBoxUsername Runat="server" Width="200"></asp:TextBox></td>
								</tr>
								<tr>
									<td class=NormalTextBold>Password</td>
									<td><asp:TextBox id=TextBoxPassword Runat="server" Width="200" TextMode="Password"></asp:TextBox></td>
								</tr>
								<tr>
									<td height=5></td>
								</tr>
								<tr>
									<td align=right><asp:CheckBox id=CheckBoxRememberMe Runat="server"></asp:CheckBox></td>
									<td class=NormalText>Remember me on this computer</td>
								</tr>
								<tr>
									<td height=10></td>
								</tr>
								<tr>
									<td></td>
									<td align=center><asp:Button id=ButtonLogin Runat="server" Width="80" Text="Login" CssClass="ButtonFlat NormalTextBold" onclick="ButtonLogin_Click"></asp:Button></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</asp:Panel>
			<asp:Panel ID="PanelAuthenticated" Runat="server" Visible="False">
				<table class=Login cellSpacing=0 cellPadding=0 border=0>
					<tr>
						<td>
							<table cellSpacing=0 cellPadding=0>
								<tr>
									<td class=BigTextBold colSpan=2>You are currently authenticated</td>
								</tr>
								<tr>
									<td height=10></td>
								</tr>
								<tr>
									<td class=NormalTextBold>Username</td>
									<td class=NormalText><asp:Label id=LabelUsername Runat="server"></asp:Label></td>
								</tr>
								<tr>
									<td class=NormalTextBold>Partner Name</td>
									<td class=NormalText><asp:Label id=LabelPartnerName Runat="server"></asp:Label></td>
								</tr>
								<tr>
									<td height=5></td>
								</tr>
								<tr>
									<td align=right width=100><asp:CheckBox id=CheckBoxCookieAuthenticated Runat="server" Checked="False"></asp:CheckBox></td>
									<td class=NormalText width=200>Remember me on this computer </td>
								</tr>
								<tr>
									<td height=20></td>
								</tr>
								<tr>
									<td align=center colSpan=2>
										<asp:Button id=ButtonUpdate Runat="server" Text="View Reports" CssClass="ButtonFlat NormalTextBold" onclick="ButtonUpdate_Click"></asp:Button>
										&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:Button id=ButtonLogOut Runat="server" Text="Log Out" CssClass="ButtonFlat NormalTextBold" onclick="ButtonLogOut_Click"></asp:Button>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</asp:Panel>
			<table width="360">
				<tr>
					<td align="center" height="100" valign="middle">
						<img src="Resources/Images/eFundraising.gif">
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Label ID="LabelLoginError" Runat="server" Visible="False"></asp:Label>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
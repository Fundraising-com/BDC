<%@ Page language="c#" Codebehind="ReportSelection.aspx.cs" AutoEventWireup="True" Inherits="efundraising.eReportWeb.ReportSelection" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Report Selection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Resources/Css/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<h1>eFundraising <% if(efundraising.eReportWeb.Components.Server.Config.IsExternal) { %> Partner<% } %> Reporting</h1>
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td vAlign="middle" colSpan="3" height="40" class="BigTextBold Active">
						<asp:Label ID="LabelLogin" Runat="server"></asp:Label>
						Reporting Home
					</td>
				</tr>
				<tr>
					<td><asp:listbox id="ReportsListBox" Width="400px" Height="200px" CssClass="reportSelection" Runat="server"></asp:listbox></td>
					<td width="20"></td>
					<td vAlign="top"><asp:button id="ViewReportButton" Text="View Report" Runat="server" onclick="ViewReportButton_Click"></asp:button></td>
				</tr>
			</table>
			<table width="360">
				<tr>
					<td align="center" height="100" valign="middle">
						<img align="center" src="Resources/Images/eFundraising.gif">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

<%@ Page language="c#" Codebehind="ReportSummary.aspx.cs" AutoEventWireup="True" Inherits="efundraising.eReportWeb.ReportSummary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Report Summary</title>
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
					<td vAlign="middle" height="40" class="BigTextBold Active">
						<asp:Label ID="LabelLogin" Runat="server"></asp:Label>
						<a href="ReportSelection.aspx">Reporting Home</a> <span class="Passive">&gt;&gt; </span>
						Summary
					</td>
				</tr>
				<tr>
					<td><asp:Label ID="LabelReport" CssClass="BigTextBold Normal" Runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td><asp:Label ID="LabelReportDescription" CssClass="NormalText Passive" Runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td height="30"></td>
				</tr>
				<tr>
					<td><asp:placeholder id="ParametersPlaceHolder" runat="server"></asp:placeholder></td>
				</tr>
				<tr>
					<td><asp:button id="GenerateButton" Width="607" Runat="server" Text="Generate Report" onclick="GenerateButton_Click"></asp:button></td>
				</tr>
			</table>
			<table width="600">
				<tr>
					<td align="center" height="100" valign="middle">
						<img align="center" src="Resources/Images/eFundraising.gif">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

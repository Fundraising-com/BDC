<%@ Page language="c#" Codebehind="ReportConfirmation.aspx.cs" AutoEventWireup="True" Inherits="efundraising.eReportWeb.ReportConfirmation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Report Confirmation</title>
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
						<a href="ReportSelection.aspx">Reporting Partner Home</a><span class="Passive"> &gt;&gt; </span>
						<asp:Literal ID="LiteralReportSummaryLink" Runat="server"></asp:Literal>
						<span class="Passive">&gt;&gt; </span>Results
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
					<td>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td><asp:Label Runat="server" CssClass="BigTextBold" ID="parTitleLabel">Parameter Description:</asp:Label></td>
							</tr>
							<tr>
								<td height="5"></td>
							</tr>
							<tr>
								<td><asp:PlaceHolder id="PlaceholderParameters" Runat="server"></asp:PlaceHolder></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="10"></td>
				</tr>
				<tr>
					<td><asp:PlaceHolder id="PlaceHolderResult" Runat="server"></asp:PlaceHolder></td>
				</tr>
				<tr>
					<td align="center" height="100" valign="middle">
						<img align="center" src="Resources/Images/eFundraising.gif">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="HeaderB" Src="HeaderTemplate/HeaderB.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderA" Src="HeaderTemplate/HeaderA.ascx" %>
<%@ Page language="c#" Codebehind="Logout.aspx.cs" AutoEventWireup="false" Inherits="eSubsReportingCenter.Logout" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Logout</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0">
		<form id="Logout" method="post" runat="server">
			<asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 189px; POSITION: absolute; TOP: 162px" runat="server" Width="377px" Font-Bold="True" Font-Names="Arial">Thank you for using the eSubs Reporting Center</asp:Label>
			<uc1:HeaderA id="HeaderA1" runat="server"></uc1:HeaderA>
			<br>
			<br>
			<br>
			<br>
			<br>
			<br>
			<br>
			&nbsp;<a href="ReportingCenter.aspx">Go to the Login Page</a>
		</form>
	</body>
</HTML>

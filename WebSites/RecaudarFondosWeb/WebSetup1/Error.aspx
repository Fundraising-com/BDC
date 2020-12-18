<%@ Register TagPrefix="uc1" TagName="HeaderB" Src="HeaderTemplate/HeaderB.ascx" %>
<%@ Page language="c#" Codebehind="Error.aspx.cs" AutoEventWireup="false" Inherits="eSubsReportingCenter.Error" %>
<%@ Register TagPrefix="uc1" TagName="HeaderA" Src="HeaderTemplate/HeaderA.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Error</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Error" method="post" runat="server">
			<asp:Label id="lbErrorMessage" style="Z-INDEX: 101; LEFT: 14px; POSITION: absolute; TOP: 123px" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="Maroon">An unexpected error has occured.<br>Try to log in again or try to come back later.</asp:Label>
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

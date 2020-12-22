<%@ Page language="c#" Codebehind="Result.aspx.cs" AutoEventWireup="false" Inherits="eSubsReportingCenter.Status" %>
<%@ Register TagPrefix="uc1" TagName="HeaderE" Src="HeaderTemplate/HeaderE.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Result</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Status" method="post" runat="server">
			<uc1:headere id="HeaderE1" runat="server"></uc1:headere><asp:label id="lbResultMsg" style="Z-INDEX: 102; LEFT: 166px; POSITION: absolute; TOP: 189px" runat="server">Report is processing. Please wait.</asp:label></form>
	</body>
</HTML>

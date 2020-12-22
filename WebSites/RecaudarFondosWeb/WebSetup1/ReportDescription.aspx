<%@ Register TagPrefix="uc1" TagName="HeaderC" Src="HeaderTemplate/HeaderC.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderE" Src="HeaderTemplate/HeaderE.ascx" %>
<%@ Page language="c#" Codebehind="ReportDescription.aspx.cs" AutoEventWireup="false" Inherits="eSubsReportingCenter.ReportDescription" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Report Description</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="ReportDescription" method="post" runat="server">
			<asp:Table id="ParamDesc" style="Z-INDEX: 103; LEFT: 18px; POSITION: absolute; TOP: 233px" runat="server" Width="600px" CellSpacing="0" CellPadding="0" BorderWidth="0px"></asp:Table>
			<asp:Label id="lbNatD" style="Z-INDEX: 104; LEFT: 19px; POSITION: absolute; TOP: 152px" runat="server">Report Name:</asp:Label>
			<asp:Label id="lbReportName" style="Z-INDEX: 105; LEFT: 121px; POSITION: absolute; TOP: 152px" runat="server">Label</asp:Label>
			<asp:Label id="lbReportDesc" style="Z-INDEX: 106; LEFT: 19px; POSITION: absolute; TOP: 185px" runat="server">Label</asp:Label>
			<uc1:HeaderE id="HeaderE1" runat="server"></uc1:HeaderE>
		</form>
	</body>
</HTML>

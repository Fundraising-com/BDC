<%@ Page language="c#" Codebehind="ReportMan.aspx.cs" AutoEventWireup="false" Inherits="eSubsReportingCenter.ReportMan" %>
<%@ Register TagPrefix="uc1" TagName="HeaderA" Src="HeaderTemplate/HeaderA.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReportMan</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="ReportMan" method="post" runat="server">
			<uc1:HeaderA id="HeaderA1" runat="server"></uc1:HeaderA>
			<asp:listbox id="lbReportList" style="Z-INDEX: 101; LEFT: 14px; POSITION: absolute; TOP: 124px" runat="server" Height="191px" Width="178px" AutoPostBack="True"></asp:listbox><asp:label id="Label1" style="Z-INDEX: 102; LEFT: 17px; POSITION: absolute; TOP: 99px" runat="server">Choose a report</asp:label><asp:table id="ReportTable" style="Z-INDEX: 103; LEFT: 209px; POSITION: absolute; TOP: 126px" runat="server"></asp:table><asp:button id="btnGetParam" style="Z-INDEX: 104; LEFT: 13px; POSITION: absolute; TOP: 312px" runat="server" Text="Load Report" Width="180px"></asp:button><asp:button id="btnGenerate" style="Z-INDEX: 105; LEFT: 209px; POSITION: absolute; TOP: 96px" runat="server" Text="Generate" Enabled="False"></asp:button>
		</form>
	</body>
</HTML>

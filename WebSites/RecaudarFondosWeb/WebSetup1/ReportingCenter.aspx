<%@ Page language="c#" Codebehind="ReportingCenter.aspx.cs" AutoEventWireup="false" Inherits="eSubsReportingCenter.ReportingCenter" %>
<%@ Register TagPrefix="uc1" TagName="HeaderD" Src="HeaderTemplate/HeaderD.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Reporting Center</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<script language="javascript">
		//<!--
	function ShowDescription()
	{
		reportChoice = document.ReportingCenter.lbReportList;
		index = reportChoice.selectedIndex;
		if (index == -1)
		{
			return false;
		}
		
		paramsArray = reportChoice.options[index].value.split(";");
		label = reportChoice.options[index].text;
		
		window.open("ReportDescription.aspx?REPORT_ID=" + paramsArray[0] + "&" +
			"REPORT_SP=" + paramsArray[1] + "&" + "REPORT_FILE_NAME=" + paramsArray[2] + "&" +
			"REPORT_LABEL=" + label, "", "toobars=no,scrollbars=yes,resizable=yes");
		//?REPORT_ID=" + params,"","toobars=no,scrollbars=yes,resizable=yes');");
		return false;		
	}
	// -->
		</script>
		<form id="ReportingCenter" method="post" runat="server">
			<uc1:headerd id="HeaderD1" runat="server"></uc1:headerd><asp:dropdownlist id="ddlReportFamily" style="Z-INDEX: 101; LEFT: 7px; POSITION: absolute; TOP: 155px" runat="server" AutoPostBack="True" Width="188px"></asp:dropdownlist><asp:listbox id="lbReportList" style="Z-INDEX: 102; LEFT: 5px; POSITION: absolute; TOP: 183px" runat="server" AutoPostBack="True" Width="187px" Height="264px"></asp:listbox><asp:button id="btnGetParam" style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 455px" runat="server" Width="169px" Text="Get Parameters"></asp:button><asp:button id="btnGetReportInfo" style="Z-INDEX: 104; LEFT: 16px; POSITION: absolute; TOP: 487px" runat="server" Width="169px" Text="Get Report Information"></asp:button><asp:table id="ReportTable" style="Z-INDEX: 105; LEFT: 209px; POSITION: absolute; TOP: 187px" runat="server"></asp:table><asp:button id="btnGenerate" style="Z-INDEX: 106; LEFT: 208px; POSITION: absolute; TOP: 153px" runat="server" Text="Generate Report" Enabled="False"></asp:button>
			<asp:Label id="lbMsg" style="Z-INDEX: 107; LEFT: 359px; POSITION: absolute; TOP: 158px" runat="server" Font-Names="Arial" ForeColor="Blue" Font-Size="X-Small"></asp:Label></form>
	</body>
</HTML>

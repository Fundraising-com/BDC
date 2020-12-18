<%@ Register TagPrefix="uc1" TagName="HeaderA" Src="HeaderTemplate/HeaderA.ascx" %>
<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="eSubsReportingCenter.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:HeaderA id="HeaderA1" runat="server"></uc1:HeaderA>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 101; LEFT: 234px; POSITION: absolute; TOP: 228px" runat="server"></asp:TextBox>
			<asp:RangeValidator id="RangeValidator1" style="Z-INDEX: 102; LEFT: 320px; POSITION: absolute; TOP: 342px" runat="server" ErrorMessage="RangeValidator" ControlToValidate="TextBox1"></asp:RangeValidator>
			<script language="javascript">
				window.open("Result.aspx","","toobars=no,scrollbars=yes,resizable=yes");
			</script>
		</form>
	</body>
</HTML>

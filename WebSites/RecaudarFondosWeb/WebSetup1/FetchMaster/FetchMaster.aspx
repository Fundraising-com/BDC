<%@ Page language="c#" Codebehind="FetchMaster.aspx.cs" AutoEventWireup="false" Inherits="eSubsReportingCenter.FetchMaster" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="WebForm1" method="post" runat="server">
			<asp:TextBox id="tbXSD" style="Z-INDEX: 101; LEFT: 159px; POSITION: absolute; TOP: 54px" runat="server" Width="191px"></asp:TextBox>
			<asp:Button id="btnGenerate" style="Z-INDEX: 102; LEFT: 19px; POSITION: absolute; TOP: 83px" runat="server" Text="Fetch Parameters"></asp:Button>
			<asp:Label id="Label1" style="Z-INDEX: 103; LEFT: 18px; POSITION: absolute; TOP: 56px" runat="server">Fichier xsd à générer :</asp:Label>
			<asp:TextBox id="Status" style="Z-INDEX: 104; LEFT: 17px; POSITION: absolute; TOP: 121px" runat="server" Width="335px" Height="130px" TextMode="MultiLine"></asp:TextBox>
			<asp:Label id="Label2" style="Z-INDEX: 105; LEFT: 17px; POSITION: absolute; TOP: 30px" runat="server">Nom de la SP :</asp:Label>
			<asp:TextBox id="tbSP" style="Z-INDEX: 106; LEFT: 160px; POSITION: absolute; TOP: 27px" runat="server" Width="191px"></asp:TextBox>
			<asp:Table id="ReportTable" style="Z-INDEX: 107; LEFT: 19px; POSITION: absolute; TOP: 269px" runat="server" Width="440px"></asp:Table>
			<asp:Button id="Button1" style="Z-INDEX: 108; LEFT: 294px; POSITION: absolute; TOP: 83px" runat="server" Text="Générer"></asp:Button>
			<asp:Button id="Button2" style="Z-INDEX: 109; LEFT: 492px; POSITION: absolute; TOP: 82px" runat="server" Text="Button"></asp:Button>
		</form>
	</body>
</HTML>

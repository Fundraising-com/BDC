<%@ Page language="c#" Codebehind="WebForm.aspx.cs" AutoEventWireup="True" Inherits="CRMWeb.WebForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:DataGrid id="DataGrid1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				Height="88px" Width="416px">
				<SelectedItemStyle BorderWidth="3px" BorderStyle="Double" BorderColor="Black" BackColor="SteelBlue"></SelectedItemStyle>
				<Columns>
					<asp:ButtonColumn Visible="False" Text="Select" CommandName="Select"></asp:ButtonColumn>
				</Columns>
			</asp:DataGrid>
		</form>
	</body>
</HTML>

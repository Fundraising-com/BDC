<%@ Page ValidateRequest="false"  language="c#" Codebehind="showerrorspage.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.showerrorspage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Error</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">

  </HEAD>
	<body topmargin=0 bottommargin=0 rightmargin=0 leftmargin=0 >
		<form id="Form1" method="post" runat="server">
			<table cellpadding=0 cellspacing=0 border=0  bgcolor="#F8F7F6"width="100%" height="100%"><tr><td align="center">
			<TABLE id="Table1"  cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD>
						<br>
						<asp:Label id="Label1" runat="server" ForeColor="Red" CssClass="CSPlainText">Label</asp:Label></TD>
				</TR>
				<TR>
					<TD align=center><br>
						<input type="button" value="Close" onclick="parent.document.getElementById('dwindow').style.display='none'">
						<asp:Button id="btnClose" runat="server" Text="Close" Visible="False"></asp:Button></TD>
				</TR>
			</TABLE>
			</td></tr></table>
		</form>
	</body>
</HTML>

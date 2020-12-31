<%@ Register TagPrefix="uc1" TagName="ControlerIncidentActionComments" Src="ControlerIncidentActionComments.ascx" %>
<%@ Page language="c#" Codebehind="Comments.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.Comments" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Comments</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
  </HEAD>
	<body onload="return window_onunload()" topmargin="0" bottommargin="0" rightmargin="0"
		leftmargin="0">
		<form id="Form1" method="post" runat="server">
		<!--#include file="fctjavascriptall.js"-->
			<img src="images/editcommentstitle.gif">
			<center>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" width="627">
				<tr>
					<td><br><br><br><asp:Label Runat=server ID="lblTitle" CssClass="CSPlainText" Font-Bold="True">Comments</asp:Label></td>
				</tr>
				<TR>
					<TD>
						
						<uc1:ControlerIncidentActionComments id="ctrlControlerIncidentActionComments" runat="server"></uc1:ControlerIncidentActionComments></TD>
				</TR>
			</TABLE>
			</center>
		</form>
		<!--#include file="errorwindow.js"-->
	</body>
</HTML>

<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Page language="c#" Codebehind="switchletter.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.CustomerService.switchletter" %>
<%@ Register TagPrefix="uc1" TagName="ControlerSwitchLetter" Src="ControlerSwitchLetter.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Reprint Switch Letter</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
	</HEAD>
	<BODY onload="return window_onunload()" topmargin="0" bottommargin="0" leftmargin="0"
		rightmargin="0" marginheight="0" marginwidth="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<center>
				<br>
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD><asp:Label CssClass="CSPageTitle" Runat="server" Text="Cancel Switch Letter Batch" id="Label1">Reprint Switch Letter Batch</asp:Label></TD>
					</TR>
					<tr>
						<td><br>
							<asp:Label Runat="server" ID="lblDirections" CssClass="CSDirections">Please click the reprint button at the right of the switch letter batch you want to reprint.</asp:Label></td>
					</tr>
					<TR>
						<TD align="center">
							<br>
							<uc1:ControlerSwitchLetter id="ctrlControlerSwitchLetter" runat="server"></uc1:ControlerSwitchLetter></TD>
					</TR>
				</TABLE>
				<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
			</center>
			<!--#include file="fctjavascriptall.js"-->
			<!--#include file="errorwindow.js"-->
		</form>
	</BODY>
</HTML>

<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="uc1" TagName="ControlerLeadInsert" Src="ControlerLeadInsert.ascx" %>
<%@ Page language="c#" Codebehind="LeadInsert.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.LeadInsert" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Lead Insert</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
  </HEAD>
	<BODY onload="return window_onunload()" bottommargin="0" topmargin="0" leftmargin="0"
		rightmargin="0">
		<!--#include file="fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<div align="center"><br>
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD>
							<asp:Label id="lblTitle" Runat="server">
								<h3><font face="Verdana" color="#2f4f88">Lead Entry</font></h3>
							</asp:Label></TD>
					</TR>
					<TR>
						<TD><br>
							<uc1:ControlerLeadInsert id="ControlerLeadInsert1" runat="server"></uc1:ControlerLeadInsert>
							<BR>
						</TD>
					</TR>
				</TABLE>
			</div>
			<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:ValidationSummary>
		</form> <!--#include file="errorwindow.js"-->
		
	</BODY>
</HTML>

<%@ Register TagPrefix="uc1" TagName="ControlerGenerateSwitchLetter" Src="ControlerGenerateSwitchLetter.ascx" %>
<%@ Page language="c#" Codebehind="GenerateSwitchLetter.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.CustomerService.GenerateSwitchLetter" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Generate Switch Letter</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
	</HEAD>
	<BODY onload="return window_onunload()" topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginheight="0" marginwidth="0">
		

		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<!--#include file="fctjavascriptall.js"-->
			<br>
			<uc1:ControlerGenerateSwitchLetter id="ctrlControlerGenerateSwitchLetter" runat="server"></uc1:ControlerGenerateSwitchLetter>
			<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
		</form>
		<P>&nbsp;</P>
		<P>
			</P>
			<!--#include file="errorwindow.js"-->
	</BODY>
</HTML>

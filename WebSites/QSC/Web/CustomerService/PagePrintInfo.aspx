<%@ Register TagPrefix="uc1" TagName="ControlerPrintInfo" Src="ControlerPrintInfo.ascx" %>
<%@ Page language="c#" Codebehind="PagePrintInfo.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.PagePrintInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Print Informations</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
	</HEAD>
	<body onload="window.print(); self.close();">
		<uc1:ControlerPrintInfo id="ctrlControlerPrintInfo" runat="server"></uc1:ControlerPrintInfo>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="ControlerIncidentHistory" Src="ControlerIncidentHistory.ascx" %>
<%@ Page language="c#" Codebehind="findincident.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.findincident" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>findincident</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
		
  </HEAD>
	<BODY onload="return window_onunload()" bottommargin="0" topmargin="0" leftmargin="0"
		rightmargin="0">
		<form id="Form1" method="post" runat="server">
		<!--#include file="fctjavascriptall.js"-->
		<asp:Image Runat="server" ID="imgTitle" ImageUrl="images/refertoincidenttitle.gif"></asp:Image>
		
			<table width="100%" cellpadding="5">
				<tr>
					<td>
						<asp:Label Runat="server" id="lblDirections" CssClass="CSDirections"><br>Please select one of the following incident:</asp:Label>
					</td>
				</tr>
				<TR>
					<TD align="center">
					
						<uc1:ControlerIncidentHistory id="ctrlControlerIncidentHistory" runat="server"></uc1:ControlerIncidentHistory></TD>
				</TR>
			</table>
			<!--#include file="errorwindow.js"-->
		</form>
	</BODY>
</HTML>

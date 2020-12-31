<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="uc1" TagName="ControlerProblemCode" Src="ControlerProblemCode.ascx" %>
<%@ Page language="c#" Codebehind="problemcode.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.problemcode" %>
<%@ Register TagPrefix="uc1" TagName="ControlerSearchProblemCode" Src="ControlerSearchProblemCode.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>
			<%= PageTitle %>
		</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	</head>
	<body onload="return window_onunload()" bottommargin="0" topmargin="0" leftmargin="0"
		rightmargin="0">
		<!--#include file="fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<asp:image runat="server" id="imgTitle" borderwidth="0" imageurl="images/findproblemcodetitle.gif"></asp:image>
			<table width="100%" cellpadding="5">
				<tr>
					<td><br>
						<asp:label runat="server" id="lblTitle" visible="False">
							<h3><font face="Verdana" color="#2f4f88">Problem Code Maintenance</font></h3>
						</asp:label>
						<table id="Table3" cellspacing="0" width="100%" cellpadding="0" bgcolor="#cecece" border="0">
							<tr>
								<td>
									<table id="Table4" height="100%" width="100%" cellspacing="1" cellpadding="2">
										<tr>
											<td valign="top" height="20">
												<font class="CSTitle">Search</font>
											</td>
										</tr>
										<tr bgcolor="#ffffff">
											<td valign="top">
												<uc1:controlersearchproblemcode id="ctrlControlerSearchProblemCode" runat="server"></uc1:controlersearchproblemcode>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<uc1:controlerproblemcode id="ctrlControlerProblemCode" runat="server"></uc1:controlerproblemcode>
					</td>
				</tr>
			</table>
			<!--#include file="errorwindow.js"-->
		</form>
	</body>
</html>

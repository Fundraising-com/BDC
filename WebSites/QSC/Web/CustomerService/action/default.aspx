<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.action._default" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService.action" Assembly="QSPFulfillment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>action</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="../../Includes/QSPFulfillment.css" type="text/css">
		<!--#include file="Action.js"-->
	</head>
	<body onload="return window_onunload()" bottommargin="0" topmargin="0" leftmargin="0"
		rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<!--#include file="../fctjavascriptall.js"-->
			<asp:image runat="server" id="imgTitle" imageurl="../images/actiontitle.gif"></asp:image>
			<table cellpadding="10" width="100%">
				<tr>
					<td>
						<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td colspan="2">
									<asp:label id="lblHeaderTitle" runat="server" cssclass="CSTitle">Action:&nbsp;</asp:label><asp:label id="lblHeader" runat="server" cssclass="CSTitle"></asp:label></td>
							</tr>
							<tr>
								<td colspan="2">
									<br>
									<asp:label id="lblMessage" runat="server" cssclass="CSDirections">This is not a valid action.</asp:label></td>
							</tr>
							<tr>
								<td colspan="2">
									<asp:label id="lblAction" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td colspan="2"><br>
									<asp:label runat="server" text="Comments" id="lblComments" cssclass="CSPlainText" font-bold="true">Comments</asp:label><br>
									<asp:textbox id="tbxComment" runat="server" textmode="MultiLine" width="627px" height="64px"
										style="FONT-SIZE:8pt; COLOR:#000000; FONT-FAMILY:verdana,arial" maxlength="500"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td align="center"><br>
									<br>
									<div id="divConfirm">
										<asp:button id="btnConfirm" runat="server" text="Confirm" onclick="btnConfirm_Click"></asp:button>
									</div>
								</td>
								<td align="center"><br>
									<br>
									<asp:button id="btnCancel" runat="server" text="Cancel" causesvalidation="False" onclick="btnCancel_Click"></asp:button></td>
							</tr>
						</table>
						<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
					</td>
				</tr>
			</table>
		</form>
		<!--#include file="../errorwindow.js"-->
	</body>
</html>

<%@ Page language="c#" Codebehind="GenerateCAStatements.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.CustomerService.GenerateCAStatements" %>
<%@ Register assembly="QSP.WebControl" namespace="QSP.WebControl" tagprefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GenerateCAStatements</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Button id="btnGenerate" style="Z-INDEX: 101; LEFT: 31px; POSITION: absolute; TOP: 57px"
				runat="server" Text="Generate"></asp:Button>
		    Statement Print Request Batch:
            <cc1:TextBox ID="StatementPrintRequestBatchIDTextBox" runat="server"></cc1:TextBox>
		</form>
	</body>
</HTML>

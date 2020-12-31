<%@ Page Language="C#" Codebehind="GLAccountDropDownList.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Finance.GLAccountDropDownList" %>
<HTML>
	<HEAD>
		<title>CA Fulfill System - List</title>
		<link REL="stylesheet" HREF="../Includes/MagSysStyle.css" TYPE="text/css">
	</HEAD>
	<body topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<center>
				<br>
				<asp:Label Runat="server" ID="lblGLAccount" CssClass="ClearTextBoxB" /><br>
				<asp:DropDownList AutoPostBack=True CssClass="boxlookW" runat="server" ID="ddl_GLAccount" 
				DataSource='<%# GetGLAccounts() %>' 
				DataTextField="GLAccountDescription"
				DataValueField="GLAccountID"
			    OnSelectedIndexChanged="SetGLAccount" 
			    />
				<asp:Literal id="Literal1" runat="server"></asp:Literal>
			</center>
		</form>
	</body>
</HTML>

<%@ Page Language="C#" Codebehind="GLNumberDropDownList.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Finance.GLNumberDropDownList" %>
<HTML>
	<HEAD>
		<title>CA Fulfill System - List</title>
		<link REL="stylesheet" HREF="../Includes/MagSysStyle.css" TYPE="text/css">
	</HEAD>
	<body topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<center>
				<br>
				<asp:Label Runat="server" ID="lblGLNumber" CssClass="ClearTextBoxB" /><br>
				<asp:DropDownList AutoPostBack=True CssClass="boxlookW" runat="server" ID="ddl_GLNumber" 
				DataSource='<%# GetGLNumber() %>' 
				DataTextField="GL_Account_Description"
				DataValueField="GL_Account_Number"
			    OnSelectedIndexChanged="SetGLNumber" 
			    />
				<asp:Literal id="Literal1" runat="server"></asp:Literal>
			</center>
		</form>
	</body>
</HTML>

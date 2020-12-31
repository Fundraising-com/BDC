<%@ Page Language="C#" Codebehind="AdjTypeDropDownList.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.AcctTypeDropDownList" %>
<HTML>
	<HEAD>
		<title>CA Fulfill System - List</title>
		<link REL="stylesheet" HREF="../Includes/MagSysStyle.css" TYPE="text/css">
	</HEAD>
	<body topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<center>
				<br>
				<asp:Label Runat="server" ID="lblAcctType" CssClass="ClearTextBoxB" /><br>
				<asp:DropDownList AutoPostBack=True CssClass="boxlookW" runat="server" ID="ddl_AccountType" 
				DataSource='<%# GetAccountType() %>' 
				DataTextField="Description"
				DataValueField="Instance"
			    OnSelectedIndexChanged="SetAccountType" 
			    />
				<asp:Literal id="Literal1" runat="server"></asp:Literal>
			</center>
		</form>
	</body>
</HTML>

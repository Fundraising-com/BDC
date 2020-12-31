<%@ Page Language="C#" Codebehind="AdjTypeDropDownList.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Finance.AdjTypeDropDownList" %>
<HTML>
	<HEAD>
		<title>CA Fulfill System - List</title>
		<link REL="stylesheet" HREF="../Includes/MagSysStyle.css" TYPE="text/css">
	</HEAD>
  <body topmargin="0" leftmargin="0">	
    <form id="Form1" method="post" runat="server">
    <center>
			<br><asp:Label Runat="server" ID="lblAdjType" CssClass="ClearTextBoxB" /><br>
			<asp:DropDownList AutoPostBack=True CssClass="boxlookW" runat="server" ID="ddl_AdjustmentType" 
				DataSource='<%# GetAdjustmentType() %>' 
				DataTextField="Description"
				DataValueField="Instance"
			    OnSelectedIndexChanged="SetAdjustmentType" 
			    />
			<asp:Literal id="Literal1" runat="server"></asp:Literal>
	</center>
	 </form>
  </body>
</html>

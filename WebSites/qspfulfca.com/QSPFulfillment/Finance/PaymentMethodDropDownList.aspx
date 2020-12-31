<%@ Page Language="C#" Codebehind="PaymentMethodDropDownList.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Finance.PaymentMethodDropDownList" %>
<HTML>
	<HEAD>
		<title>CA Fulfill System - List</title>
		<link REL="stylesheet" HREF="../Includes/MagSysStyle.css" TYPE="text/css">
	</HEAD>
  <body topmargin="0" leftmargin="0">	
    <form id="Form1" method="post" runat="server">
    <center>
			<br><asp:Label Runat="server" ID="lblPaymentMethod" CssClass="ClearTextBoxB" /><br>
			<asp:DropDownList AutoPostBack=True CssClass="boxlookW" runat="server" ID="ddl_PaymentMethod" 
				DataSource='<%# GetInvoicePaymentMethods() %>' 
				DataTextField="Description"
				DataValueField="Instance"
			    OnSelectedIndexChanged="SetPaymentMethod" 
			    />
			<asp:Literal id="Literal1" runat="server"></asp:Literal>
	</center>
	 </form>
  </body>
</html>

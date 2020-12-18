<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.aspx.cs" Inherits="EfundraisingCRM.Sales.DatePicker" Culture="en-CA" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<base target="_self">
	<title>Date Picker</title>
</head>
<body style="font-family: Tahoma" scroll="no" topmargin="0" bottommargin="0" leftmargin="0"
	rightmargin="0">
	<form id="form1" runat="server">
	<asp:Calendar ID="cDatePicker" Font-Size="Smaller" runat="server" Width="100%" OnSelectionChanged="cDatePicker_SelectionChanged">
		<TitleStyle Font-Size="Larger" />
	</asp:Calendar>
	</form>
</body>
</html>

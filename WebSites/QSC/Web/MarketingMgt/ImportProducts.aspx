<%@ Page language="c#" Codebehind="ImportProducts.aspx.cs" AutoEventWireup="True" enableSessionState="true" Inherits="QSPFulfillment.MarketingMgt.ImportProducts" %>
<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="../CustomerService/ControlerConfirmationPage.ascx" %>
<html>
	<head>
		<title>Import Products</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" onload="return window_onunload()"
		rightmargin="0" marginwidth="0" marginheight="0">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<div style="MARGIN: 3%; WIDTH: 97%; HEIGHT: 97%">
				<h3>Import Products</h3>
				<asp:placeholder id="plhProductContractSearchControl" runat="server"></asp:placeholder>
				<br>
				<div id="divImportForSeason" runat="server">
					<asp:label id="lblImportForSeason" runat="server" cssclass="CSPlainText" font-bold="True">Import for season:</asp:label>
					<br>
					<asp:radiobuttonlist id="rblImportForSeason" runat="server" cssclass="CSPlainText" repeatdirection="Horizontal"
						width="175px">
						<asp:listitem value="F" selected="True">Fall</asp:listitem>
						<asp:listitem value="S">Spring</asp:listitem>
					</asp:radiobuttonlist>
					<br>
				</div>
				<div style="TEXT-ALIGN: left">
					<asp:button id="btnImport" runat="server" text="Import" cssclass="boxlook" onclick="btnImport_Click"></asp:button>
					<asp:button id="btnImportList" runat="server" text="Import whole list" cssclass="boxlook" onclick="btnImportList_Click"></asp:button>
					<asp:button id="btnCancel" runat="server" text="Close" cssclass="boxlook" onclick="btnCancel_Click"></asp:button>
				</div>
			</div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
			<uc1:controlerconfirmationpage id="ctrlControlerConfirmationPage" runat="server"></uc1:controlerconfirmationpage>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>

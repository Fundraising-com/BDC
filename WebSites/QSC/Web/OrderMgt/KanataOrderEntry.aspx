<%@ Register TagPrefix="uc1" TagName="ControlerKanataProductConfirmation" Src="ControlerKanataProductConfirmation.ascx" %>
<%@ Page language="c#" Codebehind="KanataOrderEntry.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.OrderMgt.KanataOrderEntry" %>
<%@ Register TagPrefix="uc1" TagName="ControlerProductMultiSelectForKanata" Src="../OrderMgt/ControlerProductMultiSelectForKanata.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerCampaignsForProductReplacement" Src="../CustomerService/ControlerCampaignsForProductReplacement.ascx"%>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>KanataOrderEntry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body id="BodyTag" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout" onload="return window_onunload()">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<!--#include file="../CustomerService/fctjavascriptall.js"-->
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 931px; POSITION: relative; TOP: 5px; HEIGHT: 213px"
				cellSpacing="0" cellPadding="0" width="931" border="0">
				<TR>
					<TD style="WIDTH: 3px; HEIGHT: 101px"></TD>
					<TD style="WIDTH: 911px; HEIGHT: 101px">
						<P><asp:label id="lblPageTitle" runat="server" CssClass="CSPageTitle">Rapid Order Entry</asp:label></P>
						<P><asp:label id="lblInstructions" runat="server" cssclass="CSPlainText"></asp:label>&nbsp;<asp:hyperlink id="hypHelp" runat="server" Text="Help">Help</asp:hyperlink></P>
						<P><asp:label id="lblAccountCampaignInfo" runat="server" cssclass="CSPlainText" font-bold="True"
								font-size="X-Small"></asp:label></P>
						<P><asp:label id="lblConfirmation" runat="server" cssclass="CSPlainText" font-bold="True" font-size="X-Small"
								visible="False"></asp:label></P>
						<P><uc1:controlercampaignsforproductreplacement id="ctrlControlerCampaignsForKanataOrder" runat="server"></uc1:controlercampaignsforproductreplacement></P>
						<uc1:controlerproductmultiselectforkanata id="ctrlControlerProductMultiSelectForKanata" runat="server" visible="false"></uc1:controlerproductmultiselectforkanata><uc1:controlerkanataproductconfirmation id="ctrlControlerKanataProductConfirmation" runat="server"></uc1:controlerkanataproductconfirmation><uc1:controlerkanataproductconfirmation id="ctrlControlerKanataProductView" runat="server"></uc1:controlerkanataproductconfirmation></TD>
				</TR>
			</TABLE>
			<asp:validationsummary id="ValidationSummary1" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 176px"
				runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary></form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</HTML>

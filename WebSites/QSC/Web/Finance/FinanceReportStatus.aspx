<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register  TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Page language="c#" Codebehind="FinanceReportStatus.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.FinanceReportStatus" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Finance Report Queue</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body leftmargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include virtual="/Qspfulfillment/Includes/Menu.inc" -->
			<div align='center'>
				<p>
					<span style="FONT-WEIGHT: bold; FONT-SIZE: 115%">Report Status for Finance User:
						<asp:label id="lbReportsFor" runat="server" forecolor="Olive" font-names="Verdana" font-size="Medium" />
					</span>
				</p>
				<p>
					<span style="FONT-WEIGHT: bold; FONT-SIZE: 85%">Request new reports:<br>
						<cc1:menu id="menuFinanceLinks" runat="Server" backcolor="#FFFFCC" layout="Vertical" cursor="Pointer"
							font-size="9pt" font-names="Verdana" borderstyle="Dotted" borderwidth="2px" forecolor="Black"
							gridlines="None">
							<selectedmenuitemstyle font-size="9pt" font-names="Verdana"></selectedmenuitemstyle>
						</cc1:menu>
						<p>Choose Report Below:</p>
						<asp:label id="lbPageStatus" runat="server" forecolor="Blue" />
						<asp:datagrid id="DataGrid1" runat="server" width="95%" autogeneratecolumns="False" borderstyle="Solid"
							bordercolor="black" borderwidth="1px">
							<alternatingitemstyle backcolor="WhiteSmoke"></alternatingitemstyle>
							<itemstyle font-size="XX-Small"></itemstyle>
							<headerstyle font-size="XX-Small" font-names="Verdana" font-bold="True" backcolor="#FFFFCC"></headerstyle>
							<columns>
								<asp:templatecolumn headertext="Request Date">
									<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
									<itemstyle horizontalalign="Center"></itemstyle>
									<itemtemplate>
										<%# DataBinder.Eval(Container.DataItem,"CreateDate")%>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headertext="Report Type">
									<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
									<itemstyle horizontalalign="Center"></itemstyle>
									<itemtemplate>
										<%# DataBinder.Eval(Container.DataItem,"ReportType")%>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headertext="Status">
									<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
									<itemstyle horizontalalign="Center"></itemstyle>
									<itemtemplate>
										<%# DataBinder.Eval(Container.DataItem,"LastStatus")%>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headertext="">
									<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
									<itemstyle horizontalalign="Center"></itemstyle>
									<itemtemplate>
										<cc2:webfilestreamerlinkbutton id="lnkWebFileStreamerLinkButtonFinance" runat="server" visible="False">PRINT</cc2:webfilestreamerlinkbutton>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headertext="">
									<headerstyle horizontalalign="Center" verticalalign="Bottom"></headerstyle>
									<itemstyle horizontalalign="Center"></itemstyle>
									<itemtemplate>
										<asp:LinkButton id="btnResubmit" runat="server" Text="Rerun Report" CommandName="Resubmit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"RSSubscriptionId")%>' Visible=False>
										</asp:linkbutton>
									</itemtemplate>
								</asp:templatecolumn>
							</columns>
						</asp:datagrid>
						<br>
			</div>
		</form>
		</SPAN>
	</body>
</html>

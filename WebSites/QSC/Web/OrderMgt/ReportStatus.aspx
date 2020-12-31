<%@ Page language="c#" Codebehind="ReportStatus.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.OrderMgt.ReportStatus" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>BatchList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body leftmargin="0" topmargin="0">
		<center>
			<form id="Form1" method="post" runat="server">
				<!-- #include virtual="/Qspfulfillment/Includes/Menu.inc" -->
				<p><strong><font size="3">Report Status for Batch #
							<asp:label id="Label1" runat="server" forecolor="Olive" font-names="Verdana" font-size="Medium"></asp:label></font></strong></p>
				<p>Choose Report Below:
				</p>
				<asp:label id="Label2" runat="server" forecolor="Blue"></asp:label><asp:datagrid id="DataGrid1" runat="server" width="95%" autogeneratecolumns="False" borderstyle="Solid"
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
								<cc2:webfilestreamerlinkbutton id="lnkWebFileStreamerLinkButtonReports" runat="server" visible="False">PRINT</cc2:webfilestreamerlinkbutton>
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
				</asp:datagrid><br>
			</form>
		</center>
	</body>
</html>

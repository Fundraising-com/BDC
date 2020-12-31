<%@ Page language="c#" Codebehind="ProducePickListChoose.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.ProducePickListChoose" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
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
				<p><strong><font size="3">Choose Reports For Pick List</font></strong></p>
				<table class="Table1px" cellspacing="0" cellpadding="0" width="100%">
					<tr>
						<td class="Table1px" valign="top" align="center" width="50%" bgcolor="#ffffcc">
							<p><font face="verdana" size="2"><b>Available Reports</b></font></p>
						</td>
						<td class="Table1px" valign="top" align="center" width="50%" bgcolor="#ffffcc">
							<p><font face="verdana" size="2"><b>Orders Chosen</b></font></p>
						</td>
					</tr>
					<tr>
						<td class="Table1px" valign="top" align="center">
							<table width="100%" bgcolor="whitesmoke">
								<tr bgcolor="white">
									<td><asp:checkbox id="cbSubReport1" runat="server" checked="True" text="Order Control Sheet" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport2" runat="server" checked="True" text="Pick List" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr bgcolor="white">
									<td><asp:checkbox id="cbSubReport3" runat="server" checked="True" text="Confirm Shipment Report" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport4" runat="server" checked="True" text="Book/CD/Video Labels" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr>
									<td>
										<p>&nbsp;</p>
									</td>
								</tr>
								<tr bgcolor="white">
									<td><asp:checkbox id="cbSubReport5" runat="server" text="Participant Listing" textalign="Right" font-size="XX-Small"
											font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport6" runat="server" text="Homeroom Summary Report (2 copies)" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr bgcolor="white">
									<td><asp:checkbox id="cbSubReport7" runat="server" text="Group Summary Report (2 copies)" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport8" runat="server" text="Magazine Item Summary Report" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr bgcolor="white">
									<td><asp:checkbox id="cbSubReport9" runat="server" text="Problem Solver Report" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport10" runat="server" text="Classroom Box Labels" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr bgcolor="white">
									<td><asp:checkbox id="cbSubReport11" runat="server" text="Order Entry Follow-up Report" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport12" runat="server" text="Price Discrepancy Report" textalign="Right"
											font-size="XX-Small" font-name="Verdana" font-names="Verdana"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport13" runat="server" text="Packing Slip" textalign="Right" font-size="XX-Small"
											font-name="Verdana" font-names="Verdana"></asp:checkbox>
									</td>
								</tr>
							</table>
							<p></p>
							<asp:button id="Button1" runat="server" text="Preview" cssclass="Button4"></asp:button>&nbsp;&nbsp;&nbsp;
							<asp:button id="Button2" runat="server" text="Print" cssclass="Button1" onclick="Button2_Click"></asp:button><font size="-2"><br>
								&nbsp;</font></td>
						<td class="Table1px" valign="top" align="center" width="50%" bgcolor="#ffffff"><font size="-2"><strong><font size="2">Warehouse:</font></strong>
								<asp:label id="lblWarehouse" runat="server"></asp:label><br>
							</font>
							<asp:datagrid id="DataGrid1" runat="server" width="95%" autogeneratecolumns="False" borderstyle="Solid"
								bordercolor="black" borderwidth="1px">
								<headerstyle backcolor="#ffffcc" font-size="xx-small" font-name="Verdana" font-bold="True"></headerstyle>
								<itemstyle font-size="xx-small"></itemstyle>
								<alternatingitemstyle backcolor="WhiteSmoke"></alternatingitemstyle>
								<columns>
									<asp:templatecolumn headertext="Order ID" headerstyle-font-bold="True" headerstyle-verticalalign="Bottom"
										headerstyle-horizontalalign="Center" itemstyle-verticalalign="Top" itemstyle-horizontalalign="Center">
										<itemtemplate>
											<a href='/QSPFulfillment/OrderMgt/BatchViewer.aspx?BatchOrderId=<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' target=_blank>
												<%# DataBinder.Eval(Container.DataItem,"OrderId")%>
												<asp:label id="HOrderId" runat="server" visible="false">
													<%# DataBinder.Eval(Container.DataItem,"OrderId")%>
												</asp:label>
											</a>
										</itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn headertext="Date Order<BR>Received" itemstyle-verticalalign="Top" headerstyle-verticalalign="Bottom"
										headerstyle-horizontalalign="Center" itemstyle-horizontalalign="Center">
										<itemtemplate>
											<%# DataBinder.Eval(Container.DataItem,"DateOrdered")%>
										</itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn headertext="Campaign Id" itemstyle-verticalalign="Top" headerstyle-verticalalign="Bottom"
										headerstyle-horizontalalign="Center" itemstyle-horizontalalign="Center">
										<itemtemplate>
											<%# DataBinder.Eval(Container.DataItem,"CampaignId")%>
										</itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn headertext="Order Type" itemstyle-verticalalign="Top" headerstyle-verticalalign="Bottom"
										headerstyle-horizontalalign="Center" itemstyle-horizontalalign="Center">
										<itemtemplate>
											<%# DataBinder.Eval(Container.DataItem,"OrderType")%>
										</itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn headertext="Estimated<BR>Order Amount" itemstyle-verticalalign="Top" headerstyle-verticalalign="Bottom"
										headerstyle-horizontalalign="Center" itemstyle-horizontalalign="Center">
										<itemtemplate>
											<%# DataBinder.Eval(Container.DataItem,"ItemTotalCost", "{0:c}")%>
										</itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn headertext="Item Quantity" itemstyle-verticalalign="Top" headerstyle-verticalalign="Bottom"
										headerstyle-horizontalalign="Center" itemstyle-horizontalalign="Center">
										<itemtemplate>
											<%# DataBinder.Eval(Container.DataItem,"ItemQuantity")%>
											<br>
											<asp:label id="lblBreakdown" runat="server"></asp:label><br>
										</itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn headertext="Qualifier" itemstyle-verticalalign="Top" headerstyle-verticalalign="Bottom"
										headerstyle-horizontalalign="Center" itemstyle-horizontalalign="Center">
										<itemtemplate>
											<%# DataBinder.Eval(Container.DataItem,"OrderQualifier")%>
										</itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn headertext="On Hand<BR>Quantity Check" itemstyle-verticalalign="Top" headerstyle-verticalalign="Bottom"
										headerstyle-horizontalalign="Center" itemstyle-horizontalalign="Center">
										<itemtemplate>
											<asp:image id="Image1" runat="server" imageurl="plus.gif"></asp:image>
										</itemtemplate>
									</asp:templatecolumn>
								</columns>
							</asp:datagrid>
							<p><asp:label id="lblError" runat="server"></asp:label></p>
						</td>
					</tr>
				</table>
			</form>
		</center>
	</body>
</html>

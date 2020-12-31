<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Page language="c#" Codebehind="PrintMagQueueChoose.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.PrintMagQueueChoose" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BatchList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftmargin="0" topmargin="0">
		<CENTER>
			<form id="Form1" method="post" runat="server">
				<!-- #include virtual="/Qspfulfillment/Includes/Menu.inc" -->
				<P><STRONG><FONT size="3">Choose Reports For Magazine Queue</FONT></STRONG></P>
				<table width="100%" class="Table1px" cellpadding="0" cellspacing="0">
					<tr>
						<td vAlign="top" align="center" width="50%" bgColor="#ffffcc" class="Table1px">
							<P><font face="verdana" size="2"><b>Available Reports</b></font></P>
						</td>
						<td vAlign="top" align="center" width="50%" bgColor="#ffffcc" class="Table1px">
							<P><font face="verdana" size="2"><b>Orders Chosen</b></font></P>
						</td>
					</tr>
					<tr>
						<td vAlign="top" align="center" class="Table1px">
							<table width="100%" bgColor="whitesmoke">
								<tr bgColor="white">
									<td><asp:checkbox id="cbSubReport1" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Order Control Sheet" Checked="True"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport2" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Pick List" Checked="True"></asp:checkbox></td>
								</tr>
								<tr bgColor="white">
									<td><asp:checkbox id="cbSubReport3" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Confirm Shipment Report" Checked="True"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport4" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Book/CD/Video Labels" Checked="True"></asp:checkbox></td>
								</tr>
								<tr>
									<td>
										<P>&nbsp;</P>
									</td>
								</tr>
								<tr bgColor="white">
									<td><asp:checkbox id="cbSubReport5" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Participant Listing"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport6" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Homeroom Summary Report (2 copies)"></asp:checkbox></td>
								</tr>
								<tr bgColor="white">
									<td><asp:checkbox id="cbSubReport7" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Group Summary Report (2 copies)"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport8" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Magazine Item Summary Report"></asp:checkbox></td>
								</tr>
								<tr bgColor="white">
									<td><asp:checkbox id="cbSubReport9" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Problem Solver Report"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport10" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Classroom Box Labels"></asp:checkbox></td>
								</tr>
								<tr bgColor="white">
									<td><asp:checkbox id="cbSubReport11" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Order Entry Follow-up Report"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport12" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Price Discrepancy Report"></asp:checkbox></td>
								</tr>
							</table>
							<P></P>
							<asp:Button id="Button1" runat="server" Text="Preview" CssClass="Button4" Visible="False"></asp:Button>&nbsp;&nbsp;&nbsp;
							<asp:Button id="Button2" runat="server" Text="Print" CssClass="Button1" onclick="Button2_Click"></asp:Button><font size="-2"><br>
								&nbsp;</font></td>
						<td vAlign="top" align="center" width="50%" bgColor="#ffffff" class="Table1px">
							<font size="-2"><STRONG><FONT size="2">Warehouse:</FONT></STRONG>
								<asp:Label id="lblWarehouse" runat="server"></asp:Label><br>
							</font>
							<asp:datagrid id="DataGrid1" runat="server" BorderWidth="1px" BorderColor="black" BorderStyle="Solid"
								AutoGenerateColumns="False" Width="95%">
								<HeaderStyle BackColor="#ffffcc" Font-Size="xx-small" Font-Name="Verdana" Font-Bold="True"></HeaderStyle>
								<ItemStyle Font-Size="xx-small"></ItemStyle>
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Order ID" HeaderStyle-Font-Bold="True" HeaderStyle-VerticalAlign="Bottom"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<a href='/QSPFulfillment/OrderMgt/BatchViewer.aspx?BatchOrderId=<%# DataBinder.Eval(Container.DataItem,"OrderId")%>' target=_blank>
												<%# DataBinder.Eval(Container.DataItem,"OrderId")%>
												<asp:Label id="HOrderId" runat="server" Visible="false">
													<%# DataBinder.Eval(Container.DataItem,"OrderId")%>
												</asp:Label>
											</a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Date Order<BR>Received" ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Bottom"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem,"DateOrdered")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Campaign Id" ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Bottom"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem,"CampaignId")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Order Type" ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Bottom"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem,"OrderType")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Estimated<BR>Order Amount" ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Bottom"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem,"ItemTotalCost", "{0:c}")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Item Quantity" ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Bottom"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem,"ItemQuantity")%>
											<br>
											<asp:Label id="lblBreakdown" runat="server"></asp:Label><BR>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Qualifier" ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Bottom"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem,"OrderQualifier")%>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
							<P>
								<asp:Label id="lblError" runat="server"></asp:Label></P>
						</td>
					</tr>
				</table>
			</form>
		</CENTER>
	</body>
</HTML>

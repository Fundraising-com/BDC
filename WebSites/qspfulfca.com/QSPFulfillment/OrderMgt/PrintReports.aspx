<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Page language="c#" Codebehind="PrintReports.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.PrintReports" %>
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
	<body leftMargin="0" topMargin="0">
		<CENTER>
			<form id="Form1" method="post" runat="server">
				<!-- #include virtual="/Qspfulfillment/Includes/Menu.inc" -->
				<P><STRONG><FONT size="3">Choose Reports To Generate</FONT></STRONG></P>
				<P>
					Order #
					<asp:Label id="lblBatchOrderId" runat="server" Font-Size="Medium">Label</asp:Label></P>
				<P>Use this page to create "one-off" reports for a batch.&nbsp; These reports will 
					be submitted for<BR>
					processing and should be available for printing/viewing within 5-10 minutes.</P>
				<table class="Table1px" cellSpacing="0" cellPadding="0" width="668">
					<tr>
						<td class="Table1px" vAlign="top" align="center" width="668" bgColor="#ffffcc" style="WIDTH: 668px">
							<P><font face="verdana" size="2"><b>Available Reports</b></font></P>
						</td>
					</tr>
					<tr>
						<td class="Table1px" vAlign="top" align="center" style="WIDTH: 668px">
							<table width="100%" bgColor="whitesmoke">
								<tr bgColor="white">
									<td><asp:checkbox id="cbSubReport1" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Order Control Sheet"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport2" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Pick List"></asp:checkbox></td>
								</tr>
								<tr bgColor="white">
									<td><asp:checkbox id="cbSubReport3" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Confirm Shipment Report"></asp:checkbox></td>
								</tr>
								<tr>
									<td><asp:checkbox id="cbSubReport4" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Book/CD/Video Labels"></asp:checkbox></td>
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
											TextAlign="Right" Text="Homeroom Summary Report"></asp:checkbox></td>
								</tr>
								<tr bgColor="white">
									<td><asp:checkbox id="cbSubReport7" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Group Summary Report"></asp:checkbox></td>
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
								<tr>
									<td><asp:checkbox id="cbSubReport13" runat="server" Font-Names="Verdana" Font-Name="Verdana" Font-Size="XX-Small"
											TextAlign="Right" Text="Packing Slip"></asp:checkbox>
									</td>
								</tr>
							</table>
							<P></P>
							&nbsp;&nbsp;&nbsp;
							<asp:button id="Button2" runat="server" Text="Print" CssClass="Button1" onclick="Button2_Click"></asp:button><font size="-2"><br>
								&nbsp;</font></td>
					</tr>
				</table>
			</form>
		</CENTER>
	</body>
</HTML>

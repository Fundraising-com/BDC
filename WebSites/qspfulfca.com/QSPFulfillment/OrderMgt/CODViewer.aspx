<%@ Page language="c#" Codebehind="CODViewer.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.CODViewer" %>
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
				<STRONG><FONT face="Verdana" size="3">Batch&nbsp;Order&nbsp;Detail&nbsp;Viewer<BR>
					</FONT></STRONG>
				<BR>
				<table class="Table1px" cellSpacing="0" cellPadding="0" width="100%">
					<tr>
						<td vAlign="top">
							<table class="Table1px" cellSpacing="0" cellPadding="2" width="100%">
								<tr>
									<td bgColor="#ffffcc"><font face="verdana" color="black" size="2"><b>General Info</b></font></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="2" width="100%">
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>COH Id:</b></font></td>
												<td vAlign="top"><asp:label id="lblCOHId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Trans Id:</b></font></td>
												<td vAlign="top"><asp:label id="lblTransId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Product Code:</b></font></td>
												<td vAlign="top"><asp:label id="lblProductCode" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Product Name:</b></font></td>
												<td vAlign="top"><asp:label id="lblProductName" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Changed By:</b></font></td>
												<td vAlign="top"><asp:label id="lblChangeUserId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Change Date:</b></font></td>
												<td vAlign="top"><asp:label id="lblChangeDate" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Creation Date:</b></font></td>
												<td vAlign="top"><asp:label id="lblCreationDate" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Status:</b></font></td>
												<td vAlign="top"><asp:label id="lblStatus" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
						<td vAlign="top">
							<table class="Table1px" cellSpacing="0" cellPadding="2" width="100%">
								<tr>
									<td bgColor="#ffffcc"><font face="verdana" color="black" size="2"><b>Payment/Shipment Info</b></font></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="2" width="100%">
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Invoice Number :</b></font></td>
												<td vAlign="top"><asp:label id="lblInvoiceNumber" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Quantity Reserved :</b></font></td>
												<td vAlign="top"><asp:label id="lblQuantityReserved" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Quantity Shipped :</b></font></td>
												<td vAlign="top"><asp:label id="lblQuantityShipped" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table class="Table1px" cellSpacing="0" cellPadding="2" width="100%">
								<tr>
									<td bgColor="#ffffcc"><font face="verdana" color="black" size="2"><b>Quantity/Price Info</b></font></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="2" width="100%">
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Quantity :</b></font></td>
												<td vAlign="top"><asp:label id="lblQuantity" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Total Price :</b></font></td>
												<td vAlign="top"><asp:label id="lblPrice" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Catalog Price :</b></font></td>
												<td vAlign="top"><asp:label id="lblCatalogPrice" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</form>
		</CENTER>
		<BR>
		<BR>
	</body>
</HTML>

<%@ Page language="c#" Codebehind="ConfirmShipment.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.ConfirmShipment" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="UC" TagName="Date" Src="../Common/DateEntry.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ConfirmShipment</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<br>
			<table border="0" cellspacing="2" cellpadding="2">
				<TBODY>
					<tr>
						<td noWrap align="right">Operator Name</td>
						<td noWrap align="left">
							<asp:TextBox ID="tbOperatorName" Runat="server"></asp:TextBox>
						</td>
						<td noWrap align="right">Shipment ID</td>
						<td noWrap align="left">
							<asp:TextBox ID="tbShipmentID" Runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td noWrap align="right">Carrier</td>
						<td noWrap align="left">
							<asp:TextBox ID="tbCarrier" Runat="server"></asp:TextBox>
						</td>
						<td noWrap align="right">Waybill #</td>
						<td noWrap align="left">
							<asp:TextBox ID="tbWayBillNum" Runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td noWrap align="right">Date Shipped</td>
						<td noWrap align="left">
							<UC:DATE id="dtDateShipped" runat="server" Required="False" />
						</td>
						<td noWrap align="right">Weight</td>
						<td noWrap align="left">
							<asp:TextBox ID="tbWeight" Runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td noWrap align="right">Total Cartons Shipped</td>
						<td noWrap align="left">
							<asp:TextBox ID="tbTotalCartonsShipped" Runat="server"></asp:TextBox>
						</td>
						<td noWrap align="right">Number of Skids</td>
						<td noWrap align="left">
							<asp:TextBox ID="tbNumSkids" Runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td noWrap align="right">Expected Delivery Date</td>
						<td noWrap align="left">
							<UC:DATE id="dtExpectedDeliveryDate" runat="server" Required="False" />
						</td>
						<td noWrap align="right">Note</td>
						<td noWrap align="left">
							<asp:TextBox ID="tbNote" Runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td nowrap align="center" colspan="4">
							<asp:DataGrid ID="OrdersToShipDataGrid" Runat="server" AllowPaging="False" AllowSorting="False"
								AutoGenerateColumns="False">
								<Columns>
									<asp:TemplateColumn HeaderText="Order ID">
										<itemtemplate>
											<asp:TextBox ID="tbOrderID" Runat="server" Text="BLAH"></asp:TextBox>
										</itemtemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ship To Account ID">
										<itemtemplate>
											<asp:TextBox ID="tbShipToAccountID" Runat="server" Text="BLAH"></asp:TextBox>
										</itemtemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="FOR">
										<itemtemplate>
											<asp:TextBox ID="tbFor" Runat="server" Text="BLAH"></asp:TextBox>
										</itemtemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ship To FMID">
										<itemtemplate>
											<asp:TextBox ID="tbShipToFMID" Runat="server" Text="BLAH"></asp:TextBox>
										</itemtemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="CampaignID">
										<itemtemplate>
											<asp:TextBox ID="tbCampaignID" Runat="server" Text="BLAH"></asp:TextBox>
										</itemtemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Slip ?">
										<itemtemplate>
											<asp:TextBox ID="tbSlip" Runat="server" Text="BLAH"></asp:TextBox>
										</itemtemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Edit Order Items To Ship Button">
										<ItemTemplate>
											<asp:Button ID="btEditOrderItems" Runat="server" Text="Edit Order Items to Ship"></asp:Button>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:DataGrid>
						</td>
					</tr>
		</form>
		</TBODY></TABLE>
	</body>
</HTML>

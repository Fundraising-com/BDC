<%@ Page language="c#" Codebehind="COHViewer.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.COHViewer" %>
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
				<STRONG><FONT face="Verdana" size="3">Batch&nbsp;Order&nbsp;Viewer<BR>
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
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Batch Id:</b></font></td>
												<td vAlign="top"><asp:label id="lblBatchId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Batch Date:</b></font></td>
												<td vAlign="top"><asp:label id="lblBatchDate" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Batch Sequence:</b></font></td>
												<td vAlign="top"><asp:label id="lblOrderBatchSequence" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Account Id:</b></font></td>
												<td vAlign="top"><asp:label id="lblAccountId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Changed By:</b></font></td>
												<td vAlign="top"><asp:label id="lblChangeUserId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Change Date:</b></font></td>
												<td vAlign="top"><asp:label id="lblChangeDate" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Creation Date:</b></font></td>
												<td vAlign="top"><asp:label id="lblCreationDate" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Student:</b></font></td>
												<td vAlign="top"><asp:label id="lblStudent" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Status:</b></font></td>
												<td vAlign="top"><asp:label id="lblStatus" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>First Status:</b></font></td>
												<td vAlign="top"><asp:label id="lblFirstStatus" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Type:</b></font></td>
												<td vAlign="top"><asp:label id="lblType" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
						<td vAlign="top">
							<table class="Table1px" cellSpacing="0" cellPadding="2" width="100%">
								<tr>
									<td bgColor="#ffffcc"><font face="verdana" color="black" size="2"><b>Payment Info</b></font></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="2" width="100%">
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Customer Bill To Instance :</b></font></td>
												<td vAlign="top"><asp:label id="lblCustomerBillToInstance" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Total Processing Fee :</b></font></td>
												<td vAlign="top"><asp:label id="lblTotalProcessingFee" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Total Processing Fee A :</b></font></td>
												<td vAlign="top"><asp:label id="lblTotalProcessingFeeA" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Processing Fee Tax:</b></font></td>
												<td vAlign="top"><asp:label id="lblProcessingFeeTax" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Processing Fee Tax A :</b></font></td>
												<td vAlign="top"><asp:label id="lblProcessingFeeTaxA" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Processing Fee TransId :</b></font></td>
												<td vAlign="top"><asp:label id="lblProcessingFeeTransId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Last Sent Invoice Date :</b></font></td>
												<td vAlign="top"><asp:label id="lblLastSentInvoiceDate" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b># Invoices Sent :</b></font></td>
												<td vAlign="top"><asp:label id="lblNumberInvoicesSent" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Force Invoice :</b></font></td>
												<td vAlign="top"><asp:label id="lblForceInvoice" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Payment Method :</b></font></td>
												<td vAlign="top"><asp:label id="lblPaymentMethod" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<p></p>
				<FONT face="Verdana" size="3">Details Of Order</FONT><BR>
				<asp:datagrid id="DataGrid1" runat="server" Width="95%" AutoGenerateColumns="False" BorderStyle="Solid"
					BorderColor="black" BorderWidth="1px">
					<HeaderStyle BackColor="#ffffcc" Font-Size="xx-small" Font-Name="Verdana" Font-Bold="True"></HeaderStyle>
					<ItemStyle Font-Size="xx-small"></ItemStyle>
					<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Trans ID" HeaderStyle-Font-Bold="True" HeaderStyle-VerticalAlign="Bottom"
							HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<a href='/QSPFulfillment/OrderMgt/CODViewer.aspx?COHId=<%# DataBinder.Eval(Container.DataItem,"CustomerOrderHeaderInstance")%>&TransId=<%# DataBinder.Eval(Container.DataItem,"TransId")%>'>
									<%# DataBinder.Eval(Container.DataItem,"TransId")%>
									<asp:Label id="TransId" runat="server" Visible="false">
										<%# DataBinder.Eval(Container.DataItem,"TransId")%>
									</asp:Label>
								</a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Product Name" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"ProductCode")%>
								--
								<%# DataBinder.Eval(Container.DataItem,"ProductName")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Quantity" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"Quantity")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Total Price" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"Price", "{0:c}")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Status" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"StatusInstance")%>
								-
								<%# DataBinder.Eval(Container.DataItem,"StatusDesc")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Create Date" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"CreationDate")%>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:datagrid><BR>
			</form>
		</CENTER>
		<BR>
		<BR>
	</body>
</HTML>

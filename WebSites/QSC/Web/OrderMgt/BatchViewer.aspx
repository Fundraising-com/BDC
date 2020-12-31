<%@ Page language="c#" Codebehind="BatchViewer.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.BatchViewer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BatchList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	    <style type="text/css">
            .style1
            {
                height: 5px;
            }
        </style>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<CENTER>
			<form id="Form1" method="post" runat="server">
				<STRONG><FONT face="Verdana" size="3">Batch Viewer<BR>
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
												<td vAlign="top"><asp:label id="lblBatchId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Batch Date:</b></font></td>
												<td vAlign="top"><asp:label id="lblBatchDate" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Order Id:</b></font></td>
												<td vAlign="top"><asp:label id="lblOrderId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Order Id Incentive:</b></font></td>
												<td vAlign="top"><asp:label id="lblOrderIdIncentive" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Order Type:</b></font></td>
												<td vAlign="top"><asp:label id="lblOrderType" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Order Qualifier:</b></font></td>
												<td vAlign="top"><asp:label id="lblOrderQualifier" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Batch Status:</b></font></td>
												<td vAlign="top"><asp:label id="lblStatus" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Original Batch Status:</b></font></td>
												<td vAlign="top"><asp:label id="lblOriginalBatchStatus" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Incentive Calculation 
															Status:</b></font></td>
												<td vAlign="top"><asp:label id="lblIncentiveCalculationStatus" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Is Incentive:</b></font></td>
												<td vAlign="top"><asp:label id="lblIsIncentive" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Is Staff Order:</b></font></td>
												<td vAlign="top"><asp:label id="lblIsStaffOrder" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Account Id:</b></font></td>
												<td vAlign="top"><asp:label id="lblAccountId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Campaign Id:</b></font></td>
												<td vAlign="top"><asp:label id="lblCampaignId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Country Code:</b></font></td>
												<td vAlign="top"><asp:label id="lblCountryCode" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
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
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Create Date:</b></font></td>
												<td vAlign="top"><asp:label id="lblCreateDate" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Created By:</b></font></td>
												<td vAlign="top"><asp:label id="lblUserIdCreated" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Contact Name:</b></font></td>
												<td vAlign="top"><asp:label id="lblContactName" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Contact Email:</b></font></td>
												<td vAlign="top"><asp:label id="lblContactEmail" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Contact Phone:</b></font></td>
												<td vAlign="top"><asp:label id="lblContactPhone" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Comment:</b></font></td>
												<td vAlign="top"><asp:label id="lblComment" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table class="Table1px" cellSpacing="0" cellPadding="2" width="100%">
								<tr>
									<td bgColor="#ffffcc"><font face="verdana" color="black" size="2"><b>Misc Info</b></font></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="2" width="100%">
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Override Pct :</b></font></td>
												<td vAlign="top"><asp:label id="lblOverridePct" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Pct State:</b></font></td>
												<td vAlign="top"><asp:label id="lblPctState" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Is DM Approved:</b></font></td>
												<td vAlign="top"><asp:label id="lblIsDMApproved" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Ref Number:</b></font></td>
												<td vAlign="top"><asp:label id="lblRefNumber" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Inquire Upon Complete:</b></font></td>
												<td vAlign="top"><asp:label id="lblInquireUponComplete" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
						<td vAlign="top">
							<table class="Table1px" cellSpacing="0" cellPadding="2" width="100%">
								<tr>
									<td bgColor="#ffffcc"><font face="verdana" color="black" size="2"><b>Content Info</b></font></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="2" width="100%">
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Entered Amount:</b></font></td>
												<td vAlign="top"><asp:label id="lblEnterredAmount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Entered Count:</b></font></td>
												<td vAlign="top"><asp:label id="lblEnterredCount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Calculated Count:</b></font></td>
												<td vAlign="top"><asp:label id="lblCalculatedAmount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Teacher Count:</b></font></td>
												<td vAlign="top"><asp:label id="lblTeacherCount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Student Count:</b></font></td>
												<td vAlign="top"><asp:label id="lblStudentCount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Customer Count:</b></font></td>
												<td vAlign="top"><asp:label id="lblCustomerCount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Order Count:</b></font></td>
												<td vAlign="top"><asp:label id="lblOrderCount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>KE3 FileName:</b></font></td>
												<td vAlign="top"><asp:label id="lblKE3" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Order Count Accept:</b></font></td>
												<td vAlign="top"><asp:label id="lblOrderCountAccept" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Order Detail Count:</b></font></td>
												<td vAlign="top"><asp:label id="lblOrderDetailCount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>ReportedEnvelopes:</b></font></td>
												<td vAlign="top"><asp:label id="lblReportedEnvelopes" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Magnet Booklet Count:</b></font></td>
												<td vAlign="top"><asp:label id="lblMagnetBookletCount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Magnet Card Count:</b></font></td>
												<td vAlign="top"><asp:label id="lblMagnetCardCount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Magnet Good Card Count:</b></font></td>
												<td vAlign="top"><asp:label id="lblMagnetGoodCardCount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Magnet Cards Mailed:</b></font></td>
												<td vAlign="top"><asp:label id="lblMagnetCardsMailed" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Magnet Mail Date:</b></font></td>
												<td vAlign="top"><asp:label id="lblMagnetMailDate" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table class="Table1px" cellSpacing="0" cellPadding="2" width="100%">
								<tr>
									<td bgColor="#ffffcc"><font face="verdana" color="black" size="2"><b>Process Info</b></font></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="2" width="100%">
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Start Import Time:</b></font></td>
												<td vAlign="top"><asp:label id="lblStartImportTime" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>End Import Time:</b></font></td>
												<td vAlign="top"><asp:label id="lblEndImportTime" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Import Time (secs):</b></font></td>
												<td vAlign="top"><asp:label id="lblImportTimeSeconds" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Clerk:</b></font></td>
												<td vAlign="top"><asp:label id="lblClerk" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Date Keyed:</b></font></td>
												<td vAlign="top"><asp:label id="lblDateKeyed" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Date Batch Completed:</b></font></td>
												<td vAlign="top"><asp:label id="lblDateBatchCompleted" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Date Sent:</b></font></td>
												<td vAlign="top"><asp:label id="lblDateSent" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Date Received:</b></font></td>
												<td vAlign="top"><asp:label id="lblDateReceived" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
						<td vAlign="top">
							<table class="Table1px" cellSpacing="0" cellPadding="2" width="100%">
								<tr>
									<td bgColor="#ffffcc"><font face="verdana" color="black" size="2"><b>Shipment Info</b></font></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="2" width="100%">
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Pick Date:</b></font></td>
												<td vAlign="top"><asp:label id="lblPickDate" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Pick Line:</b></font></td>
												<td vAlign="top"><asp:label id="lblPickLine" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>ShipTo Address Id :</b></font></td>
												<td vAlign="top"><asp:label id="lblShipToAddressId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>ShipTo Account Id :</b></font></td>
												<td vAlign="top"><asp:label id="lblShipToAccountId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>ShipTo FMId :</b></font></td>
												<td vAlign="top"><asp:label id="lblShipToFMId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Order Delivery Date :</b></font></td>
												<td vAlign="top"><asp:label id="lblOrderDeliveryDate" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table class="Table1px" cellSpacing="0" cellPadding="2" width="100%">
								<tr>
									<td bgColor="#ffffcc"><font face="verdana" color="black" size="2"><b>Payment Info</b></font></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="2" width="100%">
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>BillTo Address Id :</b></font></td>
												<td vAlign="top"><asp:label id="lblBillToAddressId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>BillTo FMId :</b></font></td>
												<td vAlign="top"><asp:label id="lblBillToFMId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Payment Send :</b></font></td>
												<td vAlign="top"><asp:label id="lblPaymentSend" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Sales Before Tax :</b></font></td>
												<td vAlign="top"><asp:label id="lblSalesBeforeTax" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Check Payable To QSP Amount 
															:</b></font></td>
												<td vAlign="top"><asp:label id="lblCheckPayableToQSPAmount" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Payment Batch Date :</b></font></td>
												<td vAlign="top"><asp:label id="lblPaymentBatchDate" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Payment BatchId :</b></font></td>
												<td vAlign="top"><asp:label id="lblPaymentBatchId" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Group Profit :</b></font></td>
												<td vAlign="top"><asp:label id="lblGroupProfit" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Order Amt Due :</b></font></td>
												<td vAlign="top"><asp:label id="lblOrderAmtDue" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Campaign Net Total :</b></font></td>
												<td vAlign="top"><asp:label id="lblCampaignNetTotal" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Magnet Postage :</b></font></td>
												<td vAlign="top"><asp:label id="lblMagnetPostage" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
											<tr>
												<td vAlign="top" class="style1"><font face="verdana" color="black" size="1"><b>Processing fees :</b></font></td>
												<td vAlign="top" class="style1"><asp:label id="lblProcessingFees" runat="server" Font-Size="XX-Small" Font-Names="Verdana" /></td>
											</tr>
											<tr>
												<td vAlign="top"><font face="verdana" color="black" size="1"><b>Is Invoiced :</b></font></td>
												<td vAlign="top"><asp:label id="lblIsInvoiced" runat="server" Font-Size="XX-Small" Font-Names="Verdana"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<p>
				</p>
				<FONT face="Verdana" size="3">Orders In Batch</FONT><BR>
				<asp:datagrid id="DataGrid1" runat="server" BorderWidth="1px" BorderColor="black" BorderStyle="Solid"
					AutoGenerateColumns="False" Width="95%">
					<HeaderStyle BackColor="#ffffcc" Font-Size="xx-small" Font-Name="Verdana" Font-Bold="True"></HeaderStyle>
					<ItemStyle Font-Size="xx-small"></ItemStyle>
					<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Order ID" HeaderStyle-Font-Bold="True" HeaderStyle-VerticalAlign="Bottom"
							HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<a href='/QSPFulfillment/OrderMgt/COHViewer.aspx?COHId=<%# DataBinder.Eval(Container.DataItem,"Instance")%>'>
									<%# DataBinder.Eval(Container.DataItem,"Instance")%>
									<asp:Label id="COHInstance" runat="server" Visible="false">
										<%# DataBinder.Eval(Container.DataItem,"Instance")%>
									</asp:Label>
								</a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Name" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"AccountName")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Create Date" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"CreationDate")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Order Amount" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"ItemTotalCost", "{0:c}")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Item Quantity" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
							ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem,"ItemQuantity")%>
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

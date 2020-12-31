<%@ Register TagPrefix="uc1" TagName="ControlerSubscriptionStatusHistory" Src="ControlerSubscriptionStatusHistory.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MainSubscriptionInfo.ascx.cs" Inherits="QSPFulfillment.CustomerService.MainSubscriptionInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ControlerPaymentInfo" Src="ControlerPaymentInfo.ascx" %>
<br>
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr valign="top" align="center" width="50%">
		<td>
			<table class="csTable" id="Table1" cellspacing="0" cellpadding="2" width="90%" border="0">
				<tr class="csTableHeader">
					<td nowrap colspan="2"><asp:label id="Label50" runat="server" font-bold="True">General Information</asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label7" runat="server" cssclass="csPlainText">Recipient Name</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblCustomerName" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label1" runat="server" cssclass="csPlainText">COH Instance</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblCOHInstance" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label2" runat="server" cssclass="csPlainText">Trans ID</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblTransID" runat="server"></asp:label></td>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label9" runat="server" cssclass="csPlainText">Remit Batch ID</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblRemitBatchID" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label10" runat="server" cssclass="csPlainText">Remit Batch Date</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblRemitBatchDate" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap>
						<asp:label id="Label12" runat="server" cssclass="csPlainText">Run ID</asp:label></td>
					<td class="csTableItems" nowrap>
						<asp:label id="lblRunID" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap>
						<asp:label id="Label14" runat="server" cssclass="csPlainText">Remit Batch Count</asp:label></td>
					<td class="csTableItems" nowrap>
						<asp:label id="lblRemitBatchCount" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label11" runat="server" cssclass="csPlainText"> Order Keyed Date</asp:label>
					<td class="csTableItems" nowrap><asp:label id="lblOrderKeyedDate" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label3" runat="server" cssclass="csPlainText"> Status</asp:label>
					<td class="csTableItems" nowrap><asp:label id="lblOrderItemStatus" runat="server"></asp:label></td>
				</tr>
                  <tr>
					<td class="csTableSubHeader" nowrap><asp:label id="lblCustomerOrderIDTitle" runat="server" cssclass="csPlainText">Customer Order ID</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblCustomerOrderID" runat="server"></asp:label></td>
				</tr>
                 <tr>
					<td class="csTableSubHeader" nowrap><asp:label id="lblInvoiceIDTitle" runat="server" cssclass="csPlainText">Invoice ID</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblInvoiceID" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap>&nbsp;</td>
					<td class="csTableItems" nowrap>&nbsp;</td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label39" runat="server" cssclass="csPlainText">New/Renew</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblNewRenew" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label43" runat="server" cssclass="csPlainText">Number of Issues</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblNumberofIssues" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap>&nbsp;</td>
					<td class="csTableItems" nowrap>&nbsp;</td>
				</tr>
                <tr>
					<td class="csTableSubHeader" nowrap><asp:label id="lblShipmentIDTitle" runat="server" cssclass="csPlainText">Shipment ID</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblShipmentID" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="lblShipmentDateTitle" runat="server" cssclass="csPlainText">Shipment Date</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblShipmentDate" runat="server"></asp:label></td>
				</tr>              
                <tr>
					<td class="csTableSubHeader" colspan="2" nowrap><a id="hypLnkOrderForm" runat="server" target="_blank" cssclass="csPlainText">Order Form</a></td>					
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label41" runat="server" cssclass="csPlainText" visible="False">Premium Ind</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblPremiumInd" runat="server" visible="False"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label45" runat="server" cssclass="csPlainText" visible="False">Premium Code</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblPremiumCode" runat="server" visible="False"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" nowrap><asp:label id="Label4" runat="server" cssclass="csPlainText" visible="False">Premium Description</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblPremiumDescription" runat="server" visible="False"></asp:label></td>
				</tr>				
			</table>
		</td>
		<td valign="top" align="center" width="50%">
			<table class="csTable" id="Table8" cellspacing="0" cellpadding="2" width="90%" border="0">
				<tr class="csTableHeader">
					<td valign="top"><asp:label id="Label51" runat="server" font-bold="True">Product Information</asp:label></td>
					<td></td>
				</tr>
				<tr>
					<td class="csTableSubHeader"><asp:label id="Label23" runat="server" cssclass="csPlainText">Product Name</asp:label></td>
					<td class="csTableItems"><asp:label id="lblMagazineTitle" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader"><asp:label id="Label13" runat="server" cssclass="csPlainText">Title Code</asp:label></td>
					<td class="csTableItems"><asp:label id="lblTitleCode" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader"><asp:label id="Label27" runat="server" cssclass="csPlainText">Product Status</asp:label></td>
					<td class="csTableItems"><asp:label id="lblMagazineStatus" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader"><asp:label id="Label46" runat="server" cssclass="csPlainText">Fulfillment House Name</asp:label></td>
					<td class="csTableItems"><asp:label id="lblFulfillmentHouseName" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader"><asp:label id="Label37" runat="server" cssclass="csPlainText">Fullfillment House Contact</asp:label></td>
					<td class="csTableItems"><asp:label id="lblFullContact" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader">
						<asp:label id="Label29" runat="server" cssclass="csPlainText" visible="False">Base Price</asp:label></td>
					<td class="csTableItems">
						<asp:label id="lblPrice" runat="server" visible="False"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader"><asp:label id="Label25" runat="server" cssclass="csPlainText">Catalog Price</asp:label></td>
					<td class="csTableItems"><asp:label id="lblCatalogPrice" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader">
						<asp:label id="Label21" runat="server" cssclass="csPlainText">Paid Price</asp:label></td>
					<td class="csTableItems">
						<asp:label id="lblPriceEntered" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td>
						<br>
					</td>
					<td>
						<br>
					</td>
				</tr>
				<tr>
					<td class="csTableSubHeader">
						<asp:label id="lblRefundAmountTitle" runat="server" cssclass="csPlainText">Refund Amount</asp:label>
					</td>
					<td class="csTableItems">
						<asp:label id="lblRefundAmount" runat="server"></asp:label>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><br>
		</td>
		<td><br>
		</td>
	</tr>
	<tr>
		<td valign="top" align="center"><uc1:controlersubscriptionstatushistory id="ctrlControlerSubscriptionStatusHistory" runat="server"></uc1:controlersubscriptionstatushistory></td>
		<td valign="top" align="center">
			<table class="csTable" id="Table6" cellspacing="0" cellpadding="2" width="90%" border="0">
				<tr class="csTableHeader">
					<td nowrap colspan="2"><asp:label id="Label57" runat="server" font-bold="True">Gift Card</asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" style="WIDTH: 138px" nowrap><asp:label id="Label8" runat="server" cssclass="csPlainText">Is the Gift Card Sent?</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblIsGiftCardSent" runat="server">Label</asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" style="WIDTH: 138px" nowrap><asp:label id="Label15" runat="server" cssclass="csPlainText">Date Gift Card Sent</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblDateGiftCardSent" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" style="WIDTH: 138px" nowrap><asp:label id="Label5" runat="server" cssclass="csPlainText">Donor Name</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblDonorName" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="csTableSubHeader" style="WIDTH: 138px" nowrap><asp:label id="Label6" runat="server" cssclass="csPlainText">Gift Order Type</asp:label></td>
					<td class="csTableItems" nowrap><asp:label id="lblGiftOrderType" runat="server"></asp:label></td>
				</tr>
			</table>
			<br>
			<uc1:controlerpaymentinfo id="ctrlPaymentInfo" runat="server" visible="False"></uc1:controlerpaymentinfo></td>
		<td></td>
	</tr>
</table>
<br>

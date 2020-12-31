<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="ControlerInvoice" Src="ControlerInvoice.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerFM" Src="ControlerFM.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerCampaignProgram" Src="ControlerCampaignProgram.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerOrderItemsTotal" Src="ControlerOrderItemsTotal.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MainOrderInfo.ascx.cs" Inherits="QSPFulfillment.CustomerService.MainOrderInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ControlerAddress" Src="ControlerAddress.ascx" %>
<br>
<table width="100%">
	<tr>
		<td valign="top" align="center" width="50%">
			<table class="CSTable" id="Table2" cellspacing="0" cellpadding="2" width="90%">
				<tr class="CSTableHeader">
					<td>Account Shipping Address
					</td>
				</tr>
				<tr>
					<td><uc1:controleraddress id="ctrlControlerAddressShipToGroup" runat="server"></uc1:controleraddress></td>
				</tr>
			</table>
		</td>
		<td valign="top" align="center" width="50%">
			<table class="CSTable" id="Table3" cellspacing="0" cellpadding="2" width="90%">
				<tr class="CSTableHeader">
					<td>Account Billing Address
					</td>
				</tr>
				<tr>
					<td><uc1:controleraddress id="ctrlControlerAddressBillToGroup" runat="server"></uc1:controleraddress></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td valign="top" align="center" width="50%">
			<table class="CSTable" cellspacing="0" cellpadding="2" width="90%">
				<tr>
					<td class="CSTableHeader" colspan="2">General Order Information</td>
				</tr>
				<tr>
					<td class="CSTableItems" width="100"><asp:label id="Label2" runat="server" cssclass="csPlainText">Order Status</asp:label></td>
					<td class="CSTableItems" align="left"><asp:label id="lblStatus" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="CSTableItems" width="100"><asp:label id="Label5" runat="server" cssclass="csPlainText">Qualifier Name</asp:label></td>
					<td class="CSTableItems" align="left"><asp:label id="lblQualifierName" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="CSTableItems" width="100"><asp:label id="Label8" runat="server" cssclass="csPlainText">Order #</asp:label></td>
					<td class="CSTableItems" align="left"><asp:label id="lblOrderID" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="CSTableItems" width="100">Campaign ID</td>
					<td class="CSTableItems" align="left"><asp:label id="lblCampaignID" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="CSTableItems" width="100"><asp:label id="Label9" runat="server" cssclass="csPlainText">Account Number</asp:label></td>
					<td class="CSTableItems" align="left"><asp:label id="lblAccoutNumber" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="CSTableItems" style="HEIGHT: 6px" width="100"><asp:label id="lbl234" runat="server" cssclass="csPlainText">Recipient Name</asp:label></td>
					<td class="CSTableItems" style="HEIGHT: 6px" align="left"><asp:label id="lblCustomerName" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="CSTableItems" style="HEIGHT: 6px" width="100"><asp:label id="Label10" runat="server" cssclass="csPlainText">Student Name</asp:label></td>
					<td class="CSTableItems" style="HEIGHT: 6px" align="left"><asp:label id="lblStudentName" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td colspan="2"><uc1:controlerfm id="ctrlControlerFM" runat="server"></uc1:controlerfm></td>
				</tr>
			</table>
		</td>
		<td valign="top" align="center" width="50%">
			<table class="CSTable" cellspacing="0" cellpadding="2" width="90%">
				<tr>
					<td class="CSTableHeader" colspan="2">Campaign Program</td>
				</tr>
				<tr>
					<td><uc1:controlercampaignprogram id="ctrlControlerCampaignProgram" runat="server"></uc1:controlercampaignprogram></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td valign="top" align="center" width="50%"><br>
			<br>
			<table class="CSTable" cellspacing="0" cellpadding="2" width="90%">
				<tr>
					<td class="CSTableHeader" colspan="2">Invoice Information</td>
				</tr>
				<tr>
					<td><uc1:controlerinvoice id="ctrlControlerInvoice" runat="server"></uc1:controlerinvoice></td>
				</tr>
			</table>
		</td>
		<td valign="top" align="center" width="50%"><br>
			<br>
			<table class="CSTable" id="Table1" cellspacing="0" cellpadding="2" width="90%">
				<tr>
					<td class="CSTableHeader" colspan="2">Order Totals</td>
				</tr>
				<tr>
					<td><uc1:controlerorderitemstotal id="ctrlControlerOrderItemsTotal" runat="server"></uc1:controlerorderitemstotal></td>
				</tr>
			</table>
		</td>
	</tr>
</table>

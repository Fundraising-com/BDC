<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PeriodSalesControl.ascx.vb" Inherits="StoreFront.StoreFront.PeriodSalesControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table height="205" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Total Merchandise Sales:
		</td>
		<td class="content" noWrap align="right" width="50%">&nbsp;<asp:label id="lblMerchandise" runat="server"></asp:label>&nbsp;</td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Total Discounts Applied:
		</td>
		<td class="content" noWrap align="right" width="50%">&nbsp;<asp:label id="lblTotalDisCounts" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Local Tax Collected:
		</td>
		<td class="content" noWrap align="right" width="50%">&nbsp;<asp:label id="lblLocalTax" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">State/Province Tax Collected:
		</td>
		<td class="content" noWrap align="right" width="50%">&nbsp;<asp:label id="lblStateTax" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Country Tax:
		</td>
		<td class="content" noWrap align="right" width="50%">&nbsp;<asp:label id="lblCountryTax" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Shipping Fees:
		</td>
		<td class="content" noWrap align="right" width="50%">&nbsp;<asp:label id="lblShipFees" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Handling Fees:
		</td>
		<td class="content" noWrap align="right" width="50%">&nbsp;<asp:label id="lblHandling" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="content" noWrap align="right" width="50%">Gift Certificate Redeemed:
		</td>
		<td class="content" noWrap align="right" width="50%">&nbsp;<asp:label id="lblGiftCert" runat="server"></asp:label>&nbsp;</td>
		</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td width="100%" colSpan="2"><IMG height="5" src="images/clear.gif"></td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<td class="Headings" noWrap align="right" width="50%">Order Totals:
		</td>
		<td class="content" align="right" width="50%">&nbsp;<asp:label id="lblOrderTotals" runat="server"></asp:label>&nbsp;</td>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</tr>
	<tr>
		<td class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></td>
	</tr>
</table>

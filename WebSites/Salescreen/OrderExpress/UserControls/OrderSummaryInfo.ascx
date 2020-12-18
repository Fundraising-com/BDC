<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderSummaryInfo" Codebehind="OrderSummaryInfo.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<tr>
		<td>
			<table id="Table933" cellSpacing="0" cellPadding="0" border="0" width="600">
				<tr>
					<td align="left"><asp:label id="Label41" runat="server" CssClass="StandardLabel">
							Sub Total :
						</asp:label></td>
					<td>&nbsp;
					</td>
					<td align="right">
						<asp:label id="lblSubTotal" runat="server" CssClass="DescInfoLabel"></asp:label>
					</td>
				</tr>
				<tr>
					<td align="left"><asp:label id="Label37" runat="server" CssClass="StandardLabel">
							Tax Rate :
						</asp:label></td>
					<td>&nbsp;
					</td>
					<td align="right">
						<asp:label id="lblTaxRate" runat="server" CssClass="DescInfoLabel"></asp:label>
					</td>
				</tr>
				<tr>
					<td align="left"><asp:label id="Label39" runat="server" CssClass="StandardLabel">
							Tax Amount :
						</asp:label></td>
					<td>&nbsp;
					</td>
					<td align="right">
						<asp:label id="lblTaxAmount" runat="server" CssClass="DescInfoLabel"></asp:label>
					</td>
				</tr>
				<tr>
					<td align="left"><asp:label id="Label36" runat="server" CssClass="StandardLabel">
							Shipping Charges :
						</asp:label></td>
					<td>&nbsp;
					</td>
					<td align="right">
						<asp:label id="lblShippingCharges" runat="server" CssClass="DescInfoLabel"></asp:label>
					</td>
				</tr>
				<tr>
					<td align="left"><asp:label id="Label1" runat="server" CssClass="StandardLabel">
							Surcharges :
						</asp:label></td>
					<td>&nbsp;
					</td>
					<td align="right">
						<asp:label id="lblSurcharges" runat="server" CssClass="DescInfoLabel">Calculated on next page</asp:label>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<hr width="100%" SIZE="2">
					</td>
				</tr>
				<tr>
					<td align="left"><asp:label id="Label42" runat="server" CssClass="StandardLabel">
							Grand Total :
						</asp:label></td>
					<td>&nbsp;
					</td>
					<td align="right">
						<asp:label id="lblGrandTotal" runat="server" CssClass="DescInfoLabel"></asp:label>
					</td>
				</tr>
				<tr>
					<td colSpan="3" align="left"><asp:label id="Label14" runat="server" CssClass="StandardLabel" Font-Size="xx-small">
								Invoices will include applicable taxes unless the Organization is exempt.  Tax exempt forms or resale certificates are required with order.  Most forms are available on state websites.  Fax forms to QSP Field Support to avoid taxes on invoices.
							</asp:label>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>

<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerShipementDetail.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerShipementDetail" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<asp:Label runat="server" id="lblMessage"></asp:Label>
<cc2:DataGridObject id="dtgMain" width="100%" runat="server" TemplateCachingBase="Tablename" LoadControlMode="UserControl"
	TemplateDataMode="Table" RowExpanded="DBauer.Web.UI.WebControls.RowStates" AutoGenerateColumns="False"
	AllowPaging="True" BorderStyle="None" BorderWidth="0px" BackColor="White"
	CellPadding="3" SearchMode="0">
<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C">
</SelectedItemStyle>

<ItemStyle ForeColor="Black" CssClass="CSSearchResult">
</ItemStyle>

<HeaderStyle Font-Bold="True" ForeColor="Black" CssClass="CSSearchResult">
</HeaderStyle>

<FooterStyle ForeColor="#000066" CssClass="CSSearchResult" BackColor="White">
</FooterStyle>

<Columns>
<asp:BoundColumn DataField="shipmentid" HeaderText="Shipment ID"></asp:BoundColumn>
<asp:BoundColumn DataField="ProductCode" HeaderText="Product Code"></asp:BoundColumn>
<asp:BoundColumn DataField="ProductName" HeaderText="product Name"></asp:BoundColumn>
<asp:BoundColumn DataField="QuantityOrdered" HeaderText="Quantity Ordered"></asp:BoundColumn>
<asp:BoundColumn DataField="QuantityShipped" HeaderText="Quantity Shipped"></asp:BoundColumn>
</Columns>

<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" CssClass="CSPager" Mode="NumericPages">
</PagerStyle>
</cc2:DataGridObject>

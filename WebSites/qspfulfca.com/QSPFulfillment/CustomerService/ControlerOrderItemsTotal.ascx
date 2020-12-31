<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerOrderItemsTotal.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerOrderItemsTotal" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:Label runat="server" id="lblMessage"></asp:Label>
<cc2:DataGridObject id="dtgMain" width="100%" runat="server" TemplateCachingBase="Tablename" LoadControlMode="UserControl"
	TemplateDataMode="Table" RowExpanded="DBauer.Web.UI.WebControls.RowStates" AutoGenerateColumns="False"
	BorderStyle="None" BorderWidth="0px" BackColor="White" CellPadding="3" SearchMode="0"
	ShowFooter="True">
<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C">
</SelectedItemStyle>

<ItemStyle ForeColor="Black" CssClass="CSTableItems">
</ItemStyle>

<HeaderStyle Font-Bold="True" ForeColor="Black" CssClass="CSTableItems">
</HeaderStyle>

<FooterStyle ForeColor="#000066" CssClass="CSSearchResult" BackColor="White">
</FooterStyle>

<Columns>
<asp:BoundColumn DataField="ProductType" HeaderText="Product Type"></asp:BoundColumn>
<asp:BoundColumn DataField="TotalItems" HeaderText=" Total Items"></asp:BoundColumn>
<asp:TemplateColumn HeaderText="Total Amount">
<ItemTemplate>
				<asp:Label id="lblTotalAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalAmount") %>'>
				</asp:Label>
			
</ItemTemplate>

<FooterTemplate>
				<asp:Label id="lblTotal" runat="server" Text='<%#iTotal.ToString("C")%>'>
					
				</asp:Label>
			
</FooterTemplate>
</asp:TemplateColumn>
</Columns>

<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" CssClass="CSPager" Mode="NumericPages">
</PagerStyle>
</cc2:DataGridObject>

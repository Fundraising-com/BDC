<%@ Control Language="c#" AutoEventWireup="false" Codebehind="VendorSiteMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.VendorSiteMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<asp:label id="lblMessage" runat="server"></asp:label>
<cc2:datagridobject id="dtgMain" runat="server" allowpaging="True" searchmode="0" width="250px" cellpadding="3"
	backcolor="White" allowpagging="true" bordercolor="#CCCCCC" borderstyle="None" borderwidth="1px"
	autogeneratecolumns="False" PageSize="25">
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" CssClass="CSPager"
		Mode="NumericPages"></PagerStyle>
	<FooterStyle ForeColor="#000066" CssClass="CSSearchResult" BackColor="White"></FooterStyle>
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
	<ItemStyle ForeColor="#000066" CssClass="CSSearchResult"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CSSearchResult" BackColor="#006699"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="Vendor Site Name">
			<ItemTemplate>
				<asp:Label id="lblVendorSiteName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VendorSiteName") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</cc2:datagridobject>

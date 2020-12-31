<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ResultOrder.ascx.cs" Inherits="QSPFulfillment.CustomerService.ResultOrder" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<asp:Label id="lblMessage" runat=server></asp:Label>
<DBWC:HIERARGRID id="dtgMain" runat="server" TemplateCachingBase="Tablename"  cssClass="CSSearchResult" LoadControlMode="UserControl"
										TemplateDataMode="Table" RowExpanded="DBauer.Web.UI.WebControls.RowStates" AutoGenerateColumns="False"
										AllowPaging="True"
										BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
		BackColor="White" CellPadding="3" GridLines="Vertical">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="#DCDCDC"></AlternatingItemStyle>
		<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
		<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"  cssClass="CSSearchResult"></HeaderStyle>
		<FooterStyle ForeColor="Black"  cssClass="CSSearchResult" BackColor="#CCCCCC"></FooterStyle>
		<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages" cssClass="CSPager"></PagerStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="Order ID">
			<ItemTemplate>
				<asp:Label id="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderID") %>' EnableViewState="False">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="Date" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Order Date"></asp:BoundColumn>
		<asp:BoundColumn DataField="status" HeaderText="Status"></asp:BoundColumn>
		<asp:BoundColumn DataField="OrderType" HeaderText="Order Type"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Qualifier Name" DataField="QualifierName"></asp:BoundColumn>
		<asp:BoundColumn DataField="CampaignID" HeaderText="Campaign ID"></asp:BoundColumn>		
		<asp:BoundColumn DataField="ShipToGroupName" HeaderText="Ship Account Name"></asp:BoundColumn>
		<asp:BoundColumn DataField="ShipToGroupCity" HeaderText="Ship City"></asp:BoundColumn>
		<asp:BoundColumn DataField="ShipToGroupProvince" HeaderText="Ship Province"></asp:BoundColumn>
		<asp:BoundColumn DataField="ShipToGroupPostalCode" HeaderText="Postal Code"></asp:BoundColumn>
		<asp:BoundColumn DataField="BillToGroupCity" HeaderText="Bill City"></asp:BoundColumn>
		<asp:BoundColumn DataField="BillToGroupProvince" HeaderText="Bill Province"></asp:BoundColumn>
		<asp:BoundColumn DataField="BillToGroupPostalCode" HeaderText="Postal Code"></asp:BoundColumn>
		
	</Columns>
	<PagerStyle Mode="NumericPages"></PagerStyle>
</DBWC:HIERARGRID>

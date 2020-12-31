<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ResultSubscriptionGift.ascx.cs" Inherits="QSPFulfillment.CustomerService.ResultSubscriptionGift" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<DBWC:HIERARGRID id="dtgMains" runat="server" TemplateCachingBase="Tablename" LoadControlMode="UserControl"
										TemplateDataMode="Table" RowExpanded="DBauer.Web.UI.WebControls.RowStates" AutoGenerateColumns="False"
										AllowPaging="True" PageSize=2>
	<Columns>
		<asp:TemplateColumn HeaderText="Order ID">
			<ItemTemplate>
				<asp:Label id="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.customerorderheaderinstance") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn HeaderText="TransID" DataField="TransID"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="First Name"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Last Name"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Original/Additional"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="City"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Postal Code"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Product Code"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Title"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Issue Sent"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Catalog Price"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Override Code"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Subscription ID"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Order Item ID"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Order Item Status"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Remit ID"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Remit Date"></asp:BoundColumn>
		<asp:ButtonColumn Text="Select" CommandName="Select"></asp:ButtonColumn>
	</Columns>
	<PagerStyle Mode="NumericPages"></PagerStyle>
</DBWC:HIERARGRID>


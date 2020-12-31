<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ResultSubscription.ascx.cs" Inherits="QSPFulfillment.CustomerService.ResultSubscription" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<cc2:customerservicedatagridobjectselect id="dtgMain" IndexColumnName="lblIndex" CellPadding="3" BackColor="White" BorderWidth="1px"
	BorderStyle="None" BorderColor="#CCCCCC" AllowPaging="True" AutoGenerateColumns="False" runat="server" SearchMode="0">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	<ItemStyle ForeColor="#000066" CssClass="CSSearchResult"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CSSearchResult" BackColor="#006699"></HeaderStyle>
	<FooterStyle ForeColor="#000066" CssClass="CSSearchResult" BackColor="White"></FooterStyle>
	<Columns>
		<asp:TemplateColumn Visible="False" HeaderText="Index">
			<ItemTemplate>
				<asp:Label id="lblIndex" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerOrderHeaderInstance").ToString().PadLeft(10, Convert.ToChar("0")) + DataBinder.Eval(Container, "DataItem.TransID").ToString().PadLeft(4, Convert.ToChar("0")) %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Order ID">
			<ItemTemplate>
				<asp:Label id="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderID") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="COH Instance">
			<ItemTemplate>
				<asp:Label id="lblCustomerOrderHeaderInstance" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.customerorderheaderinstance") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Trans ID">
			<ItemTemplate>
				<asp:Label id="lblTransID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TransID") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Recipient Last Name">
			<ItemTemplate>
				<asp:Label id="lblRecipientLastName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecipientLastName") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Recipient First Name">
			<ItemTemplate>
				<asp:Label ID="lblRecipientFirstName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecipientFirstName") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Qualifier name">
			<ItemTemplate>
				<asp:Label id="lblQualifierName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QualifierName") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="CustomerCity" HeaderText="City"></asp:BoundColumn>
		<asp:BoundColumn DataField="CustomerZip" HeaderText="Postal Code"></asp:BoundColumn>
		<asp:BoundColumn DataField="TitleCode" HeaderText="Title Code"></asp:BoundColumn>
		<asp:BoundColumn DataField="Title" HeaderText="Product Name"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Student Last Name">
			<ItemTemplate>
				<asp:Label ID="lblStudentLastName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StudentLastName") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Student First Name">
			<ItemTemplate>
				<asp:Label ID="lblStudentFirstName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StudentFirstName") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Subscription Date">
			<ItemTemplate>
				<asp:label id="lblSubscriptionDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.SubscriptionDate")).ToString("MM/dd/yy") %>'></asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:BoundColumn Visible="False" DataField="CustomerState" HeaderText="State"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="IssuesSent" HeaderText="Issue Sent"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="CatalogPrice" HeaderText="Catalog Price"></asp:BoundColumn>
		<asp:TemplateColumn Visible="False" HeaderText="AccountID">
			<ItemTemplate>
				<asp:Label id="lblAccountID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AccountID")%>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Override Code">
			<ItemTemplate>
				<asp:Label id="lblOverrideCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OverrideProduct")=="true"?"1":"0" %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="OrderStatus">
			<ItemTemplate>
				<asp:Label id="lblOrderStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderStatus")%>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False">
			<ItemTemplate>
				<asp:Label id="lblCustomerInstance" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerInstance")%>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn Visible="False" DataField="Status" HeaderText="Status"></asp:BoundColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Remit ID">
			<ItemTemplate>
				<asp:Label ID="lblRemitBatchID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RemitBatchID")%>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn Visible="False" DataField="RemitBatchDate" HeaderText="Remit Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
		<asp:TemplateColumn Visible="False">
			<ItemTemplate>
				<asp:Label id="lblCampaignID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CampaignID") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False">
			<ItemTemplate>
				<asp:Label ID="lblProductType" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductType") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:ButtonColumn Text="Select" CommandName="Select"></asp:ButtonColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" CssClass="CSPager"
		Mode="NumericPages"></PagerStyle>
</cc2:customerservicedatagridobjectselect><asp:label id="lblMessage" runat="server" visible="false"></asp:label>

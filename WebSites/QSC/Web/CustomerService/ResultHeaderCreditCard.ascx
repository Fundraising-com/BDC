<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ResultHeaderCreditCard.ascx.cs" Inherits="QSPFulfillment.CustomerService.ResultHeaderCreditCard" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<asp:Label id="lblMessage" runat=server></asp:Label>
<DBWC:HIERARGRID id="dtgMain" runat="server" TemplateCachingBase="Tablename"  cssClass="CSSearchResult" LoadControlMode="UserControl"
										TemplateDataMode="Table" RowExpanded="DBauer.Web.UI.WebControls.RowStates" AutoGenerateColumns="False"
										AllowPaging="True"
										BorderColor="#999999" BorderStyle="None" BorderWidth="1px"  width="100%" 
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
				<asp:Label id="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderID") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="ExpirationDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Expiration Date"></asp:BoundColumn>
			<asp:TemplateColumn HeaderText="COH Instance">
			<ItemTemplate>
				<asp:Label id="lblCustomerOrderHeaderInstance" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.customerorderheaderinstance") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="CardholderName" HeaderText="Cardholder Name"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Credit Card Number">
			<ItemTemplate>
				<asp:Label id="lblCreditCardNumber" runat="server" Text='<%#cardExp.Replace( (DataBinder.Eval(Container, "DataItem.CreditCardNumber")).ToString() , safeOutputExp ) %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="AuthorizationCode" HeaderText="Authorization Code"></asp:BoundColumn>
		<asp:BoundColumn DataField="ReturnCode" HeaderText="ReturnCode" Visible="False"></asp:BoundColumn>
		<asp:templatecolumn headertext="Order Date">
			<itemtemplate>
				<asp:label id="lblOrderDate" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.OrderDate")).ToString("MM/dd/yy") %>'></asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:ButtonColumn Text="Select" CommandName="Select"></asp:ButtonColumn>
		
	</Columns>
	<PagerStyle Mode="NumericPages"></PagerStyle>
</DBWC:HIERARGRID>


<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CustomerOrderDetailRemitHistory.ascx.cs" Inherits="QSPFulfillment.OrderMgt.CustomerOrderDetailRemitHistory" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DataGrid id="DG1" Width="100%" runat="server" 
	AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
		BackColor="White" CellPadding="3">

	<ItemStyle ForeColor="#000066" cssClass="CSSearchResult"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699" cssClass="CSSearchResult"></HeaderStyle>
	<FooterStyle ForeColor="#000066" BackColor="White"  cssClass="CSSearchResult"></FooterStyle>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White"  cssClass="CSPager" Mode="NumericPages"></PagerStyle>
	<Columns>
		<asp:BoundColumn DataField="firstname" HeaderText="First Name"></asp:BoundColumn>
		<asp:BoundColumn DataField="Lastname" HeaderText="Last Name"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="CustomerOrderHeaderInstance" ReadOnly="True" HeaderText="CustomerOrderHeaderInstance"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="TransID" ReadOnly="True" HeaderText="TransID"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="CustomerRemitHistoryInstance" HeaderText="CustomerRemitHistoryInstance"></asp:BoundColumn>
		<asp:BoundColumn DataField="CountryCode" HeaderText="CountryCode"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="Status" HeaderText="Status"></asp:BoundColumn>
		<asp:BoundColumn DataField="Quantity" HeaderText="Quantity"></asp:BoundColumn>
		<asp:BoundColumn DataField="RemitRate" HeaderText="RemitRate" DataFormatString="{0:c}"></asp:BoundColumn>
		<asp:BoundColumn DataField="BasePrice" ReadOnly="True" HeaderText="BasePrice" DataFormatString="{0:c}">
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
		</asp:BoundColumn>
		<asp:BoundColumn DataField="CurrencyID" ReadOnly="True" HeaderText="CurrencyID"></asp:BoundColumn>
		<asp:BoundColumn DataField="Lang" ReadOnly="True" HeaderText="Lang"></asp:BoundColumn>
		<asp:BoundColumn DataField="PremiumIndicator" HeaderText="PremiumIndicator"></asp:BoundColumn>
		<asp:BoundColumn DataField="PremiumIndicator" ReadOnly="True" HeaderText="PremiumIndicator"></asp:BoundColumn>
		<asp:BoundColumn DataField="PremiumCode" ReadOnly="True" HeaderText="PremiumCode"></asp:BoundColumn>
		<asp:BoundColumn DataField="PremiumDescription" ReadOnly="True" HeaderText="PremiumDescription"></asp:BoundColumn>
		<asp:BoundColumn DataField="ABCCode" HeaderText="ABCCode"></asp:BoundColumn>
		<asp:BoundColumn DataField="Renewal" HeaderText="Renewal"></asp:BoundColumn>
		<asp:BoundColumn DataField="TitleCode" ReadOnly="True" HeaderText="TitleCode"></asp:BoundColumn>
		<asp:BoundColumn DataField="CatalogPrice" HeaderText="CatalogPrice" DataFormatString="{0:c}">
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
		</asp:BoundColumn>
		<asp:BoundColumn DataField="ItemPriceTotal" ReadOnly="True" HeaderText="ItemPriceTotal" DataFormatString="{0:c}">
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
		</asp:BoundColumn>
		<asp:BoundColumn DataField="NumberOfIssues" ReadOnly="True" HeaderText="NumberOfIssues">
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
		</asp:BoundColumn>
		<asp:BoundColumn DataField="DefaultGrossValue" ReadOnly="True" HeaderText="DefaultGrossValue" DataFormatString="{0:c}"></asp:BoundColumn>
		<asp:BoundColumn DataField="Comment" HeaderText="Comment"></asp:BoundColumn>
		<asp:BoundColumn DataField="SwitchLetterBatchID" ReadOnly="True" HeaderText="SwitchLetterBatchID"></asp:BoundColumn>
		<asp:BoundColumn DataField="GiftOrderType" HeaderText="GiftOrderType"></asp:BoundColumn>
		<asp:BoundColumn DataField="GiftOrderStatus" ReadOnly="True" HeaderText="GiftOrderStatus"></asp:BoundColumn>
		<asp:BoundColumn DataField="GiftCardDateGenerated" ReadOnly="True" HeaderText="GiftCardDateGenerated"></asp:BoundColumn>
		<asp:BoundColumn DataField="SupporterName" ReadOnly="True" HeaderText="SupporterName"></asp:BoundColumn>
		<asp:BoundColumn DataField="codedetail" HeaderText="Code Detail"></asp:BoundColumn>
	</Columns>
</asp:DataGrid>

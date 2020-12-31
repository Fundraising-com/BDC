<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RemitBatchCard.ascx.cs" Inherits="QSPFulfillment.OrderMgt.RemitBatchCard" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<P>
	<DBWC:HierarGrid id="DG1" style="Z-INDEX: 101" TemplateDataMode="Table" TemplateCachingBase="Tablename"
		 runat="server" Width="100%" AutoGenerateColumns="False"
BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
		BackColor="White" CellPadding="3">

	<ItemStyle ForeColor="#000066" cssClass="CSSearchResult"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699" cssClass="CSSearchResult"></HeaderStyle>
	<FooterStyle ForeColor="#000066" BackColor="White"  cssClass="CSSearchResult"></FooterStyle>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White"  cssClass="CSPager" Mode="NumericPages"></PagerStyle>
		<Columns>
			<asp:BoundColumn DataField="CountryCode" ReadOnly="True" HeaderText="Country Code"></asp:BoundColumn>
			<asp:BoundColumn DataField="Status" ReadOnly="True" HeaderText="Status"></asp:BoundColumn>
			<asp:BoundColumn DataField="FileName" HeaderText="File Name"></asp:BoundColumn>
			<asp:BoundColumn DataField="FulFillmenthouseNbr" ReadOnly="True" HeaderText="Ful Fillment house Nbr">
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="Ful_Name" HeaderText="Ful_Name"></asp:BoundColumn>
			<asp:BoundColumn DataField="TotalBasePrice" ReadOnly="True" HeaderText="Total Base Price">
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="TotalUnits" ReadOnly="True" HeaderText="Total Units">
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="TotalCHADD" HeaderText="TotalCHADD">
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="TotalCancelled" HeaderText="Total Cancelled">
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="Status" ReadOnly="True" HeaderText="Status"></asp:BoundColumn>
			<asp:BoundColumn DataField="TotalCatalogPrice" ReadOnly="True" HeaderText="Total Catalog Price">
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="TotalItemPrice" ReadOnly="True" HeaderText="Total Item Price">
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
		</Columns>
	</DBWC:HierarGrid></P>
<P><BR>
	&nbsp;</P>
<P><BR>
	&nbsp;</P>

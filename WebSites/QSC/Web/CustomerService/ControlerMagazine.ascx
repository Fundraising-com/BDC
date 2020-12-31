<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerMagazine.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerMagazine" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<br><asp:Label runat=server id="lblMessage"></asp:Label><br>
<cc2:DataGridObject id="dtgMain" AutoGenerateColumns="False" runat="server" width="100%" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
		BackColor="White" CellPadding="3"  SearchMode="0"
	AllowPaging="True">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
	<ItemStyle ForeColor="#000066" cssClass="CSSearchResult"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699" cssClass="CSSearchResult"></HeaderStyle>
	<FooterStyle ForeColor="#000066" BackColor="White"  cssClass="CSSearchResult"></FooterStyle>
	<Columns>
		<asp:BoundColumn DataField="Product_code" HeaderText="Title Code"></asp:BoundColumn>
		<asp:BoundColumn DataField="Product_sort_name" HeaderText="Title"></asp:BoundColumn>
		<asp:TemplateColumn>
			<ItemTemplate>
				<a href='javascript:void(0);' runat="server" id="hypSelect">Select</a>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White"  cssClass="CSPager" Mode="NumericPages"></PagerStyle>
</cc2:DataGridObject>

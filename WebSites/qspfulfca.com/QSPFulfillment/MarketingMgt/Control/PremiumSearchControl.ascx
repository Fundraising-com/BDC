<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PremiumSearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.PremiumSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:label id="lblMessage" runat="server"></asp:label><cc2:datagridobject id="dtgMain" runat="server" AutoGenerateColumns="False" BorderWidth="1px" BorderStyle="None"
	BorderColor="#CCCCCC" AllowPagging="true" BackColor="White" CellPadding="3" width="100%" SearchMode="0" AllowPaging="True">
	<SELECTEDITEMSTYLE BackColor="#008A8C" ForeColor="White" Font-Bold="True"></SELECTEDITEMSTYLE>
	<ITEMSTYLE CssClass="CSSearchResult" ForeColor="#000066"></ITEMSTYLE>
	<HEADERSTYLE CssClass="CSSearchResult" BackColor="#006699" ForeColor="White" Font-Bold="True"></HEADERSTYLE>
	<FOOTERSTYLE CssClass="CSSearchResult" BackColor="White" ForeColor="#000066"></FOOTERSTYLE>
	<COLUMNS>
		<asp:TemplateColumn Visible="false">
			<ItemTemplate>
				<asp:Label id="lblPremiumID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Premium Code">
			<ItemTemplate>
				<asp:label id="lblPremiumCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PremiumCode") %>'></asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<ASP:TEMPLATECOLUMN HeaderText="English Description">
			<ITEMTEMPLATE>
				<asp:Label id=lblEnglishDescription runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EnglishDescription") %>'>
				</asp:Label>
			</ITEMTEMPLATE>
		</ASP:TEMPLATECOLUMN>
		<ASP:TEMPLATECOLUMN Visible="False">
			<ITEMTEMPLATE>
				<asp:Label id=lblFrenchDescription runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FrenchDescription") %>'>
				</asp:Label>
			</ITEMTEMPLATE>
		</ASP:TEMPLATECOLUMN>
		<asp:templatecolumn headertext="Year">
			<itemtemplate>
				<asp:label id="lblYear" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Year") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Season">
			<itemtemplate>
				<asp:label id="lblSeason" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Season") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<ASP:TEMPLATECOLUMN HeaderText="IsValid">
			<ITEMTEMPLATE>
				<asp:Label id="lblIsValid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Valid") %>'>
				</asp:Label>
			</ITEMTEMPLATE>
		</ASP:TEMPLATECOLUMN>
		<ASP:TEMPLATECOLUMN Visible="true" HeaderText="">
			<ITEMTEMPLATE>
				<asp:LinkButton id="LinkButton1" Runat="server" CommandName="Edit" CausesValidation="True">Edit</asp:LinkButton>
			</ITEMTEMPLATE>
		</ASP:TEMPLATECOLUMN>
	</COLUMNS>
	<PAGERSTYLE CssClass="CSPager" BackColor="White" ForeColor="#000066" Mode="NumericPages" HorizontalAlign="Left"></PAGERSTYLE>
</cc2:datagridobject>

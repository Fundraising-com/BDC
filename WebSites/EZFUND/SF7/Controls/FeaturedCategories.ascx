<%@ Control Language="vb" AutoEventWireup="false" Codebehind="FeaturedCategories.ascx.vb" Inherits="StoreFront.StoreFront.FeaturedCategories" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<h2 class="ContentTableHeader">Featured Categories</h2>
<div class="error">
	<asp:Label ID="lblFCatError" Runat="server"></asp:Label>
</div>
<asp:datalist id="dlFeaturedCats" RepeatDirection="Horizontal" ItemStyle-VerticalAlign="Top" Width="100%" Runat="server" RepeatColumns="3" Visible="True" CssClass="Content">
<ItemStyle Wrap="True"></ItemStyle>
<ItemTemplate>
<ul>
	<li class="image">
		<asp:hyperlink id="hplImage" Runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"Image") %>' NavigateUrl='<%# "../SearchResult.aspx?CategoryID=" & DataBinder.Eval(Container.DataItem,"id") %>'></asp:hyperlink>
	</li>
	<li class="name">
		<asp:hyperlink id="Hyperlink1" Runat="server" NavigateUrl='<%# "../SearchResult.aspx?CategoryID=" & DataBinder.Eval(Container.DataItem,"id") %>'><%# DataBinder.Eval(Container.DataItem,"Name") %></asp:hyperlink>
	</li>
	<li class="description">
		<asp:Label Runat="server" ID="lblDesc">
			Description:&nbsp;<%# DataBinder.Eval(Container.DataItem,"Description") %>
		</asp:Label>
	</li>
</ul>
</ItemTemplate>
</asp:datalist>
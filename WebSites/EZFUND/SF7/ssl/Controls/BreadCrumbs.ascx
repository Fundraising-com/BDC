<%@ Control Language="vb" AutoEventWireup="false" Inherits="System.Web.UI.UserControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSBreadCrumbs" %>
<%' © 2003 Structured Solutions %>
<sfaddons:BreadCrumbs id="BreadCrumbs1" runat="server" ProductCategories="Shortest" ShowProduct="False">
	<ItemTemplate>
		<asp:datalist id="BreadCrumbList" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server">
			<ItemTemplate>
				<asp:HyperLink id="CatalogLink" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "url") %>'>
					<%# DataBinder.Eval(Container.DataItem, "name") %>
				</asp:HyperLink>
			</ItemTemplate>
			<SeparatorTemplate>
				&gt;
			</SeparatorTemplate>
		</asp:datalist>
	</ItemTemplate>
	<SeparatorTemplate><br></SeparatorTemplate>
</sfaddons:BreadCrumbs>

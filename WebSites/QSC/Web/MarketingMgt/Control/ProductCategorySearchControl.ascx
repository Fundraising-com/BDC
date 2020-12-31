<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProductCategorySearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.ProductCategorySearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<div id="divSearch" runat="server">
	<asp:label id="lblMessage" runat="server"></asp:label>
	<cc2:datagridobject id="dtgMain" runat="server" allowpaging="True" searchmode="0" width="100%" cellpadding="3"
		backcolor="White" allowpagging="true" bordercolor="#CCCCCC" borderstyle="None" borderwidth="1px" autogeneratecolumns="False">
		<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
		<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
		<headerstyle font-bold="True" forecolor="White" cssclass="CSSearchResult" backcolor="#006699"></headerstyle>
		<footerstyle forecolor="#000066" cssclass="CSSearchResult" backcolor="White"></footerstyle>
		<columns>
			<asp:templatecolumn visible="False">
				<itemtemplate>
					<asp:Label id="lblInstance" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Instance") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Category">
				<itemtemplate>
					<asp:Label id=lblDescription runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="true" headertext="">
				<itemtemplate>
					<asp:linkbutton id="LinkButton1" runat="server" commandname="Select" causesvalidation="True">Edit</asp:linkbutton>
				</itemtemplate>
			</asp:templatecolumn>
		</columns>
		<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
			mode="NumericPages"></pagerstyle>
	</cc2:datagridobject>
</div>
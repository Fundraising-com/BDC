<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FieldSuppliesOrderListControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.FieldSuppliesOrderListControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div style="PADDING-RIGHT: 1px;PADDING-LEFT: 1px;PADDING-BOTTOM: 1px;PADDING-TOP: 1px">
	<asp:datagrid id="dtgMain" runat="server" autogeneratecolumns="False" borderwidth="1px" borderstyle="None"
		bordercolor="#CCCCCC" backcolor="White" cellpadding="3" width="100%" searchmode="0" allowpaging="True"
		showfooter="False">
		<footerstyle forecolor="#000066" cssclass="CSSearchResult" backcolor="White"></footerstyle>
		<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
		<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
		<headerstyle font-bold="True" forecolor="White" cssclass="CSSearchResult" backcolor="#006699"></headerstyle>
		<columns>
			<asp:templatecolumn headertext="Order ID">
				<itemtemplate>
					<asp:label id="lblOrderID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.OrderID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Catalog Section">
				<itemtemplate>
					<asp:label id="lblCatalogSection" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ProgramSectionName") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Product Code">
				<itemtemplate>
					<asp:label id="lblProductCode" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Product_Code") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Product Description">
				<itemtemplate>
					<asp:label id="lblProductSortName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Product_Sort_Name") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Quantity">
				<itemtemplate>
					<asp:label id="lblQuantity" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Status">
				<itemtemplate>
					<asp:label id="lblStatus" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
		</columns>
		<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
			mode="NumericPages"></pagerstyle>
	</asp:datagrid>
</div>
<br>
<div style="text-align: center;">
	<input type="button" class="boxlook" value="Close" onclick="self.close(); window.parent.focus();" />
</div>
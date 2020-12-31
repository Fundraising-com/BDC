<%@ Register TagPrefix="uc1" TagName="ProductCodePickerControl" Src="ProductCodePickerControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PublisherContactProductListControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.PublisherContactProductListControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<br>
<asp:label id="lblMessage" runat="server"></asp:label><cc2:datagridobject id="dtgMain" runat="server" autogeneratecolumns="False" borderwidth="1px" borderstyle="None"
	bordercolor="#CCCCCC" allowpagging="true" backcolor="White" cellpadding="3" width="100%" searchmode="0" allowpaging="True" pagesize="15">
	<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
		mode="NumericPages"></pagerstyle>
	<footerstyle forecolor="#000066" cssclass="CSSearchResult" backcolor="White"></footerstyle>
	<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
	<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
	<headerstyle font-bold="True" forecolor="White" cssclass="CSSearchResult" backcolor="#006699"></headerstyle>
	<columns>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Product Code">
			<itemtemplate>
				<asp:Label id=lblProductCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Code") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Name">
			<itemtemplate>
				<asp:Label id=lblTitle runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Sort_Name") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Language">
			<itemtemplate>
				<asp:Label id="lblLanguage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Lang") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Status">
			<itemtemplate>
				<asp:label id="lblStatus" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:linkbutton id="Linkbutton1" runat="server" commandname="Delete">Delete</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
</cc2:datagridobject>
<br>
<uc1:productcodepickercontrol id="ctrlProductCodePickerControl" runat="server" required="false"></uc1:productcodepickercontrol>
&nbsp;&nbsp;&nbsp;
<asp:button id="btnAddProduct" runat="server" text="Add Title" cssclass="boxlook"></asp:button>
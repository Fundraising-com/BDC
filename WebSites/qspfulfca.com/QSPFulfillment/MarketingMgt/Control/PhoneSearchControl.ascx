<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PhoneSearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.PhoneSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:label id="lblMessage" runat="server"></asp:label>
<cc2:datagridobject id="dtgMain" runat="server" allowpaging="True" searchmode="0" width="100%" cellpadding="3"
	backcolor="White" allowpagging="true" bordercolor="#CCCCCC" borderstyle="None" borderwidth="1px"
	autogeneratecolumns="False">
	<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
	<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
	<headerstyle font-bold="True" forecolor="White" cssclass="CSSearchResult" backcolor="#006699"></headerstyle>
	<footerstyle forecolor="#000066" cssclass="CSSearchResult" backcolor="White"></footerstyle>
	<columns>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:Label id="lblPhoneListID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PhoneListID") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn HeaderText="Phone Number">
			<itemtemplate>
				<asp:Label id="lblPhoneNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PhoneNumber") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Type">
			<itemtemplate>
				<asp:Label id="lblType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Best Time To Call">
			<itemtemplate>
				<asp:Label id="lblBestTimeToCall" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BestTimeToCall") %>'>
				</asp:Label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="true" headertext="">
			<itemtemplate>
				<asp:linkbutton id="Linkbutton1" runat="server" commandname="Delete" causesvalidation="True">Delete</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="true" headertext="">
			<itemtemplate>
				<asp:linkbutton id="LinkButton2" runat="server" commandname="Select" causesvalidation="True">Edit</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
	<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
		mode="NumericPages"></pagerstyle>
</cc2:datagridobject>

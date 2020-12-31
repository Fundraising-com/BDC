<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ContactListControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.ContactListControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<asp:datagrid id="dtgMain" runat="server" autogeneratecolumns="False" borderwidth="1px" borderstyle="None"
	bordercolor="#CCCCCC" backcolor="White" cellpadding="3" width="100%" searchmode="0" allowpaging="True"
	showfooter="True">
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
		<asp:templatecolumn headertext="Title" visible="False">
			<itemtemplate>
				<ASP:LABEL id="lblTitle" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Title") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="First Name">
			<itemtemplate>
				<ASP:LABEL id="lblFirstName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FirstName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Middle Initial" visible="False">
			<itemtemplate>
				<ASP:LABEL id="lblMiddleInitial" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MiddleInitial") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Last Name">
			<itemtemplate>
				<asp:label id="lblLastName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LastName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Function">
			<itemtemplate>
				<ASP:LABEL id="lblFunction" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Function") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Email">
			<itemtemplate>
				<ASP:LABEL id="lblEmail" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Email") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Is Primary">
			<itemtemplate>
				<asp:label id="lblIsPrimary" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TypeId").ToString() == "10" ? "Y" : "N" %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Copy To">
			<itemtemplate>
				<asp:linkbutton id="lnkShipTo" runat="server" commandname="CopyContact" commandargument="ShipTo"
					causesvalidation="False">Ship To</asp:linkbutton>
				</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Copy To">
			<itemtemplate>
				<asp:linkbutton id="lnkBillTo" runat="server" commandname="CopyContact" commandargument="BillTo"
					causesvalidation="False">Bill To</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:linkbutton id="lnkDelete" runat="server" commandname="Delete" causesvalidation="False">Delete</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:linkbutton id="lnkSelect" runat="server" commandname="Select">Select</asp:linkbutton>
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
	<pagerstyle mode="NumericPages"></pagerstyle>
</asp:datagrid>

<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CatalogSectionSearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.CatalogSectionSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="../../CustomerService/ControlerConfirmationPage.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<div id="divSearch" runat="server"><asp:label id="lblMessage" runat="server"></asp:label><cc2:datagridobject id="dtgMain" runat="server" autogeneratecolumns="False" borderwidth="1px" borderstyle="None"
		bordercolor="#CCCCCC" allowpagging="true" backcolor="White" cellpadding="3" width="100%" searchmode="0" allowpaging="True">
		<selecteditemstyle backcolor="#008A8C" forecolor="White" font-bold="True"></selecteditemstyle>
		<itemstyle cssclass="CSSearchResult" forecolor="#000066"></itemstyle>
		<headerstyle cssclass="CSSearchResult" backcolor="#006699" forecolor="White" font-bold="True"></headerstyle>
		<footerstyle cssclass="CSSearchResult" backcolor="White" forecolor="#000066"></footerstyle>
		<columns>
			<asp:templatecolumn visible="False">
				<itemtemplate>
					<asp:Label id="lblSectionID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Section Name">
				<itemtemplate>
					<asp:Label id=lblSectionName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="false">
				<itemtemplate>
					<asp:label id="lblSectionTypeID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CatalogSectionTypeID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Type">
				<itemtemplate>
					<asp:Label id=lblSectionType runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="False">
				<itemtemplate>
					<asp:label id="lblFSProgramID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FSProgramID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Program">
				<itemtemplate>
					<asp:label id="lblFSProgramName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FSProgramName") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="true" headertext="">
				<itemtemplate>
					<asp:linkbutton id="Linkbutton3" runat="server" commandname="Delete" causesvalidation="True">Delete</asp:linkbutton>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="true" headertext="">
				<itemtemplate>
					<asp:linkbutton id="LinkButton1" runat="server" commandname="Edit" causesvalidation="True">Edit</asp:linkbutton>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="true" headertext="">
				<itemtemplate>
					<asp:linkbutton id="Linkbutton2" runat="server" commandname="IncludeProducts" causesvalidation="True">Include Products</asp:linkbutton>
				</itemtemplate>
			</asp:templatecolumn>
		</columns>
		<pagerstyle cssclass="CSPager" backcolor="White" forecolor="#000066" mode="NumericPages" horizontalalign="Left"></pagerstyle>
	</cc2:datagridobject></div>
<uc1:controlerconfirmationpage id="ctrlControlerConfirmationPageDelete" runat="server"></uc1:controlerconfirmationpage>

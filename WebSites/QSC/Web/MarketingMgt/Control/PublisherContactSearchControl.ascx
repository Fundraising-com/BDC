<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="../../CustomerService/ControlerConfirmationPage.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PublisherContactSearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.PublisherContactSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
				<asp:Label id="lblPublisherContactInstance" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PContact_Instance") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:Label id="lblPublisherNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Pub_Nbr") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False" headertext="Publisher Name">
			<itemtemplate>
				<asp:Label id="lblPublisherName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Pub_Name") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Contact Name">
			<itemtemplate>
				<asp:Label id="lblContactName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PContact_FName") + " " + DataBinder.Eval(Container, "DataItem.PContact_LName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="false">
			<itemtemplate>
				<asp:Label id="lblContactFirstName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PContact_FName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="false">
			<itemtemplate>
				<asp:Label id="lblContactLastName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PContact_LName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Position Title">
			<itemtemplate>
				<asp:label id="lblPositionTitle" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PContact_Title") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Email">
			<itemtemplate>
				<asp:Label id="lblEmail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PContact_Email") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="false">
			<itemtemplate>
				<asp:Label id="lblPhoneListID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PhoneListID") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Phone">
			<itemtemplate>
				<asp:Label id="lblPhoneNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PhoneNumber") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Fax">
			<itemtemplate>
				<asp:Label id="lblFaxNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FaxNumber") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Is Main Contact">
			<itemtemplate>
				<asp:label id="lblIsMainContact" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IsMainContact") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:linkbutton id="LinkButton2" runat="server" commandname="Delete" causesvalidation="False">Delete</asp:linkbutton>
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
<uc1:controlerconfirmationpage id="ctrlControlerConfirmationPageDelete" runat="server"></uc1:controlerconfirmationpage>

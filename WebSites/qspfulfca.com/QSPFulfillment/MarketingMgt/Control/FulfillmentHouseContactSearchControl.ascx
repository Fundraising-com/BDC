<%@ Control Language="c#" AutoEventWireup="false" Codebehind="FulfillmentHouseContactSearchControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.FulfillmentHouseContactSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="../../CustomerService/ControlerConfirmationPage.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
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
				<asp:label id="lblInstance" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Instance") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblFulfillmentHouseID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Ful_Nbr") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False" headertext="Fulfillment House Name">
			<itemtemplate>
				<asp:label id="lblFulfillmentHouseName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Ful_Name") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Contact Name">
			<itemtemplate>
				<asp:label id="lblContactName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FirstName") + " " + DataBinder.Eval(Container, "DataItem.LastName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="false">
			<itemtemplate>
				<asp:label id="lblContactFirstName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FirstName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="false">
			<itemtemplate>
				<asp:label id="lblContactLastName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LastName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Position Title">
			<itemtemplate>
				<asp:label id="lblPositionTitle" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Title") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Email">
			<itemtemplate>
				<asp:label id="lblEmail" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Email") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Work Phone">
			<itemtemplate>
				<asp:label id="lblWorkPhone" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.WorkPhone") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Fax">
			<itemtemplate>
				<asp:label id="lblFax" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Fax") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblCustomerServiceContactFirstName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CustSvcContactQSPFirstName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblCustomerServiceContactLastName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CustSvcContactQSPLastName") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblCustomerServiceContactEmail" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CustSvcContactQSPEmail") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn visible="False">
			<itemtemplate>
				<asp:label id="lblCustomerServiceContactPhone" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CustSvcContactQSPPhone") %>'>
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

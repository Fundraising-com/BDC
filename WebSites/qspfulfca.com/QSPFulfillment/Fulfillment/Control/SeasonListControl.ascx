<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SeasonListControl.ascx.cs" Inherits="QSPFulfillment.Fulfillment.Control.SeasonListControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ControlerConfirmationPage" Src="../../CustomerService/ControlerConfirmationPage.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<asp:datagrid id="dtgMain" runat="server" autogeneratecolumns="False" borderwidth="1px" borderstyle="None"
	bordercolor="#CCCCCC" backcolor="White" cellpadding="3" width="100%" searchmode="0" allowpaging="True"
	showfooter="False">
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
		<asp:templatecolumn headertext="Name">
			<itemtemplate>
				<ASP:LABEL id="lblName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
				</ASP:LABEL>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Fiscal Year">
			<itemtemplate>
				<ASP:LABEL id="lblFiscalYear" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FiscalYear") %>'>
				</ASP:LABEL>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Season">
			<itemtemplate>
				<ASP:LABEL id="lblSeason" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Season") %>'>
				</ASP:LABEL>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="USD Conversion Rate" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="105px">
			<itemtemplate>
				<asp:label id="lblDefaultConversionRate" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DefaultConversionRate") %>'>
				</asp:label>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Start Date">
			<itemtemplate>
				<ASP:LABEL id="lblStartDate" runat="server" text='<%# String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.StartDate")) %>'>
				</ASP:LABEL>
			</itemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="End Date">
			<itemtemplate>
				<ASP:LABEL id="lblEndDate" runat="server" text='<%# String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.EndDate"))  %>'>
				</ASP:LABEL>
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
<uc1:controlerconfirmationpage id="ctrlControlerConfirmationPageDelete" runat="server"></uc1:controlerconfirmationpage>

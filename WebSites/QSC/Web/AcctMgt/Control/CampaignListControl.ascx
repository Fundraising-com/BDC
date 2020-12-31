<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CampaignListControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.CampaignListControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSPFulfillment.AcctMgt.Control" Assembly="QSPFulfillment" %>
<div style="PADDING-RIGHT: 1px;PADDING-LEFT: 1px;PADDING-BOTTOM: 1px;PADDING-TOP: 1px">
	<asp:datagrid id="dtgMain" runat="server" autogeneratecolumns="False" borderwidth="1px" borderstyle="None"
		bordercolor="#CCCCCC" backcolor="White" cellpadding="3" width="100%" searchmode="0" allowpaging="True"
		showfooter="False">
		<footerstyle forecolor="#000066" cssclass="CSSearchResult" backcolor="White"></footerstyle>
		<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
		<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
		<headerstyle font-bold="True" forecolor="White" cssclass="CSSearchResult" backcolor="#006699"></headerstyle>
		<columns>
			<asp:templatecolumn headertext="Campaign ID">
				<itemtemplate>
					<asp:label id="lblCampaignID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CampaignID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="FM ID" visible="False">
				<itemtemplate>
					<asp:label id="lblFMID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FMID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="FM">
				<itemtemplate>
					<asp:label id="lblFMName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FMName") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Start">
				<itemtemplate>
					<asp:label id="lblStartDate" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.StartDate")) != new DateTime(1995, 01, 01) ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.StartDate")).ToString("MM/dd/yyyy") : "" %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="End">
				<itemtemplate>
					<asp:label id="lblEndDate" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.EndDate")) != new DateTime(1995, 01, 01) ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.EndDate")).ToString("MM/dd/yyyy") : "" %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="False">
				<itemtemplate>
					<asp:label id="lblStatus" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Status">
				<itemtemplate>
					<asp:label id="lblStatusName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.StatusName").ToString().Replace("campaign status - ", "") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Staff">
				<itemtemplate>
					<asp:label id="lblIsStaffOrder" runat="server" text='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsStaffOrder")) ? "Y" : "N" %>'></asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Online Only">
				<itemtemplate>
					<asp:label id="lblOnlineOnly" runat="server" text='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsOnlineOnly")) ? "Y" : "N" %>'></asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Programs" >
				<itemtemplate>
					<asp:label id="lblPrograms" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Programs") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Main Order Fulfilled">
				<itemtemplate>
					<asp:label id="lblMainOrderFulfilled" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.MainOrderFulfilled")) != new DateTime(1995, 01, 01) ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.MainOrderFulfilled")).ToString("MM/dd/yyyy") : "" %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="FS Shipped">
				<itemtemplate>
					<asp:label id="lblSupplyFulfilled" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.SupplyFulfilled")) != new DateTime(1995, 01, 01) ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.SupplyFulfilled")).ToString("MM/dd/yyyy") : "" %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn>
				<itemtemplate>
					<cc1:ConfirmationAgreementLinkButton id="hypCA" runat="server" CampaignID='<%# DataBinder.Eval(Container, "DataItem.CampaignID") %>' CausesValidation="False">Preview / Print CA</cc1:confirmationagreementlinkbutton>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn>
				<itemtemplate>
					<cc1:CASummaryHyperlink id="hypCASummary" runat="server" CampaignID='<%# DataBinder.Eval(Container, "DataItem.CampaignID") %>'>Print Sum. Form</cc1:CASummaryHyperlink>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn>
				<itemtemplate>
					<cc1:CloneCALinkButton id="lnkCloneCA" runat="server" CommandName="Clone" CampaignId='<%# DataBinder.Eval(Container, "DataItem.CampaignID") %>' CausesValidation="False">Clone</cc1:CloneCALinkButton>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn>
				<itemtemplate>
					<a href="CampaignMaintenance.aspx?CampaignID=<%# DataBinder.Eval(Container, "DataItem.CampaignID") %>">Edit</a>
				</itemtemplate>
				<footertemplate>
				</footertemplate>
			</asp:templatecolumn>
		</columns>
		<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
			mode="NumericPages"></pagerstyle>
	</asp:datagrid>
</div>

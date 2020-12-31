<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AdjustmentListControl.ascx.cs" Inherits="QSPFulfillment.Finance.Control.AdjustmentListControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
			<asp:templatecolumn headertext="Adjustment ID">
				<itemtemplate>
					<asp:label id="lblAdjustmentID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ADJUSTMENT_ID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Group Name">
				<itemtemplate>
					<asp:label id="lblAccountName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AccountName") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Campaign ID">
				<itemtemplate>
					<asp:label id="lblCampaignID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CAMPAIGN_ID") %>'></asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="CA Start Date">
				<itemtemplate>
					<asp:label id="lblCampaignStartDate" runat="server" text='<%# ((DateTime) DataBinder.Eval(Container, "DataItem.CampaignStartDate")).ToString("MM/dd/yyyy") %>'></asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="CA End Date">
				<itemtemplate>
					<asp:label id="lblCampaignEndDate" runat="server" text='<%# ((DateTime) DataBinder.Eval(Container, "DataItem.CampaignEndDate")).ToString("MM/dd/yyyy") %>'></asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Amount">
				<itemtemplate>
					<asp:label id="lblAdjustmentAmount" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ADJUSTMENT_AMOUNT") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Effective Date">
				<itemtemplate>
					<asp:label id="lblAdjustmentEffectiveDate" runat="server" text='<%# ((DateTime) DataBinder.Eval(Container, "DataItem.ADJUSTMENT_EFFECTIVE_DATE")).ToString("MM/dd/yyyy") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
		</columns>
		<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
			mode="NumericPages"></pagerstyle>
	</asp:datagrid>
</div>

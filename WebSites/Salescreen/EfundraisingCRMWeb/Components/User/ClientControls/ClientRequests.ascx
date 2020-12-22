<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientRequests.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.ClientControls.ClientRequests" %>
<asp:Panel id="Panel1" CssClass="Frame" BorderWidth="2px" runat="server" BorderStyle="None">
	<asp:datagrid id="RequestDatagrid" runat="server" BorderWidth="2px" CssClass="NormalText" AllowPaging="True"
		Width="264px" BackColor="#F7F7F7" HorizontalAlign="Left" BorderColor="Gainsboro" AutoGenerateColumns="False"
		AllowSorting="True" PageSize="2">
		<AlternatingItemStyle CssClass="AlternateItemBackGround"></AlternatingItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Left" CssClass="AlternateItemBackGround NormalTextBold Passive"></HeaderStyle>
		<Columns>
			<asp:BoundColumn DataField="date" HeaderText="Date">
				<HeaderStyle Width="50pt"></HeaderStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="promo" HeaderText="Promo">
				<HeaderStyle Width="100pt"></HeaderStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="channel" HeaderText="Chnl">
				<HeaderStyle Width="50pt"></HeaderStyle>
			</asp:BoundColumn>
			<asp:BoundColumn Visible="False" DataField="leadID"></asp:BoundColumn>
		</Columns>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
	</asp:datagrid>
</asp:Panel>

<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PayGroupMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.PayGroupMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:label id="lblMessage" runat="server"></asp:label>
<cc2:datagridobject id="dtgMain" runat="server" allowpaging="True" searchmode="0" width="250px" cellpadding="3"
	backcolor="White" allowpagging="true" bordercolor="#CCCCCC" borderstyle="None" borderwidth="1px"
	autogeneratecolumns="False" PageSize="25">
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" CssClass="CSPager"
		Mode="NumericPages"></PagerStyle>
	<FooterStyle ForeColor="#000066" CssClass="CSSearchResult" BackColor="White"></FooterStyle>
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
	<ItemStyle ForeColor="#000066" CssClass="CSSearchResult"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CSSearchResult" BackColor="#006699"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="Pay Group LookUp Code">
			<ItemTemplate>
				<asp:Label id="lblPayGroupLookUpCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PayGroupLookUpCode") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</cc2:datagridobject>

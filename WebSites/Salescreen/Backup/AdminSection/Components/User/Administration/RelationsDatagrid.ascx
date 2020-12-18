<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RelationsDatagrid.ascx.cs" Inherits="AdminSection.Components.User.Administration.RelationsDatagrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:datagrid id="PackageProductDataGrid" CssClass="NormalText" PageSize="5" Width="632px" runat="server"
	AutoGenerateColumns="False" CellPadding="3" BorderWidth="1px" BorderStyle="None" BorderColor="Navy"
	AllowPaging="True" AllowSorting="True">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	<AlternatingItemStyle BackColor="#D4D0C8"></AlternatingItemStyle>
	<ItemStyle ForeColor="#000066"></ItemStyle>
	<HeaderStyle Font-Names="Arial" Font-Bold="True" ForeColor="DimGray" BackColor="WhiteSmoke"></HeaderStyle>
	<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="productId" HeaderText="ID">
			<HeaderStyle Width="5%"></HeaderStyle>
		</asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="packageId"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Products">
			<HeaderStyle Width="60%"></HeaderStyle>
			<ItemTemplate>
				<asp:LinkButton id=ProductLink runat="server" CausesValidation="False" Text='<%# DataBinder.Eval(Container, "DataItem.product") %>' OnClick="ProductLink_Click">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Packages">
			<HeaderStyle Width="60%"></HeaderStyle>
			<ItemTemplate>
				<asp:LinkButton id="packageLink" runat="server" CausesValidation="False" Text='<%# DataBinder.Eval(Container, "DataItem.package") %>' OnClick="PackageLink_Click">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="displayOrder" HeaderText="Display_Order">
			<HeaderStyle Width="15%"></HeaderStyle>
		</asp:BoundColumn>
		<asp:BoundColumn DataField="display" HeaderText="Display">
			<HeaderStyle Width="10%"></HeaderStyle>
		</asp:BoundColumn>
		<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="Edit">
			<HeaderStyle Width="10%"></HeaderStyle>
		</asp:EditCommandColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="#D4D0C8" PageButtonCount="15"
		Mode="NumericPages"></PagerStyle>
</asp:datagrid>
<TABLE id="Table1" style="WIDTH: 632px; HEIGHT: 50px" cellSpacing="1" cellPadding="1" width="632"
	border="0">
	<TR>
		<TD></TD>
		<TD align="right"><asp:button id="CreateNewButton" runat="server" CausesValidation="False" Text="Create New"></asp:button></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD></TD>
	</TR>
</TABLE>

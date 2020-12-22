<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaleInfoList.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.Sales.SaleInfoList" %>
<LINK href="../../Resources/Css/style.css" type="text/css" rel="stylesheet">
<LINK href="../../Resources/Css/TallySale.css" type="text/css" rel="stylesheet">
<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
	<tr vAlign="top">
		<td align="left" borderColor="gray"><asp:label id="messagelabel" CssClass="NormalTextBold" Runat="server"></asp:label>
			<asp:DataGrid id="SaleInfoDataGrid" runat="server" AllowSorting="True" AllowPaging="True" BorderColor="Gray"
				BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" CssClass="NormalText"
				DataKeyField="salesId" Width="735px" BackColor="#F7F7F7">
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<AlternatingItemStyle CssClass="AlternateItemBackGround"></AlternatingItemStyle>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<HeaderStyle Font-Names="Arial" Font-Bold="True" CssClass="AlternateItemBackGround NormalTextBold Passive"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="salesId"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Sale ID">
						<HeaderStyle Width="10%"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton ID="SaleIDLinkbutton" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.salesId") %>' OnClick="SaleIDLink_Click">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="salesDate" HeaderText="Sale Date" DataFormatString="{0:yyyy-MM-dd}">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderText="Product Class">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="totalAmount" HeaderText="Amount" DataFormatString="{0:C}">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderText="Due">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="paymentTermID" HeaderText="Payment Term">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="paymentDueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}">
						<HeaderStyle Width="10%"></HeaderStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" PageButtonCount="15" CssClass="AlternateItemBackGround"
					Mode="NumericPages"></PagerStyle>
			</asp:DataGrid></td>
	</tr>
</table>

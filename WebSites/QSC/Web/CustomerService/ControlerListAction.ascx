<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerListAction.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerListAction" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>

	<table width="100%">
		<tr>
			<td valign="top">
			<asp:Label runat=server id="lblMessage"></asp:Label>
				<cc2:DataGridObject width="100%" id="dtgMain" runat="server" AutoGenerateColumns="False" ShowFooter="True"
					SearchMode="0" CssClass="CSTableItems">
<Columns>
<asp:TemplateColumn Visible="False" HeaderText="Action ID">
<ItemStyle Wrap="False">
</ItemStyle>

<ItemTemplate>
								<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActionInstance") %>'>
								</asp:Label>
							
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Action">
<ItemStyle Wrap="False">
</ItemStyle>

<ItemTemplate>
								<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActionDescription") %>'>
								</asp:Label>
							
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn Visible="False" HeaderText="Comments">
<ItemStyle Wrap="False">
</ItemStyle>

<ItemTemplate>
								<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Comments") %>'>
								</asp:Label>
							
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Comments">
<ItemTemplate>
	<asp:HyperLink runat="server" ID="hypEdit" Text="Edit" NavigateUrl="javascript:void(0);"></asp:HyperLink>
</ItemTemplate>
</asp:TemplateColumn>
</Columns>
				</cc2:DataGridObject>
			</td>
		</tr>
	</table>


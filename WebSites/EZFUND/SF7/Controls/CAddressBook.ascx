<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="AddressLabel.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CAddressBook.ascx.vb" Inherits="StoreFront.StoreFront.CAddressBook" TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<table id="PageIt" border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td>
			<asp:datagrid id="DataGrid1" runat="server" PageSize="5" AllowPaging="True" AutoGenerateColumns="False"
				ShowHeader="False" Width="100%" BorderWidth="0px">
				<SelectedItemStyle Width="100%"></SelectedItemStyle>
				<EditItemStyle Width="100%"></EditItemStyle>
				<AlternatingItemStyle Width="100%"></AlternatingItemStyle>
				<ItemStyle Width="100%"></ItemStyle>
				<HeaderStyle Width="100%"></HeaderStyle>
				<FooterStyle Width="100%"></FooterStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="ContentTableHeader" noWrap>&nbsp;<%#  DataBinder.Eval(Container.DataItem,"NickName") %></TD>
								</TR>
								<TR>
									<TD class="Content" height="5"><IMG height="5" src="images/clear.gif" width="1"></TD>
								</TR>
								<TR>
									<TD>
										<uc1:AddressLabel id="AddressLabel1" runat="server"></uc1:AddressLabel></TD>
								</TR>
								<TR>
									<TD class="Content" height="5"><IMG height="5" src="images/clear.gif" width="1"></TD>
								</TR>
								<TR>
									<TD class="Content" noWrap align="right">
										<asp:LinkButton ID="btnDelete" Runat="server" OnClick=DeleteClick CommandArgument='<%#  DataBinder.Eval(Container.DataItem,"ID") %>'>
											<asp:Image BorderWidth="0" ID="imgDelete" Runat="server" AlternateText="Delete"></asp:Image>
										</asp:LinkButton>
										&nbsp;
										<asp:LinkButton ID="btnEdit" Runat="server" OnClick=EditClick CommandArgument='<%#  DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="Edit">
											<asp:Image BorderWidth="0" ID="imgEdit" Runat="server" AlternateText="Edit"></asp:Image>
										</asp:LinkButton>
									</TD>
								</TR>
								<TR>
									<TD class="Content" height="5"><IMG height="5" src="images/clear.gif" width="1"></TD>
								</TR>
							</TABLE>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle Width="100%" HorizontalAlign="Right" Position="Top" CssClass="Content" Wrap="False"
					Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
		</td>
	</tr>
</table>

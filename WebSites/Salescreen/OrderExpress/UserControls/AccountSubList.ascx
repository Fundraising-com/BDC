<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountSubList" Codebehind="AccountSubList.ascx.cs" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0" width="100%">
	<tr>
		<td><asp:label id="lblFormTitle" CssClass="StandardLabel" runat="server"></asp:label>
			<br>
		</td>
	</tr>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:SortedDataGrid id=dtgAccount runat="server" PageSize="30" Width="100%" AllowSorting="True" CellPadding="3" CssClass=GridStyle BorderColor="#CCCCCC" AutoGenerateColumns="False" DataSource="<%# DVAccount %>" ShowFooter="True" SearchMode="0">
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" ForeColor="White" CssClass="HeaderItemStyle"></HeaderStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemStyle Width="1%"></ItemStyle>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" height=15px ImageUrl="~/images/BtnDetail.gif" CommandName="Select"
								CausesValidation="False"></ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="account_id" SortExpression="account_id" ReadOnly="True" HeaderText="QSP&#160;Acct&#160;ID&#160;#">
						<ItemStyle Wrap="False" Width="50px"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="fulf_account_id" SortExpression="fulf_account_id" ReadOnly="True" HeaderText="EDS&#160;Acct&#160;#"
						DataFormatString="{0:D9}">
						<ItemStyle Wrap="False" Width="50px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="account_name" HeaderText="Account Name">
						<ItemStyle Wrap="False" Width="250px"></ItemStyle>
						<ItemTemplate>
							<ASP:LINKBUTTON id=lnkBtnAccount runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.account_name") %>' CommandName="Select" CausesValidation="False" Text='<%# DataBinder.Eval(Container, "DataItem.account_name") %>'>
							</ASP:LINKBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fiscal_year" HeaderText="Last&#160;FY&lt;BR&gt;w/Sale">
						<HeaderStyle HorizontalAlign="Center" width=70px></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fiscal_year") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fm_id" HeaderText="FSM&#160;ID&#160;#">
						<ItemStyle Wrap="False" Width="50px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblFMID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fm_id") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fm_name" HeaderText="FSM&#160;Name">
						<ItemStyle Wrap="False" Width="150px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblFMName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fm_name") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="program_type_name" SortExpression="program_type_name" ReadOnly="True"
						HeaderText="Program&#160;Type">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
			</cc2:SortedDataGrid></TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Account(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

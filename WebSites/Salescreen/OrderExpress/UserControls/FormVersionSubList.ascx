<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FormVersionSubList" Codebehind="FormVersionSubList.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0" width="100%">
	<tr>
		<td><asp:label id="lblFormTitle" CssClass="StandardLabel" runat="server"></asp:label>
			<br>
		</td>
	</tr>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:SortedDataGrid id=dtgForm runat="server" Font-Size="8pt" PageSize="30" Width="100%" AllowPaging="False" AllowSorting="True" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#CCCCCC" AutoGenerateColumns="False" DataSource="<%# DVForm %>" ShowFooter="True" SearchMode="0">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandName="Create"
								CausesValidation="False"></ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="form_id" SortExpression="form_id" ReadOnly="True" HeaderText="Form&nbsp;ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="form_code" SortExpression="form_code" ReadOnly="True" HeaderText="Code"></asp:BoundColumn>
					<asp:TemplateColumn SortExpression="form_name" HeaderText="Form Name">
						<ItemStyle Wrap="False" Width="60%"></ItemStyle>
						<ItemTemplate>
							<ASP:LINKBUTTON id=lnkBtnForm runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.form_id") %>' CommandName="Select" CausesValidation="False" Text='<%# DataBinder.Eval(Container, "DataItem.form_name") %>'>
							</ASP:LINKBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="version" SortExpression="version" ReadOnly="True" HeaderText="Version">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
			</cc2:SortedDataGrid></TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Form(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

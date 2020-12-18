<%@ Register TagPrefix="uc1" TagName="SearchModuleSelector" Src="SearchModuleSelector.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FMSelector" Codebehind="FMSelector.ascx.cs" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD><uc1:SearchModuleSelector id="QSPFormSearchModule" runat="server"></uc1:SearchModuleSelector></TD>
				</TR>
			</TABLE>
			<BR>
			<asp:label id="lblCurrentIndex" runat="server" Font-Size="xx-small" CssClass="eRewardsInstr">Page 1 of 1</asp:label></TD>
	</TR>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgList runat="server"  ShowFooter="True" DataSource="<%# DVList %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" CssClass=GridStyle CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="10">
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="Black" BackColor="#CCCCCC"></SelectedItemStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle CssClass="HeaderItemStyle"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
						<ItemTemplate>
							<asp:ImageButton id="imgBtnSelect" runat="server" ImageUrl="~/images/BtnSelect.gif" CommandName="Select"
								CausesValidation="False"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FMNumber" HeaderText="FSM&#160;ID">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FMNumber") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FMName" HeaderText="Name">
						<HeaderStyle Wrap="False" Width="250px"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:HyperLink id="hypLnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FMName") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
			</cc2:sorteddatagrid>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of FSM(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td>
						<asp:ImageButton id="imgBtnOK" Visible=False runat="server" CausesValidation="False" ImageUrl="~/images/btnOK.gif"
							AlternateText="Click here to confirm your selection"></asp:ImageButton>
					</td>
					<td>
						<asp:HyperLink id="hypLnkCancel" Visible=False runat="server" ImageUrl="~/images/btnCancel.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

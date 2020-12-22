<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.UserSelector" Codebehind="UserSelector.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModuleSelector" Src="SearchModuleSelector.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD><uc1:searchmoduleselector id="QSPFormSearchModule" runat="server"></uc1:searchmoduleselector></TD>
				</TR>
			</TABLE>
			<BR>
			<asp:label id="lblCurrentIndex" runat="server" CssClass="eRewardsInstr" Font-Size="xx-small">Page 1 of 1</asp:label></TD>
	</TR>
	<TR>
		<TD><!--DataGrid  --><cc2:sorteddatagrid id=dtgList runat="server" Font-Size="8pt" PageSize="10" AllowPaging="True" AllowSorting="True" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#CCCCCC" AutoGenerateColumns="False" DataSource="<%# DVList %>" ShowFooter="True" >
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Wrap="False" ForeColor="White" CssClass="HeaderItemStyle"></HeaderStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
						<ItemTemplate>
							<asp:ImageButton id="imgBtnSelect" runat="server" ImageUrl="~/images/BtnSelect.gif" CommandName="Select"
								CausesValidation="False"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="user_id" HeaderText="User&#160;ID">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.user_id") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="last_name, first_name" HeaderText="User Name">
						<HeaderStyle Wrap="False" Width="200px"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblUserName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.last_name") + ", " + DataBinder.Eval(Container, "DataItem.first_name") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="role_name" HeaderText="Role">
						<HeaderStyle Wrap="False" Width="125px"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.role_name") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" CssClass="HeaderItemStyle"
					Mode="NumericPages"></PagerStyle>
			</cc2:sorteddatagrid></TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" Font-Size="X-Small" Font-Name="Verdana" Font-Bold="true"
							ForeColor="#003366">
							Number of User(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="center"><br>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><asp:imagebutton id="imgBtnOK" runat="server" Visible=False AlternateText="Click here to confirm your selection"
							ImageUrl="~/images/btnOK.gif" CausesValidation="False"></asp:imagebutton></td>
					<td><asp:hyperlink id="hypLnkCancel" runat="server" Visible=False ImageUrl="~/images/BtnCancel.gif" NavigateUrl="javascript:window.close();">Cancel</asp:hyperlink></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

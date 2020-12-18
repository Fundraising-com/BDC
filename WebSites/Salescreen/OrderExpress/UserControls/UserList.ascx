<%@ Reference Control="UserDetailInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.UserList" Codebehind="UserList.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="20%" border="0">
				<TR>
					<TD><uc1:searchmodule id="QSPFormSearchModule" runat="server"></uc1:searchmodule></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD><br>
			<asp:HyperLink id="hypLnkAddNew" runat="server" NavigateUrl="javascript:void(0);" ImageUrl="~/images/BtnAdd.gif"></asp:HyperLink>
		</TD>
	</TR>
	<tr>
		<td><BR>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">&nbsp;:&nbsp;Click on Column Headings to Resort Data.</asp:label></td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:sorteddatagrid id="dtgUsers" runat="server" Font-Size="10pt" ShowFooter="True" AutoGenerateColumns="False" 
			DataSource="<%# DVUsers %>" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
			BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="30" width=700px>
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<FooterStyle CssClass="FooterItemStyle" Font-Size="10px"></FooterStyle>
                <ItemStyle CssClass="ItemStyle_off" Font-Size="12px"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" Font-Size="12px" ForeColor="White">
                </HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						
						<ItemTemplate>
							<asp:ImageButton id="imgBtnDetail" runat="server" CausesValidation="False" ImageUrl="~/images/BtnDetail.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.user_id") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="user_name" HeaderText="User Name">
						
						<ItemTemplate>
							<asp:Label id="lblUserName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.user_name") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="last_name" HeaderText="Last name">
						
						<ItemTemplate>
							<asp:Label id="lblLastName" Width="90px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.last_name") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="first_name" HeaderText="First name">
						
						<ItemTemplate>
							<asp:Label id="lblFirstName" Width="90px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.first_name") %>' />
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="title" HeaderText="Title">
						
						<ItemTemplate>
							<asp:Label id="lblTitle" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Title") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Email" HeaderText="Email">
						
						<ItemTemplate>
							<asp:Label id="Label1" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Email") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="day_phone_no" HeaderText="Phone">
						
						<ItemTemplate>
							<asp:Label id="Label2" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.day_phone_no") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="role_name" HeaderText="Role">
						
						<ItemTemplate>
							<asp:Label id="Label3" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.role_name") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</cc2:sorteddatagrid>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE id="tblSales" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" Font-Bold="true" ForeColor="#003366" Font-Size="X-Small"
							Font-Name="Verdana">
							Number of User(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

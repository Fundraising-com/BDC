<%@ Reference Control="TemplateEmailDetailInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.TemplateEmailList" Codebehind="TemplateEmailList.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="20%" border="0">
				<TR>
					<TD><uc1:searchmodule id="QSPFormSearchModule" runat="server" visible="false"></uc1:searchmodule></TD>
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
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click on Column Headings to Resort Data.</asp:label></td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:sorteddatagrid id="dtgTemplateEmailItems" runat="server" Font-Size="10pt" ShowFooter="True" AutoGenerateColumns="False" 
			DataSource="<%# DVTemplateEmail%>" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
			BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="30" width=700px>
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<FooterStyle CssClass="FooterItemStyle" Font-Size="10px"></FooterStyle>
                <ItemStyle CssClass="ItemStyle_off" Font-Size="12px"></ItemStyle>
                <HeaderStyle Wrap="False" CssClass="HeaderItemStyle" Font-Size="11px" ForeColor="White">
                </HeaderStyle>
				
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle></HeaderStyle>
						<ItemStyle Wrap="False" width="1%"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton id="imgBtnDetail" runat="server" CausesValidation="False" ImageUrl="~/images/BtnDetail.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.template_email_id") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="template_email_id" HeaderText="ID">
						
						<ItemTemplate>
							<asp:Label id="Label1" width="15px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.template_email_id") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="template_email_name" HeaderText="Name">
						
						<ItemTemplate>
							<asp:Label id="lblName" width="300px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.template_email_name") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="description" HeaderText="Description" visible="false">
						
						<ItemTemplate>
							<asp:Label id="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.description") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</cc2:sorteddatagrid>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number 
							of template(s) :
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

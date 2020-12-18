<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CampaignSubList" Codebehind="CampaignSubList.ascx.cs" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
			<BR>
		</TD>
	</TR>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgCamp runat="server" Font-Size="10pt" ShowFooter="True" DataSource="<%# DVCampaigns %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="False" PageSize="30">
				<PagerStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="#003366"
					Mode="NumericPages"></PagerStyle>
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<EditItemStyle Wrap="False"></EditItemStyle>
				<AlternatingItemStyle Font-Size="X-Small" Wrap="False" BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle Font-Size="X-Small" Wrap="False" ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Size="X-Small" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Campaign_ID") %>' CommandName="Select" CausesValidation="False">
							</ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="campaign_id" SortExpression="campaign_id" ReadOnly="True" HeaderText="ID"></asp:BoundColumn>
					<asp:TemplateColumn SortExpression="Campaign_Name" HeaderText="Campaign Name">
						<ItemStyle Wrap="False" Width="200px"></ItemStyle>
						<ItemTemplate>
							<ASP:LINKBUTTON id="lnkBtnCampaign" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Campaign_Name") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Campaign_ID") %>' CommandName="Select">
							</ASP:LINKBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="start_date" HeaderText="Start&#160;Date">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.start_date", "{0:MM/dd/yyyy}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="end_date" HeaderText="End&#160;Date">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.end_date", "{0:MM/dd/yyyy}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="fiscal_year" SortExpression="fiscal_year" ReadOnly="True" HeaderText="FY">
						<ItemStyle Wrap="False" Width="40px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="program_type_name" HeaderText="Program&#160;Type">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.program_type_name").ToString().Replace(" ", "&nbsp;") %>'>
							</asp:Label>
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
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" ForeColor="#993300" Font-Bold="true" Font-Size="X-Small"
							Font-Name="Verdana">
							Number of Campaign(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

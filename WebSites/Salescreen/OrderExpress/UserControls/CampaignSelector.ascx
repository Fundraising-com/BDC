<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CampaignSelector" Codebehind="CampaignSelector.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModuleSelector" Src="SearchModuleSelector.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD><uc1:SearchModuleSelector id="QSPFormSearchModule" runat="server"></uc1:SearchModuleSelector></TD>
				</TR>
			</TABLE>
			<BR>
			<asp:label id="lblCurrentIndex" runat="server" Font-Size="xx-small" CssClass="CurrentIndexSelector">Page 1 of 1</asp:label></TD>
	</TR>
	<TR>
		<TD><!--DataGrid  -->
			<cc2:sorteddatagrid id=dtgList runat="server" Font-Size="8pt" ShowFooter="True" DataSource="<%# DVList %>" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowSorting="True" AllowPaging="True" PageSize="10">
				<AlternatingItemStyle CssClass="AlternatingItemStyle_off"></AlternatingItemStyle>
				<FooterStyle ForeColor="Black" BackColor="#F5F5F5"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="Black" BackColor="#CCCCCC"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle_off"></ItemStyle>
				<HeaderStyle Font-Bold="True" Wrap="False" ForeColor="White" BackColor="#003366"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
						<ItemTemplate>
							<asp:ImageButton id="imgBtnSelect" runat="server" ImageUrl="~/images/BtnSelect.gif" CommandName="Select"
								CausesValidation="False"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fulf_account_id" HeaderText="Account&nbsp;#">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbFULFAccountID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fulf_account_id") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="campaign_id" HeaderText="ID" visible="false">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.campaign_id") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="campaign_name" HeaderText="Name">
						<HeaderStyle Wrap="False" Width="100%"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:HyperLink id="hypLnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.campaign_name") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="program_type_name" HeaderText="Program type">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.program_type_name") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fm_id" HeaderText="FMID">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbFMID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fm_id") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fm_name" HeaderText="FM Name">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbFMname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fm_name") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="City" HeaderText="City">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="subdivision_name_1" HeaderText="State">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbState" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.subdivision_name_1") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lbZip" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="#003366"
					Mode="NumericPages"></PagerStyle>
			</cc2:sorteddatagrid>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" ForeColor="#993300" Font-Bold="true" Font-Size="x-small"
							Font-Name="Verdana">
							Number of Campaign(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR id="trButton" runat="server">
		<TD align="center">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td>
						<asp:ImageButton id="imgBtnOK" runat="server" CausesValidation="False" ImageUrl="~/images/btnOKBig.gif"
							AlternateText="Click here to confirm your selection"></asp:ImageButton>
					</td>
					<td>
						<asp:HyperLink id="hypLnkCancel" runat="server" ImageUrl="~/images/btnCancelBig.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

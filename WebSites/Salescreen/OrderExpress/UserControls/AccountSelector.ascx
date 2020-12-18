<%@ Reference Control="AccountDetailInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountSelector" Codebehind="AccountSelector.ascx.cs" %>
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
			<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
				<TR>
					<TD align="left">
						<asp:label id="lblCurrentIndex" runat="server" CssClass="eRewardsInstr">Page 1 of 1</asp:label>
					</TD>
					<TD align="right">
						<TABLE cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD align="left">
									<asp:label id="Label1" runat="server" Font-Size="xx-small" Font-Bold="True" CssClass="eRewardsInstr">Legend&nbsp;:&nbsp;</asp:label>
								</TD>
								<TD align="left">
									<asp:Label id="Label5" runat="server" Width="5px" BackColor="White" Height="3px" BorderWidth="1px"
										BorderStyle="Solid" BorderColor="Black">
										&nbsp;&nbsp;
									</asp:Label>
								</TD>
								<TD align="left">
									<asp:label id="Label3" runat="server" Font-Size="xx-small" Font-Bold="True" CssClass="eRewardsInstr">Active</asp:label>
								</TD>
								<TD align="left">
									&nbsp;&nbsp;
								</TD>
								<TD align="left">
									<asp:Label id="Label6" runat="server" Width="5px" BackColor="Orange" Height="3px" BorderWidth="1px"
										BorderStyle="Solid" BorderColor="Black">
										&nbsp;&nbsp;
									</asp:Label>
								</TD>
								<TD align="left">
									<asp:label id="Label4" runat="server" Font-Bold="True" Font-Size="xx-small" CssClass="eRewardsInstr">Inactive</asp:label>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</TD>
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
					<asp:TemplateColumn>
						<ItemTemplate>
							<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CausesValidation="False"></ASP:IMAGEBUTTON>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<HeaderStyle Width="10px" HorizontalAlign="Center"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblStatusRead" runat="server" Width="5px" BackColor='<%# System.Drawing.Color.FromName(DataBinder.Eval(Container, "DataItem.Color_Code").ToString()) %>' Height="5px" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
								&nbsp;&nbsp;
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="account_id" HeaderText="QSP&nbsp;ID">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.account_id")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fulf_account_id" HeaderText="EDS&nbsp;Acct&nbsp;#">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id="lblAccountNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fulf_account_id", "{0:D9}")%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="account_name" HeaderText="Account&nbsp;Name">
						<HeaderStyle Width="250px"></HeaderStyle>
						<ItemTemplate>
							<asp:HyperLink id="hypLnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.account_name") %>' ForeColor="#336699" NavigateUrl="javascript:void(0);">
							</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fiscal_year" HeaderText="FY">
						<ItemTemplate>
							<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fiscal_year") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="city" SortExpression="city" ReadOnly="True" HeaderText="City">
						<ItemStyle Wrap="False" Width="130px"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="Zip" HeaderText="Zip">
						<ItemStyle Wrap="False" Width="100px"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="subdivision_name_1" SortExpression="subdivision_name_1" ReadOnly="True"
						HeaderText="State">
						<ItemStyle Wrap="False" Width="130px"></ItemStyle>
					</asp:BoundColumn>
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
						<asp:label id="lblTotal" runat="server" ForeColor="#993300" Font-Bold="true" Font-Size="X-Small"
							Font-Name="Verdana">
							Number of Account(s):
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

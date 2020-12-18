<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.EntityExceptionList" Codebehind="EntityExceptionList.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ShippingChargesCustomer" Src="ShippingChargesCustomer.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr id="trResultList" runat="server">
		<td><asp:datagrid id=dtgEntityException runat="server" DataSource="<%# dvException %>" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" DataKeyField="entity_exception_id" AllowSorting="True" ShowFooter="True" Font-Size="8pt" Font-Names="Verdana">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="No">
						<HeaderStyle Width="5px"></HeaderStyle>
						<ItemTemplate>
							<ASP:LABEL id=lblItemNo runat="server" CssClass="StandardLabel" Width="10px" Text='<%# (Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex")) + 1) %>'>
							</ASP:LABEL>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="exception_type_id" HeaderText="Type" Visible="False">
						<ItemTemplate>
							<ASP:LABEL id="lblExceptionTypeID" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.exception_type_id") %>'>
							</ASP:LABEL>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="exception_type_name" HeaderText="Type" Visible="False">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemTemplate>
							<ASP:LABEL id="Label6" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.exception_type_name") %>'>
							</ASP:LABEL>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
						    <asp:Image ID=imgSeverity runat=server ImageUrl='<%#  (Convert.ToInt32(DataBinder.Eval(Container, "DataItem.exception_type_id")) < 102 )|| (Convert.ToInt32(DataBinder.Eval(Container, "DataItem.exception_type_id")) == 104) ? "~/images/icon/level_1.gif" : Convert.ToInt32(DataBinder.Eval(Container, "DataItem.exception_type_id")) < 200 ? "~/images/icon/level_2.gif" : "~/images/icon/level_3.gif"  %>' />
							<ASP:LABEL id="Label1" Visible=false runat="server" Text='<%#  Convert.ToInt32(DataBinder.Eval(Container, "DataItem.exception_type_id")) >= 200 ? "Y" : "N"  %>'>
							</ASP:LABEL>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Approvable" Visible="False">
						<ItemTemplate>
							<ASP:LABEL id="Label7" runat="server" Text='<%#  Convert.ToInt32(DataBinder.Eval(Container, "DataItem.exception_type_id")) >= 200 && Convert.ToInt32(DataBinder.Eval(Container, "DataItem.exception_type_id")) <= 299 ? "Y" : "N"  %>'>
							</ASP:LABEL>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="message" HeaderText="Message">
						<HeaderStyle Width="100%"></HeaderStyle>
						<ItemTemplate>
							<ASP:LABEL id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.message") %>' CssClass="lblMessage" >
							</ASP:LABEL>
						</ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="exception_expression" HeaderText="Expression">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemTemplate>
							<ASP:LABEL id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.exception_expression") %>'>
							</ASP:LABEL>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="fees_value_amount" HeaderText="Fees">
						<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<ASP:LABEL id=Label4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fees_value_amount", "{0:C}") %>'>
							</ASP:LABEL>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="approved" HeaderText="Approved">
						<HeaderStyle Width="30px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<ASP:CheckBox id="chkApproved" runat="server" Enabled=False Checked='<%#  DataBinder.Eval(Container, "DataItem.approved").ToString() != "" ? DataBinder.Eval(Container, "DataItem.approved") : false  %>'>
							</ASP:CheckBox>
							<ASP:LABEL id="lblNonApplicable" Visible="False" runat="server" Text="N/A"></ASP:LABEL>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="approve_user_name" HeaderText="Approved&#160;by">
						<ItemTemplate>
							<ASP:LABEL id="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.approve_user_name") %>'>
							</ASP:LABEL>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
		</td>
	</tr>
	<tr id="trResultListEmpty" runat="server">
		<td>
			<table id="Table6" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><br>
						<table id="Table12" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td colSpan="4"><asp:label id="lblBusinessRule" runat="server" CssClass="StandardLabel">The order has performed sucessfully all validation.
									</asp:label><br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<uc1:ShippingChargesCustomer id="ShippingChargesCustomerDetail" runat="server"></uc1:ShippingChargesCustomer>
		</td>
	</tr>
</table>

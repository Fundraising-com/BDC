<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessFieldList" Codebehind="BusinessFieldList.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td>
			<table id="Table2" cellSpacing="0" cellPadding="3" width="100%" align="center" border="0">
				<tr>
					<td><asp:label id="lblFormTitle" CssClass="TitleLabel2" runat="server"></asp:label>
						<br>
					</td>
				</tr>
				<tr>
					<td><asp:imagebutton id="imgBtnAddNew" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/BtnAdd.gif"
							ToolTip="Click here to add a new Business Field !" AlternateText="Add New"></asp:imagebutton></td>
				</tr>
			</table>
			<asp:datagrid id=dtgBusinessField runat="server" Font-Names="Verdana" Font-Size="10pt" ShowFooter="True" AllowSorting="True" DataKeyField="field_id" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#999999" DataSource="<%# dtBusinessField %>">
				<PagerStyle CssClass="PagerItemStyle" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle CssClass="AlternatingItemStyle"></AlternatingItemStyle>
				<FooterStyle CssClass="FooterItemStyle"></FooterStyle>
				<SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
				<ItemStyle CssClass="ItemStyle"></ItemStyle>
				<HeaderStyle Wrap="False" CssClass="HeaderItemStyle" forecolor="White"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:ImageButton id="imgBtnDelete" runat="server" ImageUrl="~/images/BtnDelete.gif" CommandName="Delete"
								CausesValidation="False"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="field_type_name" HeaderText="Type">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemTemplate>
							<asp:DropDownList id=ddlType DataSource="<%# tblTypeBusinessField %>" runat="server" SelectedIndex='<%# getSelectedIndex(tblTypeBusinessField, Convert.ToString(DataBinder.Eval(Container, "DataItem.field_type_id"))) %>' DataValueField="field_type_id" DataTextField="field_type_name">
							</asp:DropDownList>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="field_name" HeaderText="Field Name">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtName" Text='<%# DataBinder.Eval(Container, "DataItem.field_name") %>' runat="server" Width="200px">
										</asp:TextBox>
									</td>
									<td>
										<asp:RequiredFieldValidator id="ReqFldVal_Name" CssClass="LabelError" runat="server" ErrorMessage="The Field Name is required"
											ControlToValidate="txtName">*</asp:RequiredFieldValidator>
									</td>
								</tr>
							</table>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="description" HeaderText="Description">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtDescription" Text='<%# DataBinder.Eval(Container, "DataItem.description") %>' runat="server" Width="200px">
										</asp:TextBox>
									</td>
									<td>
										<asp:RequiredFieldValidator id="ReqFldVal_Desc" CssClass="LabelError" runat="server" ErrorMessage="The Description is required"
											ControlToValidate="txtDescription">*</asp:RequiredFieldValidator>
									</td>
								</tr>
							</table>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="is_form_property" HeaderText="Form<br>Property">
						<HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkBoxIsFormProperty" Checked='<%# DataBinder.Eval(Container, "DataItem.is_form_property")  %>' Runat="server">
							</asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="is_apply_to_account" HeaderText="Apply&nbsp;To<br>Account">
						<HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkBoxApplyToAccount" Checked='<%# DataBinder.Eval(Container, "DataItem.is_apply_to_account")  %>' runat="server">
							</asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="is_apply_to_credit_application" HeaderText="Apply&nbsp;To<br>Credit&nbsp;App.">
						<HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkBoxApplyToCreditApp" Checked='<%# DataBinder.Eval(Container, "DataItem.is_apply_to_credit_application")  %>' Runat="server">
							</asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="is_apply_to_order" HeaderText="Apply&nbsp;To<br>Order">
						<HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkBoxApplyToOrder" Checked='<%# DataBinder.Eval(Container, "DataItem.is_apply_to_order")  %>' Runat="server">
							</asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="is_system" HeaderText="Is<br>System">
						<HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox id="chkBoxIsSystem" Enabled=False Checked='<%# DataBinder.Eval(Container, "DataItem.is_system")  %>' Runat="server">
							</asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White" BackColor="#999999"
					Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
	<tr>
		<td align="center"><br>
		</td>
	</tr>
	<tr>
		<td align="center">
			<asp:ImageButton id="imgBtnSave" runat="server" AlternateText="Save" ImageUrl="~/images/btnSave.gif"
				CausesValidation="False"></asp:ImageButton></td>
	</tr>
</table>

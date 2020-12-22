<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.PhoneNumberList" Codebehind="PhoneNumberList.ascx.cs" %>

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
							ToolTip="Click here to add a new phone number !" AlternateText="Add New"></asp:imagebutton></td>
				</tr>
			</table>
			<asp:datagrid id=dtgPhoneNumber runat="server" Font-Names="Verdana" Font-Size="Small" ShowFooter="True" AllowSorting="True" DataKeyField="phone_number_entity_id" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#999999" DataSource="<%# dtPhoneNumber %>">
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<EditItemStyle Wrap="False"></EditItemStyle>
				<AlternatingItemStyle Font-Size="X-Small" Wrap="False" BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle Font-Size="X-Small" Wrap="False" ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Size="X-Small" Font-Bold="True" ForeColor="White" BackColor="#2F4F88"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:ImageButton id="imgBtnDelete" runat="server" ImageUrl="~/images/BtnDelete.gif" CommandName="Delete"
								CausesValidation="False"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="phone_number_type_name" HeaderText="Type">
						<HeaderStyle Width="100px" ></HeaderStyle>
						<ItemTemplate>
							<asp:DropDownList id=ddlType DataSource="<%# tblTypePhoneNumber %>" runat="server" SelectedIndex='<%# getSelectedIndex(tblTypePhoneNumber, Convert.ToString(DataBinder.Eval(Container, "DataItem.phone_number_type_id"))) %>' DataValueField="phone_number_type_id" DataTextField="phone_number_type_name">
							</asp:DropDownList>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="phone_number" HeaderText="Phone Number">
						<HeaderStyle Width="200px"></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtPhoneNumber" Text='<%# DataBinder.Eval(Container, "DataItem.phone_number") %>' runat="server" Width="400px">
										</asp:TextBox>
									</td>
									<td>
										<asp:RequiredFieldValidator id="ReqFldVal_PhoneNumber" CssClass="LabelError" runat="server" ErrorMessage="The Phone Number is required"
											ControlToValidate="txtPhoneNumber">*</asp:RequiredFieldValidator>
									</td>
								</tr>
							</table>
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
		<td align="center"><asp:label id="lblMessage" runat="server" CssClass="QSPGAppError"></asp:label></td>
	</tr>
</table>

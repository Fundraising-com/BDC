<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.EmailAddressList" Codebehind="EmailAddressList.ascx.cs" %>

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
			<asp:datagrid id=dtgEmailAddress runat="server" Font-Names="Verdana" Font-Size="Small" ShowFooter="True" AllowSorting="True" DataKeyField="email_entity_id" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#999999" DataSource="<%# dtEmailAddress %>">
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
					<asp:TemplateColumn SortExpression="email_type_name" HeaderText="Type">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemTemplate>
							<asp:DropDownList id=ddlType DataSource="<%# dvTypeEmailAddress %>" runat="server" SelectedIndex='<%# getSelectedIndex(dvTypeEmailAddress, Convert.ToString(DataBinder.Eval(Container, "DataItem.email_type_id"))) %>' DataValueField="email_type_id" DataTextField="email_type_name">
							</asp:DropDownList>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="email_address" HeaderText="Email&nbsp;Address">
						<HeaderStyle Width="200px"></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtEmailAddress" Text='<%# DataBinder.Eval(Container, "DataItem.email_address") %>' runat="server" Width="200px">
										</asp:TextBox>
									</td>
									<td>
										<asp:RequiredFieldValidator id="ReqFldVal_PhoneNumber" CssClass="LabelError" runat="server" ErrorMessage="The Email Address is required"
											ControlToValidate="txtEmailAddress">*</asp:RequiredFieldValidator>
									</td>
									<td>
									</td>
								</tr>
							</table>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="recipient_name" HeaderText="Recipient&nbsp;Name">
						<HeaderStyle Width="200px"></HeaderStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<asp:TextBox id="txtRecipientName" Text='<%# DataBinder.Eval(Container, "DataItem.recipient_name") %>' runat="server" Width="200px">
										</asp:TextBox>
									</td>
									<td>
										<asp:RequiredFieldValidator id="ReqFldVal_RecipientName" CssClass="LabelError" runat="server" ErrorMessage="The Recipient Name is required"
											ControlToValidate="txtRecipientName">*</asp:RequiredFieldValidator>
									</td>
									<td>
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

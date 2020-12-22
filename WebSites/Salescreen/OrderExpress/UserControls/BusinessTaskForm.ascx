<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessTaskForm" Codebehind="BusinessTaskForm.ascx.cs" %>

<table id="tblFormDetailTitle" cellSpacing="0" cellPadding="0" width="550" border="0">
	<TR>
		<TD>
			<asp:ImageButton id="imgBtnAddNew" runat="server" ImageUrl="~/images/BtnAdd.gif" CommandName="Add"
				CausesValidation="False"></asp:ImageButton>
		</TD>
	</TR>
	<tr>
		<td><asp:datalist id=dtLstBizTask  runat="server" DataKeyField="business_task_id" width="500px" DataSource="<%# DVBizTask %>">
				<SeparatorTemplate>
					<hr width="100%" size="1px" color="#003366">
				</SeparatorTemplate>
				<ItemTemplate>
					<TABLE cellSpacing="0" cellPadding="3" width="500px" border="0">
						<TR>
							<TD>
								<asp:label id="lblName" CssClass="StandardLabel" runat="server">Name&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<asp:TextBox id="txtName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.business_task_name") %>' Width="350px">
											</asp:TextBox>
										</td>
										<td>
											<asp:RequiredFieldValidator id="ReqFldVal_Name" CssClass="LabelError" runat="server" ErrorMessage="The Name is required"
												ControlToValidate="txtName">*</asp:RequiredFieldValidator>
										</td>
									</tr>
								</table>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="Label3" CssClass="StandardLabel" runat="server">Task&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD vAlign="top">
								<asp:DropDownList id="ddlTask" runat="server" DataSource="<%# tblTask %>" DataTextField="task_name" DataValueField="task_id" SelectedIndex='<%# getSelectedIndex(tblTask,Convert.ToString(DataBinder.Eval(Container, "DataItem.task_id"))) %>'>
								</asp:DropDownList>
							</TD>
						</TR>
						<TR id="trRelatedBizTask" runat="server" visible="false">
							<TD>
								<asp:label id="Label2" CssClass="StandardLabel" runat="server">Related&nbsp;Biz&nbsp;Task&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD vAlign="top">
								<asp:DropDownList id="ddlBusinessTask" runat="server" DataSource='<%# FilterDataViewForParentBizTask(DVParentBizTask, Convert.ToString(DataBinder.Eval(Container, "DataItem.business_task_id"))) %>' DataTextField="business_task_name" DataValueField="business_task_id" SelectedIndex='<%# getSelectedIndex(DVParentBizTask,Convert.ToString(DataBinder.Eval(Container, "DataItem.parent_business_task_id"))) %>'>
								</asp:DropDownList>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="Label1" CssClass="StandardLabel" runat="server">Assign. Type&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD vAlign="top">
								<asp:DropDownList  id="ddlAssignmentType" runat="server" SelectedIndex='<%# (Convert.ToInt32(DataBinder.Eval(Container, "DataItem.assignment_type_id")) -1) %>'>
									<asp:ListItem Selected="True" Value="1">Specific User</asp:ListItem>
									<asp:ListItem Value="2">Current User</asp:ListItem>									
									<asp:ListItem Value="3">Specific Role</asp:ListItem>
									<asp:ListItem Value="4">Current FSM</asp:ListItem>
								</asp:DropDownList>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="Label7" runat="server" CssClass="StandardLabel">Assigned To :</asp:label></TD>
							<TD>
								<TABLE cellSpacing="0" cellPadding="0" width="90%" border="0">
									<TR id="UserSelectorRow" style="DISPLAY: block" runat="server">
										<TD>
											<asp:TextBox id="txtAssignedUserID" runat="server" Width="50px" Enabled="True" Text='<%# DataBinder.Eval(Container, "DataItem.assigned_user_id") %>' >
											</asp:TextBox>
										</TD>
										<TD>&nbsp;
											<asp:TextBox id="txtAssignedUserName" runat="server" Width="230px" Enabled="True" Text='<%# DataBinder.Eval(Container, "DataItem.last_name") + ", " + DataBinder.Eval(Container, "DataItem.first_name") %>' >
											</asp:TextBox>
										</TD>
										<TD align="right">
											<asp:ImageButton id="imgBtnSelectUser" runat="server" ImageUrl="~/images/BtnSelect.gif" CausesValidation="False"></asp:ImageButton></TD>
									</TR>
									<TR id="RoleSelectorRow" style="DISPLAY: none" runat="server">
										<TD vAlign="top" colspan="3">
											<asp:DropDownList id="ddlRole" runat="server" DataSource='<%# DVRole %>' DataTextField="Role_Name" DataValueField="Role_ID" SelectedIndex='<%# getSelectedIndex(DVRole,Convert.ToString(DataBinder.Eval(Container, "DataItem.assigned_role_id"))) %>'>
											</asp:DropDownList>
										</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<asp:label id="lblExpression" CssClass="StandardLabel" runat="server">Expression&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:TextBox id="txtExpression" runat="server" CssClass=DescLabel TextMode=MultiLine Rows=3 Text='<%# DataBinder.Eval(Container, "DataItem.task_expression") %>' Width="350px">
								</asp:TextBox>
							</TD>
						</TR>
						<tr>
							<td valign="top" colspan="2" align="right">
								<asp:ImageButton id="imgBtnExBuilder" runat="server" ImageUrl="~/images/btnExBuilder.gif" CommandName="ExpressionBuilder"
									CausesValidation="False"></asp:ImageButton>
							</td>
						</tr>
						<TR>
							<TD vAlign="top">
								<asp:label id="lblDescription" CssClass="StandardLabel" runat="server">Description&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:TextBox id="txtDescription" runat="server" CssClass=DescLabel TextMode=MultiLine Rows=3 Text='<%# DataBinder.Eval(Container, "DataItem.description") %>' Width="350px">
								</asp:TextBox>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<asp:label id="lblMessage" CssClass="StandardLabel" runat="server">Message&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:TextBox id="txtMessage" runat="server" CssClass=DescLabel TextMode=MultiLine Rows=3 Text='<%# DataBinder.Eval(Container, "DataItem.Message") %>' Width="350px">
								</asp:TextBox>
							</TD>
						</TR>
						<tr>
							<td colspan="3">
								<asp:ImageButton id="imgBtnDelete" runat="server" ImageUrl="~/images/BtnDelete.gif" CommandName="Delete"
									CausesValidation="False"></asp:ImageButton>
							</td>
						</tr>
					</TABLE>
				</ItemTemplate>
			</asp:datalist>
		</td>
	</tr>
	<tr>
		<td align="center"></td>
	</tr>
</table>
<input type="hidden" id="hidFormID" runat="server">

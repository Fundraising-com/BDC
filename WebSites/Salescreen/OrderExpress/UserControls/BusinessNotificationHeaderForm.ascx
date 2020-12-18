<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessNotificationHeaderForm" Codebehind="BusinessNotificationHeaderForm.ascx.cs" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td>
			<table id="htmlTableToDoHeader" cellSpacing="0" cellPadding="2" border="0" runat="server"
				width="500">
				<tr>
					<td class="StandardLabel">Note ID # :</td>
					<td colSpan="3" width="300"><asp:label id="lblBusinessNotificationID" runat="server" CssClass="DescInfoLabel"></asp:label></td>
				</tr>
				<tr>
					<td class="StandardLabel">Note Name :</td>
					<td colSpan="3">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<asp:textbox id="txtName" runat="server" Width="400px" MaxLength="100"></asp:textbox>
								</td>
								<td>
									<asp:requiredfieldvalidator id="ReqFldVal_Name" runat="server" Display="Dynamic" Text="*" ErrorMessage="Please enter a Note Name"
										ControlToValidate="txtName"></asp:requiredfieldvalidator>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="StandardLabel">Note Type&nbsp;:
					</td>
					<td colSpan="3">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><asp:dropdownlist id="ddlNoteType" Runat="server"></asp:dropdownlist></td>
								<td><asp:CompareValidator id="compVal_NoteType" runat="server" Display="Dynamic" Text="*" ErrorMessage="Please enter a Note Type"
										ControlToValidate="ddlNoteType" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"></asp:CompareValidator></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="StandardLabel">Business&nbsp;Task :</td>
					<td colSpan="3"><asp:label id="lblTask" runat="server" CssClass="DescInfoLabel"></asp:label></td>
				</tr>
				<tr>
					<td class="StandardLabel">Assigned User :</td>
					<td colSpan="3">
						<TABLE cellSpacing="0" cellPadding="0" width="90%" border="0">
							<TR>
								<TD><asp:textbox id=txtAssignedUserID runat="server" Width="50px" Text='<%# DataBinder.Eval(Container, "DataItem.assigned_user_id") %>' Enabled="True" >
									</asp:textbox></TD>
								<TD>&nbsp;
									<asp:textbox id=txtAssignedUserName runat="server" Width="230px" Text='<%# DataBinder.Eval(Container, "DataItem.last_name") + ", " + DataBinder.Eval(Container, "DataItem.first_name") %>' Enabled="True" ReadOnly="True">
									</asp:textbox>
								</TD>
								<td>
									<asp:requiredfieldvalidator id="ReqFldVal_AssignedUserID" runat="server" Display="Dynamic" Text="*" ErrorMessage="Please assigne a User to this note"
										ControlToValidate="txtAssignedUserID"></asp:requiredfieldvalidator>
								</td>
								<TD align="right"><asp:imagebutton id="imgBtnSelectUser" runat="server" ImageUrl="~/images/BtnSelect.gif" CausesValidation="False"></asp:imagebutton></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td class="StandardLabel">Context&nbsp;:</td>
					<td colSpan="3">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><asp:dropdownlist id="ddlEntityTypeID" Runat="server">
										<asp:ListItem Value="0" Selected="True">--SELECT--</asp:ListItem>
										<asp:ListItem Value="1">Organization</asp:ListItem>
										<asp:ListItem Value="2">Account</asp:ListItem>
										<asp:ListItem Value="4">Order</asp:ListItem>
										<asp:ListItem Value="6">Credit Application</asp:ListItem>
									</asp:dropdownlist></td>
								<td class="StandardLabel">&nbsp;#&nbsp;<asp:textbox id="txtEntityID" runat="server"></asp:textbox>
								</td>
								<td><asp:regularexpressionvalidator id="ReqFldVal_EntityID" runat="server" Display="Dynamic" Text="*" ErrorMessage="Please enter a numeric value"
										ControlToValidate="txtEntityID" ValidationExpression="\d*"></asp:regularexpressionvalidator></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="StandardLabel">Subject :</td>
					<td colSpan="3">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<asp:textbox id="txtSubject" Width="400px" MaxLength="100" Runat="server"></asp:textbox>
								</td>
								<td>
									<asp:requiredfieldvalidator id="ReqFldVal_Subject" runat="server" ErrorMessage="The subject is required" ControlToValidate="txtSubject">*</asp:requiredfieldvalidator>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="StandardLabel" vAlign="top">Message :</td>
					<td colSpan="3">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<asp:textbox id="txtMessage" Width="400px" MaxLength="200" Runat="server" Height="64px" TextMode="MultiLine"></asp:textbox>
								</td>
								<td valign="top">
									<asp:requiredfieldvalidator id="ReqFldVal_Message" runat="server" CssClass="LabelError" ErrorMessage="The message is required"
										ControlToValidate="txtMessage">*</asp:requiredfieldvalidator>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="StandardLabel" vAlign="top">Description :</td>
					<td colSpan="3"><asp:textbox id="txtDescription" Width="400px" MaxLength="200" Runat="server" Height="64px" TextMode="MultiLine"></asp:textbox></td>
				</tr>
				<tr id="trCompleteRow" runat="server">
					<td class="StandardLabel">Is&nbsp;Complete :</td>
					<td><asp:checkbox id="chkComplete" Runat="server"></asp:checkbox></td>
					<td class="StandardLabel" align="right">&nbsp;&nbsp;&nbsp;&nbsp;Completion&nbsp;Date&nbsp;:</td>
					<td><asp:label id="lblCompleteDate" CssClass="DescInfoLabel" Runat="server" Width="120px"></asp:label></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="center">
			<table id="tblAudit" runat="server" visible="false">
				<tr>
					<td class="StandardLabel">Created by :</td>
					<td><asp:label id="lblCreateBy" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
					<td class="StandardLabel">Created date :</td>
					<td><asp:label id="lblCreateDate" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="StandardLabel">Updated by :</td>
					<td><asp:label id="lblUpdateBy" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
					<td class="StandardLabel">Update date :</td>
					<td><asp:label id="lblUpdateDate" CssClass="DescInfoLabel" Runat="server"></asp:label></td>
				</tr>
			</table>
		</td>
	</tr>
</table>

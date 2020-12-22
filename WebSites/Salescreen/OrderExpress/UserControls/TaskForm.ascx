<%@ Register TagPrefix="ccval" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.TaskForm" Codebehind="TaskForm.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="SectionPageTitleInfo" colSpan="2"><asp:label id="Label3" runat="server">
			Task Information
			</asp:label></td>
	</tr>
	<tr>
		<td><br>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td>
									<TABLE cellSpacing="0" cellPadding="1" width="600" border="0">
										<TR>
											<TD><asp:label id="Label5" runat="server" CssClass="StandardLabel">Task&nbsp;ID&nbsp;:&nbsp;</asp:label></TD>
											<TD width="100%" colSpan="3"><asp:label id="lblTaskID" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label4" runat="server" CssClass="StandardLabel">Task&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtTaskName" runat="server" CssClass="DescLabel" MaxLength="100" Width="400px"></asp:textbox></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="Label2" runat="server" CssClass="StandardLabel">Task&nbsp;Type&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:dropdownlist id="ddlTaskType" runat="server" CssClass="DescLabel" AutoPostBack="True"></asp:dropdownlist></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_TaskType" runat="server" CssClass="LabelError" ControlToValidate="ddlTaskType"
																ErrorMessage="The Task Type is required.">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="trNoteType" runat="server" CssClass="StandardLabel">Note&nbsp;Type&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:dropdownlist id="ddlNoteType" runat="server" CssClass="DescLabel" AutoPostBack="True"></asp:dropdownlist></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_NoteType" runat="server" CssClass="LabelError" ControlToValidate="ddlNoteType"
																ErrorMessage="The Note Type is required.">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<tr id="trTemplateEmail" runat="server">
											<TD><asp:label id="Label1" runat="server" CssClass="StandardLabel">Template&nbsp;Email&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:dropdownlist id="ddlTemplateEmail" runat="server" CssClass="DescLabel"></asp:dropdownlist></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_TemplateEmail" runat="server" CssClass="LabelError" ControlToValidate="ddlTemplateEmail"
																ErrorMessage="The Template Name is required when task type is SEND EMAIL">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</tr>
										<tr id="trProcName" runat="server">
											<TD><asp:label id="lblProcName" runat="server" CssClass="StandardLabel">Stored&nbsp;Procedure&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtProcName" runat="server" CssClass="DescLabel" Width="300px"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_ProcName" runat="server" CssClass="LabelError" ControlToValidate="txtProcName"
																ErrorMessage="The Stored Procedure Name is required when task type is EXECUTE SQL">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</tr>
										<tr id="trParamName" runat="server">
											<TD><asp:label id="lblParamName" runat="server" CssClass="StandardLabel">Parameter&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtParamName" runat="server" CssClass="DescLabel" MaxLength="50" Width="300px"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_ParamName" runat="server" CssClass="LabelError" ControlToValidate="txtParamName"
																ErrorMessage="The Stored Procedure Parameter Name is required when task type is EXECUTE SQL">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</tr>
										<TR>
											<TD vAlign="top"><asp:label id="Label8" runat="server" CssClass="StandardLabel">Description&nbsp;:&nbsp;</asp:label></TD>
											<TD vAlign="top" colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtDescription" runat="server" CssClass="DescLabel" Width="400px" TextMode="Multiline"></asp:textbox></td>
													</tr>
												</table>
											</TD>
										</TR>
									</TABLE>
									<br>
									<br>
								</td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>

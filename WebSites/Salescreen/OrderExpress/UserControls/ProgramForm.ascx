<%@ Register TagPrefix="ccval" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramForm" Codebehind="ProgramForm.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="SectionPageTitleInfo" colSpan="2"><asp:label id="Label3" runat="server">
			Program Information
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
											<TD><asp:label id="Label5" runat="server" CssClass="StandardLabel">Program&nbsp;ID&nbsp;:&nbsp;</asp:label></TD>
											<TD width="100%" colSpan="3"><asp:label id="lblProgramID" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label4" runat="server" CssClass="StandardLabel">Program&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtProgramName" runat="server" CssClass="DescLabel" MaxLength="100" Width="400px"></asp:textbox></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="Label2" runat="server" CssClass="StandardLabel">Program&nbsp;Type&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:dropdownlist id="ddlProgramType" runat="server" CssClass="DescLabel" AutoPostBack="False"></asp:dropdownlist></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_ProgramType" runat="server" CssClass="LabelError" ControlToValidate="ddlProgramType"
																ErrorMessage="The Program Type is required.">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>	
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

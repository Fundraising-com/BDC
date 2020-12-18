<%@ Register TagPrefix="ccval" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.WarehouseForm" Codebehind="WarehouseForm.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="SectionPageTitleInfo" colSpan="2">
			<asp:label id="Label3" runat="server" Visible=False>
				Warehouse Information
			</asp:label></td>
	</tr>
	<tr>
		<td>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td>
									<TABLE cellSpacing="0" cellPadding="1" width="600" border="0">
										<TR>
											<TD><asp:label id="Label5" CssClass="StandardLabel" runat="server">Warehouse&nbsp;ID&nbsp;:&nbsp;</asp:label></TD>
											<TD width="100%" colSpan="3"><asp:label id="lblWarehouseID" CssClass="DescInfoLabel" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label6" CssClass="StandardLabel" runat="server">EDS&nbsp;Warehouse&nbsp;#&nbsp;:&nbsp;</asp:label></TD>
											<TD width="100%" colSpan="3"><asp:label id="lblFulfWarehouseID" CssClass="DescInfoLabel" runat="server"></asp:label></TD>
										</TR>
										<tr>
											<td><asp:label id="Label17" CssClass="StandardLabel" runat="server">
													Warehouse&nbsp;Status:&nbsp;
												</asp:label></td>
											<td width="100%" colSpan="3"><asp:label id="lblWarehouseStatusColor" runat="server" Width="5px" BackColor="White" Height="3px"
													BorderWidth="1px" BorderStyle="Solid" BorderColor="Black">
												&nbsp;&nbsp;
												</asp:label>&nbsp;
												<asp:label id="lblWarehouseStatus" CssClass="DescInfoLabel" runat="server">New Warehouse</asp:label></td>
										</tr>
										<TR>
											<TD><asp:label id="Label4" CssClass="StandardLabel" runat="server">Warehouse&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtWarehouseName" CssClass="DescLabel" runat="server" Width="400px" MaxLength="100"></asp:textbox></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="Label2" CssClass="StandardLabel" runat="server">Vendor&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:dropdownlist id="ddlVendor" CssClass="DescLabel" runat="server" DataTextField="vendor_name" DataValueField="vendor_id"></asp:dropdownlist></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="Label1" CssClass="StandardLabel" runat="server">Company&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtCompanyName" CssClass="DescLabel" runat="server" Width="400px" MaxLength="100"></asp:textbox></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="lblFirstName" CssClass="StandardLabel" runat="server">First&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtFirstName" CssClass="DescLabel" runat="server" Width="300px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_FirstName" CssClass="LabelError" runat="server" ErrorMessage="The Contact First Name is required"
																ControlToValidate="txtFirstName">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="lblLastName" CssClass="StandardLabel" runat="server">Last&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtLastName" CssClass="DescLabel" runat="server" Width="300px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_LastName" CssClass="LabelError" runat="server" ErrorMessage="The Contact Last Name is required"
																ControlToValidate="txtLastName">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD vAlign="top"><asp:label id="Label8" CssClass="StandardLabel" runat="server">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtAddressLine1" CssClass="DescLabel" runat="server" Width="400px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_AddrL1" CssClass="LabelError" runat="server" ErrorMessage="The Address Line 1 is required"
																ControlToValidate="txtAddressLine1">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD vAlign="top"><asp:label id="Label9" CssClass="StandardLabel" runat="server">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3"><asp:textbox id="txtAddressLine2" CssClass="DescLabel" runat="server" Width="400px" MaxLength="50"></asp:textbox></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label10" CssClass="StandardLabel" runat="server">City&nbsp;:&nbsp;</asp:label></TD>
											<td>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtCity" CssClass="DescLabel" runat="server" Width="200px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_City" CssClass="LabelError" runat="server" ErrorMessage="The City is required"
																ControlToValidate="txtCity">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</td>
											<TD><asp:label id="Label11" CssClass="StandardLabel" runat="server">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:textbox id="txtCounty" CssClass="DescLabel" runat="server" Width="150px" MaxLength="50"></asp:textbox></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label12" CssClass="StandardLabel" runat="server">State&nbsp;:&nbsp;</asp:label></TD>
											<td>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:dropdownlist id="ddlState" CssClass="DescLabel" runat="server" Width="200px" DataTextField="subdivision_name_1"
																DataValueField="subdivision_code"></asp:dropdownlist></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_State" CssClass="LabelError" runat="server" ErrorMessage="The State is required"
																ControlToValidate="ddlState">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</td>
											<td><asp:label id="Label14" CssClass="StandardLabel" runat="server">&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></td>
											<TD>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtZip" CssClass="DescLabel" runat="server" Width="70px" MaxLength="5"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_Zip" CssClass="LabelError" runat="server" ErrorMessage="The Zip Code is required"
																ControlToValidate="txtZip">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="lblPhoneNumber" CssClass="StandardLabel" runat="server">Phone&nbsp;Number&nbsp;:&nbsp;</asp:label></TD>
											<TD>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtPhoneNumber" CssClass="DescLabel" runat="server" Width="150px" MaxLength="20"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_Phone" CssClass="LabelError" runat="server" ErrorMessage="The Phone Number is required"
																ControlToValidate="txtPhoneNumber">*</asp:requiredfieldvalidator></td>
														<td>
															<asp:RegularExpressionValidator id="RegExpVal_PhoneNumber" runat="server" CssClass="LabelError" ControlToValidate="txtPhoneNumber"
																ErrorMessage="The Phone Number is not entered in a valid format (000-000-0000)." ValidationExpression="\d{3}-\d{3}-\d{4}">*</asp:RegularExpressionValidator>
														</td>
													</tr>
												</table>
											</TD>
											<TD><asp:label id="lblFaxNumber" CssClass="StandardLabel" runat="server">&nbsp;Fax&nbsp;Phone&nbsp;:&nbsp;</asp:label></TD>
											<TD>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtFaxNumber" CssClass="DescLabel" runat="server" Width="150px" MaxLength="20"></asp:textbox></td>
														<td>
															<asp:RegularExpressionValidator id="RegExpVal_Fax" runat="server" CssClass="LabelError" ControlToValidate="txtFaxNumber"
																ErrorMessage="The Fax is not entered in a valid format (000-000-0000)." ValidationExpression="\d{3}-\d{3}-\d{4}">*</asp:RegularExpressionValidator>
														</td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="Label7" CssClass="StandardLabel" runat="server">Receiving&nbsp;Phone&nbsp;#&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtReceivingPhoneNumber" CssClass="DescLabel" runat="server" Width="150px" MaxLength="20"></asp:textbox></td>
														<td>
															<asp:RegularExpressionValidator id="RegExpVal_Receiving" runat="server" CssClass="LabelError" ControlToValidate="txtReceivingPhoneNumber"
																ErrorMessage="The Receiving Phone Number is not entered in a valid format (000-000-0000)." ValidationExpression="\d{3}-\d{3}-\d{4}">*</asp:RegularExpressionValidator>
														</td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="Label13" CssClass="StandardLabel" runat="server">Email&nbsp;Address&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtEmailAddress" CssClass="DescLabel" runat="server" Width="400px" MaxLength="20"></asp:textbox></td>
														<td>
															<asp:RegularExpressionValidator id="RegExpVal_EmailAddress" runat="server" CssClass="LabelError" ControlToValidate="txtEmailAddress"
																ErrorMessage="The Email Address is invalid." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
														</td>
													</tr>
												</table>
											</TD>
										</TR>
										            <TR>
								            <TD><asp:label id="Label15" runat="server" CssClass="StandardLabel">Pick&nbsp;Up:&nbsp;</asp:label></TD>
								            <TD colspan="3"><asp:checkbox id="chkPickUp" runat="server" CssClass="DescInfoLabel" Enabled="False"></asp:checkbox></TD>
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

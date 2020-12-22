<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FormHeaderDetailForm" Codebehind="FormHeaderDetailForm.ascx.cs" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td><br>
		</td>
	</tr>
	<tr>
		<td>
			<TABLE cellSpacing="0" cellPadding="2" border="0">
				<TBODY>
					<TR>
						<TD noWrap width="160"><asp:label id="lblLabelFormID" CssClass="StandardLabel" runat="server">Form ID :</asp:label></TD>
						<TD noWrap width="440"><asp:textbox id="txtFormID" runat="server" Enabled="False" Width="100px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label4" CssClass="StandardLabel" runat="server">Form Group ID :</asp:label></TD>
						<TD><asp:label id="lblFormGroupID" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label5" CssClass="StandardLabel" runat="server">Version :</asp:label></TD>
						<TD><asp:label id="lblVersion" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblLabelFormCode" CssClass="StandardLabel" runat="server">Code :</asp:label></TD>
						<TD>
							<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
								<TR>
									<TD><asp:textbox id="txtFormCode" runat="server" Width="100px"></asp:textbox></TD>
									<TD><asp:requiredfieldvalidator id="reqFldVal_FormCode" CssClass="LabelError" runat="server" ControlToValidate="txtFormCode"
											ErrorMessage="The Code is required.">*</asp:requiredfieldvalidator></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD><asp:label id="Label16" CssClass="StandardLabel" runat="server">Name :</asp:label></TD>
						<TD>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><asp:textbox id="txtName" runat="server" Width="400px"></asp:textbox></td>
									<TD><asp:requiredfieldvalidator id="reqFldVal_Name" CssClass="LabelError" runat="server" ControlToValidate="txtName"
											ErrorMessage="The Name is required.">*</asp:requiredfieldvalidator></TD>
								</tr>
							</table>
						</TD>
					</TR>
					<TR id="trEntityTypeEdit" runat="server">
						<TD><asp:label id="lblLabelEntityTypeID" CssClass="StandardLabel" runat="server">Entity Type :</asp:label></TD>
						<TD><asp:dropdownlist id="ddlEntityType" runat="server" DataValueField="entity_type_id" DataTextField="entity_type_name"></asp:dropdownlist></TD>
					</TR>
					<TR id="trEntityTypeInfo" runat="server">
						<TD><asp:label id="Label3" CssClass="StandardLabel" runat="server">Entity Type :</asp:label></TD>
						<TD><asp:label id="lblEntityType" runat="server" CssClass="DescInfoLabel">Account</asp:label></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblLabelProgTypeID" CssClass="StandardLabel" runat="server">Program Type :</asp:label></TD>
						<TD><asp:dropdownlist id="ddlProgType" runat="server" DataValueField="program_type_id" DataTextField="program_type_name"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label7" CssClass="StandardLabel" runat="server">Program :</asp:label></TD>
						<TD><asp:dropdownlist id="ddlProgram" runat="server" DataValueField="program_id" DataTextField="program_name"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblLabelTaxPostalTypeID" CssClass="StandardLabel" runat="server">Tax Postal Type :</asp:label></TD>
						<TD><asp:dropdownlist id="ddlTaxPostalType" runat="server" DataValueField="postal_address_type_id" DataTextField="postal_address_type_name"></asp:dropdownlist></TD>
					</TR>
					<TR id="trParentForm" runat="server">
						<TD><asp:label id="Label2" CssClass="StandardLabel" runat="server">Parent Form:</asp:label></TD>
						<TD><asp:dropdownlist id="ddlParentForm" runat="server" DataValueField="form_id" DataTextField="form_name"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:label id="lblLabelDescription" CssClass="StandardLabel" runat="server">Description :</asp:label></TD>
						<TD><asp:textbox id="txtDescription" CssClass="DescLabel" runat="server" Width="400px" MaxLength="2000"
								Rows="4" TextMode="MultiLine"></asp:textbox></TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:label id="lblProgramBasics" CssClass="StandardLabel" runat="server">Prog. basics info :</asp:label></TD>
						<TD><asp:textbox id="txtProgramBasics" CssClass="DescLabel" runat="server" Width="400px" MaxLength="2000"
								Rows="4" TextMode="MultiLine"></asp:textbox></TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:label id="lblOrderTerms" CssClass="StandardLabel" runat="server">Order Terms info :</asp:label></TD>
						<TD><asp:textbox id="txtOrderTerms" CssClass="DescLabel" runat="server" Width="400px" MaxLength="2000"
								Rows="4" TextMode="MultiLine"></asp:textbox></TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:label id="lblClosingTime" CssClass="StandardLabel" runat="server">Closing time :</asp:label></TD>
						<TD>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><asp:textbox id="txtClosingTime" runat="server" Width="100px" MaxLength="5"></asp:textbox></td>
									<TD><asp:requiredfieldvalidator id="reqFldVal_ClosingTime" CssClass="LabelError" runat="server" ControlToValidate="txtClosingTime"
											ErrorMessage="The Closing time is required.">*</asp:requiredfieldvalidator></TD>
								</tr>
							</table>
						</TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:label id="lblIsProductPriceUpdatable" CssClass="StandardLabel" runat="server">Price is Updatable :</asp:label></TD>
						<TD><asp:checkbox id="chkBoxIsProductPriceUpdatable" runat="server"></asp:checkbox></TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:label id="Label6" CssClass="StandardLabel" runat="server">Pro Code is Allowed :</asp:label></TD>
						<TD><asp:checkbox id="chkBoxIsQtyAdjustmentAllowed" runat="server"></asp:checkbox></TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:label id="Label1" CssClass="StandardLabel" runat="server">Image URL :</asp:label></TD>
						<TD><asp:textbox id="txtImageURL" runat="server" Width="400px" MaxLength="100"></asp:textbox></TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:label id="lblIsBaseForm" CssClass="StandardLabel" runat="server">Is Base Form :</asp:label></TD>
						<TD><asp:checkbox id="chkBoxIsBaseForm" Enabled="False" runat="server"></asp:checkbox></TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:label id="lblEnabled" CssClass="StandardLabel" runat="server">Enabled :</asp:label></TD>
						<TD><asp:checkbox id="chkBoxEnabled" runat="server"></asp:checkbox></TD>
					</TR>
				</TBODY>
			</TABLE>
		</td>
	</tr>
</table>

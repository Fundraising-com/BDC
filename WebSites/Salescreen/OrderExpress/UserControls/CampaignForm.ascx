<%@ Reference Control="~/UserControls/AccountDetail.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CampaignForm" Codebehind="CampaignForm.ascx.cs" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td><br>
		</td>
	</tr>
	<tr>
		<td>
			<TABLE cellSpacing="0" cellPadding="2" border="0">
				<TR>
					<TD noWrap width="160">
						<asp:label id="lblLabelCampID" runat="server" CssClass="StandardLabel">Campaign ID :</asp:label></TD>
					<TD noWrap width="440">
						<asp:TextBox id="txtCampID" runat="server" Width="100px" Enabled="False"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblLabelTypeID" runat="server" CssClass="StandardLabel">Type :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD>
									<asp:DropDownList id="ddlType" runat="server" DataValueField="program_type_id" DataTextField="program_type_name"></asp:DropDownList>
								</TD>
								<TD width="100%">&nbsp;
								</TD>
								<TD>
									<asp:label id="Label28" runat="server" CssClass="StandardLabel">Fiscal&nbsp;Year&nbsp;:&nbsp;</asp:label></TD>
								<TD>
									<asp:TextBox id="txtFiscalYear" runat="server" Width="100px"></asp:TextBox>
								</TD>
								<TD>
									<asp:RequiredFieldValidator id="reqFldVal_FiscalYear" CssClass="LabelError" runat="server" ErrorMessage="The Fiscal Year is required."
										ControlToValidate="txtFiscalYear">*</asp:RequiredFieldValidator>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label16" runat="server" CssClass="StandardLabel">Name :</asp:label></TD>
					<TD>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:TextBox id="txtName" runat="server" Width="400px"></asp:TextBox>
								</td>
								<TD>
									<asp:RequiredFieldValidator id="reqFldVal_Name" CssClass="LabelError" runat="server" ErrorMessage="The Name is required."
										ControlToValidate="txtName">*</asp:RequiredFieldValidator>
								</TD>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label27" runat="server" CssClass="StandardLabel">Start Date :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD>
									<asp:TextBox id="txtStartDate" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
								</TD>
								<TD>
									<asp:RequiredFieldValidator id="reqFldVal_StartDate" CssClass="LabelError" runat="server" ErrorMessage="The Start Date is required."
										ControlToValidate="txtStartDate">*</asp:RequiredFieldValidator>
								</TD>
								<td>
									<asp:CompareValidator id="CompVal_StartDate" runat="server" CssClass="LabelError" ErrorMessage="The Start Date is invalid."
										ControlToValidate="txtStartDate" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
								</td>
								<TD width="100%">&nbsp;
								</TD>
								<TD>
									<asp:label id="Label29" runat="server" CssClass="StandardLabel">End&nbsp;Date&nbsp;:&nbsp;</asp:label></TD>
								<TD>
									<asp:TextBox id="txtEndDate" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
								</TD>
								<TD>
									<asp:RequiredFieldValidator id="reqFldVal_EndDate" CssClass="LabelError" runat="server" ErrorMessage="The End Date is required."
										ControlToValidate="txtEndDate">*</asp:RequiredFieldValidator>
								</TD>
								<td>
									<asp:CompareValidator id="CompVal_EndDate" runat="server" CssClass="LabelError" ErrorMessage="The End Date is invalid."
										ControlToValidate="txtEndDate" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label7" runat="server" CssClass="StandardLabel">Account :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD>
									<asp:TextBox id="txtAccountID" ReadOnly="True" runat="server" Width="50px" Enabled="True"></asp:TextBox>
								<TD>
									<asp:CompareValidator id="CompValAccID" runat="server" CssClass="LabelError" ErrorMessage="The account ID is invalid (must be a number)."
										ControlToValidate="txtAccountID" Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator></TD>
								<TD>&nbsp;
									<asp:TextBox id="txtFULFAccountID" runat="server" ReadOnly="True" Width="230px" Enabled="True"></asp:TextBox></TD>
								<TD align="right">
									<asp:ImageButton id="imgBtnDetailAcc" runat="server" ImageUrl="~/images/BtnDetail.gif" CausesValidation="False"></asp:ImageButton></TD>
								<TD align="right">
									<asp:ImageButton id="imgBtnSelectAcc" runat="server" ImageUrl="~/images/BtnSelect.gif" CausesValidation="False"></asp:ImageButton></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblLabelFM" runat="server" CssClass="StandardLabel">Field Sales Manager :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD>
									<asp:TextBox id="txtFMID" ReadOnly="True" runat="server" Width="50px" Enabled="True"></asp:TextBox>
								<TD>
									<asp:CompareValidator id="CompValFMID" runat="server" CssClass="LabelError" ErrorMessage="The FM ID is invalid (must be a number)."
										ControlToValidate="txtFMID" Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator></TD>
								<TD>&nbsp;
									<asp:TextBox id="txtFMName" runat="server" ReadOnly="True" Width="230px" Enabled="True"></asp:TextBox></TD>
								<TD align="right">
									<asp:ImageButton id="imgBtnDetail" runat="server" ImageUrl="~/images/BtnDetail.gif" CausesValidation="False"></asp:ImageButton></TD>
								<TD align="right">
									<asp:ImageButton id="imgBtnSelect" runat="server" ImageUrl="~/images/BtnSelect.gif" CausesValidation="False"></asp:ImageButton></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblLabelTaxExemptionNumber" runat="server" CssClass="StandardLabel">Tax Exemption Number :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD>
									<asp:TextBox id="txtTaxExemptionNumber" runat="server" MaxLength="25" Width="200px"></asp:TextBox></TD>
								<TD width="100%">&nbsp;
								</TD>
								<TD>
									<asp:label id="Label23" runat="server" CssClass="StandardLabel">Expire&nbsp;:&nbsp;</asp:label></TD>
								<TD>
									<asp:TextBox id="txtTaxExemptionExpirationDate" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
								</TD>
								<td>
									<asp:CompareValidator id="compVal_TaxExemptionExpirationDate" runat="server" CssClass="LabelError" ErrorMessage="The Tax Exemption Expiration Date is invalid."
										ControlToValidate="txtTaxExemptionExpirationDate" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label30" runat="server" CssClass="StandardLabel">Warehouse :</asp:Label></TD>
					<TD>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:TextBox id="txtWarehouse" runat="server" Width="100px"></asp:TextBox>
								</td>
								<td>
									<asp:CompareValidator id="compVal_Warehouse" runat="server" CssClass="LabelError" ErrorMessage="The Warehouse is invalid."
										ControlToValidate="txtWarehouse" Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr>
					<TD>
						<asp:Label id="Label2" runat="server" CssClass="StandardLabel">Gross Estimated Amount :</asp:Label>
					</TD>
					<TD>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:textbox id="txtEstimatedAmount" runat="server" Width="100px"></asp:textbox>
								</td>
								<td>
									<asp:comparevalidator id="compVal_EstimatedAmount" runat="server" CssClass="LabelError" ErrorMessage="The Estimated Amount is invalid."
										ControlToValidate="txtEstimatedAmount" Operator="DataTypeCheck" Type="Currency">*</asp:comparevalidator>
								</td>
								<TD>
									<asp:Label id="Label1" runat="server" CssClass="StandardLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Enrollment&nbsp;:&nbsp;</asp:Label>
								</TD>
								<td>
									<asp:TextBox id="txtEnrollment" runat="server" Width="100px"></asp:TextBox>
								</td>
								<td>
									<asp:CompareValidator id="compVal_Enrollment" runat="server" CssClass="LabelError" ErrorMessage="The Enrollment is invalid."
										ControlToValidate="txtEnrollment" Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
								</td>
							</tr>
						</table>
					</TD>
				</tr>
				<TR>
					<TD valign="top">
						<asp:label id="lblLabelComments" runat="server" CssClass="StandardLabel">Comments :</asp:label></TD>
					<TD>
						<asp:TextBox id="txtComments" runat="server" MaxLength="4000" TextMode="MultiLine" Rows="4" Width="400px"></asp:TextBox></TD>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>

<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrganizationHeaderDetailForm" Codebehind="OrganizationHeaderDetailForm.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="DualAddressForm" Src="DualAddressForm.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="center">
			<table id="Table1e" cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<td class="SectionPageTitleInfo"><asp:label id="lblTitleOrganizationInfo" runat="server">
							Organization Information
						</asp:label></td>
				</tr>
				<tr id="trValSumOrganizationInfo" runat="server" visible="false">
					<td><asp:validationsummary id="ValSumOrganizationInfo" runat="server" CssClass="LabelError" HeaderText="Correct the following error to proceed."></asp:validationsummary></td>
				</tr>
				<tr>
					<td align="left"> <!--Section Body -->
						<table id="tblOrganizationInfo" cellSpacing="0" cellPadding="0" width="100%" border="0"
							runat="server">
							<tr>
								<td><asp:label id="Label2" runat="server" CssClass="StandardLabel">QSP&nbsp;Organization&nbsp;ID&nbsp;#&nbsp;:&nbsp;
									</asp:label></td>
								<td></td>
								<td width="100%"><asp:label id="lblOrgID" runat="server" CssClass="DescInfoLabel"></asp:label></td>
							</tr>
							<tr>
								<td><asp:label id="Label9" runat="server" CssClass="StandardLabel">
										Organization&nbsp;Name:&nbsp;<span class="RequiredSymbolLabel">*</span>
									</asp:label></td>
								<td></td>
								<td width="100%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:textbox id="txtOrganizationName" runat="server" CssClass="DescLabel" Width="400px"></asp:textbox></td>
											<td><asp:requiredfieldvalidator id="ReqFldVal_OrganizationName" runat="server" CssClass="LabelError" ErrorMessage="The Organization Name is required."
													ControlToValidate="txtOrganizationName">*</asp:requiredfieldvalidator></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td vAlign="top"><asp:label id="Label20" runat="server" CssClass="StandardLabel">
										Organization&nbsp;Type:&nbsp;<span class="RequiredSymbolLabel">*</span>
									</asp:label></td>
								<td></td>
								<td vAlign="top" width="100%">
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD>
												<TABLE cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD><asp:dropdownlist id="ddlOrgType" runat="server" CssClass="DescLabel" DataValueField="organization_type_id"
																DataTextField="organization_type_name">
																<asp:ListItem Value="1" Selected="True">Public</asp:ListItem>
																<asp:ListItem Value="2">Catholic</asp:ListItem>
																<asp:ListItem Value="3">Christian</asp:ListItem>
																<asp:ListItem Value="4">Other</asp:ListItem>
																<asp:ListItem Value="5">Campus</asp:ListItem>
															</asp:dropdownlist></TD>
														<td><asp:requiredfieldvalidator id="ReqFldVal_OrgType" runat="server" CssClass="LabelError" ErrorMessage="The Organization Type is required."
																ControlToValidate="ddlOrgType">*</asp:requiredfieldvalidator></td>
													</TR>
												</TABLE>
											</TD>
											<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
											<td vAlign="top"><asp:label id="Label19" runat="server" CssClass="StandardLabel">
													Organization&nbsp;Level:&nbsp;<span class="RequiredSymbolLabel">*</span>
												</asp:label></td>
											<td>&nbsp;</td>
											<td width="100%">
												<TABLE cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD><asp:dropdownlist id="ddlOrgLevel" runat="server" CssClass="DescLabel" DataValueField="organization_level_id"
																DataTextField="organization_level_name">
																<asp:ListItem Value="1">Elementary</asp:ListItem>
																<asp:ListItem Value="2">Middle</asp:ListItem>
																<asp:ListItem Value="3">High</asp:ListItem>
																<asp:ListItem Value="4" Selected="True">Other</asp:ListItem>
																<asp:ListItem Value="5">Baseball</asp:ListItem>
															</asp:dropdownlist></TD>
														<td><asp:requiredfieldvalidator id="ReqFldVal_OrgLevel" runat="server" CssClass="LabelError" ErrorMessage="The Organization Level is required."
																ControlToValidate="ddlOrgLevel">*</asp:requiredfieldvalidator></td>
													</TR>
												</TABLE>
												
											</td>
										</TR>
									</TABLE>
								</td>
							</tr>
							<tr id="trMDRInfo" runat="server">
								<td><asp:label id="Label1" runat="server" CssClass="StandardLabel">
										MDR PID :&nbsp;
									</asp:label></td>
								<td></td>
								<td width="100%"><asp:label id="lblMDRPID" runat="server" CssClass="DescInfoLabel"></asp:label></td>
							</tr>
							<TR id="trMDREdit" runat="server">
								<TD><asp:label id="lblLabelMDRPID" runat="server" CssClass="StandardLabel">MDR PID :</asp:label></TD>
								<td></td>
								<TD>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><asp:textbox id="txtMDRPID" runat="server" Width="100px"></asp:textbox></TD>
											<TD><asp:comparevalidator id="compValMDRPID" runat="server" CssClass="LabelError" ErrorMessage="The MDR PID is invalid (must be a number)."
													ControlToValidate="txtMDRPID" Operator="DataTypeCheck" Type="Integer">*</asp:comparevalidator></TD>
											<TD align="right"><asp:imagebutton id="imgBtnSelectMDR" runat="server" CausesValidation="False" ImageUrl="~/images/BtnSelect.gif"></asp:imagebutton></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<tr>
								<td><asp:label id="Label6" runat="server" CssClass="StandardLabel">
										Tax&nbsp;Exemption&nbsp;#&nbsp;:&nbsp;
									</asp:label></td>
								<td></td>
								<td colSpan="4">
									<TABLE id="tblTaxInfoEdit" cellSpacing="0" cellPadding="0" width="400" border="0" runat="server">
										<TR>
											<TD><asp:textbox id="txtTaxExemptionNumber" runat="server" CssClass="DescLabel" Width="200px" MaxLength="25"></asp:textbox></TD>
											<TD width="100%">&nbsp;
											</TD>
											<TD><asp:label id="Label4" runat="server" CssClass="StandardLabel">Expire&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:textbox id="txtTaxExemptionExpirationDate" runat="server" Font-Size="9px" Height="20px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:textbox>
													<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTaxExemptionExpirationDate"
                                                                                     Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
													</TD>
											<td><asp:hyperlink id="hypLnkTaxExemptionExpirationDate" runat="server" ImageUrl="~/images/Calendar.gif"
													ToolTip="Click here to select the date from a popup calendar !" NavigateUrl="javascript:void(0);">HyperLink</asp:hyperlink>&nbsp;
											</td>
											<td><asp:comparevalidator id="compVal_TaxExemptionExpirationDate" runat="server" CssClass="LabelError" ErrorMessage="The Tax Exemption Expiration Date is invalid."
													ControlToValidate="txtTaxExemptionExpirationDate" Operator="DataTypeCheck" Type="Date">*</asp:comparevalidator></td>
										</TR>
									</TABLE>
									<TABLE id="tblTaxInfoReadOnly" cellSpacing="0" cellPadding="0" width="400" border="0" runat="server">
										<TR>
											<TD><asp:label id="lblTaxExemptionNumber" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
											<TD width="100">&nbsp;&nbsp;&nbsp;
											</TD>
											<TD width="1"><asp:label id="Label5" runat="server" CssClass="StandardLabel">Expire&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:label id="lblTaxExemptionExpirationDate" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td vAlign="top"><asp:label id="Label12ee" runat="server" CssClass="StandardLabel">
										Note&nbsp;:&nbsp;
									</asp:label></td>
								<td></td>
								<td colSpan="4"><asp:textbox id="txtComment" runat="server" CssClass="DescLabel" Width="95%" TextMode="MultiLine"
										Rows="4"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
						<table id="Table244" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td colSpan="5">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td vAlign="top"><asp:label id="Label21" runat="server" CssClass="NoteLabel">
												Note:&nbsp;
											</asp:label></td>
											<td><asp:label id="Label7" runat="server" CssClass="NoteLabel">
												Exemption or resale certificate forms required with order.  Based on state laws, Invoice will include taxes unless account is tax exempt.
											</asp:label></td>
										</tr>
										<tr>
											<td vAlign="top"><asp:label id="Label22" runat="server" CssClass="RequiredSymbolLabel">
												*&nbsp;
											</asp:label></td>
											<td><asp:label id="Label25" runat="server" CssClass="RequiredSymbolLabel" Font-Size="x-small">
												Required Field
											</asp:label></td>
										</tr>
									</table>
									<br>
									<br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><br>
		</td>
	</tr>
	<tr>
		<td>
		    <uc1:DualAddressForm ID="DualAddressFormControl" runat="server" HygieneAddress="true" />
			<br>
		</td>
	</tr>
</table>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CatalogHeaderDetailForm" Codebehind="CatalogHeaderDetailForm.ascx.cs" %>

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
						<TD noWrap width="160"><asp:label id="lblLabelCatalogID" runat="server" CssClass="StandardLabel">Catalog ID :</asp:label></TD>
						<TD noWrap width="440"><asp:textbox id="txtCatalogID" runat="server" Width="100px" Enabled="False"></asp:textbox></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblLabelCatalogCode" runat="server" CssClass="StandardLabel">Code :&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:label></TD>
						<TD>
							<TABLE cellSpacing="0" cellPadding="0" width="120" border="0">
								<TR>
									<TD><asp:textbox id="txtCatalogCode" runat="server" Width="100px"></asp:textbox></TD>
									<TD><asp:requiredfieldvalidator id="reqFldVal_CatalogCode" runat="server" CssClass="LabelError" ErrorMessage="The Code is required."
											ControlToValidate="txtCatalogCode">*</asp:requiredfieldvalidator></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD><asp:label id="Label16" runat="server" CssClass="StandardLabel">Name&nbsp;:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:label></TD>
						<TD>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><asp:textbox id="txtName" runat="server" Width="400px"></asp:textbox></td>
									<TD><asp:requiredfieldvalidator id="reqFldVal_Name" runat="server" CssClass="LabelError" ErrorMessage="The Name is required."
											ControlToValidate="txtName">*</asp:requiredfieldvalidator></TD>
								</tr>
							</table>
						</TD>
					</TR>
					<TR>
						<TD><asp:label id="lblCulture" runat="server" CssClass="StandardLabel">Culture&nbsp;:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:label></TD>
						<TD>
							<asp:dropdownlist id="ddlCulture" runat="server"></asp:dropdownlist>
						</TD>
					</TR>
					<TR>
						<TD><asp:label id="Label3" runat="server" CssClass="StandardLabel">Start&nbsp;Date&nbsp;:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:label></TD>
						<TD>
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD><asp:textbox id="txtStartDate" runat="server" Width="100px" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma"></asp:textbox>
									<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
									</TD>
									<td><asp:hyperlink id="hypLnkStartDate" runat="server" ImageUrl="~/images/Calendar.gif" NavigateUrl="javascript:void(0);"
											ToolTip="Click here to select the start date from a popup calendar !">HyperLink</asp:hyperlink>&nbsp;
									</td>
									<td><asp:requiredfieldvalidator id="ReqFldVal_StartDate" runat="server" CssClass="LabelError" ControlToValidate="txtStartDate"
											ErrorMessage="The Program Start Date is required.">*</asp:requiredfieldvalidator></td>
									<td><asp:comparevalidator id="compVal_StartDate" runat="server" CssClass="LabelError" ControlToValidate="txtStartDate"
											ErrorMessage="The Program Start Date is invalid." Type="Date" Operator="DataTypeCheck">*</asp:comparevalidator></td>
									<TD>&nbsp;</TD>
									<TD><asp:label id="Label29" runat="server" CssClass="StandardLabel">End&nbsp;Date:&nbsp;<span class="RequiredSymbolLabel">*</span>&nbsp;</asp:label></TD>
									<TD><asp:textbox id="txtEndDate" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:textbox>
									<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEndDate"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
									</TD>
									<td><asp:hyperlink id="hypLnkEndDate" runat="server" ImageUrl="~/images/Calendar.gif" NavigateUrl="javascript:void(0);"
											ToolTip="Click here to select the end date from a popup calendar !">HyperLink</asp:hyperlink>&nbsp;
									</td>
									<td><asp:requiredfieldvalidator id="ReqFldVal_EndDate" runat="server" CssClass="LabelError" ControlToValidate="txtEndDate"
											ErrorMessage="The Program End Date is required.">*</asp:requiredfieldvalidator></td>
									<td><asp:comparevalidator id="compVal_EndDate" runat="server" CssClass="LabelError" ControlToValidate="txtEndDate"
											ErrorMessage="The Program End Date is invalid." Type="Date" Operator="DataTypeCheck">*</asp:comparevalidator></td>
									<td><asp:comparevalidator id="compValProgDate" runat="server" CssClass="LabelError" ControlToValidate="txtStartDate"
											ErrorMessage="The End Date must be greater than the Start Date." Type="Date" Operator="LessThan" ControlToCompare="txtEndDate">*</asp:comparevalidator></td>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:label id="lblLabelDescription" runat="server" CssClass="StandardLabel">Description :</asp:label></TD>
						<TD><asp:textbox id="txtDescription" runat="server" Width="400px" CssClass="DescLabel" TextMode="MultiLine"
								Rows="4" MaxLength="2000"></asp:textbox></TD>
					</TR>
					<tr>
						<td colspan="2">
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td vAlign="top">
										<asp:label id="Label18" runat="server" CssClass="RequiredSymbolLabel">
											*&nbsp;
										</asp:label>
									</td>
									<td>
										<asp:label id="Label24" runat="server" Font-Size="x-small" CssClass="RequiredSymbolLabel">
											Required Field
										</asp:label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</TBODY>
			</TABLE>
		</td>
	</tr>
</table>

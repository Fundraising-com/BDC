<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AddressControlForm" Codebehind="AddressControlForm.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AddressHygiene" Src="~/UserControls/AddressHygiene.ascx" %>

<uc1:AddressHygiene ID="AddressHygieneControl" runat="server" EnableSuggestionList="true"></uc1:AddressHygiene>
<table id="htmlTblAccountAddressForm" runat="server" cellSpacing="0" cellPadding="2" width="380"
	border="0" class="DescTableInfo">
	<tr>
		<td>
			<TABLE cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR class="ItemStyle" align="left">
					<TD>
						<asp:label id="lblOrgName" CssClass="OrderAddressStandardLabel" runat="server">Org.&nbsp;Name</asp:label><span class="OrderAddressStandardLabel">:&nbsp;</span>
						<span id="OrgNameRequiredIndicatorLabel" runat="server" class="RequiredSymbolLabel" visible="false">*</span>
					</TD>
					<TD colspan="3">
					    <table border="0" cellpadding="0" cellspacing="0">
					        <tr>
					            <td>
					                <asp:TextBox id="txtName" CssClass="OrderAddressDescLabel" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
					            </td>
					            <td>
					                <asp:RequiredFieldValidator id="ReqFldVal_OrgName" CssClass="LabelError" runat="server" ErrorMessage="The Org. Name is required."
										ControlToValidate="txtName" Visible="false" Enabled="false">*</asp:RequiredFieldValidator>
					            </td>
					        </tr>
					    </table>
					</TD>
				</TR>
				<TR class="ItemStyle" align="left">
					<TD>
						<asp:label id="lblFirstName" CssClass="OrderAddressStandardLabel" runat="server">First&nbsp;Name:&nbsp;<span class="RequiredSymbolLabel">
								*</span></asp:label>
					</TD>
					<TD colspan="3">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:TextBox id="txtFirstName" CssClass="OrderAddressDescLabel" runat="server" Width="250px"
										MaxLength="50"></asp:TextBox>
								</td>
								<td>
									<asp:RequiredFieldValidator id="ReqFldVal_FirstName" CssClass="LabelError" runat="server" ErrorMessage="The Contact First Name is required"
										ControlToValidate="txtFirstName">*</asp:RequiredFieldValidator>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR class="AlternatingItemStyle" align="left">
					<TD>
						<asp:label id="lblLastName" CssClass="OrderAddressStandardLabel" runat="server">Last&nbsp;Name:&nbsp;<span class="RequiredSymbolLabel">
								*</span></asp:label>
					</TD>
					<TD colspan="3">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:TextBox id="txtLastName" CssClass="OrderAddressDescLabel" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
								</td>
								<td>
									<asp:RequiredFieldValidator id="ReqFldVal_LastName" CssClass="LabelError" runat="server" ErrorMessage="The Contact Last Name is required"
										ControlToValidate="txtLastName">*</asp:RequiredFieldValidator>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR class="ItemStyle" align="left">
					<TD >
						<asp:label id="lblAddressLine1" CssClass="OrderAddressStandardLabel" runat="server">Address&nbsp;Line&nbsp;1:<span class="RequiredSymbolLabel">
								*</span></asp:label>
					</TD>
					<TD colspan="3">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:TextBox id="txtAddressLine1" CssClass="OrderAddressDescLabel" runat="server" Width="250px"
										MaxLength="50"></asp:TextBox>
								</td>
								<td>
									<asp:RequiredFieldValidator id="ReqFldVal_AddrL1" CssClass="LabelError" runat="server" ErrorMessage="The Address Line 1 is required"
										ControlToValidate="txtAddressLine1">*</asp:RequiredFieldValidator>
								</td>
								<td>
									<asp:CustomValidator id="CustVal_AddrL1" runat="server" CssClass="LabelError" ErrorMessage="Postal Box Address is not allowed"
										ControlToValidate="txtAddressLine1">*</asp:CustomValidator>
								    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ControlToValidate="txtAddressLine1" CssClass="LabelError" Display="Dynamic" 
                                        ErrorMessage="Asterisks (*) is not allowed." ValidationExpression="[^\*]*">*</asp:RegularExpressionValidator>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR class="AlternatingItemStyle" align="left">
					<TD >
						<asp:label id="lblAddressLine2" CssClass="OrderAddressStandardLabel" runat="server">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label>
					</TD>
					<TD colspan="3">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:TextBox id="txtAddressLine2" CssClass="OrderAddressDescLabel" runat="server" Width="250px"
										MaxLength="50"></asp:TextBox>
								</td>
								<td>
									<asp:CustomValidator id="CustVal_AddrL2" runat="server" CssClass="LabelError" ErrorMessage="Postal Box Address is not allowed"
										ControlToValidate="txtAddressLine2">*</asp:CustomValidator>
								    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                        ControlToValidate="txtAddressLine2" CssClass="LabelError" Display="Dynamic" 
                                        ErrorMessage="Asterisks (*) is not allowed." ValidationExpression="[^\*]*">*</asp:RegularExpressionValidator>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR class="ItemStyle" align="left">
					<TD>
						<asp:label id="lblCity" CssClass="OrderAddressStandardLabel" runat="server">City:&nbsp;<span class="RequiredSymbolLabel">
								*</span></asp:label>
					</TD>
					<TD>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td>
												<asp:TextBox id="txtCity" CssClass="OrderAddressDescLabel" runat="server" Width="120px" MaxLength="50"></asp:TextBox>
											</td>
											<td>
												<asp:RequiredFieldValidator id="ReqFldVal_City" CssClass="LabelError" runat="server" ErrorMessage="The City is required"
													ControlToValidate="txtCity">*</asp:RequiredFieldValidator>
											    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                    ControlToValidate="txtCity" CssClass="LabelError" Display="Dynamic" 
                                                    ErrorMessage="Asterisks (*) is not allowed." ValidationExpression="[^\*]*">*</asp:RegularExpressionValidator>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</TD>
					<TD>
						<asp:label id="lblCounty" CssClass="OrderAddressStandardLabel" runat="server">County&nbsp;:&nbsp;</asp:label>
					</TD>
					<TD >
						<asp:TextBox id="txtCounty" CssClass="OrderAddressDescLabel" runat="server" Width="65px" MaxLength="50"></asp:TextBox>
					    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                            ControlToValidate="txtCounty" CssClass="LabelError" Display="Dynamic" 
                            ErrorMessage="Asterisks (*) is not allowed." ValidationExpression="[^\*]*">*</asp:RegularExpressionValidator>
					</TD>
				</TR>
				<TR class="AlternatingItemStyle" align="left">
					<TD valign="bottom">
						<asp:label id="lblState" CssClass="OrderAddressStandardLabel" runat="server">State:&nbsp;<span class="RequiredSymbolLabel">
								*</span></asp:label>
					</TD>
					<TD valign="bottom">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td>
												<asp:DropDownList id="ddlState" CssClass="OrderAddressDescLabel" runat="server" Width="90px" DataTextField="subdivision_name_1"
													DataValueField="subdivision_code" ondatabinding="ddlState_DataBinding"></asp:DropDownList>
											</td>
											<td>
												<asp:RequiredFieldValidator id="ReqFldVal_State" CssClass="LabelError" runat="server" ErrorMessage="The State is required"
													ControlToValidate="ddlState">*</asp:RequiredFieldValidator>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</TD>
					<td valign="bottom">
						<asp:label id="lblZip" CssClass="OrderAddressStandardLabel" runat="server">Zip:&nbsp;<span class="RequiredSymbolLabel">
								*</span></asp:label></td>
					<TD valign="bottom">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:TextBox id="txtZip" runat="server" CssClass="OrderAddressDescLabel" Width="65px" MaxLength="10"></asp:TextBox>
								</td>
								<td>
									<asp:RequiredFieldValidator id="ReqFldVal_Zip" CssClass="LabelError" runat="server" ErrorMessage="The Zip Code is required"
										ControlToValidate="txtZip">*</asp:RequiredFieldValidator>
								</td>
								<td>
									<asp:RegularExpressionValidator id="ReqExpVal_Zip" runat="server" CssClass="LabelError" ControlToValidate="txtZip"
										ErrorMessage="The Zip Code is not entered in a valid format (00000 or 00000-0000) ." ValidationExpression="\d{5}(-\d{4})?">*</asp:RegularExpressionValidator>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR class="ItemStyle" align="left">
					<TD valign="bottom">
						<asp:label id="lblPhoneNumber" CssClass="OrderAddressStandardLabel" Width="120px" runat="server">
							Phone&nbsp;Number:&nbsp;<span class="RequiredSymbolLabel">*</span>
						</asp:label>
					</TD>
					<TD valign="bottom">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td colspan="3">
												<span class="NoteLabel">000-000-0000</span>
											</td>
										</tr>
										<tr>
											<td>
												<asp:TextBox id="txtPhoneNumber" runat="server" CssClass="OrderAddressDescLabel" Width="90px"
													MaxLength="12"></asp:TextBox>
											</td>
											<td>
												<asp:RequiredFieldValidator id="ReqFldVal_Phone" CssClass="LabelError" runat="server" ErrorMessage="The Phone Number is required"
													ControlToValidate="txtPhoneNumber">*</asp:RequiredFieldValidator>
											</td>
											<td>
												<asp:RegularExpressionValidator id="RegExpPhoneNumber" runat="server" CssClass="LabelError" ControlToValidate="txtPhoneNumber"
													ErrorMessage="The Phone number is not entered in a valid format (000-000-0000)." ValidationExpression="\d{3}-\d{3}-\d{4}">*</asp:RegularExpressionValidator>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</TD>
					<TD valign="bottom" colspan="2">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
								</td>
								<td colspan="3">
									<span class="NoteLabel">000-000-0000</span>
								</td>
							</tr>
							<tr>
								<td>
									<asp:label id="lblFaxNumber" CssClass="OrderAddressStandardLabel" runat="server">Fax&nbsp;:&nbsp;</asp:label>
								</td>
								<td>
									<asp:TextBox id="txtFaxNumber" runat="server" CssClass="OrderAddressDescLabel" Width="85px" MaxLength="12"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="RegExpFax" runat="server" CssClass="LabelError" ControlToValidate="txtFaxNumber"
										ErrorMessage="The Fax is not entered in a valid format (000-000-0000)." ValidationExpression="\d{3}-\d{3}-\d{4}">*</asp:RegularExpressionValidator>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr class="AlternatingItemStyle" align="left">
					<td>
						<asp:label id="lblEmailAddress" CssClass="OrderAddressStandardLabel" runat="server">Email&nbsp;Address&nbsp;:&nbsp;</asp:label>
					</td>
					<td colspan="3">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:TextBox id="txtEmailAddress" runat="server" CssClass="OrderAddressDescLabel" Width="250px"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="RegExpVal_EmailAddress" runat="server" CssClass="LabelError" ControlToValidate="txtEmailAddress"
										ErrorMessage="The Email Address is invalid." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="ItemStyle" align="left">
					<td>
						<asp:label id="Label1" CssClass="OrderAddressStandardLabel" runat="server">Residential&nbsp;Area&nbsp;:&nbsp;</asp:label>
					</td>
					<td colspan="3">
						<asp:CheckBox id="chkBoxResidentialArea" runat="server" CssClass="OrderAddressStandardLabel"></asp:CheckBox>
					</td>
				</tr>
			</TABLE>
		</td>
	</tr>
	<tr>
		<td align="center"></td>
	</tr>
</table>
<!--<input type="hidden" runat="server" id="hidFieldID">-->

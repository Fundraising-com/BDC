<%@ Register TagPrefix="ccval" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CreditApplicationForm" Codebehind="CreditApplicationForm.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td bgColor="#336699"><asp:label id="lblTitleAccountInfo" ForeColor="White" runat="server" CssClass="StandardLabel">
				Organization information
			</asp:label></td>
	</tr>
	<tr>
		<td>
			<hr width="100%" SIZE="2">
		</td>
	</tr>
	<tr id="trValSumAccountInfo" runat="server" visible="false">
		<td><asp:validationsummary id="ValSumAccountInfo" runat="server" CssClass="LabelError" HeaderText="Correct the following error to proceed."></asp:validationsummary></td>
	</tr>
	<tr>
		<td>
			<TABLE id="tblAccountInfo" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<TR>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td>
						<TABLE cellSpacing="0" cellPadding="1" width="500" border="0">
							<TR>
								<TD><asp:label id="Label15" runat="server" CssClass="StandardLabel">Account&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD colSpan="3"><asp:label id="lblAccountName" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label29" runat="server" CssClass="StandardLabel">Officer&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD width="100%" colSpan="3">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:textbox id="txtOfficerName" runat="server" Width="200px"></asp:textbox></td>
											<td><asp:requiredfieldvalidator id="reqFld_OfficerName" runat="server" CssClass="LabelError" ControlToValidate="txtOfficerName"
													ErrorMessage="The Officer Name is required">*</asp:requiredfieldvalidator></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD><asp:label id="Label1" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
								<TD colSpan="3"><asp:label id="lblAddressLine1" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label2" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
								<TD colSpan="3"><asp:label id="lblAddressline2" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label4" runat="server" CssClass="StandardLabel">City&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblCity" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
								<TD><asp:label id="Label6" runat="server" CssClass="StandardLabel" Width="50px">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblCounty" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label5" runat="server" CssClass="StandardLabel">State&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblState" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
								<TD><asp:label id="Label7" runat="server" CssClass="StandardLabel" Width="50px">&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblZip" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
							</TR>
						</TABLE>
						<br>
						<TABLE cellSpacing="0" cellPadding="1" width="700" border="0">
							<TR>
								<TD vAlign="top"><asp:label id="lblCreditAppType" runat="server" CssClass="StandardLabel">Credit&nbsp;Appl.&nbsp;Type&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:radiobuttonlist id="radBtnList_CreditAppType" runat="server" CssClass="DescInfoLabel" Width="600px"
										RepeatDirection="Vertical" CellPadding="0" CellSpacing="0" AutoPostBack="True" onselectedindexchanged="radBtnList_CreditAppType_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">Credit&nbsp;Authorization&nbsp;--&nbsp;Individual&nbsp;Responsible&nbsp;for&nbsp;Payment</asp:ListItem>
										<asp:ListItem Value="4">Credit&nbsp;Authorization&nbsp;--&nbsp;Individual&nbsp;Responsible&nbsp;for&nbsp;Payment&nbsp;(No&nbsp;Credit&nbsp;Check&nbsp;required)</asp:ListItem>
										<asp:ListItem Value="2">Credit&nbsp;Card&nbsp;Payments&nbsp;Only</asp:ListItem>
										<asp:ListItem Value="3">QSP&nbsp;Sales&nbsp;Representative&nbsp;Authorization</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
						</TABLE>
						<br>
						<br>
					</td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<tr id="trCreditApp" runat="server">
		<td>
			<table cellSpacing="0" cellPadding="o" width="100%" border="0">
				<tr>
					<td bgColor="#336699"><asp:label id="lblTitleCreditAppInfo" ForeColor="White" runat="server" CssClass="StandardLabel">
							Individual Responsible for Payment  -- (Must be 18 years or older)
						</asp:label></td>
				</tr>
				<tr>
					<td>
						<hr width="100%" SIZE="2">
					</td>
				</tr>
				<tr id="trValSumCreditAppInfo" runat="server" visible="false">
					<td><asp:validationsummary id="ValSumCreditAppInfo" runat="server" CssClass="LabelError" HeaderText="Correct the following error to proceed."></asp:validationsummary></td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td>
									<TABLE cellSpacing="0" cellPadding="1" width="600" border="0">
										<TR>
											<TD><asp:label id="lblFirstName" runat="server" CssClass="StandardLabel">First&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtFirstName" runat="server" CssClass="DescLabel" Width="400px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_FirstName" runat="server" CssClass="LabelError" ControlToValidate="txtFirstName"
																ErrorMessage="The Contact First Name is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="lblLastName" runat="server" CssClass="StandardLabel">Last&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtLastName" runat="server" CssClass="DescLabel" Width="400px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_LastName" runat="server" CssClass="LabelError" ControlToValidate="txtLastName"
																ErrorMessage="The Contact Last Name is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD vAlign="top"><asp:label id="Label8" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtAddressLine1" runat="server" CssClass="DescLabel" Width="400px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_AddrL1" runat="server" CssClass="LabelError" ControlToValidate="txtAddressLine1"
																ErrorMessage="The Address Line 1 is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD vAlign="top"><asp:label id="Label9" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3"><asp:textbox id="txtAddressLine2" runat="server" CssClass="DescLabel" Width="400px" MaxLength="50"></asp:textbox></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label10" runat="server" CssClass="StandardLabel">&nbsp;City&nbsp;:&nbsp;</asp:label></TD>
											<td>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtCity" runat="server" CssClass="DescLabel" Width="200px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_City" runat="server" CssClass="LabelError" ControlToValidate="txtCity"
																ErrorMessage="The City is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</td>
											<TD><asp:label id="Label11" runat="server" CssClass="StandardLabel">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:textbox id="txtCounty" runat="server" CssClass="DescLabel" Width="150px" MaxLength="50"></asp:textbox></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label12" runat="server" CssClass="StandardLabel">State&nbsp;:&nbsp;</asp:label></TD>
											<td>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:dropdownlist id="ddlState" runat="server" CssClass="DescLabel" Width="200px" DataValueField="subdivision_code"
																DataTextField="subdivision_name_1" ondatabinding="ddlState_DataBinding"></asp:dropdownlist></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_State" runat="server" CssClass="LabelError" ControlToValidate="ddlState"
																ErrorMessage="The State is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</td>
											<td><asp:label id="Label14" runat="server" CssClass="StandardLabel">&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></td>
											<TD>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtZip" runat="server" CssClass="DescLabel" Width="70px" MaxLength="5"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_Zip" runat="server" CssClass="LabelError" ControlToValidate="txtZip"
																ErrorMessage="The Zip Code is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="lblPhoneNumber" runat="server" CssClass="StandardLabel">Phone&nbsp;Number&nbsp;:&nbsp;</asp:label></TD>
											<TD>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtPhoneNumber" runat="server" CssClass="DescLabel" Width="150px" MaxLength="20"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_Phone" runat="server" CssClass="LabelError" ControlToValidate="txtPhoneNumber"
																ErrorMessage="The Phone Number is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
											<TD><asp:label id="lblHomePhoneNumber" runat="server" CssClass="StandardLabel">&nbsp;Home&nbsp;Phone&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:textbox id="txtHomePhoneNumber" runat="server" CssClass="DescLabel" Width="150px" MaxLength="20"></asp:textbox></TD>
										</TR>
										<tr>
											<td><asp:label id="lblSSN" runat="server" CssClass="StandardLabel">Social&nbsp;Security&nbsp;Number&nbsp;:&nbsp;</asp:label></td>
											<td>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtSSN" runat="server" CssClass="DescLabel" Width="150px"></asp:textbox></td>
														<td><asp:regularexpressionvalidator id="RegExpVal_SSN" runat="server" CssClass="LabelError" ControlToValidate="txtSSN"
																ErrorMessage="The Social Security Number is invalid (000-00-0000)." ValidationExpression="\d{3}-\d{2}-\d{4}">*</asp:regularexpressionvalidator></td>
													</tr>
												</table>
											</td>
											<TD><asp:label id="Label16" runat="server" CssClass="StandardLabel">&nbsp;Credit&nbsp;Limit&nbsp;:&nbsp;</asp:label></TD>
											<td>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<TD><asp:textbox id="txtCreditLimit" runat="server" CssClass="DescLabel" Width="150px" MaxLength="20"></asp:textbox></TD>
														<td><asp:requiredfieldvalidator id="reqFld_CreditLimit" runat="server" CssClass="LabelError" ControlToValidate="txtCreditLimit"
																ErrorMessage="The Credit Limit is required">*</asp:requiredfieldvalidator></td>
														<td><asp:comparevalidator id="compVal_CreditLimit" runat="server" CssClass="LabelError" ControlToValidate="txtCreditLimit"
																ErrorMessage="The Credit Limit is invalid." Type="Currency" Operator="DataTypeCheck" EnableClientScript="False">*</asp:comparevalidator></td>
														<td><asp:comparevalidator id="compVal_CreditLimitZero" runat="server" CssClass="LabelError" ControlToValidate="txtCreditLimit"
																ErrorMessage="The Credit Limit must be greather than zero." Type="Currency" Operator="GreaterThan"
																EnableClientScript="False" ValueToCompare="0">*</asp:comparevalidator></td>
													</tr>
												</table>
											</td>
										</tr>
									</TABLE>
									<br>
									<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<td><asp:label id="Label23" runat="server" CssClass="StandardLabel" Font-Size="xx-small">
													By completing this information, you authorize QSP to obtain a consumer report 
													from a consumer reporting agency in connection with the credit transaction.
													Upon request, we will provide the name and address at theconsumer report agency.
												</asp:label><br>
												<br>
											</td>
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
	<tr id="trCreditCard" runat="server">
		<td>
			<table cellSpacing="0" cellPadding="o" width="100%" border="0">
				<tr>
					<td bgColor="#336699"><asp:label id="lblTitleCreditCardInfo" ForeColor="White" runat="server" CssClass="StandardLabel">
							Credit Card Information -- Credit Card Payments Only
						</asp:label></td>
				</tr>
				<tr>
					<td>
						<hr width="100%" SIZE="2">
					</td>
				</tr>
				<tr id="trValSumCreditCardInfo" runat="server" visible="false">
					<td><asp:validationsummary id="ValSumCreditCardInfo" runat="server" CssClass="LabelError" HeaderText="Correct the following error to proceed."></asp:validationsummary></td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td>
									<TABLE cellSpacing="0" cellPadding="1" width="600" border="0">
										<TR>
											<TD><asp:label id="Label17" runat="server" CssClass="StandardLabel">First&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtCCFirstName" runat="server" CssClass="DescLabel" Width="400px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="LabelError" ControlToValidate="txtCCFirstName"
																ErrorMessage="The Contact First Name is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="Label18" runat="server" CssClass="StandardLabel">Last&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtCCLastName" runat="server" CssClass="DescLabel" Width="400px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="LabelError" ControlToValidate="txtCCLastName"
																ErrorMessage="The Contact Last Name is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD vAlign="top"><asp:label id="Label19" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtCCAddressLine1" runat="server" CssClass="DescLabel" Width="400px" MaxLength="50"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="LabelError" ControlToValidate="txtCCAddressLine1"
																ErrorMessage="The Address Line 1 is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD vAlign="top"><asp:label id="Label20" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3"><asp:textbox id="txtCCAddressLine2" runat="server" CssClass="DescLabel" Width="400px" MaxLength="50"></asp:textbox></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label21" runat="server" CssClass="StandardLabel">City&nbsp;:&nbsp;</asp:label></TD>
											<TD>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td>
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td><asp:textbox id="txtCCCity" runat="server" CssClass="DescLabel" Width="200px" MaxLength="50"></asp:textbox></td>
																	<td><asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" CssClass="LabelError" ControlToValidate="txtCCCity"
																			ErrorMessage="The City is required">*</asp:requiredfieldvalidator></td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</TD>
											<TD><asp:label id="Label22" runat="server" CssClass="StandardLabel">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:textbox id="txtCCCounty" runat="server" CssClass="DescLabel" Width="150px" MaxLength="50"></asp:textbox></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label30" runat="server" CssClass="StandardLabel">State&nbsp;:&nbsp;</asp:label></TD>
											<TD vAlign="top">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td>
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td><asp:dropdownlist id="ddlCCState" runat="server" CssClass="DescLabel" Width="200px" DataValueField="subdivision_code"
																			DataTextField="subdivision_name_1"></asp:dropdownlist></td>
																	<td><asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" CssClass="LabelError" ControlToValidate="ddlState"
																			ErrorMessage="The State is required">*</asp:requiredfieldvalidator></td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</TD>
											<td><asp:label id="Label31" runat="server" CssClass="StandardLabel">&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></td>
											<TD>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtCCZip" runat="server" CssClass="DescLabel" Width="70px" MaxLength="5"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" CssClass="LabelError" ControlToValidate="txtZip"
																ErrorMessage="The Zip Code is required">*</asp:requiredfieldvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD vAlign="top"><asp:label id="Label32" runat="server" CssClass="StandardLabel">Card&nbsp;Type&nbsp;:&nbsp;</asp:label></TD>
											<TD colSpan="3"><asp:radiobuttonlist id="radBtnLstCreditCardType" runat="server" CssClass="DescLabel" Width="150px" RepeatDirection="Horizontal"
													CellPadding="0" CellSpacing="0">
													<asp:ListItem Value="1" Selected="True">Mastercard</asp:ListItem>
													<asp:ListItem Value="2">Visa</asp:ListItem>
													<asp:ListItem Value="3">Discover</asp:ListItem>
													<asp:ListItem Value="4">American&#160;Express</asp:ListItem>
												</asp:radiobuttonlist></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label35" runat="server" CssClass="StandardLabel">Credit&nbsp;Card&nbsp;#&nbsp;:&nbsp;</asp:label></TD>
											<TD vAlign="top">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td>
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td><asp:textbox id="txtCCNumber" runat="server" CssClass="DescLabel" Width="150px" MaxLength="16"></asp:textbox></td>
																	<td><asp:requiredfieldvalidator id="ReqFldVal_CCNumber" runat="server" CssClass="LabelError" ControlToValidate="txtCCNumber"
																			ErrorMessage="The Credit Card # is required">*</asp:requiredfieldvalidator></td>
																	<td><asp:regularexpressionvalidator id="RegExpVal_CCNumber" ControlToValidate="txtCCNumber" ErrorMessage="Please enter the card number without spaces or dashes."
																			ValidationExpression="^\d+$" EnableClientScript="false" RunAt="server">*</asp:regularexpressionvalidator></td>
																	<td><ccval:creditcardvalidator id="CCVal_CCNumber" CssClass="LabelError" ControlToValidate="txtCCNumber" ErrorMessage="Please enter a valid credit card number"
																			EnableClientScript="False" RunAt="server" AcceptedCardTypes="Amex" ValidateCardType="True">*</ccval:creditcardvalidator></td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</TD>
											<td><asp:label id="Label37" runat="server" CssClass="StandardLabel">&nbsp;Exp.&nbsp;Date&nbsp;(MM/YY)&nbsp;:&nbsp;</asp:label></td>
											<TD colSpan="3">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:textbox id="txtCCExpDate" runat="server" CssClass="DescLabel" Width="70px" MaxLength="5"></asp:textbox></td>
														<td><asp:requiredfieldvalidator id="ReqFldVal_CCExpDate" runat="server" CssClass="LabelError" ControlToValidate="txtCCExpDate"
																ErrorMessage="The Credit Card Expiration Date is required">*</asp:requiredfieldvalidator></td>
														<td><asp:regularexpressionvalidator id="RegExpVal_CCExpDate" CssClass="LabelError" ControlToValidate="txtCCExpDate"
																ErrorMessage="The Credit Card Expiration Date is invalid. (MM/YY)" ValidationExpression="^([0][0-9]|[1][0-2])\/\d{2}$"
																EnableClientScript="false" RunAt="server">*</asp:regularexpressionvalidator></td>
														<td><asp:customvalidator id="custVal_CCExpDate" runat="server" CssClass="LabelError" ErrorMessage="The Credit Card Expiration Date must be greater or equal than the current month"
																EnableClientScript="False">*</asp:customvalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
									</TABLE>
								</td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<br>
			<br>
		</td>
	</tr>
	<tr id="trQSPSalesRep" runat="server">
		<td>
			<table cellSpacing="0" cellPadding="o" width="100%" border="0">
				<tr>
					<td bgColor="#336699"><asp:label id="lblTitleQSPSalesRepInfo" ForeColor="White" runat="server" CssClass="StandardLabel">
								QSP Sales Representative Authorization
							</asp:label></td>
				</tr>
				<tr>
					<td>
						<hr width="100%" SIZE="2">
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td><br>
									<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<td><asp:label id="Label27" runat="server" CssClass="StandardLabel">
														On behalf of the above selling organization. I accept full responsibility for payment of this account
													</asp:label><br>
												<br>
											</td>
										</TR>
									</TABLE>
									<TABLE cellSpacing="0" cellPadding="1" width="600" border="0">
										<TR>
											<TD><asp:label id="Label26" runat="server" CssClass="StandardLabel">FSM&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:label id="lblFMName" runat="server" CssClass="DescInfoLabel" Width="300px"></asp:label></TD>
											<TD><asp:label id="Label33" runat="server" CssClass="StandardLabel">FSM&nbsp;ID&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:label id="lblFMID" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
										</TR>
									</TABLE>
								</td>
							</TR>
						</TABLE>
						<br>
						<br>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr id="trDocumentSection" runat="server">
		<td>
			<table cellSpacing="0" cellPadding="o" width="100%" border="0">
				<tr>
					<td bgColor="#336699"><asp:label id="lblTitleDocumentSection" ForeColor="White" runat="server" CssClass="StandardLabel">
							Documentation Section
						</asp:label></td>
				</tr>
				<tr>
					<td>
						<hr width="100%" SIZE="2">
					</td>
				</tr>
				<tr id="trValSumDocumentSection" runat="server" visible="false">
					<td><asp:validationsummary id="ValSumDocumentSection" runat="server" CssClass="LabelError" HeaderText="Correct the following error to proceed."></asp:validationsummary></td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td>
									<TABLE cellSpacing="0" cellPadding="1" width="600" border="0">
										<TR>
											<TD colSpan="2"><asp:label id="Label13" runat="server" CssClass="StandardLabel">
													The document is received:&nbsp;
												</asp:label><asp:checkbox id="chkBoxDocumentApproved" runat="server" Enabled="False"></asp:checkbox></TD>
										</TR>
										<TR runat="server" visible="false" tr="ReceptionDocument">
											<TD colSpan="2">
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td><asp:label id="Label25" runat="server" CssClass="StandardLabel">Document reception date:&nbsp;</asp:label></td>
														<TD><asp:textbox id="txtDocReceivedDate" runat="server" Font-Size="9px" Height="20px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:textbox>
														<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDocReceivedDate"
                                                                                     Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
														</TD>
														<td><asp:hyperlink id="hypLnkDocReceivedDate" runat="server" ToolTip="Click here to select the received date from a popup calendar !"
																ImageUrl="~/images/Calendar.gif" NavigateUrl="javascript:void(0);">HyperLink</asp:hyperlink>&nbsp;
														</td>
														<td><asp:comparevalidator id="compVal_StartDate" runat="server" CssClass="LabelError" ControlToValidate="txtDocReceivedDate"
																ErrorMessage="The Received Date is invalid." Type="Date" Operator="DataTypeCheck">*</asp:comparevalidator></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="Label24" runat="server" CssClass="StandardLabel">Received&nbsp;By&nbsp;:&nbsp;</asp:label></TD>
											<TD>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<TD><asp:label id="lblDocumentApprovedBy" runat="server" CssClass="DescInfoLabel" Width="250px"></asp:label></TD>
														<td>&nbsp;
														</td>
														<TD><asp:label id="Label28" runat="server" CssClass="StandardLabel">Date&nbsp;:&nbsp;</asp:label></TD>
														<TD><asp:label id="lblDocumentApprovedDate" runat="server" CssClass="DescInfoLabel" Width="200px"></asp:label></TD>
													</tr>
												</table>
											</TD>
										</TR>
									</TABLE>
								</td>
							</TR>
						</TABLE>
						<br>
						<br>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr id="trAccountingSection" runat="server">
		<td>
			<table cellSpacing="0" cellPadding="o" width="100%" border="0">
				<tr>
					<td bgColor="#336699"><asp:label id="Label34" ForeColor="White" runat="server" CssClass="StandardLabel">
								Accounting Department Approval Section
							</asp:label></td>
				</tr>
				<tr>
					<td>
						<hr width="100%" SIZE="2">
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td>
									<TABLE cellSpacing="0" cellPadding="1" width="600" border="0">
										<TR>
											<TD>
												<asp:label id="Label43" runat="server" CssClass="StandardLabel">Approved&nbsp;:&nbsp;</asp:label>
											</TD>
											<TD>
												<asp:checkbox id="chkBoxApproved" runat="server" Enabled="False"></asp:checkbox>
											</TD>
											<td>
												&nbsp;
											</td>
											<TD>
												<asp:label id="Label3" runat="server" CssClass="StandardLabel">Approve&nbsp;Code&nbsp;:&nbsp;</asp:label>
											</TD>
											<TD>
												<asp:TextBox id="txtApproveCode" runat="server" MaxLength="50"></asp:TextBox>
											</TD>
										</TR>
										<TR>
											<TD>
												<asp:label id="Label39" runat="server" CssClass="StandardLabel">Approved&nbsp;By&nbsp;:&nbsp;</asp:label>
											</TD>
											<TD>
												<asp:label id="lblApprovedBy" runat="server" CssClass="DescInfoLabel" Width="250px"></asp:label>
											</TD>
											<td>
												&nbsp;
											</td>
											<TD>
												<asp:label id="Label41" runat="server" CssClass="StandardLabel">Date&nbsp;:&nbsp;</asp:label>
											</TD>
											<TD>
												<asp:label id="lblApprovalDate" runat="server" CssClass="DescInfoLabel" Width="200px"></asp:label>
											</TD>
										</TR>
									</TABLE>
								</td>
							</TR>
						</TABLE>
						<br>
						<br>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>

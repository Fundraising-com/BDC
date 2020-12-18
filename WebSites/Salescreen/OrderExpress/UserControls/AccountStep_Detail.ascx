<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountStep_Detail" Codebehind="AccountStep_Detail.ascx.cs" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td align="left"> <!--Section Title --><asp:label id="Label4" Font-Size="small" runat="server" CssClass="StandardLabel"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label5" runat="server" CssClass="StandardLabel">
				2 - Validate or Enter the information for the new account:
			</asp:label><br>
			<br>
		</td>
	</tr>
	<tr>
		<td>
			<TABLE cellSpacing="0" cellPadding="2" border="0">
				<TR>
					<TD>
						<asp:label id="lblLabelTypeID" runat="server" CssClass="StandardLabel">Type :</asp:label></TD>
					<TD>
						<asp:DropDownList id="ddlType" runat="server" DataValueField="account_type_id" DataTextField="account_type_name"></asp:DropDownList>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label16" runat="server" CssClass="StandardLabel">EDS Account # :</asp:label></TD>
					<TD>
						<asp:TextBox id="txtEDSAccount" runat="server" Width="200px"></asp:TextBox>
					</TD>
				</TR>
				<TR id="trFmIDRow" runat="server">
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
				<TR id="trTaxRow" runat="server">
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
									<asp:TextBox id="txtTaxExemptionExpirationDate" runat="server" MaxLength="4" Width="100px"></asp:TextBox>
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
						<asp:Label id="lblLabelCreditLimit" runat="server" CssClass="StandardLabel">Credit Limit :</asp:Label></TD>
					<TD>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:TextBox id="txtCreditLimit" runat="server" Width="100px"></asp:TextBox>
								</td>
								<td>
									<asp:CompareValidator id="compVal_CreditLimit" runat="server" CssClass="LabelError" ErrorMessage="The Credit Limit is invalid."
										ControlToValidate="txtCreditLimit" Operator="DataTypeCheck" Type="Currency">*</asp:CompareValidator>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR id="trCommentRow">
					<TD valign="top">
						<asp:label id="lblLabelComments" runat="server" CssClass="StandardLabel">Comments :</asp:label></TD>
					<TD>
						<asp:TextBox id="txtComments" runat="server" MaxLength="4000" TextMode="MultiLine" Rows="4" Width="400px"></asp:TextBox></TD>
				</TR>
			</TABLE>
		</td>
	</tr>
	<tr>
		<td>
			<br>
		</td>
	</tr>
	<TR>
		<TD align="center">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td>
						<asp:ImageButton id="imgBtnBack" runat="server" CausesValidation="False" ImageUrl="~/images/btnBack.gif"
							AlternateText="Click here to go back to the previous STEP"></asp:ImageButton>
					</td>
					<td width="100%">
						&nbsp;
					</td>
					<td>
						<asp:ImageButton id="imgBtnNext" runat="server" ImageUrl="~/images/btnNext.gif" AlternateText="Click here to confirm your selection and go to the next STEP"></asp:ImageButton>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>

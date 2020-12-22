<%@ Register TagPrefix="uc1" TagName="PhoneNumberList" Src="PhoneNumberList.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrgStep_EmailAddress" Codebehind="OrgStep_EmailAddress.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="EmailAddressList" Src="EmailAddressList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostalAddressForm" Src="PostalAddressForm.ascx" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td align="left"> <!--Section Title --><asp:label id="Label4" Font-Size="small" runat="server" CssClass="StandardLabel"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label5" runat="server" CssClass="StandardLabel">
				5 - Validate or Enter the Email Address Information:
			</asp:label><br>
			<br>
		</td>
	</tr>
	<tr>
		<td>
			<uc1:EmailAddressList id="EmailAddressList_Org" runat="server"></uc1:EmailAddressList>
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
			<TABLE cellSpacing="0" cellPadding="0" border="0" id="Table1">
				<TR>
					<td>
						<asp:ImageButton id="imgBtnBack" runat="server" CausesValidation="False" ImageUrl="~/images/btnBackBig.gif"
							AlternateText="Click here to go back to the previous STEP"></asp:ImageButton>
					</td>
					<td width="100%">
						&nbsp;
					</td>
					<td>
						<asp:ImageButton id="imgBtnNext" runat="server" ImageUrl="~/images/btnNextBig.gif" AlternateText="Click here to confirm your selection and go to the next STEP"></asp:ImageButton>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>

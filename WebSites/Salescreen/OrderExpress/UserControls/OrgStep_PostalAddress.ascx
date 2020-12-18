<%@ Register TagPrefix="uc1" TagName="PostalAddressForm" Src="PostalAddressForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrgStep_PostalAddress" Codebehind="OrgStep_PostalAddress.ascx.cs" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td align="left"> <!--Section Title --><asp:label id="Label4" Font-Size="small" runat="server" CssClass="StandardLabel"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label5" runat="server" CssClass="StandardLabel">
				3 - Validate or Enter the Postal Address Information:
			</asp:label><br>
			<br>
		</td>
	</tr>
	<tr>
		<td>
			<uc1:PostalAddressForm id="PostalAddressForm_Org" HideButton="True" HideTypeAddress="True" runat="server"></uc1:PostalAddressForm>
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

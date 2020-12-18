<%@ Register TagPrefix="uc1" TagName="AddressControlForm" Src="~/UserControls/AddressControlForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramAgreementStep_PAInformation" Codebehind="ProgramAgreementStep_PAInformation.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ProgramAgreementHeaderDetailForm" Src="~/UserControls/ProgramAgreementHeaderDetailForm.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left">
		</td>
	</tr>
	<tr>
		<td><!--Section Body -->
			<uc1:ProgramAgreementHeaderDetailForm id="HeaderDetail" runat="server" ></uc1:ProgramAgreementHeaderDetailForm>
		</td>
	</tr>
	<TR>
		<TD>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" border="0" id="Table2" width="98%">
				<TR>
					<td>
						<asp:ImageButton id="imgBtnBack" runat="server" CausesValidation="False" ImageUrl="~/images/btnBack.gif"
							AlternateText="Click here to go back to the previous STEP"></asp:ImageButton>
					</td>
					<td align="right">
						<asp:ImageButton id="imgBtnNext" runat="server" CausesValidation="False" ImageUrl="~/images/btnNext.gif"
							AlternateText="Click here to go to the next STEP" ToolTip="Click here to go to the next STEP"></asp:ImageButton>
					</td>
				</TR>				
			</TABLE>
		</TD>
	</TR>
</table>

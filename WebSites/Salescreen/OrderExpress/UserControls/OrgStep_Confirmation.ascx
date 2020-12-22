<%@ Register TagPrefix="uc1" TagName="OrganizationInfo" Src="OrganizationInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrgStep_Confirmation" Codebehind="OrgStep_Confirmation.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="MDRSchoolInfo" Src="MDRSchoolInfo.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table5" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="Label2" CssClass="StandardLabel" runat="server"> 9 - Please review the organization information and confirm :
						</asp:label>&nbsp;&nbsp;&nbsp;
						<br>
						<br>
					</td>
					<td vAlign="top"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<uc1:OrganizationInfo id="OrganizationInfo1" runat="server"></uc1:OrganizationInfo>
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
						<asp:ImageButton id="imgBtnBack" runat="server" CausesValidation="False" ImageUrl="~/images/btnBackBig.gif"
							AlternateText="Click here to go back to the previous STEP"></asp:ImageButton>
					</td>
					<td width="100%">
						&nbsp;
					</td>
					<td>
						<asp:ImageButton id="imgBtnNext" runat="server" CausesValidation="False" ImageUrl="~/images/btnConfirmBig.gif"
							AlternateText="Click here to confirm your selection and go to the next STEP"></asp:ImageButton>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>

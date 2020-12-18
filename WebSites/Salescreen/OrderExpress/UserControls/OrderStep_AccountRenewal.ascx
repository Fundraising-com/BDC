<%@ Register TagPrefix="uc1" TagName="AccountHeaderDetailForm" Src="AccountHeaderDetailForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderStep_AccountRenewal" Codebehind="OrderStep_AccountRenewal.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table5" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="Label2" runat="server" CssClass="StandardLabel" Visible="False">
							2 - Please review the Account Information and complete the information :
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
		<td>
			<uc1:AccountHeaderDetailForm id="HeaderDetail" runat="server"></uc1:AccountHeaderDetailForm>
		</td>
	</tr>
	<TR>
		<TD align="center"><br>
			<TABLE cellSpacing="0" cellPadding="0" border="0" id="Table2">
				<TR>
					<td><asp:imagebutton id="imgBtnBack" runat="server" AlternateText="Click here to go back to the previous STEP"
							ImageUrl="~/images/btnBack.gif" CausesValidation="False"></asp:imagebutton></td>
					<td width="100%">&nbsp;
					</td>
					<td><asp:imagebutton id="imgBtnNext" runat="server" AlternateText="Click here to confirm your selection and go to the next STEP"
							ImageUrl="~/images/btnNext.gif" CausesValidation="False"></asp:imagebutton></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>

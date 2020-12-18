<%@ Register TagPrefix="uc1" TagName="AccountHeaderDetailForm" Src="AccountHeaderDetailForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountStep_Information" Codebehind="AccountStep_Information.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AddressControlForm" Src="AddressControlForm.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="center"><uc1:accountheaderdetailform id="HeaderDetail" runat="server" ValidateDuplicateAccounts="true"></uc1:accountheaderdetailform></td>
	</tr>
	<TR>
		<TD align="center"><br>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><asp:imagebutton id="imgBtnBack" runat="server" CausesValidation="False" ImageUrl="~/images/btnBack.gif"
							AlternateText="Click here to go back to the previous STEP"></asp:imagebutton></td>
					<td width="100%">&nbsp;
					</td>
					<td><asp:imagebutton id="imgBtnNext" runat="server" CausesValidation="False" ImageUrl="~/images/btnNext.gif"
							AlternateText="Click here to confirm your selection and go to the next STEP"></asp:imagebutton></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>

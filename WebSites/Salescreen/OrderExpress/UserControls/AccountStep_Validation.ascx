<%@ Register TagPrefix="uc1" TagName="AccountInfo" Src="AccountInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountStep_Validation" Codebehind="AccountStep_Validation.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table5" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="Label2" CssClass="StandardLabel" runat="server" Visible="False"> 4 - Please review the account information and confirm :
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
			<uc1:AccountInfo id="AccountInfo_Final" runat="server"></uc1:AccountInfo>
		</td>
	</tr>
	<tr>
		<td align="center">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td align="center">
						<asp:CheckBox id="chkBoxShowOnlyException" runat="server" Text="Show Only Information on Notice and Exception"
							CssClass="StandardLabel" AutoPostBack="True" Visible="False" oncheckedchanged="chkBoxShowOnlyException_CheckedChanged"></asp:CheckBox>
					</td>
				</TR>
				<TR>
					<td align="center">
						<asp:Label id="lblShowOnlyExceptionNote" runat="server" Visible="False">Uncheck this check box, if you want to review the order.</asp:Label>
					</td>
				</TR>
			</TABLE>
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
							AlternateText="Click here to go back to the previous STEP" ></asp:ImageButton>
					</td>
					<td width="100%">
						&nbsp;
					</td>
					<td>
						<asp:ImageButton id="imgBtnConfirm" runat="server" CausesValidation="False" ImageUrl="~/images/btnConfirm.gif"
							AlternateText="Click here to confirm your selection and go to the next STEP"></asp:ImageButton>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>

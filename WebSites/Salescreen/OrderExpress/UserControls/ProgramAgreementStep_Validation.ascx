<%@ Reference Control="ProgramAgreementStep_Confirmation.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramAgreementStep_Validation" Codebehind="ProgramAgreementStep_Validation.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ProgramAgreementInfo" Src="ProgramAgreementInfo.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table5" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="Label2" runat="server" CssClass="StandardLabel" Visible="False"> 7 - Please review your order form and follow the instruction below :
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
		<td align="left"> <!--Section Body --><uc1:ProgramAgreementInfo id="ProgramAgreementInfo1" runat="server"></uc1:ProgramAgreementInfo></td>
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
		<TD align="center"><br>
	<TR>
		<TD align="center"><br>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR>
					<td><asp:imagebutton id="imgBtnBack" runat="server" AlternateText="Click here to go back to the previous STEP"
							ImageUrl="~/images/btnBack.gif" CausesValidation="False"></asp:imagebutton>
					</td>
					<td align="right">
						<TABLE id="Table2sss" cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<td align="right">
									<asp:label id="lblSaveForLaterDesc" runat="server" CssClass="StandardLabel">
										Save . . . Hold PA/Process Later
									</asp:label>
								</td>
								<td align="right" valign="top">
									<asp:imagebutton id="imgBtnSaveForLater" runat="server" AlternateText="Click here to save for later process"
										ImageUrl="~/images/btnSavePA.gif" CausesValidation="False"></asp:imagebutton><br>
									<br>
								</td>
							</TR>
							<TR>
								<td align="right">
									<asp:label id="lblConfirmDesc" runat="server" CssClass="StandardLabel">
										Submit . . . Process PA Immediately
									</asp:label>
								</td>
								<td align="right">
									<asp:imagebutton id="imgBtnConfirm" runat="server" AlternateText="Click here to confirm your PA"
										ImageUrl="~/images/btnSubmitPA.gif" CausesValidation="False" ToolTip="Click here to submit and process your PA"></asp:imagebutton>
								</td>
							</TR>
						</TABLE>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>

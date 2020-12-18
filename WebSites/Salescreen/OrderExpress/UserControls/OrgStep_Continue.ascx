<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrgStep_Continue" Codebehind="OrgStep_Continue.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table5" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="Label2" CssClass="StandardLabel" runat="server"> 7 - The Organization Creation process is now terminated. You may continue and add Account to this new organization :
						</asp:label>
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
		</td>
	</tr>
	<tr id="trConfirmation" runat="server">
		<td>
			<br>
			<table id="Table6" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td>
						<table id="Table12" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<asp:label id="lblMessageConfirmation" CssClass="StandardLabel" runat="server" ForeColor="#CC6600">
										The order have been saved sucessfully with the status proceed.<br>													
									</asp:label>
									<br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<br>
			<br>
		</td>
	</tr>
	<TR>
		<TD align="center">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" border="0" width="400">
				<TR>
					<td>
						<asp:ImageButton id="imgBtnFinish" runat="server" CausesValidation="False" ImageUrl="~/images/btnFinishBig.gif"
							AlternateText="Click here to finsh the process" ToolTip="Click here to finsh the process"></asp:ImageButton>
					</td>
					<td width="100%" align="center">
						&nbsp;
					</td>
					<td align="center">
						<asp:ImageButton id="imgBtnContinue" runat="server" CausesValidation="False" ImageUrl="~/images/btnAddAccountBig.gif"
							AlternateText="Click here to continue and order supply" ToolTip="Click here to continue and add account to this New Organization."></asp:ImageButton>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>

<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessFormStep_Confirmation" Codebehind="BusinessFormStep_Confirmation.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table5" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="Label2" runat="server" CssClass="StandardLabel" Visible="False"> 
						6 - The Form Creation process is now terminated :
						</asp:label><br>
						<br>
					</td>
					<td vAlign="top"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body --></td>
	</tr>
	<tr id="trConfirmation" runat="server">
		<td><br>
			<table id="Table6" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td>
						<table id="Table12" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><asp:label id="lblMessageConfirmation" runat="server" CssClass="DescInfoLabel" Font-Name="Times New Roman">
										The Business Form has been saved sucessfully.<br>													
									</asp:label><br>
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
		<td><br>
			<br>
		</td>
	</tr>
	<TR>
		<TD align="center"><br>
			<TABLE cellSpacing="0" cellPadding="0" width="500" border="0">
				<TR>
					<td align="center"><asp:imagebutton id="imgBtnFormList" runat="server" ToolTip="Click here to go the Form List" AlternateText="Click here to go the Form List"
							ImageUrl="~/images/btnFormList.gif" CausesValidation="False"></asp:imagebutton>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>

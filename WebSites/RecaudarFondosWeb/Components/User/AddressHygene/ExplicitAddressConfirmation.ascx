<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ExplicitAddressConfirmation.ascx.cs" Inherits="efundraising.RecaudarFondosWeb.Components.User.AddressHygiene.ExplicitAddressConfirmation" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE cellSpacing="0" cellPadding="0" width="90%" align="center" bgColor="#0">
	<TR>
		<TD width="1" bgColor="#f4692e" height="1"></TD>
		<TD bgColor="#f4692e" height="1"></TD>
		<TD width="1" bgColor="#f4692e" height="1"></TD>
	</TR>
	<TR>
		<TD width="1" bgColor="#f4692e"></TD>
		<TD bgColor="#f7e8e2">
			<TABLE cellSpacing="0" cellPadding="5" width="100%" border="0">
				<TR>
					<TD><img src="http://www.magfundraising.com/Resources/Images/_efund_/_classic_/en-US/WIZARDSPONSOR/question.gif"></TD>
					<TD class="NormalText">
						<table cellpadding="0" cellspacing="0" border="0" >
							<tr>
								<td valign="top" class="CmplxBold">Please confirm:&nbsp;&nbsp;&nbsp;</td>
								<td class="CmplxBold"><asp:Literal ID="AddressLiteral" Runat="server"></asp:Literal></td>
							</tr>
						</table>
						<asp:Button ID="ConfirmButton" Text="Confirm" Runat="server" onclick="ConfirmButton_Click"></asp:Button>
						<asp:Button ID="CancelButton" Text="Cancel" Runat="server" onclick="CancelButton_Click"></asp:Button>
						<asp:Button ID="SaveWithoutChangeButton" Text="NoChange" Runat="server" onclick="SaveWithoutChangeButton_Click"></asp:Button>
					</TD>
				</TR>
			</TABLE>
		</TD>
		<TD width="1" bgColor="#f4692e"></TD>
	</TR>
	<TR>
		<TD width="1" bgColor="#f4692e" height="1"></TD>
		<TD bgColor="#f4692e" height="1"></TD>
		<TD width="1" bgColor="#f4692e" height="1"></TD>
	</TR>
</TABLE>

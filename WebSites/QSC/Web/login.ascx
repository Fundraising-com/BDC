<%@ Control CodeBehind="login.ascx.cs" Language="c#" AutoEventWireup="True" Inherits="QSPFulfillment.login" %>
<HEAD>
	<TITLE>QSP Fulfillment System</TITLE>
</HEAD>
<table  bgcolor="white" cellspacing="0" cellpadding="0">
	<tr>
		<td>
			<table  cellspacing="2" border="0">
				<tr>
					<td colspan="2" align="center" class="CSPlainText"><br>
						Please enter your Username and Password.<br><br><br>
					</td>
				</tr>
				<tr>
					<td align="center" class="CSPlainText">
						
						Username </td>
					<td><ASP:TextBox id="User" Visible="True" class="textbox" runat="server" /></td>
				</tr>
				<tr>
					<td class="CSPlainText" align="center">Password </td><td><ASP:TextBox id="Pass" class="textbox" TextMode="Password" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2" align="center"><br>
						<ASP:Button Text="Login" class="boxlook" OnServerClick="Submit_Click" runat="server" ID="Button1" onclick="Button1_Click" /></td>
				</tr>
				<tr>
					<td class="Font8BoldVRed" align="center" valign="top" colspan="2"><br><b>
							<asp:RequiredFieldValidator id="Validator2" ControlToValidate="User" Display="Dynamic" runat="server">
				Please enter a Username<br>
			</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="Validator3" ControlToValidate="Pass" Display="Dynamic" runat="server">
				Please enter a Password<br>
			</asp:RequiredFieldValidator></b>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>

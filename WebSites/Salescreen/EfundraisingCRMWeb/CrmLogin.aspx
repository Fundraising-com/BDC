<%@ Page language="c#" Trace="false" Codebehind="CrmLogin.aspx.cs" AutoEventWireup="True" Inherits="EFundraisingCRMWeb.CrmLogin" %>
<head runat="server" id="Header" />
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>CrmLogin</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
		<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td>Please use your login password.</td>
		</tr>
		<tr>
			<td><br></td>
		</tr>
		<tr>
			<td>
				<table cellpadding="0" cellspacing="0" border="0">
					<tr>
						<td>Username:</td>
						<td></td>
						<td><asp:TextBox ID="UsernameTextBox" Runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td>Password:</td>
						<td>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </td>
						<td><asp:TextBox ID="PassswordTextBox" TextMode="Password" Runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td colspan="3" align="right">
							<asp:Button ID="LoginButton" Runat="server" Text="Log In" onclick="LoginButton_Click"></asp:Button>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		</table>
        <asp:Label ID="lblDisabled" runat="server" Font-Italic="True" 
            Text="SecurityDisabled" Visible="False"></asp:Label>
     </form>
  </body>
</html>

<%@ Page ValidateRequest="false"  language="c#" Codebehind="showconfirmationpage.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.showconfirmationpage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Error</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td align="center">
						<table id="Table1" cellspacing="0" cellpadding="0" border="0">
							<tr>
								<td style="TEXT-ALIGN: center"><br>
									<asp:label id="lblMessage" runat="server" cssclass="CSPlainText">Label</asp:label></td>
							</tr>
							<tr>
								<td align="center"><br>
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td style="TEXT-ALIGN: left">
												<input id="btnYes" style="WIDTH: 54px; HEIGHT: 24px" type="button" value="Yes" runat="server">
											</td>
											<td>
												<input id="btnNo" style="WIDTH: 54px; HEIGHT: 24px" onclick="parent.document.getElementById('dwindow').style.display='none'"
													type="button" value="No" runat="server">
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

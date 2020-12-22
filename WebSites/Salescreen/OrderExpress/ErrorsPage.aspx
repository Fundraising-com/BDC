<%@ Register TagPrefix="uc1" TagName="MenuBar" Src="~/UserControls/MenuBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Page language="c#" Inherits="QSP.OrderExpress.Web.ErrorsPage" Codebehind="ErrorsPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QSPForm - Errors Page</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
			<TABLE id="tblErrorPage" height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
				border="0">
				<tr>
					<td vAlign="top" align="center" width="100%" height="100%">
						<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="0">
							<TR>
								<TD width="100%"><uc1:header id="HeaderErr" runat="server"></uc1:header></TD>
							</TR>
							<TR>
								<TD bgColor="white"><uc1:menubar id="QSPFormMenuBar" runat="server"></uc1:menubar></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="center" width="100%" height="100%">
									<asp:label id="lblInstruction" runat="server" CssClass="campaigninfo" Font-Bold="True">
																	Dynamic Instructions
																</asp:label>
									<table class="Login" id="tblWelcome" cellSpacing="0" cellPadding="0" width="550" align="middle"
										border="0" runat="server">
										<tr>
											<td style="FONT-SIZE: 20px" rowSpan="4">&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td colSpan="2"><br>
												<asp:label id="lblTitle" runat="server" CssClass="Login" Font-Size="16"> The operation cannot be performed.
										</asp:label><br>
											</td>
										</tr>
										<tr>
											<td colSpan="2">
												<br>
												<asp:Label id="lblDescription" runat="server" Font-Size="12pt" Font-Bold="True">
												An email has been sent to our support center. Please retry in a few minutes.
											</asp:Label>
											</td>
										</tr>
										<tr>
											<td colspan="2" Align="center">
												<br>
												<asp:label id="lblMessage" runat="server" ForeColor="Red"></asp:label>
											</td>
										</tr>
										<tr>
											<td colSpan="2"><br>
											<TD align="center">
											</TD>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="center" width="100%" height="100%"></TD>
							</TR>
							<TR>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD><uc1:Footer id="FooterErr" runat="server"></uc1:Footer></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

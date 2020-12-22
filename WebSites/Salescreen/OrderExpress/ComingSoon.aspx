<%@ Register TagPrefix="uc1" TagName="MenuBar" Src="~/UserControls/MenuBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Page language="c#" Inherits="QSP.OrderExpress.Web.ComingSoon" Codebehind="ComingSoon.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QSPForm - Coming Soon</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
			<TABLE id="tblUnderConstruction" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				align="center" border="0">
				<tr>
					<td vAlign="top" align="center" width="100%" height="100%"><TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="0">
							<TR>
								<TD width="100%"><uc1:header id="HeaderCS" runat="server"></uc1:header></TD>
							</TR>
							<TR>
								<TD bgColor="white"><uc1:menubar id="QSPFormMenuBar" runat="server"></uc1:menubar></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="center" width="100%" height="100%">
									<asp:label id="lblInstruction" runat="server" CssClass="campaigninfo" Font-Bold="True">
																	Dynamic Instructions
																</asp:label>
									<table class="Login" id="tblComingSoon" cellSpacing="0" cellPadding="0" width="450" align="middle"
										border="0" runat="server">
										<tr>
											<td style="FONT-SIZE: 20px" rowSpan="6">&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td colSpan="2"><br>
												<asp:label id="lblTitle" runat="server" CssClass="Login" Font-Size="16">
												This page is under construction
											</asp:label><br>
											</td>
										</tr>
										<tr>
											<td colSpan="2">
												<br>
												<asp:Label id="lblDescription" runat="server" Font-Size="12pt" Font-Bold="True">
												  Please retry in a few days.
												</asp:Label>
											</td>
										</tr>
										<tr>
											<td align="right" colspan="2">
												<table cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td>
															<asp:Label id="lblBack" CssClass="LblButton" runat="server">Go to Home Page&nbsp;</asp:Label>
														</td>
														<td>
															<asp:ImageButton id="imgBtnBack" ImageUrl="~/images/small_go_off.gif" CommandArgument="-1" runat="server"
																CssClass="imgRollover" AlternateText="Back to the Home Page"></asp:ImageButton>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colSpan="2"><br>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="center" width="100%" height="100%"></TD>
							</TR>
							<TR>
								<TD align="center"><asp:label id="lblMessage" runat="server" ForeColor="Red"></asp:label></TD>
							</TR>
							<TR>
								<TD><uc1:Footer id="Footer1" runat="server"></uc1:Footer></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

<%@ Page language="c#" Inherits="QSP.OrderExpress.Web.FAQ_viewer" Codebehind="FAQ_viewer.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FAQ</title>
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
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#fdf6ee" border="0">
				<TR>
					<TD width="35" height="1"><IMG height="1" src="images/spacer.gif" width="35"></TD>
					<TD width="58"><IMG height="1" src="images/spacer.gif" width="58"></TD>
					<TD><IMG height="1" src="images/spacer.gif" width="407"></TD>
				</TR>
				<TR>
					<TD colSpan="3" height="84">
						<table cellSpacing="0" cellPadding="0" width="100%" background="images/faqviewerbg.gif"
							border="0">
							<tr>
								<TD align="left" colSpan="2"><IMG height="84" src="images/faqviewer_01.gif" width="93"></TD>
								<TD align="right"><IMG height="84" src="images/faqviewer_02.gif" width="407"></TD>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left" colSpan="3" height="16"><IMG src="images/faqviewer_03.gif" width="500"></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left" width="35" height="231" rowSpan="2"><IMG height="231" src="images/spacer.gif" width="35"></TD>
					<TD vAlign="top" align="left" colSpan="2" height="4"><IMG height="4" src="images/spacer.gif" width="465"></TD>
				</TR>
				<TR bgColor="#fdf6ee">
					<TD vAlign="top" width="465" colSpan="2">
						<TABLE id="table" cellSpacing="1" cellPadding="1" width="548" border="0">
							<TR>
								<TD style="HEIGHT: 37px"><asp:label id="lblFAQ" runat="server" cssclass="setup"> Error occurred</asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblAnswer" runat="server" cssclass="copyright" Height="35px">
											Sorry
										</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td align="center" colSpan="3"><asp:label id="lblMessage" runat="server" ForeColor="Red" CssClass="eRewardsError"></asp:label>
						<br>
					</td>
				</tr>
				<TR>
					<TD COLSPAN="3" bgcolor="#993300" height="10">
						<IMG SRC="images/faqviewer_07.gif" WIDTH="500" HEIGHT="10"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

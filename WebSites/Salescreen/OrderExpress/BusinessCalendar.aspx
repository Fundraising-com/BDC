<%@ Page language="c#" Inherits="QSP.OrderExpress.Web.BusinessCalendar" Codebehind="BusinessCalendar.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="BusinessCalendarControlForm" Src="~/UserControls/BusinessCalendarControlForm.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Business Calendar</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
				<TR>
					<TD align="center">
						<uc1:BusinessCalendarControlForm id="BizCalendar_Ctrl" runat="server"></uc1:BusinessCalendarControlForm>
					</TD>
				</TR>
				<TR id="trButton" runat="server" visible="false">
					<TD align="center">
						<br>
						<TABLE cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<td>
									<asp:ImageButton id="imgBtnOK" runat="server" CausesValidation="False" ImageUrl="~/images/btnOK.gif"
										AlternateText="Click here to confirm your selection"></asp:ImageButton>
								</td>
								<td>
									<asp:HyperLink id="hypLnkCancel" runat="server" ImageUrl="~/images/btnCancel.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

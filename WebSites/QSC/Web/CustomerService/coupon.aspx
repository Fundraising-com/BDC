<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="uc1" TagName="couponstep3" Src="couponstep3.ascx" %>
<%@ Register TagPrefix="uc1" TagName="couponstep2" Src="couponstep2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="couponstep1" Src="couponstep1.ascx" %>
<%@ Page language="c#" Codebehind="coupon.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.coupon" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>New Sub For Certificate</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
  </HEAD>
	<BODY topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginheight="0" marginwidth="0"
		onload="return window_onunload()">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<!--#include file="fctjavascriptall.js"-->
			<center>
				<table cellpadding="0" cellspacing="0" width="500">
					<tr>
						<td>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" width="100%">
								<TR>
									<TD>
										<br>
										<asp:Label Runat="server" ID="lblHeader" CssClass="CSPageTitle">New Sub For Certificate</asp:Label></TD>
								</TR>
								<TR>
									<TD>
										<br>
										<asp:Label id="lblMessage" runat="server" CssClass="CSDirections"></asp:Label></TD>
								</TR>
								<TR>
									<TD><br>
										<asp:Label id="lblErrorMessage" runat="server" CssClass="CSDirections" ForeColor="Red"></asp:Label></TD>
								</TR>
								<TR>
									<TD>
										<uc1:couponstep1 id="ctrlCouponstep1" runat="server"></uc1:couponstep1>
										<uc1:couponstep2 id="ctrlCouponstep2" runat="server"></uc1:couponstep2>
										<uc1:couponstep3 id="ctrlCouponstep3" runat="server"></uc1:couponstep3></TD>
								</TR>
								<TR>
									<TD><BR>
										<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD>
													<asp:Button id="btnBack" runat="server" CausesValidation="False" Text="Back"></asp:Button></TD>
												<TD align="center">
													<asp:Button id="btnNext" runat="server" Text="Next"></asp:Button></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD></TD>
								</TR>
							</TABLE>
						</td>
					</tr>
				</table>
			</center>
			<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
		</form>
		<!--#include file="errorwindow.js"-->
	</BODY>
</HTML>

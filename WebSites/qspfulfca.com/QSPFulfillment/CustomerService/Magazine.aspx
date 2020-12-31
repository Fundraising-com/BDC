<%@ Register TagPrefix="uc1" TagName="ControlerMagazine" Src="ControlerMagazine.ascx" %>
<%@ Page language="c#" Codebehind="Magazine.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.Magazine" %>
<%@ Register TagPrefix="uc1" TagName="ControlerSearchMagazine" Src="ControlerSearchMagazine.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Magazine</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
		
	</HEAD>
	<BODY onload="return window_onunload()" topmargin="0" bottommargin="0" rightmargin="0"
		leftmargin="0">
		<FORM id="Form2" method="post" runat="server">
		<!--#include file="fctjavascriptall.js"-->
			<asp:Image Runat="server" ID="imgTitle" BorderWidth="0" ImageUrl="images/findtitlecodetitle.gif"></asp:Image>
			<TABLE width="100%" cellpadding="5">
				<TR>
					<TD>
						<br>
						<TABLE id="Table3" cellspacing="0" width="100%" cellpadding="0" bgcolor="#cecece" border="0">
							<TR>
								<TD>
									<TABLE id="Table4" height="100%" width="100%" cellspacing="1" cellpadding="2">
										<TR>
											<TD valign="top" height="20">
												<font class="CSTitle">Search</font>
											</TD>
										</TR>
										<TR bgcolor="#ffffff">
											<TD valign="top">
												<uc1:ControlerSearchMagazine id="ctrlControlerSearchMagazine" runat="server"></uc1:ControlerSearchMagazine>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
						<br>
						<uc1:ControlerMagazine id="ctrlControlerMagazine" runat="server"></uc1:ControlerMagazine>
					</TD>
				</TR>
			</TABLE>
			<!--#include file="errorwindow.js"-->
		</FORM>
	</BODY>
</HTML>

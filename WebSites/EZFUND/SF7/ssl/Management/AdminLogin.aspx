<%@ Register TagPrefix="uc1" TagName="AdminLists" Src="Controls/AdminLists.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AdminLogin.aspx.vb" Inherits="StoreFront.StoreFront.AdminLogin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.0

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
		<script language="javascript">
		
			function SetValidation()
			{
				window.document.Form2.elements["txtUserName"].required=true;
				window.document.Form2.elements["txtUserName"].title="User Name";
				window.document.Form2.elements["txtPassword"].required=true;
				window.document.Form2.elements["txtPassword"].title="Password";
				return ValidateForm(window.document.Form2);
			}
			
		
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server" MS_POSITIONING="FlowLayout">
		<form id="Form2" method="post" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<tr>
					<td class="TopBanner">
						<!-- Top Banner Start -->
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td class="TopBanner2" width="20%"><IMG src="images/sflogo.jpg"></td>
								<td class="TopBanner">&nbsp;&nbsp;Merchant Tools</td>
							</tr>
						</table>
					</td>
					<!-- Top Banner End -->
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell">
						<!-- Top Sub Banner Start --> Login 
						<!-- Top Sub Banner End --></td>
				</tr>
				<tr>
					<!-- Left Column Start -->
					<!-- Left Column End --> </TD>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table cellSpacing="3" cellPadding="5" width="100%" border="0">
							<tr>
								<td class="content">
									<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></P>
								</td>
							</tr>
							<TR>
								<TD class="content" vAlign="top" align="center" colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="contenttable" width="1"><img src="images/clear.gif"></td>
											<td class="contenttable" width="100%"><img src="images/clear.gif"></td>
											<td class="contenttable" width="1"><img src="images/clear.gif"></td>
										</tr>
										<TR>
											<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="left" width="100%">
												<table class="Content" cellPadding="5" border="0" cellspacing="0" width="100%">
													<TR>
														<TD class="content" align="right" width="30%">User Name:&nbsp;</TD>
														<TD width="70%" class="content"><asp:TextBox id="txtUserName" Runat="server"></asp:TextBox></TD>
													</TR>
													<tr>
														<TD class="content" align="right" width="30%">Password:&nbsp;</TD>
														<TD width="70%" class="content"><asp:TextBox id="txtPassword" Runat="server" TextMode="Password" MaxLength="10"></asp:TextBox>&nbsp;Max 
															10 characters</TD>
													</tr>
													<TR>
														<TD class="content" align="right" width="30%"></TD>
														<TD class="content" width="70%"><asp:LinkButton id="cmdSubmit" Runat="server">
																<asp:image ID="img1" Runat="server" ImageUrl="images/submit.jpg"></asp:image>
															</asp:LinkButton></TD>
													</TR>
													<tr>
														<TD class="content" colspan="2"><img src="images/clear.gif"></TD>
													</tr>
													<tr>
														<TD class="content" colspan="2" align="center"><a href="Authentication/default.aspx">Log 
																in as Built-in Administrator</a></TD>
													</tr>
												</table>
											</td>
											<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
										</TR>
										<tr>
											<td class="contenttable" width="1"><img src="images/clear.gif"></td>
											<td class="contenttable" width="100%"><img src="images/clear.gif"></td>
											<td class="contenttable" width="1"><img src="images/clear.gif"></td>
										</tr>
									</table>
								</TD>
							</TR>
						</table>
						<!-- Content End -->
					</td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
	</body>
</HTML>

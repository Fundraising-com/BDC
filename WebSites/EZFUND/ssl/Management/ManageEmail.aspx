<%@ Register TagPrefix="uc1" TagName="StandardSearchControl" Src="Controls/StandardSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Page Language="vb" autoeventwireup="false" codebehind="ManageEmail.aspx.vb" Inherits="StoreFront.StoreFront.ManageEmail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Edit Email</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.1.0

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
		<% Me.PageHeader %>
		<script language="javascript">
		
			function SetValidation()
			{
				
				window.document.Form2.elements["txtEMailAddress"].required=true;
				window.document.Form2.elements["txtEMailAddress"].email=true;
				window.document.Form2.elements["txtEMailAddress"].title="E-Mail Address";
				window.document.Form2.elements["txtSecondaryEmail"].email=true;
				window.document.Form2.elements["txtSecondaryEmail"].title="Secondary E-Mail Address";
				
				return ValidateForm(window.document.Form2)
			}
			
			
			
		</script>
	</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<!-- Top Banner Start -->
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td class="TopBanner2" width="20%"><img src="images/sflogo.jpg"></td>
								<td class="TopBanner">&nbsp;&nbsp;Merchant Tools</td>
							</tr>
						</table>
						<!-- Top Banner End -->
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<!-- Top Sub Banner Start --> E-Mail Management 
						<!-- Top Sub Banner End -->
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<A href="javascript: doHelp(' http://support.storefront.net/mtdocs/email_ov.asp ')">
										<IMG src="images/help.jpg" border="0"></A> 
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="content" align="middle">
									<font color="#ff0000">
										<asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label>
										<asp:label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:label>
									</font>
								</td>
							</tr>
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td class="ContentTable" HEIGHT="1" colspan="5" width="100%"><img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="Content" colspan="5" width="100%"><uc1:AdminTabControl id="AdminTabControl1" runat="server"></uc1:AdminTabControl></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1" colspan="5" width="100%"><img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
									</table>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td class="ContentTable" HEIGHT="1"><img src="images/clear.gif" HEIGHT="1"></td>
											<td class="Content" HEIGHT="1" colspan="3" width="100%">&nbsp;</td>
											<td class="ContentTable" HEIGHT="1"><img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1"><img src="images/clear.gif" HEIGHT="1"></td>
											<td class="Content">&nbsp;</td>
											<td class="Content" width="100%">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="ContentTableHeader" colspan="2" align="left">&nbsp;&nbsp;E-Mail Settings</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<tr>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" colSpan="2">&nbsp;</td>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="right">Your E-Mail Address:&nbsp;&nbsp;</td>
														<td class="Content"><asp:textbox id="txtEMailAddress" runat="server" MaxLength=100 Columns="50"></asp:textbox></td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="right">Secondary E-Mail Address:&nbsp;&nbsp;</td>
														<td class="Content"><asp:textbox id="txtSecondaryEmail" runat="server" MaxLength=100 Columns="50"></asp:textbox></td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="Content" align="middle" colSpan="2" height="1">
															<asp:LinkButton ID="btnSave" Runat="server">
																<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="images/save.jpg" AlternateText="Submit"></asp:Image>
															</asp:LinkButton>
														</TD>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<tr>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" colSpan="2">&nbsp;</td>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<tr>
														<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</tr>
												</table>
											</td>
											<td class="Content">&nbsp;</td>
											<td class="ContentTable" HEIGHT="1"><img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1"><img src="images/clear.gif" HEIGHT="1"></td>
											<td class="Content" colspan="3" width="100%">&nbsp;</td>
											<td class="ContentTable" HEIGHT="1"><img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1" colspan="5"><img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustSignIn.aspx.vb" Inherits="StoreFront.StoreFront.CustSignIn"  TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Customer Sign In</title>
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
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
		<!--
			function SetValidationSignIn()
			{
			
				ResetForm(window.document.Form2);

				window.document.Form2.elements["txtSIEMail"].required=true;
				window.document.Form2.elements["txtSIEMail"].email=true;
				window.document.Form2.elements["txtSIEMail"].title="E-Mail Address";
				window.document.Form2.elements["txtSIPassword"].required=true;
				window.document.Form2.elements["txtSIPassword"].title="Password";
				return ValidateForm(window.document.Form2);
				
			}
			
			function SetValidationNew()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["txtCAFirstName"].required=true;
				window.document.Form2.elements["txtCAFirstName"].title="First Name";
				window.document.Form2.elements["txtCALastName"].required=true;
				window.document.Form2.elements["txtCALastName"].title="Last Name";
				window.document.Form2.elements["txtCAEMail"].required=true;
				window.document.Form2.elements["txtCAEMail"].email=true;
				window.document.Form2.elements["txtCAEMail"].title="E-Mail Address";
				window.document.Form2.elements["txtCAPassword"].required=true;
				window.document.Form2.elements["txtCAPassword"].password=true;
				window.document.Form2.elements["txtCAPassword"].title="Password";
				window.document.Form2.elements["txtCAConfirmPassword"].required=true;
				window.document.Form2.elements["txtCAConfirmPassword"].password=true;
				window.document.Form2.elements["txtCAConfirmPassword"].title="Confirmation Password";
				return ValidateForm(window.document.Form2)
			}
			function onReturn()
			{
				if (event.keyCode == 13)
					document.all["btnSignIn"].click();
			}
		
			//-->
		</script>
	</HEAD>
	<body id="BodyTag" runat="server" class="generalpage" onkeydown="onReturn();">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1" runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start -->
									<uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
									<!-- Top Banner End -->
								</td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start -->
									<uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
									<!-- Top Sub Banner End -->
								</td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell">
									<!-- Left Column Start -->
									<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
									<!-- Left Column End -->
								</td>
								<td class="Content" vAlign="top" id="ContentCell">
									<!-- Content Start -->
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="Instructions">
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content" align="left">
												<P id="ErrorAlignment" runat="server" align="center">
													<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label>
												</P>
												<P id="P1" runat="server" align="center">
													<asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label>
												</P>
												<asp:TextBox id="ReturnPage" runat="server" Visible="False"></asp:TextBox>
												<P>
													<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD vAlign="top">
																<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
																	<TR>
																		<TD class="Headings" colSpan="2" noWrap>&nbsp;Sign In To Your Account</TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD vAlign="top" height="10"></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD class="Content" colSpan="2">To access your account, enter your e-mail address 
																			and password below.</TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD height="10"></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD class="Content" align="right" noWrap>E-Mail Address:&nbsp;</TD>
																		<TD>
																			<asp:TextBox id="txtSIEMail" MaxLength="255" runat="server" tabIndex="1" cssclass="Content"></asp:TextBox></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD class="Content" align="right" noWrap>Password:&nbsp;</TD>
																		<TD>
																			<asp:TextBox id="txtSIPassword" runat="server" TextMode="Password" tabIndex="2" cssclass="Content" MaxLength="255"></asp:TextBox></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD colSpan="2" class="Content" align="right">
																			<asp:HyperLink id="ForgotLink" class="Content" runat="server" NavigateUrl="CustForgotPassword.aspx" tabIndex="3">
																				<u>Forgot Your Password?</u></asp:HyperLink></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD height="10"></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD colSpan="2" align="right">
																			<asp:LinkButton ID="btnSignIn" Runat="server">
																				<asp:Image BorderWidth="0" ID="imgSignIn" Runat="server" AlternateText="Sign In"></asp:Image>
																			</asp:LinkButton>
																		</TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																</TABLE>
															</TD>
															<TD width="1" bgColor="#000000"><img src="images/black.gif" width="1" height="1"></TD>
															<TD vAlign="top">
																<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0">
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Headings" noWrap colSpan="2" width="285">&nbsp;Create a New Account</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD height="10"></TD>
																		<TD height="10" width="152"></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" colSpan="2" width="285">Sign up for an account today and 
																			benefit from faster checkout and more site features.</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD height="10"></TD>
																		<TD height="10" width="152"></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">First Name:&nbsp;</TD>
																		<TD width="152">
																			<asp:TextBox id="txtCAFirstName" runat="server" tabIndex="5" cssclass="Content" MaxLength="100"></asp:TextBox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Last Name:&nbsp;</TD>
																		<TD width="152">
																			<asp:TextBox id="txtCALastName" runat="server" tabIndex="6" cssclass="Content" MaxLength="100"></asp:TextBox><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" height="10"></TD>
																		<TD height="10" width="152"></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right" valign="top">E-Mail Address:&nbsp;</TD>
																		<TD width="152">
																			<asp:TextBox id="txtCAEMail" runat="server" tabIndex="7" cssclass="Content" MaxLength="255"></asp:TextBox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Password:&nbsp;</TD>
																		<TD width="152">
																			<asp:TextBox id="txtCAPassword" runat="server" TextMode="Password" tabIndex="8" cssclass="Content" MaxLength="255"></asp:TextBox><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Confirm Password:&nbsp;</TD>
																		<TD width="152">
																			<asp:TextBox id="txtCAConfirmPassword" runat="server" TextMode="Password" tabIndex="9" cssclass="Content" MaxLength="255"></asp:TextBox><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD height="10"></TD>
																		<TD height="10" width="152"></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Subscribe To Mail List:&nbsp;</TD>
																		<TD width="152">
																			<asp:CheckBox id="chkSubscribe" runat="server" tabIndex="10"></asp:CheckBox></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD height="10"></TD>
																		<TD height="10" width="152"></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD colSpan="2" align="right" width="285">
																			<asp:LinkButton ID="btnCreate" Runat="server">
																				<asp:Image BorderWidth="0" ID="imgCreate" Runat="server" AlternateText="Create Account"></asp:Image>
																			</asp:LinkButton>
																		</TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</P>
											</td>
										</tr>
									</table>
									<!-- Content End -->
								</td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start -->
									<uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
									<!-- Right Column End -->
								</td>
							</tr>
							<tr>
								<td colSpan="3" class="Footer" id="FooterCell">
									<!-- Footer Start -->
									<uc1:footer id="Footer1" runat="server"></uc1:footer>
									<!-- Footer End -->
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</TD></TR></TABLE></form>
	</body>
</HTML>

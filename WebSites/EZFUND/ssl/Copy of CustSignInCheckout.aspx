<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustSignInCheckout.aspx.vb" Inherits="StoreFront.StoreFront.CustSignInCheckout"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>
			<% writeTitle() %>
			- Customer Sign In</TITLE>
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
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
	<body class="generalpage" id="BodyTag" runat="server" onkeydown="onReturn();">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1" runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start --><uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
									<!-- Top Banner End --></td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start --><uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
									<!-- Top Sub Banner End --></td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell">
									<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
									<!-- Left Column End --></td>
								<td class="Content" id="ContentCell" vAlign="top">
									<!-- Content Start -->
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td>
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content" tabIndex="1">
												<P id="ErrorAlignment" align="center" runat="server"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></P>
												<P id="MessageAlignment" align="center" runat="server"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
												<asp:textbox id="ReturnPage" tabIndex="12" runat="server" Visible="False"></asp:textbox>
												<P>
													<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD vAlign="top" width="50%">
																<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD class="Headings" noWrap colSpan="2">Sign In To Your Account</TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD vAlign="top" height="10"></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD class="Content" colSpan="2">To access your account, enter your e-mail address 
																			and password below <P>- please note that as of 3/29/04, we have moved to a new database - you will have to reenter your information.</TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD height="10"></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD class="Content" noWrap align="right">E-Mail Address:&nbsp;</TD>
																		<TD><asp:textbox id="txtSIEMail" runat="server" cssclass="Content" MaxLength="255" tabIndex="1"></asp:textbox></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD class="Content" noWrap align="right">Password:&nbsp;</TD>
																		<TD><asp:textbox id="txtSIPassword" MaxLength="255" tabIndex="2" runat="server" cssclass="Content" TextMode="Password"></asp:textbox></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD class="Content" align="right" colSpan="2"><asp:hyperlink id="ForgotLink" tabIndex="3" runat="server" NavigateUrl="CustForgotPassword.aspx?ReturnPage=CustSignInCheckout.aspx">Forgot Your Password?</asp:hyperlink></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD height="10"></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD align="right" colSpan="2"><asp:linkbutton id="btnSignIn" Runat="server" tabIndex="4">
																				<asp:Image runat="server" AlternateText="Sign In" BorderWidth="0px" ID="imgSignIn"></asp:Image>
																			</asp:linkbutton></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																</TABLE>
															</TD>
															<TD width="1" bgColor="#000000"><IMG height="1" src="images/black.gif" width="1"></TD>
															<TD vAlign="top" width="50%">
																<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Headings" noWrap colSpan="2">Create a New Account</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD height="10"></TD>
																		<TD height="10"></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" colSpan="2">Sign up for an account today and benefit from 
																			faster checkout and more site features.</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD height="10"></TD>
																		<TD height="10"></TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD class="Content" noWrap align="right" height="10">First Name:</TD>
																		<TD height="10"><asp:textbox id="txtCAFirstName" MaxLength="100" tabIndex="5" runat="server" cssclass="Content"></asp:textbox><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD class="Content" noWrap align="right" height="10">Last Name:</TD>
																		<TD height="10"><asp:textbox id="txtCALastName" MaxLength="100" tabIndex="6" runat="server" cssclass="Content"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD class="Content" height="10"></TD>
																		<TD height="10"></TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD class="Content" vAlign="top" noWrap align="right" height="10">E-Mail Address:</TD>
																		<TD height="10"><asp:textbox id="txtCAEMail" tabIndex="7" MaxLength="255" runat="server" cssclass="Content"></asp:textbox><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD class="Content" noWrap align="right" height="10">Password:</TD>
																		<TD height="10"><asp:textbox id="txtCAPassword" tabIndex="7" MaxLength="255" runat="server" cssclass="Content" TextMode="Password"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	</TR>
																	<TR>
																		<TD height="22" style="HEIGHT: 22px"></TD>
																		<TD class="Content" noWrap align="right" height="22" style="HEIGHT: 22px">Confirm 
																			Password:&nbsp;</TD>
																		<TD height="22" style="HEIGHT: 22px"><asp:textbox id="txtCAConfirmPassword" MaxLength="255" tabIndex="8" runat="server" cssclass="Content" TextMode="Password"></asp:textbox><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD class="Content" height="10"></TD>
																		<TD height="10"></TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD class="Content" noWrap align="right" height="10">Subscribe To Mail List:</TD>
																		<TD height="10"><asp:checkbox id="chkSubscribe" tabIndex="9" runat="server"></asp:checkbox></TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD class="Content" height="10"></TD>
																		<TD height="10"></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD align="right" colSpan="2"><asp:linkbutton id="btnContinue" Runat="server" tabIndex="10">
																				<asp:Image runat="server" AlternateText="Create Account" BorderWidth="0px" ID="imgContinue"></asp:Image>
																			</asp:linkbutton></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</P>
											</td>
										</tr>
									</table>
									<!-- Content End --></td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start --><uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
									<!-- Right Column End --></td>
							</tr>
							<tr>
								<td class="Footer" id="FooterCell" colSpan="3">
									<!-- Footer Start --><uc1:footer id="Footer1" runat="server"></uc1:footer>
									<!-- Footer End --></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>


<%@Import Namespace="StoreFront.Systembase" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnonymousLogin" Src="Controls/AnonymousLogin.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustSignInCheckout.aspx.vb" Inherits="StoreFront.StoreFront.CustSignInCheckout"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>
			<% writeTitle() %>
			- Customer Sign In</TITLE>
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
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
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
			
			
			
			function SetValidationAnonymousSignIn()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["AnonymousLogin1:txtAnonEMail"].required=true;
				window.document.Form2.elements["AnonymousLogin1:txtAnonEMail"].email=true;
				window.document.Form2.elements["AnonymousLogin1:txtAnonEMail"].title="E-Mail Address";
				return ValidateForm(window.document.Form2);
			}
			//-->
		</script>
	</HEAD>
	<body class="generalpage" id="BodyTag"  runat="server">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1"
							runat="server">
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
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<h1 class="Headings">Account Sign In</h1>
									<P id="ErrorAlignment" runat="server" align="center"><asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
									<P id="P1" runat="server" align="center"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
									<asp:TextBox id="ReturnPage" runat="server" Visible="False"></asp:TextBox>									
												<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" class="Content col2">
													<TR>
														<TD vAlign="top" class="c1">
															<h2 class="subHeadings">Sign In To Your Account</h2>
															<p>To access your account, enter your e-mail address and password below.</p>
															<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0" class="Content formtbl">
																<TR>
																	<TD class="name">E-Mail Address:&nbsp;</TD>
																	<TD class="input"><asp:textbox id="txtSIEMail" tabIndex="1" runat="server" cssclass="Content" MaxLength="255" ></asp:textbox></TD>
																</TR>
																<TR>
																	<TD class="name">Password:&nbsp;</TD>
																	<TD class="input"><asp:textbox id="txtSIPassword" tabIndex="2" runat="server" cssclass="Content" MaxLength="255"
																				TextMode="Password"></asp:textbox></TD>
																</TR>
																<TR>
																	<TD colSpan="2"><asp:hyperlink id="ForgotLink" tabIndex="3" runat="server" NavigateUrl="CustForgotPassword.aspx?ReturnPage=CustSignInCheckout.aspx">Forgot Your Password?</asp:hyperlink></TD>
																</TR>
																<asp:panel id="pnlPayPalOld" Runat="server">
																	<TR>
																		<TD class="Content" align="right" colSpan="2">
																			<asp:checkbox id="chkPayPalOld" tabIndex="3" Runat="server" Checked="True"></asp:checkbox>&nbsp;Use 
																				my PayPal account for future &nbsp;purchases with
																			<%=StoreFrontConfiguration.StoreName%>
																		</TD>
																	</TR>
																	</asp:panel>
																<TR>
																	<TD colSpan="2" class="button"><asp:linkbutton id="btnSignIn" tabIndex="4" Runat="server">
																			<asp:Image runat="server" AlternateText="Sign In" BorderWidth="0px" ID="imgSignIn"></asp:Image>
																		</asp:linkbutton></TD>
																</TR>
															</TABLE>
														</TD>
														<TD vAlign="top" class="c2">
															<h2 class="subHeadings">Create a New Account</h2>
															<p>Sign up for an account today and benefit from 
																		faster checkout and more site features.</p>
															<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0" class="Content formtbl">
																<TR>
																	<TD class="name">First Name:</TD>
																	<TD class="input"><asp:textbox id="txtCAFirstName" tabIndex="5" runat="server" cssclass="Content" MaxLength="100"></asp:textbox><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																</TR>
																<TR>
																	<TD class="name">Last Name:</TD>
																	<TD class="input"><asp:textbox id="txtCALastName" tabIndex="6" runat="server" cssclass="Content" MaxLength="100"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																</TR>
																<TR>
																	<TD height="20" colspan="2" class="Content"></TD>
																</TR>
																<TR>
																	<TD class="name">E-Mail Address:</TD>
																	<TD class="input"><asp:textbox id="txtCAEMail" tabIndex="7" runat="server" cssclass="Content" MaxLength="255"></asp:textbox><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																</TR>
																<TR>
																	<TD class="name">Password:</TD>
																	<TD class="input"><asp:textbox id="txtCAPassword" tabIndex="8" runat="server" cssclass="Content" MaxLength="255"
																				TextMode="Password"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																</TR>
																<TR>
																	<TD class="name">Confirm 
																		Password:&nbsp;</TD>
																	<TD class="input"><asp:textbox id="txtCAConfirmPassword" tabIndex="9" runat="server" cssclass="Content" MaxLength="255"
																				TextMode="Password"></asp:textbox><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																</TR>
																<TR>
																	<TD colspan="2" class="Content"></TD>
																</TR>
																<TR>
																	<TD class="name">Subscribe To Mail List:</TD>
																	<TD class="input"><asp:checkbox id="chkSubscribe" tabIndex="10" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD colspan="2" class="Content" height="10"></TD>
																</TR>
																<asp:panel id="pnlPayPalNew" Runat="server">
																	<TR>
																		<TD class="Content" align="right" colSpan="2">
																			<asp:CheckBox id="chkPayPalNew" tabIndex="11" Runat="server" Checked="True"></asp:CheckBox>Use 
																				my PayPal account for future purchases &nbsp;with
																			<%=StoreFrontConfiguration.StoreName%>
																		</TD>
																	</TR>
																	</asp:panel>
																<TR>
																	<TD class="button" colSpan="2"><asp:linkbutton id="btnContinue" tabIndex="11" Runat="server">
																			<asp:Image runat="server" AlternateText="Create Account" BorderWidth="0px" ID="imgContinue"></asp:Image>
																		</asp:linkbutton></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
													<tr>
														<td colSpan="2">									
														<uc1:anonymouslogin id="AnonymousLogin1" runat="server"></uc1:anonymouslogin></td>
													</tr>
													</TABLE>
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

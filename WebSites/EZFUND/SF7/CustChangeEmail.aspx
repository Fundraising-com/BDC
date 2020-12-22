<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="custchangeemail.aspx.vb" Inherits="StoreFront.StoreFront.CustChangeEmail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>
			<% writeTitle() %>
			- Customer Forgot Password</TITLE>
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
			function SetValidation()
			{
			if (window.document.Form2.elements["rdoChangeEmail"][2].checked)
			{window.document.Form2.elements["txtUpdateEmail"].required=true;
			//E-mail Address 
			window.document.Form2.elements["txtUpdateEmail"].title="E-mail Address"
			window.document.Form2.elements["txtUpdateEmail"].email=true;
			}
			else
			{window.document.Form2.elements["txtUpdateEmail"].required=false;
			window.document.Form2.elements["txtUpdateEmail"].email=false;
			}
				return ValidateForm(window.document.Form2)
			}
			//-->
		</script>
	</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table class="GeneralTable" id="PageSubTable" runat="server" cellspacing="0">
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
									<table id="Table2" cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="Instructions">
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content">
												<P id="ErrorAlignment" runat="server">
													<font color="#ff0000">
														<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
													</font>
												</P>
												<P id="P1" runat="server">
													<font color="#ff0000">
														<asp:label id="Message" runat="server" Visible="False" CssClass="Message"></asp:label>
													</font>
												</P>
												<P>
													<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD class="Headings" noWrap>Change E-Mail Settings
																<asp:textbox id="txtEmail" runat="server" Visible="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD>&nbsp;</TD>
														</TR>
														<TR>
															<TD class="Content">
																<P>Dear
																	<asp:label id="lblCustName" runat="server"></asp:label>,</P>
																<P>We wish to communicate special events, offers and sales to you via E-Mail, but 
																	only if you enjoy receiving them. You can change your E-Mail address or remove 
																	yourself from our list by choosing from the options below.</P>
															</TD>
														</TR>
														<TR>
															<TD class="Content" height="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD vAlign="top" align="middle">
																<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="300" border="0">
																	<TR>
																		<TD class="Content" align="right">
																			<!--<asp:RadioButtonList Runat="server" id="RadioButtonList1">
																					<asp:ListItem Value="Please remove me from your E-Mail list."></asp:ListItem>
																					<asp:ListItem Value="Please continue to notify me."></asp:ListItem>
																					<asp:ListItem Value="Please change my E-Mail address to"></asp:ListItem>
																				</asp:RadioButtonList>-->
																			<asp:RadioButton ID="rdoRemove" Runat="server" GroupName="rdoChangeEmail" class="Content"></asp:RadioButton>
																		</TD>
																		<TD class="Content" noWrap>Please remove me from the mailing list.</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD height="10"></TD>
																	</TR>
																	<TR>
																		<TD class="Content" align="right"><asp:radiobutton id="rdoNoAction" Runat="server" GroupName="rdoChangeEmail" class="Content" Checked></asp:radiobutton></TD>
																		<TD class="Content" noWrap>Please continue to notify me of
																			<asp:label id="lblStoreName" runat="server"></asp:label>&nbsp;news.</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD height="10"></TD>
																	</TR>
																	<TR>
																		<TD class="Content" align="right"><asp:radiobutton id="rdoChangeEmail" Runat="server" GroupName="rdoChangeEmail" class="Content"></asp:radiobutton></TD>
																		<TD class="Content" noWrap>Change my E-Mail address to
																			<asp:textbox id="txtUpdateEmail" runat="server" MaxLength="255"></asp:textbox><br>
																			Note: This will also change your username.</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD height="10"></TD>
																	</TR>
																	<TR>
																		<TD align="right" colSpan="2">
																			<asp:LinkButton ID="btnSubmit" Runat="server">
																				<asp:Image BorderWidth="0" ID="imgSubmit" Runat="server" AlternateText="Submit"></asp:Image>
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
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EMailWishList.aspx.vb" Inherits="StoreFront.StoreFront.EMailWishList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Wish List</title>
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
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
		
			function SetValidation()
			{
			window.document.Form2.elements["txtRecipientName"].required=true;
			window.document.Form2.elements["txtRecipientName"].title="Recipient's Name";
			window.document.Form2.elements["txtRecipientEmail"].required=true;
			window.document.Form2.elements["txtRecipientEmail"].email=true;
			window.document.Form2.elements["txtRecipientEmail"].title="Recipient's E-Mail";
			window.document.Form2.elements["txtSenderName"].required=true;
			window.document.Form2.elements["txtSenderName"].title="Sender's Name";
			window.document.Form2.elements["txtSenderEmail"].required=true;
			window.document.Form2.elements["txtSenderEmail"].email=true;
			window.document.Form2.elements["txtSenderEmail"].title="Sender's E-Mail";
				return ValidateForm(window.document.Form2)
			}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" runat="server" cellspacing="0">
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
												<P align="center"><FONT color="#ff0000"> <STRONG>
															<asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessage"></asp:label>
														</STRONG></FONT>
												</P>
												<P align="center"><FONT color="#ff0000">
														<asp:label id="Message" runat="server" CssClass="Messages"></asp:label></FONT>
												</P>
												<P>
													<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Headings" noWrap colSpan="3">E-Mail&nbsp;Wish List</TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" colSpan="3">&nbsp;</TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" noWrap align="right">Sender's Name:
															</TD>
															<TD class="Content" colSpan="2">&nbsp;
																<asp:textbox id="txtSenderName" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" colSpan="3">&nbsp;</TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" noWrap align="right">Sender's E-Mail:
															</TD>
															<TD class="Content" colSpan="2">&nbsp;
																<asp:textbox id="txtSenderEmail" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" colSpan="3">&nbsp;</TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" noWrap align="right">Recipient's Name:
															</TD>
															<TD class="Content" colSpan="2">&nbsp;
																<asp:textbox id="txtRecipientName" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" colSpan="3">&nbsp;</TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" noWrap align="right">Recipient's E-Mail:
															</TD>
															<TD class="Content" colSpan="2">&nbsp;
																<asp:textbox id="txtRecipientEmail" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" colSpan="3">&nbsp;</TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" noWrap align="right">Message:
															</TD>
															<TD class="Content" colSpan="2">&nbsp;
																<asp:textbox id="txtMessage" runat="server" Rows="5" TextMode="MultiLine" Columns="40"></asp:textbox></TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" colSpan="3">&nbsp;</TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" align="right" colSpan="3">
																<asp:LinkButton ID="btnSend" Runat="server">
																	<asp:Image BorderWidth="0" ID="imgSend" Runat="server" AlternateText="Send"></asp:Image>
																</asp:LinkButton>
															</TD>
															<TD width="10">&nbsp;</TD>
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

<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="emailafriend.aspx.vb" Inherits="StoreFront.StoreFront.EMailAFriend"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- E-Mail A Friend</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.0.2

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
			window.document.Form2.elements["txtSendTo"].required=true;
			window.document.Form2.elements["txtSendTo"].title="Recipient's Name";
			window.document.Form2.elements["txtSendToEmail"].required=true;
			window.document.Form2.elements["txtSendToEmail"].email=true;
			window.document.Form2.elements["txtSendToEmail"].title="Recipient's E-Mail";
			window.document.Form2.elements["txtSenderName"].required=true;
			window.document.Form2.elements["txtSenderName"].title="Sender's Name";
			window.document.Form2.elements["txtSenderEmail"].required=true;
			window.document.Form2.elements["txtSenderEmail"].email=true;
			window.document.Form2.elements["txtSenderEmail"].title="Sender's E-Mail";
				return ValidateForm(window.document.Form2)
			}
		
		</script>
	</HEAD>
	<body id="BodyTag" runat="server" class="GeneralPage">
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
									<table id="Table2" cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td>
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content">
												<P id="ErrorAlignment" runat="server" align="center">
													<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
												<P id="MessageAlignment" runat="server" align="center">
													<asp:Label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:Label>
												</P>
												<asp:textbox id="txtProdID" runat="server" Visible="False"></asp:textbox><asp:TextBox id="txtReferrer" runat="server" Visible="False"></asp:TextBox>
												<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TBODY>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Headings" noWrap colSpan="3">E-Mail&nbsp;A Friend</TD>
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
																<asp:textbox id="txtSendTo" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
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
																<asp:textbox id="txtSendToEmail" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 11px" width="10"></TD>
															<TD class="Content" style="HEIGHT: 11px" noWrap align="right"></TD>
															<TD class="Content" style="HEIGHT: 11px" colSpan="2"></TD>
															<TD style="HEIGHT: 11px" width="10"></TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" noWrap align="right">Sender's Name:
															</TD>
															<TD class="Content" colSpan="2">&nbsp;
																<asp:textbox id="txtSenderName" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10"></TD>
															<TD class="Content" colSpan="3">&nbsp;</TD>
															<TD width="10"></TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" noWrap valign="top" align="right">Sender's E-Mail:
															</TD>
															<TD class="Content" colSpan="2">&nbsp;
																<asp:textbox id="txtSenderEmail" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label>
																<br>
																<asp:checkbox id="chkEmailCopyToSender" runat="server"></asp:checkbox>E-Mail a 
																copy to the sender</TD>
															<TD width="10">&nbsp;</TD>
														</TR>
														<TR>
															<TD width="10"></TD>
															<TD class="Content" colSpan="3">&nbsp;</TD>
															<TD width="10"></TD>
														</TR>
														<TR>
															<TD width="10">&nbsp;</TD>
															<TD class="Content" noWrap align="right">Message:
															</TD>
															<TD class="Content" colSpan="2">&nbsp;
																<asp:textbox id="txtMessage" runat="server" Columns="40" TextMode="MultiLine" Rows="5"></asp:textbox></TD>
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
													</TBODY>
												</TABLE>
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

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
		<title><% writeTitle() %>			- E-mail Wish List</title>
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
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<p class="PageNavigation">
										<a href="CustEdit.aspx">Edit My Profile</a> <span>|</span>
										<a href="OrderHistory.aspx">View Order Status and History</a> <span>|</span>
										<a href="SavedCart.aspx">Access My Wish List</a> <span>|</span>
										<a href="CustAddressBook.aspx">Manage Address Book</a>
									</p>
									<h1 class="Headings">E-mail Wish List</h1>
									<P id="ErrorAlignment" runat="server" align="center"><asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
									<P id="P1" runat="server" align="center"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
									
									<table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="Content">
												<TABLE id="Table3" cellSpacing="0" cellPadding="0" class="Content formtbl" border="0">
													<TR>
														<TD class="name">Sender's Name:</TD>
														<TD class="input"><asp:textbox id="txtSenderName" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">Sender's E-Mail:</TD>
														<TD class="input"><asp:textbox id="txtSenderEmail" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD colSpan="3">&nbsp;</TD>
													</TR>
													<TR>
														<TD class="name">Recipient's Name:</TD>
														<TD class="input"><asp:textbox id="txtRecipientName" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">Recipient's E-Mail:</TD>
														<TD class="input"><asp:textbox id="txtRecipientEmail" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD colspan="2" class="name">&nbsp;</TD>
													</TR>
													<TR>
														<TD class="name">Message:</TD>
														<TD class="input"><asp:textbox id="txtMessage" runat="server" Rows="5" TextMode="MultiLine" Columns="40"></asp:textbox></TD>
													</TR>
													<TR>
														<TD class="button" colSpan="3">
															<asp:LinkButton ID="btnSend" Runat="server">
																<asp:Image BorderWidth="0" ID="imgSend" Runat="server" AlternateText="Send"></asp:Image>
															</asp:LinkButton>
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

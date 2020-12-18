<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="custedit.aspx.vb" Inherits="StoreFront.StoreFront.CustEdit"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>
			<% writeTitle() %>
			- Customer Account Edit</TITLE>
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
				
				window.document.Form2.elements["txtCAFirstName"].required=true;
				window.document.Form2.elements["txtCAFirstName"].title="First Name";
				window.document.Form2.elements["txtCALastName"].required=true;
				window.document.Form2.elements["txtCALastName"].title="Last Name";
				window.document.Form2.elements["txtCAEMail"].required=true;
				window.document.Form2.elements["txtCAEMail"].email=true;
				window.document.Form2.elements["txtCAEMail"].title="E-Mail Address";
				window.document.Form2.elements["txtCAPassword"].password=true;
				window.document.Form2.elements["txtCAPassword"].title="New Password";
				window.document.Form2.elements["txtCAConfirmPassword"].password=true;
				window.document.Form2.elements["txtCAConfirmPassword"].title="Confirmation Password";
				return ValidateForm(window.document.Form2)
			}
			//-->
		</script>
	</HEAD>
	<body id="BodyTag" runat="server" class="GeneralPage">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1"
							runat="server">
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
									<h1 class="Headings">My Profile</h1>
									<P id="ErrorAlignment" runat="server" align="center"><asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
									<P id="P1" runat="server" align="center"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
									<p>Update your Profile below. Click Save to save your changes</p>			
												<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" class="Content formtbl">
													<TR>
														<TD class="name">First Name:&nbsp;</TD>
														<TD class="input"><asp:textbox id="txtCAFirstName" runat="server" cssclass="Content" MaxLength="100"></asp:textbox><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">Last Name:&nbsp;</TD>
														<TD class="input"><asp:textbox id="txtCALastName" runat="server" cssclass="Content" MaxLength="100"></asp:textbox><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD colspan="2" height="20"></TD>
													</TR>
													<TR>
														<TD class="name">E-Mail Address:&nbsp;</TD>
														<TD class="input"><asp:textbox id="txtCAEMail" runat="server" cssclass="Content" MaxLength="255"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">Old Password:&nbsp;</TD>
														<TD class="input"><asp:textbox id="txtOldPassWord" runat="server" TextMode="Password" cssclass="Content" MaxLength="255"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">New Password:&nbsp;</TD>
														<TD class="input"><asp:textbox id="txtCAPassword" runat="server" TextMode="Password" cssclass="Content" MaxLength="255"></asp:textbox><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">Confirm New Password:&nbsp;</TD>
														<TD class="input"><asp:textbox id="txtCAConfirmPassword" runat="server" TextMode="Password" cssclass="Content"
																MaxLength="255"></asp:textbox><asp:label id="Label6" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD height="20" colspan="2"></TD>
													</TR>
													<TR>
														<TD class="name">Subscribe To Mail List:&nbsp;</TD>
														<TD class="input"><asp:checkbox id="chkSubscribe" runat="server"></asp:checkbox></TD>
													</TR>
													<TR>
														<TD colspan="2" class="button">
															<asp:LinkButton ID="cmdUpdate" Runat="server">
																<asp:Image BorderWidth="0" ID="imgSave" Runat="server" AlternateText="Save"></asp:Image>
															</asp:LinkButton></TD>
													</TR>
												</TABLE>
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

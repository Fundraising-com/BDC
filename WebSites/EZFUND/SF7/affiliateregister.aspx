<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="affiliateregister.aspx.vb" Inherits="StoreFront.StoreFront.affiliateregister"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Affiliate Information</title>
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
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
		<!--
			function SetValidation()
			{	if (window.document.Form2.elements["FirstName"] != null)	
				{	
					window.document.Form2.elements["FirstName"].required=true;
					window.document.Form2.elements["FirstName"].title="First Name";
					window.document.Form2.elements["LastName"].required=true;
					window.document.Form2.elements["LastName"].title="Last Name";
					window.document.Form2.elements["Address1"].required=true;
					window.document.Form2.elements["Address1"].title="Address";
					window.document.Form2.elements["City"].required=true;
					window.document.Form2.elements["City"].title="City";
					window.document.Form2.elements["Phone"].required=true;
					window.document.Form2.elements["Phone"].phonenumber=true;
					window.document.Form2.elements["Phone"].title="Phone";
					window.document.Form2.elements["Fax"].phonenumber=true;
					window.document.Form2.elements["Fax"].title="Fax";
					if (window.document.Form2.elements["Country"].value == "US" ||
						window.document.Form2.elements["Country"].value == "CA")
					{
						window.document.Form2.elements["Zip"].required=true;
						window.document.Form2.elements["Zip"].title="Postal Code";
					}
					else
					{
						window.document.Form2.elements["Zip"].required=false;
					}
				}
				if (window.document.Form2.elements["WebSite"] != null)
					{window.document.Form2.elements["WebSite"].required=true;
					window.document.Form2.elements["WebSite"].title="Web Site Address";
					window.document.Form2.elements["EMail"].required=true;
					window.document.Form2.elements["EMail"].email=true;
					window.document.Form2.elements["EMail"].title="E-Mail Address";
					window.document.Form2.elements["password"].password=true;
					window.document.Form2.elements["password"].title="New Password";
					window.document.Form2.elements["Confirmpassword"].password=true;
					window.document.Form2.elements["Confirmpassword"].title="Confirmation Password";
					}
				return ValidateForm(window.document.Form2)
			}
			//-->
		</script>
	</HEAD>
	<body id="BodyTag" runat="server" class="generalpage">
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
											<td class="Content">
												<P id="ErrorAlignment" runat="server" align="center">
													<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
											</td>
										</tr>
										<TR>
											<TD class="Headings" noWrap>Affiliate Information</TD>
										</TR>
										<tr>
											<td class="Content">
												<table cellpadding="0" cellspacing="0">
													<TR>
														<TD class="Content" noWrap align="right">&nbsp;</TD>
														<TD class="Content" noWrap align="right">&nbsp;</TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">First Name:&nbsp;</TD>
														<TD class="Content" noWrap>
															<asp:textbox id="FirstName" runat="server" CssClass="Content" MaxLength="24"></asp:textbox><asp:Label Runat="server" CssClass="ErrorMessages" id="Label2">*</asp:Label></TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">Middle Initial:&nbsp;
														</TD>
														<TD class="Content"><asp:textbox id="MI" runat="server" Columns="3" CssClass="Content" MaxLength="2"></asp:textbox></TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">Last Name:&nbsp;
														</TD>
														<TD class="Content" noWrap><asp:textbox id="LastName" runat="server" CssClass="Content" MaxLength="25"></asp:textbox><asp:Label Runat="server" CssClass="ErrorMessages" id="Label3">*</asp:Label></TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">Company:&nbsp;</TD>
														<TD class="Content">
															<asp:TextBox id="Company" runat="server" CssClass="Content" MaxLength="75"></asp:TextBox></TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">Address:&nbsp;</TD>
														<TD class="Content" noWrap>
															<asp:textbox id="Address1" runat="server" CssClass="Content" MaxLength="255"></asp:textbox><asp:Label Runat="server" CssClass="ErrorMessages" id="Label4">*</asp:Label></TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">Address2:&nbsp;</TD>
														<TD class="Content">
															<asp:textbox id="Address2" runat="server" CssClass="Content" MaxLength="255"></asp:textbox></TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">City:&nbsp;</TD>
														<TD class="Content" noWrap>
															<asp:textbox id="City" runat="server" CssClass="Content" MaxLength="50"></asp:textbox><asp:Label Runat="server" CssClass="ErrorMessages" id="Label5">*</asp:Label></TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">State/Province:&nbsp;</TD>
														<TD class="Content" noWrap>
															<cc1:SelectValControl id="State" runat="server" Width="206px" CssClass="Content" DisplaySelect="States">
																<asp:ListItem Value="States">[States]</asp:ListItem>
															</cc1:SelectValControl><asp:Label Runat="server" CssClass="ErrorMessages" id="Label6">*</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">Postal Code:&nbsp;</TD>
														<TD class="Content" noWrap>
															<asp:textbox id="Zip" runat="server" CssClass="Content" MaxLength="50"></asp:textbox><asp:Label Runat="server" CssClass="ErrorMessages" id="Label7"></asp:Label></TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">Country:&nbsp;</TD>
														<TD class="Content" noWrap>
															<cc1:SelectValControl id="Country" runat="server" Width="206px" CssClass="Content" DisplaySelect="Country">
																<asp:ListItem Value="States">[States]</asp:ListItem>
															</cc1:SelectValControl><asp:Label Runat="server" CssClass="ErrorMessages" id="Label8">*</asp:Label></TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">Phone:&nbsp;</TD>
														<TD class="Content" noWrap>
															<asp:textbox id="Phone" runat="server" CssClass="Content" MaxLength="50"></asp:textbox><asp:Label Runat="server" CssClass="ErrorMessages" id="Label9">*</asp:Label></TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right">Fax:&nbsp;</TD>
														<TD class="Content">
															<asp:textbox id="Fax" runat="server" CssClass="Content" MaxLength="50"></asp:textbox></TD>
													</TR>
												</table>
												<asp:Panel Runat="server" ID="ShowME">
													<table cellpadding="0" cellspacing="0">
														<TR>
															<TD class="Content" noWrap align="right" colSpan="2">&nbsp;
															</TD>
														</TR>
														<TR>
															<TD class="Content" noWrap align="right">Web Site Address:&nbsp;
															</TD>
															<TD class="Content" noWrap>
																<asp:textbox id="WebSite" runat="server" CssClass="Content" Width="206px" MaxLength="50"></asp:textbox>
																<asp:Label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:Label></TD>
														</TR>
														<TR>
															<TD class="Content" noWrap align="right">E-mail Address:&nbsp;
															</TD>
															<TD class="Content" noWrap>
																<asp:textbox id="EMail" runat="server" CssClass="Content" MaxLength="50"></asp:textbox>
																<asp:Label id="Label10" CssClass="ErrorMessages" Runat="server">*</asp:Label></TD>
														</TR>
														<TR>
															<TD class="Content" noWrap align="right">New&nbsp;Password:&nbsp;
															</TD>
															<TD class="Content" noWrap>
																<asp:textbox id="password" runat="server" CssClass="Content" TextMode="Password" MaxLength="50"></asp:textbox>
																<asp:Label id="Label11" CssClass="ErrorMessages" Runat="server">*</asp:Label></TD>
														</TR>
														<TR>
															<TD class="Content" noWrap align="right">Confirm&nbsp;New&nbsp;Password:&nbsp;
															</TD>
															<TD class="Content" noWrap>
																<asp:textbox id="Confirmpassword" runat="server" CssClass="Content" TextMode="Password" MaxLength="50"></asp:textbox>
																<asp:Label id="Label12" CssClass="ErrorMessages" Runat="server">*</asp:Label></TD>
														</TR>
													</table>
												</asp:Panel>
												<table cellpadding="0" cellspacing="0">
													<tr>
														<td colspan="2">&nbsp;</td>
													</tr>
													<TR>
														<TD class="Content" colspan="2" align="middle">
															<asp:LinkButton ID="cmdSave" Runat="server">
																<asp:Image BorderWidth="0" ID="imgSave" Runat="server" AlternateText="Save"></asp:Image>
															</asp:LinkButton>
														</TD>
													</TR>
													<TR>
														<TD class="Content" noWrap align="right"></TD>
														<TD class="Content">&nbsp;</TD>
													</TR>
												</table>
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
			</TD></TR></TABLE>
		</form>
		</TD></TR></TABLE>
	</body>
</HTML>

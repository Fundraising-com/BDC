<%@ Register TagPrefix="uc1" TagName="StandardSearchControl" Src="Controls/StandardSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
<%@ Page Language="vb" autoeventwireup="false" codebehind="ManageEmail.aspx.vb" Inherits="StoreFront.StoreFront.ManageEmail" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Edit E-mail</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.1

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
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="E-Mail Management"></uc1:TopSubBanner>
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/email_ov.asp ')">
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
													<%-- begin: GJV - 7.0.2 - Add means to edit email method and server --%>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="content" colspan="2">&nbsp;</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="right">E-Mail Method:&nbsp;&nbsp;</td>
														<td class="Content">
															<asp:DropDownList ID="ddlEmailMethod" Runat="server">
																<asp:ListItem Value="CDONTS Mail">CDONTS Mail</asp:ListItem>
																<asp:ListItem Value="CDOSYS Mail">CDOSYS Mail</asp:ListItem>
																<asp:ListItem Value="CDONTS/CDOSYS Mail">CDONTS/CDOSYS Mail</asp:ListItem>
																<asp:ListItem Value="ASP Mail">ASP Mail</asp:ListItem>
																<asp:ListItem Value="ASP QMail">ASP QMail</asp:ListItem>
																<asp:ListItem Value="SimpleMail 3">SimpleMail 3</asp:ListItem>
																<asp:ListItem Value="J Mail">J Mail</asp:ListItem>
																<asp:ListItem Value="ASP Email">ASP Email</asp:ListItem>
																<asp:ListItem Value="AB Mail">AB Mail</asp:ListItem>
																<asp:ListItem Value="OCX Mail">OCX Mail</asp:ListItem>
															</asp:DropDownList>
														</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" align="right">E-Mail Server:&nbsp;&nbsp;</td>
														<td class="Content"><asp:textbox id="txtEmailServer" runat="server" MaxLength="255" Columns="50"></asp:textbox></td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<tr>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="content" colspan="2">&nbsp;</td>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<%-- end: GJV - 7.0.2 - Add means to edit email method and server --%>
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

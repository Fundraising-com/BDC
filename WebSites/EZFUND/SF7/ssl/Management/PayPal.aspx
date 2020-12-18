<%@ Register TagPrefix="uc1" TagName="UploadControl" Src="Controls/UploadControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PaymentMethodsControl" Src="Controls/PaymentMethodsControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PayPal.aspx.vb" Inherits="StoreFront.StoreFront.PayPal"%>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@Import Namespace="Storefront.systembase"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
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
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server">
	
	<script language="javascript">
		function ValidateSave()
		{
			
			var chkpaymethod = document.getElementById("chkBoxPayPalAsPayMethod");
			
			
			var chkexpress = document.getElementById("chkBoxExpress");
			
			if(chkexpress.checked){ 
				if(!chkpaymethod.checked)
				{
				alert("PayPal Express Checkout requires that you accept PayPal as a Payment Method. Your selection could not be saved.  Please check 'Accept PayPal as a Payment Method' and try again.")
				return false;
				}
			}
			return true;
		}
		
	
	</script>  
		<form id="Form2" method="post" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<tr>
					<td class="TopBanner" colSpan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Payments"></uc1:TopSubBanner>
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table cellSpacing="3" cellPadding="5" width="100%" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/paypal/default.asp ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="content" vAlign="top" align="center" colSpan="2">
									<p id="ErrorAlignment" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></p>
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" width="100%" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="center"><%= IIF(restrictedPages(tasks.paymentmethods)=true,"<span class=DisableLink>Payment Methods</span>","<a class=content href='PaymentMethods.aspx'>Payment Methods</a>")%>
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="center"><%= IIF(restrictedPages(tasks.onlineprocessing)=true,"<span class=DisableLink>Online Processing</span>","<a class=content href='PaymentProcessors.aspx'>Online Processing</a>")%>
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">&nbsp;&nbsp;<%= IIF(restrictedPages(tasks.Encryption)=true,"<span class=DisableLink>Encryption</span>","<a class=content href='Encryption.aspx'>Encryption</a>")%>&nbsp;&nbsp;
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">&nbsp;&nbsp;&nbsp;&nbsp;<A class="content" href="PayPal.aspx"><b>PayPal</b></A>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="content" align="center" colSpan="7"><br>
												<!-- PayPal Table Starts -->
												<table class="ContentTable" cellSpacing="0" cellPadding="0" width="97%" border="0">
													<tr>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td class="ContentTableHeader" colSpan="5">PayPal Payment Options</td>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
													</tr>
													<tr>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<td class="Content" width="1" colSpan="5" height="10"><IMG src="images/clear.gif" width="1"></td>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</tr>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/paypal.gif"></TD>
														<TD class="content" align="left" colSpan="4">PayPal makes E-Commerce easy. With 
															just an e-mail address, you can accept credit card payments online in minutes! <A href="http://www.paypal.com/cgi-bin/webscr?cmd=_merchant-outside" target="_blank">
																Click Here</A> for more information.</TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</TR>
												</table>
												<table class="ContentTable" cellSpacing="0" cellPadding="0" width="97%" border="0">
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" align="right" width="6" colSpan="1"><IMG src="images/clear.gif"></TD>
														<TD class="content" align="right" width="215" colSpan="1">Accept PayPal As A 
															Payment Method:</TD>
														<TD class="content" align="left" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkBoxPayPalAsPayMethod" Runat="server"></asp:checkbox></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" align="right" width="6" colSpan="1"><IMG src="images/clear.gif"></TD>
														<TD class="content" align="right" width="215" colSpan="1">Accept PayPal Express 
															Checkout:</TD>
														<TD class="content" align="left" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkBoxExpress" Runat="server"></asp:checkbox></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" align="right" width="6" colSpan="1"><IMG src="images/clear.gif"></TD>
														<TD class="content" align="right" width="215" colSpan="1">Certificate Type:</TD>
														<TD class="content" align="left" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:radiobutton id="firstParty" Runat="server" Text="First Party" GroupName="PayPalCertficateType"
																OnCheckedChanged="PayPalCertificateTypeChanged" AutoPostBack="True"></asp:radiobutton></TD>
														<TD class="content" align="left" colSpan="1"><asp:radiobutton id="thirdParty" Runat="server" Text="Third Party" GroupName="PayPalCertficateType"
																OnCheckedChanged="PayPalCertificateTypeChanged" AutoPostBack="True"></asp:radiobutton></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<asp:panel id="pnlThirdParty" Runat="server">
														<TR>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="content" align="right" width="6" colSpan="1"><IMG src="images/clear.gif"></TD>
															<TD class="content" align="right" width="215" colSpan="1">PayPal E-Mail ID:</TD>
															<TD class="content" align="left" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:textbox id="txtPayPalEmailID" runat="server" Width="175px" MaxLength="100"></asp:textbox></TD>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="content" align="right" width="6" colSpan="1"><IMG src="images/clear.gif"></TD>
															<TD class="content" align="right" width="215" colSpan="1">Third Party Certificate 
																Configuration File Path:</TD>
															<TD class="content" align="left" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:textbox id="txtthirdpartycertfilepath" runat="server" Width="300px"></asp:textbox></TD>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
													</asp:panel><asp:panel id="pnlFirstParty" Visible="False" Runat="server">
														<TR>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="content" align="right" width="6" colSpan="1"><IMG src="images/clear.gif"></TD>
															<TD class="content" align="right" width="215" colSpan="1">PayPal Merchant ID:</TD>
															<TD class="content" align="left" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:textbox id="PPMerchantID" runat="server" Width="152px" MaxLength="100"></asp:textbox></TD>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="content" align="right" width="6" colSpan="1"><IMG src="images/clear.gif"></TD>
															<TD class="content" align="right" width="215" colSpan="1">PayPal Merchant Password:</TD>
															<TD class="content" align="left" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:textbox id="PPMerchantPWD" runat="server" Width="152px" MaxLength="100" TextMode="Password"></asp:textbox></TD>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="content" align="right" width="6" colSpan="1"><IMG src="images/clear.gif"></TD>
															<TD class="content" align="right" width="215" colSpan="1">&nbsp;Private-Key 
																Password:</TD>
															<TD class="content" align="left" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:TextBox id="PPPrivateKeyPWD" Runat="server" Width="152px" MaxLength="100" TextMode="Password"></asp:TextBox></TD>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
															<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
															<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														</TR>
													</asp:panel>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" align="right" width="6" colSpan="1"><IMG src="images/clear.gif"></TD>
														<TD class="content" align="right" width="215" colSpan="1">Payment Type:</TD>
														<TD class="content" align="left" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:radiobutton id="PayPalAuthOnly" Runat="server" Text="Authorize" GroupName="PayPalAuth"></asp:radiobutton></TD>
														<TD class="content" align="left" colSpan="1"><asp:radiobutton id="PayPalSale" Runat="server" Text="Sale" GroupName="PayPalAuth"></asp:radiobutton></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
														<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<tr>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td class="ContentTableHeader" width="6"><IMG src="images/clear.gif" width="1"></td>
													</tr>
												</table>
											</td>
											<td class="contenttableheader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="contenttableheader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" colSpan="7" height="10"><IMG height="10" src="images/clear.gif"></td>
											<td class="contenttableheader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<TR>
											<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<TD class="Content" colSpan="6"><IMG src="images/clear.gif" width="1"></TD>
											<td class="content" align="center">
											<asp:linkbutton id="cmdSave" Runat="server">
													<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="images/save.jpg" AlternateText="Save"></asp:Image>
												</asp:linkbutton></td>
											<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
										</TR>
										<tr>
											<td class="contenttableheader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" colSpan="7" height="10"><IMG height="10" src="images/clear.gif"></td>
											<td class="contenttableheader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!-- Content End --> </TD></TR></TBODY></TABLE></form>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="PaymentMethodsControl" Src="Controls/PaymentMethodsControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Encryption.aspx.vb" Inherits="StoreFront.StoreFront.Encryption" validateRequest="False" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Import Namespace="Storefront.systembase"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
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
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server">
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
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/encrypt_ov.asp ')"><IMG src="images/help.jpg" border="0"></A>
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
											<td class="Content" align="center"><%= IIF(restrictedPages(tasks.onlineprocessing)=true,"<span class=DisableLink>Online Processors</span>","<a class=content href='PaymentProcessors.aspx'>Online Processing</a>")%>
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">&nbsp;&nbsp;<A class="content" href="Encryption.aspx"><b>Encryption</b></A>&nbsp;&nbsp;
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">&nbsp;&nbsp;&nbsp;&nbsp;<%= IIF(restrictedPages(tasks.paypal)=true,"<span class=DisableLink>PayPal</span>","<a class=content href='PayPal.aspx'>PayPal</a>")%>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="content" align="center" colSpan="7">
												<br>
												<!-- Encryption Table Starts -->
												<table cellSpacing="0" cellPadding="0" width="97%" border="0" class="ContentTable">
													<tr>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td class="ContentTableHeader">Generate Keys</td>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
													</tr>
													<tr>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td class="content" align="center">
															<asp:panel id="pnlKeyOptions" Runat="server">
																<TABLE class="content" width="100%">
																	<TR>
																		<TD class="Content">
																			<P>In Order to secure access to full credit card numbers, an encryption key must be 
																				entered by the merchant when accessing order data. Use the tool below to 
																				generate the encryption key for your store.</P>
																		</TD>
																	</TR>
																	<TR>
																		<TD class="Content">
																			<TABLE>
																				<TR>
																					<TD class="Content">
																						<asp:RadioButton id="rbNewKey" Runat="server" GroupName="key" Text="Generate a New Key"></asp:RadioButton></TD>
																				</TR>
																				<TR>
																					<TD class="Content">
																						<P>&nbsp;&nbsp;&nbsp;&nbsp; Select this option if you are generating a key for the 
																							first time or if you have lost your encryption key</P>
																					</TD>
																				</TR>
																			</TABLE>
																	<TR>
																		<TD class="Content">
																			<TABLE>
																				<TR>
																					<TD class="Content">
																						<asp:RadioButton id="rbReplaceKey" Runat="server" GroupName="key" Text="Replace an Existing Key"></asp:RadioButton></TD>
																				</TR>
																				<TR>
																					<TD class="Content">
																						<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select this option to replace a previous key for 
																							security reasons. You must enter the previous encryption key below.</P>
																					</TD>
																				</TR>
																			</TABLE>
																		</TD>
																	</TR>
																	<TR>
																		<TD class="Content">
																			<asp:panel id="pnlUploadKey" Runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Encryption 
                              Key: <INPUT class="content" id="filename" type="file" name="filename" runat="server"></asp:panel></TD>
																	</TR>
																	<TR>
																		<TD class="Content" height="5"></TD>
																	<TR>
																		<TD align="center">
																			<TABLE class="ContentTable" cellSpacing="1" cellPadding="3" width="99%">
																				<TR>
																					<TD class="Content">
																						<asp:CheckBox id="chkconfirm" Runat="server" Text="Confirmation- You must check this box to continue. Please read the warning below carefully before checking this box"></asp:CheckBox>
																						<P>When generating a replacement key, Storefront will attempt to convert your old 
																							credit card number to use the new encryption key. If your previous credit card 
																							numbers were encrypted with a private key, you must provide that key in order 
																							to successfully convert the numbers. If you do not provide the previous key 
																							encryption key or if you are creating a new key, access to existing credit card 
																							numbers will no longer be available.
																						</P>
																					</TD>
																				</TR>
																			</TABLE>
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content" align="right">
																			<asp:LinkButton id="lnkSubmit" Runat="server">
																				<img src="Images\Submit.jpg" border="0"></asp:LinkButton></TD>
																	</TR>
																</TABLE>
															</asp:panel></td>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
													</tr>
													<tr>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td><asp:panel id="pnlDownloadKey" Visible="False" Runat="server">
																<TABLE class="Content" width="100%">
																	<TR>
																		<TD class="content">
																			<P>Your Encryption key has been successfully generated and is included in a 
																				temporary text file. Click the link below to download the file. Once the file 
																				is downloaded, it will be deleted from the system. For security reasons, the 
																				file is not stored on the web server or in your web store.</P>
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content"><IMG src="images\clear.gif" border="0">
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content" align="center">
																			<asp:linkbutton id="lnkdownloadkey" Runat="server">Download Encryption File</asp:linkbutton></TD>
																	</TR>
																	<TR>
																		<TD class="content"><IMG src="images\clear.gif" border="0">
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content">
																			<P>If you have problems downloading the encryption key file, please copy the key 
																				(shown below) to a text file.</P>
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content"><IMG src="images\clear.gif" border="0">
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content" vAlign="middle" align="center" width="100%">Encryption Key:
																			<BR>
																			<asp:TextBox id="txtKey" Runat="server" Height="156px" Width="499px" TextMode="MultiLine"></asp:TextBox></TD>
																	</TR>
																	<TR>
																		<TD class="content"><IMG src="images\clear.gif" border="0">
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content">
																			<P>IMPORTANT: You will be required to enter this key to access full credit card 
																				numbers through your merchant tools. Please store the file in a secure 
																				location. If you lose the file, you will not be able to access exisiting credit 
																				card numbers in your web store.</P>
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content"><IMG src="images\clear.gif" border="0">
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content">
																			<P>To complete the setup of your encryption keys, please confirm that you have 
																				successfully downloaded or copied the key. By clicking the Submit button, you 
																				authorize StoreFront to convert all stored credit card data to use the new 
																				encryption keys.</P>
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content"><IMG src="images\clear.gif" border="0">
																		</TD>
																	</TR>
																	<TR>
																		<TD class="content" align="right">
																			<asp:LinkButton id="lnkCancel" Runat="server">
																				<img src="images\Cancel.jpg" border="0"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																			<asp:LinkButton id="lnkConfirmKey" Runat="server">
																				<img src="images\Submit.jpg" border="0">
																			</asp:LinkButton></TD>
																	</TR>
																</TABLE>
															</asp:panel>
														</td>
														<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
													</tr>
													<tr>
														<td class="ContentTableHeader" height="1" colspan="3"><IMG src="images/clear.gif" width="1"></td>
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

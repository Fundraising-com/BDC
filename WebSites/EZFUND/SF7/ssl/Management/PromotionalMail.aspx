<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StandardSearchControl" Src="Controls/StandardSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="editemaildynamic" Src="Controls/editemaildynamic.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PromotionalMail.aspx.vb" ValidateRequest="False" Inherits="StoreFront.StoreFront.PromotionalMail" codePage="1252"%>
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
		<meta charset="Windows-1252" http-equiv="Content-Type" content="text/html; charset=Windows-1252">
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="ace.js"></script>
		<style>.tblCoolbar { BORDER-RIGHT: buttonshadow 1px solid; PADDING-RIGHT: 1px; BORDER-TOP: buttonhighlight 1px solid; PADDING-LEFT: 1px; PADDING-BOTTOM: 1px; BORDER-LEFT: buttonhighlight 1px solid; COLOR: menutext; PADDING-TOP: 1px; BORDER-BOTTOM: buttonshadow 1px solid; BACKGROUND-COLOR: #e2e2e2 }
	.bar { BORDER-TOP: #99ccff 1px solid; BACKGROUND: #316ac5; WIDTH: 100%; BORDER-BOTTOM: #000000 1px solid; HEIGHT: 20px }
	TD { FONT: 8pt verdana,arial,sans-serif }
	DIV { FONT: 10pt tahoma,arial,sans-serif }
		</style>
		<script language="JavaScript">
				function errorsuppressor(){
					return true;
				}
				window.onerror=errorsuppressor
				
				//Tee 10/17/2007 extract text
				function ChangeFormat(id){
				    if (document.getElementById("idContent") != null){
				        document.getElementById("hdnText").value = document.getElementById("idContentTemp").innerText;
				    }
				}
				//end Tee
		</script>
		<script language="JavaScript">
			
				//5.a. DEFINE YOUR BASE URL (Used for constructing relative
				//path instead of the default complete path when inserting 
				//links or images)
				
				//Example :
				
				//If this is the location of your editor :
				//	http://localhost/ContentEditor/folder01/folder02/default.asp
				//	then use :
				//	var sBaseUrl = "http://localhost/ContentEditor/";
				//	var sBaseUrlNew = "../../";
				//	
				//If this is the location of your editor :
				//	http://localhost/ContentEditor/folder01/default.asp
				//	then use :
				//	var sBaseUrl = "http://localhost/ContentEditor/";
				//	var sBaseUrlNew = "../";
			
				//var sBaseUrl = "http://localhost/Twisterweb/ssl/management/";
				//var sBaseUrlNew = "http://localhost/Twisterweb";
				//var sEMailType;		
				//var sBaseUrl=document.Form1.hdnBaseURL.value;
				//var sBaseUrlNew = document.Form1.hdnBaseURLNew.value;
				//alert(sBaseURL);
				
				//5.b. PREFILL YOUR EDITOR
				//		
					
				function LoadDoc()
					{
						idContent.document.body.innerHTML = idContentTemp.innerHTML;
						sEMailType = document.Form1.hdnEMailType.value;
					}		
				function Send2()
					{	
						if (window.document.Form1.elements["ddDateRange"].options[window.document.Form1.elements["ddDateRange"].options.selectedIndex].value == "0")
						{window.document.Form1.elements["txtFrom"].required=true;
						window.document.Form1.elements["txtTo"].required=true;
						window.document.Form1.elements["txtFrom"].date=true;
						window.document.Form1.elements["txtFrom"].title="Date From";
						window.document.Form1.elements["txtTo"].date=true;
						window.document.Form1.elements["txtTo"].title="Date To";
						
						}
						else
						{//window.document.Form1.elements["txtFrom"].required=false;
						//window.document.Form1.elements["txtTo"].required=false;
						}
						//window.document.Form1.elements["txtFrom"].date=true;
						//window.document.Form1.elements["txtFrom"].title="Date From";
						//window.document.Form1.elements["txtTo"].date=true;
						//window.document.Form1.elements["txtTo"].title="Date To";
	
				
						if (ValidateForm(window.document.Form1))
						{
							window.document.Form1.elements["hdnAction"].value="send2";
							if (window.document.Form1.elements["ddFormat"].options[window.document.Form1.elements["ddFormat"].options.selectedIndex].value == "TEXT")
							{
								window.document.Form1.elements["hdnText"].value=window.document.Form1.elements["txtBody"].value;
							}
							else
							{
								if (document.all != null)
									window.document.Form1.elements["hdnText"].value=idContent.document.body.innerHTML;
								else
									window.document.Form1.elements["hdnText"].value=window.document.Form1.elements["txtBody"].value;
							}
							window.document.Form1.submit();
							window.document.Form1.elements["hdnAction"].value="";
						}
						
					}
					
						
				function Send()
					{	
						if (window.document.Form1.elements["ddDateRange"].options[window.document.Form1.elements["ddDateRange"].options.selectedIndex].value == "0")
						{window.document.Form1.elements["txtFrom"].required=true;
						window.document.Form1.elements["txtTo"].required=true;
						window.document.Form1.elements["txtFrom"].date=true;
						window.document.Form1.elements["txtFrom"].title="Date From";
						window.document.Form1.elements["txtTo"].date=true;
						window.document.Form1.elements["txtTo"].title="Date To";
						
						}
						else
						{//window.document.Form1.elements["txtFrom"].required=false;
						//window.document.Form1.elements["txtTo"].required=false;
						}
						//window.document.Form1.elements["txtFrom"].date=true;
						//window.document.Form1.elements["txtFrom"].title="Date From";
						//window.document.Form1.elements["txtTo"].date=true;
						//window.document.Form1.elements["txtTo"].title="Date To";
	
				
						if (ValidateForm(window.document.Form1))
						{
							window.document.Form1.elements["hdnAction"].value="send";
							if (window.document.Form1.elements["ddFormat"].options[window.document.Form1.elements["ddFormat"].options.selectedIndex].value == "TEXT")
							{
								window.document.Form1.elements["hdnText"].value=window.document.Form1.elements["txtBody"].value;
							}
							else
							{
								if (document.all != null)
									window.document.Form1.elements["hdnText"].value=idContent.document.body.innerHTML;
								else
									window.document.Form1.elements["hdnText"].value=window.document.Form1.elements["txtBody"].value;
							}
							window.document.Form1.submit();
							window.document.Form1.elements["hdnAction"].value="";
						}
						
					}	
				
				function ValidatePromoMail()
				{
					if (window.document.Form1.elements["txtPromoMailServer"].value  == window.document.Form1.elements["hdnPromoServer"].value) 
					{
						return confirm("You have specified the same server as your regular mail server. Most hosts require that promotional mail be sent from a special server. Please check with your host to get the correct address, or click yes to continue to use your normal mail server. Are you sure you wish to use this server ?");
					}
					return true;
				}
						
		</script>
		<script language="JavaScript" src="../General.js"></script>
		<script language="vbscript">
				function vbReplace(sInput,sFind,sReplacement)
					vbReplace = replace(LCase(sInput),LCase(sFind),LCase(sReplacement))
				end function
		</script>
	</HEAD>
	<body class="GeneralPage" style="FONT: 8pt verdana,arial,sans-serif" onload="initEditor()">
		<form id="Form1" name="Form1" action="PromotionalMail.aspx" method="post" runat="server">
			<input id=hdnBaseURLNew type=hidden 
value='<%#DataBinder.Eval(me, "BaseURLNew")%>' name=hdnBaseURLNew> <input id=hdnBaseURL type=hidden 
value='<%#DataBinder.Eval(me, "BaseURL")%>' name=hdnBaseURL>&nbsp;&nbsp; <INPUT id="hdnPromoServer" type="hidden" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<tr>
					<td class="TopBanner" colSpan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Promotional E-mail"></uc1:TopSubBanner>
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top" width="100%">
						<!-- Content Start -->
						<table cellSpacing="3" cellPadding="5" width="100%" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/promotional_email.asp ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<TR>
								<TD class="content" vAlign="top" align="center">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<!--<tr>
											<td class="ContentTableHeader" align="left">&nbsp;<asp:label id="lblPDName" Runat="server">Compose Promotional EMail</asp:label></td>
										</tr>-->
										<TR>
											<TD class="content" vAlign="top" align="center" width="100%">
												<table class="content" width="100%" border="0">
													<tr>
														<td class="content" align="center" colSpan="2">
															<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></P>
														</td>
													</tr>
													<!-- Table for Storing Bulk Mail server -->
													<tr>
														<td class="content" width="100%" colspan="2">
															<table class="Content" cellspacing="0" cellpadding="0" width="100%" border="0">
																<tr>
																	<td class="ContentTableHeader" width="1" style="HEIGHT: 16px"><IMG src="images/clear.gif" width="1"></td>
																	<td class="ContentTableHeader" colSpan="3" style="HEIGHT: 16px">&nbsp;&nbsp;Mail 
																		Server to use for Promo Mail</td>
																	<td class="ContentTableHeader" width="1" style="HEIGHT: 16px"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="center" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" style="HEIGHT: 17px" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="content" style="HEIGHT: 17px" align="middle"><asp:textbox id="txtPromoMailServer" Runat="server" Columns="55"></asp:textbox>&nbsp;&nbsp;<A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/promomail_ov.asp#server')"> What's this ?</A></td>
																	<td class="content" style="HEIGHT: 17px" colSpan="2"><asp:LinkButton ID="lnkPromoServerSave" Runat="server"><img border="0" src="images\Save.jpg"></asp:LinkButton>
																	</td>
																	<td class="ContentTableHeader" style="HEIGHT: 17px" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="center" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1" colSpan="5"><IMG src="images/clear.gif"></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td colspan="2">
															<table class="Content" cellSpacing="0" cellPadding="0" width="100%" border="0">
																<tr>
																	<td class="ContentTableHeader" width="1" style="HEIGHT: 16px"><IMG src="images/clear.gif" width="1"></td>
																	<td class="ContentTableHeader" colSpan="3" style="HEIGHT: 16px">&nbsp;&nbsp;E-Mail 
																		Content</td>
																	<td class="ContentTableHeader" width="1" style="HEIGHT: 16px"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="center" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="content" align="right">Format:&nbsp;&nbsp;</td>
																	<td class="content" colSpan="2"><asp:dropdownlist class="content" id="ddFormat" AutoPostBack="true" Runat="server">
																			<asp:ListItem Value="TEXT" Selected>Text</asp:ListItem>
																			<asp:ListItem Value="HTML">Html</asp:ListItem>
																		</asp:dropdownlist></td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="center" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" style="HEIGHT: 17px" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="content" style="HEIGHT: 17px" align="right">Subject:&nbsp;&nbsp;</td>
																	<td class="content" style="HEIGHT: 17px" colSpan="2"><asp:textbox id="txtSubject" Runat="server" Columns="55">[CustomerFirstName], now is the time to visit [StoreName]</asp:textbox></td>
																	<td class="ContentTableHeader" style="HEIGHT: 17px" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="center" colSpan="3">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="content" vAlign="top" align="right">Body:&nbsp;</td>
																	<td class="content" vAlign="top" noWrap colSpan="2">
																		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<tr>
																				<td class="content" vAlign="top" noWrap width="100%"><asp:textbox id=txtBody Runat="server" Columns="55" Text='<%# DataBinder.Eval(me, "TextBody") %>' TextMode="MultiLine" rows="10">
																					</asp:textbox></td>
																				<td class="content" vAlign="top" align="left" noWrap><asp:label id="lblPanel" runat="server">Label</asp:label></td>
																			</tr>
																		</table>
																		<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<tr>
																				<td class="content"><uc1:editemaildynamic id="Editemaildynamic1" runat="server"></uc1:editemaildynamic></td>
																				<td>&nbsp;</td>
																			</tr>
																		</table>
																		<div id="idContentTemp" style="DISPLAY: none"><%# DataBinder.Eval(me, "TextBody") %></div>
																	</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" colSpan="3"></td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content">&nbsp;</td>
																	<td colSpan="2"><asp:linkbutton id="btnClear" Runat="server">
																			<asp:Image BorderWidth="0" ID="imgClear" runat="server" ImageUrl="images/Clear.jpg" AlternateText="Clear"></asp:Image>
																		</asp:linkbutton>
																	</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1" colSpan="5"><IMG src="images/clear.gif"></td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
												<table class="content" width="100%" border="0">
													<tr>
														<td colspan="2">&nbsp;</td>
													</tr>
													<tr>
														<td colSpan="2">
															<table class="Content" cellSpacing="0" cellPadding="0" width="100%" border="0">
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="ContentTableHeader" colSpan="3">&nbsp;&nbsp;Mailing List</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="center" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" colSpan="3">&nbsp;&nbsp;Send&nbsp;To&nbsp;Customers&nbsp;Who&nbsp;Purchased:&nbsp;
																		<asp:dropdownlist id="ddDateRange" AutoPostBack="True" Runat="server">
																			<asp:ListItem Value="14">Within The Last Two Weeks</asp:ListItem>
																			<asp:ListItem Value="30">Within The Last 30 Days</asp:ListItem>
																			<asp:ListItem Value="60">Within The Last 60 Days</asp:ListItem>
																			<asp:ListItem Value="90">Within The Last 90 Days</asp:ListItem>
																			<asp:ListItem Value="0">Enter A Date Range</asp:ListItem>
																		</asp:dropdownlist></td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="center" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr id="trFromToRow" runat="server">
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" colSpan="3">&nbsp;&nbsp;From:&nbsp;<asp:textbox id="txtFrom" Runat="server" Width="85"></asp:textbox>
																		&nbsp;&nbsp;To:&nbsp;<asp:textbox id="txtTo" Runat="server" Width="85"></asp:textbox></td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="center" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="center" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" colSpan="3">&nbsp;&nbsp;Send&nbsp;To&nbsp;Customers&nbsp;Who&nbsp;Purchased&nbsp;Selected&nbsp;Product(s)</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" colSpan="3">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="left" colSpan="2">&nbsp;&nbsp;
																		<asp:linkbutton id="btnSelectProducts" Runat="server">
																			<asp:Image BorderWidth="0" ID="imgSelectProducts" runat="server" ImageUrl="images/select.jpg"
																				AlternateText="Select Products"></asp:Image>
																		</asp:linkbutton></td>
																	<td class="Content" align="right"><A href="javascript:Send()"><IMG id="btnSend" alt="Send" src="images/send.jpg" border="0"></A>&nbsp;&nbsp;&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1" colSpan="5"><IMG src="images/clear.gif"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" align="left" colspan="2">&nbsp;&nbsp;&nbsp;Send&nbsp;To&nbsp;All&nbsp;Subscribers</td>
																	<td class="Content" align="right"><A href="javascript:Send2()"><IMG id="btnSend2" alt="Send" src="images/send.jpg" border="0"></A>&nbsp;&nbsp;&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																	<td class="Content" colSpan="3" height="10">&nbsp;</td>
																	<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
																</tr>
																<tr>
																	<td class="ContentTableHeader" width="1" colSpan="5"><IMG src="images/clear.gif"></td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</TD>
										</TR>
									</table>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE><input id="hdnEMailType" type="hidden" value="PromoMail" name="hdnEMailType" runat="server">
			<input id="hdnAction" type="hidden" name="hdnAction" runat="server"> <input id="hdnText" type="hidden" name="hdnText" runat="server">
		</form>
		</TD></TR></TABLE>
	</body>
</HTML>

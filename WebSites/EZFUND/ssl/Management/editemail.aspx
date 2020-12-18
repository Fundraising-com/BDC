<%@ Register TagPrefix="uc1" TagName="StandardSearchControl" Src="Controls/StandardSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="editemaildynamic" Src="Controls/editemaildynamic.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EditEMail.aspx.vb" Inherits="StoreFront.StoreFront.EditEMail" codePage="1252"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Edit Email</title>
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
		<meta charset="Windows-1252" http-equiv="Content-Type" content="text/html; charset=Windows-1252">
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
		<script language="JavaScript" src="ace.js"></script>
		<style>
			.tblCoolbar { BORDER-RIGHT: buttonshadow 1px solid; PADDING-RIGHT: 1px; BORDER-TOP: buttonhighlight 1px solid; PADDING-LEFT: 1px; PADDING-BOTTOM: 1px; BORDER-LEFT: buttonhighlight 1px solid; COLOR: menutext; PADDING-TOP: 1px; BORDER-BOTTOM: buttonshadow 1px solid; BACKGROUND-COLOR: #e2e2e2 }
			.bar { BORDER-TOP: #99ccff 1px solid; BACKGROUND: #316ac5; WIDTH: 100%; BORDER-BOTTOM: #000000 1px solid; HEIGHT: 20px }
			TD { FONT: 8pt verdana,arial,sans-serif }
			DIV { FONT: 10pt tahoma,arial,sans-serif }
		</style>
		<script language="JavaScript">
				function errorsuppressor(){
					return true;
				}
				window.onerror=errorsuppressor
		</script>
		<script language="JavaScript">
				/*
				5.a. DEFINE YOUR BASE URL (Used for constructing relative
				path instead of the default complete path when inserting 
				links or images)
				
				Example :
				
				If this is the location of your editor :
					http://localhost/ContentEditor/folder01/folder02/default.asp
					then use :
					var sBaseUrl = "http://localhost/ContentEditor/";
					var sBaseUrlNew = "../../";
					
				If this is the location of your editor :
					http://localhost/ContentEditor/folder01/default.asp
					then use :
					var sBaseUrl = "http://localhost/ContentEditor/";
					var sBaseUrlNew = "../";
				*/
		
				//var sBaseURL = Form1.hdnSSLPath.value + "/management/;
				//var sBaseUrlNew = Form1.hdnRootPath.value;
													
				//var sBaseUrl = "http://localhost/Twisterweb/ssl/management/";
				//var sBaseUrlNew = "http://localhost/Twisterweb";
				var sEMailType;		
				var sBaseUrl=document.Form1.hdnBaseURL.value;
				var sBaseUrlNew = document.Form1.hdnBaseURLNew.value;
				
																				
				/*
				5.b. PREFILL YOUR EDITOR
				*/				
				function LoadDoc()
					{	
						idContent.document.body.innerHTML = idContentTemp.innerHTML;
						sEMailType = Form1.hdnEMailType.value;
					}

				/*
				5.c. HANDLE THE SAVE ACTION
				*/	
				function Save()
					{
					
					if (displayMode == "HTML"){alert("Please uncheck 'HTML view'");return false;}//this is a must
					else{
					//Transfer the edited content to the form
						//window.document.forms		
					Form1.hdnAction.value="save";
					Form1.hdnText.value=idContent.document.body.innerHTML;
					Form1.submit();
					Form1.hdnAction.value="";	
					//idContent.document.body.innerHTML = idContentTemp.innerHTML;
												
					}
			}		
		</script>
		<script language="vbscript">
				function vbReplace(sInput,sFind,sReplacement)
					vbReplace = replace(LCase(sInput),LCase(sFind),LCase(sReplacement))
				end function
		</script>
		<!-- /STEP 5 -->
		<meta http-equiv="Pragma" content="no-cache">
	</HEAD>
	<body class="GeneralPage" id="BodyTag" style="FONT: 8pt verdana,arial,sans-serif" onload="initEditor()">
		<form id="Form1" name="Form1" action="EditEMail.aspx" method="post" runat="server">
			<input type="hidden" id="hdnBaseURLNew" name="hdnBaseURLNew" value='<%#DataBinder.Eval(me, "BaseURLNew")%>'>
			<input type="hidden" id="hdnBaseURL" name="hdnBaseURL" value='<%#DataBinder.Eval(me, "BaseURL")%>'>
			<table class="GeneralTable" cellSpacing="0">
				<TBODY>
					<tr>
						<td class="TopBanner" colSpan="3">
							<!-- Top Banner Start -->
							<table cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td class="TopBanner2" width="20%"><IMG src="images/sflogo.jpg"></td>
									<td class="TopBanner">&nbsp;&nbsp;Merchant Tools</td>
								</tr>
							</table>
							<!-- Top Banner End --></td>
					</tr>
					<tr>
						<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
							<!-- Top Sub Banner Start --> E-Mail Management 
							<!-- Top Sub Banner End --></td>
					</tr>
					<tr>
						<td class="LeftColumn" id="LeftColumnCell">
							<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
							<!-- Left Column End --></td>
						<td class="Content" vAlign="top">
							<!-- Content Start -->
							<table cellSpacing="3" cellPadding="5" width="100%" border="0">
								<TBODY>
									<tr>
										<td class="Content" align="right">
											<!-- Help Button -->
											<A href="javascript: doHelp(' http://support.storefront.net/mtdocs/email_ov.asp ')">
												<IMG src="images/help.jpg" border="0"></A> 
											<!-- End Help Button --></td>
									</tr>
									<tr>
										<td class="Content">
											<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
											<!-- Instruction End --></td>
									</tr>
									<tr>
										<td class="content" align="middle"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></font></td>
									</tr>
									<tr>
										<td>
											<table cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<td class="ContentTable" width="100%" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
												</tr>
												<tr>
													<td class="Content" width="100%" colSpan="5"><uc1:admintabcontrol id="AdminTabControl1" runat="server"></uc1:admintabcontrol></td>
												</tr>
												<tr>
													<td class="ContentTable" width="100%" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
												</tr>
											</table>
											<asp:panel id="pnlSubmenu" runat="server" Visible="True">
												<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="Content" noWrap width="100%">&nbsp;&nbsp; E-mail&nbsp;To:&nbsp;
															<asp:linkbutton id="lnkWishList" onclick="lnkWishList_Click" runat="server">WishList</asp:linkbutton>&nbsp;
															<asp:linkbutton id="lnkCustomer" runat="server">Customer</asp:linkbutton>&nbsp;
															<asp:linkbutton id="lnkVendor" runat="server">Vendor</asp:linkbutton>&nbsp;
															<asp:linkbutton id="lnkMerchant" runat="server">Merchant</asp:linkbutton>
														&nbsp;
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="100%" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</TR>
													<TR>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
														<TD class="Content" noWrap width="100%">&nbsp;&nbsp; Dynamic&nbsp;Section(s):&nbsp;
															<asp:linkbutton id="lnkWishListComponents" onclick="lnkWishListComponent_Click" runat="server">WishList Components</asp:linkbutton>&nbsp;
															<asp:linkbutton id="lnkShipping" runat="server">Shipping</asp:linkbutton>&nbsp;
															<asp:linkbutton id="lnkBilling" runat="server">Billing</asp:linkbutton>&nbsp;
															<asp:linkbutton id="lnkProducts" runat="server">Products</asp:linkbutton>&nbsp;
															<asp:linkbutton id="lnkOrderTotal" runat="server">Order Total</asp:linkbutton></TD>
														<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													</TR>
													<TR>
														<TD class="ContentTable" width="100%" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
													</TR>
												</TABLE>
											</asp:panel>
											<table cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<td class="Content"><p id="ErrorAlignment" runat="server">&nbsp;</p>
													</td>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												</tr>
												<tr>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<td class="Content" align="middle">
														<table id="Table3" cellSpacing="0" cellPadding="0" width="100%">
															<tr>
																<td class="ContentTableHeader">
																	<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center" runat="server">
																		<TR>
																			<TD class="ContentTableHeader" align="middle">&nbsp;<asp:label id="lblEmailType" Runat="server"></asp:label></TD>
																		</TR>
																		<TR>
																			<td>
																				<table id="Table2" cellSpacing="0" cellPadding="8" width="100%" align="center" runat="server">
																					<tr>
																						<TD class="Content" align="right" width="15%"><asp:label id="lblSubject" Runat="server"></asp:label></TD>
																						<TD class="Content" noWrap><asp:textbox id="txtSubject" runat="server" Columns="65"></asp:textbox></TD>
																					</tr>
																					<tr>
																						<TD class="Content" align="right" width="15%">E-Mail Format</TD>
																						<TD class="Content" noWrap><asp:dropdownlist id="ddFormat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="EmailType_ChangeClick">
																								<asp:ListItem Value="Text">Plain Text</asp:ListItem>
																								<asp:ListItem Value="HTML">HTML</asp:ListItem>
																							</asp:dropdownlist></TD>
																					</tr>
																					<tr>
																						<TD class="Content" vAlign="top" align="right" width="15%">Message</TD>
																						<TD class="Content" noWrap>
																							<table cellSpacing="0" cellPadding="0" width="100%">
																								<tr>
																									<td><asp:textbox class="Content" id="txtMessage" runat="server" Columns="60" TextMode="MultiLine" Rows="20"></asp:textbox></td>
																									<td vAlign="top" width="40%"><asp:label id="lblPanel" runat="server">Label</asp:label></td>
																								</tr>
																							</table>
																							<uc1:editemaildynamic id="Editemaildynamic1" runat="server"></uc1:editemaildynamic></TD>
																					</tr>
																					<tr>
																						<TD class="Content"></TD>
																						<TD class="Content" align="left" height="1">
																							<asp:LinkButton ID="btnSave" Runat="server">
																								<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="images/Save.jpg" AlternateText="Save"></asp:Image>
																							</asp:LinkButton>
																						</TD>
																					</tr>
																				</table>
																			</td>
																		</TR>
																	</table>
																</td>
															</tr>
														</table>
													</td>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												</tr>
												<tr>
													<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<td class="Content">&nbsp;</td>
													<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
												</tr>
												<tr>
													<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
												</tr>
											</table>
											<div id="idContentTemp" style="DISPLAY: none"><%# DataBinder.Eval(me, "HTMLBody") %></div>
											<asp:label id="lblEmailTypeHidden" Visible="False" Runat="server"></asp:label><input 
            id=hdnSSLPath type=hidden 
            value='<%#DataBinder.Eval(me, "sslPath") %>' 
            runat="server"> <input id=hdnRootPath type=hidden value='<%#DataBinder.Eval(me, "RootPath") %>' runat="server"> <input id="hdnAction" type="hidden" runat="server">
											<input id="hdnText" type="hidden" runat="server"> <input id="hdnEMailType" type="hidden" runat="server">
		</form>
		</TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
	</body>
</HTML>

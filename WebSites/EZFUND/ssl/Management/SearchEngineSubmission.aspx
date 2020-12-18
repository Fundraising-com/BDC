<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Page Language="vb" autoeventwireup="false" codebehind="SearchEngineSubmission.aspx.vb" Inherits="StoreFront.StoreFront.SearchEngineSubmission" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Search Engine Submission</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.0.0

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
		<form id="Form1" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<!-- Top Banner Start -->
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td class="TopBanner2" width="20%"><img src="images/sflogo.jpg"></td>
								<td class="TopBanner">&nbsp;&nbsp;Merchant Tools</td>
							</tr>
						</table>
						<!-- Top Banner End -->
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colspan="3">
						<!-- Top Sub Banner Start --> Search Engines 
						<!-- Top Sub Banner End -->
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" valign="top">
						<!-- Content Start -->
						<table cellspacing="3" cellpadding="5" width="100%" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs/searcheng_ov.asp  ')">
										<img src="images/help.jpg" border="0"></a> 
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
									<p id="ErrorAlignment" runat="server">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</p>
								</td>
							</tr>
							<tr>
								<td class="Content">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="ContentTableHeader" colspan="2">&nbsp;Meta Tags</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content">&nbsp;</td>
											<td class="Content">&nbsp;</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" colspan="2">
												<p align="center">
													<asp:label id="lblConfirmation" runat="server" visible="False" CssClass="Messages"></asp:label>
													<asp:validationsummary id="valSummary" runat="server"></asp:validationsummary></p>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="content" valign="top" align="right">Keywords:<br>
												(seperate by comma)</td>
											<td class="content" align="left" valign="top">&nbsp;<asp:textbox id="txtKeywords" runat="server" rows="3" columns="50" textmode="MultiLine" CssClass="content"></asp:textbox><asp:requiredfieldvalidator id="valKeywords" runat="server" controltovalidate="txtKeywords" errormessage="Keywords is a required field." display="Static">*</asp:requiredfieldvalidator>
												<asp:RequiredFieldValidator id="Keywords_RequiredFieldValidator" runat="server" Width="68px" ControlToValidate="txtKeywords" Display="None"></asp:RequiredFieldValidator></td>
											<td class="ContentTableHeader" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content" nowrap align="left" colspan="2">&nbsp;</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" valign="top" align="right">Description:</td>
											<td class="Content" align="left" colSpan="1" rowSpan="1" valign="top">&nbsp;<asp:textbox id="txtDescription" runat="server" rows="3" columns="50" textmode="MultiLine" CssClass="content"></asp:textbox><asp:requiredfieldvalidator id="valDescription" runat="server" controltovalidate="txtDescription" errormessage="Description is a required field." display="Static">*</asp:requiredfieldvalidator>
												<asp:RequiredFieldValidator id="Desc_RequiredFieldValidator" runat="server" ControlToValidate="txtDescription" Display="None"></asp:RequiredFieldValidator></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1">
											</td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content" nowrap align="left" colspan="2">&nbsp;</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content" align="right" colspan="2">
												<asp:LinkButton ID="btnSubmit" Runat="server">
													<asp:Image BorderWidth="0" ID="imgSubmit" runat="server" ImageUrl="images/submit.jpg" AlternateText="Submit"></asp:Image>
												</asp:LinkButton>
												&nbsp;</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content" nowrap align="left" colspan="2">&nbsp;</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colspan="4" height="1" width="555">
												<img height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="Content">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="ContentTableHeader" colspan="3">&nbsp;Search Engines</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" colspan="3">
												<table cellspacing="0" border="0" cellpadding="5" width="100%">
													<tr>
														<td class="Content">
															<asp:label id="lblInfo" runat="server" visible="False"></asp:label>
															<p><br>
															</p>
														</td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Headings" colspan="3">&nbsp;<b>Crawlers (Free Basic Listing)</b></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content" width="50%">&nbsp;Google</td>
											<td class="content" width="25%">&nbsp;<asp:linkbutton id="Linkbutton1" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content" width="25%">&nbsp;<a href="http://www.google.com/addurl.html" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;AllTheWeb</td>
											<td class="content">&nbsp;<asp:linkbutton id="Linkbutton3" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content">&nbsp;<a href="http://www.alltheweb.com/add_url.php" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;AltaVista</td>
											<td class="content">&nbsp;<asp:linkbutton id="Linkbutton4" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content">&nbsp;<a href="http://addurl.altavista.com/sites/addurl/newurl" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;</td>
											<td class="content">&nbsp;</td>
											<td class="content">&nbsp;</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Headings" colspan="3">&nbsp;<b>Directories (May Charge)</b></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;Lycos</td>
											<td class="content">&nbsp;<asp:linkbutton id="Linkbutton2" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content">&nbsp;<a href="http://searchservices.lycos.com/searchservices/default.asp" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;Open Directory Project</td>
											<td class="content">&nbsp;<asp:linkbutton id="Linkbutton5" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content">&nbsp;<a href="http://dmoz.org/add.html" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;Yahoo!</td>
											<td class="content">&nbsp;<asp:linkbutton id="Linkbutton6" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content">&nbsp;<a href="http://docs.yahoo.com/info/suggest/" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;Ask Jeeves</td>
											<td class="content">&nbsp;<asp:linkbutton id="Linkbutton7" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content">&nbsp;<a href="http://sp.ask.com/docs/addjeeves/submit.html" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;</td>
											<td class="content">&nbsp;</td>
											<td class="content">&nbsp;</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Headings" colspan="3">&nbsp;<b>Pay-Per-Click Paid Submissions</b></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;LookSmart</td>
											<td class="content">&nbsp;<asp:linkbutton id="Linkbutton8" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content">&nbsp;<a href="http://www.looksmart.com/help/subsite.html" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;Overture</td>
											<td class="content">&nbsp;<asp:linkbutton id="Linkbutton9" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content">&nbsp;<a href="http://overture.com/d/USm/about/advertisers/ays_home.jhtml" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;Google AdWords</td>
											<td class="content">&nbsp;<asp:linkbutton id="Linkbutton10" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content">&nbsp;<a href="http://adwords.google.com/select" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></A></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;Inktomi (iwon.com)</td>
											<td class="content">&nbsp;<asp:linkbutton id="Linkbutton11" runat="server" CausesValidation="false">Info</asp:linkbutton></td>
											<td class="content">&nbsp;<a href="http://www.inktomi.com/services/web_search/sms.html" target="_blank">Submit</a></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content">&nbsp;</td>
											<td class="content">&nbsp;</td>
											<td class="content">&nbsp;</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content" nowrap align="left" colspan="3">&nbsp;</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colspan="5" height="1">
												<img height="1" src="images/clear.gif"></td>
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

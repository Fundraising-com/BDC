<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Payment.aspx.vb" Inherits="StoreFront.StoreFront.CreditCard"%>
<%@ Register TagPrefix="uc1" TagName="GiftCertificates" Src="Controls/GiftCertificates.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="Controls/AddressLabel.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Payment Information</title>
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
		
			function SubmitPayerAuth(sId, MD, paRes)
			{
				var theform = document.forms[0];
				theform.hdPayerAuthId.value = sId;
				theform.hdMD.value = MD ;
				theform.hdPaRes.value = paRes;
				theform.submit();
			}
		
			function SetValidationComplete()
			{
			if(window.document.Form2.elements["bButtonClick"].value == "1"){
				return false;
			}
			DisableButton();
			d = new Date();
			//window.alert(window.document.Form2.item("AdvancedSearch1:AdvPriceStart").value);
			ResetForm(window.document.Form2);
			//window.alert("hi");
			//window.alert(window.document.Form2.elements["txtCheckNumber"]);
			//return false
			
			if ((window.document.Form2.elements["txtExpMonth"] != null) && (window.document.Form2.elements["txtExpYear"] != null))
			{
				if ((window.document.Form2.elements["txtExpMonth"].options[window.document.Form2.elements["txtExpMonth"].options.selectedIndex].value < d.getMonth() + 1) &&(window.document.Form2.elements["txtExpYear"].options[window.document.Form2.elements["txtExpYear"].options.selectedIndex].value == d.getFullYear()) )
				{//alert(d.getFullYear());
					EnableButton();
					return ExpirationDateMessage()					
				}
			}
			//2425, 2468
			if (window.document.Form2.elements["txtCardType"] != null) 
			{
				if((window.document.Form2.elements["txtCardType"].options[window.document.Form2.elements["txtCardType"].options.selectedIndex].text.toLowerCase()=="switch")||(window.document.Form2.elements["txtCardType"].options[window.document.Form2.elements["txtCardType"].options.selectedIndex].text.toLowerCase()=="solo"))
				{
					if((window.document.Form2.elements["txtStartMonth"].options[window.document.Form2.elements["txtStartMonth"].options.selectedIndex].text == "N/A")||(window.document.Form2.elements["txtStartYear"].options[window.document.Form2.elements["txtStartYear"].options.selectedIndex].text == "N/A"))
					{
						if(window.document.Form2.elements["txtIssueNum"].value == "" )
						{
							EnableButton();
							return DateIssueNumMessage()
						}
					}
					else if ((window.document.Form2.elements["txtStartMonth"].options[window.document.Form2.elements["txtStartMonth"].options.selectedIndex].value > d.getMonth() + 1) &&(window.document.Form2.elements["txtStartYear"].options[window.document.Form2.elements["txtStartYear"].options.selectedIndex].value == d.getFullYear()) )
					{
						EnableButton();
						return StartDateMessage()
					}
				}
			}	
			if (window.document.Form2.elements["txtCardNumber"] !=null && window.document.Form2.elements["txtPONumber"] !=null)
				{//nothing is required
				}
			else if (window.document.Form2.elements["txtCardNumber"] !=null)
				{window.document.Form2.elements["txtCardNumber"].required=true;
				window.document.Form2.elements["txtCardNumber"].title="Card Number";
				window.document.Form2.elements["txtCardNumber"].creditcardnumber=true;
				if (window.document.Form2.elements["txtSecureCode"] != null)
				{	
					//2425	
					if((window.document.Form2.elements["txtCardType"].options[window.document.Form2.elements["txtCardType"].options.selectedIndex].text.toLowerCase()=="switch")||(window.document.Form2.elements["txtCardType"].options[window.document.Form2.elements["txtCardType"].options.selectedIndex].text.toLowerCase()=="solo"))
					{	
						window.document.Form2.elements["txtSecureCode"].required=false;
						window.document.Form2.elements["txtSecureCode"].number=false;
					}
					else
					{
						window.document.Form2.elements["txtSecureCode"].required=true;
						window.document.Form2.elements["txtSecureCode"].number=true;
					}	
						window.document.Form2.elements["txtSecureCode"].title="Security Code";
				}
			}
			else if (window.document.Form2.elements["txtPONumber"] !=null)
				{window.document.Form2.elements["txtPONumber"].required=true;
				window.document.Form2.elements["txtPONumber"].title="Purchase Order Number";}
			else if (window.document.Form2.elements["txtCheckNumber"] !=null)
				{window.document.Form2.elements["txtCheckNumber"].required=true;
				window.document.Form2.elements["txtCheckNumber"].title="Check Number";
				window.document.Form2.elements["txtBankName"].required=true;
				window.document.Form2.elements["txtBankName"].title="Bank Name";
				window.document.Form2.elements["txtRoutingNumber"].required=true;
				window.document.Form2.elements["txtRoutingNumber"].title="Routing Number";
				window.document.Form2.elements["txtAccountNumber"].required=true;
				window.document.Form2.elements["txtAccountNumber"].title="Account Number";
				window.document.Form2.elements["txtSSN"].required=true;
				window.document.Form2.elements["txtSSN"].title="Social Security Number";
				window.document.Form2.elements["txtSSN"].ssn=true;
				}
				if (ValidateForm(window.document.Form2))
				{
				return true;
				}
				else
				{
					EnableButton();
					return false;
				} 
			}
			
			function SetValidationGiftCert()
			{
				ResetForm(window.document.Form2);
			
				window.document.Form2.elements["GiftCertificates1:txtGiftCertificateCode"].required=true;
				window.document.Form2.elements["GiftCertificates1:txtGiftCertificateCode"].title="Gift Certificate Code";
				return ValidateForm(window.document.Form2)
			}
			function DisableButton()
			{
				window.document.Form2.elements["bButtonClick"].value = "1";
			}
			function EnableButton()
			{
				window.document.Form2.elements["bButtonClick"].value = "0";
			}
			//Cardinal hot fix
			function popUp(url)
			 {
				popupWin=window.open(url,"win",'toolbar=0,location=0,directories=0,status=1,menubar=1,scrollbars=1,width=570,height=450');
				self.name = "mainWin"; 
			}
	
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" onsubmit="javascript: return checkValue(this);"
		runat="server" MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server">
			<input type="hidden" id="bButtonClick" value="0">
			<table id="PageTable" width="100%" cellSpacing="0" runat="server">
				<tr>
					<td id="PageCell">
						<table class="GeneralTable" id="PageSubTable" cellSpacing="0" runat="server">
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
									<h1 class="Headings">Payment Information</h1>
									<P id="ErrorAlignment" runat="server" align="center"><asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
									<P id="P1" runat="server" align="center"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>

									<table width="100%" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="Content">
												<asp:Panel ID="pnlRegular" Visible="True" Runat="server">
														<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
															<TR>
																<TD class="Content" colSpan="3">
																	<uc1:giftcertificates id="GiftCertificates1" runat="server"></uc1:giftcertificates></TD>
															</TR>
															<TR>
																<TD class="Content" colSpan="3">&nbsp;</TD>
															</TR>
															<TR>
																<TD vAlign="top" width="50%">
																	<TABLE id="Table14" cellSpacing="0" cellPadding="0" width="100%" border="0">
																		<TR>
																			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="ContentTableHeader" noWrap colSpan="4">&nbsp;Billing Information</TD>
																			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="Content" colSpan="4">&nbsp;</TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="Content">&nbsp;</TD>
																			<TD class="Content" colSpan="2">
																				<uc1:addresslabel id="AddressLabel1" runat="server"></uc1:addresslabel></TD>
																			<TD class="Content">&nbsp;</TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"></TD>
																			<TD class="Content"></TD>
																			<TD class="Content" colSpan="2">&nbsp;</TD>
																			<TD class="Content"></TD>
																			<TD class="ContentTable" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="Content">&nbsp;</TD>
																			<TD class="Content" width="60%"></TD>
																			<TD class="Content" align="right">
																				<asp:linkbutton id="btnEdit" Runat="server">
																					<asp:Image BorderWidth="0" ID="imgEdit" Runat="server" AlternateText="Edit"></asp:Image>
																				</asp:linkbutton></TD>
																			<TD class="Content">&nbsp;</TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="Content" colSpan="4">&nbsp;</TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="Content" colSpan="4">&nbsp;</TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
																		</TR>
																	</TABLE>
																</TD>
																<TD>&nbsp;</TD>
																<TD vAlign="top">
																	<cc1:totaldisplay id="TotalDisplay1" runat="server" DisplaySubHandlingTotal="False" DisplayShipmentTotal="False"
																		DisplayTaxShipNotIncluded="False" DisplayPaymentMethod="False" HeadingClass="ContentTableHeader"
																		GrandTotalStyle="subHeadings" TableBorderStyle="ContentTable" HorizontalBorderStyle="ContentTableHorizontal"
																		HandlingTotalLabel="Handling:" ShippingTotalLabel="Shipping:" SubTotalLabel="Subtotal:"></cc1:totaldisplay></TD>
															</TR>
															<TR>
																<TD vAlign="top">&nbsp;</TD>
																<TD></TD>
																<TD vAlign="top" width="50%" colSpan="4" rowSpan="1"></TD>
															</TR>
															<TR>
																<TD colSpan="3">
																	<TABLE id="tblCreditCard" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
																		<TR>
																			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="ContentTableHeader" style="HEIGHT: 12px">&nbsp;Credit Card</TD>
																			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="inner">
																				<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
																					<TR>
																						<TD colspan="10" align="left" noWrap class="Content"><FONT size="2">The information 
																						above must match that of the card holder.</FONT></TD>
																						<TD width="13" align="left" class="Content"></TD>
																					</TR>
																					<TR>
																						<TD colspan="11" align="right" noWrap class="Content">&nbsp;&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD width="101" align="right" noWrap class="Content">Card Type:&nbsp;</TD>
																						<TD class="Content" vAlign="top" align="left" colSpan="10">
																							<cc1:selectvalcontrol id="txtCardType" runat="server" DisplaySelect="CreditCards">
																								<asp:ListItem Value="CreditCards">[CreditCards]</asp:ListItem>
																					  </cc1:selectvalcontrol></TD>
																					</TR>
																					<TR>
																					  <TD colspan="11">&nbsp;</TD>
																				  </TR>
																					<TR>
																						<TD class="Content" style="WIDTH: 97px" noWrap align="right">Card Number:&nbsp;</TD>
																						<TD colspan="2" align="left" vAlign="middle" class="Content"><INPUT id="txtCardNumber" onblur="javascript: specialCase(this);" type="text" maxLength="16"
																								runat="server"></TD>
																						<TD colspan="8" align="left" class="Content">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD colspan="11" align="right" noWrap class="Content">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">Expiration Date:&nbsp;</TD>
																						<TD width="41" align="center" class="Content">Month</TD>
																						<TD width="186" align="center" nowrap class="Content"><cc1:selectvalcontrol id="txtExpMonth" runat="server" DisplaySelect="ExpireMonth">
																								<asp:ListItem Value="Month">[ExpireMonth]</asp:ListItem>
																								<asp:ListItem Value="Month">[ExpireMonth]</asp:ListItem>
																					  </cc1:selectvalcontrol></TD>
																						<TD width="44" align="left" noWrap class="Content">Year&nbsp;
																							</TD>
																						<TD width="83" align="left" noWrap class="Content">																							<cc1:selectvalcontrol id="txtExpYear" runat="server" DisplaySelect="ExpireYear">
																								<asp:ListItem Value="Year">[ExpireYear]</asp:ListItem>
																							</cc1:selectvalcontrol></TD>
																						<TD colSpan="5" rowSpan="1" align="right" vAlign="middle" noWrap class="Content">&nbsp;																						</TD>
																					</TR>
																					<TR>
																					  <TD class="Content" id="tdSecurityCode" style="WIDTH: 97px" noWrap align="right" runat="server">Security 
																							Code:&nbsp;</TD>
																					  <TD align="center" class="Content"><asp:textbox id="txtSecureCode" runat="server" MaxLength="4" Columns="5"></asp:textbox></TD>
																					  <TD colspan="8" align="center" nowrap class="Content">&nbsp;</TD>
																				  </TR>
																					<asp:Panel id="pnlswitchSoloElements" Runat="server" Visible="False">
																						<TR>
																							<TD class="Content" style="WIDTH: 494px" noWrap align="left" colSpan="9"><FONT size="2">These 
																									Fields are required for Switch/Solo Transactions Only!.</FONT></TD>
																						</TR>
																						<TR>
																							<TD class="Content" noWrap align="right">&nbsp;</TD>
																							<TD colspan="2" align="right" noWrap class="Content">Start Date:&nbsp;</TD>
																							<TD colspan="2" align="center" class="Content">Month</TD>
																							<TD width="170" align="left" noWrap class="Content">
																								<cc1:selectvalcontrol id="txtStartMonth" runat="server" DisplaySelect="StartMonth">
																									<asp:ListItem Value="Month">[StartMonth]</asp:ListItem>
																									<asp:ListItem Value="Month">[StartMonth]</asp:ListItem>
																								</cc1:selectvalcontrol></TD>
																							<TD width="107" colSpan="1" rowSpan="1" align="right" vAlign="middle" noWrap class="Content">Year&nbsp;</TD>
																							<TD width="124" class="Content">
																								<cc1:selectvalcontrol id="txtStartYear" runat="server" DisplaySelect="StartYear">
																									<asp:ListItem Value="Year">[StartYear]</asp:ListItem>
																								</cc1:selectvalcontrol></TD>
																							<TD width="99" align="right" noWrap class="Content" id="tdIssueNum" style="WIDTH: 97px" runat="server">Issue 
																								Number:&nbsp;</TD>
																							<TD width="4" class="Content">
																								<asp:textbox id="txtIssueNum" runat="server" MaxLength="4" Columns="5"></asp:textbox></TD>
																							<TD class="Content" align="left">&nbsp;</TD>
																						</TR>
																					</asp:Panel>
																					<TR>
																						<TD colspan="11" align="right" noWrap class="Content">&nbsp;&nbsp;</TD>
																					</TR>	
																					<TR>
																						<TD colspan="11" align="right" noWrap class="Content">&nbsp;&nbsp;</TD>
																					</TR>	
																					<asp:Panel id="payerAuthPanel" Runat="server" Visible="False">
																						<TR><!-- Cardinal hot fix -->
																							<TD class="Content" noWrap align="right">&nbsp;</TD>
																							<TD class="Content" align="left" colSpan="9">
																									<P>After you click the 'Complete Order' button, your transaction will be processed. 
																										For security reasons, you may be asked to authenticate or to enroll in an 
																										authentication program.<BR><br>
																										To ensure the security of your transactions, we're proud to partner with these 
																										programs:<br>
																									</P>
																						  </TD>
																							<TD class="Content" noWrap align="right" >&nbsp;</TD>
																						</TR>
																						<TR>
																							<TD class="Content" noWrap align="right" colspan="6">&nbsp;</TD>
																							<TD class="Content" noWrap align="right">
																								<a href="javascript:popUp('vbv_learn_more.htm')"><img src='images/vbv_logo_learn_more.gif' border="0" width="100" height="88"/></a>
																							</TD>
																							<TD class="Content" noWrap valign="center">
																								<a href="javascript:popUp('http://www.mastercardbusiness.com/mcbiz/index.jsp?template=/orphans&amp;content=securecodepopup')"><img src='images/securecode_logo_learn_more.gif' border="0" width="120" height="65"/></a>
																							</TD>
																							<TD class="Content" noWrap align="right" colspan="3">&nbsp;</TD>
																						</TR>
																					</asp:Panel><!-- Cardinal hot fix ends-->																		
																				</TABLE>
																			</TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="Content" colSpan="4">&nbsp;</TD>
																		</TR>
																	</TABLE>
																	<TABLE id="tblPurchaseOrder" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
																		<TR>
																			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="ContentTableHeader" width="100%">&nbsp;Purchase Order</TD>
																			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD>
																				<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
																					<TR>
																						<TD class="Content" noWrap align="right" colSpan="3">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" vAlign="middle" noWrap align="center" width="100%">Purchase 
																							Order Number:&nbsp;
																							<asp:textbox id="txtPONumber" runat="server" MaxLength="100"></asp:textbox></TD>
																						<TD class="Content" noWrap align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right" colSpan="3">&nbsp;</TD>
																					</TR>
																				</TABLE>
																			</TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="Content" colSpan="4">&nbsp;</TD>
																		</TR>
																	</TABLE>
																	<TABLE id="tblECheck" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
																		<TR>
																			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="ContentTableHeader" style="HEIGHT: 12px">&nbsp;E-Check</TD>
																			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD>
																				<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right" colSpan="5">&nbsp;&nbsp;</TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" vAlign="middle" noWrap align="right">Check Number:&nbsp;</TD>
																						<TD class="Content" align="left">
																							<asp:textbox id="txtCheckNumber" runat="server" MaxLength="8" Columns="5"></asp:textbox></TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																						<TD class="Content" noWrap align="right">Bank Name:&nbsp;</TD>
																						<TD class="Content" align="left">
																							<asp:textbox id="txtBankName" runat="server" MaxLength="255"></asp:textbox></TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right" colSpan="5">&nbsp;</TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right">Bank&nbsp;<BR>
																							Routing Number:&nbsp;</TD>
																						<TD class="Content" align="left">
																							<asp:textbox id="txtRoutingNumber" runat="server" MaxLength="9"></asp:textbox></TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																						<TD class="Content" noWrap align="right">Checking&nbsp;<BR>
																							Account Number:&nbsp;</TD>
																						<TD class="Content" noWrap align="left">
																							<asp:textbox id="txtAccountNumber" runat="server" MaxLength="18"></asp:textbox></TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right" colSpan="5">&nbsp;</TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" vAlign="baseline" noWrap align="right">SSN:&nbsp;</TD>
																						<TD class="Content" noWrap align="left">
																							<P>
																								<asp:textbox id="txtSSN" Runat="server" MaxLength="11"></asp:textbox></P>
																							<P>&nbsp;</P>
																						</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right" colSpan="5">&nbsp;&nbsp;</TD>
																						<TD class="Content" noWrap align="left" width="1">&nbsp;</TD>
																					</TR>
																				</TABLE>
																			</TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																		<TR>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		</TR>
																	</TABLE>
																</TD>
															</TR>
															<TR>
																<TD class="Content" colSpan="3">&nbsp;</TD>
															</TR>
															<TR>
																<TD class="Content" align="left">
																	<asp:panel id="pnlConsent" runat="server" Visible="False" CssClass="Content">
																		<asp:CheckBox id="chkConsent" runat="server" AutoPostBack="True"></asp:CheckBox>&nbsp;I 
																			understand that I am 
																			purchasing one or more subscription-based products, and that a 
																			recurring charge will be billed for the term of the 
																			subscriptions. I agree to the terms and conditions of 
																			those products. </asp:panel>
																</TD>
																<TD align="right" colSpan="2"><%= ProcessorCode %>
																	<asp:LinkButton id="btnCompleteOrder" Runat="server">
																		<asp:Image BorderWidth="0" ID="imgCompleteOrder" Runat="server" AlternateText="Complete Order"></asp:Image>
																	</asp:LinkButton></TD>
															</TR>
															<TR>
																<TD align="right" colSpan="3">
																<asp:LinkButton id="btnPayPalCompleteOrder" Runat="server">
																		<asp:Image BorderWidth="0" ID="imgPayPalOrder" Runat="server" AlternateText="Complete Order"></asp:Image>
																	</asp:LinkButton>
																</TD>
															</TR>
														</TABLE>
													</P>
												</asp:Panel>
											</td>
										</tr>
										<tr>
											<td class="Content" width="100%">
												<asp:Panel id="pnlPayerAuth" Visible="False" Runat="server">
													<p>
														<b>For your security, please fill out the form below to complete your order.</b><br>
													Do not click the refresh or back button or this transaction may be interrupted or cancelled.
													</p>
													<IFRAME src="PayerAuth1.aspx" frameBorder="0" width="400" scrolling="no" height="400" runat="server">
													</IFRAME>
												</asp:Panel>
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

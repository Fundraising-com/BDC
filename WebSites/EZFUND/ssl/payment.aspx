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
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
		
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
			
			if ((window.document.Form2.elements["txtMonth"] != null) && (window.document.Form2.elements["txtYear"] != null))
			{
				if ((window.document.Form2.elements["txtMonth"].options[window.document.Form2.elements["txtMonth"].options.selectedIndex].value < d.getMonth() + 1) &&(window.document.Form2.elements["txtYear"].options[window.document.Form2.elements["txtYear"].options.selectedIndex].value == d.getFullYear()) )
				{//alert(d.getFullYear());
					EnableButton();
					return ExpirationDateMessage()					
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
					{window.document.Form2.elements["txtSecureCode"].required=true;
					window.document.Form2.elements["txtSecureCode"].number=true;
					window.document.Form2.elements["txtSecureCode"].title="Security Code";}
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
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td>
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content">
												<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></P>
												<P>
													<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD class="Headings" colSpan="3">Payment Information</TD>
														</TR>
														<TR>
															<TD class="Content" colSpan="3"></TD>
														</TR>
														<TR>
															<TD class="Content" colSpan="3"><uc1:giftcertificates id="GiftCertificates1" runat="server"></uc1:giftcertificates></TD>
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
																		<TD class="Content" colSpan="2"><uc1:addresslabel id="AddressLabel1" runat="server"></uc1:addresslabel></TD>
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
																		<TD class="Content" align="right"><asp:linkbutton id="btnEdit" Runat="server">
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
															<td vAlign="top"><cc1:totaldisplay id="TotalDisplay1" runat="server" DisplaySubHandlingTotal="False" DisplayShipmentTotal="False"
																	DisplayTaxShipNotIncluded="False" DisplayPaymentMethod="False" HeadingClass="ContentTableHeader" GrandTotalStyle="Headings"
																	TableBorderStyle="ContentTable" HorizontalBorderStyle="ContentTableHorizontal" HandlingTotalLabel="Handling:"
																	ShippingTotalLabel="Shipping:" SubTotalLabel="Subtotal:"></cc1:totaldisplay></td>
														</TR>
														<TR>
															<TD vAlign="top">&nbsp;</TD>
															<TD></TD>
															<TD vAlign="top" width="50%" colSpan="4" rowSpan="1"></TD>
														</TR>
														<tr>
															<td colSpan="3">
																<TABLE id="tblCreditCard" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
																	<TR>
																		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		<TD class="ContentTableHeader" style="HEIGHT: 12px">&nbsp;Credit Card</TD>
																		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		<TD>
																			<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TBODY>
																					<TR>
																						<TD class="Content" noWrap align="right"></TD>
																						<TD class="Content" noWrap align="left" colSpan="7"><FONT size="2">The information 
																								above must match that of the card holder.</FONT></TD>
																						<TD class="Content" align="left"></TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right" colSpan="7">&nbsp;&nbsp;</TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right">Card Type:&nbsp;</TD>
																						<TD class="Content" vAlign="top" align="left" colSpan="4">&nbsp;
																							<cc1:selectvalcontrol id="txtCardType" runat="server" DisplaySelect="CreditCards">
																								<asp:ListItem Value="CreditCards">[CreditCards]</asp:ListItem>
																							</cc1:selectvalcontrol></TD>
																						<TD class="Content" noWrap align="right">Card Number:&nbsp;</TD>
																						<TD class="Content" vAlign="middle" align="left"><input id="txtCardNumber" onblur="javascript: specialCase(this);" type="text" maxLength="16"
																								runat="server"></TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right" colSpan="7">&nbsp;</TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right">Expiration Date:&nbsp;</TD>
																						<TD class="Content" align="center">Month</TD>
																						<TD class="Content" noWrap align="left"><cc1:selectvalcontrol id="txtMonth" runat="server" DisplaySelect="Month">
																								<asp:ListItem Value="Month">[Month]</asp:ListItem>
																								<asp:ListItem Value="Month">[Month]</asp:ListItem>
																							</cc1:selectvalcontrol></TD>
																						<TD class="Content" vAlign="middle" noWrap align="right" colSpan="1" rowSpan="1">Year&nbsp;</TD>
																						<TD class="Content"><cc1:selectvalcontrol id="txtYear" runat="server" DisplaySelect="Year">
																								<asp:ListItem Value="Year">[Year]</asp:ListItem>
																							</cc1:selectvalcontrol></TD>
																						<td class="Content" id="tdSecurityCode" noWrap align="right" runat="server">Security 
																							Code:&nbsp;</td>
																						<td class="Content"><asp:textbox id="txtSecureCode" runat="server" MaxLength="4" Columns="5"></asp:textbox></td>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right" colSpan="7">&nbsp;&nbsp;</TD>
																						<TD class="Content" noWrap align="left" width="1">&nbsp;</TD>
																					</TR>
																				</TBODY>
																			</TABLE>
																		</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	</TR>
																	<tr>
																		<td class="Content" colSpan="4">&nbsp;</td>
																	</tr>
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
																				<TBODY>
																					<TR>
																						<TD class="Content" noWrap align="right" colSpan="3">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" vAlign="middle" noWrap align="center" width="100%">Purchase 
																							Order Number:&nbsp;<asp:textbox id="txtPONumber" runat="server" MaxLength="100"></asp:textbox></TD>
																						<TD class="Content" noWrap align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right" colSpan="3">&nbsp;</TD>
																					</TR>
																				</TBODY>
																			</TABLE>
																		</TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	</TR>
																	<tr>
																		<td class="Content" colSpan="4">&nbsp;</td>
																	</tr>
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
																				<TBODY>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right" colSpan="5">&nbsp;&nbsp;</TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" vAlign="middle" noWrap align="right">Check Number:&nbsp;</TD>
																						<TD class="Content" align="left"><asp:textbox id="txtCheckNumber" runat="server" MaxLength="8" Columns="5"></asp:textbox></TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																						<TD class="Content" noWrap align="right">Bank Name:&nbsp;</TD>
																						<TD class="Content" align="left"><asp:textbox id="txtBankName" runat="server" MaxLength="255"></asp:textbox></TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right" colSpan="5">&nbsp;</TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right">Bank&nbsp;<br>
																							Routing Number:&nbsp;</TD>
																						<TD class="Content" align="left"><asp:textbox id="txtRoutingNumber" runat="server" MaxLength="9"></asp:textbox></TD>
																						<TD class="Content" align="left">&nbsp;</TD>
																						<TD class="Content" noWrap align="right">Checking&nbsp;<br>
																							Account Number:&nbsp;</TD>
																						<TD class="Content" noWrap align="left"><asp:textbox id="txtAccountNumber" runat="server" MaxLength="18"></asp:textbox></TD>
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
																							<P><asp:textbox id="txtSSN" Runat="server" MaxLength="11"></asp:textbox></P>
																							<P>&nbsp;</P>
																						</TD>
																					</TR>
																					<TR>
																						<TD class="Content" noWrap align="right">&nbsp;</TD>
																						<TD class="Content" noWrap align="right" colSpan="5">&nbsp;&nbsp;</TD>
																						<TD class="Content" noWrap align="left" width="1">&nbsp;</TD>
																					</TR>
																				</TBODY>
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
															</td>
														</tr>
														<TR>
															<TD class="Content" colSpan="3">&nbsp;</TD>
														</TR>
														<tr>
															<td align="right" colSpan="3"><%= ProcessorCode %>
																<asp:LinkButton ID="btnCompleteOrder" Runat="server">
																	<asp:Image BorderWidth="0" ID="imgCompleteOrder" Runat="server" AlternateText="Complete Order"></asp:Image>
																</asp:LinkButton></td>
														</tr>
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
		<script type="text/javascript">
		var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
		document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
		</script>
		<script type="text/javascript">
		var pageTracker = _gat._getTracker("UA-1536999-1");
		pageTracker._initData();
		pageTracker._trackPageview();
		</script>
	</body>
</HTML>

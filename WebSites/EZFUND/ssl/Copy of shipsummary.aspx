<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ShipSummary.aspx.vb" Inherits="StoreFront.StoreFront.ShipSummary"%>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="Controls/AddressLabel.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Shipment Summary</title>
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
		<!--
			function SetValidation(cnt)
			{
				//ResetForm(window.document.Form2);
				
				for (var i=0; i<cnt; i++) {
				
				//	window.alert(window.document.Form2.elements["DataList1:_ctl" + i + ":cboShipChoices"]);
				//return false;
				
					if (window.document.Form2.elements["DataList1:_ctl" + i + ":cboShipChoices"] != null)
						{window.document.Form2.elements["DataList1:_ctl" + i + ":cboShipChoices"].required=true;
						window.document.Form2.elements["DataList1:_ctl" + i + ":cboShipChoices"].title="Shipping Method";
						
						}
					if (window.document.Form2.elements["DataList1:_ctl" + i + ":cboShipping"] != null)
						{window.document.Form2.elements["DataList1:_ctl" + i + ":cboShipping"].title="Shipping Carrier";
						window.document.Form2.elements["DataList1:_ctl" + i + ":cboShipping"].required=true;
				//		window.alert(window.document.Form2.elements["DataList1:_ctl" + i + ":cboShipping"].name);
						}
				}
				//return false;
				return ValidateForm(window.document.Form2)
			}
			//-->
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server" MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" width="100%" runat="server">
				<tr>
					<td id="PageCell">
						<table class="GeneralTable" id="PageSubTable" cellSpacing="0" runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start --><uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
									<!-- Top Banner End --></td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start --><uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
									<!-- Top Sub Banner End --></td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell">
									<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
									<!-- Left Column End --></td>
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
												<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<TD class="Headings">Shipment Summary</TD>
													</TR>
													<TR>
														<TD class="Content">&nbsp;</TD>
													</TR>
													<TR>
														<TD><asp:datalist id="DataList1" runat="server" Width="100%">
																<FooterTemplate>
																	<table border="0" cellpadding="0" cellspacing="0" width="100%">
																		<tr>
																			<td class="ContentTable" height="1"><IMG src="images/clear.gif" width="1" height="1"></td>
																		</tr>
																	</table>
																</FooterTemplate>
																<ItemTemplate>
																	<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
																		<TR>
																			<TD vAlign="top" width="50%">
																				<TABLE id="Table10" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
																					<TR>
																						<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="ContentTableHeader">&nbsp;</TD>
																						<TD class="ContentTableHeader">Ship To:
																							<asp:Label id="Label2" runat="server" CssClass="ContentTableHeader">
																								<%# DataBinder.Eval(Container.DataItem.Address,"NickName") %>
																							</asp:Label></TD>
																						<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="Content">
																							<asp:TextBox id=txtShipID Visible="False" Text='<%# DataBinder.Eval(Container.DataItem.Address,"ID") %>' Runat="server">
																							</asp:TextBox>
																							<uc1:AddressLabel id=Addresslabel1 runat="server" AddressSource='<%# DataBinder.Eval(Container.DataItem,"Address") %>'>
																							</uc1:AddressLabel></TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR id="CarrierShipping">
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="Content">Shipping Carrier:
																							<cc1:SelectValControl id="cboShipping" runat="server" OnSelectedIndexChanged="cboShipping_SelectedIndexChanged"
																								DisplaySelect="ShippingMethod" AutoPostBack="True"></cc1:SelectValControl>
																						</TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR id="CarrierShippingChoices">
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="Content">
																							<asp:DropDownList id="cboShipChoices" runat="server" AutoPostBack="true"></asp:DropDownList>
																							<asp:Label id="lblShippingError" Runat="server">This carrier is not available for this package.</asp:Label>
																							<asp:Label id="lblBackupShip" Runat="server">Backup shipping is being used.</asp:Label>
																						</TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR id="PremiumShipping">
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="Content">
																							<asp:CheckBox ID="chkPremiumShipping" Runat="server" AutoPostBack="true"></asp:CheckBox>
																						</TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR id="UPS">
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="Content">
																							<table width="100%" border="0">
																								<tr>
																									<td class="Content" width="1" align="left" valign="top">
																										<asp:Image ID="UPSShield" Runat="server" ImageUrl="images/LOGO_S.gif"></asp:Image></td>
																									<td class="Content" align="left" valign="top">
																										<asp:Label ID="UPSRateChange" Runat="server">These fees do not necessarily represent UPS published rates and may include handling charges levied by the online merchant.</asp:Label>&nbsp;</td>
																								</tr>
																							</table>
																						</TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
																						<TD class="ContentTable" style="HEIGHT: 1px" colSpan="2"><IMG height="1" src="images/clear.gif" width="1"></TD>
																						<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
																					</TR>
																				</TABLE>
																				<BR>
																				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																					<TR>
																						<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="ContentTableHeader">&nbsp;</TD>
																						<TD class="ContentTableHeader">Special Instructions</TD>
																						<TD class="ContentTableHeader">&nbsp;</TD>
																						<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content" colSpan="3">&nbsp;</TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="Content" align="middle">
																							<asp:TextBox id="txtSpecialInstruction" runat="server" Rows="2" Columns="25" TextMode="MultiLine"></asp:TextBox></TD>
																						<TD class="Content">&nbsp;</TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																						<TD class="Content" colSpan="3">&nbsp;</TD>
																						<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
																						<TD class="ContentTable" style="HEIGHT: 1px" colSpan="2"><IMG height="1" src="images/clear.gif" width="1"></TD>
																						<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif" width="1"></TD>
																					</TR>
																				</TABLE>
																			</TD>
																			<TD><IMG height="1" src="images/clear.gif" width="10"></TD>
																			<TD vAlign="top" width="50%">
																				<cc1:DynamicCartDisplay id="DynamicCartDisplay1" runat="server" Width="100%" HeadingClass="ContentTableHeader"
																					PriceLabel="Price" QuantityLabel="Qty" ProductLabel="Product" GiftWrapDetail="True" DisplayGiftWrapRow="True"
																					TotalColumnDisplay="False" OptionsColumnDisplay="False" ReOrderBtnDisplay="True" StatusColumnDisplay="False"
																					DesignCount="2" BorderClass="ContentTable" HorizontalClass="ContentTableHorizontal"></cc1:DynamicCartDisplay><BR>
																				<BR>
																				<cc1:TotalDisplay id="ShipmentTotalDisplay1" runat="server" ShippingTotalLabel="Shipping:" SubTotalLabel="Subtotal:"
																					HandlingTotalLabel="Handling:" HorizontalBorderStyle="ContentTableHorizontal" TableBorderStyle="ContentTable"
																					HeadingClass="ContentTableHeader" DisplayOrderTotal="False" DisplayTaxShipNotIncluded="False"
																					HeadingString="Shipment Total" ShipmentTotalLabel="Shipment Total:" ShipmentTotalStyle="Headings"
																					DisplayPaymentMethod="False" DisplayGrandTotal="False" DisplayGiftCertificateTotal="False"></cc1:TotalDisplay></TD>
																		</TR>
																	</TABLE>
																</ItemTemplate>
																<SeparatorTemplate>
																	<table border="0" cellpadding="0" cellspacing="0" width="100%">
																		<tr>
																			<td class="ContentTableHorizontal" height="1"><IMG src="images/clear.gif" width="1" height="1"></td>
																		</tr>
																	</table>
																</SeparatorTemplate>
															</asp:datalist></TD>
													</TR>
													<TR>
														<TD class="Content">&nbsp;</TD>
													</TR>
													<TR>
														<TD>
															<table cellSpacing="0" cellPadding="0" border="0">
																<tr>
																	<td class="Content" width="50%">&nbsp;</td>
																	<TD><IMG height="1" src="images/clear.gif" width="10"></TD>
																	<td class="Content" align="right" width="50%"><cc1:totaldisplay id="TotalDisplay1" runat="server" ShippingTotalLabel="Shipping:" SubTotalLabel="Subtotal:"
																			HandlingTotalLabel="Handling:" HorizontalBorderStyle="ContentTableHorizontal" TableBorderStyle="ContentTable" HeadingClass="ContentTableHeader"
																			GrandTotalStyle="Headings" DisplaySubTotal="False" DisplayStateTaxTotal="False" DisplayShippingTotal="False" DisplayMerchandiseTotal="False"
																			DisplayLocalTaxTotal="False" DisplayHandlingTotal="False" DisplayDiscountTotal="False" DisplayCountryTaxTotal="False" DisplayOrderTotal="False"
																			DisplayTaxShipNotIncluded="False"></cc1:totaldisplay></td>
																</tr>
																<tr>
																	<td class="Content" colSpan="3">&nbsp;</td>
																</tr>
																<tr>
																	<td class="Content" width="50%">&nbsp;</td>
																	<TD><IMG height="1" src="images/clear.gif" width="10"></TD>
																	<td align="right" width="50%">
																		<asp:LinkButton ID="btnContinue" Runat="server">
																			<asp:Image BorderWidth="0" ID="imgContinue" Runat="server" AlternateText="Continue"></asp:Image>
																		</asp:LinkButton>
																	</td>
																</tr>
															</table>
														</TD>
													</TR>
													<TR id="UPSCW" runat="server">
														<TD class="Content">UPS®, UPS &amp; Shield Design® and UNITED PARCEL SERVICE® are 
															registered trademarks of United Parcel Service of America, Inc.</TD>
													</TR>
												</TABLE>
											</td>
										</tr>
									</table>
									<!-- Content End --></td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start --><uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
									<!-- Right Column End --></td>
							</tr>
							<tr>
								<td class="Footer" id="FooterCell" colSpan="3">
									<!-- Footer Start --><uc1:footer id="Footer1" runat="server"></uc1:footer>
									<!-- Footer End --></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

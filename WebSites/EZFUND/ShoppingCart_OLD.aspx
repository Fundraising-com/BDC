<%@ Register TagPrefix="uc1" TagName="SalesDiscount" Src="Controls/SalesDiscount.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CCartControl" Src="Controls/CCartControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ShoppingCart.aspx.vb" Inherits="StoreFront.StoreFront.ShoppingCart" EnableViewState=True%>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Shopping Cart</title>
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
		
			function SetValidationUpdateQty(cnt)
			{
			//window.alert(window.document.Form2.item("AdvancedSearch1:AdvPriceStart").value);
			ResetForm(window.document.Form2);
			
			for (var i=1; i<cnt+1; i++) {
			if (window.document.Form2.elements["CCartControl1:DynaCart:Qty" + i] !=null)
				{window.document.Form2.elements["CCartControl1:DynaCart:Qty" + i].required=true;
				window.document.Form2.elements["CCartControl1:DynaCart:Qty" + i].quantitybox=true;
				window.document.Form2.elements["CCartControl1:DynaCart:Qty" + i].title="Qty " + i;
				}
				}
				return ValidateForm(window.document.Form2)
			}
		function SetValidationCoupon()
		{
		ResetForm(window.document.Form2);
		
		window.document.Form2.elements["SalesDiscount1:txtPromotionCode"].required=true;
		window.document.Form2.elements["SalesDiscount1:txtPromotionCode"].title="Coupon Code";
		return ValidateForm(window.document.Form2)
		}
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
												<P id="MessageAlignment" runat="server" align="center">
													<asp:Label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:Label>
												</P>
												<TABLE id="Table3" runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<TD class="Headings" colSpan="3">Order Summary<br>
														</TD>
													</TR>
													<TR>
														<TD colSpan="3"><uc1:ccartcontrol id="CCartControl1" runat="server"></uc1:ccartcontrol></TD>
													</TR>
													<TR>
														<TD class="Content" colSpan="3">&nbsp;</TD>
													</TR>
													<TR>
														<TD align="right" colSpan="3">
															<asp:LinkButton ID="btnUpdateQty" Runat="server">
																<asp:Image BorderWidth="0" ID="imgUpdateQty" Runat="server" AlternateText="Update Quantity"></asp:Image>
															</asp:LinkButton>
														</TD>
													</TR>
													<TR>
														<TD class="Content" colSpan="3">&nbsp;</TD>
													</TR>
													<TR>
														<TD class="Content" vAlign="top" width="50%">
															<P><uc1:salesdiscount id="SalesDiscount1" runat="server"></uc1:salesdiscount></P>
															<P>&nbsp;</P>
														</TD>
														<TD>&nbsp;</TD>
														<TD vAlign="top" align="right" width="50%">
															<cc1:TotalDisplay id="TotalDisplay1" runat="server" DisplayPaymentMethod="False" DisplayLocalTaxTotal="False" DisplayStateTaxTotal="False" DisplayCountryTaxTotal="False" DisplayShipmentTotal="False" DisplayShippingTotal="False" DisplayHandlingTotal="False" DisplayOrderTotal="False" GrandTotalLabel="Order Total:" GrandTotalStyle="Headings" HeadingClass="ContentTableHeader" TableBorderStyle="ContentTable" HorizontalBorderStyle="ContentTableHorizontal" SubTotalLabel="Subtotal:" ShippingTotalLabel="Shipping:" HandlingTotalLabel="Handling:"></cc1:TotalDisplay></TD>
													</TR>
													<TR>
														<TD class="Content" colSpan="3">&nbsp;</TD>
													</TR>
													<TR>
														<TD align="right" colspan="3">
															<asp:LinkButton ID="btnCheckout" Runat="server">
																<asp:Image BorderWidth="0" ID="imgCheckout" Runat="server" AlternateText="Checkout"></asp:Image>
															</asp:LinkButton>
														</TD>
													</TR>
												</TABLE>
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

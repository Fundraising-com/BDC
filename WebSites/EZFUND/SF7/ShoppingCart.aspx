<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ShoppingCart.aspx.vb" Inherits="StoreFront.StoreFront.ShoppingCart" EnableViewState=True%>
<%@ Register TagPrefix="uc1" TagName="CCartControl" Src="Controls/CCartControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SalesDiscount" Src="Controls/SalesDiscount.ascx" %>
<%@ OutputCache Location="None" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Shopping Cart</title>
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
		<script language="javascript">
		
		//CCartControl1_DynaCart_MultiShip
		function chkPayPal()
		{
			if (window.document.Form2.elements["CCartControl1:DynaCart:MultiShip"]!=null)
			{
				if (window.document.Form2.elements["CCartControl1:DynaCart:MultiShip"].checked == true)
				{
					window.alert("PayPal Express Checkout is only available for \u000Dorders shipping to a single address.\u000DTo checkout with PayPal, please deselect \u000Dthe Ship To Multiple Addresses option. ");
					return false;
				} 
				else
				{
					return true;
				}
			}
		}
		</script>
	</HEAD>
	<body class="generalpage" id="BodyTag" runat="server">
		<form id="Form2" method="post" runat="server">
			<input id="myhiddenfield" type="hidden" value="null" name="myhiddenfield" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1"
							runat="server">
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
								<td class="Content" id="ContentCell" vAlign="top">
									<!-- Content Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<h1 class="Headings">Shopping Cart</h1>
									<P id="ErrorAlignment" align="center" runat="server"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></P>
									<P id="OrderErrorAlignment" align="center" runat="server"><asp:label id="OrderErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></P>
									<P id="MessageAlignment" align="center" runat="server"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
											<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
													<TR>
														<TD colspan="3"><h2 class="subHeadings">Order Summary</h2></TD>
													</TR>
													<TR>
														<TD colSpan="3"><uc1:ccartcontrol id="CCartControl1" runat="server"></uc1:ccartcontrol></TD>
													</TR>
													<TR>
														<TD align="right" colSpan="3" class="button">
															<asp:linkbutton id="btnUpdateQty" Runat="server"><asp:Image BorderWidth="0" ID="imgUpdateQty" Runat="server" AlternateText="Update Quantity"></asp:Image></asp:linkbutton>
														</TD>
													</TR>
													<TR>
														<TD class="Content" colSpan="3">&nbsp;</TD>
													</TR>
													<TR>
														<TD class="Content" vAlign="top" width="50%">
															<uc1:salesdiscount id="SalesDiscount1" runat="server"></uc1:salesdiscount>
														</TD>
														<TD style="WIDTH: 62px">&nbsp;</TD>
														<TD vAlign="top" align="right" width="50%"><cc1:totaldisplay id="TotalDisplay1" runat="server" HandlingTotalLabel="Handling:" ShippingTotalLabel="Shipping:"
																SubTotalLabel="Subtotal:" HorizontalBorderStyle="ContentTableHorizontal" TableBorderStyle="ContentTable" HeadingClass="ContentTableHeader"
																GrandTotalStyle="subHeadings" GrandTotalLabel="Order Total:" DisplayOrderTotal="False" DisplayHandlingTotal="False" DisplayShippingTotal="False"
																DisplayShipmentTotal="False" DisplayCountryTaxTotal="False" DisplayStateTaxTotal="False" DisplayLocalTaxTotal="False" DisplayPaymentMethod="False"></cc1:totaldisplay></TD>
													</TR>
													<TR>
														<TD align="right" colSpan="3" class="button">
															<a href="javascript:history.go(-1)"><asp:Image BorderWidth="0" ID="imgContinueShopping" Runat="server" AlternateText="Continue Shopping"></asp:Image></a>
															<asp:linkbutton id="btnCheckout" Runat="server"><asp:Image BorderWidth="0" ID="imgCheckout" Runat="server" AlternateText="Checkout"></asp:Image></asp:linkbutton>
														</TD>
													</TR>
													<TR>
														<TD class="Content" colSpan="3">&nbsp;</TD>
													</TR>
													<TR>
														<TD class="Content" colSpan="3">&nbsp;</TD>
													</TR>
												</TABLE>
												<!-- PayPal Row Starts -->
												<asp:panel id="pnlPayPal" Runat="server">
													<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server" ID="Table1">
														<TR>
															<TD colSpan="2" class="Content">
																<p>Save time. Check out securely. Click the PayPal&reg; button to use the<br>
																shipping and billing information you have stored with PayPal&reg;<br>
																<B>This option is only available for orders shipping to a single address.</B></p>
															</TD>
															<TD align="right" colSpan="1">
																<asp:LinkButton id="btnPayPal" Runat="server">
																	<IMG border="0" src="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" align="left">
																</asp:LinkButton></TD>
														</TR>
													</TABLE>
												</asp:panel>
												<!-- PayPal Row ends -->
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

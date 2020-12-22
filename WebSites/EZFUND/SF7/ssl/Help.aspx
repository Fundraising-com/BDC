<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Help.aspx.vb" Inherits="StoreFront.StoreFront.Help" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

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

<HTML>
  <HEAD>
		<TITLE><% writeTitle() %> - Help</TITLE>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		
	</HEAD>
<body id="BodyTag" runat="server" class=generalpage>
		<a name="#top"></a>
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
											<h1 class="Headings">Help</h1>
											
											<a name="13"></a>
											<h2 class="subHeadings">Ship To Information</h2>
											<p class="Content">If you've placed an order here before, simply enter your e-mail address and password and click &quot;Sign In&quot;. If this is your first time shopping with us, please supply the required information and click &quot;Continue&quot;.</p>
											<p class="top"><a href="#top">Back To Top</a></p>
											
											<a name="9"></a>
											<h2 class="subHeadings divide">Customer Address Book</h2>
											<p class="Content">For faster checkout, use your personal Address Book to save addresses to which your orders may be shipped or billed. To create a new Address Book entry, enter the information requested by the form below, then click &quot;Save&quot;. To edit an entry, click the &quot;Edit&quot; button, make any changes to the entry, then click &quot;Save&quot;. To delete an entry, click &quot;Delete&quot;. </p>
											<p class="top"><a href="#top">Back To Top</a></p>
											
											<a name="14"></a>
											<h2 class="subHeadings divide">Multiple Shipping </h2>
											<p class="Content">Select a shipping destination from your Address Book for each item in your order. If no suitable entry exists in the Address Book, or if you need to make changes to an Address Book entry, click &quot;Manage Addresses&quot;. If you have no entries in your Address Book, click &quot;Add Address&quot;. When you have chosen a shipping destination for each item, click &quot;Continue&quot; to proceed to the next step of the checkout process. </p>
											<p class="top"><a href="#top">Back To Top</a></p>
											
											<a name="10"></a>
											<h2 class="subHeadings divide">Shipment Summary</h2>
											<p class="Content">Your current order is shown. If changes to your order are necessary, click on the &quot;Checkout&quot; link and make any corrections. When you are satisfied with the contents of your order, click &quot;Continue&quot; to proceed to our secure location and begin the checkout process. </p>
											<p class="top"><a href="#top">Back To Top</a></p>
											
											<a name="15"></a>
											<h2 class="subHeadings divide">Billing Information</h2>
											<p class="Content">This step of the checkout process and the next collect the information that will be used to bill your order. Eenter your billing address or select a suitable billing address from your Address Book, then click &quot;Continue&quot;. This address must match that associated with your payment method. </p>
											<p class="top"><a href="#top">Back To Top</a></p>
											
											<a name="16"></a>
											<h2 class="subHeadings divide">Payment Page</h2>
											<p class="Content">This is the final step of the checkout process. Review the charges shown below and, if you are satisfied, enter your payment information in the form provided. When you are finished, click &quot;Complete Order&quot;. </p>
											<p class="top"><a href="#top">Back To Top</a></p>
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

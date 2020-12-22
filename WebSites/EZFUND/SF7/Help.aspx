<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Help.aspx.vb" Inherits="StoreFront.StoreFront.Help" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

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
										
										<a name="1"></a>
										<h2 class="subHeadings">My Profile Page</h2>
										<p class="Content">Use the options provided to view and edit your account information. To change your registered password or email address, click &quot;Edit My Profile&quot;. To view a list of previous orders placed under your account and to check on the status of pending orders, click &quot;View Order Status and History&quot;. To view and modify the items in your Saved Order/Wish List, click &quot;Access My Saved Order/Wish List&quot;. To view, delete, or add new addresses to your Address Book, click &quot;Manage Address Book&quot;. </p>
										<p class="top"><a href="#top">Back To Top</a></p>

										<a name="2"></a>
										<h2 class="subHeadings divide">Customer Sign In Page</h2>
										<p class="Content">Enter your email address and password to gain access to your account information. If you don't remember your password, click &quot;Forgot your Password?&quot; to have the password emailed to you. If this is your first time shopping here, you can create a new account by supplying the information requested under &quot;Create a New Account&quot;, then clicking &quot;Continue&quot;. </p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="3"></a>
										<h2 class="subHeadings divide">Customer Edit Profile Page</h2>
										<p class="Content">Your profile consists of the name assigned to your account and the email address and password you use to gain access to the account. To change this information, simply modify it in the form displayed. When you are finished making changes, click the &quot;Save&quot; button to update your profile. </p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="4"></a>
										<h2 class="subHeadings divide">Order History Page</h2>
										<p class="Content">The order history for your account is displayed. To view more information about an order, click &quot;View&quot;. Click &quot;Track&quot; to view the shipment's current status. </p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="5"></a>
										<h2 class="subHeadings divide">Order Detail</h2>
										<p class="Content">The details of your order are shown. You can check the status of your order by clicking &quot;Track&quot;, or you can reorder the same items by clicking the &quot;Re-order&quot; button. </p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="6"></a>
										<h2 class="subHeadings divide">Checkout Page</h2>
										<p class="Content">Your current order is displayed. To remove an item from the order, click &quot;Remove&quot;. To move an item to your Saved Cart to be purchased at a later time, click &quot;Save Cart&quot;. To change the quantity of an item, change the number displayed in the Quantity field and then click &quot;Update&quot;. If you have any Coupons you wish to use, enter them in the Coupon Code field and click &quot;Apply&quot;. </p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="7"></a>
										<h2 class="subHeadings divide">My Wish List</h2>
										<p class="Content">Your personal Wish List is displayed. To remove an item from your Wish List, click &quot;Remove&quot;. To move an item from the Wish List to your current order for immediate purchase, click &quot;Buy Now&quot;. To change the quantity of an item in the Wish List, change the number displayed in the Quantity field and then click Update. You can also email your Wish List to friends and family by clicking &quot;E-Mail List&quot;. </p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="8"></a>
										<h2 class="subHeadings divide">E-Mail Wish List</h2>
										<p class="Content">To send your Wish List to a friend or family member, first enter the name of the recipient in the Recipient's Name field, then enter the email address where the Wish List should be sent in the Recipient's Email Address field. Compose a suitable message in the Personal Message area and click &quot;Send&quot;. </p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="9"></a>
										<h2 class="subHeadings divide">Customer Address Book</h2>
										<p class="Content">For faster checkout, use your personal Address Book to save addresses to which your orders may be shipped or billed. To create a new Address Book entry, enter the information requested by the form below, then click &quot;Save&quot;. To edit an entry, click the &quot;Edit&quot; button, make any changes to the entry, then click &quot;Save&quot;. To delete an entry, click &quot;Delete&quot;. </p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="11"></a>
										<h2 class="subHeadings divide">Gift Wrap</h2>
										<p class="Content">For each item you wish to gift wrap, check the &quot;Gift Wrap&quot; option, then enter your name in the From field and the name of the recipient in the To field. Enter a special greeting in the Message field. When you are finished, click &quot;Continue&quot;. </p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="12"></a>
										<h2 class="subHeadings divide">Customer Sign In Page</h2>
										<p class="Content">Enter the address that your order will be shipped to or select an address from your Address Book. New addresses will be saved to your Address Book and you'll be able to retrieve and reuse this information the next time you order. When you've finished, click &quot;Continue&quot; to proceed to the next step of the checkout process.</p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="18"></a>
										<a name="19"></a>
										<h2 class="subHeadings divide">Search Page</h2>
										<p class="Content">You can use this page to search our inventory of products for items matching a certain description. To initiate a search, enter your criteria and then click Go. You can also use our <a href="search.aspx?advanced=1">Advanced Search</a> for more detailed searches. </p>
										<p class="Content">You can use this page to perform a detailed search of our product inventory using a variety of criteria. To initiate a search, enter your criteria and click Search. </p>
										<p class="top"><a href="#top">Back To Top</a></p>
										
										<a name="20"></a>
										<h2 class="subHeadings divide">My Wish List</h2>
										<p class="Content">Your personal Saved Cart is displayed below. To remove an item from your Saved Cart, click &quot;Remove&quot;. To move an item from the Saved Cart to your current order for immediate purchase, click &quot;Buy Now&quot;. To change the quantity of an item in the Saved Cart, change the number displayed in the Quantity field and then click &quot;Update&quot;. </p>
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

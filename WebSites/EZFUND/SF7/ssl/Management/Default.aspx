<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Default.aspx.vb" Inherits="StoreFront.StoreFront.m_Default" EnableViewState=True smartNavigation="true"  TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0"%>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
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
		<title><% writeTitle() %>  - Merchant Tools</title>
		
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
</HEAD>
	
<body class="GeneralPage" runat="server" id="BodyTag">
<form id="Form2" method="post" runat="server">

<table cellspacing="0" class="GeneralTable">
<tr>
<td class="TopBanner" colspan="3">
	<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
</td>
</tr>
<tr>
<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="&nbsp;"></uc1:TopSubBanner>
</td>
</tr><tr>
<td class="LeftColumn" id="LeftColumnCell">
	<!-- Left Column Start -->
	<uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
	<!-- Left Column End -->
</td>
<td class="Content" vAlign="top">
	<!-- Content Start -->
	<table width="100%" cellSpacing="3" cellPadding="5" border="0">
	<tr>
		<td class="content" align=middle>
			<P id="ErrorAlignment" runat="server" align=center>
				<font color=#ff0000>
					<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages">
					</asp:label>
				</font>
			</P>
		</td>
	</tr>
	<tr>
   	<td class="Content" valign="top">
		<table width="100%" border="0" cellspacing="15" cellpadding="3">
        <tr> 
   	     <td width="100%" colspan="3" class="Headings">
		 	Welcome to Merchant Tools
		</td>
   	 	</tr>
        <tr> 
   	    <td width="100%" colspan="3" class="Content">
			Use the resources below to assist you with your e-commerce project.
		</td>
   	    </tr>
		<tr> 
   	    <td width="100%" colspan="3" class="Content">&nbsp;</td>
   	    </tr>
   	    <tr> 
   	    <td width="33%" class="ContentTableHeader">Online Help</td>
   	    <td width="33%" class="ContentTableHeader">Quick Start Guide</td>
		<td width="33%" class="ContentTableHeader">Knowledge Base</td>
    	</tr>
       	<tr valign="top"> 
       	<td class="Content">
			Access context specific help from 
       	    any Merchant Tools screen. Simply click the help button 
       	    below to get help for the section you are working on.
		</td>
       	<td class="Content">
			Use the Quick Start Guide to jump 
       	    start your e-commerce project. Learn how to configure 
       	    your store's business rules, enter products, and more.
		</td>
       	<td class="Content">
			Search the StoreFront Knowledge Base 
       	    for a variety of articles on setting up and customizing 
       	    your StoreFront web store. 
		</td>
       	</tr>
       	<tr> 
       	<td class="Content"> 
       		<A href="javascript: doHelp('http://support.storefront.net/mtdocs70/index.asp')"><img src="images/help.jpg" border="0"></A> 
      	 	</td>
       	<td class="Content"><strong><a href="http://support.storefront.net/storefront7/getstarted/quickstart.aspx" style="COLOR: black" target="_blank">Launch the Quick Start Guide</a></strong></td>
       	<td class="Content"><strong><a href="http://support.storefront.net/Resources/kbase/kbsearch.asp" style="COLOR: black" target="_blank">Search the Knowledge Base</a></strong></td>
       	</tr>
		<tr> 
       	<td width="100%" colspan="3" class="Content">&nbsp;</td>
       	</tr>
       	<tr>
       	<td class="Content" width="100%" colspan="3">
			<table width="100%" >
			<tr>
			<td class="Content">
				<img src="images/newsgroup.jpg">
			</td>
			<td class="Content">Be a part of an online discussion 
       		 	about StoreFront with StoreFront's online forums. <br>
      	    	<strong><a href="http://forums.storefront.net/" style="COLOR: black" target="_blank">Access the StoreFront Forums</a></strong>
			</td>
           	</tr>
			</table> 
		</td>
       	</tr>
       	</table>
	</td>
	</tr>
	</table>
	<!-- Content End -->
</td>
</tr>
</table>

</form>
</body>
</HTML>

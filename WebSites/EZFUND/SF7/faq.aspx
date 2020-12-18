<%@ Import Namespace = "StoreFront.SystemBase"%>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default.aspx.vb" Inherits="StoreFront.StoreFront.DefaultPage" EnableViewState=True TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
<HEAD>
<title>FAQ's - <% writeTitle() %></title>
<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
<meta content="JavaScript" name="vs_defaultClientScript">
<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
<script language="JavaScript" src="general.js"></script>
<meta http-equiv="PRAGMA" content="NO-CACHE">
<meta http-equiv="EXPIRES" content="-1">

<% Me.PageHeader %>

</HEAD>

<body id="BodyTag" runat="server" class="GeneralPage">

<form id="Form2" method="post" runat="server">
<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
<tr>
<td id="PageCell">
<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1" runat="server" class="static">
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
<h1>FAQ's</h1>
<p>Coming Soon</p>
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
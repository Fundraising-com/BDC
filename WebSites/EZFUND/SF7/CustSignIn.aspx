<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustSignIn.aspx.vb" Inherits="StoreFront.StoreFront.CustSignIn"  TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0"%>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
<HEAD>
<title><% writeTitle() %> - Customer Sign In</title>
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
<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
<meta name="vs_defaultClientScript" content="JavaScript">
<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
<script language="JavaScript" src="general.js"></script>
<% Me.PageHeader %>
<script language="javascript">
<!--
function SetValidationSignIn()
{

ResetForm(window.document.Form2);

window.document.Form2.elements["txtSIEMail"].required=true;
window.document.Form2.elements["txtSIEMail"].email=true;
window.document.Form2.elements["txtSIEMail"].title="E-Mail Address";
window.document.Form2.elements["txtSIPassword"].required=true;
window.document.Form2.elements["txtSIPassword"].title="Password";
return ValidateForm(window.document.Form2);

}

function SetValidationNew()
{
ResetForm(window.document.Form2);
window.document.Form2.elements["txtCAFirstName"].required=true;
window.document.Form2.elements["txtCAFirstName"].title="First Name";
window.document.Form2.elements["txtCALastName"].required=true;
window.document.Form2.elements["txtCALastName"].title="Last Name";
window.document.Form2.elements["txtCAEMail"].required=true;
window.document.Form2.elements["txtCAEMail"].email=true;
window.document.Form2.elements["txtCAEMail"].title="E-Mail Address";
window.document.Form2.elements["txtCAPassword"].required=true;
window.document.Form2.elements["txtCAPassword"].password=true;
window.document.Form2.elements["txtCAPassword"].title="Password";
window.document.Form2.elements["txtCAConfirmPassword"].required=true;
window.document.Form2.elements["txtCAConfirmPassword"].password=true;
window.document.Form2.elements["txtCAConfirmPassword"].title="Confirmation Password";
return ValidateForm(window.document.Form2)
}
function onReturn()
{
if (event.keyCode == 13)
document.all["btnSignIn"].click();
}

//-->
</script>
</HEAD>
<body id="BodyTag" runat="server" class="generalpage" onkeydown="onReturn();">
<form id="Form2" method="post" runat="server">
<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
<tr>
<td id="PageCell">
<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" runat="server" class="account">
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
<h1 class="Headings">Account Sign In</h1>
<P id="ErrorAlignment" runat="server"><asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
<P id="P1" runat="server"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
<asp:TextBox id="ReturnPage" runat="server" Visible="False"></asp:TextBox>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" class="Content col2">
<TR>
<TD vAlign="top" class="c1">
<h2 class="subHeadings">Sign In To Your Account</h2>
<p>To access your account, enter your e-mail address and password below.</p>
<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0" class="Content formtbl">
<TR>
<TD class="name">E-Mail Address:&nbsp;</TD>
<TD class="input"><asp:TextBox id="txtSIEMail" MaxLength="255" runat="server" tabIndex="1" cssclass="Content"></asp:TextBox></TD>
</TR>
<TR>
<TD class="name">Password:&nbsp;</TD>
<TD class="input"><asp:TextBox id="txtSIPassword" runat="server" TextMode="Password" tabIndex="2" cssclass="Content" MaxLength="255"></asp:TextBox></TD>
</TR>
<TR>
<TD colSpan="2">
<asp:HyperLink id="ForgotLink" class="Content" runat="server" NavigateUrl="CustForgotPassword.aspx" tabIndex="3">Forgot Your Password?</asp:HyperLink></TD>
</TR>
<TR>
<TD colSpan="2" class="button">
<asp:LinkButton ID="btnSignIn" Runat="server" tabIndex="4">
<asp:Image BorderWidth="0" ID="imgSignIn" Runat="server" AlternateText="Sign In"></asp:Image>
</asp:LinkButton>
</TD>
</TR>
</TABLE>
</TD>
<TD vAlign="top" class="c2">
<h2 class="subHeadings">Create a New Account</h2>
<p>Sign up for an account today and benefit from faster checkout and more site features.</p>
<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0" class="Content formtbl">
<TR>
<TD class="name">First Name:&nbsp;</TD>
<TD class="input"><asp:TextBox id="txtCAFirstName" runat="server" tabIndex="5" cssclass="Content" MaxLength="100"></asp:TextBox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
</TR>
<TR>
<TD class="name">Last Name:&nbsp;</TD>
<TD class="input"><asp:TextBox id="txtCALastName" runat="server" tabIndex="6" cssclass="Content" MaxLength="100"></asp:TextBox><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
</TR>
<TR>
<TD class="name">E-Mail Address:&nbsp;</TD>
<TD class="input"><asp:TextBox id="txtCAEMail" runat="server" tabIndex="7" cssclass="Content" MaxLength="255"></asp:TextBox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
</TR>
<TR>
<TD class="name">Password:&nbsp;</TD>
<TD class="input"><asp:TextBox id="txtCAPassword" runat="server" TextMode="Password" tabIndex="8" cssclass="Content" MaxLength="255"></asp:TextBox><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
</TR>
<TR>
<TD class="name">Confirm Password:&nbsp;</TD>
<TD class="input"><asp:TextBox id="txtCAConfirmPassword" runat="server" TextMode="Password" tabIndex="9" cssclass="Content" MaxLength="255"></asp:TextBox><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
</TR>
<TR>
<TD class="name">Subscribe To Mail List:&nbsp;</TD>
<TD class="input"><asp:CheckBox id="chkSubscribe" runat="server" tabIndex="10"></asp:CheckBox></TD>
</TR>
<TR>
<TD colSpan="2" class="button">
<asp:LinkButton ID="btnCreate" Runat="server" tabIndex="11">
<asp:Image BorderWidth="0" ID="imgCreate" Runat="server" AlternateText="Create Account"></asp:Image>
</asp:LinkButton>
</TD>
</TR>
</TABLE>
</TD>
</TR>
</TABLE>
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
</TD></TR></TABLE></form>
</body>
</HTML>

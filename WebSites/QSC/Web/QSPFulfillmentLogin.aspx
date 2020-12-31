<%@ Page language="c#" CodeBehind="QSPFulfillmentLogin.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.QSPFulfillmentLogin" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Register TagPrefix="Login" TagName="Login" Src="login.ascx" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<HTML>
	<HEAD>
		<link REL="stylesheet" HREF="Includes/QSPFulfillment.css" TYPE="text/css">
	</HEAD>
	<body bgColor="white" leftMargin="0" topMargin="0" border="0" marginheight="0" marginwidth="0">
		<form id="Form1" runat="server">
			<!-- #include file="Includes/Menu.inc" -->
			<table width="100%" bgColor="#ffffff" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td align="right" bgColor="#336699" colspan="2"><IMG src="/QSPFulfillment/Images/LogoText.gif">
					</td>
				</tr>
				<tr valign="top">
					<td vAlign="top" align="right" bgColor="#003366" colspan="2">
						<a href="mailto:qsp_it@rd.com"><font color="white" face="verdana, arial" size="2"><b>help | 
									contact</b></font></a>
					</td>
				</tr>
				<tr valign="top">
					<td align="left" valign="top">
					</td>
					<td vAlign="top" align="right"><IMG src="/QSPFulfillment/Images/QSPLogo.jpg" 
                            height="57" width="213">
					</td>
				</tr>
				<TR>
					<td colspan="2" class="bodytext" align="center" height="38"><br>
						<asp:Label ID="lblStatus" Runat="server" cssClass="errorMessage"></asp:Label>
						<br>
						<br>
					</td>
				</TR>
			</table>
			<div align="center"><font color="#ff0000"></font></div>
			<table width="400" align="center" class="boxlook">
				<tr>
					<td align="center">
						<LOGIN:LOGIN id="QLogin" runat="server"></LOGIN:LOGIN>
						<span class="ClearTextBoxR" id="Message" runat="server" EnableViewState="false"></span>
					</td>
				</tr>
			</table>
			<table align="center" width="795">
				<tr>
					<td align="center" class="CSPlainText">
						<br>
						<br>
						* Usage of this system is restricted to authorized users only. *<br>
						<br>
					</td>
				</tr>
				<tr>
					<td width="789" align="center" class="CSPlainText">
					</td>
				</tr>
				<tr>
					<td height="38" align="center" class="CSPlainText">
					</td>
				</tr>
			</table>
			<!-- #Include File="Includes/Footer.inc" -->
			<P></P>
		</form>
	</body>
</HTML>

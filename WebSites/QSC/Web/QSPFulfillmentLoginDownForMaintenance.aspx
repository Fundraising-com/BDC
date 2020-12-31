<%@ Page language="c#" AutoEventWireup="True" %>
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
					<td vAlign="top" align="right"><IMG src="/QSPFulfillment/Images/loginlogo.gif">
					</td>
				</tr>
				<TR>
					<td colspan="2" class="bodytext" align="center" height="38"><br>
						<asp:Label ID="lblStatus" Runat="server" cssClass="errorMessage" 
                            Font-Bold="True" Font-Size="Larger">The Fulfillment site is down for maintenance. Please try back later.</asp:Label>
						<br>
						<br>
					</td>
				</TR>
			</table>
			<div align="center"><font color="#ff0000"></font></div>
			<table width="400" align="center" class="boxlook">
				<tr>
					<td align="center">
						<LOGIN:LOGIN id="QLogin" runat="server" Visible="False"></LOGIN:LOGIN>
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
						<img src="images/arr_b1.gif" width="9" height="9">&nbsp;Click <a href="mailto:qsp_it@rd.com">
							<font class="smallLinks">here</font></a> if you have forgotten your 
						password.
					</td>
				</tr>
				<tr>
					<td height="38" align="center" class="CSPlainText">
						<div align="center"><strong>Tech Support:</strong>&nbsp;<font color="#ff0000">(914) 
								244-5555</font> Monday - Friday 9AM to 5PM EST or <a href="mailto:qsp_it@rd.com">
								<font class="smallLinks">email</font></a> QSP IT</div>
					</td>
				</tr>
			</table>
			<!-- #Include File="Includes/Footer.inc" -->
			<P></P>
		</form>
	</body>
</HTML>

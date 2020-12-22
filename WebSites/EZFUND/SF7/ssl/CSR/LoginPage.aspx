<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LoginPage.aspx.vb" Inherits="StoreFront.StoreFront.LoginPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Order Entry</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.0

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2004 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
		<script language="javascript">
		function isKey(e, k)
{
        var key;
        if(window.event) //IE
        {
                e = window.event;
                key = e.keyCode;
        }
        else //Mozilla
        {
                key = e.which;
        }
        if (key == k)
        {
                return true;
        }
        return false;
}
                
function isEnterKey(e)
{
   return isKey(e, 13);
}
 
 
function postBack(e, eventTarget, eventArgument)
{
   e.returnValue=false;
   e.cancel = true;
   __doPostBack(eventTarget, eventArgument);
}
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server">
		<form id="Form1" method="post" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<TBODY>
					<tr>
						<td class="TopBanner" colSpan="3">
							<!-- Top Banner Start -->
							<!-- Top Banner End --></td>
					</tr>
					<tr>
						<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
							<!-- Top Sub Banner Start --> &nbsp; 
							<!-- Top Sub Banner End --></td>
					</tr>
					<tr>
						<td class="LeftColumn" id="LeftColumnCell">
							<!-- Left Column Start -->
							<!-- Left Column End -->
						</td>
						<td class="Content" vAlign="top">
							<!-- Content Start -->
							<table width="100%" cellSpacing="3" cellPadding="5" border="0">
								<tr>
								    <td class="Content" align="right">
									    <!-- Help Button -->
									    <a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/csr_login.asp  ')">
										    <img src="images/help.jpg" border="0"></a> 
									    <!-- End Help Button --></td>
							    </tr>
								<tr>
									<td class="content" align="center">
										<P id="ErrorAlignment" runat="server" align="center">
											<font color="#ff0000">
												<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
											</font>
										</P>
									</td>
								</tr>
								<tr>
									<td>
										<table>
											<tr>
												<td>Username:</td>
												<td><asp:TextBox ID="txtUser" Runat="server" Width="144px"></asp:TextBox></td>
											</tr>
											<tr>
												<td>Password:</td>
												<td><asp:TextBox ID="txtPass" Runat="server" TextMode="Password"></asp:TextBox></td>
											</tr>
											<tr>
												<td colspan="2">&nbsp;</td>
											</tr>
											<tr>
												<td>
													<asp:LinkButton ID="btnSignIn" Runat="server">
														<asp:Image BorderWidth="0" ID="imgSignIn" Runat="server" AlternateText="Sign In"></asp:Image>
													</asp:LinkButton>
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
		</form>
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>

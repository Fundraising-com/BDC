<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AddProductPopUp.aspx.vb" Inherits="StoreFront.StoreFront.AddProductPopUp"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Add Product Confirmation</title>
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
		<SCRIPT language="javascript">
		function openParent(sHref) {
			window.opener.location = sHref;
			window.close();
		}
		</SCRIPT>
	</HEAD>
	<body class="GeneralPage" MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" width="100%" runat="server">
				<tr>
					<td id="PageCell">
						<table class="GeneralTable" id="PageSubTable" cellSpacing="0" width="100%" runat="server">
							<tr>
								<td class="TopBanner" width="100%">
									<!-- Top Banner Start --> Thank You! 
									<!-- Top Banner End --></td>
							</tr>
							<tr>
								<td class="Content" vAlign="top" width="100%">
									<!-- Content Start -->
									<P id="ErrorAlignment" runat="server">
										<asp:Label id="lblErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="Content"><asp:label id="lblDisplay" runat="server" text="Confirmation Message"></asp:label></td>
										</tr>
									</table>
									<!-- Content End --></td>
							</tr>
							<tr>
								<td class="Content" align="middle">
									<!-- Footer Start -->
									<asp:LinkButton ID="btnCheckout" Runat="server">
										<asp:Image BorderWidth="0" ID="imgCheckout" Runat="server" AlternateText="Checkout"></asp:Image>
									</asp:LinkButton>
									<asp:LinkButton ID="btnClose" Runat="server">
										<asp:Image BorderWidth="0" ID="imgClose" Runat="server" AlternateText="Close"></asp:Image>
									</asp:LinkButton>
									<!-- Footer End --></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

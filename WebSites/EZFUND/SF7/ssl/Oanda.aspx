<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Oanda.aspx.vb" Inherits="StoreFront.StoreFront.Oanda" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Oanda Conversion</title>
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
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server" onsubmit="javascript: OandaMessage();">
			<table id="PageTable" cellSpacing="0" cellPadding="2" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" runat="server">
							<TBODY>
								<tr align="middle">
									<td class="TopBanner" align="middle" width="100%"><uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
										<!-- Top Banner End --></td>
								</tr>
								<tr>
									<td class="Content" vAlign="top" width="80%">
										<!-- Content Start -->
										<table cellSpacing="3" cellPadding="5" width="100%" border="0">
											<TBODY>
												<tr>
													<td class="Content" align="left">
														<div id="Message" align="<%= MessageAlign %>" class="Messages" style="VISIBILITY:visible">
															Please wait while Currencies are Downloaded.
														</div>
														Select your home currency from the drop down box below. Then click the 
														"Convert" button to set your home currency.<br>
														<br>
														<asp:DropDownList id="Dropdownlist1" runat="server"></asp:DropDownList></td>
												</tr>
											</TBODY>
										</table>
									</td>
								</tr>
							</TBODY>
						</table>
						<!-- Content End --></td>
				</tr>
				<tr>
					<td class="Content" align="middle">
						<!-- Footer Start -->
						<asp:LinkButton ID="cmdConvert" Runat="server">
							<asp:Image BorderWidth="0" ID="imgConvert" Runat="server" AlternateText="Convert"></asp:Image>
						</asp:LinkButton>
						&nbsp;
						<asp:LinkButton ID="cmdClose" Runat="server">
							<asp:Image BorderWidth="0" ID="imgClose" Runat="server" AlternateText="Close"></asp:Image>
						</asp:LinkButton>
						<!-- Footer End -->
					</td>
				</tr>
				</TD></TR>
			</table>
			</TD></TR></TBODY></TABLE></form>
		<script language="javascript">
OandaHideMessage();
		</script>
	</body>
</HTML>

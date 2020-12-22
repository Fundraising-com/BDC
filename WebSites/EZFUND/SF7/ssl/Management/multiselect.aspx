<%@ Register TagPrefix="uc1" TagName="StandardSearchControl2" Src="Controls/StandardSearchControl2.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="multiselect.aspx.vb" Inherits="StoreFront.StoreFront.multiselect"%>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
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
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="General.js"></script>
		<% Me.PageHeader %>
	</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<TR>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Select Options"></uc1:TopSubBanner>
					</td>
				</TR>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="content" align="middle">
									<P id="ErrorAlignment" runat="server" align="center">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</P>
								</td>
							</tr>
							<tr>
								<td class="Content">
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td class="ContentTable" HEIGHT="1" colspan="5">
												<img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1">
												<img src="images/clear.gif" HEIGHT="1"></td>
											<td class="Content" HEIGHT="1" colspan="3" width="100%">&nbsp;</td>
											<td class="ContentTable" HEIGHT="1">
												<img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1">
												<img src="images/clear.gif" HEIGHT="1"></td>
											<td class="Content">&nbsp;</td>
											<td class="Content" width="100%">
												<uc1:StandardSearchControl2 id="StandardSearchControl1" runat="server"></uc1:StandardSearchControl2>
											</td>
											<td class="Content">&nbsp;</td>
											<td class="ContentTable" HEIGHT="1">
												<img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" height="1"></td>
											<td class="Content"></td>
											<td class="Content" width="100%">&nbsp;</td>
											<td class="Content"></td>
											<td class="ContentTable" height="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" height="1"></td>
											<td class="Content"></td>
											<td class="Content" width="100%" align="right">
												<asp:LinkButton ID="btnBack" Runat="server" OnClick="BackClick">
													<asp:Image BorderWidth="0" ID="imgBack" runat="server" ImageUrl="images/Back.jpg" AlternateText="Back"></asp:Image>
												</asp:LinkButton>
												&nbsp;&nbsp;
												<asp:LinkButton ID="btnSubmit" Runat="server" OnClick="SubmitClick">
													<asp:Image BorderWidth="0" ID="imgSubmit" runat="server" ImageUrl="images/submit.jpg" AlternateText="Submit"></asp:Image>
												</asp:LinkButton>
											</td>
											<td class="Content"></td>
											<td class="ContentTable" height="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1">
												<img src="images/clear.gif" HEIGHT="1"></td>
											<td class="Content" colspan="3" width="100%">&nbsp;</td>
											<td class="ContentTable" HEIGHT="1">
												<img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1" colspan="5">
												<img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="StandardSearch" Src="Controls/StandardSearch.ascx" %>
<%@ Page Culture="en-us" enableViewState="True"  UICulture="en-us" Language="vb" AutoEventWireup="false" Codebehind="Search.aspx.vb" Inherits="StoreFront.StoreFront.Search"%>
<%@ Register TagPrefix="uc1" TagName="AdvancedSearch" Src="Controls/AdvancedSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Search</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.1.0

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
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
			function onReturn()
			{
				if (event.keyCode == 13)
				{
					if (document.all["StandardSearch1_btnSearch"] == null)
						document.all["AdvancedSearch1_btnAdvSearch"].click();
					else
						document.all["StandardSearch1_btnSearch"].click();
				}
			}
		
			function SetValidation()
			{
			//window.alert(window.document.Form2.item("AdvancedSearch1:AdvPriceStart").value);
			//		window.alert("hi");
			//window.alert(window.document.Form2.elements["AdvancedSearch1:AdvPriceStart"].value);
 			//	return false	
 			if ((window.document.Form2.elements["AdvancedSearch1:AdvPriceStart"] != null) && (window.document.Form2.elements["AdvancedSearch1:AdvPriceEnd"] != null)){
				if (window.document.Form2.elements["AdvancedSearch1:AdvPriceStart"].value !="" || window.document.Form2.elements["AdvancedSearch1:AdvPriceEnd"].value !="") {
					window.document.Form2.elements["AdvancedSearch1:AdvPriceEnd"].required=true;
					window.document.Form2.elements["AdvancedSearch1:AdvPriceEnd"].title="Price End";
					window.document.Form2.elements["AdvancedSearch1:AdvPriceStart"].required=true;
					window.document.Form2.elements["AdvancedSearch1:AdvPriceStart"].title="Price Start";
					window.document.Form2.elements["AdvancedSearch1:AdvPriceEnd"].number=true;
					window.document.Form2.elements["AdvancedSearch1:AdvPriceStart"].number=true;
					return ValidateForm(window.document.Form2)
						}
					}
				}
		</script>
	</HEAD>
	<body id="BodyTag" runat="server" class="generalpage" onkeydown="onReturn();">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1"
							runat="server">
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
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="Instructions">
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="content">
												<P id="ErrorAlignment" runat="server" align="center">
													<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label>
												</P>
											</td>
										</tr>
										<tr>
											<td class="Content">
												<uc1:StandardSearch id="StandardSearch1" runat="server"></uc1:StandardSearch>
												<uc1:AdvancedSearch id="AdvancedSearch1" runat="server" Visible="False"></uc1:AdvancedSearch>
											</td>
										</tr>
									</table>
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

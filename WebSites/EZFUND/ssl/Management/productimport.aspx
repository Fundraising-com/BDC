<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductImport.aspx.vb" Inherits="StoreFront.StoreFront.ProductImport"%>
<%@ Register TagPrefix="uc1" TagName="UploadProductFileControl" Src="Controls/UploadProductFileControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.0.0

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
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<% me.WriteRefresh %>
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
		<script language="javascript">
		
			function SetValidationContinue()
			{
				if (window.document.Form1.elements["UploadProductFileControl1:FileType"].options[window.document.Form1.elements["UploadProductFileControl1:FileType"].options.selectedIndex].value == "Text") 
				{
					if (window.document.Form1.elements["UploadProductFileControl1:FileDelimiter"].options[window.document.Form1.elements["UploadProductFileControl1:FileDelimiter"].options.selectedIndex].value == "Other")
					{
					window.document.Form1.elements["UploadProductFileControl1:OtherDelimiter"].required=true;
					window.document.Form1.elements["UploadProductFileControl1:OtherDelimiter"].title="Other Delimiter";
					}
					else
					{
					window.document.Form1.elements["UploadProductFileControl1:OtherDelimiter"].required=false;
					}
				}
				return ValidateForm(window.document.Form1)
			}
			
			function SetValidationImport()
			{var e
			var strSelections
			var bduplicates
			var bEmpty
			bEmpty=true
			bduplicates=false
			strSelections=";"
			
			for (var i = 0; i < window.document.Form1.length; i++) 
			{
				e = window.document.Form1.elements[i]
				if (e.type=="select-one" && e.name.indexOf("SFFields")>-1)
				{
					if (e.options[e.options.selectedIndex].value != "skip")
						{bEmpty=false;
						if (strSelections.indexOf(";" + e.options[e.options.selectedIndex].value + ";") >-1)
							{bduplicates=true}
						else
							{strSelections=strSelections+e.options[e.options.selectedIndex].value + ";"}
								
					}
				}
			}
			if (bduplicates==true)
			{window.alert("Please select each field only once.")
			return false}
			
			if (bEmpty==true)
			{window.alert("Please select at least one field to import.")
			return false}
			}
		
		</script>
	</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form enctype="multipart/form-data" runat="server" ID="Form1">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<!-- Top Banner Start -->
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td class="TopBanner2" width="20%"><img src="images/sflogo.jpg"></td>
								<td class="TopBanner">&nbsp;&nbsp;Merchant Tools</td>
							</tr>
						</table>
						<!-- Top Banner End -->
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<!-- Top Sub Banner Start --> Products Import 
						<!-- Top Sub Banner End -->
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End -->
					</td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<A href="javascript: doHelp(' http://support.storefront.net/mtdocs/products_import.asp ')">
										<img src="images/help.jpg" border="0"></A> 
									<!-- End Help Button -->
								</td>
							</tr>
							<tr>
								<td class="Instructions">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="content" align="middle">
									<p id="ErrorAlignment" runat="server">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</p>
								</td>
							</tr>
							<tr>
								<td class="Content" align="middle">
									<uc1:UploadProductFileControl id="UploadProductFileControl1" runat="server"></uc1:UploadProductFileControl>
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

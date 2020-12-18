<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductSelect.aspx.vb" Inherits="StoreFront.StoreFront.ProductSalesSelect"%>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Store Reports</title>
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
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--
		function SetValidation()
			{
				if (window.document.Form2.elements["txtProd"].value=="" && window.document.Form2.elements["ddProducts"].options.selectedIndex==0 && window.document.Form2.elements["ddCat"].options.selectedIndex==0)
				{window.alert("Please select criteria for report")
				return false
				}
			}
			
			
function resetboxes(ddlist)
{
}
//-->
		</script>
		
	</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
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
						<!-- Top Banner End --></td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<!-- Top Sub Banner Start --> Sales Reports 
						<!-- Top Sub Banner End --></td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button --><a href="javascript: doHelp(' http://support.storefront.net/mtdocs/reports_ov.asp ')"><img src="images/help.jpg" border="0"></a>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="content" align="middle">
									<p id="ErrorAlignment" runat="server"><font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</p>
								</td>
							</tr>
							<tr class="Headings" vAlign="top">
								<td class="Headings">&nbsp;Product Sales
								</td>
							</tr>
							<tr>
								<td class="Content">&nbsp;<asp:label id="DateInfo" CssClass="Content" Runat="server"></asp:label></td>
							</tr>
							<tr class="Headings" vAlign="top">
								<td class="Headings">&nbsp;View Sales For a Product
								</td>
							</tr>
							<tr>
								<td class="content">&nbsp;
								</td>
							</tr>
							<tr>
								<td class="Content">&nbsp;Select a Product:&nbsp;<asp:dropdownlist id="ddProducts" runat="server" CssClass="content" AutoPostBack="True"></asp:dropdownlist>
								</td>
							</tr>
							<tr>
								<td class="content">&nbsp;
								</td>
							</tr>
							<tr>
								<td class="Content">&nbsp;or Enter a Product ID or Product Name:&nbsp;<asp:textbox id="txtProd" CssClass="content" Runat="server"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td class="content">&nbsp;
								</td>
							</tr>
							<tr class="Headings" vAlign="top">
								<td class="Headings">&nbsp;View Sales For All Products in a Category
								</td>
							</tr>
							<tr>
								<td class="content">&nbsp;
								</td>
							</tr>
							<tr>
								<td class="Content" height="15">&nbsp;Select a Category:&nbsp;<asp:dropdownlist id="ddCat" runat="server" CssClass="content" DataMember="Categories" DataValueField="Name" DataTextField="Name" AutoPostBack="True"></asp:dropdownlist>
								</td>
							</tr>
							<tr>
								<td class="content">&nbsp;
								</td>
							</tr>
							<tr>
								<td class="content" align="right">
									<asp:LinkButton ID="cmdSubmit" Runat="server">
										<asp:Image BorderWidth="0" ID="imgSubmit" runat="server" ImageUrl="images/submit.jpg" AlternateText="Submit"></asp:Image>
									</asp:LinkButton>
								</td>
							</tr>
							<tr>
								<td class="content">&nbsp;
								</td>
							</tr>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

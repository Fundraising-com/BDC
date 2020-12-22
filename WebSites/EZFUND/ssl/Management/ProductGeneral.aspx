<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductGeneral.aspx.vb" Inherits="StoreFront.StoreFront.ProductGeneral"%>
<%@ Register TagPrefix="uc1" TagName="ProductGeneralControl" Src="Controls/ProductGeneralControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
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
		<title><% writeTitle() %>  - Merchant Tools</title>
		
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
		<script language="javascript">
		
			function SetValidation()
			{
			
			window.document.Form2.elements["ProductGeneralControl1:ProdCode"].required=true;
			window.document.Form2.elements["ProductGeneralControl1:ProdCode"].title="Product Code";
			window.document.Form2.elements["ProductGeneralControl1:ProdName"].required=true;
			window.document.Form2.elements["ProductGeneralControl1:ProdName"].title="Product Name";
			window.document.Form2.elements["ProductGeneralControl1:ProdPrice"].number=true;
			if (window.document.Form2.elements["ProductGeneralControl1:ProdPrice"].value=="")
			{window.document.Form2.elements["ProductGeneralControl1:ProdPrice"].value="0"}
			window.document.Form2.elements["ProductGeneralControl1:ProdPrice"].title="Product Price";
			window.document.Form2.elements["ProductGeneralControl1:ProdCost"].number=true;
			if (window.document.Form2.elements["ProductGeneralControl1:ProdCost"].value=="")
			{window.document.Form2.elements["ProductGeneralControl1:ProdCost"].value="0"}
			window.document.Form2.elements["ProductGeneralControl1:ProdCost"].title="Product Cost";
			
			
			return ValidateForm(window.document.Form2)
			}
			
		
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
						<!-- Top Banner End -->
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<!-- Top Sub Banner Start --> Products 
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
									<A href="javascript: doHelp(' http://support.storefront.net/mtdocs/products_gen.asp ')">
										<img src="images/help.jpg" border="0"></A> 
									<!-- End Help Button -->
								</td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End -->
								</td>
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
							<TR>
								<TD class="content" vAlign="top" align="middle">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TBODY>
											<tr>
												<td class="ContentTable" colSpan="17" height="1"><IMG height="1" src="images/clear.gif"></td>
											</tr>
											
												<TR id="Add" runat="server">
													<TD class="ContentTableHeader" align="left" colSpan="17" width=100%>&nbsp;New Product</TD>
												</TR>
												<TR id="Add2" runat="server">
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle"><A class="content" href="ProductGeneral.aspx"><B>&nbsp;General&nbsp;</B></A>
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle">&nbsp;Details&nbsp;
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle">&nbsp;Categories&nbsp;
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle">&nbsp;Attributes&nbsp;
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle">&nbsp;Fulfillment&nbsp;
													</TD>
													<TD class="ContentTableHeader" id="InvCell1" width="1" runat=server><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" id="InvCell2" align="middle" runat=server>&nbsp;Inventory&nbsp;
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle">&nbsp;Discounts&nbsp;
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle">&nbsp;Marketing&nbsp;
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												</TR>
												
											
											
												<TR ID="Edit" runat="server">
													<TD class="ContentTableHeader" align="left" colSpan="17" width=100%>&nbsp;
														<asp:label id="lblPDName" runat="server" CssClass="ContentTableHeader">Product Name</asp:label></TD>
												</TR>
												<TR ID="Edit2" runat="server">
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle"><A class="content" href="ProductGeneral.aspx"><B>&nbsp;General&nbsp;</B></A>
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle"><A class="content" href="ProductDetails.aspx">&nbsp;Details&nbsp;</A>
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle"><A class="content" href="ProductCategories.aspx">&nbsp;Categories&nbsp;</A>
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle"><A class="content" href="attributesmng.aspx">&nbsp;Attributes&nbsp;</A>
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle"><A class="content" href="productfulfillment.aspx">&nbsp;Fulfillment&nbsp;</A>
													</TD>
													<TD class="ContentTableHeader" width="1" id=InvCell3 runat=server><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle" id=InvCell4 runat=server><A class="content" href="inventorymng.aspx">&nbsp;Inventory&nbsp;</A>
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle"><A class="content" href="productdiscounts.aspx">&nbsp;Discounts&nbsp;</A>
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
													<TD class="Content" align="middle"><A class="content" href="productmarketing.aspx">&nbsp;Marketing&nbsp;</A>
													</TD>
													<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												</TR>
												
												
											<TR>
													<TD class="ContentTableHeader" colSpan="17" height="1"><IMG height="1" src="images/clear.gif"></TD>
												</TR>
											<TR>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<TD class="content" vAlign="top" align="middle" width="100%" colSpan="15">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="middle">
															<uc1:ProductGeneralControl id="ProductGeneralControl1" runat="server"></uc1:ProductGeneralControl>
															</td>
													</tr>
												</table>
											</TD>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
							</TR>
							<tr>
								<td class="ContentTable" colSpan="18" height="1"><IMG height="1" src="images/clear.gif"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!-- Content End --> </TD></TR></TBODY></TABLE></form>
	</body>
</HTML>

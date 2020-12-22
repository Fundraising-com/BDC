<%@ Register TagPrefix="uc1"  TagName="attributechoice" Src="Controls/attributechoice.ascx" %>
<%@ Register TagPrefix="uc1" TagName="attTemplates" Src="Controls/attTemplates.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="attributesmng.aspx.vb" Inherits="StoreFront.StoreFront.attributesmng" %>
<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
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
			- Merchant Tools</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.0.1

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
		<script language="JavaScript" src="../General.js"></script>
		<script language="javascript">
		
			function SetValidationAdd()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["attTemplates1:txtAttName"].required=true;
				window.document.Form2.elements["attTemplates1:txtAttName"].title="Attribute Name";
			
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationAttDetail()
			{
				ResetForm(window.document.Form2);
			
				window.document.Form2.elements["attTemplates1:Attdetailctrl1:txtDetailName"].required=true;
				window.document.Form2.elements["attTemplates1:Attdetailctrl1:txtDetailName"].title="Option";				
				window.document.Form2.elements["attTemplates1:Attdetailctrl1:txtPrice"].number=true;
				if (window.document.Form2.elements["attTemplates1:Attdetailctrl1:txtPrice"].value=="")
					{window.document.Form2.elements["attTemplates1:Attdetailctrl1:txtPrice"].value="0"}
				window.document.Form2.elements["attTemplates1:Attdetailctrl1:txtPrice"].title="Price";
				window.document.Form2.elements["attTemplates1:Attdetailctrl1:txtWeight"].number=true;
				if (window.document.Form2.elements["attTemplates1:Attdetailctrl1:txtWeight"].value=="")
					{window.document.Form2.elements["attTemplates1:Attdetailctrl1:txtWeight"].value="0"}
				window.document.Form2.elements["attTemplates1:Attdetailctrl1:txtWeight"].title="Weight";
			
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationAttMain()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["attTemplates1:AttMainctrl1:txtAttName"].required=true;
				window.document.Form2.elements["attTemplates1:AttMainctrl1:txtAttName"].title="Name";				
				
				if (window.document.Form2.elements["attTemplates1:AttMainctrl1:InvPrompt"].value=="true")
				{
					if (!ConfirmCancel('Inventory Stock will be overwritten. Do you want to continue?'))
					{return false}
				}
				if (window.document.Form2.elements["attTemplates1:AttMainctrl1:txtPrice"]!=null)
					{window.document.Form2.elements["attTemplates1:AttMainctrl1:txtPrice"].number=true;
					if (window.document.Form2.elements["attTemplates1:AttMainctrl1:txtPrice"].value=="")
						{window.document.Form2.elements["attTemplates1:AttMainctrl1:txtPrice"].value="0"}
					window.document.Form2.elements["attTemplates1:AttMainctrl1:txtPrice"].title="Price";
					
					}
				if (window.document.Form2.elements["attTemplates1:AttMainctrl1:txtWeight"]!=null)
					{window.document.Form2.elements["attTemplates1:AttMainctrl1:txtWeight"].number=true;
					if (window.document.Form2.elements["attTemplates1:AttMainctrl1:txtWeight"].value=="")
						{window.document.Form2.elements["attTemplates1:AttMainctrl1:txtWeight"].value="0"}
					window.document.Form2.elements["attTemplates1:AttMainctrl1:txtWeight"].title="Weight";
					}
				return ValidateForm(window.document.Form2)
			}
		</script>
	</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" enctype="multipart/form-data" method="post" runat="server">
			<table class="GeneralTable" runat="server">
				<tr>
					<td class="TopBanner" colspan="3">
						<!-- Top Banner Start --> Merchant Tools 
						<!-- Top Banner End --></td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<!-- Top Sub Banner Start --> Products 
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
									<!-- Help Button -->
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs/products_attr.asp ')">
										<img src="images/help.jpg" border="0"></a> 
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
							<TR>
								<td class="content" vAlign="top" align="middle">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server" id="Table001">
										<tr>
											<td class="ContentTable" colSpan="17" height="1">
												<img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" align="left" colSpan="17" width="100%">&nbsp;<asp:label id="lblPDName" Runat="server" CssClass="ContentTableHeader">&nbsp;Product 
			Name</asp:label></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<a class="content" href="ProductGeneral.aspx">&nbsp;General&nbsp;</a>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<a class="content" href="ProductDetails.aspx">&nbsp;Details&nbsp;</a>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<a class="content" href="ProductCategories.aspx">&nbsp;Categories&nbsp;</a>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<a class="content" href="attributesmng.aspx"><b>&nbsp;Attributes&nbsp;</b></a>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<a class="content" href="productfulfillment.aspx">&nbsp;Fulfillment&nbsp;</a>
											</td>
											<td class="ContentTableHeader" width="1" id="InvCell1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle" id="InvCell2">
												<a class="content" href="inventorymng.aspx">&nbsp;Inventory&nbsp;</a>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<a class="content" href="productdiscounts.aspx">&nbsp;Discounts&nbsp;</a>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="middle">
												<a class="content" href="productmarketing.aspx">&nbsp;Marketing&nbsp;</a>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="17" height="1">
												<img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content" vAlign="top" align="middle" colSpan="15">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
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
														<td class="content" align="middle">
															<uc1:attributechoice id="Attributechoice1" runat="server"></uc1:attributechoice>
															<p>
																<uc1:attTemplates id="attTemplates1" runat="server"></uc1:attTemplates></p>
															<p>
															</p>
														</td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="18" height="1">
												<img height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</td>
							</TR>
						</table>
						<!-- Content End -->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

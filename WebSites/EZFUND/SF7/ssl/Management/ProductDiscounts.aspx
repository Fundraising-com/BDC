<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductDiscounts.aspx.vb" Inherits="StoreFront.StoreFront.ProductDiscounts"%>
<%@ Register TagPrefix="uc1" TagName="ProductDiscountsControl" Src="Controls/ProductDiscountsControl.ascx" %>
<%@ Import Namespace="Storefront.systembase"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Merchant Tools</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.0

'@STARTCOPYRIGHT
'The contents of this file is protected under the United States
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
		
			function SetValidation()
			{
			    //Tee 7/16/2007 check prices
			    if (!ChkPrice()){return false;}
			    if (window.document.Form2.elements["ProductDiscounts:tbClearance"].value=="" && window.document.Form2.elements["ProductDiscounts:cbClearance"].checked==true){
					alert("Clearance Price cannot be empty.");
					window.document.Form2.elements["ProductDiscounts:tbClearance"].focus();
					return false;
			    }
			    //end Tee
			    if (window.document.Form2.elements["ProductDiscounts:SalePrice"].value=="" && window.document.Form2.elements["ProductDiscounts:ActivateSale"].checked==true) {
			    alert("Sale Price cannot be empty")
			    window.document.Form2.elements["ProductDiscounts:SalePrice"].focus();
					return false;
			    }
			    else if (window.document.Form2.elements["ProductDiscounts:SalePrice"].value==""){
					window.document.Form2.elements["ProductDiscounts:SalePrice"].value="0";
				}
			    
				window.document.Form2.elements["ProductDiscounts:SalePrice"].number=true;
				/*
				if (window.document.Form2.elements["ProductDiscounts:SalePrice"].value=="")
					{
					window.document.Form2.elements["ProductDiscounts:SalePrice"].value="0"}
				window.document.Form2.elements["ProductDiscounts:SalePrice"].title="Sale Price";
			
				for (var i = 0; i < window.document.Form2.length; i++) 
				{
					e = window.document.Form2.elements[i]
					if (e.type=="text" && (e.name.indexOf("BreakLevel")>-1 || e.name.indexOf("Amount")>-1))
					{
						e.number=true;
						if (e.value=="")
							{e.value="0"}
						if (e.name.indexOf("BreakLevel")>-1 )
						{e.title="Amount Of Purchase";}
						else
						{e.title="Discount Amount";}
					}
				}
				*/
				return ValidateForm(window.document.Form2)
			}
			//Tee 7/16/2007 added validation to make checkbox selection exclusive
			function ValidateExclusive(e){
				if (e.checked && e.id.indexOf("ActivateSale") != -1){
					window.document.Form2.elements["ProductDiscounts:cbClearance"].checked = false;
					window.document.Form2.elements["ProductDiscounts:tbClearance"].value = "";
					return;
				}
				if (e.checked && e.id.indexOf("cbClearance") != -1){
					window.document.Form2.elements["ProductDiscounts:ActivateSale"].checked = false;
					window.document.Form2.elements["ProductDiscounts:SalePrice"].value = "";
					return;
				}
			}
			//Tee 7/16/2007 check prices
			function ChkPrice(){
				var listPrice = window.document.Form2.elements["ProductDiscounts:hidListPrice"].value;
				var cost = window.document.Form2.elements["ProductDiscounts:hidCost"].value;
				var sale = window.document.Form2.elements["ProductDiscounts:SalePrice"].value;
				var clearance = window.document.Form2.elements["ProductDiscounts:tbClearance"].value;
				if (sale != "" && !IsNumeric(sale)){
					alert("Please enter valid Sale Price.");
					return false;
				}
				if  (clearance != "" && !IsNumeric(clearance)){
					alert("Please enter valid Clearance Price.");
					return false;
				}
				if ((sale != "" && parseInt(sale) > parseInt(listPrice)) || (clearance != "" && parseInt(clearance) > parseInt(listPrice))){
					return confirm("You have entered a discounted price greater than the original List Price, are you sure you want to continue?");
				}
				if ((sale != "" && parseInt(sale) < parseInt(cost)) || (clearance != "" && parseInt(clearance) < parseInt(cost))){
					return confirm("You have entered a discounted price less than the cost of the product, are you sure you want to continue?");
				}
				return true;
			}
			//Tee 7/16/2007 chk numeric
			function IsNumeric(sText){
   				var ValidChars = "0123456789.";
   				var IsNumber=true;
   				var Char;
   				for (i = 0; i < sText.length && IsNumber == true; i++){ 
      				Char = sText.charAt(i); 
      				if (ValidChars.indexOf(Char) == -1){
         				IsNumber = false;
         			}
      			}
   				return IsNumber; 
   			}
		</script>
	</HEAD>
	<body class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table cellspacing="0" class="GeneralTable">
				<tr>
					<td class="TopBanner" colspan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Products"></uc1:TopSubBanner>
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start -->
						<uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table width="100%" cellSpacing="3" cellPadding="5" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button -->
									<a href="javascript: doHelp('<%=HelpUrl()%>')">
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
								<td class="content" align="center">
									<p id="ErrorAlignment" runat="server">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</p>
								</td>
							</tr>
							<tr>
								<td class="content" vAlign="top" align="center">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server"
										id="Table001">
										<tr>
											<td class="ContentTable" colSpan="25" height="1">
												<img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" align="left" colSpan="25" width="100%">&nbsp;<asp:label id="lblPDName" Runat="server" CssClass="ContentTableHeader">&nbsp;Product 
			Name</asp:label></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">
												<%= IIF(restrictedPages(tasks.productgeneral)=true,"<span class=DisableLink>&nbsp;General&nbsp;</span>","<a class=content href='ProductGeneral.aspx'>&nbsp;General&nbsp;</a>")%>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">
												<%= IIF(restrictedPages(tasks.productdetails)=true,"<span class=DisableLink>&nbsp;Details&nbsp;</span>","<a class=content href='Productdetails.aspx'>&nbsp;Details&nbsp;</a>")%>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">
												<%= IIF(restrictedPages(tasks.productcategories)=true,"<span class=DisableLink>&nbsp;Categories&nbsp;</span>","<a class=content href='ProductCategories.aspx'>&nbsp;Categories&nbsp;</a>")%>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<%-- Tee 8/17/2007 product configurator --%>
											<TD class="Content" align="center" id="tcEditAttributes" runat="Server"><%= IIF(restrictedPages(tasks.productattributes)=true,"<span class=DisableLink>&nbsp;Attributes&nbsp;</span>","<a class=content href='attributesmng.aspx'>&nbsp;Attributes&nbsp;</a>")%>
											</TD>
											<TD class="ContentTableHeader" width="1" id="tcEditAttributesSP" runat="server"><IMG src="images/clear.gif" width="1"></TD>
											<TD class="Content" align="center" id="tcEditImageControl" runat="server"><%= IIF(restrictedPages(tasks.ProductImages)=true,"<span class=DisableLink>&nbsp;Image Control&nbsp;</span>","<a class=content href='ProductImages.aspx'>&nbsp;Image Control&nbsp;</a>")%></TD>
											<TD class="ContentTableHeader" width="1" id="tcEditImageControlSP" runat="server"><IMG src="images/clear.gif" width="1"></TD>
											<TD class="Content" id="tcEditFulfillment" align="center" runat="server"><%= IIF(restrictedPages(tasks.fulfillment)=true,"<span class=DisableLink>&nbsp;Fulfillment&nbsp;</span>","<a class=content href='productfulfillment.aspx'>&nbsp;Fulfillment&nbsp;</a>")%>
											</TD>
											<TD class="ContentTableHeader" id="tcEditFulfillmentSP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
											<TD class="Content" id="tcEditInventory" align="center" runat="Server"><%= IIF(restrictedPages(tasks.inventory)=true,"<span class=DisableLink>&nbsp;Inventory&nbsp;</span>","<a class=content href='inventorymng.aspx'>&nbsp;Inventory&nbsp;</a>")%>
											</TD>
											<TD class="ContentTableHeader" id="tcEditInventorySP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
											<%-- end Tee --%>
											<td class="Content" align="center">
												<a class="content" href="productdiscounts.aspx"><b>&nbsp;Discounts&nbsp;</b></a>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<%-- Tee 7/31/2007 product configurator --%>
											<TD class="Content" id="tcEditMarketing" runat="server" align="center">
												<%= IIF(restrictedPages(tasks.marketing)=true,"<span class=DisableLink>&nbsp;Marketing&nbsp;</span>","<a class=content href='productmarketing.aspx'>&nbsp;Marketing&nbsp;</a>")%>
											</TD>
											<TD class="ContentTableHeader" id="tcEditMerchantSP" runat="server" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<TD class="Content" id="tcEditMerchantBundleComponents" align="center" runat="server">
												<%= IIF(restrictedPages(tasks.BundleComponents)=true,"<span class=DisableLink>&nbsp;Bundle&nbsp;Components&nbsp;</span>","<a class=content href='BundleComponents.aspx?ProdType=2'>&nbsp;Bundle&nbsp;Components&nbsp;</a>")%>
											</TD>
											<TD class="ContentTableHeader" id="tcEditCustBundleSP" runat="server" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<TD class="Content" id="tcEditCustBundleComponents" align="center" runat="server">
												<%= IIF(restrictedPages(tasks.BundleComponents)=true,"<span class=DisableLink>&nbsp;Bundle&nbsp;Components&nbsp;</span>","<a class=content href='BundleComponents.aspx?ProdType=4'>&nbsp;Bundle&nbsp;Components&nbsp;</a>")%>
											</TD>
											<TD class="ContentTableHeader" id="tcEditCustDefinedSP" runat="server" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<TD class="Content" id="tcEditCustDefinedRules" align="center" runat="server">
												<%= IIF(restrictedPages(tasks.CustomerDefinedRules)=true,"<span class=DisableLink>&nbsp;&nbsp;Customer Defined Rules&nbsp;&nbsp;</span>","<a class=content href='CustomerDefinedRules.aspx'>&nbsp;Customer Defined Rules&nbsp;</a>")%>
											</TD>
											<%-- end Tee --%>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="25" height="1">
												<img height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
											<td class="content" vAlign="top" align="center" colSpan="23">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="center">
															<uc1:ProductDiscountsControl id="ProductDiscounts" runat="server"></uc1:ProductDiscountsControl>
														</td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1">
												<img src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="25" height="1">
												<img height="1" src="images/clear.gif"></td>
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

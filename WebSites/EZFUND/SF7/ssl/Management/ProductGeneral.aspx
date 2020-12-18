<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductGeneral.aspx.vb" Inherits="StoreFront.StoreFront.ProductGeneral"%>
<%@ Register TagPrefix="uc1" TagName="ProductGeneralControl" Src="Controls/ProductGeneralControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@Import Namespace="Storefront.systembase"%>
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
			
			if (window.document.Form2.elements["ProductGeneralControl1:ProdPrice"]!=null){
				window.document.Form2.elements["ProductGeneralControl1:ProdPrice"].number=true;
				if (window.document.Form2.elements["ProductGeneralControl1:ProdPrice"].value=="")
				{window.document.Form2.elements["ProductGeneralControl1:ProdPrice"].value="0"}
				window.document.Form2.elements["ProductGeneralControl1:ProdPrice"].title="Product Price";
				window.document.Form2.elements["ProductGeneralControl1:ProdCost"].number=true;
				if (window.document.Form2.elements["ProductGeneralControl1:ProdCost"].value=="")
				{window.document.Form2.elements["ProductGeneralControl1:ProdCost"].value="0"}
				window.document.Form2.elements["ProductGeneralControl1:ProdCost"].title="Product Cost";
			}
			
			if (window.document.Form2.elements["ProductGeneralControl1:txtSubscriptionPrice"]!=null)
			{
				window.document.Form2.elements["ProductGeneralControl1:txtSubscriptionPrice"].number=true;
				if (window.document.Form2.elements["ProductGeneralControl1:txtSubscriptionPrice"].value=="")
				{window.document.Form2.elements["ProductGeneralControl1:txtSubscriptionPrice"].value="0"}
				window.document.Form2.elements["ProductGeneralControl1:txtSubscriptionPrice"].title="Subscription Price";
			}
			if (window.document.Form2.elements["ProductGeneralControl1:txtSubscriptionTerm"]!=null)
			{
				window.document.Form2.elements["ProductGeneralControl1:txtSubscriptionTerm"].number=true;
				if (window.document.Form2.elements["ProductGeneralControl1:txtSubscriptionTerm"].value=="")
				{window.document.Form2.elements["ProductGeneralControl1:txtSubscriptionTerm"].value="0"}
				window.document.Form2.elements["ProductGeneralControl1:txtSubscriptionTerm"].title="Subscription Term";
			}
			
			if (window.document.Form2.elements["ProductGeneralControl1:txtBillingDelay"]!=null)
			{
				window.document.Form2.elements["ProductGeneralControl1:txtBillingDelay"].number=true;
				if (window.document.Form2.elements["ProductGeneralControl1:txtBillingDelay"].value=="")
				{window.document.Form2.elements["ProductGeneralControl1:txtBillingDelay"].value="0"}
				window.document.Form2.elements["ProductGeneralControl1:txtBillingDelay"].title="Billing Delay";
			}
			
			return ValidateForm(window.document.Form2)
			}
			//Tee 8/7/2007 product configurator
			function Validation(){
				if (document.getElementById("ProductGeneralControl1_trPriceSetting")!=null){
					if (document.getElementById("ProductGeneralControl1_rbPrice").checked){
						var price = document.getElementById("ProductGeneralControl1_txtPrice").value;
						if (price==""){
							alert("Please provide valid price for bundle if\n'Set price for bundle' is checked.");
							return false;
						}
						if (!IsNumeric(price)){
							alert("Please provide a valid Price for the bundle.");
							return false;
						}
					}else{
						var amount = document.getElementById("ProductGeneralControl1_txtAmount").value;
						var minus = document.getElementById("ProductGeneralControl1_rbMinus");
						if (!IsNumeric(amount)){
							alert("Please provide a valid Amount for the bundle.");
							return false;
						}
						if (document.getElementById("ProductGeneralControl1_rbPercent").checked && minus.checked){
							if (!IsPercent(amount)){
								alert("Please provide percentage value between 0 to 100\nif 'Minus' and 'Percent' are selected.");
								return false;
							}
						}
						if (minus.checked && document.getElementById("ProductGeneralControl1_rbDollar").checked){
							var lowestPrice = document.getElementById("ProductGeneralControl1_hidMinPrice").value;
							if (parseFloat(amount) > parseFloat(lowestPrice)){
								alert("The lowest possible price for this bundle is $" + lowestPrice + ".\nPlease decrease your amount.");
								return false;
							}
						}
					}
				}
				return SetValidation();
			}
			function IsPercent(sText){
				if (parseInt(sText) > 100 || parseInt(sText) < 0){
					return false;
				}
				return true;
			}
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
			//end Tee
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server">
		<form id="Form2" method="post" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<tr>
					<td class="TopBanner" colSpan="3"><uc1:topbanner id="TopBanner2" runat="server"></uc1:topbanner></td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Products"></uc1:TopSubBanner>
					</td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav2" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table cellSpacing="3" cellPadding="5" width="100%" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button --><A href="javascript: doHelp('<%=HelpUrl()%>')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="content" align="center">
									<p id="ErrorAlignment" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></p>
								</td>
							</tr>
							<TR>
								<TD class="content" vAlign="top" align="center">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TBODY>
											<tr>
												<td class="ContentTable" colSpan="25" height="1"><IMG height="1" src="images/clear.gif"></td>
											</tr>
											<TR id="Add" runat="server">
												<TD class="ContentTableHeader" align="left" width="100%" colSpan="25">&nbsp;New 
													Product</TD>
											</TR>
											<TR id="Add2" runat="server">
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" align="center"><A class="content" href="ProductGeneral.aspx"><B>&nbsp;General&nbsp;</B></A>
												</TD>
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" align="center"><%= IIF(restrictedPages(tasks.productdetails)=true,"<span class=DisableLink>&nbsp;Details&nbsp;</span>","&nbsp;Details&nbsp;")%>
												</TD>
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" align="center"><%= IIF(restrictedPages(tasks.productcategories)=true,"<span class=DisableLink>&nbsp;Categories&nbsp;</span>","&nbsp;Categories&nbsp;")%>
												</TD>
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												<%-- Tee 8/17/2007 product configurator --%>
												<TD class="Content" id="tcAddAttributes" align="center" runat="Server"><%= IIF(restrictedPages(tasks.productattributes)=true,"<span class=DisableLink>&nbsp;Attributes&nbsp;</span>","&nbsp;Attributes&nbsp;")%>
												</TD>
												<TD class="ContentTableHeader" id="tcAddAttributesSP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" align="center" id="tcAddImageControl" runat="server"><%= IIF(restrictedPages(tasks.ProductImages)=true,"<span class=DisableLink>&nbsp;Image Control&nbsp;</span>","&nbsp;Image Control&nbsp;")%></TD>
												<TD class="ContentTableHeader" width="1" id="tcAddImageControlSP" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" id="tcAddFulfillment" align="center" runat="server"><%= IIF(restrictedPages(tasks.ProductImages)=true,"<span class=DisableLink>&nbsp;Fulfillment&nbsp;</span>","&nbsp;Fulfillment&nbsp;")%>
												</TD>
												<TD class="ContentTableHeader" id="tcAddFulfillmentSP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" id="tcAddInventory" align="center" runat="Server"><%= IIF(restrictedPages(tasks.inventory)=true,"<span class=DisableLink>&nbsp;Inventory&nbsp;</span>","&nbsp;Inventory&nbsp;")%>
												</TD>
												<TD class="ContentTableHeader" id="tcAddInventorySP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<%-- end Tee --%>
												<TD class="Content" align="center"><%= IIF(restrictedPages(tasks.discounts)=true,"<span class=DisableLink>&nbsp;Discounts&nbsp;</span>","&nbsp;Discounts&nbsp;")%>
												</TD>
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												<%-- Tee 7/31/2007 product configurator --%>
												<TD class="Content" id="tcAddMarketing" align="center" runat="server"><%= IIF(restrictedPages(tasks.marketing)=true,"<span class=DisableLink>&nbsp;Marketing&nbsp;</span>","&nbsp;Marketing&nbsp;")%>
												</TD>
												<TD class="ContentTableHeader" id="tcAddBundleSP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" id="tcAddBundleComponents" align="center" colSpan="3" runat="server">
													<%= IIF(restrictedPages(tasks.BundleComponents)=true,"<span class=DisableLink>&nbsp;Bundle&nbsp;Components&nbsp;</span>","&nbsp;Bundle&nbsp;Components&nbsp;")%>
												</TD>
												<TD class="ContentTableHeader" id="tcAddCustDefinedSP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" id="tcAddCustDefinedRules" align="center" runat="server">
													<%= IIF(restrictedPages(tasks.CustomerDefinedRules)=true,"<span class=DisableLink>&nbsp;&nbsp;Customer Defined Rules&nbsp;&nbsp;</span>","&nbsp;Customer Defined Rules&nbsp;")%>
												</TD>
												<%-- end Tee --%>
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											</TR>
											<TR id="Edit" runat="server">
												<TD class="ContentTableHeader" align="left" width="100%" colSpan="25">&nbsp;
													<asp:label id="lblPDName" runat="server" CssClass="ContentTableHeader">Product Name</asp:label></TD>
											</TR>
											<TR id="Edit2" runat="server">
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" align="center"><A class="content" href="ProductGeneral.aspx"><B>&nbsp;General&nbsp;</B></A>
												</TD>
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												<TD align="center"><%= IIF(restrictedPages(tasks.productdetails)=true,"<span class=DisableLink>&nbsp;Details&nbsp;</span>","<a class=content href='productdetails.aspx'>&nbsp;Details&nbsp;</a>")%>
												</TD>
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" align="center"><%= IIF(restrictedPages(tasks.productcategories)=true,"<span class=DisableLink>&nbsp;Categories&nbsp;</span>","<a class=content href='ProductCategories.aspx'>&nbsp;Categories&nbsp;</a>")%>
												</TD>
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												<%-- Tee 8/17/2007 product configurator --%>
												<TD class="Content" id="tcEditAttributes" align="center" runat="Server"><%= IIF(restrictedPages(tasks.productattributes)=true,"<span class=DisableLink>&nbsp;Attributes&nbsp;</span>","<a class=content href='attributesmng.aspx'>&nbsp;Attributes&nbsp;</a>")%>
												</TD>
												<TD class="ContentTableHeader" id="tcEditAttributesSP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" align="center" id="tcEditImageControl" runat="server"><%= IIF(restrictedPages(tasks.ProductImages)=true,"<span class=DisableLink>&nbsp;Image Control&nbsp;</span>","<a class=content href='ProductImages.aspx'>&nbsp;Image Control&nbsp;</a>")%></TD>
												<TD class="ContentTableHeader" width="1" id="tcEditImageControlSP" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" id="tcEditFulfillment" align="center" runat="server"><%= IIF(restrictedPages(tasks.fulfillment)=true,"<span class=DisableLink>&nbsp;Fulfillment&nbsp;</span>","<a class=content href='productfulfillment.aspx'>&nbsp;Fulfillment&nbsp;</a>")%>
												</TD>
												<TD class="ContentTableHeader" id="tcEditFulfillmentSP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" id="tcEditInventory" align="center" runat="Server"><%= IIF(restrictedPages(tasks.inventory)=true,"<span class=DisableLink>&nbsp;Inventory&nbsp;</span>","<a class=content href='inventorymng.aspx'>&nbsp;Inventory&nbsp;</a>")%>
												</TD>
												<TD class="ContentTableHeader" id="tcEditInventorySP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<%-- end Tee --%>
												<TD class="Content" align="center"><%= IIF(restrictedPages(tasks.discounts)=true,"<span class=DisableLink>&nbsp;Discounts&nbsp;</span>","<a class=content href='productdiscounts.aspx'>&nbsp;Discounts&nbsp;</a>")%>
												</TD>
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
												<%-- Tee 7/31/2007 product configurator --%>
												<TD class="Content" id="tcEditMarketing" align="center" runat="server">
													<%= IIF(restrictedPages(tasks.marketing)=true,"<span class=DisableLink>&nbsp;Marketing&nbsp;</span>","<a class=content href='productmarketing.aspx'>&nbsp;Marketing&nbsp;</a>")%>
												</TD>
												<TD class="ContentTableHeader" id="tcEditMerchantSP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" id="tcEditMerchantBundleComponents" align="center" runat="server">
													<%= IIF(restrictedPages(tasks.BundleComponents)=true,"<span class=DisableLink>&nbsp;Bundle&nbsp;Components&nbsp;</span>","<a class=content href='BundleComponents.aspx?ProdType=2'>&nbsp;Bundle&nbsp;Components&nbsp;</a>")%>
												</TD>
												<TD class="ContentTableHeader" id="tcEditCustBundleSP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" id="tcEditCustBundleComponents" align="center" runat="server">
													<%= IIF(restrictedPages(tasks.BundleComponents)=true,"<span class=DisableLink>&nbsp;Bundle&nbsp;Components&nbsp;</span>","<a class=content href='BundleComponents.aspx?ProdType=4'>&nbsp;Bundle&nbsp;Components&nbsp;</a>")%>
												</TD>
												<TD class="ContentTableHeader" id="tcEditCustDefinedSP" width="1" runat="server"><IMG src="images/clear.gif" width="1"></TD>
												<TD class="Content" id="tcEditCustDefinedRules" align="center" runat="server">
													<%= IIF(restrictedPages(tasks.CustomerDefinedRules)=true,"<span class=DisableLink>&nbsp;&nbsp;Customer Defined Rules&nbsp;&nbsp;</span>","<a class=content href='CustomerDefinedRules.aspx'>&nbsp;Customer Defined Rules&nbsp;</a>")%>
												</TD>
												<%-- end Tee --%>
												<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
											</TR>
											<TR>
												<TD class="ContentTableHeader" colSpan="25" height="1"><IMG height="1" src="images/clear.gif"></TD>
											</TR>
											<TR>
												<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
												<TD class="content" vAlign="top" align="center" width="100%" colSpan="23">
													<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
														<tr>
															<td class="content" align="center"><uc1:productgeneralcontrol id="ProductGeneralControl1" runat="server"></uc1:productgeneralcontrol></td>
														</tr>
													</table>
												</TD>
												<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											</TR>
											<tr>
												<td class="ContentTable" colSpan="25" height="1"><IMG height="1" src="images/clear.gif"></td>
											</tr>
										</TBODY></table>
								</TD>
							</TR>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

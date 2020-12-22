<%@ import namespace="storefront.systembase"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BundleComponents.aspx.vb" Inherits="StoreFront.StoreFront.BundleComponents"%>
<%@ Register TagPrefix="uc1" TagName="BundleTemplateControl" Src="Controls/BundleTemplateControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="ProductBundleControl" Src="Controls/ProductBundleControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.1

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
			//Tee 8/8/2007 Product configurator
			function Validation(){
				var e = document.getElementsByTagName("INPUT");
				for (i=0; i<e.length; i++){
					if (e[i].id.indexOf("quantity1") != -1){
						if (e[i].value == ""){
							alert("Bundle quantity cannot be blank.");
							return false;
						}
						if (!IsNumeric(e[i].value)){
							alert("Please provide a valid numerical value for bundle quantity.");
							return false;
						}
					}
					if (e[i].id.indexOf("txtDisplayOrder") != -1){
						if (e[i].value == ""){
							alert("Bundle display order cannot be blank.");
							return false;
						}
						if (!IsNumeric(e[i].value)){
							alert("Please provide a valid numerical value for bundle display order.");
							return false;
						}
					}
				}
				return true;
			}
			function IsNumeric(sText){
   				var ValidChars = "0123456789.";
   				var IsNumber=true;
   				var Char;
   				for (j = 0; j < sText.length && IsNumber == true; j++){ 
      				Char = sText.charAt(j); 
      				if (ValidChars.indexOf(Char) == -1){
         				IsNumber = false;
         			}
      			}
   				return IsNumber; 
   			}
   			function ValidateAllSteps(){
   				var prodCountId;
   				var selectableId;
   				var displayOrdId;
   				var stepNameId;
   				var o = document.getElementsByTagName("INPUT");
   				for (k=0; k<o.length; k++){
   					if (o[k].id.indexOf("txtSelectable") != -1){
   						selectableId = o[k].name;
   						prodCountId = o[k].name.replace("txtSelectable", "hidProdCount");
   						displayOrdId = o[k].name.replace("txtSelectable", "txtDisplayOrder");
   						stepNameId  =o[k].name.replace("txtSelectable", "txtStep");
   						if (!ValidateStepDetails(prodCountId, selectableId, displayOrdId, stepNameId)){
   							return false;
   						}
   					}
   				}
   				return true;
   			}
			function ValidateStepDetails(prodCountId, selectableId, displayOrdId, stepNameId, stepId){
				var modifiedStep  = document.getElementById("BundleTemplateControl1_hidModified").value;
				if (modifiedStep != ""){
					modifiedStep = modifiedStep.split("::")[0];
					if (modifiedStep == stepId){
						skipPrompt = true;
					}
				}
				var stepNameTxt = window.document.Form2.elements[stepNameId].value;
				var selectableQty = window.document.Form2.elements[selectableId].value;
				var displayOrd = window.document.Form2.elements[displayOrdId].value;
				var prodCount = window.document.Form2.elements[prodCountId].value;
				if (stepNameTxt == "" || trim(stepNameTxt) == ""){
					alert("Step name cannot be blank to save step.");
					return false;
				}
				if (!IsNumeric(selectableQty) || selectableQty == "" || trim(selectableQty) == ""){
					alert("Please provide valid numerical value for 'Total Selectable' field in " + stepNameTxt + ".");
					return false;
				}
				if (parseInt(selectableQty) > parseInt(prodCount)){
					alert("You only have " + prodCount + " component(s) in step '" + stepNameTxt + "',\n" +
					"please lower the Total Selectable to < or equals to " + prodCount + ".");
					return false;
				}
				if (!IsNumeric(displayOrd) || displayOrd == "" || trim(displayOrd) == ""){
					alert("Please provide valid numerical value for 'Display Order' field in " + stepNameTxt + ".");
					return false;
				}
				var commonId = selectableId.replace("txtSelectable", '');
				var e = document.getElementsByTagName("INPUT");
				var bFound = false;
				for (i=0; i<e.length; i++){
					if (e[i].name.indexOf(commonId) != -1){
						if (e[i].id.indexOf("quantity") != -1){
							bFound = true;
							if (e[i].value == "" || trim(e[i].value) == ""){
								alert("Bundle quantity cannot be blank in " + stepNameTxt + ".");
								return false;
							}
							if (!IsNumeric(e[i].value)){
								alert("Please provide a valid numerical value for bundle quantity in " + stepNameTxt + ".");
								return false;
							}
						}
						if (e[i].id.indexOf("tbDisplayOrder") != -1){
							if (e[i].value == "" || trim(e[i].value) == ""){
								alert("Display order cannot be blank in " + stepNameTxt + ".");
								return false;
							}
							if (!IsNumeric(e[i].value)){
								alert("Please provide a valid numerical value for display order in " + stepNameTxt + ".");
								return false;
							}
						}
					}
				}
				if (!bFound){
					return confirm("Step '" + stepNameTxt + "' does not have any bundle component within,\n are you sure you want to save it?");
				}
				if (parseInt(selectableQty) == 0){
					return confirm("You have 0 'Total Selectable' field in " + stepNameTxt + ".\n" +
					"This step will not be visible to the customer,\nclick 'OK' to confirm or 'Cancel' to exit without saving.");
				}
				return true;
			}
			function ValidateNewStep(newDisplayOrdId, newStepId){
				var newStepText = window.document.Form2.elements[newStepId];
				var newDisplayOrd = window.document.Form2.elements[newDisplayOrdId];
				if (newStepText.value == "" || trim(newStepText.value) == ""){
					alert("Step name cannot be blank for new step added.");
					return false;
				}
				if (newDisplayOrd.value == "" || trim(newDisplayOrd.value) == ""){
					alert("Display order cannot be blank for new step added.");
					return false;
				}
				if (!IsNumeric(newDisplayOrd.value)){
					alert("Please provide a valid numerical value for display order.");
					return false;
				}
				return true;
			}
			function trim(sText){
				return sText.replace(/^\s+|\s+$/g, '');
			}
			var skipPrompt = false;
			function chkModified(){
				var e = document.getElementById("BundleTemplateControl1_hidModified");
				if (e.value != "" && !skipPrompt){
					skipPrompt = true;
					setTimeout("skipPrompt = false;", 100);
					return "Bundle component(s) in step '" + e.value.split("::")[1] + 
					"' have changed.\nPlease save your changes or they will be discarded.";
				}
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
								<td class="content" vAlign="top" align="center" width="100%">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="25" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" align="left" colSpan="25">&nbsp;<asp:label id="lblPDName" CssClass="ContentTableHeader" Runat="server">&nbsp;Product Name</asp:label></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">
												<%= IIF(restrictedPages(tasks.productgeneral)=true,"<span class=DisableLink>&nbsp;General&nbsp;</span>","<a class=content href='ProductGeneral.aspx'>&nbsp;General&nbsp;</a>")%>
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">
												<%= IIF(restrictedPages(tasks.productdetails)=true,"<span class=DisableLink>&nbsp;Details&nbsp;</span>","<a class=content href='Productdetails.aspx'>&nbsp;Details&nbsp;</a>")%>
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<td class="Content" align="center">
												<%= IIF(restrictedPages(tasks.productcategories)=true,"<span class=DisableLink>&nbsp;Categories&nbsp;</span>","<a class=content href='ProductCategories.aspx'>&nbsp;Categories&nbsp;</a>")%>
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
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
												<%= IIF(restrictedPages(tasks.discounts)=true,"<span class=DisableLink>&nbsp;Discounts&nbsp;</span>","<a class=content href='productdiscounts.aspx'>&nbsp;Discounts&nbsp;</a>")%>
											</td>
											<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
											<%-- Tee 7/31/2007 product configurator --%>
											<TD class="Content" id="tcEditMarketing" runat="server" align="center">
												<%= IIF(restrictedPages(tasks.marketing)=true,"<span class=DisableLink>&nbsp;Marketing&nbsp;</span>","<a class=content href='productmarketing.aspx'>&nbsp;Marketing&nbsp;</a>")%>
											</TD>
											<TD class="ContentTableHeader" id="tcEditMerchantSP" runat="server" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<TD class="Content" id="tcEditMerchantBundleComponents" align="center" runat="server">
												<%= IIF(restrictedPages(tasks.BundleComponents)=true,"<span class=DisableLink>&nbsp;Bundle&nbsp;Components&nbsp;</span>","<a class=content href='BundleComponents.aspx?ProdType=2'><b>&nbsp;Bundle&nbsp;Components&nbsp;</b></a>")%>
											</TD>
											<TD class="ContentTableHeader" id="tcEditCustBundleSP" runat="server" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<TD class="Content" id="tcEditCustBundleComponents" align="center" runat="server">
												<%= IIF(restrictedPages(tasks.BundleComponents)=true,"<span class=DisableLink>&nbsp;Bundle&nbsp;Components&nbsp;</span>","<a class=content href='BundleComponents.aspx?ProdType=4'><b>&nbsp;Bundle&nbsp;Components&nbsp;</b></a>")%>
											</TD>
											<TD class="ContentTableHeader" id="tcEditCustDefinedSP" runat="server" width="1"><IMG src="images/clear.gif" width="1"></TD>
											<TD class="Content" id="tcEditCustDefinedRules" align="center" runat="server"><%= IIF(restrictedPages(tasks.CustomerDefinedRules)=true,"<span class=DisableLink>&nbsp;&nbsp;Customer Defined Rules&nbsp;&nbsp;</span>","<a class=content href='CustomerDefinedRules.aspx'>&nbsp;Customer Defined Rules&nbsp;</a>")%></TD>
											<%-- end Tee --%>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="25" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="content" vAlign="top" align="center" colSpan="23">
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="center">
															<p id="ErrorAlignment" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></p>
															<p><uc1:productbundlecontrol id="ProductBundleControl1" runat="server" visible="False"></uc1:productbundlecontrol><uc1:bundletemplatecontrol id="BundleTemplateControl1" runat="server" visible="False"></uc1:bundletemplatecontrol></p>
														</td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="25" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
		</TD></TR></TABLE></TD></TR></TABLE>
	</body>
</HTML>

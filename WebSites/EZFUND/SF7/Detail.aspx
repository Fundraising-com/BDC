<%@ Register TagPrefix="uc1" TagName="ProductDetail2" Src="Controls/ProductDetail2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProductDetail1" Src="Controls/ProductDetail1.ascx" %>
<%@ Page Language="vb"  AutoEventWireup="false" Codebehind="Detail.aspx.vb" Inherits="StoreFront.StoreFront.Detail1" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BreadCrumbs" Src="Controls/BreadCrumbs.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RelatedProductControl" Src="Controls/RelatedProductControl.ascx" %>
<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Detail1 - <% WriteProductName() %></title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.1.0

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
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<!-- Tee 11/16/2007 css positioning -->
		<style type="text/css">
		    
		</style>
		<!-- end Tee -->
		<script language="JavaScript" src="<%=VirtualPath%>general.js"></script>
		<script language="javascript">var AddProductPopup = "<%=VirtualPath%>AddProductPopup.aspx";</script>
		<script language="javascript">
<!--
function DetailValidation(txtid)
  {
               
                try
                 {
               
                
                 window.document.Form2.elements[txtid].title="Product Quantity";
                 window.document.Form2.elements[txtid].number=true;
                
                 }
                 catch(e)
                 {
               // alert(e)
                 }
              
				return ValidateForm(window.document.Form2)
    }
function CloseView()
{
	var sFeatures, h, w, myThanks, i
	h = window.screen.availHeight 
	w = window.screen.availWidth 
	sFeatures = "height=" + h*.50 + ",width=" + h*.50 + ",screenY=" + (h*.30) + ",screenX=" + (w*.33) + ",top=" + (h*.30) + ",left=" + (w*.33) + ",resizable,scrollbars=yes"
	myThanks = window.open("<%=VirtualPath%>CloseUp.aspx?uid=" + window.document.Form2.SwatchID.value,"",sFeatures)
}
//-->
//browser check
var ie = (document.all) ? true : false
var ns = (document.layers) ? true : false



function chgImg(imgField,newImg,UID) {
			if (ns) {eval('window.document.Form2.images[imgField].src = '+newImg+'.src')}
			else {window.document.Form2[imgField].src = eval(newImg+'.src')}
			window.document.Form2.SwatchID.value=UID
}

function preload(imgObj,imgSrc) {
	if (document.images) {
		eval(imgObj+' = new Image()'); 
		eval(imgObj+'.src = "'+imgSrc+'"');
	}
}
            //Tee 10/15/2007 bug 312 fix
            function DisplayStatus(btnId){
                var trIn;
                var ctrlIn;
                if (btnId.indexOf("btnVolumePricing") != -1){
                    trIn = btnId.replace("btnVolumePricing", "trVolumePricing3");
                    ctrlIn = btnId.replace("btnVolumePricing", "Volumepricing1_lblVolumePricing");
                }else{
                    trIn = btnId.replace("StockInfo", "trStockStatus");
                    ctrlIn = btnId.replace("StockInfo", "CInventoryControl1_lblStockInfo");
                }
                var tr = document.getElementById(trIn);
                var ctrl = document.getElementById(ctrlIn);
                if (ctrl != null){
                    if (tr.style.display == "none"){
                        tr.style.display = "";
                    }else{
                        tr.style.display = "none";
                    }
                    return false;
                }
                else{
                    return true;
                }
            }
            
            function Resize(img, imgPath, type){
                var imgSize;
                if (type == 2){
                    imgSize = 150;
                }else if (type == 4){
                    imgSize = 120;
                }
                if (img.src.indexOf("Busy.gif") == -1){
                    if (img.height > imgSize){
                        img.width = parseInt(img.width * imgSize / img.height);
                        img.height = imgSize;
                    }
                    if (img.width > imgSize){
                        img.height = parseInt(img.height * imgSize / img.width);
                        img.width = imgSize;
                    }
                    return;
                }
                var newImg = new Image();
                var hdnImg = document.getElementById("hdnImg");
                newImg.src = imgPath;
                if (newImg.height > imgSize){
                    img.width = parseInt(newImg.width * imgSize / newImg.height);
                    img.height = imgSize;
                }
                if (newImg.width > imgSize){
                    img.height = parseInt(newImg.height * imgSize / newImg.width);
                    img.width = imgSize;
                }
                img.src = newImg.src;
            }
            //end Tee
		</script>
		<% Me.PageHeader %>
	</HEAD>
	<body id="BodyTag" runat="server" class="generalpage">
		<form id="Form2" method="post" runat="server">
		    <input id="myhiddenfield" name="myhiddenfield" type="hidden" runat="server"> <input type="hidden" id="SwatchID" name="SwatchID" value="">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1"
							runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start -->
									<uc1:TopBanner id="TopBanner1" runat="server"></uc1:TopBanner>
									<!-- Top Banner End -->
								</td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start -->
									<uc1:TopSubBanner id="TopSubBanner1" runat="server"></uc1:TopSubBanner>
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
											<td class="Content">
												<p class="Content"><uc1:BreadCrumbs id="BreadCrumbs1" runat="server" /></p>
												<P id="ErrorAlignment" runat="server" align="center">
													<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
												<P id="MessageAlignment" runat="server" align="center">
													<asp:Label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:Label>
												</P>
												<uc1:ProductDetail1 id="ProductDetail11" runat="server"></uc1:ProductDetail1>
												<uc1:ProductDetail2 id="ProductDetail21" runat="server" Visible="False"></uc1:ProductDetail2>
												<%-- Tee 10/22/2007 related product ajax implementation--%>
												<ajax:AjaxPanel ID="apSampleTest" runat="server">
												    <uc1:RelatedProductControl id="RelatedProdControl" runat="Server" Visible='<%# DisplayRecommendedItems %>'></uc1:RelatedProductControl>
												</ajax:AjaxPanel>
												<%-- end Tee --%>
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

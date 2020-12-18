<%@ Register TagPrefix="uc1" TagName="SearchTemplate3" Src="Controls/SearchTemplate3.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchTemplate2" Src="Controls/SearchTemplate2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchTemplate1" Src="Controls/SearchTemplate1.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SpeedSearch" Src="Controls/SpeedSearch.ascx" %>
<%@ Page Language="vb"  AutoEventWireup="false" Codebehind="SearchResult.aspx.vb" Inherits="StoreFront.StoreFront.SearchResult"%>
<%@ Register TagPrefix="uc1" TagName="BreadCrumbs" Src="Controls/BreadCrumbs.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Product Catalog - <% WriteCategoryName() %></title>
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
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="<%=VirtualPath%>general.js"></script>
		<script language="javascript">var AddProductPopup = "<%=VirtualPath%>AddProductPopup.aspx";</script>
		<% Me.PageHeader %>
		<script language="javascript">
<!--
function SearchValidation(txtid)
  {
               
                try
                 {
               
                
                 window.document.Form2.elements[txtid].title="Product Quantity";
                 window.document.Form2.elements[txtid].number=true;
                
                 }
                 catch(e)
                 {
              //  alert(e)
                 }
              
				return ValidateForm(window.document.Form2)
    }
    //Tee 10/15/2007 bug 312 fix
        function DisplayStatus(btnId){
            var trIn;
            if (btnId.indexOf("StockInfo") == -1){
                trIn = btnId.replace("StockInfo", "trStockStatus");
            }else{
                trIn = btnId.replace("StockInfo", "trStockStatus");
            }
            var tr = document.getElementById(trIn);
            if (tr != null){
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
        //end Tee

//-->
		</script>
	</HEAD>
	<body id="BodyTag" runat="server" class="generalpage">
		<form id="Form2" method="post" runat="server">
			<input id="myhiddenfield" name="myhiddenfield" type="hidden" runat="server" value="null">
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
											<td>
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content">
												<p class="Content"><uc1:BreadCrumbs id=BreadCrumbs1 runat="server" /></p>
												<P id="ErrorAlignment" runat="server" align="center">
													<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
												<P id="MessageAlignment" runat="server" align="center">
													<asp:Label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:Label>
												</P>
												<uc1:SearchTemplate1 id="SearchTemplate11" runat="server" Visible="False"></uc1:SearchTemplate1>
												<uc1:SearchTemplate2 id="SearchTemplate12" runat="server" Visible="False"></uc1:SearchTemplate2>
												<uc1:SearchTemplate3 id="SearchTemplate13" runat="server" Visible="False"></uc1:SearchTemplate3>
												<uc1:SpeedSearch id="SpeedSearch1" runat="server"></uc1:SpeedSearch>
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

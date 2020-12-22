<%@ Page Language="vb" AutoEventWireup="false" Codebehind="storecoupons.aspx.vb" Inherits="StoreFront.StoreFront.storecoupons"%>
<%@ Register TagPrefix="uc1" TagName="standardsearchcontrol2" Src="Controls/standardsearchcontrol2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="editcoupon" Src="Controls/editcoupon.ascx" %>
<%@ Register TagPrefix="uc1" TagName="addcoupon" Src="Controls/addcoupon.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StandardSearchControl" Src="Controls/StandardSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
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
		<% Me.PageHeader %>
			<script language="javascript">
			<!--
			function SetValidationAdd()
			{
				//Tee 2/6/2008 bug 1017 fix
				var reg = new RegExp("[']");
				if (window.document.Form2.elements["Addcoupon1:txtDiscription"].value.match(reg)){
				    alert("Special characters found in Coupon Description: " + window.document.Form2.elements["Addcoupon1:txtDiscription"].value + "\n Removed from description.");
				    window.document.Form2.elements["Addcoupon1:txtDiscription"].value = window.document.Form2.elements["Addcoupon1:txtDiscription"].value.replace(/'+/, "");
				}
				if (window.document.Form2.elements["Addcoupon1:txtCode"].value.match(reg)){
				    alert("Special characters found in Coupon Code: " + window.document.Form2.elements["Addcoupon1:txtCode"].value + "\n Removed from code.");
				    window.document.Form2.elements["Addcoupon1:txtCode"].value = window.document.Form2.elements["Addcoupon1:txtCode"].value.replace(/'+/, "");
				}
				//end Tee
				ResetForm(window.document.Form2);
				window.document.Form2.elements["Addcoupon1:txtDiscription"].required=true
				window.document.Form2.elements["Addcoupon1:txtDiscription"].title="Coupon Description"
				window.document.Form2.elements["Addcoupon1:txtCode"].required=true
				window.document.Form2.elements["Addcoupon1:txtCode"].title="Coupon Code"
				
				
				window.document.Form2.elements["Addcoupon1:txtAmount"].required=true
				window.document.Form2.elements["Addcoupon1:txtAmount"].number=true
				window.document.Form2.elements["Addcoupon1:txtAmount"].title="Amount"
				
				window.document.Form2.elements["Addcoupon1:minAmt"].required=true
				window.document.Form2.elements["Addcoupon1:minAmt"].number=true
				window.document.Form2.elements["Addcoupon1:minAmt"].title="For Orders Equal to Or Above"
				
				if (window.document.Form2.elements["Addcoupon1:DropDownList1"].options[window.document.Form2.elements["Addcoupon1:DropDownList1"].options.selectedIndex].value != "Never")
				{window.document.Form2.elements["Addcoupon1:txtDate"].required=true
				}
				else
				{window.document.Form2.elements["Addcoupon1:txtDate"].required=false
				}
				window.document.Form2.elements["Addcoupon1:txtDate"].date=true
				window.document.Form2.elements["Addcoupon1:txtDate"].title="Expiration Date"

				
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationEdit()
			{
				//Tee 2/6/2008 bug 1017 fix
				var reg = new RegExp("[']");
				if (window.document.Form2.elements["Editcoupon1:txtDiscription"].value.match(reg)){
				    alert("Special characters found in Coupon Description: " + window.document.Form2.elements["Editcoupon1:txtDiscription"].value + "\nRemoved from description.");
				    window.document.Form2.elements["Editcoupon1:txtDiscription"].value = window.document.Form2.elements["Editcoupon1:txtDiscription"].value.replace(/'/g, "");
				}
				if (window.document.Form2.elements["Editcoupon1:txtCode"].value.match(reg)){
				    alert("Special characters found in Coupon Code: " + window.document.Form2.elements["Editcoupon1:txtCode"].value + "\nRemoved from code.");
				    window.document.Form2.elements["Editcoupon1:txtCode"].value = window.document.Form2.elements["Editcoupon1:txtCode"].value.replace(/'/g, "");
				}
				//end Tee
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["Editcoupon1:txtDiscription"].required=true
				window.document.Form2.elements["Editcoupon1:txtDiscription"].title="Coupon Description"
				window.document.Form2.elements["Editcoupon1:txtCode"].required=true
				window.document.Form2.elements["Editcoupon1:txtCode"].title="Coupon Code"
				
				window.document.Form2.elements["Editcoupon1:txtAmount"].required=true
				window.document.Form2.elements["Editcoupon1:txtAmount"].number=true
				window.document.Form2.elements["Editcoupon1:txtAmount"].title="Amount"
				
				window.document.Form2.elements["Editcoupon1:minAmt"].required=true
				window.document.Form2.elements["Editcoupon1:minAmt"].number=true
				window.document.Form2.elements["Editcoupon1:minAmt"].title="For Orders Equal to Or Above"
				
				
				if (window.document.Form2.elements["Editcoupon1:DropDownList1"].options[window.document.Form2.elements["Editcoupon1:DropDownList1"].options.selectedIndex].value != "Never")
				{window.document.Form2.elements["Editcoupon1:txtDate"].required=true
				}
				else
				{window.document.Form2.elements["Editcoupon1:txtDate"].required=false
				}
				window.document.Form2.elements["Editcoupon1:txtDate"].date=true
				window.document.Form2.elements["Editcoupon1:txtDate"].title="Expiration Date"

				
				return ValidateForm(window.document.Form2)
			}


			//-->
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server" MS_POSITIONING="FlowLayout">
		<form id="Form2" method="post" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<tr>
					<td class="TopBanner" colSpan="3">
						<uc1:TopBanner id="TopBanner2" runat="server"></uc1:TopBanner>
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<uc1:TopSubBanner id="TopSubBanner2" runat="server" Title="Coupons"></uc1:TopSubBanner>
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
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs70/coupons_ov.asp ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<tr>
								<td class="content" align="middle">
									<p id="ErrorAlignment" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></p>
								</td>
							</tr>
							<tr>
								<td class="Content">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="Content" colSpan="5"><uc1:admintabcontrol id="AdminTabControl1" runat="server"></uc1:admintabcontrol></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
											<td class="Content" width="100%" colSpan="3" height="1">&nbsp;</td>
											<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
											<td class="Content">&nbsp;</td>
											<td class="Content" width="100%">
												<P><uc1:standardsearchcontrol id="StandardSearchControl1" runat="server"></uc1:standardsearchcontrol><uc1:editcoupon id="Editcoupon1" runat="server"></uc1:editcoupon><uc1:addcoupon id="Addcoupon1" runat="server"></uc1:addcoupon>
													<uc1:standardsearchcontrol2 id="Standardsearchcontrol21" runat="server"></uc1:standardsearchcontrol2></P>
												<P>
													<asp:LinkButton ID="btnBackSelect" Runat="server" Visible="False">
														<asp:Image BorderWidth="0" ID="imgBackSelect" runat="server" ImageUrl="images/back.jpg" AlternateText="Back" Visible="False"></asp:Image>
													</asp:LinkButton>
													<asp:LinkButton ID="btnSaveSelect" Runat="server" Visible="False">
														<asp:Image BorderWidth="0" ID="imgSaveSelect" runat="server" ImageUrl="images/save.jpg" AlternateText="Save" Visible="False"></asp:Image>
													</asp:LinkButton>
													<asp:LinkButton ID="btnEditBackSelect" Runat="server" Visible="False">
														<asp:Image BorderWidth="0" ID="imgEditBackSelect" runat="server" ImageUrl="images/back.jpg" AlternateText="Back" Visible="False"></asp:Image>
													</asp:LinkButton>
													<asp:LinkButton ID="btnEditSaveSelect" Runat="server" Visible="False">
														<asp:Image BorderWidth="0" ID="imgEditSaveSelect" runat="server" ImageUrl="images/save.jpg" AlternateText="Save" Visible="False"></asp:Image>
													</asp:LinkButton></P>
												<table class="ContentTable" id="tblGeneralInfo" cellSpacing="0" cellPadding="0" width="100%" runat="server">
													<tr id="trEditRow">
														<td class="ContentTableHeader" align="left" colSpan="4">&nbsp;General<span class="ContentTableHeader" id="CategoryControl1_lblCustomerHeader"></span></td>
													</tr>
													<tr>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td class="Content" colSpan="2" height="1">&nbsp;</td>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
													</tr>
													<tr align="left">
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td class="Content" vAlign="center" align="left">&nbsp;&nbsp;&nbsp;Apply&nbsp;coupons&nbsp;on&nbsp;sale&nbsp;items:&nbsp;<asp:checkbox id="chkSaleItems" Runat="server"></asp:checkbox></td>
														<td class="Content" align="left">&nbsp;</td>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
													</tr>
													<tr>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td class="Content" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Allow&nbsp;multiple&nbsp;coupons:&nbsp;<asp:checkbox id="chkMultipleCoupons" Runat="server"></asp:checkbox></td>
														<td class="Content" align="left">&nbsp;</td>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
													</tr>
													<tr>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td class="Content" colSpan="2" height="10">&nbsp;</td>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
													</tr>
													<tr>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td class="Content" align="middle" colSpan="2">
															<asp:LinkButton ID="btnSaveGeneral" Runat="server" onclick="btnSaveGeneral_Click">
																<asp:Image BorderWidth="0" ID="imgSaveGeneral" runat="server" ImageUrl="images/save.jpg" AlternateText="Save"></asp:Image>
															</asp:LinkButton>
														</td>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
													</tr>
													<tr>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
														<td class="Content" colSpan="2" height="10">&nbsp;</td>
														<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
													</tr>
													<tr>
														<td class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></td>
													</tr>
												</table>
											</td>
											<td class="Content">&nbsp;</td>
											<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
											<td class="Content" width="100%" colSpan="3">&nbsp;</td>
											<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
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

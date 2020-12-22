<%@ Register TagPrefix="uc1" TagName="freeshipping" Src="Controls/freeshipping.ascx" %>
<%@ Register TagPrefix="uc1" TagName="adddiscount" Src="Controls/adddiscount.ascx" %>
<%@ Register TagPrefix="uc1" TagName="editdiscount" Src="Controls/editdiscount.ascx" %>
<%@ Register TagPrefix="uc1" TagName="standardsearchcontrol2" Src="Controls/standardsearchcontrol2.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="storediscounts.aspx.vb" Inherits="StoreFront.StoreFront.storediscounts"%>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="../CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="../CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="../CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="../CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AdminTabControl" Src="Controls/AdminTabControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StandardSearchControl" Src="Controls/StandardSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Merchant Tools</title>
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
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../General.js"></script>
		<% Me.PageHeader %>
			<script language="javascript">
			<!--
			function SetValidationAdd()
			{
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["Adddiscount1:txtDiscription"].required=true
				window.document.Form2.elements["Adddiscount1:txtDiscription"].title="Discount Description"
				
				window.document.Form2.elements["Adddiscount1:txtAmount"].required=true
				window.document.Form2.elements["Adddiscount1:txtAmount"].number=true
				window.document.Form2.elements["Adddiscount1:txtAmount"].title="Amount"
				
				window.document.Form2.elements["Adddiscount1:minAmt"].required=true
				window.document.Form2.elements["Adddiscount1:minAmt"].number=true
				window.document.Form2.elements["Adddiscount1:minAmt"].title="For Orders Equal to or Above"
				
				if (window.document.Form2.elements["Adddiscount1:DropDownList1"].options[window.document.Form2.elements["Adddiscount1:DropDownList1"].options.selectedIndex].value != "Never")
				{window.document.Form2.elements["Adddiscount1:txtDate"].required=true
				}
				else
				{window.document.Form2.elements["Adddiscount1:txtDate"].required=false
				}
				window.document.Form2.elements["Adddiscount1:txtDate"].date=true
				window.document.Form2.elements["Adddiscount1:txtDate"].title="Expiration Date"

				
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationEdit()
			{
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["Editdiscount1:txtDiscription"].required=true
				window.document.Form2.elements["Editdiscount1:txtDiscription"].title="Discount Description"
				
				window.document.Form2.elements["Editdiscount1:txtAmount"].required=true
				window.document.Form2.elements["Editdiscount1:txtAmount"].number=true
				window.document.Form2.elements["Editdiscount1:txtAmount"].title="Amount"
				
				window.document.Form2.elements["Editdiscount1:minAmt"].required=true
				window.document.Form2.elements["Editdiscount1:minAmt"].number=true
				window.document.Form2.elements["Editdiscount1:minAmt"].title="For Orders Equal to or Above"
				
				if (window.document.Form2.elements["Editdiscount1:DropDownList1"].options[window.document.Form2.elements["Editdiscount1:DropDownList1"].options.selectedIndex].value != "Never")
				{window.document.Form2.elements["Editdiscount1:txtDate"].required=true
				}
				else
				{window.document.Form2.elements["Editdiscount1:txtDate"].required=false
				}
				window.document.Form2.elements["Editdiscount1:txtDate"].date=true
				window.document.Form2.elements["Editdiscount1:txtDate"].title="Expiration Date"

				
				return ValidateForm(window.document.Form2)
			}

			function SetValidationFreeShipping()
			{
				ResetForm(window.document.Form2);
				
				window.document.Form2.elements["Freeshipping1:txtAmount"].required=true
				window.document.Form2.elements["Freeshipping1:txtAmount"].number=true
				window.document.Form2.elements["Freeshipping1:txtAmount"].title="Amount"
				
				if (window.document.Form2.elements["Freeshipping1:DropDownList1"].options[window.document.Form2.elements["Freeshipping1:DropDownList1"].options.selectedIndex].value != "Never")
				{window.document.Form2.elements["Freeshipping1:txtDate"].required=true
				}
				else
				{window.document.Form2.elements["Freeshipping1:txtDate"].required=false
				}
				window.document.Form2.elements["Freeshipping1:txtDate"].date=true
				window.document.Form2.elements["Freeshipping1:txtDate"].title="Expiration Date"

				
				return ValidateForm(window.document.Form2)
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
						<!-- Top Banner End -->
					</td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<!-- Top Sub Banner Start --> Store Discounts 
						<!-- Top Sub Banner End -->
					</td>
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
									<a href="javascript: doHelp(' http://support.storefront.net/mtdocs/swdisc_ov.asp ')">
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
								<td class="content" align="middle">
									<p id="ErrorAlignment" runat="server">
										<font color="#ff0000">
											<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
										</font>
									</p>
								</td>
							</tr>
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td class="ContentTable" HEIGHT="1" colspan="5" width="100%"><img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="Content" colspan="5" width="100%"><uc1:AdminTabControl id="AdminTabControl1" runat="server"></uc1:AdminTabControl></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1" colspan="5"><img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
									</table>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td class="ContentTable" HEIGHT="1"><img src="images/clear.gif" HEIGHT="1"></td>
											<td class="Content" HEIGHT="1" colspan="3" width="100%">&nbsp;</td>
											<td class="ContentTable" HEIGHT="1"><img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1">
												<img src="images/clear.gif" HEIGHT="1"></td>
											<td class="Content">&nbsp;</td>
											<td class="Content" width="100%">
												<P>
													<uc1:StandardSearchControl id="StandardSearchControl1" runat="server"></uc1:StandardSearchControl>
													<uc1:editdiscount id="Editdiscount1" runat="server"></uc1:editdiscount>
													<uc1:adddiscount id="Adddiscount1" runat="server"></uc1:adddiscount>
													<uc1:freeshipping id="Freeshipping1" runat="server"></uc1:freeshipping>
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
													</asp:LinkButton>
												</P>
											</td>
											<td class="Content">&nbsp;</td>
											<td class="ContentTable" HEIGHT="1">
												<img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1">
												<img src="images/clear.gif" HEIGHT="1"></td>
											<td class="Content" colspan="3" width="100%">&nbsp;</td>
											<td class="ContentTable" HEIGHT="1">
												<img src="images/clear.gif" HEIGHT="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" HEIGHT="1" colspan="5">
												<img src="images/clear.gif" HEIGHT="1"></td>
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

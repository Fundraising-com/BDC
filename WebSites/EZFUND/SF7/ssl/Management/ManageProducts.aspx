<%@ Register TagPrefix="uc1" TagName="StandardSearchControl" Src="Controls/StandardSearchControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="../CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ManageProducts.aspx.vb" Inherits="StoreFront.StoreFront.ManageProducts"%>
<%@ Register TagPrefix="uc1" TagName="StandardSearchLive" Src="Controls/StandardSearchLive.ascx" %>
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
		function CheckAll(dgChkBoxID, checkVal)
 {
  re = new RegExp(':' + dgChkBoxID + '$')  
  for(i = 0; i < Form2.elements.length; i++)
  {
   elm = document.forms[0].elements[i]
   if (elm.type == 'checkbox')
   {
		if (re.test(elm.name)){
			if (checkVal >0)
				elm.checked = true;
			else{
				elm.checked =false;
				}
		}
   }
  }
 }

 function CheckSelected(dgChkBoxID)
 {
 var checked =false;
  re = new RegExp(':' + dgChkBoxID + '$')  
  for(i = 0; i < Form1.elements.length; i++)
  {
   elm = document.forms[0].elements[i]
   if (elm.type == 'checkbox')
   {
    if (re.test(elm.name)){
		if (elm.checked == true){
			checked = true;
			 break;
         }
     }
   }
  }
  /*if (checked == false){
	alert("Please select at least one product to add to cart.")}
     return checked;*/
 }
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server" MS_POSITIONING="FlowLayout">
		<form id="Form2" method="post" runat="server">
			<input id="txtGroupIDHidden" type="hidden" name="txtGroupIDHidden" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<tr>
					<td class="TopBanner" colSpan="3">
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
								<td class="content">
									<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="lblErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></P>
								</td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<TR>
								<TD class="content" vAlign="top" align="center" colSpan="2">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<TR>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="Content" align="left">
												<table class="Content" cellPadding="5" border="0" width="100%">
													<tr>
														<td class="Content" colspan="2"><uc1:standardsearchlive id="StandardSearchLive1" runat="server"></uc1:standardsearchlive></td>
													</tr>
													<tr>
														<td class="Content" width="30%"><asp:linkbutton id="btnAdd" Runat="server">
																<asp:Image runat="server" AlternateText="Add" BorderWidth="0px" ID="imgAdd" ImageUrl="images/add_new.jpg"></asp:Image>
															</asp:linkbutton>&nbsp;
															<asp:LinkButton id="btnSave" runat="server">
																<asp:Image runat="server" AlternateText="Save" BorderWidth="0px" ID="Image1" ImageUrl="images/save.jpg"></asp:Image>
															</asp:LinkButton></td>
														<td class="Content" width="70%" align="right">
															<asp:linkbutton id="btnSelectAll" runat="server">Select All</asp:linkbutton>&nbsp;&nbsp;
															<asp:linkbutton id="btnDeselectAll" runat="server">Deselect All</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1" colspan="2"><IMG src="images/clear.gif" width="1"></td>
										<tr>
											<td class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
									</table>
								</TD>
							</TR>
						</table>
						<!-- Content End --></td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="attTemplates" Src="Controls/attTemplates.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="attributesTemplate.aspx.vb" Inherits="StoreFront.StoreFront.AttTemplate"%>
<%@ Register TagPrefix="uc1" TagName="StandardSearchControl" Src="Controls/StandardSearchControl.ascx" %>
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
			function SetValidationAddTemplate()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["txtTemplateName"].required=true;
				window.document.Form2.elements["txtTemplateName"].title="Template Name";
			
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationAdd()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["AttTemplates1:txtAttName"].required=true;
				window.document.Form2.elements["AttTemplates1:txtAttName"].title="Attribute Name";
			
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationAttDetail()
			{
				ResetForm(window.document.Form2);
			
				window.document.Form2.elements["AttTemplates1:Attdetailctrl1:txtDetailName"].required=true;
				window.document.Form2.elements["AttTemplates1:Attdetailctrl1:txtDetailName"].title="Option";				
				window.document.Form2.elements["AttTemplates1:Attdetailctrl1:txtPrice"].number=true;
				if (window.document.Form2.elements["AttTemplates1:Attdetailctrl1:txtPrice"].value=="")
					{window.document.Form2.elements["AttTemplates1:Attdetailctrl1:txtPrice"].value="0"}
				window.document.Form2.elements["AttTemplates1:Attdetailctrl1:txtPrice"].title="Price";
				window.document.Form2.elements["AttTemplates1:Attdetailctrl1:txtWeight"].number=true;
				if (window.document.Form2.elements["AttTemplates1:Attdetailctrl1:txtWeight"].value=="")
					{window.document.Form2.elements["AttTemplates1:Attdetailctrl1:txtWeight"].value="0"}
				window.document.Form2.elements["AttTemplates1:Attdetailctrl1:txtWeight"].title="Weight";
			
				return ValidateForm(window.document.Form2)
			}
			
			function SetValidationAttMain()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtAttName"].required=true;
				window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtAttName"].title="Name";				
				
				if (window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtPrice"]!=null)
					{window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtPrice"].number=true;
					if (window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtPrice"].value=="")
						{window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtPrice"].value="0"}
					window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtPrice"].title="Price";
					
					}
				if (window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtWeight"]!=null)
					{window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtWeight"].number=true;
					if (window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtWeight"].value=="")
						{window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtWeight"].value="0"}
					window.document.Form2.elements["AttTemplates1:AttMainctrl1:txtWeight"].title="Weight";
					}
				return ValidateForm(window.document.Form2)
			}
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server">
		<form id="Form2" method="post" encType="multipart/form-data" runat="server">
			<table class="GeneralTable" cellSpacing="0">
				<tr>
					<td class="TopBanner" colSpan="3">
						<!-- Top Banner Start -->
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td class="TopBanner2" width="20%"><IMG src="images/sflogo.jpg"></td>
								<td class="TopBanner">&nbsp;&nbsp;Merchant Tools</td>
							</tr>
						</table>
						<!-- Top Banner End --></td>
				</tr>
				<tr>
					<td class="TopSubBanner" id="TopSubBannerCell" colSpan="3">
						<!-- Top Sub Banner Start --> Attributes 
						<!-- Top Sub Banner End --></td>
				</tr>
				<tr>
					<td class="LeftColumn" id="LeftColumnCell">
						<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
						<!-- Left Column End --></td>
					<td class="Content" vAlign="top">
						<!-- Content Start -->
						<table cellSpacing="3" cellPadding="5" width="100%" border="0">
							<tr>
								<td class="Content" align="right">
									<!-- Help Button --><A href="javascript: doHelp(' http://support.storefront.net/mtdocs/attrtemp_ov.asp ')"><IMG src="images/help.jpg" border="0"></A>
									<!-- End Help Button --></td>
							</tr>
							<tr>
								<td class="Content">
									<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<!-- Instruction End --></td>
							</tr>
							<TR>
								<TD class="content" vAlign="top" align="middle">
									<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="content" vAlign="top" align="middle" colSpan="3">
												<p id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></p>
												<table class="content" cellSpacing="3" cellPadding="5" width="100%" border="0">
													<tr>
														<td class="content" align="middle">
															<uc1:StandardSearchControl id="StandardSearchControl1" runat="server"></uc1:StandardSearchControl>
															<uc1:attTemplates id="AttTemplates1" runat="server"></uc1:attTemplates></td>
													</tr>
												</table>
											</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="content" align="left" width="100%" nowrap colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTemplateName" Runat="server" CssClass="content" MaxLength="50"></asp:TextBox>
												&nbsp;&nbsp;
												<asp:LinkButton ID="btnAdd" Runat="server">
													<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="images/add.jpg" AlternateText="Add"></asp:Image>
												</asp:LinkButton>
											</td>
											<td class="content" align="right">
												<asp:LinkButton ID="cmdDone" Runat="server">
													<asp:Image BorderWidth="0" ID="imgDone" runat="server" ImageUrl="images/Back.jpg" AlternateText="Done"></asp:Image>
												</asp:LinkButton>
												&nbsp;&nbsp;</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
											<td class="content" colspan="3">&nbsp;</td>
											<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
										</tr>
										<tr>
											<td class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
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

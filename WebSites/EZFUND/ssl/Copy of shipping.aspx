<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Shipping.aspx.vb" Inherits="StoreFront.StoreFront.Shipping"%>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Shipping Information</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.1.0

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
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
		<!--
			function SetValidation()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["Ship_NickName"].required=true;
				window.document.Form2.elements["Ship_NickName"].title="Save As";
				window.document.Form2.elements["Ship_FirstName"].required=true;
				window.document.Form2.elements["Ship_FirstName"].title="First Name";
				window.document.Form2.elements["Ship_LastName"].required=true;
				window.document.Form2.elements["Ship_LastName"].title="Last Name";
				window.document.Form2.elements["Ship_Address1"].required=true;
				window.document.Form2.elements["Ship_Address1"].title="Address";
				window.document.Form2.elements["Ship_City"].required=true;
				window.document.Form2.elements["Ship_City"].title="City";
				window.document.Form2.elements["Ship_Phone"].required=true;
				window.document.Form2.elements["Ship_Phone"].phonenumber=true;
				window.document.Form2.elements["Ship_Phone"].title="Phone";
				window.document.Form2.elements["Ship_Fax"].phonenumber=true;
				window.document.Form2.elements["Ship_Fax"].title="Fax";
				
				if (window.document.Form2.elements["Ship_Country"].value == "US" ||
					window.document.Form2.elements["Ship_Country"].value == "CA")
				{
					window.document.Form2.elements["Ship_Zip"].required=true;
					window.document.Form2.elements["Ship_Zip"].title="Postal Code";
				}
				else
				{
					window.document.Form2.elements["Ship_Zip"].required=false;
				}
				
				
				return ValidateForm(window.document.Form2)
			}
			//-->
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" width="100%" runat="server">
				<tr>
					<td id="PageCell">
						<table class="GeneralTable" id="PageSubTable" cellSpacing="0" width="100%" runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start --><uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
									<!-- Top Banner End --></td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start --><uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
									<!-- Top Sub Banner End --></td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell">
									<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
									<!-- Left Column End --></td>
								<td class="Content" id="ContentCell" vAlign="top">
									<!-- Content Start -->
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="Instructions">
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="content">
												<P id="ErrorAlignment" align="center" runat="server"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></P>
											</td>
										</tr>
										<tr>
											<td class="Content">
												<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<TD class="Headings" colSpan="3">Ship To Information</TD>
													</TR>
													<TR>
														<TD class="Content" colSpan="3">&nbsp;</TD>
													</TR>
													<TR>
														<TD width="10">&nbsp;</TD>
														<TD class="Content" align="center">
															<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
																<TR>
																	<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="ContentTableHeader">&nbsp;</td>
																	<TD class="ContentTableHeader" colSpan="3"><asp:label id="Label1" runat="server" CssClass="ContentTableHeader">Label</asp:label><asp:dropdownlist id="ShipAddressList" runat="server" DataTextField="NickName" DataValueField="ID"
																			AutoPostBack="True"></asp:dropdownlist></TD>
																	<TD class="ContentTableHeader" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<TD class="Content" align="right" colSpan="4">&nbsp;</TD>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">Save As:&nbsp;
																	</TD>
																	<TD class="Content" vAlign="top" noWrap><asp:textbox id="Ship_NickName" runat="server" cssclass="Content" MaxLength="50"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">First Name:&nbsp;</TD>
																	<TD class="Content" vAlign="top" noWrap><asp:textbox id="Ship_FirstName" runat="server" cssclass="Content" MaxLength="100"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">Middle Initial:&nbsp;
																	</TD>
																	<TD class="Content" vAlign="top"><asp:textbox id="Ship_MI" runat="server" cssclass="Content" MaxLength="2" Columns="3"></asp:textbox></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">Last Name:&nbsp;</TD>
																	<TD class="Content" vAlign="top" noWrap><asp:textbox id="Ship_LastName" runat="server" cssclass="Content" MaxLength="100"></asp:textbox><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">Company:&nbsp;</TD>
																	<TD class="Content" vAlign="top"><asp:textbox id="Ship_Company" runat="server" cssclass="Content" MaxLength="75"></asp:textbox></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">Address:&nbsp;</TD>
																	<TD class="Content" vAlign="top" noWrap><asp:textbox id="Ship_Address1" runat="server" cssclass="Content" MaxLength="255"></asp:textbox><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">Address2:&nbsp;</TD>
																	<TD class="Content" vAlign="top"><asp:textbox id="Ship_Address2" runat="server" cssclass="Content" MaxLength="255"></asp:textbox></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">City:&nbsp;</TD>
																	<TD class="Content" vAlign="top" noWrap><asp:textbox id="Ship_City" runat="server" cssclass="Content" MaxLength="50"></asp:textbox><asp:label id="Label6" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">State/Province:&nbsp;</TD>
																	<TD class="Content" vAlign="top" noWrap><cc1:selectvalcontrol id="Ship_State" runat="server" Width="204px" DisplaySelect="States">
																			<asp:ListItem Value="States">[States]</asp:ListItem>
																		</cc1:selectvalcontrol><FONT color="#ff0000"></FONT></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">Postal Code:&nbsp;</TD>
																	<TD class="Content" vAlign="top" noWrap><asp:textbox id="Ship_Zip" runat="server" cssclass="Content" MaxLength="50"></asp:textbox></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">Country:&nbsp;</TD>
																	<TD class="Content" vAlign="top" noWrap><cc1:selectvalcontrol id="Ship_Country" runat="server" Width="205px" DisplaySelect="Country">
																			<asp:ListItem Value="Country">[Countries]</asp:ListItem>
																		</cc1:selectvalcontrol><FONT color="#ff0000"></FONT></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">Phone:&nbsp;</TD>
																	<TD class="Content" vAlign="top" noWrap><asp:textbox id="Ship_Phone" runat="server" cssclass="Content"></asp:textbox><asp:label id="Label10" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="Content" vAlign="top" noWrap align="right">Fax:&nbsp;</TD>
																	<TD class="Content" vAlign="top"><asp:textbox id="Ship_Fax" runat="server" cssclass="Content"></asp:textbox></TD>
																	<td class="Content">&nbsp;</td>
																	<TD class="ContentTable" style="WIDTH: 1px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="WIDTH: 1px; HEIGHT: 21px"><IMG src="images/clear.gif" width="1"></TD>
																	<TD class="Content" style="HEIGHT: 21px"></TD>
																	<TD class="Content" noWrap align="right" style="HEIGHT: 21px"></TD>
																	<TD class="Content" style="HEIGHT: 21px" vAlign="middle" align="right">&nbsp;Use 
																		Same For Billing Address:
																		<asp:CheckBox id="chk_SameBill" runat="server" Checked="True" EnableViewState="False" ToolTip="Check to use the same address fo billing"></asp:CheckBox></TD>
																	<TD class="Content" style="HEIGHT: 21px"></TD>
																	<TD class="ContentTable" style="WIDTH: 1px; HEIGHT: 21px"><IMG src="images/clear.gif" width="1"></TD>
																</TR>
																<TR>
																	<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif"></TD>
																	<TD class="ContentTable" style="HEIGHT: 1px" colSpan="4"><IMG height="1" src="images/clear.gif"></TD>
																	<TD class="ContentTable" style="HEIGHT: 1px"><IMG height="1" src="images/clear.gif"></TD>
																</TR>
																<TR>
																	<TD class="Content" colSpan="6">&nbsp;</TD>
																</TR>
																<TR>
																	<TD align="right" colSpan="6"><asp:linkbutton id="btnContinue" Runat="server">
																			<asp:Image BorderWidth="0" ID="imgContinue" Runat="server" AlternateText="Continue"></asp:Image>
																		</asp:linkbutton></TD>
																</TR>
															</TABLE>
														</TD>
														<TD width="10">&nbsp;</TD>
													</TR>
													<TR>
														<TD class="Content">&nbsp;</TD>
													</TR>
												</TABLE>
											</td>
										</tr>
									</table>
									<!-- Content End --></td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start --><uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
									<!-- Right Column End --></td>
							</tr>
							<tr>
								<td class="Footer" id="FooterCell" colSpan="3">
									<!-- Footer Start --><uc1:footer id="Footer1" runat="server"></uc1:footer>
									<!-- Footer End --></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

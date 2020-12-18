<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Billing.aspx.vb" Inherits="StoreFront.StoreFront.Billing"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Billing Information</title>
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
				window.document.Form2.elements["Bill_NickName"].required=true;
				window.document.Form2.elements["Bill_NickName"].title="Save As";
				window.document.Form2.elements["Bill_FirstName"].required=true;
				window.document.Form2.elements["Bill_FirstName"].title="First Name";
				window.document.Form2.elements["Bill_LastName"].required=true;
				window.document.Form2.elements["Bill_LastName"].title="Last Name";
				window.document.Form2.elements["Bill_Address1"].required=true;
				window.document.Form2.elements["Bill_Address1"].title="Address";
				window.document.Form2.elements["Bill_City"].required=true;
				window.document.Form2.elements["Bill_City"].title="City";
				window.document.Form2.elements["Bill_Phone"].required=true;
				window.document.Form2.elements["Bill_Phone"].phonenumber=true;
				window.document.Form2.elements["Bill_Phone"].title="Phone";
				window.document.Form2.elements["Bill_Fax"].phonenumber=true;
				window.document.Form2.elements["Bill_Fax"].title="Fax";
				
				if (window.document.Form2.elements["Bill_Country"].value == "US" ||
					window.document.Form2.elements["Bill_Country"].value == "CA")
				{
					window.document.Form2.elements["Bill_Zip"].required=true;
					window.document.Form2.elements["Bill_Zip"].title="Postal Code";
				}
				else
				{
					window.document.Form2.elements["Bill_Zip"].required=false;
				}

				return ValidateForm(window.document.Form2)
			}
			//-->
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server" MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" runat="server">
				<tr>
					<td id="PageCell">
						<table class="GeneralTable" id="PageSubTable" cellSpacing="0" runat="server">
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
												<P id="ErrorAlignment" runat="server" align="center">
													<font color="#ff0000">
														<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
													</font>
												</P>
												<P>
													<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD class="Headings">Billing&nbsp;and Account Information</TD>
														</TR>
														<TR>
															<TD class="Content">&nbsp;</TD>
														</TR>
														<TR>
															<TD>
																<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD class="ContentTableHeader" height="1"><IMG height="1" src="images/clear.gif"></TD>
																		<TD class="ContentTableHeader" height="1">&nbsp;</TD>
																		<TD class="ContentTableHeader" style="HEIGHT: 12px">Select From Address Book:
																			<asp:dropdownlist id="BillAddressList" runat="server" AutoPostBack="True" DataValueField="ID" DataTextField="NickName"></asp:dropdownlist></TD>
																		<TD class="ContentTableHeader" height="1">&nbsp;</TD>
																		<TD class="ContentTableHeader" height="1"><IMG height="1" src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></TD>
																		<TD class="Content" height="1">&nbsp;</TD>
																		<TD>
																			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TR>
																					<TD class="Content" align="right" colSpan="2">&nbsp;</TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">Save As:&nbsp;
																					</TD>
																					<TD noWrap class="Content"><asp:textbox id="Bill_NickName" runat="server"></asp:textbox><asp:label id="Label1" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">First Name:&nbsp;</TD>
																					<TD noWrap class="Content"><asp:textbox id="Bill_FirstName" runat="server" MaxLength="100"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">Middle Initial:&nbsp;
																					</TD>
																					<TD class="Content"><asp:textbox id="Bill_MI" runat="server" Columns="3" MaxLength="2"></asp:textbox></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">Last Name:&nbsp;
																					</TD>
																					<TD class="Content" noWrap><asp:textbox id="Bill_LastName" runat="server"></asp:textbox><asp:label id="Label7" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">Company:&nbsp;</TD>
																					<TD class="Content"><asp:textbox id="Bill_Company" runat="server" MaxLength="75"></asp:textbox></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">Address:&nbsp;</TD>
																					<TD class="Content" noWrap><asp:textbox id="Bill_Address1" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">Address2:&nbsp;</TD>
																					<TD class="Content"><asp:textbox id="Bill_Address2" runat="server" MaxLength="255"></asp:textbox></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">City:&nbsp;</TD>
																					<TD class="Content" noWrap><asp:textbox id="Bill_City" runat="server" MaxLength="50"></asp:textbox><asp:label id="Label4" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">State/Province:&nbsp;</TD>
																					<TD class="Content" noWrap><cc1:selectvalcontrol id="Bill_State" runat="server" DisplaySelect="States" Width="206px">
																							<asp:ListItem Value="States">[States]</asp:ListItem>
																						</cc1:selectvalcontrol><FONT color="#ff0000"></FONT></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">Postal Code:&nbsp;</TD>
																					<TD class="Content" noWrap><asp:textbox id="Bill_Zip" runat="server"></asp:textbox></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">Country:&nbsp;</TD>
																					<TD class="Content" noWrap><cc1:selectvalcontrol id="Bill_Country" runat="server" DisplaySelect="Country" Width="206px">
																							<asp:ListItem Value="States">[States]</asp:ListItem>
																						</cc1:selectvalcontrol><FONT color="#ff0000"></FONT></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">Phone:&nbsp;</TD>
																					<TD class="Content" noWrap><asp:textbox id="Bill_Phone" runat="server"></asp:textbox><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right">Fax:&nbsp;</TD>
																					<TD class="Content"><asp:textbox id="Bill_Fax" runat="server"></asp:textbox></TD>
																				</TR>
																				<TR>
																					<TD class="Content" noWrap align="right"></TD>
																					<TD class="Content">&nbsp;</TD>
																				</TR>
																			</TABLE>
																		</TD>
																		<TD class="Content" height="1">&nbsp;</TD>
																		<TD class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></TD>
																	</TR>
																	<TR>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																		<TD class="ContentTable" height="1" colspan="3"><IMG height="1" src="images/clear.gif" width="1"></TD>
																		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD class="Content">&nbsp;
															</TD>
														</TR>
														<TR>
															<TD align="right">
																<asp:LinkButton ID="btnContinue" Runat="server">
																	<asp:Image BorderWidth="0" ID="imgContinue" Runat="server" AlternateText="Continue"></asp:Image>
																</asp:LinkButton>
															</TD>
														</TR>
													</TABLE>
												</P>
											</td>
										</tr>
									</table>
									<!-- Content End --></td>
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

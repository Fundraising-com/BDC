<%@ Register TagPrefix="uc1" TagName="CAddressBook" Src="Controls/CAddressBook.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="custaddressbook.aspx.vb" Inherits="StoreFront.StoreFront.CustAddressBook" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Customer Address Book</title>
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
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
		<!--
			function SetValidation()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["NickName"].required=true;
				window.document.Form2.elements["NickName"].title="Save As";
				window.document.Form2.elements["FirstName"].required=true;
				window.document.Form2.elements["FirstName"].title="First Name";
				window.document.Form2.elements["LastName"].required=true;
				window.document.Form2.elements["LastName"].title="Last Name";
				window.document.Form2.elements["Address1"].required=true;
				window.document.Form2.elements["Address1"].title="Address";
				window.document.Form2.elements["City"].required=true;
				window.document.Form2.elements["City"].title="City";
				window.document.Form2.elements["Phone"].required=true;
				window.document.Form2.elements["Phone"].phonenumber=true;
				window.document.Form2.elements["Phone"].title="Phone";
				window.document.Form2.elements["Fax"].phonenumber=true;
				window.document.Form2.elements["Fax"].title="Fax";
				
				if (window.document.Form2.elements["Country"].value == "US" ||
					window.document.Form2.elements["Country"].value == "CA")
				{
					window.document.Form2.elements["Zip"].required=true;
					window.document.Form2.elements["Zip"].title="Postal Code";
				}
				else
				{
					window.document.Form2.elements["Zip"].required=false;
				}

				return ValidateForm(window.document.Form2)
			}
			//-->
		</script>
	</HEAD>
	<body id="BodyTag" runat="server" class="GeneralPage">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1" runat="server">
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
											<td class="Instructions">
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content">
												<P id="ErrorAlignment" runat="server" align="center">
													<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
												<P id="MessageAlignment" runat="server" align="center">
													<asp:Label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:Label>
												</P>
												<P><asp:label id="Label1" runat="server" CssClass="Headings">My Address Book</asp:label>
													<asp:TextBox id="ReturnPage" runat="server" Visible="False"></asp:TextBox></P>
												<P>
													<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" width="100%">
														<TR>
															<TD vAlign="top" width="50%">
																<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0" width="100%">
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap colSpan="2"><b>
																				<asp:Label id="NewEditLabel" runat="server">Add An Address</asp:Label></b></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<tr>
																		<TD height="10">&nbsp;</TD>
																		<td class="Content" noWrap align="right">Save As:</td>
																		<td><asp:textbox id="NickName" runat="server" MaxLength="50"></asp:textbox><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></td>
																		<TD height="10">&nbsp;</TD>
																	</tr>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">First Name:</TD>
																		<TD><asp:textbox id="FirstName" runat="server" MaxLength="100"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Middle Initial:</TD>
																		<TD><asp:textbox id="MI" runat="server" Width="33px" MaxLength="2"></asp:textbox></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Last Name:</TD>
																		<TD><asp:textbox id="LastName" runat="server" MaxLength="100"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10"></TD>
																		<TD class="Content" noWrap align="right">Company:</TD>
																		<TD>
																			<asp:TextBox id="Company" runat="server" MaxLength="75"></asp:TextBox></TD>
																		<TD height="10"></TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Address 1:</TD>
																		<TD><asp:textbox id="Address1" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label6" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Address 2:</TD>
																		<TD><asp:textbox id="Address2" runat="server" MaxLength="255"></asp:textbox></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">City:</TD>
																		<TD><asp:textbox id="City" runat="server" MaxLength="50"></asp:textbox><asp:label id="Label7" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="16" style="HEIGHT: 16px">&nbsp;</TD>
																		<TD class="Content" noWrap align="right" style="HEIGHT: 16px">State/Province:</TD>
																		<TD style="HEIGHT: 16px">
																			<cc1:SelectValControl id="State" runat="server" Width="206px"></cc1:SelectValControl><asp:label id="Label8" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																		<TD height="16" style="HEIGHT: 16px">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Postal Code:</TD>
																		<TD><asp:textbox id="Zip" runat="server" MaxLength="50"></asp:textbox></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Country:</TD>
																		<TD>
																			<cc1:SelectValControl id="Country" runat="server" Width="203px" DisplaySelect="Country"></cc1:SelectValControl></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Phone:</TD>
																		<TD><asp:textbox id="Phone" runat="server"></asp:textbox><asp:label id="Label9" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right">Fax:</TD>
																		<TD><asp:textbox id="Fax" runat="server"></asp:textbox></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<tr>
																		<td colspan="3">&nbsp;</td>
																	</tr>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right" colSpan="2">
																			<asp:TextBox id="AddressID" runat="server" Visible="False"></asp:TextBox>
																			<asp:LinkButton ID="btnAdd" Runat="server">
																				<asp:Image BorderWidth="0" ID="imgAdd" Runat="server" AlternateText="Add"></asp:Image>
																			</asp:LinkButton>
																			&nbsp;
																			<asp:LinkButton ID="btnClear" Runat="server">
																				<asp:Image BorderWidth="0" ID="imgClear" Runat="server" AlternateText="Clear"></asp:Image>
																			</asp:LinkButton>
																		</TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap align="right" colSpan="2">
																			<asp:LinkButton ID="btnSave" Runat="server">
																				<asp:Image BorderWidth="0" ID="imgSave" Runat="server" AlternateText="Save"></asp:Image>
																			</asp:LinkButton>
																			&nbsp;
																			<asp:LinkButton ID="btnCancel" Runat="server">
																				<asp:Image BorderWidth="0" ID="imgCancel" Runat="server" AlternateText="Cancel"></asp:Image>
																			</asp:LinkButton>
																		</TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																</TABLE>
															</TD>
															<TD width="1" bgColor="#000000"><IMG height="1" src="images/black.gif" width="1"></TD>
															<TD vAlign="top" width="50%">
																<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0" width="100%">
																	<TR>
																		<TD height="10">&nbsp;</TD>
																		<TD class="Content" noWrap><b>Saved Addresses</b></TD>
																		<TD height="10">&nbsp;</TD>
																	</TR>
																	<tr>
																		<TD height="10">&nbsp;</TD>
																		<td><uc1:caddressbook id="CAddressBook1" runat="server"></uc1:caddressbook></td>
																		<TD height="10">&nbsp;</TD>
																	</tr>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD class="Content" vAlign="top" align="right" width="50%">
																<asp:LinkButton ID="btnContinue" Runat="server">
																	<asp:Image BorderWidth="0" ID="imgContinue" Runat="server" AlternateText="Continue"></asp:Image>
																</asp:LinkButton>
															</TD>
															<TD width="1" bgColor="#000000"></TD>
															<TD vAlign="top" width="50%"></TD>
														</TR>
													</TABLE>
												</P>
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
